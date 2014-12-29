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
    public static class LANG
    {
        static Dictionary<string, string> mWordslist = new Dictionary<string, string>();
        static SortedList<LMSG, string> msglist = new SortedList<LMSG, string>();
        static string SEP_CONTROL = ".";
        static string SEP_LINE = " ";

        #region 获取消息文字
        public static string GetMsg(LMSG lMsg)
        {
            if (msglist.IndexOfKey(lMsg) >= 0)
                return msglist[lMsg];
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
            if (mWordslist.TryGetValue(key, out v))
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
                if (GetLabel(pName +SEP_CONTROL + tsmi.Name, out title))
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
        public static void GetFormLabel(Form fm)
        {
            if (fm == null)
                return;
            // fm.SuspendLayout();
          //fm.ResumeLayout(true);
            GetControlLabel(fm, "", fm.Name);
            //fm.ResumeLayout(false);
            //fm.PerformLayout();
        }

        static void AddLabel(string key, string title)
        {
            if (!mWordslist.ContainsKey(key))
                mWordslist.Add(key, title);
        }

        static void GetControlLabel(Control c, string pName, 
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

        static void GetMenuItem(string pName, ToolStripItem tsi)
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
        public static bool SaveLanguage(string conf)
        {
            using (FileStream fs = new FileStream(conf, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                foreach (string k in mWordslist.Keys)
                {
                    sw.WriteLine(k + SEP_LINE + mWordslist[k]);
                }
                sw.Close();
                fs.Close();
            }
            return true;
        }

        public static bool SaveMessage(string f)
        {
            using (FileStream fs = new FileStream(f, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                foreach (LMSG k in msglist.Keys)
                {
                    sw.WriteLine("0x" + ((uint)k).ToString("x") + "\t" + msglist[k].Replace("\n", "/n"));
                }
                foreach (LMSG k in Enum.GetValues(typeof(LMSG)))
                {
                    if (!msglist.ContainsKey(k))
                        sw.WriteLine("0x" + ((uint)k).ToString("x") + "\t" + k.ToString());
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
            mWordslist.Clear();
            using (FileStream fs = new FileStream(f, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                string line, sk, v;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith("#")&&line.Length>0)
                    {
                        int si = line.IndexOf(SEP_LINE);
                        if (si > 0)
                        {
                            sk = line.Substring(0, si);
                            v = line.Substring(si + 1);

                            if (!mWordslist.ContainsKey(sk))
                                mWordslist.Add(sk, v);
                        }
                    }
                }
                sr.Close();
                fs.Close();
            }

        }

        public static void LoadMessage(string f)
        {
            if (!File.Exists(f))
                return;
            msglist.Clear();
            using (FileStream fs = new FileStream(f, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                string line, sk, v;
                uint utemp;
                LMSG ltemp;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith("#"))
                    {
                        int si = line.IndexOf("\t");
                        if (si > 0)
                        {
                            sk = line.Substring(0, si);
                            v = line.Substring(si + 1);
                            if (sk.StartsWith("0x"))
                                uint.TryParse(sk.Replace("0x", ""), NumberStyles.HexNumber, null, out utemp);
                            else
                                uint.TryParse(sk, out utemp);
                            ltemp = (LMSG)utemp;
                            if (msglist.IndexOfKey(ltemp) < 0)
                                msglist.Add(ltemp, v.Replace("/n", "\n"));
                        }
                    }
                }
                sr.Close();
                fs.Close();
            }
        }
        #endregion
    }

}
