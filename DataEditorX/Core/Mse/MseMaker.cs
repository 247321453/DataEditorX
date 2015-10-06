/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-12
 * 时间: 12:48
 * 
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

using System.Drawing;
using DataEditorX.Core.Info;
using DataEditorX.Config;
using DataEditorX.Language;
using DataEditorX.Common;
using System.Windows.Forms;
using System.Threading;

namespace DataEditorX.Core.Mse
{
	/// <summary>
	/// MSE制作
	/// </summary>
	public class MseMaker
	{
		#region 常量
		public const string TAG_CARD = "card";
		public const string TAG_CARDTYPE = "card type";
		public const string TAG_NAME = "name";
		public const string TAG_ATTRIBUTE = "attribute";
		public const string TAG_LEVEL = "level";
		public const string TAG_IMAGE = "image";
		/// <summary>种族</summary>
		public const string TAG_TYPE1 = "type 1";
		/// <summary>效果1</summary>
		public const string TAG_TYPE2 = "type 2";
		/// <summary>效果2/summary>
		public const string TAG_TYPE3 = "type 3";
		/// <summary>效果3</summary>
		public const string TAG_TYPE4 = "type 4";
		public const string TAG_TEXT = "rule text";
		public const string TAG_ATK = "attack";
		public const string TAG_DEF = "defense";
		public const string TAG_PENDULUM = "pendulum";
		public const string TAG_PSCALE1 = "pendulum scale 1";
		public const string TAG_PSCALE2 = "pendulum scale 2";
		public const string TAG_PEND_TEXT = "pendulum text";
		public const string TAG_CODE = "gamecode";
		public const string UNKNOWN_ATKDEF = "?";
		public const int UNKNOWN_ATKDEF_VALUE = -2;
		public const string TAG_REP_TEXT = "%text%";
		public const string TAG_REP_PTEXT = "%ptext%";
		#endregion

		#region 成员，初始化
		MSEConfig cfg;
		public int MaxNum
		{
			get { return cfg.maxcount; }
		}

		public string ImagePath
		{
			get { return cfg.imagepath; }
		}

		public MseMaker(MSEConfig mcfg)
		{
			SetConfig(mcfg);
		}
		public void SetConfig(MSEConfig mcfg)
		{
			cfg = mcfg;
		}
		public MSEConfig GetConfig()
		{
			return cfg;
		}
		#endregion

