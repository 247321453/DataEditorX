using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DataEditorX.Config
{
    public class ConfHelper
    {
        public const string SEP_LINE = " ";
        public static string getValue(string line)
        {
            int t = line.IndexOf('=');
            if (t > 0)
                return line.Substring(t + 1).Trim();
            return "";
        }
        public static string getValue1(string word)
        {
            int i = word.IndexOf(SEP_LINE);
            if (i > 0)
                return word.Substring(0, i);
            return "";
        }
        public static string getValue2(string word)
        {
            int i = word.IndexOf(SEP_LINE);
            if (i > 0)
                return word.Substring(i + SEP_LINE.Length);
            return "";
        }
        public static string getMultLineValue(string line)
        {
            return getRegex(getValue(line));
        }
        public static string getRegex(string word)
        {
            StringBuilder sb = new StringBuilder(word);
            sb.Replace("\\r", "\r");
            sb.Replace("\\n", "\n");
            sb.Replace("\\t", "\t");
            sb.Replace("[:space:]", " ");
            return sb.ToString();
        }
        public static bool getBooleanValue(string line)
        {
            if (getValue(line).ToLower() == "true")
                return true;
            else
                return false;
        }

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
        public static void DicAdd(Dictionary<long, string> dic, string line)
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
