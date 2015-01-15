using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditorX.Common
{
    public class StrUtil
    {
        public static string AutoEnter(string str, int lineNum, string re)
        {
            if (str == null || str.Length == 0 || re == null || re.Length == 0)
                return str;
            char[] ch = str.ToCharArray();
            int count = ch.Length;

            StringBuilder sb = new StringBuilder();

            int i = 0;
            foreach (char c in ch)
            {
                int ic = c;
                if (ic > 128)
                    i += 2;
                else
                    i += 1;
                sb.Append(c);
                if (c == '\n' || c == '\r')
                    i = 0;
                if (i >= lineNum)
                {
                    sb.Append(re);
                    i = 0;
                }
            }
            return sb.ToString();
        }
    }
}