		#region 数据处理
		//合并
		public string GetLine(string key, string word)
		{
			return "	" + key + ": " + word;
		}
		//特殊字
		public string reItalic(string str)
		{
			str = cn2tw(str);
			foreach (string rs in cfg.replaces.Keys)
			{
				str = Regex.Replace(str, rs, cfg.replaces[rs]);
			}
			return str;
		}
		//简体转繁体
		public string cn2tw(string str)
		{
			if (cfg.Iscn2tw)
			{
				str = Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
				str = str.Replace("巖", "岩");
			}
			return str;
		}
		//获取魔法陷阱的类型符号
		public string GetSpellTrapSymbol(Card c, bool isSpell)
		{
			string level;
			if (c.IsType(CardType.TYPE_EQUIP))
				level = MseSpellTrap.EQUIP;
			else if (c.IsType(CardType.TYPE_QUICKPLAY))
				level = MseSpellTrap.QUICKPLAY;
			else if (c.IsType(CardType.TYPE_FIELD))
				level = MseSpellTrap.FIELD;
			else if (c.IsType(CardType.TYPE_CONTINUOUS))
				level = MseSpellTrap.CONTINUOUS;
			else if (c.IsType(CardType.TYPE_RITUAL))
				level = MseSpellTrap.RITUAL;
			else if (c.IsType(CardType.TYPE_COUNTER))
				level = MseSpellTrap.COUNTER;
			else if (cfg.str_spell == MSEConfig.TAG_REP && cfg.str_trap == MSEConfig.TAG_REP)
				level = MseSpellTrap.NORMAL;//带文字的图片
			else
				level = "";

			if (isSpell)
				level = cfg.str_spell.Replace(MSEConfig.TAG_REP, level);
			else
				level = cfg.str_trap.Replace(MSEConfig.TAG_REP, level);
			return level;
		}
		//获取图片路径
		public static string GetCardImagePath(string picpath, Card c)
		{
			//密码，带0密码，卡名
			string jpg = MyPath.Combine(picpath, c.id + ".jpg");
			string jpg2 = MyPath.Combine(picpath, c.idString + ".jpg");
			string jpg3 = MyPath.Combine(picpath, c.name + ".jpg");
			string png = MyPath.Combine(picpath, c.id + ".png");
			string png2 = MyPath.Combine(picpath, c.idString + ".png");
			string png3 = MyPath.Combine(picpath, c.name + ".png");
			if (File.Exists(jpg))
			{
				return jpg;
			}
			else if (File.Exists(jpg2))
			{
				return jpg2;
			}
			else if (File.Exists(jpg3))
			{
				File.Copy(jpg3, jpg, true);
				if (File.Exists(jpg))
				{//复制失败
					return jpg;
				}
			}
			else if (File.Exists(png))
			{
				return png;
			}
			else if (File.Exists(png2))
			{
				return png2;
			}
			else if (File.Exists(png3))
			{
				File.Copy(png3, png, true);
				if (File.Exists(png))
				{//复制失败
					return png;
				}
			}
			return "";
		}
		//获取属性
		public static string GetAttribute(int attr)
		{
			CardAttribute cattr = (CardAttribute)attr;
			string sattr = MseAttribute.NONE;
			switch (cattr)
			{
				case CardAttribute.ATTRIBUTE_DARK:
					sattr = MseAttribute.DARK;
					break;
				case CardAttribute.ATTRIBUTE_DEVINE:
					sattr = MseAttribute.DIVINE;
					break;
				case CardAttribute.ATTRIBUTE_EARTH:
					sattr = MseAttribute.EARTH;
					break;
				case CardAttribute.ATTRIBUTE_FIRE:
					sattr = MseAttribute.FIRE;
					break;
				case CardAttribute.ATTRIBUTE_LIGHT:
					sattr = MseAttribute.LIGHT;
					break;
				case CardAttribute.ATTRIBUTE_WATER:
					sattr = MseAttribute.WATER;
					break;
				case CardAttribute.ATTRIBUTE_WIND:
					sattr = MseAttribute.WIND;
					break;
			}
			return sattr;
		}
		//获取效果文本
		public static string GetDesc(string cdesc, string regx)
		{
			string desc = cdesc;
			desc = desc.Replace("\r\n", "\n");
			desc = desc.Replace("\r", "\n");
			Regex regex = new Regex(regx, RegexOptions.Multiline);
			Match mc = regex.Match(desc);
			if (mc.Success)
				return ((mc.Groups.Count > 1) ?
				        mc.Groups[1].Value : mc.Groups[0].Value);
			return "";
		}

		public string ReText(string text)
		{
			text = text.Trim('\n');
			StringBuilder sb = new StringBuilder(text);
			sb.Replace("\r\n", "\n");
			sb.Replace("\r", "\n");
			sb.Replace("\n\n", "\n");
			sb.Replace("\n", "\n\t\t");
			return sb.ToString();
		}
		//获取星星
		public static string GetStar(long level)
		{
			long j = level & 0xff;
			string star = "";
			for (int i = 0; i < j; i++)
			{
				star += "*";
			}
			return star;
		}
		//获取种族
		public string GetRace(long race)
		{
			if (cfg.raceDic.ContainsKey(race))
				return cfg.raceDic[race].Trim();
			return race.ToString("x");
		}
		//获取类型文字
		public string GetType(CardType ctype)
		{
			long type = (long)ctype;
			if (cfg.typeDic.ContainsKey(type))
				return cfg.typeDic[type].Trim();
			return type.ToString("x");
		}

