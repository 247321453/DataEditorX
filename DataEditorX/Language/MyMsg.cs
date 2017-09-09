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

namespace DataEditorX.Language
{
	/// <summary>
	/// 消息
	/// </summary>
	public static class MyMsg
	{
		static string info, warning, error, question;
		static MyMsg()
		{
			info = LanguageHelper.GetMsg(LMSG.titleInfo);
			warning = LanguageHelper.GetMsg(LMSG.titleWarning);
			error = LanguageHelper.GetMsg(LMSG.titleError);
			question = LanguageHelper.GetMsg(LMSG.titleQuestion);
		}
		public static void Show(string strMsg)
		{
			MessageBox.Show(strMsg, info,
							MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		public static void Warning(string strWarn)
		{
			MessageBox.Show(strWarn, warning,
							MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		public static void Error(string strError)
		{
			MessageBox.Show(strError, error,
							MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		public static bool Question(string strQues)
		{
			if(MessageBox.Show(strQues, question,
							   MessageBoxButtons.OKCancel,
							   MessageBoxIcon.Question)==DialogResult.OK)
				return true;
			else
				return false;
		}
		public static void Show(LMSG msg)
		{
			MessageBox.Show(LanguageHelper.GetMsg(msg), info,
							MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		public static void Warning(LMSG msg)
		{
			MessageBox.Show(LanguageHelper.GetMsg(msg), warning,
							MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		public static void Error(LMSG msg)
		{
			MessageBox.Show(LanguageHelper.GetMsg(msg), error,
							MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		public static bool Question(LMSG msg)
		{
			if(MessageBox.Show(LanguageHelper.GetMsg(msg), question,
							   MessageBoxButtons.OKCancel,
							   MessageBoxIcon.Question)==DialogResult.OK)
				return true;
			else
				return false;
		}
	}
}
