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
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Windows.Forms;
using Microsoft.VisualBasic;

using DataEditorX.Config;

namespace DataEditorX.Core
{


    /// <summary>
    /// Description of MSE.
    /// </summary>
    public class MseMaker
    {
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
        //调整换行符
        public string ReDesc(string desc)
        {
            desc = cn2tw(desc);
            StringBuilder sb = new StringBuilder(reItalic(desc));

            sb.Replace(Environment.NewLine, "\n");
            sb.Replace("\n\n", "\n");
            sb.Replace("\n", "\n\t\t");
            return sb.ToString();
        }
        //获取魔法陷阱的类型符号
        public string GetST(Card c, bool isSpell)
        {
            string level;
            if (c.IsType(CardType.TYPE_EQUIP))
                level = "+";
            else if (c.IsType(CardType.TYPE_QUICKPLAY))
                level = "$";
            else if (c.IsType(CardType.TYPE_FIELD))
                level = "&";
            else if (c.IsType(CardType.TYPE_CONTINUOUS))
                level = "%";
            else if (c.IsType(CardType.TYPE_RITUAL))
                level = "#";
            else if (c.IsType(CardType.TYPE_COUNTER))
                level = "!";
            else if (cfg.str_spell == MSEConfig.TAG_REP && cfg.str_trap == MSEConfig.TAG_REP)
                level = "^";//带文字的图片
            else
                level = "";

            if (isSpell)
                level = cfg.str_spell.Replace(MSEConfig.TAG_REP, level);
            else
                level = cfg.str_trap.Replace(MSEConfig.TAG_REP, level);
            return level;
        }
        public static string GetCardImagePath(string picpath, Card c)
        {
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
        public static string GetAttribute(int attr)
        {
            CardAttribute cattr = (CardAttribute)attr;
            string sattr = "none";
            switch (cattr)
            {
                case CardAttribute.ATTRIBUTE_DARK:
                    sattr = "dark";
                    break;
                case CardAttribute.ATTRIBUTE_DEVINE:
                    sattr = "divine";
                    break;
                case CardAttribute.ATTRIBUTE_EARTH:
                    sattr = "earth";
                    break;
                case CardAttribute.ATTRIBUTE_FIRE:
                    sattr = "fire";
                    break;
                case CardAttribute.ATTRIBUTE_LIGHT:
                    sattr = "light";
                    break;
                case CardAttribute.ATTRIBUTE_WATER:
                    sattr = "water";
                    break;
                case CardAttribute.ATTRIBUTE_WIND:
                    sattr = "wind";
                    break;
            }
            return sattr;
        }
        public static string GetDesc(string desc, string regx)
        {
            desc = desc.Replace(Environment.NewLine, "\n");
            Regex regex = new Regex(regx);
            Match mc = regex.Match(desc);
            if (mc.Success)
                return (mc.Groups.Count > 1) ?
                    mc.Groups[1].Value : mc.Groups[0].Value;
            return "";
        }
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
        public string GetRace(long race)
        {
            if (cfg.raceDic.ContainsKey(race))
                return cfg.raceDic[race].Trim();
            return race.ToString("x");
        }
        public string GetType(CardType ctype)
        {
            long type = (long)ctype;
            if (cfg.typeDic.ContainsKey(type))
                return cfg.typeDic[type].Trim();
            return type.ToString("x");
        }
        public string[] GetTypes(Card c)
        {
            string[] types = new string[] { "normal monster", "", "", "" };
            if (c.IsType(CardType.TYPE_MONSTER))
            {//卡片类型和第1效果
                if (c.IsType(CardType.TYPE_XYZ))
                {
                    types[0] = "xyz monster";
                    types[1] = GetType(CardType.TYPE_XYZ);
                }
                else if (c.IsType(CardType.TYPE_TOKEN))
                {
                    types[0] = (c.race == 0) ? "token card" : "token monster";
                    types[1] = GetType(CardType.TYPE_TOKEN);
                }
                else if (c.IsType(CardType.TYPE_RITUAL))
                {
                    types[0] = "ritual monster";
                    types[1] = GetType(CardType.TYPE_RITUAL);
                }
                else if (c.IsType(CardType.TYPE_FUSION))
                {
                    types[0] = "fusion monster";
                    types[1] = GetType(CardType.TYPE_FUSION);
                }
                else if (c.IsType(CardType.TYPE_SYNCHRO))
                {
                    types[0] = "synchro monster";
                    types[1] = GetType(CardType.TYPE_SYNCHRO);
                }
                else if (c.IsType(CardType.TYPE_EFFECT))
                {
                    types[0] = "effect monster";
                }
                else
                    types[0] = "normal monster";
                //同调
                if (types[0] == "synchro monster" || types[0] == "token monster")
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
                else if (types[0] == "normal monster")
                {
                    if (c.IsType(CardType.TYPE_PENDULUM))//灵摆
                        types[1] = GetType(CardType.TYPE_PENDULUM);
                    else if (c.IsType(CardType.TYPE_TUNER))//调整
                        types[1] = GetType(CardType.TYPE_TUNER);
                }
                else if (types[0] != "effect monster")
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
            if (c.race == 0)
            {
                types[1] = "";
                types[2] = "";
            }
            return types;
        }
        public string[] WriteSet(string file, Card[] cards)
        {
            List<string> list = new List<string>();
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
                        list.Add(jpg);
                        jpg = Path.GetFileName(jpg);
                    }
                    if (c.IsType(CardType.TYPE_SPELL) || c.IsType(CardType.TYPE_TRAP))
                        sw.WriteLine(getSpellTrap(c, jpg, c.IsType(CardType.TYPE_SPELL)));
                    else
                        sw.WriteLine(getMonster(c, jpg, c.IsType(CardType.TYPE_PENDULUM)));
                }
                sw.Close();
            }

