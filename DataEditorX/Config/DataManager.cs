/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 18:08
 * 
 */
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;

namespace DataEditorX.Config
{
	public class DataManager
	{
		/// <summary>
		/// 内容开头
		/// </summary>
		public const string TAG_START = "##";
		/// <summary>
		/// 内容结尾
		/// </summary>
		public const string TAG_END = "#";
		/// <summary>
		/// 行分隔符
		/// </summary>
		public const char SEP_LINE = '\t';

		#region 根据tag获取内容
		static string reReturn(string content)
		{
			string text = content.Replace("\r\n", "\n");
			text = text.Replace("\r", "\n");
			return text;
		}
		public static string subString(string content, string tag)
		{
			Regex reg = new Regex(string.Format(@"{0}{1}\n([\S\s]*?)\n{2}", TAG_START, tag, TAG_END), RegexOptions.Multiline);
			Match mac = reg.Match(reReturn(content));
			if (mac.Success)//把相应的内容提取出来
			{
				return mac.Groups[1].Value.Replace("\n",Environment.NewLine);
			}
			return "";
		}
		#endregion

		#region 读取
		/// <summary>
		/// 从字符串中，按tag来分割内容，并读取内容
		/// </summary>
		/// <param name="content">字符串</param>
		/// <param name="tag">开始的标志</param>
		/// <returns></returns>
		public static Dictionary<long, string> Read(string content, string tag)
		{
			return Read(subString(content,tag));
		}
		/// <summary>
		/// 从文件读取内容，按行读取
		/// </summary>
		/// <param name="strFile"></param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static Dictionary<long, string> Read(string strFile, Encoding encode)
		{
			return Read(File.ReadAllLines(strFile, encode));
		}
		/// <summary>
		/// 从字符串中读取内容，需要分行
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static Dictionary<long, string> Read(string content)
		{
			string text = reReturn(content);
			return Read(text.Split('\n'));
		}
		/// <summary>
		/// 从行读取内容
		/// </summary>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static Dictionary<long, string> Read(string[] lines)
		{
			Dictionary<long, string> tempDic = new Dictionary<long, string>();
			long lkey;
			foreach (string line in lines)
			{
				if (line.StartsWith("#"))
					continue;
				string[] words = line.Split(SEP_LINE);
				if (words.Length < 2)
					continue;
				if (words[0].StartsWith("0x"))
					long.TryParse(words[0].Replace("0x", ""), NumberStyles.HexNumber, null, out lkey);
				else
					long.TryParse(words[0], out lkey);
				// N/A 的数据不显示
				if (!tempDic.ContainsKey(lkey) && words[1] != "N/A")
					tempDic.Add(lkey, words[1]);
			}
			return tempDic;
		}

		#endregion
		
		#region 查找
		public static List<long> GetKeys(Dictionary<long, string> dic)
		{
			List<long> list = new List<long>();
			foreach (long l in dic.Keys)
			{
				list.Add(l);
			}
			return list;
		}
		public static string[] GetValues(Dictionary<long, string> dic)
		{
			List<string> list = new List<string>();
			foreach (long l in dic.Keys)
			{
				list.Add(dic[l]);
			}
			return list.ToArray();
		}
		/// <summary>
		/// 获取值
		/// </summary>
		/// <param name="dic"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetValue(Dictionary<long, string> dic, long key)
		{
			if(dic.ContainsKey(key))
				return dic[key].Trim();
			return key.ToString("x");
		}
		#endregion
	}
}
