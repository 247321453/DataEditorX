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
	public class MSE
	{
		MSEConfig cfg;
		
		public int MaxNum
		{
			get{return cfg.maxcount;}
		}
		
		public string ImagePath
		{
			get {return cfg.imagepath;}
		}
		
		public MSE(string path,
		           Dictionary<long,string> typedic,
		           Dictionary<long,string> racedic)
		{
			cfg=new MSEConfig(path);
		}
        public string reItalic(string str)
        {
            str = cn2tw(str);
            foreach (RegStr rs in cfg.replaces)
            {
                str = Regex.Replace(str, rs.pstr, rs.rstr);
            }
            return str;
        }
        public string cn2tw(string str)
        {
            if (cfg.Iscn2tw)
            {
                str = Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
                str = str.Replace("巖", "岩");
            }
            return str;
        }
        public string ReDesc(string desc)
        {
            desc = cn2tw(desc);
            StringBuilder sb = new StringBuilder(reItalic(desc));

            sb.Replace(Environment.NewLine, "\n");
            sb.Replace("\n\n", "\n");
            sb.Replace("\n", "\n\t\t");
            return sb.ToString();
        }
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
            else if (cfg.st_is_symbol)
                level = "^";
            else
                level = "";

            if (isSpell)
                level = cfg.str_spell.Replace("%%", level);
            else
                level = cfg.str_trap.Replace("%%", level);
            return level;
        }
		public string[] WriteSet(string file,Card[] cards)
		{
			List<string> list=new List<string>();
			string pic=cfg.imagepath;
			using(FileStream fs=new FileStream(file,
			                                   FileMode.Create, FileAccess.Write))
			{
				StreamWriter sw=new StreamWriter(fs, Encoding.UTF8);
				sw.WriteLine(cfg.head);
				foreach(Card c in cards)
				{
                    string jpg = YGOUtil.GetCardImagePath(pic, c);
                    if (!string.IsNullOrEmpty(jpg))
                    {
                        list.Add(jpg);
                        jpg = Path.GetFileName(jpg);
                    }
					if(c.IsType(CardType.TYPE_SPELL)||c.IsType(CardType.TYPE_TRAP))
						sw.WriteLine(getSpellTrap(c, jpg, c.IsType(CardType.TYPE_SPELL)));
					else
						sw.WriteLine(getMonster(c, jpg, c.IsType(CardType.TYPE_PENDULUM)));
				}
				sw.Close();
			}

			return list.ToArray();
		}
		
		string getMonster(Card c,string img,bool isPendulum)
		{
			StringBuilder sb=new StringBuilder();
			if(isPendulum)
				sb.Append(cfg.pendulum);
			else
				sb.Append(cfg.monster);

            string[] types = YGOUtil.GetTypes(c);
            string race = YGOUtil.GetRace(c.race);
			sb.Replace("%type%", types[0]);
            sb.Replace("%name%", reItalic(c.name));
            sb.Replace("%attribute%", YGOUtil.GetAttribute(c.attribute));
            sb.Replace("%level%", YGOUtil.GetStar(c.level));
			sb.Replace("%image%", img);
			sb.Replace("%race%", cn2tw(race));
			sb.Replace("%type1%",cn2tw(types[1]));
			sb.Replace("%type2%",cn2tw(types[2]));
			sb.Replace("%type3%",cn2tw(types[3]));
			if(isPendulum){
                string text = YGOUtil.GetDesc(c.desc, cfg.regx_monster);
				if(string.IsNullOrEmpty(text))
					text=c.desc;
				sb.Replace("%desc%", ReDesc(text));
				sb.Replace("%pl%", ((c.level >> 0x18) & 0xff).ToString());
				sb.Replace("%pr%", ((c.level >> 0x10) & 0xff).ToString());
                sb.Replace("%pdesc%", ReDesc(
					YGOUtil.GetDesc(c.desc, cfg.regx_pendulum)));
			}
			else
				sb.Replace("%desc%", ReDesc(c.desc));
			sb.Replace("%atk%", (c.atk<0)?"?":c.atk.ToString());
			sb.Replace("%def%", (c.def<0)?"?":c.def.ToString());
			
			sb.Replace("%code%", c.idString);
			return sb.ToString();
		}
		string getSpellTrap(Card c,string img,bool isSpell)
		{
			StringBuilder sb=new StringBuilder(cfg.spelltrap);
			sb.Replace("%type%", isSpell?"spell card":"trap card");
			sb.Replace("%name%", reItalic(c.name));
			sb.Replace("%attribute%", isSpell?"spell":"trap");
			sb.Replace("%level%", GetST(c, isSpell));
			sb.Replace("%image%", img);
			sb.Replace("%desc%", ReDesc(c.desc));
			sb.Replace("%code%", c.idString);
			return sb.ToString();
		}
	}
}
