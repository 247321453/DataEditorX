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
        public const string TAG_CN2TW = "cn2tw";
        public const string TAG_SPELL = "spell";
        public const string TAG_TRAP = "trap";
        public const string TAG_REG_PENDULUM = "pendulum-text";
        public const string TAG_REG_MONSTER = "monster-text";
        public const string TAG_MAXCOUNT = "maxcount";
        public const string TAG_RACE = "race";
        public const string TAG_TYPE = "type";

        public const string TAG_IMAGE = "imagepath";
        public const string TAG_REPALCE = "replace";
        public const string TAG_TEXT = "text";

        public const string TAG_REP = "%%";
        public const string SEP_LINE = " ";
        //默认的配置
        public const string FILE_CONFIG_NAME = "Chinese-Simplified";
        public const string PATH_IMAGE = "Images";
        public string configName = FILE_CONFIG_NAME;
        
        public MSEConfig(string path)
        {
            init(path);
        }
        public void SetConfig(string config, string path)
        {
            if (!File.Exists(config))
                return;
            regx_monster = "(\\s\\S*?)";
            regx_pendulum = "(\\s\\S*?)";
            //设置文件名
            configName = getLanguage(config);

            replaces = new Dictionary<string, string>();

            typeDic = new Dictionary<long, string>();
            raceDic = new Dictionary<long, string>();
            string[] lines = File.ReadAllLines(config, Encoding.UTF8);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;
                if (line.StartsWith(TAG_CN2TW))
                    Iscn2tw = ConfHelper.getBooleanValue(line);
                else if (line.StartsWith(TAG_SPELL))
                    str_spell = ConfHelper.getValue(line);
                else if (line.StartsWith(TAG_HEAD))
                    head = ConfHelper.getMultLineValue(line);
                else if (line.StartsWith(TAG_TEXT))
                    temp_text = ConfHelper.getMultLineValue(line);
                else if (line.StartsWith(TAG_TRAP))
                    str_trap = ConfHelper.getValue(line);
                else if (line.StartsWith(TAG_REG_PENDULUM))
                    regx_pendulum = ConfHelper.getValue(line);
                else if (line.StartsWith(TAG_REG_MONSTER))
                    regx_monster = ConfHelper.getValue(line);
                else if (line.StartsWith(TAG_MAXCOUNT))
                    maxcount = ConfHelper.getIntegerValue(line, 0);
                else if (line.StartsWith(TAG_IMAGE))
                {
                    //如果路径不合法，则为后面的路径
                    imagepath = MyPath.CheckDir(ConfHelper.getValue(line), MyPath.Combine(path, PATH_IMAGE));
                }
                else if (line.StartsWith(TAG_REPALCE))
                {//特数字替换
                    string word = ConfHelper.getValue(line);
                    string p = ConfHelper.getRegex(ConfHelper.getValue1(word));
                    string r = ConfHelper.getRegex(ConfHelper.getValue2(word));
                    if (!string.IsNullOrEmpty(p))
                        replaces.Add(p, r);

                }
                else if (line.StartsWith(TAG_RACE))
                {//种族
                    ConfHelper.DicAdd(raceDic, line);
                }
                else if (line.StartsWith(TAG_TYPE))
                {//类型
                    ConfHelper.DicAdd(typeDic, line);
                }
            }
        }
        public void init(string path)
        {
            Iscn2tw = false;
 
            //读取配置
            string tmp = MyPath.Combine(path, getFileName(MyConfig.readString(MyConfig.TAG_MSE)));
            
            if (!File.Exists(tmp))
            {
                tmp = MyPath.Combine(path, getFileName(FILE_CONFIG_NAME));
                if(!File.Exists(tmp))
                    return;//如果默认的也不存在
            }
            SetConfig(tmp, path);
        }
        public static string getFileName(string lang)
        {
            return "mse_" + lang + ".txt";
        }
        public static string getLanguage(string file)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            if (!name.StartsWith("mse_"))
                return "";
            else
                return name.Replace("mse_", "");
        }

        //每个存档最大数
        public int maxcount;
        //图片路径
        public string imagepath;
        //魔法标志
        public string str_spell;
        //陷阱标志
        public string str_trap;
        //效果格式
        public string temp_text;
        //简体转繁体？
        public bool Iscn2tw;
        //特数字替换
        public Dictionary<string, string> replaces;
        //效果文正则提取
        public string regx_pendulum;
        public string regx_monster;
        //存档头部
        public string head;
        public Dictionary<long, string> typeDic;
        public Dictionary<long, string> raceDic;
    }
}
