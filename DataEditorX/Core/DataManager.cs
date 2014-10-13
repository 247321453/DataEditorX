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
using System.Globalization;
using System.Collections.Generic;

namespace DataEditorX.Core
{
    public class DataManager
    {
        #region 读取
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public static Dictionary<long, string> Read(string strFile)
        {
            Dictionary<long, string> m_dic=new Dictionary<long, string>();
            if(File.Exists(strFile))
            {
                using(FileStream fstream=new FileStream(
                    strFile,FileMode.Open, FileAccess.ReadWrite))
                {
                    StreamReader sreader=new StreamReader(fstream, Encoding.UTF8);
                    string line , strkey, strword;
                    int l;
                    long lkey;
                    while((line = sreader.ReadLine()) !=null )
                    {
                        if(line.StartsWith("#"))
                            continue;
                        if((l = line.IndexOf(" ")) < 0)
                           continue;
                        strkey=line.Substring(0,l).Replace("0x","");
                        strword=line.Substring(l+1);
                        int t=strword.IndexOf('\t');
                        if(t>0)
                        	strword=strword.Substring(0,t);
                        if(line.StartsWith("0x"))
                            long.TryParse(strkey, NumberStyles.HexNumber, null, out lkey);
                        else
                            long.TryParse(strkey, out lkey);
                        if(!m_dic.ContainsKey(lkey) && strword !="N/A")
                            m_dic.Add(lkey, strword);
                    }
                    sreader.Close();
                    fstream.Close();
                }
            }
            return m_dic;
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
    }
}