		//获取卡片类型
		public string[] GetTypes(Card c)
		{
			//卡片类型，效果1，效果2，效果3
			string[] types = new string[] {
				MseCardType.CARD_NORMAL, "", "", "" };
			if (c.IsType(CardType.TYPE_MONSTER))
			{//卡片类型和第1效果
				if (c.IsType(CardType.TYPE_XYZ))
				{
					types[0] = MseCardType.CARD_XYZ;
					types[1] = GetType(CardType.TYPE_XYZ);
				}
				else if (c.IsType(CardType.TYPE_TOKEN))
				{
					types[0] = (c.race == 0) ?
						MseCardType.CARD_TOKEN2
						: MseCardType.CARD_TOKEN;
					types[1] = GetType(CardType.TYPE_TOKEN);
				}
				else if (c.IsType(CardType.TYPE_RITUAL))
				{
					types[0] = MseCardType.CARD_RITUAL;
					types[1] = GetType(CardType.TYPE_RITUAL);
				}
				else if (c.IsType(CardType.TYPE_FUSION))
				{
					types[0] = MseCardType.CARD_FUSION;
					types[1] = GetType(CardType.TYPE_FUSION);
				}
				else if (c.IsType(CardType.TYPE_SYNCHRO))
				{
					types[0] = MseCardType.CARD_SYNCHRO;
					types[1] = GetType(CardType.TYPE_SYNCHRO);
				}
				else if (c.IsType(CardType.TYPE_EFFECT))
				{
					types[0] = MseCardType.CARD_EFFECT;
				}
				else
					types[0] = MseCardType.CARD_NORMAL;
				//同调
				if (types[0] == MseCardType.CARD_SYNCHRO
				    || types[0] == MseCardType.CARD_TOKEN)
				{
					if (c.IsType(CardType.TYPE_TUNER)
					    && c.IsType(CardType.TYPE_EFFECT))
					{//调整效果
						types[2] = GetType(CardType.TYPE_TUNER);
						types[3] = GetType(CardType.TYPE_EFFECT);
					}
					else if (c.IsType(CardType.TYPE_TUNER))
					{
						types[2] = GetType(CardType.TYPE_TUNER);
					}
					else if (c.IsType(CardType.TYPE_EFFECT))
					{
						types[2] = GetType(CardType.TYPE_EFFECT);
					}
				}
				else if (types[0] == MseCardType.CARD_NORMAL)
				{
					if (c.IsType(CardType.TYPE_PENDULUM))//灵摆
						types[1] = GetType(CardType.TYPE_PENDULUM);
					else if (c.IsType(CardType.TYPE_TUNER))//调整
						types[1] = GetType(CardType.TYPE_TUNER);
				}
				else if (types[0] != MseCardType.CARD_EFFECT)
				{//效果
					if (c.IsType(CardType.TYPE_EFFECT))
						types[2] = GetType(CardType.TYPE_EFFECT);
				}
				else
				{//效果怪兽
					types[2] = GetType(CardType.TYPE_EFFECT);
					if (c.IsType(CardType.TYPE_PENDULUM))
						types[1] = GetType(CardType.TYPE_PENDULUM);
					else if (c.IsType(CardType.TYPE_TUNER))
						types[1] = GetType(CardType.TYPE_TUNER);
					else if (c.IsType(CardType.TYPE_SPIRIT))
						types[1] = GetType(CardType.TYPE_SPIRIT);
					else if (c.IsType(CardType.TYPE_TOON))
						types[1] = GetType(CardType.TYPE_TOON);
					else if (c.IsType(CardType.TYPE_UNION))
						types[1] = GetType(CardType.TYPE_UNION);
					else if (c.IsType(CardType.TYPE_DUAL))
						types[1] = GetType(CardType.TYPE_DUAL);
					else if (c.IsType(CardType.TYPE_FLIP))
						types[1] = GetType(CardType.TYPE_FLIP);
					else
					{
						types[1] = GetType(CardType.TYPE_EFFECT);
						types[2] = "";
					}
				}

			}
			if (c.race == 0)//如果没有种族
			{
				types[1] = "";
				types[2] = "";
			}
			return types;
		}
		#endregion

