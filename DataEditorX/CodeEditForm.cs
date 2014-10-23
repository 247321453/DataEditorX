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

namespace DataEditorX
{
	/// <summary>
	/// Description of CodeEditForm.
	/// </summary>
	public partial class CodeEditForm : DockContent
	{
		#region Style
		TextStyle KeyStyle = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
		TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold|FontStyle.Italic);
		TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
		TextStyle NumberStyle = new TextStyle(Brushes.Orange, null, FontStyle.Regular);
		TextStyle ConStyle = new TextStyle(Brushes.YellowGreen, null, FontStyle.Regular);
		TextStyle YellowStyle = new TextStyle(Brushes.Yellow, null, FontStyle.Italic);
		TextStyle FunStyle = new TextStyle(Brushes.SlateGray, null, FontStyle.Bold);
		MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.White)));
		#endregion
		
		#region init
		AutocompleteMenu popupMenu;
		AutocompleteMenu popupMenu_fun;
		AutocompleteMenu popupMenu_con;
		AutocompleteMenu popupMenu_find;
		string nowFile;
		string title;
		Dictionary<string,string> tooltipDic;
		public CodeEditForm()
		{
			InitForm();
		}
		public CodeEditForm(string file)
		{
			InitForm();
			Open(file);
		}
		void InitForm()
		{
			tooltipDic=new Dictionary<string, string>();
			InitializeComponent();
			Font ft=new Font(fctb.Font.Name,fctb.Font.Size/1.2f,FontStyle.Regular);
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
			title=this.Text;
		}

		public void LoadXml(string xmlfile)
		{
			fctb.DescriptionFile=xmlfile;
		}
		
		#endregion
		
		#region Open
		public void Open(string file)
		{
			if(!string.IsNullOrEmpty(file))
			{
				if(!File.Exists(file))
				{
					FileStream fs=new FileStream(file, FileMode.Create);
					fs.Close();
				}
				nowFile=file;
				fctb.OpenFile(nowFile, new UTF8Encoding(false));
				SetTitle();
			}
		}
		void HideMenu()
		{
			if(this.MdiParent ==null)
				return;
			menuStrip1.Visible=false;
			menuitem_file.Visible=false;
			menuitem_file.Enabled=false;
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
			if(menuitem_showmap.Checked)
			{
				documentMap1.Visible=false;
				menuitem_showmap.Checked=false;
			}else{
				documentMap1.Visible=true;
				menuitem_showmap.Checked=true;
			}
		}
		#endregion
		
		#region folding
		void FctbTextChangedDelayed(object sender, TextChangedEventArgs e)
		{
			//delete all markers
			fctb.Range.ClearFoldingMarkers();

			var currentIndent = 0;
			var lastNonEmptyLine = 0;

			for (int i = 0; i < fctb.LinesCount; i++)
			{
				var line = fctb[i];
				var spacesCount = line.StartSpacesCount;
				if (spacesCount == line.Count) //empty line
					continue;
				if (currentIndent < spacesCount)
					//append start folding marker
					fctb[lastNonEmptyLine].FoldingStartMarker = "m" + currentIndent;
				else if (currentIndent > spacesCount)
					//append end folding marker
					fctb[lastNonEmptyLine].FoldingEndMarker = "m" + spacesCount;
				currentIndent = spacesCount;
				lastNonEmptyLine = i;
			}
		}
		#endregion
		
		#region title
		void SetTitle()
		{
			string str=title;
			if(string.IsNullOrEmpty(nowFile))
				str=title;
			else
				str=nowFile;
			if(this.MdiParent !=null)
			{
				if(string.IsNullOrEmpty(nowFile))
					this.Text=title;
				else
					this.Text=Path.GetFileName(nowFile);
				this.MdiParent.Text=str;
			}
			else
				this.Text=str;
		}

		void CodeEditFormEnter(object sender, EventArgs e)
		{
			SetTitle();
		}
		#endregion
		
		#region tooltip
		public void InitTooltip(Dictionary<string,string> tooltipDic
		                        ,AutocompleteItem[] funlist
		                        ,AutocompleteItem[] conlist)
		{
			this.tooltipDic=tooltipDic;
			
			List<AutocompleteItem> items=new List<AutocompleteItem>();
			items.AddRange(funlist);
			items.AddRange(conlist);
			popupMenu.Items.SetAutocompleteItems(items);
			popupMenu_con.Items.SetAutocompleteItems(conlist);
			popupMenu_fun.Items.SetAutocompleteItems(funlist);
		}

		string FindTooltip(string word)
		{
			string desc="";
			foreach(string v in tooltipDic.Keys)
			{
				int t=v.IndexOf(".");
				string k=v;
				if(t>0)
					k=v.Substring(t+1);
				if(word==k)
					desc=tooltipDic[v];
			}
			return desc;
		}
		void FctbToolTipNeeded(object sender, ToolTipNeededEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.HoveredWord))
			{
				string desc=FindTooltip(e.HoveredWord);
				if(!string.IsNullOrEmpty(desc))
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
			}else if (e.KeyData == (Keys.T | Keys.Control))
			{
				//forced show (MinFragmentLength will be ignored)
				popupMenu_con.Show(true);
				e.Handled = true;
			}
		}
		#endregion
		
		#region input
		void FctbSelectionChanged(object sender, EventArgs e)
		{
			tb_input.Text=fctb.SelectedText;
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
			foreach(var r in ranges)
				r.SetStyle(SameWordsStyle);
		}
		
		void Menuitem_showinputClick(object sender, EventArgs e)
		{
			if(menuitem_showinput.Checked)
			{
				menuitem_showinput.Checked=false;
				tb_input.Visible=false;
			}
			else{
				menuitem_showinput.Checked=true;
				tb_input.Visible=true;
			}
		}
		#endregion
		
		#region menu
		public void Save()
		{
			if(string.IsNullOrEmpty(nowFile))
			{
				using(SaveFileDialog sfdlg=new SaveFileDialog())
				{
					sfdlg.Filter="Script(*.lua)|*.lua|All Files(*.*)|*.*";
					if(sfdlg.ShowDialog()==DialogResult.OK)
					{
						nowFile=sfdlg.FileName;
						SetTitle();
					}
					else
						return;
				}
			}
			fctb.SaveToFile(nowFile, new UTF8Encoding(false));
		}
		public void SaveAs()
		{
			using(SaveFileDialog sfdlg=new SaveFileDialog())
			{
				sfdlg.Filter="Script(*.lua)|*.lua|All Files(*.*)|*.*";
				if(sfdlg.ShowDialog()==DialogResult.OK)
				{
					nowFile=sfdlg.FileName;
				}
				else
					return;
			}
			fctb.SaveToFile(nowFile, new UTF8Encoding(false));
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
				LANG.GetMsg(LMSG.About)+"\t"+Application.ProductName+"\n"
				+LANG.GetMsg(LMSG.Version)+"\t1.1.0.0\n"
				+LANG.GetMsg(LMSG.Author)+"\t247321453\n"
				+"Email:\t247321453@qq.com");
		}
		
		void Menuitem_openClick(object sender, EventArgs e)
		{
			using(OpenFileDialog sfdlg=new OpenFileDialog())
			{
				sfdlg.Filter="Script(*.lua)|*.lua|All Files(*.*)|*.*";
				if(sfdlg.ShowDialog()==DialogResult.OK)
				{
					nowFile=sfdlg.FileName;
					fctb.OpenFile(nowFile, new UTF8Encoding(false));
				}
			}
		}
		
		#endregion
		
		void FctbTextChanged(object sender, TextChangedEventArgs e)
		{
			LuaSyntaxHighlight(e);
		}
		private void LuaSyntaxHighlight(TextChangedEventArgs e)
		{
			//fctb.LeftBracket = '(';
			//fctb.RightBracket = ')';
			//fctb.LeftBracket2 = '\x0';
			//fctb.RightBracket2 = '\x0';
			//clear style of changed range
			e.ChangedRange.ClearStyle(YellowStyle, BoldStyle, GrayStyle, NumberStyle, FunStyle, ConStyle);

			//string highlighting
			e.ChangedRange.SetStyle(YellowStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
			//comment highlighting
			e.ChangedRange.SetStyle(GrayStyle, @"--.*$", RegexOptions.Multiline);
			e.ChangedRange.SetStyle(GrayStyle, @"--\[\[[\S\s]*?|--\[\[[\S\s]*?\]\]--|[\S\s]*?\]\]--",  RegexOptions.Multiline);
			e.ChangedRange.SetStyle(GrayStyle, @"--\[\[[\S\s]*?|--\[\[[\S\s]*?\]\]--|[\S\s]*?\]\]--", RegexOptions.Multiline|RegexOptions.RightToLeft);
			//number highlighting
			e.ChangedRange.SetStyle(NumberStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b|\bc[0-9]+\b");
			//attribute highlighting
			//e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
			//class name highlighting
			e.ChangedRange.SetStyle(BoldStyle, @"\b(Effect|Card|Group|Duel|Debug)\b");
			//keyword highlighting
			e.ChangedRange.SetStyle(KeyStyle, @"\b(and|break|do|else|elseif|end|false|for|function|goto|if|in|local|nil|not|or|repeat|return|then|true|until|while)\b");
			//constant
			e.ChangedRange.SetStyle(ConStyle, @"[\s|\(|+|,]{0,1}(?<range>[A-Z_]+?)[\)|+|\s|,]");
			//function
			//e.ChangedRange.SetStyle(FunStyle, @"[:|.|\s](?<range>[^\(]*?)[\(|\)|\s]");
			
			//clear folding markers
			e.ChangedRange.ClearFoldingMarkers();

			//set folding markers
			//e.ChangedRange.SetFoldingMarkers(@"--\[\[[\S\s]*?\]\]--", RegexOptions.Multiline);//allow to collapse comment block
			e.ChangedRange.SetFoldingMarkers("if\b", "end\b");//allow to collapse brackets block
			e.ChangedRange.SetFoldingMarkers("function\b", "end\b");//allow to collapse #region blocks
			
		}
		void Menuitem_findClick(object sender, EventArgs e)
		{
			fctb.ShowFindDialog();
		}
		
		void Menuitem_replaceClick(object sender, EventArgs e)
		{
			fctb.ShowReplaceDialog();
		}
		
		#region find
		void Tb_inputKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				//
				string key=tb_input.Text;
				List<AutocompleteItem> tlist=new List<AutocompleteItem>();
				foreach(string k in tooltipDic.Keys)
				{
					if(tooltipDic[k].IndexOf(key)>=0)
					{
						AutocompleteItem ai=new AutocompleteItem(k);
						ai.ToolTipTitle=k;
						ai.ToolTipText=tooltipDic[k];
						tlist.Add(ai);
					}
				}
				popupMenu_find.Items.SetAutocompleteItems(tlist.ToArray());
				popupMenu_find.Show(true);
			}
		}
		#endregion
	}
}
