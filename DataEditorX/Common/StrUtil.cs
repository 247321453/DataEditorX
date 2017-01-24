using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditorX.Common
{
	public class StrUtil
	{
		public static string AutoEnter(string str, int lineNum, char re)
		{
			if (str == null || str.Length == 0)
				return "";
			str = " "+str.Replace("\r\n", "\n");
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
				if (c == '\n' || c == '\r')
				{
					sb.Append(re);
					i = 0;
				}
				else if(c == re)
				{
					sb.Append(c);
					i = 0;
				}
				else if (i >= lineNum)
				{
					sb.Append(c);
					sb.Append(re);
					i = 0;
				}else{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}
	}
}