		#region 写存档
		//写存档
		public Dictionary<Card, string> WriteSet(string file, Card[] cards)
		{
//			MessageBox.Show(""+cfg.replaces.Keys[0]+"/"+cfg.replaces[cfg.replaces.Keys[0]]);
			Dictionary<Card, string> list = new Dictionary<Card, string>();
			string pic = cfg.imagepath;
			using (FileStream fs = new FileStream(file,
			                                      FileMode.Create, FileAccess.Write))
			{
				StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
				sw.WriteLine(cfg.head);
				foreach (Card c in cards)
				{
					string jpg = GetCardImagePath(pic, c);
					if (!string.IsNullOrEmpty(jpg))
					{
						list.Add(c, jpg);
						jpg = Path.GetFileName(jpg);
					}
					if (c.IsType(CardType.TYPE_SPELL) || c.IsType(CardType.TYPE_TRAP))
						sw.WriteLine(getSpellTrap(c, jpg, c.IsType(CardType.TYPE_SPELL)));
					else
						sw.WriteLine(getMonster(c, jpg, c.IsType(CardType.TYPE_PENDULUM)));
				}
				sw.WriteLine(cfg.end);
				sw.Close();
			}

			return list;
		}
		//怪兽，pendulum怪兽
		string getMonster(Card c, string img, bool isPendulum)
		{
			StringBuilder sb = new StringBuilder();
			string[] types = GetTypes(c);
			string race = GetRace(c.race);
			sb.AppendLine(TAG_CARD + ":");
			sb.AppendLine(GetLine(TAG_CARDTYPE, types[0]));
			sb.AppendLine(GetLine(TAG_NAME, reItalic(c.name)));
			sb.AppendLine(GetLine(TAG_ATTRIBUTE, GetAttribute(c.attribute)));
			sb.AppendLine(GetLine(TAG_LEVEL, GetStar(c.level)));
			sb.AppendLine(GetLine(TAG_IMAGE, img));
			sb.AppendLine(GetLine(TAG_TYPE1, cn2tw(race)));
			sb.AppendLine(GetLine(TAG_TYPE2, cn2tw(types[1])));
			sb.AppendLine(GetLine(TAG_TYPE3, cn2tw(types[2])));
			sb.AppendLine(GetLine(TAG_TYPE4, cn2tw(types[3])));
			if (isPendulum)//P怪兽
			{
				string text = GetDesc(c.desc, cfg.regx_monster);
				if (string.IsNullOrEmpty(text))
					text = c.desc;
				sb.AppendLine("	" + TAG_TEXT + ":");
				//sb.AppendLine(cfg.regx_monster + ":" + cfg.regx_pendulum);
				sb.AppendLine("		" + ReText(reItalic(text)));
				sb.AppendLine(GetLine(TAG_PENDULUM, "medium"));
				sb.AppendLine(GetLine(TAG_PSCALE1, ((c.level >> 0x18) & 0xff).ToString()));
				sb.AppendLine(GetLine(TAG_PSCALE2, ((c.level >> 0x10) & 0xff).ToString()));
				sb.AppendLine("	" + TAG_PEND_TEXT + ":");
				sb.AppendLine("		" + ReText(reItalic(GetDesc(c.desc, cfg.regx_pendulum))));
			}
			else//一般怪兽
			{
				sb.AppendLine("	" + TAG_TEXT + ":");
				sb.AppendLine("		" + ReText(reItalic(c.desc)));
			}
			sb.AppendLine(GetLine(TAG_ATK, (c.atk < 0) ? UNKNOWN_ATKDEF : c.atk.ToString()));
			sb.AppendLine(GetLine(TAG_DEF, (c.def < 0) ? UNKNOWN_ATKDEF : c.def.ToString()));

			sb.AppendLine(GetLine(TAG_CODE, c.idString));
			return sb.ToString();
		}
		//魔法陷阱
		string getSpellTrap(Card c, string img, bool isSpell)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(TAG_CARD + ":");
			sb.AppendLine(GetLine(TAG_CARDTYPE, isSpell ? "spell card" : "trap card"));
			sb.AppendLine(GetLine(TAG_NAME, reItalic(c.name)));
			sb.AppendLine(GetLine(TAG_ATTRIBUTE, isSpell ? "spell" : "trap"));
			sb.AppendLine(GetLine(TAG_LEVEL, GetSpellTrapSymbol(c, isSpell)));
			sb.AppendLine(GetLine(TAG_IMAGE, img));
			sb.AppendLine("	" + TAG_TEXT + ":");
			sb.AppendLine("		" + ReText(reItalic(c.desc)));
			sb.AppendLine(GetLine(TAG_CODE, c.idString));
			return sb.ToString();
		}
		#endregion

