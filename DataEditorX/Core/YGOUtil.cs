using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Configuration;

namespace DataEditorX.Core
{
    static class YGOUtil
    {
        static DataConfig datacfg;
        static YGOUtil()
        {
            datacfg = new DataConfig();
            datacfg.Init();
        }
        public static void SetConfig(DataConfig dcfg)
        {
            datacfg = dcfg;
        }

        public static string GetCardImagePath(string picpath,Card c)
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
        public static string GetAttributeString(int attr)
        {
            return DataManager.GetValue(datacfg.dicCardAttributes, attr);
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

        public static string GetRace(long race)
        {
            return DataManager.GetValue(datacfg.dicCardRaces, race);
        }
        public static string[] GetTypes(Card c)
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
                    types[0] = (c.race == 0)?"token card":"token monster";
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

        public static string GetCardType(Card c)
        {
            string str = "???";
            if (c.IsType(CardType.TYPE_MONSTER))
            {//卡片类型和第1效果
                if (c.IsType(CardType.TYPE_XYZ))
                {
                    str = GetType(CardType.TYPE_XYZ);
                }
                else if (c.IsType(CardType.TYPE_TOKEN))
                {
                    str = GetType(CardType.TYPE_TOKEN);
                }
                else if (c.IsType(CardType.TYPE_RITUAL))
                {
                    str = GetType(CardType.TYPE_RITUAL);
                }
                else if (c.IsType(CardType.TYPE_FUSION))
                {
                    str = GetType(CardType.TYPE_FUSION);
                }
                else if (c.IsType(CardType.TYPE_SYNCHRO))
                {
                    str = GetType(CardType.TYPE_SYNCHRO);
                }
                else if (c.IsType(CardType.TYPE_EFFECT))
                {
                    str = GetType(CardType.TYPE_EFFECT);
                }
                else
                    str = GetType(CardType.TYPE_NORMAL);
                str += GetType(CardType.TYPE_MONSTER);
            }
            else if (c.IsType(CardType.TYPE_SPELL))
            {
                if (c.IsType(CardType.TYPE_EQUIP))
                    str = GetType(CardType.TYPE_EQUIP);
                else if (c.IsType(CardType.TYPE_QUICKPLAY))
                    str = GetType(CardType.TYPE_QUICKPLAY);
                else if (c.IsType(CardType.TYPE_FIELD))
                    str = GetType(CardType.TYPE_FIELD);
                else if (c.IsType(CardType.TYPE_CONTINUOUS))
                    str = GetType(CardType.TYPE_CONTINUOUS);
                else if (c.IsType(CardType.TYPE_RITUAL))
                    str = GetType(CardType.TYPE_RITUAL);
                else
                    str = GetType(CardType.TYPE_NORMAL);
                str += GetType(CardType.TYPE_SPELL);
            }
            else if (c.IsType(CardType.TYPE_TRAP))
            {
                if (c.IsType(CardType.TYPE_CONTINUOUS))
                    str = GetType(CardType.TYPE_CONTINUOUS);
                else if (c.IsType(CardType.TYPE_COUNTER))
                    str = GetType(CardType.TYPE_COUNTER);
                else
                    str = GetType(CardType.TYPE_NORMAL);
                str += GetType(CardType.TYPE_TRAP);
            }
            return str;
        }

        static string GetType(CardType type)
        {
            return DataManager.GetValue(datacfg.dicCardTypes, (long)type);
        }

        public static string GetTypeString(long type)
        {
            string str="";
            foreach (long k in datacfg.dicCardTypes.Keys)
            {
                if ((type & k) == k)
                    str += GetType((CardType)k)+"|";
            }
            if (str.Length > 0)
                str = str.Substring(0, str.Length - 1);
            else
                str = "???";
            return str;
        }
        public static string GetSetNameString(long type)
        {
            return "";
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

        #region 根据文件读取数据库
        /// <summary>
        /// 读取ydk文件为密码数组
        /// </summary>
        /// <param name="file">ydk文件</param>
        /// <returns>密码数组</returns>
        public static string[] ReadYDK(string ydkfile)
        {
            string str;
            List<string> IDs = new List<string>();
            if (File.Exists(ydkfile))
            {
                using (FileStream f = new FileStream(ydkfile, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(f, Encoding.Default);
                    str = sr.ReadLine();
                    while (str != null)
                    {
                        if (!str.StartsWith("!") && !str.StartsWith("#") && str.Length > 0)
                        {
                            if (IDs.IndexOf(str) < 0)
                                IDs.Add(str);
                        }
                        str = sr.ReadLine();
                    }
                    sr.Close();
                    f.Close();
                }
            }
            if (IDs.Count == 0)
                return null;
            return IDs.ToArray();
        }
        #endregion

        #region 图像
        public static string[] ReadImage(string path)
        {
            List<string> list = new List<string>();
            string[] files = Directory.GetFiles(path, "*.*");
                int n = files.Length;
                for (int i = 0; i < n; i++)
                {
                    string ex = Path.GetExtension(files[i]).ToLower();
                    if (ex == ".jpg" || ex == ".png" || ex == ".bmp")
                        list.Add(Path.GetFileNameWithoutExtension(files[i]));
                }
            return list.ToArray();
        }
        #endregion
    }
}
