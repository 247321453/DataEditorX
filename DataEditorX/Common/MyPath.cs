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
	/// 路径处理
	/// </summary>
	public class MyPath
	{
		/// <summary>
		/// 从相对路径获取真实路径
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static string GetRealPath(string dir)
		{
			string path = Application.StartupPath;
			if (dir.StartsWith("."))
			{
				dir = MyPath.Combine(path, dir.Substring(2));
			}
			return dir;
		}
		/// <summary>
		/// 合并路径
		/// </summary>
		/// <param name="paths"></param>
		/// <returns></returns>
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
		/// <summary>
		/// 检查目录是否合法
		/// </summary>
		/// <param name="dir">目录</param>
		/// <param name="defalut">不合法时，采取的目录</param>
		/// <returns></returns>
		public static string CheckDir(string dir,string defalut)
		{
			DirectoryInfo fo;
			try
			{
				fo = new DirectoryInfo(MyPath.GetRealPath(dir));
			}
			catch{
				//路径不合法
				fo = new DirectoryInfo(defalut);
			}
			if (!fo.Exists)
				fo.Create();
			dir = fo.FullName;
			return dir;
		}
		/// <summary>
		/// 根据tag获取文件名
		/// tag_lang.txt
		/// </summary>
		/// <param name="tag">前面</param>
		/// <param name="lang"></param>
		/// <returns></returns>
		public static string getFileName(string tag,string lang)
		{
			return tag + "_" + lang + ".txt";
		}
		/// <summary>
		/// 由tag和lang获取文件名
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="file"></param>
		/// <returns></returns>
		public static string getFullFileName(string tag, string file)
		{
			string name = Path.GetFileNameWithoutExtension(file);
			if (!name.StartsWith(tag + "_"))
				return "";
			else
				return name.Replace(tag + "_", "");
		}

		public static void CreateDir(string dir)
		{
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
		}
		public static void CreateDirByFile(string file)
		{
			string dir = Path.GetDirectoryName(file);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
		}
	}
}
