/*
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

		#region init 函数提示菜单
		//自动完成
		AutocompleteMenu popupMenu;
		string nowFile;
		string title;
		string oldtext;
		SortedList<string, string> tooltipDic;
		AutocompleteItem[] items;
		bool tabisspaces = false;
		string nowcdb;
		public CodeEditForm()
		{
			InitForm();
		}
		void InitForm()
		{
			cardlist = new SortedDictionary<long, string>();
			tooltipDic = new SortedList<string, string>();
			InitializeComponent();
			//设置字体，大小
			string fontname = MyConfig.readString(MyConfig.TAG_FONT_NAME);
			float fontsize = MyConfig.readFloat(MyConfig.TAG_FONT_SIZE, fctb.Font.Size);
			fctb.Font = new Font(fontname, fontsize);
			if (MyConfig.readBoolean(MyConfig.TAG_IME))
				fctb.ImeMode = ImeMode.On;
			if (MyConfig.readBoolean(MyConfig.TAG_WORDWRAP))
				fctb.WordWrap = true;
			else
				fctb.WordWrap = false;
			if (MyConfig.readBoolean(MyConfig.TAG_TAB2SPACES))
				tabisspaces = true;
			else
				tabisspaces = false;

			Font ft = new Font(fctb.Font.Name, fctb.Font.Size / 1.2f, FontStyle.Regular);
			popupMenu = new FastColoredTextBoxNS.AutocompleteMenu(fctb);
			popupMenu.MinFragmentLength = 2;
			popupMenu.Items.Font = ft;
			popupMenu.Items.MaximumSize = new System.Drawing.Size(200, 400);
			popupMenu.Items.Width = 300;
			popupMenu.BackColor = fctb.BackColor;
			popupMenu.ForeColor = fctb.ForeColor;
			popupMenu.Closed += new ToolStripDropDownClosedEventHandler(popupMenu_Closed);

			popupMenu.SelectedColor = Color.LightGray;

			title = this.Text;
		}

		void popupMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			popupMenu.Items.SetAutocompleteItems(items);
		}
		#endregion

		#region IEditForm接口
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
		public bool Save()
		{
			return savefile(string.IsNullOrEmpty(nowFile));
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
				SetCardDB(cdb);//后台加载卡片数据
				fctb.OpenFile(nowFile, new UTF8Encoding(false));
				oldtext = fctb.Text;
				SetTitle();
				return true;
			}
			return false;
		}

		#endregion

		#region 文档视图
		//文档视图
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

		#region 设置标题
		void SetTitle()
		{
			string str = title;
			if (string.IsNullOrEmpty(nowFile))
				str = title;
			else
				str = nowFile + "-" + title;
			if (this.MdiParent != null)//如果父容器不为空
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

		#region 自动完成
		public void LoadXml(string xmlfile)
		{
			fctb.DescriptionFile = xmlfile;
		}
		public void InitTooltip(CodeConfig codeconfig)
		{
			this.tooltipDic = codeconfig.TooltipDic;
			items = codeconfig.Items;
			popupMenu.Items.SetAutocompleteItems(items);
		}
		#endregion
	   
		#region 悬停的函数说明
		//查找函数说明
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

		//悬停的函数说明
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
					//获取卡片信息
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

		#region 保存文件
		bool savefile(bool saveas)
		{
			string alltext = fctb.Text;
			if (!tabisspaces)
				alltext = alltext.Replace("	", "\t");
			if (saveas)
			{
				using (SaveFileDialog sfdlg = new SaveFileDialog())
				{
					sfdlg.Filter = LanguageHelper.GetMsg(LMSG.ScriptFilter);
					if (sfdlg.ShowDialog() == DialogResult.OK)
					{
						nowFile = sfdlg.FileName;
						SetTitle();
					}
					else
						return false;
				}
			}
			oldtext = fctb.Text;
			File.WriteAllText(nowFile, alltext, new UTF8Encoding(false));
			return true;
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
		#endregion

		#region 菜单
		//显示/隐藏输入框
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
		//如果是作为mdi，则隐藏菜单
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
		void Menuitem_findClick(object sender, EventArgs e)
		{
			fctb.ShowFindDialog();
		}

		void Menuitem_replaceClick(object sender, EventArgs e)
		{
			fctb.ShowReplaceDialog();
		}

		void QuitToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Close();
		}

		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			MyMsg.Show(
				LanguageHelper.GetMsg(LMSG.About) + "\t" + Application.ProductName + "\n"
				+ LanguageHelper.GetMsg(LMSG.Version) + "\t1.1.0.0\n"
				+ LanguageHelper.GetMsg(LMSG.Author) + "\t菜菜");
		}

		void Menuitem_openClick(object sender, EventArgs e)
		{
			using (OpenFileDialog sfdlg = new OpenFileDialog())
			{
				sfdlg.Filter = LanguageHelper.GetMsg(LMSG.ScriptFilter);
				if (sfdlg.ShowDialog() == DialogResult.OK)
				{
					nowFile = sfdlg.FileName;
					fctb.OpenFile(nowFile, new UTF8Encoding(false));
				}
			}
		}

		#endregion

		#region 搜索函数
		//搜索函数
		void Tb_inputKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				string key = tb_input.Text;
				List<AutocompleteItem> list =new List<AutocompleteItem>();
				foreach (AutocompleteItem item in items)
				{
					if (item.ToolTipText.Contains(key))
						list.Add(item);
				}
				popupMenu.Items.SetAutocompleteItems(list.ToArray());
				popupMenu.Show(true);
			}
		}
		#endregion

		#region 提示保存
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

		#region 卡片提示
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

		#region 选择高亮
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

		#region 调转函数
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