		#region 读存档
		public static int GetAttributeInt(string cattr)
		{
			int iattr = 0;
			switch (cattr)
			{
				case MseAttribute.DARK:
					iattr = (int)CardAttribute.ATTRIBUTE_DARK;
					break;
				case MseAttribute.DIVINE:
					iattr = (int)CardAttribute.ATTRIBUTE_DEVINE;
					break;
				case MseAttribute.EARTH:
					iattr = (int)CardAttribute.ATTRIBUTE_EARTH;
					break;
				case MseAttribute.FIRE:
					iattr = (int)CardAttribute.ATTRIBUTE_FIRE;
					break;
				case MseAttribute.LIGHT:
					iattr = (int)CardAttribute.ATTRIBUTE_LIGHT;
					break;
				case MseAttribute.WATER:
					iattr = (int)CardAttribute.ATTRIBUTE_WATER;
					break;
				case MseAttribute.WIND:
					iattr = (int)CardAttribute.ATTRIBUTE_WIND;
					break;
			}
			return iattr;
		}
		long GetRaceInt(string race)
		{
			if (!string.IsNullOrEmpty(race))
			{
				foreach (long key in cfg.raceDic.Keys)
				{
					if (race.Equals(cfg.raceDic[key]))
						return key;
				}
			}
			return (long)CardRace.RACE_NONE;
		}
		long GetTypeInt(string type)
		{
			if (!string.IsNullOrEmpty(type))
			{
				foreach (long key in cfg.typeDic.Keys)
				{
					if (type.Equals(cfg.typeDic[key]))
						return key;
				}
			}
			return 0;
		}
		static string GetValue(string content, string tag)
		{
			Regex regx = new Regex(@"^[\t]+?" + tag + @":([\s\S]*?)$", RegexOptions.Multiline);
			Match m = regx.Match(content);
			if (m.Success)
			{
				if (m.Groups.Count >= 2)
					return RemoveTag(m.Groups[1].Value);
			}
			return "";
		}
		//多行
		static string GetMultiValue(string content, string tag)
		{
			//TODO
			content = content.Replace("\t\t", "");
			Regex regx = new Regex(@"^[\t]+?" + tag + @":([\S\s]*?)^\t[\S\s]+?:", RegexOptions.Multiline);
			Match m = regx.Match(content);
			if (m.Success)
			{
				if (m.Groups.Count >= 2)
				{
					string word = m.Groups[1].Value;
					return RemoveTag(word).Replace("^", "").Replace("\t", "");
				}
			}
			return "";
		}
		long GetSpellTrapType(string level)
		{
			long type = 0;
			//魔法陷阱
			if (level.Contains(MseSpellTrap.EQUIP))
				type = (long)CardType.TYPE_EQUIP;
			if (level.Contains(MseSpellTrap.QUICKPLAY))
				type = (long)CardType.TYPE_QUICKPLAY;
			if (level.Contains(MseSpellTrap.FIELD))
				type = (long)CardType.TYPE_FIELD;
			if (level.Contains(MseSpellTrap.CONTINUOUS))
				type = (long)CardType.TYPE_CONTINUOUS;
			if (level.Contains(MseSpellTrap.RITUAL))
				type = (long)CardType.TYPE_RITUAL;
			if (level.Contains(MseSpellTrap.COUNTER))
				type = (long)CardType.TYPE_COUNTER;
			return type;
		}

