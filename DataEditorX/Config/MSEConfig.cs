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

        public const string TAG_REP = "%%";
        public const string SEP_LINE = " ";
        public const string FILE_CONFIG = "mse-config.txt";
        public const string PATH_IMAGE = "Images";
        
        public MSEConfig(string path)
        {
            init(path);
        }
        public void init(string path)
        {
            Iscn2tw = false;
            regx_monster = "(\\s\\S*?)";
            regx_pendulum = "(\\s\\S*?)";

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
                if (line.StartsWith(TAG_CN2TW))
                    Iscn2tw = ConfHelper.getBooleanValue(line);
                else if (line.StartsWith(TAG_SPELL))
                    str_spell = ConfHelper.getValue(line);
                else if (line.StartsWith(TAG_HEAD))
                    head = ConfHelper.getMultLineValue(line);
                else if (line.StartsWith(TAG_TRAP))
                    str_trap = ConfHelper.getValue(line);
                else if (line.StartsWith(TAG_REG_PENDULUM))
                    regx_pendulum = ConfHelper.getMultLineValue(line);
                else if (line.StartsWith(TAG_REG_MONSTER))
                    regx_monster = ConfHelper.getMultLineValue(line);
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

        //每个存档最大数
        public int maxcount;
        //图片路径
        public string imagepath;
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
        //存档头部
        public string head;
        public Dictionary<long, string> typeDic;
        public Dictionary<long, string> raceDic;
    }
}