            return list.ToArray();
        }
        //pendulum怪兽
        string getMonster(Card c, string img, bool isPendulum)
        {
            StringBuilder sb = new StringBuilder();
            string[] types = GetTypes(c);
            string race = GetRace(c.race);
            sb.AppendLine("card:");
            sb.AppendLine("	card type: " + types[0]);
            sb.AppendLine("	name: " + reItalic(c.name));
            sb.AppendLine("	attribute: " + GetAttribute(c.attribute));
            sb.AppendLine("	level: " + GetStar(c.level));
            sb.AppendLine("	image: " + img);
            sb.AppendLine("	type 1: " + cn2tw(race));
            sb.AppendLine("	type 2: " + cn2tw(types[1]));
            sb.AppendLine("	type 3: " + cn2tw(types[2]));
            sb.AppendLine("	type 4: " + cn2tw(types[3]));
            if (isPendulum)
            {
                string text = GetDesc(c.desc, cfg.regx_monster);
                if (string.IsNullOrEmpty(text))
                    text = c.desc;
                sb.AppendLine("	rule text: ");
                sb.AppendLine("		" + ReDesc(text));
                sb.AppendLine("	pendulum scale 1: " + ((c.level >> 0x18) & 0xff).ToString());
                sb.AppendLine("	pendulum scale 2:" + ((c.level >> 0x10) & 0xff).ToString());
                sb.AppendLine("	pendulum text: ");
                sb.AppendLine("		" + ReDesc(GetDesc(c.desc, cfg.regx_pendulum)));
            }
            else
            {
                sb.AppendLine("	rule text: ");
                sb.AppendLine("		" + ReDesc(c.desc));
            }
            sb.AppendLine("	attack: " + ((c.atk < 0) ? "?" : c.atk.ToString()));
            sb.AppendLine("	defense: " + ((c.def < 0) ? "?" : c.def.ToString()));

            sb.AppendLine("	gamecode: " + c.idString);
            return sb.ToString();
        }
        //魔法陷阱
        string getSpellTrap(Card c, string img, bool isSpell)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("card:");
            sb.AppendLine("	card type: " + (isSpell ? "spell card" : "trap card"));
            sb.AppendLine("	name: " + reItalic(c.name));
            sb.AppendLine("	attribute: " + (isSpell ? "spell" : "trap"));
            sb.AppendLine("	level: " + GetST(c, isSpell));
            sb.AppendLine("	image: " + img);
            sb.AppendLine("	rule text: ");
            sb.AppendLine("		" + ReDesc(c.desc));
            sb.AppendLine("	gamecode: " + c.idString);
            return sb.ToString();
        }
    }
}
