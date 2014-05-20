/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月20 星期二
 * 时间: 7:40
 * 
 */
using System;
using System.Windows.Forms;

namespace DataEditorX
{
    /// <summary>
    /// 消息
    /// </summary>
    public static class MyMsg
    {   
        public static void Show(string strMsg)
        {
            MessageBox.Show(strMsg, "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Warning(string strWarn)
        {
            MessageBox.Show(strWarn, "警告",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Error(string strError)
        {
            MessageBox.Show(strError, "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool Question(string strQues)
        {
            if(MessageBox.Show(strQues, "询问",
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Question)==DialogResult.OK)
                return true;
            else
                return false;
        }
    }
}
