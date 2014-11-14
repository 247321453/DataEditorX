/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-26
 * 时间: 10:26
 * 
 */
using System;
using System.Text;
using System.Windows.Forms;

namespace System.IO
{
	/// <summary>
	/// Description of MyPath.
	/// </summary>
	public class MyPath
	{
        public static string GetFullPath(string dir)
        {
            string path = Application.StartupPath;
            if (dir.StartsWith("."))
            {
                dir = MyPath.Combine(path, dir.Substring(2));
            }
            return dir;
        }
		public static string Combine(params string[] paths)
		{
			if (paths.Length == 0)
			{
				throw new ArgumentException("please input path");
			}
			else
			{
				StringBuilder builder = new StringBuilder();
				string spliter = Path.DirectorySeparatorChar.ToString();
				string firstPath = paths[0];
				if (firstPath.StartsWith("HTTP", StringComparison.OrdinalIgnoreCase))
				{
					spliter = "/";
				}
				if (!firstPath.EndsWith(spliter))
				{
					firstPath = firstPath + spliter;
				}
				builder.Append(firstPath);
				for (int i = 1; i < paths.Length; i++)
				{
					string nextPath = paths[i];
					if (nextPath.StartsWith("/") || nextPath.StartsWith("\\"))
					{
						nextPath = nextPath.Substring(1);
					}
					if (i != paths.Length - 1)//not the last one
					{
						if (nextPath.EndsWith("/") || nextPath.EndsWith("\\"))
						{
							nextPath = nextPath.Substring(0, nextPath.Length - 1) + spliter;
						}
						else
						{
							nextPath = nextPath + spliter;
						}
					}
					builder.Append(nextPath);
				}
				return builder.ToString();
			}
		}
	}
}
