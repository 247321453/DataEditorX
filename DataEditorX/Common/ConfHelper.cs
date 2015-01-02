using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DataEditorX.Common
{
    public class ConfHelper
    {
        /// <summary>
        /// 内容分隔符
        /// </summary>
        public const string SEP_LINE = " ";
        /// <summary>
        /// 从行获取值
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string getValue(string line)
        {
            int t = line.IndexOf('=');
            if (t > 0)
                return line.Substring(t + 1).Trim();
            return "";
        }
        /// <summary>
        /// 从词中获取第一个值
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string getValue1(string word)
        {
            int i = word.IndexOf(SEP_LINE);
            if (i > 0)
                return word.Substring(0, i);
            return "";
        }
        /// <summary>
        /// 从词中获取第二个值
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string getValue2(string word)
        {
            int i = word.IndexOf(SEP_LINE);
            if (i > 0)
                return word.Substring(i + SEP_LINE.Length);
            return "";
        }
        /// <summary>
        /// 获取多行值，替换\n \t
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string getMultLineValue(string line)
        {
            return getRegex(getValue(line));
        }
        /// <summary>
        /// 替换特殊符
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string getRegex(string word)
        {
            StringBuilder sb = new StringBuilder(word);
            sb.Replace("\\r", "\r");
            sb.Replace("\\n", "\n");
            sb.Replace("\\t", "\t");
            sb.Replace("[:space:]", " ");
            return sb.ToString();
        }
        /// <summary>
        /// 获取boolean值
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool getBooleanValue(string line)
        {
            if (getValue(line).ToLower() == "true")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取int值
        /// </summary>
        /// <param name="line"></param>
        /// <param name="defalut">失败的值</param>
        /// <returns></returns>
        public static int getIntegerValue(string line, int defalut)
        {
            int i;
            try
            {
                i = int.Parse(getValue(line));
                return i;
            }
            catch{}
            return defalut;
        }
        /// <summary>
        /// 从行获取内容添加到字典
        /// race 0x1 xxx
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="line"></param>
        public static void DicAdd(SortedList<long, string> dic, string line)
        {
            int i = line.IndexOf("0x");
            int j = (i > 0) ? line.IndexOf(SEP_LINE, i + 1) : -1;
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
    }
}
