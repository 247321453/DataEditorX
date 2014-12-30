﻿/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-22
 * 时间: 19:16
 * 
 */
using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using FastColoredTextBoxNS;
using DataEditorX.Language;
using System.Text.RegularExpressions;
using DataEditorX.Core;
using DataEditorX.Config;
using System.Configuration;
using DataEditorX.Controls;

namespace DataEditorX
{
    /// <summary>
    /// Description of CodeEditForm.
    /// </summary>
    public partial class CodeEditForm : DockContent, IEditForm
    {
        #region Style
        SortedDictionary<long, string> cardlist;
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.White)));
        #endregion

        #region init
        AutocompleteMenu popupMenu;
        AutocompleteMenu popupMenu_fun;
        AutocompleteMenu popupMenu_con;
        AutocompleteMenu popupMenu_find;
        string nowFile;
        string title;
        string oldtext;
        Dictionary<string, string> tooltipDic;
        bool tabisspaces = false;
        string nowcdb;
        public CodeEditForm()
        {
            InitForm();
        }
        void InitForm()
        {
            cardlist = new SortedDictionary<long, string>();
            tooltipDic = new Dictionary<string, string>();
            InitializeComponent();
            Font ft = new Font(fctb.Font.Name, fctb.Font.Size / 1.2f, FontStyle.Regular);
            popupMenu = new FastColoredTextBoxNS.AutocompleteMenu(fctb);
            popupMenu.MinFragmentLength = 2;
            popupMenu.Items.Font = ft;
            popupMenu.Items.MaximumSize = new System.Drawing.Size(200, 400);
            popupMenu.Items.Width = 300;

            popupMenu_fun = new FastColoredTextBoxNS.AutocompleteMenu(fctb);
            popupMenu_fun.MinFragmentLength = 2;
            popupMenu_fun.Items.Font = ft;
            popupMenu_fun.Items.MaximumSize = new System.Drawing.Size(200, 400);
            popupMenu_fun.Items.Width = 300;

            popupMenu_con = new FastColoredTextBoxNS.AutocompleteMenu(fctb);
            popupMenu_con.MinFragmentLength = 2;
            popupMenu_con.Items.Font = ft;
            popupMenu_con.Items.MaximumSize = new System.Drawing.Size(200, 400);
            popupMenu_con.Items.Width = 300;

            popupMenu_find = new FastColoredTextBoxNS.AutocompleteMenu(fctb);
            popupMenu_find.MinFragmentLength = 2;
            popupMenu_find.Items.Font = ft;
            popupMenu_find.Items.MaximumSize = new System.Drawing.Size(200, 400);
            popupMenu_find.Items.Width = 300;
            title = this.Text;

            string fontname = MyConfig.readString(CodeConfig.TAG_FONT_NAME);
            float fontsize = MyConfig.readFloat(CodeConfig.TAG_FONT_SIZE, 14);
            fctb.Font = new Font(fontname, fontsize);
            if (MyConfig.readBoolean(CodeConfig.TAG_IME))
                fctb.ImeMode = ImeMode.On;
            if (MyConfig.readBoolean(CodeConfig.TAG_WORDWRAP))
                fctb.WordWrap = true;
            else
                fctb.WordWrap = false;
            if (MyConfig.readBoolean(CodeConfig.TAG_TAB2SPACES))
                tabisspaces = true;
            else
                tabisspaces = false;
        }

        public void LoadXml(string xmlfile)
        {
            fctb.DescriptionFile = xmlfile;
        }

        #endregion

        #region Open
        public void SetActived()
        {
            this.Activate();
        }
        public bool CanOpen(string file)
        {
            return YGOUtil.isScript(file);
        }
        public string GetOpenFile()
        {
            return nowFile;
        }
        public bool Create(string file)
        {
            return Open(file);
        }
        public bool Open(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    FileStream fs = new FileStream(file, FileMode.Create);
                    fs.Close();
                }
                nowFile = file;
                string cdb = MyPath.Combine(
                    Path.GetDirectoryName(file), "../cards.cdb");
                SetCardDB(cdb);
                fctb.OpenFile(nowFile, new UTF8Encoding(false));
                oldtext = fctb.Text;
                SetTitle();
                return true;
            }
            return false;
        }

        void HideMenu()
        {
            if (this.MdiParent == null)
                return;
            mainMenu.Visible = false;
            menuitem_file.Visible = false;
            menuitem_file.Enabled = false;
        }

        void CodeEditFormLoad(object sender, EventArgs e)
        {
            HideMenu();
            fctb.OnTextChangedDelayed(fctb.Range);
        }
        #endregion

        #region doc map
        void ShowMapToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (menuitem_showmap.Checked)
            {
                documentMap1.Visible = false;
                menuitem_showmap.Checked = false;
                fctb.Width += documentMap1.Width;
            }
            else
            {
                documentMap1.Visible = true;
                menuitem_showmap.Checked = true;
                fctb.Width -= documentMap1.Width;
            }
        }
        #endregion

        #region title
        void SetTitle()
        {
            string str = title;
            if (string.IsNullOrEmpty(nowFile))
                str = title;
            else
                str = nowFile + "-" + title;
            if (this.MdiParent != null)
            {
                if (string.IsNullOrEmpty(nowFile))
                    this.Text = title;
                else
                    this.Text = Path.GetFileName(nowFile);
                this.MdiParent.Text = str;
            }
            else
                this.Text = str;
        }

        void CodeEditFormEnter(object sender, EventArgs e)
        {
            SetTitle();
        }
        #endregion

        #region tooltip
        public void InitTooltip(CodeConfig codeconfig)
        {
            this.tooltipDic = codeconfig.TooltipDic;
            List<AutocompleteItem> items = new List<AutocompleteItem>();
            items.AddRange(codeconfig.FunList);
            items.AddRange(codeconfig.ConList);
            popupMenu.Items.SetAutocompleteItems(items);
            popupMenu_con.Items.SetAutocompleteItems(codeconfig.ConList);
            popupMenu_fun.Items.SetAutocompleteItems(codeconfig.FunList);
        }

        string FindTooltip(string word)
        {
            string desc = "";
            foreach (string v in tooltipDic.Keys)
            {
                int t = v.IndexOf(".");
                string k = v;
                if (t > 0)
                    k = v.Substring(t + 1);
                if (word == k)
                    desc = tooltipDic[v];
            }
            return desc;
        }
        void FctbToolTipNeeded(object sender, ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                long tl = 0;
                string name = e.HoveredWord;
                string desc = "";
                if (!name.StartsWith("0x") && name.Length <= 9)
                {
                    name = name.Replace("c", "");
                    long.TryParse(name, out tl);
                }

                if (tl > 0)
                {
                    if (cardlist.ContainsKey(tl))
                        desc = cardlist[tl];
                }
                else
                    desc = FindTooltip(e.HoveredWord);
                if (!string.IsNullOrEmpty(desc))
                {
                    e.ToolTipTitle = e.HoveredWord;
                    e.ToolTipText = desc;
                }
            }
        }
        #endregion

        #region Key
        void FctbKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.K | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                popupMenu_fun.Show(true);
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.T | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                popupMenu_con.Show(true);
                e.Handled = true;
            }
            //else if(e.KeyData == Keys(Keys.Control | Keys
        }
        #endregion

        #region input

        void Menuitem_showinputClick(object sender, EventArgs e)
        {
            if (menuitem_showinput.Checked)
            {
                menuitem_showinput.Checked = false;
                tb_input.Visible = false;
            }
            else
            {
                menuitem_showinput.Checked = true;
                tb_input.Visible = true;
            }
        }
        #endregion

        #region menu
        void Menuitem_findClick(object sender, EventArgs e)
        {
            fctb.ShowFindDialog();
        }

        void Menuitem_replaceClick(object sender, EventArgs e)
        {
            fctb.ShowReplaceDialog();
        }
        bool savefile(bool saveas)
        {
            string alltext = fctb.Text;
            if (!tabisspaces)
                alltext = alltext.Replace("    ", "\t");
            if (saveas)
            {
                using (SaveFileDialog sfdlg = new SaveFileDialog())
                {
                    sfdlg.Filter = LANG.GetMsg(LMSG.ScriptFilter);
                    if (sfdlg.ShowDialog() == DialogResult.OK)
                    {
                        nowFile = sfdlg.FileName;
                        SetTitle();
                    }
                    else
                        return false;
                }
            }
            File.WriteAllText(nowFile, alltext, new UTF8Encoding(false));
            return true;
        }
        public bool Save()
        {
            return savefile(string.IsNullOrEmpty(nowFile));
        }
        public bool SaveAs()
        {
            return savefile(true);
        }

        void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            Save();
        }
        void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            SaveAs();
        }

        void QuitToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Close();
        }

        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            MyMsg.Show(
                LANG.GetMsg(LMSG.About) + "\t" + Application.ProductName + "\n"
                + LANG.GetMsg(LMSG.Version) + "\t1.1.0.0\n"
                + LANG.GetMsg(LMSG.Author) + "\t柯永裕\n"
                + "Email:\t247321453@qq.com");
        }

        void Menuitem_openClick(object sender, EventArgs e)
        {
            using (OpenFileDialog sfdlg = new OpenFileDialog())
            {
                sfdlg.Filter = "Script(*.lua)|*.lua|All Files(*.*)|*.*";
                if (sfdlg.ShowDialog() == DialogResult.OK)
                {
                    nowFile = sfdlg.FileName;
                    fctb.OpenFile(nowFile, new UTF8Encoding(false));
                }
            }
        }

        #endregion

        #region find
        void Tb_inputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //
                string key = tb_input.Text;
                List<AutocompleteItem> tlist = new List<AutocompleteItem>();
                foreach (string k in tooltipDic.Keys)
                {
                    if (tooltipDic[k].IndexOf(key) >= 0)
                    {
                        AutocompleteItem ai = new AutocompleteItem(k);
                        ai.ToolTipTitle = k;
                        ai.ToolTipText = tooltipDic[k];
                        tlist.Add(ai);
                    }
                }
                popupMenu_find.Items.SetAutocompleteItems(tlist.ToArray());
                popupMenu_find.Show(true);
            }
        }
        #endregion

        #region close
        void CodeEditFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(oldtext))
            {
                if (fctb.Text != oldtext)
                {
                    if (MyMsg.Question(LMSG.IfSaveScript))
                        Save();
                }
            }
            else if (fctb.Text.Length > 0)
            {
                if (MyMsg.Question(LMSG.IfSaveScript))
                    Save();
            }
        }
        #endregion

        #region card tooltip
        public void SetCDBList(string[] cdbs)
        {
            if (cdbs == null)
                return;
            foreach (string cdb in cdbs)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(cdb);
                tsmi.Click += MenuItem_Click;
                menuitem_setcard.DropDownItems.Add(tsmi);
            }
        }
        void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi != null)
            {
                string file = tsmi.Text;
                SetCardDB(file);
            }
        }
        public void SetCardDB(string name)
        {
            nowcdb = name;
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (nowcdb != null && File.Exists(nowcdb))
                SetCards(DataBase.Read(nowcdb, true, ""));
        }
        public void SetCards(Card[] cards)
        {
            if (cards == null)
                return;
            cardlist.Clear();
            foreach (Card c in cards)
            {
                cardlist.Add(c.id, c.ToString());
            }
        }
        #endregion

        #region selection
        void FctbSelectionChangedDelayed(object sender, EventArgs e)
        {
            tb_input.Text = fctb.SelectedText;
            fctb.VisibleRange.ClearStyle(SameWordsStyle);
            if (!fctb.Selection.IsEmpty)
                return;//user selected diapason

            //get fragment around caret
            var fragment = fctb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            var ranges = fctb.VisibleRange.GetRanges("\\b" + text + "\\b");
            foreach (var r in ranges)
                r.SetStyle(SameWordsStyle);
        }
        #endregion

        #region goto function define
        void FctbMouseClick(object sender, MouseEventArgs e)
        {
            var fragment = fctb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control)
            {
                List<int> linenums = fctb.FindLines(@"function\s+?\S+?\." + text + @"\(", RegexOptions.Singleline);
                if (linenums.Count > 0)
                {
                    fctb.Navigate(linenums[0]);
                    //MessageBox.Show(linenums[0].ToString());
                }
            }
        }
        #endregion


    }
}
