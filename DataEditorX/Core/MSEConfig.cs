/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-15
 * 时间: 15:47
 * 
 */
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataEditorX.Language;

namespace DataEditorX.Core
{
	public class RegStr
	{
		public RegStr(string p,string r)
		{
			pstr=Re(p);
			rstr=Re(r);
		}
		string Re(string str)
		{
			return str.Replace("\\n","\n").Replace("\\t","\t");
		}
		public string pstr;
		public string rstr;
	}
	/// <summary>
	/// Description of MSEConfig.
	/// </summary>
	public class MSEConfig
	{
		public MSEConfig(string path)
		{
			Iscn2tw=false;
			regx_monster="(\\s\\S*?)";
			regx_pendulum="(\\s\\S*?)";

			head = read(path, "mse-head.txt");
			monster = read(path, "mse-monster.txt");
			pendulum = read(path, "mse-pendulum.txt");
			spelltrap = read(path, "mse-spelltrap.txt");
			
			string tmp=Path.Combine(path, "mse-config.txt");
			replaces=new List<RegStr>();
			
			if(File.Exists(tmp))
			{
				string[] lines=File.ReadAllLines(tmp, Encoding.UTF8);
				foreach(string line in lines)
				{
					if(string.IsNullOrEmpty(line) || line.StartsWith("#"))
						continue;
					if(line.StartsWith("cn2tw"))
						Iscn2tw=(getValue(line).ToLower()=="true")?true:false;
					else if(line.StartsWith("STisEn"))
						STisEn=(getValue(line).ToLower()=="true")?true:false;
					else if(line.StartsWith("pendulum-text"))
						regx_pendulum=getRegex(getValue(line));
					else if(line.StartsWith("monster-text"))
						regx_monster=getRegex(getValue(line));
					else if(line.StartsWith("replace")){
						string word=getValue(line);
						int t=word.IndexOf(" ");
						if(t>0){
							string p=word.Substring(0,t);
							string r=word.Substring(t+1);
							if(!string.IsNullOrEmpty(p))
								replaces.Add(new RegStr(p, r));
						}
						
					}
				}
			}
			else
			{
				Iscn2tw=false;
			}
		}
		string getRegex(string word)
		{
			return word.Replace("\\n","\n").Replace("\\t","\t");
		}
		string getValue(string line)
		{
			int t=line.IndexOf('=');
			if(t>0)
				return line.Substring(t+1).Trim();
			return "";
		}
		string read(string path,string name)
		{
			string tmp=Path.Combine(path, name);
			return File.Exists(tmp)?File.ReadAllText(tmp):"";
		}
		public bool STisEn;
		public bool Iscn2tw;
		public List<RegStr> replaces;
		public string regx_pendulum;
		public string regx_monster;
		public string head;
		public string monster;
		public string pendulum;
		public string spelltrap;
	}
}
