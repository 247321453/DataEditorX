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
            replaces = new Dictionary<string, string>();

            typeDic = new Dictionary<long, string>();
            raceDic = new Dictionary<long, string>();
            //读取配置
            if (!File.Exists(tmp))
                return;
            string[] lines = File.ReadAllLines(tmp, Encoding.UTF8);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;
                if (line.StartsWith("cn2tw"))
                    Iscn2tw = ConfHelper.getBooleanValue(line);
                else if (line.StartsWith("spell"))
                    str_spell = ConfHelper.getValue(line);
                else if (line.StartsWith("trap"))
                    str_trap = ConfHelper.getValue(line);
                else if (line.StartsWith("pendulum-text"))
                    regx_pendulum = ConfHelper.getRegexValue(line);
                else if (line.StartsWith("monster-text"))
                    regx_monster = ConfHelper.getRegexValue(line);
                else if (line.StartsWith("maxcount"))
                    maxcount = ConfHelper.getIntegerValue(line, 0);
                else if (line.StartsWith("imagepath"))
                {
                    //如果路径不合法，则为后面的路径
                    imagepath = MyPath.CheckDir(ConfHelper.getValue(line), MyPath.Combine(path, "Images"));
                }
                else if (line.StartsWith("replace"))
                {//特数字替换
                    string word = ConfHelper.getValue(line);
                    string p = ConfHelper.getRegex(ConfHelper.getValue1(word));
                    string r = ConfHelper.getRegex(ConfHelper.getValue2(word));
                    if (!string.IsNullOrEmpty(p))
                        replaces.Add(p, r);

                }
                else if (line.StartsWith("race"))
                {//种族
                    ConfHelper.DicAdd(raceDic, line);
                }
                else if (line.StartsWith("type"))
                {//类型
                    ConfHelper.DicAdd(typeDic, line);
                }
            }
            //判断魔法标志是否为纯符号
            if (str_spell == "%%" && str_trap == "%%")
                st_is_symbol = true;
            else
                st_is_symbol = false;
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
        public Dictionary<string, string> replaces;
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