		long GetMonsterType(string cardtype)
		{
			long type = 0;
			if (cardtype.Equals(MseCardType.CARD_SPELL))
				type = (long)CardType.TYPE_SPELL;
			else if (cardtype.Equals(MseCardType.CARD_TRAP))
				type = (long)CardType.TYPE_TRAP;
			else
			{
				type = (long)CardType.TYPE_MONSTER;
				switch (cardtype)
				{
					case MseCardType.CARD_NORMAL:
						type |= (long)CardType.TYPE_NORMAL;
						break;
					case MseCardType.CARD_EFFECT:
						type |= (long)CardType.TYPE_EFFECT;
						break;
					case MseCardType.CARD_XYZ:
						type |= (long)CardType.TYPE_XYZ;
						break;
					case MseCardType.CARD_RITUAL:
						type |= (long)CardType.TYPE_RITUAL;
						break;
					case MseCardType.CARD_FUSION:
						type |= (long)CardType.TYPE_FUSION;
						break;
					case MseCardType.CARD_TOKEN:
					case MseCardType.CARD_TOKEN2:
						type |= (long)CardType.TYPE_TOKEN;
						break;
					case MseCardType.CARD_SYNCHRO:
						type |= (long)CardType.TYPE_SYNCHRO;
						break;
					default:
						type |= (long)CardType.TYPE_NORMAL;
						break;
				}
			}
			return type;
		}
		//卡片类型
		long GetCardType(string cardtype, string level, string type1,
		                 string type2, string type3)
		{
			long type = 0;
			//魔法陷阱
			type |= GetSpellTrapType(level);
			//怪兽
			type |= GetMonsterType(cardtype);
			//type2-4是识别怪兽效果类型
			type |= GetTypeInt(type1);
			type |= GetTypeInt(type2);
			type |= GetTypeInt(type3);
			return type;
		}

		static string RemoveTag(string word)
		{
			//移除标签<>
			word = Regex.Replace(word, "<[^>]+?>", "");
			return word.Trim().Replace("\t", "");
		}
		//解析卡片
		public Card ReadCard(string content, out string img)
		{
			string tmp;
			int itmp;
			Card c = new Card();
			c.ot = (int)CardRule.OCGTCG;
			//卡名
			c.name = GetValue(content, TAG_NAME);
			tmp = GetValue(content, TAG_LEVEL);
			//卡片种族
			c.race = GetRaceInt(GetValue(content, TAG_TYPE1));
			//卡片类型
			c.type = GetCardType(GetValue(content, TAG_CARDTYPE), tmp,
			                     GetValue(content, TAG_TYPE2),
			                     GetValue(content, TAG_TYPE3),
			                     GetValue(content, TAG_TYPE4));
			long t = GetSpellTrapType(GetValue(content, TAG_LEVEL));
			//不是魔法，陷阱卡片的星数
			if (!(c.IsType(CardType.TYPE_SPELL)
			      || c.IsType(CardType.TYPE_TRAP)) && t == 0)
				c.level = GetValue(content, TAG_LEVEL).Length;

			//属性
			c.attribute = GetAttributeInt(GetValue(content, TAG_ATTRIBUTE));
			//密码
			long.TryParse(GetValue(content, TAG_CODE), out c.id);
			//ATK
			tmp = GetValue(content, TAG_ATK);
			if (tmp == UNKNOWN_ATKDEF)
				c.atk = UNKNOWN_ATKDEF_VALUE;
			else
				int.TryParse(tmp, out c.atk);
			//DEF
			tmp = GetValue(content, TAG_DEF);
			if (tmp == UNKNOWN_ATKDEF)
				c.def = UNKNOWN_ATKDEF_VALUE;
			else
				int.TryParse(tmp, out c.def);
			//图片
			img = GetValue(content, TAG_IMAGE);
			//摇摆
			if (c.IsType(CardType.TYPE_PENDULUM))
			{//根据预设的模版，替换内容
				tmp = cfg.temp_text.Replace(TAG_REP_TEXT,
				                            GetMultiValue(content,TAG_TEXT));
				tmp = tmp.Replace(TAG_REP_PTEXT,
				                  GetMultiValue(content, TAG_PEND_TEXT));
				c.desc = tmp;
			}
			else
				c.desc = GetMultiValue(content,TAG_TEXT);
			//摇摆刻度
			int.TryParse(GetValue(content, TAG_PSCALE1), out itmp);
			c.level += (itmp << 0x18);
			int.TryParse(GetValue(content, TAG_PSCALE2), out itmp);
			c.level += (itmp << 0x10);
			return c;
		}
		//读取所有卡片
		public Card[] ReadCards(string set, bool repalceOld)
		{
			List<Card> cards = new List<Card>();
			if (!File.Exists(set))
				return null;
			string allcontent = File.ReadAllText(set, Encoding.UTF8);

			Regex regx = new Regex(@"^card:[\S\s]+?gamecode:[\S\s]+?$",
			                       RegexOptions.Multiline);
			MatchCollection matchs = regx.Matches(allcontent);
			int i = 0;
			
			foreach (Match match in matchs)
			{
				string content = match.Groups[0].Value;
				i++;
				string img;
				Card c = ReadCard(content, out img);
				if (c.id <= 0)
					c.id = i;
				//添加卡片
				cards.Add(c);
				//已经解压出来的图片
				string saveimg = MyPath.Combine(cfg.imagepath, img);
				if (!File.Exists(saveimg))//没有解压相应的图片
					continue;
				//改名后的图片
				img = MyPath.Combine(cfg.imagepath, c.idString + ".jpg");
				if (img == saveimg)//文件名相同
					continue;
				if (File.Exists(img))
				{
					if (repalceOld)//如果存在，则备份原图
					{
						File.Delete(img + ".bak");//删除备份
						File.Move(img, img + ".bak");//备份
						File.Move(saveimg, img);//改名
					}
				}
				else
					File.Move(saveimg, img);
			}
			File.Delete(set);
			return cards.ToArray();
		}
		#endregion
		
