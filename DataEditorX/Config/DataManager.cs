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
        public const string TAG_START = "##";
        public const string TAG_END = "#";

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
            string strkey, strword;
            int l;
            long lkey;
            foreach (string line in lines)
            {
                if (line.StartsWith("#"))
                    continue;
                if ((l = line.IndexOf(" ")) < 0)
                    continue;
                strkey = line.Substring(0, l).Replace("0x", "");
                strword = line.Substring(l + 1);
                int t = strword.IndexOf('\t');
                if (t > 0)
                    strword = strword.Substring(0, t);
                if (line.StartsWith("0x"))
                    long.TryParse(strkey, NumberStyles.HexNumber, null, out lkey);
                else
                    long.TryParse(strkey, out lkey);
                if (!tempDic.ContainsKey(lkey) && strword != "N/A")
                    tempDic.Add(lkey, strword);
            }
            return tempDic;
        }

        #endregion
        
        #region 查找
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="strValue"></param>
        /// <param name="defaultKey"></param>
        /// <returns></returns>
        public static long GetKey(
            Dictionary<long, string> dic, 
            string strValue, 
            long defaultKey
        ){
            long lkey=defaultKey;
            if(!dic.ContainsValue(strValue))
                return lkey;
            foreach(long key in dic.Keys)
            {
                if(dic[key]==strValue)
                {
                    lkey=key;
                    break;
                }
            }
            return lkey;
        }
        #endregion

        #region value
        public static string[] GetValues(Dictionary<long, string> dic)
        {
            int length=dic.Count;
            string[] words=new string[1];
            words[0]="N/A";
            if(length > 0)
            {
                words=new string[length];
                dic.Values.CopyTo(words,0);
            }
            return words;
        }
        public static string GetValue(Dictionary<long, string> dic,long key){
        	if(dic.ContainsKey(key))
        		return dic[key].Trim();
        	return key.ToString("x");
        }
        #endregion
    }
}
