/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-20
 * 时间: 9:19
 * 
 */
namespace DataEditorX
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuitem_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_open = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_new = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_findluafunc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_copyselect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_copyall = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_pastecards = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_comp1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_comp2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_history = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_shistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_quit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_windows = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_dataeditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_codeeditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_close = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_closeother = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_closeall = new System.Windows.Forms.ToolStripMenuItem();
            this.bgWorker1 = new System.ComponentModel.BackgroundWorker();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.Location = new System.Drawing.Point(0, 25);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(868, 572);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel1.Skin = dockPanelSkin1;
            this.dockPanel1.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_file,
            this.menuitem_windows});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.MdiWindowListItem = this.menuitem_windows;
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(868, 25);
            this.mainMenu.TabIndex = 3;
            this.mainMenu.Text = "mainMenu";
            // 
            // menuitem_file
            // 
            this.menuitem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_open,
            this.menuitem_new,
            this.menuitem_save,
            this.menuitem_findluafunc,
            this.toolStripSeparator3,
            this.menuitem_copyselect,
            this.menuitem_copyall,
            this.menuitem_pastecards,
            this.toolStripSeparator4,
            this.menuitem_comp1,
            this.menuitem_comp2,
            this.toolStripSeparator1,
            this.menuitem_history,
            this.menuitem_shistory,
            this.toolStripSeparator5,
            this.menuitem_quit});
            this.menuitem_file.Name = "menuitem_file";
            this.menuitem_file.Size = new System.Drawing.Size(53, 21);
            this.menuitem_file.Text = "File(&F)";
            // 
            // menuitem_open
            // 
            this.menuitem_open.Name = "menuitem_open";
            this.menuitem_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuitem_open.Size = new System.Drawing.Size(261, 22);
            this.menuitem_open.Text = "Open";
            this.menuitem_open.Click += new System.EventHandler(this.Menuitem_openClick);
            // 
            // menuitem_new
            // 
            this.menuitem_new.Name = "menuitem_new";
            this.menuitem_new.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuitem_new.Size = new System.Drawing.Size(261, 22);
            this.menuitem_new.Text = "New";
            this.menuitem_new.Click += new System.EventHandler(this.Menuitem_newClick);
            // 
            // menuitem_save
            // 
            this.menuitem_save.Name = "menuitem_save";
            this.menuitem_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuitem_save.Size = new System.Drawing.Size(261, 22);
            this.menuitem_save.Text = "Save";
            this.menuitem_save.Click += new System.EventHandler(this.Menuitem_saveClick);
            // 
            // menuitem_findluafunc
            // 
            this.menuitem_findluafunc.Name = "menuitem_findluafunc";
            this.menuitem_findluafunc.Size = new System.Drawing.Size(261, 22);
            this.menuitem_findluafunc.Text = "Find LuaFunctons";
            this.menuitem_findluafunc.Click += new System.EventHandler(this.Menuitem_findluafuncClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(258, 6);
            // 
            // menuitem_copyselect
            // 
            this.menuitem_copyselect.Name = "menuitem_copyselect";
            this.menuitem_copyselect.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.menuitem_copyselect.Size = new System.Drawing.Size(261, 22);
            this.menuitem_copyselect.Text = "Copy Select Cards";
            this.menuitem_copyselect.Click += new System.EventHandler(this.Menuitem_copyselecttoClick);
            // 
            // menuitem_copyall
            // 
            this.menuitem_copyall.Name = "menuitem_copyall";
            this.menuitem_copyall.Size = new System.Drawing.Size(261, 22);
            this.menuitem_copyall.Text = "Copy All Cards";
            this.menuitem_copyall.Click += new System.EventHandler(this.Menuitem_copyallClick);
            // 
            // menuitem_pastecards
            // 
            this.menuitem_pastecards.Name = "menuitem_pastecards";
            this.menuitem_pastecards.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.V)));
            this.menuitem_pastecards.Size = new System.Drawing.Size(261, 22);
            this.menuitem_pastecards.Text = "Paste Cards";
            this.menuitem_pastecards.Click += new System.EventHandler(this.Menuitem_pastecardsClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(258, 6);
            // 
            // menuitem_comp1
            // 
            this.menuitem_comp1.Name = "menuitem_comp1";
            this.menuitem_comp1.Size = new System.Drawing.Size(261, 22);
            this.menuitem_comp1.Text = "Compare DB 1";
            this.menuitem_comp1.Click += new System.EventHandler(this.Menuitem_comp1Click);
            // 
            // menuitem_comp2
            // 
            this.menuitem_comp2.Enabled = false;
            this.menuitem_comp2.Name = "menuitem_comp2";
            this.menuitem_comp2.Size = new System.Drawing.Size(261, 22);
            this.menuitem_comp2.Text = "Compare DB 2";
            this.menuitem_comp2.Click += new System.EventHandler(this.Menuitem_comp2Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // menuitem_history
            // 
            this.menuitem_history.Name = "menuitem_history";
            this.menuitem_history.Size = new System.Drawing.Size(261, 22);
            this.menuitem_history.Text = "History(&H)";
            // 
            // menuitem_shistory
            // 
            this.menuitem_shistory.Name = "menuitem_shistory";
            this.menuitem_shistory.Size = new System.Drawing.Size(261, 22);
            this.menuitem_shistory.Text = "Script History";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(258, 6);
            // 
            // menuitem_quit
            // 
            this.menuitem_quit.Name = "menuitem_quit";
            this.menuitem_quit.Size = new System.Drawing.Size(261, 22);
            this.menuitem_quit.Text = "Quit";
            this.menuitem_quit.Click += new System.EventHandler(this.QuitToolStripMenuItemClick);
            // 
            // menuitem_windows
            // 
            this.menuitem_windows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_dataeditor,
            this.menuitem_codeeditor,
            this.toolStripSeparator2,
            this.menuitem_close,
            this.menuitem_closeother,
            this.menuitem_closeall});
            this.menuitem_windows.Name = "menuitem_windows";
            this.menuitem_windows.Size = new System.Drawing.Size(93, 21);
            this.menuitem_windows.Text = "Windows(&W)";
            // 
            // menuitem_dataeditor
            // 
            this.menuitem_dataeditor.Name = "menuitem_dataeditor";
            this.menuitem_dataeditor.Size = new System.Drawing.Size(157, 22);
            this.menuitem_dataeditor.Text = "DataEditor";
            this.menuitem_dataeditor.Click += new System.EventHandler(this.DataEditorToolStripMenuItemClick);
            // 
            // menuitem_codeeditor
            // 
            this.menuitem_codeeditor.Name = "menuitem_codeeditor";
            this.menuitem_codeeditor.Size = new System.Drawing.Size(157, 22);
            this.menuitem_codeeditor.Text = "CodeEditor";
            this.menuitem_codeeditor.Click += new System.EventHandler(this.Menuitem_codeeditorClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // menuitem_close
            // 
            this.menuitem_close.Name = "menuitem_close";
            this.menuitem_close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.menuitem_close.Size = new System.Drawing.Size(157, 22);
            this.menuitem_close.Text = "Close";
            this.menuitem_close.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
            // 
            // menuitem_closeother
            // 
            this.menuitem_closeother.Name = "menuitem_closeother";
            this.menuitem_closeother.Size = new System.Drawing.Size(157, 22);
            this.menuitem_closeother.Text = "Close Other";
            this.menuitem_closeother.Click += new System.EventHandler(this.CloseOtherToolStripMenuItemClick);
            // 
            // menuitem_closeall
            // 
            this.menuitem_closeall.Name = "menuitem_closeall";
            this.menuitem_closeall.Size = new System.Drawing.Size(157, 22);
            this.menuitem_closeall.Text = "Close All";
            this.menuitem_closeall.Click += new System.EventHandler(this.CloseAllToolStripMenuItemClick);
            // 
            // bgWorker1
            // 
            this.bgWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker1_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(868, 597);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripMenuItem menuitem_findluafunc;
		private System.Windows.Forms.ToolStripMenuItem menuitem_save;
		private System.Windows.Forms.ToolStripMenuItem menuitem_codeeditor;
		private System.Windows.Forms.ToolStripMenuItem menuitem_copyall;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menuitem_comp2;
		private System.Windows.Forms.ToolStripMenuItem menuitem_comp1;
		private System.Windows.Forms.ToolStripMenuItem menuitem_pastecards;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem menuitem_copyselect;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem menuitem_history;
		private System.Windows.Forms.ToolStripMenuItem menuitem_new;
		private System.Windows.Forms.ToolStripMenuItem menuitem_quit;
		private System.Windows.Forms.ToolStripMenuItem menuitem_open;
		private System.Windows.Forms.ToolStripMenuItem menuitem_closeall;
		private System.Windows.Forms.ToolStripMenuItem menuitem_closeother;
		private System.Windows.Forms.ToolStripMenuItem menuitem_close;
		private System.Windows.Forms.ToolStripMenuItem menuitem_dataeditor;
		private System.Windows.Forms.ToolStripMenuItem menuitem_windows;
		private System.Windows.Forms.ToolStripMenuItem menuitem_file;
		private System.Windows.Forms.MenuStrip mainMenu;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripMenuItem menuitem_shistory;
        private System.ComponentModel.BackgroundWorker bgWorker1;
	
	}
}
