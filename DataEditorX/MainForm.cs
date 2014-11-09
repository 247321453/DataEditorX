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
		string cdbHistoryFile;
        const int MAX_HIS = 0x20;
		List<string> hittorylist;
		string datapath;
		string conflang,conflang_de,conflang_ce,confmsg,conflang_pe;
		DataEditForm compare1,compare2;
		Card[] tCards;
		//
        DataConfig datacfg = null;
        CodeConfig codecfg = null;
		#endregion
		
		#region init
		public MainForm(string datapath, string file)
		{
			Init(datapath);
			if(MainForm.isScript(file))
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
			tCards = null;
			hittorylist = new List<string>();
            
			this.datapath = datapath;
            InitDataEditor();
            InitCodeEditor();

			cdbHistoryFile =MyPath.Combine(datapath, "history.txt");
			conflang = MyPath.Combine(datapath, "language-mainform.txt");
			conflang_de = MyPath.Combine(datapath, "language-dataeditor.txt");
			conflang_ce = MyPath.Combine(datapath, "language-codeeditor.txt");
			conflang_pe = MyPath.Combine(datapath, "language-puzzleditor.txt");
			confmsg = MyPath.Combine(datapath, "message.txt");

			InitializeComponent();
			LANG.InitForm(this, conflang);
			LANG.LoadMessage(confmsg);
			LANG.SetLanguage(this);
			ReadHistory();
			MenuHistory();
            bgWorker1.RunWorkerAsync();
		}
		#endregion
		
		#region const
		public const int WM_OPEN=0x0401;
		public const int WM_OPEN_SCRIPT=0x0402;
		public const string TMPFILE="open.tmp";
		public const int MAX_HISTORY=0x20;
		public static bool isScript(string file)
		{
			if(file!=null && file.EndsWith("lua",StringComparison.OrdinalIgnoreCase))
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
				if(File.Exists(line) && hittorylist.IndexOf(line)<0){
					hittorylist.Add(line);
				}
			}
		}
		void AddHistory(string file)
		{
			int index=hittorylist.IndexOf(file);
			if(index>=0){
				hittorylist.RemoveAt(index);
			}
			string[] tmps=hittorylist.ToArray();
			hittorylist.Clear();
			hittorylist.Add(file);
			hittorylist.AddRange(tmps);
			SaveHistory();
			MenuHistory();
		}
		void SaveHistory()
		{
			string texts="# history";
			foreach(string str in hittorylist)
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
            menuitem_shistory.DropDownItems.Clear();
			foreach(string str in hittorylist)
			{
				ToolStripMenuItem tsmi=new ToolStripMenuItem(str);
				tsmi.Click+=MenuHistoryItem_Click;
                if(MainForm.isScript(str))
                    menuitem_shistory.DropDownItems.Add(tsmi);
                else
				    menuitem_history.DropDownItems.Add(tsmi);
			}
			menuitem_history.DropDownItems.Add(new ToolStripSeparator());
			ToolStripMenuItem tsmiclear=new ToolStripMenuItem(LANG.GetMsg(LMSG.ClearHistory));
			tsmiclear.Click+=MenuHistoryClear_Click;
			menuitem_history.DropDownItems.Add(tsmiclear);
            menuitem_shistory.DropDownItems.Add(new ToolStripSeparator());
            ToolStripMenuItem tsmiclear2 = new ToolStripMenuItem(LANG.GetMsg(LMSG.ClearHistory));
            tsmiclear2.Click += MenuHistoryClear2_Click;
            menuitem_shistory.DropDownItems.Add(tsmiclear2);
		}
        void MenuHistoryClear2_Click(object sender, EventArgs e)
        {
            int i = hittorylist.Count - 1;
            while (i >= 0)
            {
                if (MainForm.isScript(hittorylist[i]))
                    hittorylist.RemoveAt(i);
                i--;
            }
            MenuHistory();
            SaveHistory();
        }
		void MenuHistoryClear_Click(object sender, EventArgs e)
		{
            int i=hittorylist.Count-1;
            while (i >= 0)
            {
                if (!MainForm.isScript(hittorylist[i]))
                    hittorylist.RemoveAt(i);
                i--;
            }
			MenuHistory();
			SaveHistory();
		}
		void MenuHistoryItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi=sender as ToolStripMenuItem;
			if(tsmi!=null){
                string file = tsmi.Text;
                if (MainForm.isScript(file))
                {
                    OpenScript(file);
                }
                else
                    Open(file);
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
						this.Activate();
						Open(File.ReadAllText(file));
						File.Delete(file);
					}
					break;
				case MainForm.WM_OPEN_SCRIPT:
					file=Path.Combine(Application.StartupPath, MainForm.TMPFILE);
					if(File.Exists(file)){
						this.Activate();
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
            if (checkOpen(file))
                return;
            if (OpenInNull(file))
                return;
			CodeEditForm cf=new CodeEditForm();
			LANG.InitForm(cf, conflang_ce);
			LANG.SetLanguage(cf);
            InitCodeEditor();
			cf.SetCDBList(hittorylist.ToArray());
			cf.DockAreas = DockAreas.Document;
            cf.InitTooltip(codecfg.TooltipDic, 
			               codecfg.FunList, codecfg.ConList);
			//cf.SetIMEMode(ImeMode.Inherit);
			cf.Open(file);
			cf.Show(dockPanel1, DockState.Document);
		}
        void InitCodeEditor()
        {
            if (codecfg == null)
            {
                codecfg = new CodeConfig(datapath);
                codecfg.Init();
                InitDataEditor();
                codecfg.SetNames(datacfg.dicSetnames);
                codecfg.AddStrings();
            }
        }
        void InitDataEditor()
        {
            if (datacfg == null)
            {
                datacfg = new DataConfig(datapath);
                datacfg.Init();
                YGOUtil.SetConfig(datacfg);
            }
        }
		public void Open(string file)
		{
            if (MainForm.isScript(file))
            {
                OpenScript(file);
                return;
            }
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
            InitDataEditor();
			def.InitGameData(datacfg);
			def.Show(dockPanel1, DockState.Document);
		}
		
		bool checkOpen(string file)
		{
            DockContentCollection contents = dockPanel1.Contents;
            foreach (DockContent dc in contents)
			{
                if (!MainForm.isScript(file))
                {
                    DataEditForm df = dc as DataEditForm;
                    if (df != null && !df.IsDisposed)
                    {
                        if (df.getNowCDB() == file)
                        {
                            df.Show();
                            return true;
                        }
                    }
                }
                else
                {
                    CodeEditForm cf = dc as CodeEditForm;
                    if (cf != null && !cf.IsDisposed)
                    {
                        if (cf.NowFile == file)
                        {
                            cf.Show();
                            return true;
                        }
                    }
                }
			}
			return false;
		}
		bool OpenInNull(string file)
		{
			if(string.IsNullOrEmpty(file) || !File.Exists(file))
				return false;
            DockContentCollection contents = dockPanel1.Contents;
            foreach (DockContent dc in contents)
            {
                if (!MainForm.isScript(file))
                {
                    DataEditForm df = dc as DataEditForm;
                    if (df != null && !df.IsDisposed)
                    {
                        if (string.IsNullOrEmpty(df.getNowCDB()))
                        {
                            df.Open(file);
                            df.Show();
                            return true;
                        }
                    }
                }
                else
                {
                    CodeEditForm cf = dc as CodeEditForm;
                    if (cf != null && !cf.IsDisposed)
                    {
                        if (string.IsNullOrEmpty(cf.NowFile))
                        {
                            cf.Open(file);
                            cf.Show();
                            return true;
                        }

                    }
                }
            }
			return false;
		}
		
		void DataEditorToolStripMenuItemClick(object sender, EventArgs e)
		{
			Open(null);
		}

		#endregion
		
		#region form
		void MainFormLoad(object sender, System.EventArgs e)
		{
            //
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
        DataEditForm GetActive()
        {
            DataEditForm df = dockPanel1.ActiveContent as DataEditForm;
            return df;
        }
		void Menuitem_openClick(object sender, EventArgs e)
		{
			using(OpenFileDialog dlg=new OpenFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.OpenFile);
				if(GetActive() !=null)
					dlg.Filter=LANG.GetMsg(LMSG.CdbType);
				else
					dlg.Filter=LANG.GetMsg(LMSG.ScriptFilter);
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
				if(GetActive() !=null)
					dlg.Filter=LANG.GetMsg(LMSG.CdbType);
				else
					dlg.Filter=LANG.GetMsg(LMSG.ScriptFilter);
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
				if(cf.Save())
				    MyMsg.Show(LMSG.SaveFileOK);
			}
		}
		#endregion
		
		#region copy
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
        void Menuitem_findluafuncClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fd = new FolderBrowserDialog())
            {
                fd.Description = "Folder Name: ocgcore";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    LuaFunction.Read(codecfg.funtxt);
                    LuaFunction.Find(fd.SelectedPath);
                    MessageBox.Show("OK");
                }
            }
        }
		#endregion

        private void bgWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            TaskHelper.CheckVersion(false);
        }
	}
}
