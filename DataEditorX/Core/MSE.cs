/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-12
 * 时间: 12:48
 * 
 */
using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;
using System.Windows.Forms;

namespace DataEditorX.Core
{
	
	
	/// <summary>
	/// Description of MSE.
	/// </summary>
	public class MSE
	{
		/*
		 * 
		normal monster	通常怪兽
		effect monster	效果怪兽
		fusion monster	融合怪兽
		ritual monster	仪式怪兽
		synchro monster	同调怪兽
		token monster	衍生物
		xyz monster	超量怪兽
		spell card	魔法
		trap card	陷阱
		 */
		static bool isInit=false;
		static MSEConfig cfg;
		static Dictionary<long,string> mTypedic;
		static Dictionary<long,string> mRacedic;
		
		public static void Init(string path,
		                        Dictionary<long,string> typedic,
		                        Dictionary<long,string> racedic)
		{
			if(isInit)
				return;
			cfg=new MSEConfig(path);
			mTypedic = typedic;
			mRacedic = racedic;
			MSEConvert.Init(typedic, racedic);
		}

		public static void Save(string file, Card[] cards,string pic){
			
			string setFile=Path.Combine(Application.StartupPath, "set.tmp");
			string[] images=WriteSet(setFile, cards, pic);
			using(ZipStorer zips=ZipStorer.Create(file, ""))
			{
				zips.AddFile(setFile,"set","");
				foreach ( string img in images )
				{
					zips.AddFile(img, Path.GetFileNameWithoutExtension(img),"");
				}
				zips.Close();
			}
			File.Delete(setFile);
		}
		public static string[] WriteSet(string file,Card[] cards,string pic)
		{
			List<string> list=new List<string>();
			using(FileStream fs=new FileStream(file,
			                                   FileMode.Create, FileAccess.Write))
			{
				StreamWriter sw=new StreamWriter(fs, Encoding.UTF8);
				sw.WriteLine(cfg.head);
				foreach(Card c in cards)
				{
					string jpg=Path.Combine(pic,c.id+".jpg");
					if(File.Exists(jpg)){
						list.Add(jpg);
						jpg=Path.GetFileNameWithoutExtension(jpg);
					}
					else
						jpg="";

					if(c.IsType(CardType.TYPE_SPELL))
						sw.WriteLine(getSpell(c, jpg));
					else if(c.IsType(CardType.TYPE_TRAP))
						sw.WriteLine(getTrap(c, jpg));
					else if(c.IsType(CardType.TYPE_PENDULUM))
						sw.WriteLine(getPendulum(c, jpg));
					else
						sw.WriteLine(getMonster(c, jpg));
				}
				sw.Close();
			}

			return list.ToArray();
		}
		
		public static string reItalic(string str)
		{
			foreach(string s in cfg.repalces)
			{
				if(!string.IsNullOrEmpty(s))
					str=str.Replace(s,"<i>"+s+"</i>");
			}
			return str;
		}
		
		static string getMonster(Card c,string img)
		{
			StringBuilder sb=new StringBuilder(cfg.monster);
			string[] types=MSEConvert.GetTypes(c);
			string race=MSEConvert.GetRace(c.race);
			sb.Replace("%type%", types[0]);
			sb.Replace("%name%", MSE.reItalic(c.name));
			sb.Replace("%attribute%", MSEConvert.GetAttribute(c.attribute));
			sb.Replace("%level%", MSEConvert.GetStar(c.level));
			sb.Replace("%image%", img);
			sb.Replace("%race%", race);
			sb.Replace("%type1%",types[1]);
			sb.Replace("%type2%",types[2]);
			sb.Replace("%type3%",types[3]);
			sb.Replace("%desc%", MSEConvert.ReDesc(c.desc));
			if(!string.IsNullOrEmpty(race))
			{
				sb.Replace("%atk%", (c.atk<0)?"?":c.atk.ToString());
				sb.Replace("%def%", (c.def<0)?"?":c.def.ToString());
			}
			sb.Replace("%code%",c.id.ToString("00000000"));
			return sb.ToString();
		}
		static string getPendulum(Card c,string img)
		{
			StringBuilder sb=new StringBuilder(cfg.pendulum);
			string[] types=MSEConvert.GetTypes(c);
			string race=MSEConvert.GetRace(c.race);
			sb.Replace("%type%", types[0]);
			sb.Replace("%name%", MSE.reItalic(c.name));
			sb.Replace("%attribute%", MSEConvert.GetAttribute(c.attribute));
			sb.Replace("%level%", MSEConvert.GetStar(c.level));
			sb.Replace("%image%", img);
			sb.Replace("%race%", race);
			sb.Replace("%type1%",types[1]);
			sb.Replace("%type2%",types[2]);
			sb.Replace("%type3%",types[3]);
			sb.Replace("%desc%", MSEConvert.ReDesc(
				MSEConvert.GetDesc(c.desc, cfg.regx_monster)));
			if(!string.IsNullOrEmpty(race))
			{
				sb.Replace("%atk%", (c.atk<0)?"?":c.atk.ToString());
				sb.Replace("%def%", (c.def<0)?"?":c.def.ToString());
			}
			sb.Replace("%code%",c.id.ToString("00000000"));
			sb.Replace("%pl%", ((c.level >> 0x18) & 0xff).ToString());
			sb.Replace("%pr%", ((c.level >> 0x10) & 0xff).ToString());
			sb.Replace("%pdesc%",MSEConvert.ReDesc(
				MSEConvert.GetDesc(c.desc, cfg.regx_pendulum)));
			return sb.ToString();
		}
		static string getSpell(Card c,string img)
		{
			string level="";
			if(c.IsType(CardType.TYPE_EQUIP))
				level="+";
			else if(c.IsType(CardType.TYPE_QUICKPLAY))
				level="$";
			else if(c.IsType(CardType.TYPE_FIELD))
				level="&";
			else if(c.IsType(CardType.TYPE_CONTINUOUS))
				level="%";
			else if(c.IsType(CardType.TYPE_RITUAL))
				level="#";
			else
				level="^";
			StringBuilder sb=new StringBuilder(cfg.spelltrap);
			sb.Replace("%type%", "spell card");
			sb.Replace("%name%", MSE.reItalic(c.name));
			sb.Replace("%attribute%", "spell");
			sb.Replace("%level%", level);
			sb.Replace("%image%", img);
			sb.Replace("%desc%", MSEConvert.ReDesc(c.desc));
			sb.Replace("%code%", c.id.ToString("00000000"));
			return sb.ToString();
		}
		static string getTrap(Card c,string img)
		{
			string level="";
			if(c.IsType(CardType.TYPE_COUNTER))
				level="!";
			else if(c.IsType(CardType.TYPE_CONTINUOUS))
				level="%";
			else
				level="^";
			StringBuilder sb=new StringBuilder(cfg.spelltrap);
			sb.Replace("%type%", "trap card");
			sb.Replace("%name%", MSE.reItalic(c.name));
			sb.Replace("%attribute%", "trap");
			sb.Replace("%level%", level);
			sb.Replace("%image%", img);
			sb.Replace("%desc%", MSEConvert.ReDesc(c.desc));
			sb.Replace("%code%", c.id.ToString("00000000"));
			return sb.ToString();
		}

	}
}
