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
using System.Globalization;

namespace DataEditorX.Config
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
			return str.Replace("\\n","\n").Replace("\\t","\t").Replace("\\s"," ");
		}
		public string pstr;
		public string rstr;
	}
	/// <summary>
	/// Description of MSEConfig.
	/// </summary>
	public class MSEConfig
	{
        public const string TAG_HEAD = "head";
        public const string TAG_MONSTER = "monster";
        public const string TAG_PENDULUM = "pendulum";
        public const string TAG_SPELL_TRAP = "spelltrap";
        public const string FILE_CONFIG = "mse-config.txt";
        public const string FILE_TEMPLATE = "mse-template.txt";
        public const string SEP_LINE = " ";

		public MSEConfig(string path)
		{
            init(path);
		}
        public void init(string path)
        {
            Iscn2tw = false;
            regx_monster = "(\\s\\S*?)";
            regx_pendulum = "(\\s\\S*?)";

            string file = MyPath.Combine(path, FILE_TEMPLATE);
            if (File.Exists(file))
            {
                string content = File.ReadAllText(file, Encoding.UTF8);
                head = DataManager.subString(content, TAG_HEAD);
                monster = DataManager.subString(content, TAG_MONSTER);
                pendulum = DataManager.subString(content, TAG_PENDULUM);
                spelltrap = DataManager.subString(content, TAG_SPELL_TRAP);
            }


            string tmp = MyPath.Combine(path, FILE_CONFIG);
            replaces = new List<RegStr>();

            typeDic = new Dictionary<long, string>();
            raceDic = new Dictionary<long, string>();
            //读取配置
            if (File.Exists(tmp))
            {
                string[] lines = File.ReadAllLines(tmp, Encoding.UTF8);
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                        continue;
                    if (line.StartsWith("cn2tw"))
                        Iscn2tw = (getValue(line).ToLower() == "true") ? true : false;
                    else if (line.StartsWith("spell"))
                        str_spell = getValue(line);
                    else if (line.StartsWith("trap"))
                        str_trap = getValue(line);
                    else if (line.StartsWith("pendulum-text"))
                        regx_pendulum = getRegex(getValue(line));
                    else if (line.StartsWith("monster-text"))
                        regx_monster = getRegex(getValue(line));
                    else if (line.StartsWith("maxcount"))
                        int.TryParse(getValue(line), out maxcount);
                    else if (line.StartsWith("imagepath"))
                        imagepath = MyPath.CheckDir(getValue(line), MyPath.Combine(path, "Images"));
                    else if (line.StartsWith("replace"))
                    {
                        string word = getValue(line);
                        int t = word.IndexOf(" ");
                        if (t > 0)
                        {
                            string p = word.Substring(0, t);
                            string r = word.Substring(t + 1);
                            if (!string.IsNullOrEmpty(p))
                                replaces.Add(new RegStr(p, r));
                        }
                    }
                    else if (line.StartsWith("race"))
                    {
                        DicAdd(raceDic, line);
                    }
                    else if (line.StartsWith("type"))
                    {
                        DicAdd(typeDic, line);
                    }
                }
                if (str_spell == "%%" && str_trap == "%%")
                    st_is_symbol = true;
                else
                    st_is_symbol = false;
            }
            else
            {
                Iscn2tw = false;
            }
        }
        void DicAdd(Dictionary<long,string> dic, string line)
        {
            int i = line.IndexOf("0x");
            int j = (i>0)?line.IndexOf(SEP_LINE, i+1):-1;
            if (j > 0)
            {
                string strkey = line.Substring(i + 2, j - i - 1);
                string strval = line.Substring(j + 1);
                long key;
                long.TryParse(strkey, NumberStyles.HexNumber, null, out key);
                if (!dic.ContainsKey(key))
                    dic.Add(key, strval.Trim());
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
        //每个存档最大数
		public int maxcount;
        //图片路径
		public string imagepath;
        //标志是符号
		public bool st_is_symbol;
        //魔法标志
		public string str_spell;
        //陷阱标志
		public string str_trap;
        //简体转繁体？
		public bool Iscn2tw;
        //特数字替换
		public List<RegStr> replaces;
        //效果文正则提取
		public string regx_pendulum;
		public string regx_monster;
        //模版
		public string head;
		public string monster;
		public string pendulum;
		public string spelltrap;
        public Dictionary<long, string> typeDic;
        public Dictionary<long, string> raceDic;
	}
}
