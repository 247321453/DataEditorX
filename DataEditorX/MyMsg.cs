/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月20 星期二
 * 时间: 7:40
 * 
 */
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;

namespace DataEditorX
{
    /// <summary>
    /// 消息
    /// </summary>
    public static class MyMsg
    {   
        static Dictionary<string ,string> strDic=new Dictionary<string,string>();
        public static bool Init(string file)
        {
            if(!File.Exists(file))
                return false;
            strDic.Clear();
            using(FileStream fs=new FileStream(file,FileMode.Open, FileAccess.Read))
            {
                StreamReader sr=new StreamReader(fs,Encoding.UTF8);
                string line;
                string k,v;
                while((line=sr.ReadLine())!=null)
                {
                    if(!string.IsNullOrEmpty(line)&&!line.StartsWith("!"))
                    {
                        int l=line.IndexOf("	");
                        k=line.Substring(0,l);
                        v=line.Substring(l+1);
                        if(!strDic.ContainsKey(k))
                            strDic.Add(k,v);
                    }
                }
                sr.Close();
                fs.Close();
            }
            return true;
        }
        public static string GetString(string keyStr)
        { 
            //string str=ConfigurationManager.AppSettings[keyStr];
            string str=keyStr;
            if(strDic.ContainsKey(keyStr))
                str=strDic[keyStr];
            if(string.IsNullOrEmpty(str))
                return keyStr;
            return str;
        }
        public static void Show(string strMsg)
        {
            MessageBox.Show(strMsg, GetString("info"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Warning(string strWarn)
        {
            MessageBox.Show(strWarn, GetString("warning"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Error(string strError)
        {
            MessageBox.Show(strError, GetString("error"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool Question(string strQues)
        {
            if(MessageBox.Show(strQues, GetString("question"),
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Question)==DialogResult.OK)
                return true;
            else
                return false;
        }
    }
}
