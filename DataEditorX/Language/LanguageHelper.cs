/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 7月8 星期二
 * 时间: 9:52
 * 
 */
using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataEditorX.Language
{
	/// <summary>
	/// Description of Language.
	/// </summary>
	public class LanguageHelper
	{
		static Dictionary<string, string> gWordsList = new Dictionary<string, string>();
		static SortedList<LMSG, string> gMsgList = new SortedList<LMSG, string>();
		const char SEP_CONTROL = '.';
		const char SEP_LINE = '\t';
		Dictionary<string, string> mWordslist = new Dictionary<string, string>();

		#region 获取消息文字
		public static string GetMsg(LMSG lMsg)
		{
			if (gMsgList.IndexOfKey(lMsg) >= 0)
				return gMsgList[lMsg];
			else
				return lMsg.ToString().Replace("_", " ");
		}
		#endregion

		#region 设置控件信息
		/// <summary>
		/// 设置控件文字
		/// </summary>
		/// <param name="fm"></param>
		public static void SetFormLabel(Form fm)
		{
			if (fm == null)
				return;
			// fm.SuspendLayout();
			fm.ResumeLayout(true);
			SetControlLabel(fm, "", fm.Name);
			fm.ResumeLayout(false);
			//fm.PerformLayout();
		}

		static bool GetLabel(string key, out string title)
		{
			string v;
			if (gWordsList.TryGetValue(key, out v))
			{
				title = v;
				return true;
			}
			title = null;
			return false;
		}

		static void SetControlLabel(Control c, string pName, string formName)
		{
			if (!string.IsNullOrEmpty(pName))
				pName += SEP_CONTROL;
			pName += c.Name;
			string title;
			if (c is ListView)
			{
				ListView lv = (ListView)c;
				int i, count = lv.Columns.Count;
				for (i = 0; i < count; i++)
				{
					ColumnHeader ch = lv.Columns[i];
					if (GetLabel(pName + SEP_CONTROL + i.ToString(), out title))
						ch.Text = title;
				}
			}
			else if (c is ToolStrip)
			{
				ToolStrip ms = (ToolStrip)c;
				foreach (ToolStripItem tsi in ms.Items)
				{
					SetMenuItem(formName + SEP_CONTROL + ms.Name, tsi);
				}
			}
			else
			{
				if (GetLabel(pName, out title))
					c.Text = title;
			}

			if (c.Controls.Count > 0)
			{
				foreach (Control sc in c.Controls)
				{
					SetControlLabel(sc, pName, formName);
				}
			}
			ContextMenuStrip conms = c.ContextMenuStrip;
			if (conms != null)
			{
				foreach (ToolStripItem ts in conms.Items)
				{
					SetMenuItem(formName + SEP_CONTROL + conms.Name, ts);
				}
			}
		}

		static void SetMenuItem(string pName, ToolStripItem tsi)
		{
			string title;

			if (tsi is ToolStripMenuItem)
			{
				ToolStripMenuItem tsmi = (ToolStripMenuItem)tsi;
				if (GetLabel(pName + SEP_CONTROL + tsmi.Name, out title))
					tsmi.Text = title;
				if (tsmi.HasDropDownItems)
				{
					foreach (ToolStripItem subtsi in tsmi.DropDownItems)
					{
						SetMenuItem(pName, subtsi);
					}
				}
			}
			else if (tsi is ToolStripLabel)
			{
				ToolStripLabel tlbl = (ToolStripLabel)tsi;
				if (GetLabel(pName + SEP_CONTROL + tlbl.Name, out title))
					tlbl.Text = title;
			}
		}

		#endregion

		#region 获取控件信息
		public void GetFormLabel(Form fm)
		{
			if (fm == null)
				return;
			// fm.SuspendLayout();
			//fm.ResumeLayout(true);
			GetControlLabel(fm, "", fm.Name);
			//fm.ResumeLayout(false);
			//fm.PerformLayout();
		}

		void AddLabel(string key, string title)
		{
			if (!mWordslist.ContainsKey(key))
				mWordslist.Add(key, title);
		}

		void GetControlLabel(Control c, string pName,
			string formName)
		{
			if (!string.IsNullOrEmpty(pName))
				pName += SEP_CONTROL;
			if (string.IsNullOrEmpty(c.Name))
				return;
			pName += c.Name;
			if (c is ListView)
			{
				ListView lv = (ListView)c;
				int i, count = lv.Columns.Count;
				for (i = 0; i < count; i++)
				{
					AddLabel(pName + SEP_CONTROL + i.ToString(),
						lv.Columns[i].Text);
				}
			}
			else if (c is ToolStrip)
			{
				ToolStrip ms = (ToolStrip)c;
				foreach (ToolStripItem tsi in ms.Items)
				{
					GetMenuItem(formName + SEP_CONTROL + ms.Name, tsi);
				}
			}
			else
			{
				AddLabel(pName, c.Text);
			}

			if (c.Controls.Count > 0)
			{
				foreach (Control sc in c.Controls)
				{
					GetControlLabel(sc, pName, formName);
				}
			}
			ContextMenuStrip conms = c.ContextMenuStrip;
			if (conms != null)
			{
				foreach (ToolStripItem ts in conms.Items)
				{
					GetMenuItem(formName + SEP_CONTROL + conms.Text, ts);
				}
			}
		}

		void GetMenuItem(string pName, ToolStripItem tsi)
		{
			if (string.IsNullOrEmpty(tsi.Name))
				return;
			if (tsi is ToolStripMenuItem)
			{
				ToolStripMenuItem tsmi = (ToolStripMenuItem)tsi;
				AddLabel(pName + SEP_CONTROL + tsmi.Name, tsmi.Text);
				if (tsmi.HasDropDownItems)
				{
					foreach (ToolStripItem subtsi in tsmi.DropDownItems)
					{
						GetMenuItem(pName, subtsi);
					}
				}
			}
			else if (tsi is ToolStripLabel)
			{
				ToolStripLabel tlbl = (ToolStripLabel)tsi;
				AddLabel(pName + SEP_CONTROL + tlbl.Name, tlbl.Text);
			}
		}

		#endregion

		#region 保存语言文件
		public bool SaveLanguage(string conf)
		{
			using (FileStream fs = new FileStream(conf, FileMode.Create, FileAccess.Write))
			{
				StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
				foreach (string k in mWordslist.Keys)
				{
					sw.WriteLine(k + SEP_LINE + mWordslist[k]);
				}
				sw.WriteLine("#");
				foreach (LMSG k in gMsgList.Keys)
				{
					//记得替换换行符
					sw.WriteLine("0x" + ((uint)k).ToString("x") + SEP_LINE + gMsgList[k].Replace("\n", "\\n"));
				}
				foreach (LMSG k in Enum.GetValues(typeof(LMSG)))
				{
					if (!gMsgList.ContainsKey(k))
						sw.WriteLine("0x" + ((uint)k).ToString("x") + SEP_LINE + k.ToString());
				}
				sw.Close();
				fs.Close();
			}
			return true;
		}
		#endregion

		#region 加载语言文件
		public static void LoadFormLabels(string f)
		{
			if (!File.Exists(f))
				return;
			gWordsList.Clear();
			gMsgList.Clear();
			using (FileStream fs = new FileStream(f, FileMode.Open, FileAccess.Read))
			{
				StreamReader sr = new StreamReader(fs, Encoding.UTF8);
				string line;
				uint utemp;
				LMSG ltemp;
				while ((line = sr.ReadLine()) != null)
				{
					if (line.Length == 0)
						continue;
					string[] words = line.Split(SEP_LINE);
					if (words.Length < 2)
						continue;
					if (line.StartsWith("0x"))//加载消息文字
					{
						uint.TryParse(words[0].Replace("0x", ""), NumberStyles.HexNumber, null, out utemp);
						ltemp = (LMSG)utemp;
						if (gMsgList.IndexOfKey(ltemp) < 0)//记得替换换行符
							gMsgList.Add(ltemp, words[1].Replace("\\n", "\n"));
					}
					else if (!line.StartsWith("#"))//加载界面语言
					{
						if (!gWordsList.ContainsKey(words[0]))
							gWordsList.Add(words[0], words[1]);
					}
				}
				sr.Close();
				fs.Close();
			}

		}
		#endregion
	}

}
