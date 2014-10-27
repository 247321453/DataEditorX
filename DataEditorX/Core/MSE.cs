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

namespace DataEditorX.Core
{
	
	
	/// <summary>
	/// Description of MSE.
	/// </summary>
	public class MSE
	{
		MSEConfig cfg;
		MSEConvert conv;
		
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
			conv=new MSEConvert(typedic, racedic, cfg);
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
					string jpg=MyPath.Combine(pic,c.id+".jpg");
					string jpg1=MyPath.Combine(pic,c.idString+".jpg");
					string jpg2=MyPath.Combine(pic,c.name+".jpg");
					if(File.Exists(jpg)){
						list.Add(jpg);
						jpg=Path.GetFileName(jpg);
					}
					else if(File.Exists(jpg1)){
						list.Add(jpg1);
						jpg=Path.GetFileName(jpg1);
					}
					else if(File.Exists(jpg2)){
						File.Copy(jpg2, jpg, true);
						if(File.Exists(jpg)){//复制失败
							list.Add(jpg);
							jpg=Path.GetFileName(jpg);
						}
						else
							jpg="";
					}
					else
						jpg="";
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
			
			string[] types = conv.GetTypes(c);
			string race = conv.GetRace(c.race);
			sb.Replace("%type%", types[0]);
			sb.Replace("%name%", conv.reItalic(c.name));
			sb.Replace("%attribute%", conv.GetAttribute(c.attribute));
			sb.Replace("%level%", conv.GetStar(c.level));
			sb.Replace("%image%", img);
			sb.Replace("%race%", race);
			sb.Replace("%type1%",types[1]);
			sb.Replace("%type2%",types[2]);
			sb.Replace("%type3%",types[3]);
			if(isPendulum){
				string text= conv.GetDesc(c.desc, cfg.regx_monster);
				if(string.IsNullOrEmpty(text))
					text=c.desc;
				sb.Replace("%desc%", conv.ReDesc(text));
				sb.Replace("%pl%", ((c.level >> 0x18) & 0xff).ToString());
				sb.Replace("%pr%", ((c.level >> 0x10) & 0xff).ToString());
				sb.Replace("%pdesc%", conv.ReDesc(
					conv.GetDesc(c.desc, cfg.regx_pendulum)));
			}
			else
				sb.Replace("%desc%", conv.ReDesc(c.desc));
			sb.Replace("%atk%", (c.atk<0)?"?":c.atk.ToString());
			sb.Replace("%def%", (c.def<0)?"?":c.def.ToString());
			
			sb.Replace("%code%", c.idString);
			return sb.ToString();
		}
		string getSpellTrap(Card c,string img,bool isSpell)
		{
			StringBuilder sb=new StringBuilder(cfg.spelltrap);
			sb.Replace("%type%", isSpell?"spell card":"trap card");
			sb.Replace("%name%", conv.reItalic(c.name));
			sb.Replace("%attribute%", isSpell?"spell":"trap");
			sb.Replace("%level%", conv.GetST(c, isSpell));
			sb.Replace("%image%", img);
			sb.Replace("%desc%", conv.ReDesc(c.desc));
			sb.Replace("%code%", c.idString);
			return sb.ToString();
		}
	}
}
