/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月20 星期二
 * 时间: 7:40
 * 
 */
using System;
using System.Windows.Forms;
using System.Configuration;

namespace DataEditorX
{
    /// <summary>
    /// 消息
    /// </summary>
    public static class MyMsg
    {   
        public static string GetString(string keyStr)
        {
            if(ConfigurationManager.AppSettings["language"]=="en")
                return keyStr;
            string str=ConfigurationManager.AppSettings[keyStr];
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
