/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-20
 * 时间: 9:19
 * 
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using WeifenLuo.WinFormsUI.Docking;

using FastColoredTextBoxNS;
using DataEditorX.Language;
using DataEditorX.Core;
using System.Text;

namespace DataEditorX
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		#region member
		bool isInitAuto=false;
		bool isInitDataEditor=false;
		string cdbHistoryFile;
		List<string> cdblist;
		string datapath;
		string conflang,conflang_de,conflang_ce,confmsg,conflang_pe;
		string funtxt,conlua,fieldtxt;
		DataEditForm compare1,compare2;
		Card[] tCards;
		Dictionary<DataEditForm,string> list;
		//
		DataConfig datacfg;
		//函数提示
		Dictionary<string,string> tooltipDic;
		//自动完成
		List<AutocompleteItem> funList;
		List<AutocompleteItem> conList;
		#endregion
		
		#region init
		public MainForm(string datapath, string file)
		{
			Init(datapath);
			if(file.EndsWith("lua",StringComparison.OrdinalIgnoreCase))
				OpenScript(file);
			else
				Open(file);
		}
		public MainForm(string datapath)
		{
			Init(datapath);
		}
		void Init(string datapath)
		{
			tCards=null;
			cdblist=new List<string>();
			tooltipDic=new Dictionary<string, string>();
			funList=new List<AutocompleteItem>();
			conList=new List<AutocompleteItem>();
			list=new Dictionary<DataEditForm,string>();
			this.datapath=datapath;
			datacfg=new DataConfig(datapath);
			cdbHistoryFile =Path.Combine(datapath, "history.txt");
			conflang = Path.Combine(datapath, "language-mainform.txt");
			conflang_de = Path.Combine(datapath, "language-dataeditor.txt");
			conflang_ce = Path.Combine(datapath, "language-codeeditor.txt");
			conflang_pe = Path.Combine(datapath, "language-puzzleditor.txt");
			fieldtxt= Path.Combine(datapath, "Puzzle.txt");
			confmsg = Path.Combine(datapath, "message.txt");
			funtxt = Path.Combine(datapath, "_functions.txt");
			conlua = Path.Combine(datapath, "constant.lua");
			InitializeComponent();
			LANG.InitForm(this, conflang);
			LANG.LoadMessage(confmsg);
			LANG.SetLanguage(this);
			ReadHistory();
			MenuHistory();
		}
		#endregion
		
		#region const
		public const int WM_OPEN=0x0401;
		public const int WM_OPEN_SCRIPT=0x0402;
		public const string TMPFILE="open.tmp";
		public const int MAX_HISTORY=0x20;
		public static bool isScript(string file)
		{
			if(file.EndsWith("lua",StringComparison.OrdinalIgnoreCase))
				return true;
			return false;
		}
		#endregion
		
		#region History
		void ReadHistory()
		{
			if(!File.Exists(cdbHistoryFile))
				return;
			string[] lines=File.ReadAllLines(cdbHistoryFile);
			foreach(string line in lines)
			{
				if(string.IsNullOrEmpty(line) || line.StartsWith("#"))
					continue;
				if(File.Exists(line) && cdblist.IndexOf(line)<0){
					cdblist.Add(line);
				}
			}
		}
		void AddHistory(string file)
		{
			int index=cdblist.IndexOf(file);
			if(index>=0){
				cdblist.RemoveAt(index);
			}
			else{
				int i=cdblist.Count-MainForm.MAX_HISTORY+1;
				while(i>=0 && i<cdblist.Count)
				{
					cdblist.RemoveAt(i);
					i--;
				}
			}
			cdblist.Add(file);
			SaveHistory();
			MenuHistory();
		}
		void SaveHistory()
		{
			string texts="# history";
			foreach(string str in cdblist)
			{
				if(File.Exists(str))
					texts += Environment.NewLine + str;
			}
			File.Delete(cdbHistoryFile);
			File.WriteAllText(cdbHistoryFile, texts);
		}
		void MenuHistory()
		{
			menuitem_history.DropDownItems.Clear();
			foreach(string str in cdblist)
			{
				ToolStripMenuItem tsmi=new ToolStripMenuItem(str);
				tsmi.Click+=MenuHistoryItem_Click;
				menuitem_history.DropDownItems.Add(tsmi);
			}
			menuitem_history.DropDownItems.Add(new ToolStripSeparator());
			ToolStripMenuItem tsmiclear=new ToolStripMenuItem(LANG.GetMsg(LMSG.ClearHistory));
			tsmiclear.Click+=MenuHistoryClear_Click;
			menuitem_history.DropDownItems.Add(tsmiclear);
			
		}
		void MenuHistoryClear_Click(object sender, EventArgs e)
		{
			cdblist.Clear();
			MenuHistory();
			SaveHistory();
		}
		void MenuHistoryItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi=sender as ToolStripMenuItem;
			if(tsmi!=null){
				string file=tsmi.Text;
				if(MainForm.isScript(file))
					OpenScript(file);
				else
					Open(tsmi.Text);
			}
		}
		#endregion
		
		#region message
		protected override void DefWndProc(ref System.Windows.Forms.Message m)
		{
			string file=null;
			switch (m.Msg)
			{
				case MainForm.WM_OPEN://处理消息
					file=Path.Combine(Application.StartupPath, MainForm.TMPFILE);
					if(File.Exists(file)){
						Open(File.ReadAllText(file));
						File.Delete(file);
					}
					break;
				case MainForm.WM_OPEN_SCRIPT:
					file=Path.Combine(Application.StartupPath, MainForm.TMPFILE);
					if(File.Exists(file)){
						OpenScript(File.ReadAllText(file));
						File.Delete(file);
					}
					break;
				default:
					base.DefWndProc(ref m);
					break;
			}
		}
		#endregion
		
		#region open

		public void OpenScript(string file)
		{
			if(!string.IsNullOrEmpty(file) && File.Exists(file)){
				AddHistory(file);
			}
			CodeEditForm cf=new CodeEditForm(file);
			LANG.InitForm(cf, conflang_ce);
			LANG.SetLanguage(cf);
			if(!isInitAuto)
			{
				isInitAuto=true;
				InitCodeEditor(funtxt, conlua);
			}
			cf.InitTooltip(tooltipDic, funList.ToArray(), conList.ToArray());
			//cf.SetIMEMode(ImeMode.Inherit);
			cf.Show(dockPanel1, DockState.Document);
		}
		public void Open(string file)
		{
			if(!string.IsNullOrEmpty(file) && File.Exists(file)){
				AddHistory(file);
			}
			if(checkOpen(file))
				return;
			if(OpenInNull(file))
				return;
			DataEditForm def;
			if(string.IsNullOrEmpty(file)|| !File.Exists(file))
				def=new DataEditForm(datapath);
			else
				def=new DataEditForm(datapath,file);
			LANG.InitForm(def, conflang_de);
			LANG.SetLanguage(def);
			if(!isInitDataEditor)
				datacfg.Init();
			def.InitGameData(datacfg);
			def.FormClosed+=new FormClosedEventHandler(def_FormClosed);
			def.Show(dockPanel1, DockState.Document);
			list.Add(def, "");
		}
		
		bool checkOpen(string file)
		{
			foreach(DataEditForm df in list.Keys)
			{
				if(df!=null && !df.IsDisposed)
				{
					if(df.getNowCDB()==file)
						return true;
				}
			}
			return false;
		}
		bool OpenInNull(string file)
		{
			if(string.IsNullOrEmpty(file) || !File.Exists(file))
				return false;
			foreach(DataEditForm df in list.Keys)
			{
				if(df!=null && !df.IsDisposed)
				{
					if(string.IsNullOrEmpty(df.getNowCDB())){
						df.Open(file);
						return true;
					}
				}
			}
			return false;
		}

		void def_FormClosed(object sender, FormClosedEventArgs e)
		{
			DataEditForm df=sender as DataEditForm;
			if(df!=null)
			{
				list.Remove(df);
			}
		}
		
		void DataEditorToolStripMenuItemClick(object sender, EventArgs e)
		{
			Open(null);
		}
		#endregion
		
		#region form
		void MainFormLoad(object sender, System.EventArgs e)
		{

		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			#if DEBUG
			LANG.SaveMessage(confmsg+".bak");
			#endif
		}
		#endregion
		
		#region windows
		void CloseToolStripMenuItemClick(object sender, EventArgs e)
		{
			dockPanel1.ActiveContent.DockHandler.Close();
		}
		void Menuitem_codeeditorClick(object sender, EventArgs e)
		{
			OpenScript(null);
		}
		void CloseMdi(bool isall)
		{
			DockContentCollection contents = dockPanel1.Contents;
			int num = contents.Count-1;
			try{
				while (num >=0)
				{
					if (contents[num].DockHandler.DockState == DockState.Document)
					{
						if(isall)
							contents[num].DockHandler.Close();
						else if(dockPanel1.ActiveContent != contents[num])
							contents[num].DockHandler.Close();
					}
					num--;
				}
			}catch{}
		}
		void CloseOtherToolStripMenuItemClick(object sender, EventArgs e)
		{
			CloseMdi(false);
		}
		
		void CloseAllToolStripMenuItemClick(object sender, EventArgs e)
		{
			CloseMdi(true);
		}
		#endregion
		
		#region file
		void Menuitem_openClick(object sender, EventArgs e)
		{
			using(OpenFileDialog dlg=new OpenFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.OpenFile);
				dlg.Filter=LANG.GetMsg(LMSG.OpenFileFilter);
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					string file=dlg.FileName;
					if(MainForm.isScript(file))
						OpenScript(file);
					else
						Open(file);
				}
			}
		}
		
		void QuitToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void Menuitem_newClick(object sender, EventArgs e)
		{
			using(SaveFileDialog dlg=new SaveFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.NewFile);
				dlg.Filter=LANG.GetMsg(LMSG.OpenFileFilter);
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					string file=dlg.FileName;
					if(MainForm.isScript(file)){
						File.Delete(file);
						OpenScript(file);
					}
					else
					{
						if(DataBase.Create(file))
						{
							if(MyMsg.Question(LMSG.IfOpenDataBase))
								Open(file);
						}
					}
				}
			}
		}
		void Menuitem_saveClick(object sender, EventArgs e)
		{
			CodeEditForm cf= dockPanel1.ActiveContent as CodeEditForm;
			if(cf!=null)
			{
				cf.Save();
				MyMsg.Show(LMSG.SaveFileOK);
			}
		}
		#endregion
		
		#region copy
		DataEditForm GetActive()
		{
			foreach(DataEditForm df in list.Keys)
			{
				if(df==dockPanel1.ActiveContent)
					return df;
			}
			return null;
		}
		
		void Menuitem_copyselecttoClick(object sender, EventArgs e)
		{
			DataEditForm df =GetActive();
			if(df!=null)
			{
				tCards=df.getCardList(true);
				if(tCards!=null){
					SetCopyNumber(tCards.Length);
					MyMsg.Show(LMSG.CopyCards);
				}
			}
		}
		void Menuitem_copyallClick(object sender, EventArgs e)
		{
			DataEditForm df =GetActive();
			if(df!=null)
			{
				tCards=df.getCardList(false);
				if(tCards!=null){
					SetCopyNumber(tCards.Length);
					MyMsg.Show(LMSG.CopyCards);
				}
			}
		}
		void SetCopyNumber(int c)
		{
			string tmp=menuitem_pastecards.Text;
			int t=tmp.LastIndexOf(" (");
			if(t>0)
				tmp=tmp.Substring(0,t);
			tmp=tmp+" ("+c.ToString()+")";
			menuitem_pastecards.Text=tmp;
		}

		void Menuitem_pastecardsClick(object sender, EventArgs e)
		{
			if(tCards==null)
				return;
			DataEditForm df =GetActive();
			if(df==null)
				return;
			df.SaveCards(tCards);
			MyMsg.Show(LMSG.PasteCards);
		}
		
		#endregion
		
		#region compare
		void Menuitem_comp1Click(object sender, EventArgs e)
		{
			compare1 = GetActive();
			if(compare1 != null && !string.IsNullOrEmpty(compare1.getNowCDB()))
			{
				menuitem_comp2.Enabled=true;
				CompareDB();
			}
		}
		void CompareDB()
		{
			if(compare1 == null || compare2 == null)
				return;
			string cdb1=compare1.getNowCDB();
			string cdb2=compare2.getNowCDB();
			if(string.IsNullOrEmpty(cdb1)
			   || string.IsNullOrEmpty(cdb2)
			   ||cdb1==cdb2)
				return;
			
			bool checktext=MyMsg.Question(LMSG.CheckText);
			compare1.CompareCards(cdb2, checktext);
			compare2.CompareCards(cdb1, checktext);
			MyMsg.Show(LMSG.CompareOK);
			menuitem_comp2.Enabled=false;
			compare1=null;
			compare2=null;
		}
		void Menuitem_comp2Click(object sender, EventArgs e)
		{
			compare2 = GetActive();
			if(compare2 != null && !string.IsNullOrEmpty(compare2.getNowCDB()))
			{
				CompareDB();
			}
		}
		#endregion

		#region complate
		void InitCodeEditor(string funtxt,string conlua)
		{
			if(!isInitDataEditor)
				datacfg.Init();
			tooltipDic.Clear();
			funList.Clear();
			conList.Clear();
			AddFunction(funtxt);
			AddConstant(conlua);
			foreach(long k in datacfg.dicSetnames.Keys)
			{
				string key="0x"+k.ToString("x");
				if(!tooltipDic.ContainsKey(key))
				{
					AddConToolTip(key, datacfg.dicSetnames[k]);
				}
			}
			//MessageBox.Show(funList.Count.ToString());
		}
		#endregion
		
		#region function
		void AddFunction(string funtxt)
		{
			if(File.Exists(funtxt))
			{
				string[] lines=File.ReadAllLines(funtxt);
				bool isFind=false;
				string name="";
				string desc="";
				foreach(string line in lines)
				{
					if(string.IsNullOrEmpty(line)
					   || line.StartsWith("==")
					   || line.StartsWith("#"))
						continue;
					if(line.StartsWith("●"))
					{
						//add
						AddFuncTooltip(name, desc);
						int w=line.IndexOf("(");
						int t=line.IndexOf(" ");
						
						if(t<w && t>0){
							name=line.Substring(t+1,w-t-1);
							isFind=true;
							desc=line;
						}
					}
					else if(isFind){
						desc+=Environment.NewLine+line;
					}
				}
				AddFuncTooltip(name, desc);
			}
		}
		string GetFunName(string str)
		{
			int t=str.IndexOf(".");
			//if(str.StartsWith("Debug.")
			//   || str.StartsWith("Duel.")
			//   || str.StartsWith("bit.")
			//   || str.StartsWith("aux.")
			//  )
			//	return str;
			if(t>0)
				return str.Substring(t+1);
			return str;
		}
		bool isANSIChar(char c)
		{
			if((int)c>127)
				return false;
			return true;
		}
		int CheckReturn(char[] chars, int index, int MAX)
		{
			int k=0;
			for(k=0;k<MAX;k++)
			{
				if((index+k)<chars.Length){
					if(chars[index+k]=='\n')
						return k+1;
				}
			}
			return -1;
		}
		void AddFuncTooltip(string name,string desc)
		{
			if(!string.IsNullOrEmpty(name))
			{
				string fname=GetFunName(name);
				if(!tooltipDic.ContainsKey(fname)){
					tooltipDic.Add(fname, desc );
					AutocompleteItem aitem=new AutocompleteItem(name);
					aitem.ToolTipTitle = name;
					aitem.ToolTipText = desc;
					funList.Add(aitem);
				}
				else
					tooltipDic[fname] += Environment.NewLine + desc;
			}
		}
		#endregion
		
		#region constant
		void AddConToolTip(string key, string desc)
		{
			AutocompleteItem aitem=new AutocompleteItem(key);
			aitem.ToolTipTitle = key;
			aitem.ToolTipText = desc;
			conList.Add(aitem);
			tooltipDic.Add(key, desc);
		}
		
		
		
		void AddConstant(string conlua)
		{
			//conList.Add("con");
			if(File.Exists(conlua))
			{
				string[] lines=File.ReadAllLines(conlua);
				foreach(string line in lines)
				{
					if(line.StartsWith("--"))
						continue;
					string k=line,desc=line;
					int t=line.IndexOf("=");
					int t2=line.IndexOf("--");
					k = (t>0)?line.Substring(0,t).TrimEnd(new char[]{' ','\t'})
						:line;
					desc = (t>0)?line.Substring(t+1).Replace("--","\n")
						:line;
					if(!tooltipDic.ContainsKey(k)){
						AddConToolTip(k, desc);
					}
					else
						tooltipDic[k] += Environment.NewLine + desc;
				}
			}
		}
		#endregion
		
		void Menuitem_findluafuncClick(object sender, EventArgs e)
		{
			using(FolderBrowserDialog fd=new FolderBrowserDialog())
			{
				fd.Description="Folder Name: ocgcore";
				if(fd.ShowDialog()==DialogResult.OK)
				{
					LuaFunction.Read(funtxt);
					LuaFunction.Find(fd.SelectedPath);
					MessageBox.Show("OK");
				}
			}
		}
	}
}
