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
    /// Description of MyMsg.
    /// </summary>
    public static class MyMsg
    {
        public static readonly string ABOUT="程序：DataEditorX\n作者：247321453\nE-mail:247321453@qq.com\n";
        public static readonly string ERROR_NoSeclectScriptText="请选中脚本文本";
        public static readonly string INFO_Delete="删除成功!";
        public static readonly string INFO_Modifty="修改 {0} 成功!";
        public static readonly string INFO_Addition="添加 {0} 成功!";
        public static readonly string INFO_NoChanged="卡片没有被修改!";
        public static readonly string ERROR_NoCard="没有卡片!";
        public static readonly string WARN_NoSelectCDB="请打开一个数据库!";
        public static readonly string ERROR_FileNotExisit="文件不存在！\n{0}";
        public static readonly string ERROR_ModiftyFail="修改失败!";
        public static readonly string ERROR_AdditionFail="修添加失败!";
        public static readonly string ERROR_CardCodeIsZero="卡片密码必须大于0!";
        public static readonly string ERROR_DeleteFail="删除失败!";
        public static readonly string QUES_CreateLua="是否创建脚本?\n{0}";
        public static readonly string QUES_DeleteCard="是否删除选择的{0}张卡?";
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