		/// <summary>
		/// 图片缓存
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		public string getImageCache(string img,Card card){
			if(cfg.width<=0 && cfg.height<=0)
				return img;
			string md5=MyUtils.GetMD5HashFromFile(img);
			if(MyUtils.Md5isEmpty(md5)||cfg.imagecache==null){
				//md5为空
				return img;
			}
			string file = MyPath.Combine(cfg.imagecache, md5);
			if(!File.Exists(file)){
				//生成缓存
				Bitmap bmp=MyBitmap.readImage(file);
				//缩放
				if(card!=null && card.IsType(CardType.TYPE_PENDULUM)){
					bmp=MyBitmap.Zoom(bmp, cfg.pwidth,cfg.pheight);
				}else{
					bmp=MyBitmap.Zoom(bmp, cfg.width,cfg.height);
				}
				//保存文件
				MyBitmap.SaveAsJPEG(bmp, file,100);
			}
			return img;
		}
		private static void exportSetThread(object obj){
			string[] args=(string[])obj;
			if(args==null||args.Length<3){
				System.Windows.Forms.MessageBox.Show(Language.LanguageHelper.GetMsg(LMSG.exportMseImagesErr));
				return;
			}
			string mse_path=args[0];
			string setfile=args[1];
			string path=args[2];
			if(mse_path==null||mse_path.Length==0||setfile==null||setfile.Length==0){
				System.Windows.Forms.MessageBox.Show(Language.LanguageHelper.GetMsg(LMSG.exportMseImagesErr));
				return;
			}else{
				string cmd=" --export "+setfile.Replace("\\\\","\\").Replace("\\","/")+" {card.gamecode}.png";
				System.Diagnostics.Process   ie   =   new   System.Diagnostics.Process();
				ie.StartInfo.FileName   =   mse_path;
				ie.StartInfo.Arguments   =  cmd;
				ie.StartInfo.WorkingDirectory=path;
				MyPath.CreateDir(path);
				try{
					ie.Start();
					//等待结束，需要把当前方法放到线程里面
					ie.WaitForExit();
					ie.Close();
					System.Windows.Forms.MessageBox.Show(Language.LanguageHelper.GetMsg(LMSG.exportMseImages));
				}catch{
					
				}
			}
		}
		public static void exportSet(string mse_path,string setfile,string path){
			if(mse_path==null||mse_path.Length==0||setfile==null||setfile.Length==0){
				return;
			}
			ParameterizedThreadStart ParStart = new ParameterizedThreadStart(exportSetThread);
			Thread myThread = new Thread(ParStart);
			myThread.IsBackground=true;
			myThread.Start(new string[]{mse_path,setfile,path});
		}
	}
}
