/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-22
 * 时间: 19:16
 * 
 */
namespace DataEditorX
{
	partial class CodeEditForm
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuitem_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_open = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_saveas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_quit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_setting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_showmap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_showinput = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_find = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_replace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_setcard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.fctb = new FastColoredTextBoxNS.FastColoredTextBoxEx();
            this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_file,
            this.menuitem_setting,
            this.menuitem_help});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(695, 25);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // menuitem_file
            // 
            this.menuitem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_open,
            this.menuitem_save,
            this.menuitem_saveas,
            this.toolStripSeparator1,
            this.menuitem_quit});
            this.menuitem_file.Name = "menuitem_file";
            this.menuitem_file.Size = new System.Drawing.Size(53, 21);
            this.menuitem_file.Text = "File(&F)";
            // 
            // menuitem_open
            // 
            this.menuitem_open.Name = "menuitem_open";
            this.menuitem_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuitem_open.Size = new System.Drawing.Size(155, 22);
            this.menuitem_open.Text = "Open";
            this.menuitem_open.Click += new System.EventHandler(this.Menuitem_openClick);
            // 
            // menuitem_save
            // 
            this.menuitem_save.Name = "menuitem_save";
            this.menuitem_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuitem_save.Size = new System.Drawing.Size(155, 22);
            this.menuitem_save.Text = "Save";
            this.menuitem_save.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // menuitem_saveas
            // 
            this.menuitem_saveas.Name = "menuitem_saveas";
            this.menuitem_saveas.Size = new System.Drawing.Size(155, 22);
            this.menuitem_saveas.Text = "Save As";
            this.menuitem_saveas.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // menuitem_quit
            // 
            this.menuitem_quit.Name = "menuitem_quit";
            this.menuitem_quit.Size = new System.Drawing.Size(155, 22);
            this.menuitem_quit.Text = "Quit";
            this.menuitem_quit.Click += new System.EventHandler(this.QuitToolStripMenuItemClick);
            // 
            // menuitem_setting
            // 
            this.menuitem_setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_showmap,
            this.menuitem_showinput,
            this.menuitem_find,
            this.menuitem_replace,
            this.menuitem_setcard});
            this.menuitem_setting.Name = "menuitem_setting";
            this.menuitem_setting.Size = new System.Drawing.Size(67, 21);
            this.menuitem_setting.Text = "Tools(&S)";
            // 
            // menuitem_showmap
            // 
            this.menuitem_showmap.Name = "menuitem_showmap";
            this.menuitem_showmap.Size = new System.Drawing.Size(168, 22);
            this.menuitem_showmap.Text = "Show Map";
            this.menuitem_showmap.Click += new System.EventHandler(this.ShowMapToolStripMenuItemClick);
            // 
            // menuitem_showinput
            // 
            this.menuitem_showinput.Checked = true;
            this.menuitem_showinput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuitem_showinput.Name = "menuitem_showinput";
            this.menuitem_showinput.Size = new System.Drawing.Size(168, 22);
            this.menuitem_showinput.Text = "Show InputBox";
            this.menuitem_showinput.Click += new System.EventHandler(this.Menuitem_showinputClick);
            // 
            // menuitem_find
            // 
            this.menuitem_find.Name = "menuitem_find";
            this.menuitem_find.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuitem_find.Size = new System.Drawing.Size(168, 22);
            this.menuitem_find.Text = "Find";
            this.menuitem_find.Click += new System.EventHandler(this.Menuitem_findClick);
            // 
            // menuitem_replace
            // 
            this.menuitem_replace.Name = "menuitem_replace";
            this.menuitem_replace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuitem_replace.Size = new System.Drawing.Size(168, 22);
            this.menuitem_replace.Text = "Replace";
            this.menuitem_replace.Click += new System.EventHandler(this.Menuitem_replaceClick);
            // 
            // menuitem_setcard
            // 
            this.menuitem_setcard.Name = "menuitem_setcard";
            this.menuitem_setcard.Size = new System.Drawing.Size(168, 22);
            this.menuitem_setcard.Text = "Set Cards";
            // 
            // menuitem_help
            // 
            this.menuitem_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_about});
            this.menuitem_help.Name = "menuitem_help";
            this.menuitem_help.Size = new System.Drawing.Size(64, 21);
            this.menuitem_help.Text = "Help(&H)";
            // 
            // menuitem_about
            // 
            this.menuitem_about.Name = "menuitem_about";
            this.menuitem_about.Size = new System.Drawing.Size(111, 22);
            this.menuitem_about.Text = "About";
            this.menuitem_about.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // tb_input
            // 
            this.tb_input.BackColor = System.Drawing.SystemColors.Control;
            this.tb_input.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tb_input.Location = new System.Drawing.Point(0, 394);
            this.tb_input.Margin = new System.Windows.Forms.Padding(0);
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(504, 21);
            this.tb_input.TabIndex = 1;
            this.tb_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_inputKeyDown);
            // 
            // fctb
            // 
            this.fctb.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctb.AutoIndent = false;
            this.fctb.AutoIndentChars = false;
            this.fctb.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>.+)";
            this.fctb.AutoIndentExistingLines = false;
            this.fctb.AutoScrollMinSize = new System.Drawing.Size(0, 22);
            this.fctb.BackBrush = null;
            this.fctb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fctb.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fctb.CharHeight = 22;
            this.fctb.CharWidth = 10;
            this.fctb.CommentPrefix = "--";
            this.fctb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctb.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctb.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.fctb.ForeColor = System.Drawing.Color.GhostWhite;
            this.fctb.IndentBackColor = System.Drawing.SystemColors.WindowFrame;
            this.fctb.IsReplaceMode = false;
            this.fctb.Language = FastColoredTextBoxNS.Language.Lua;
            this.fctb.LeftBracket = '(';
            this.fctb.LeftBracket2 = '{';
            this.fctb.LineNumberColor = System.Drawing.Color.Gainsboro;
            this.fctb.Location = new System.Drawing.Point(0, 25);
            this.fctb.Margin = new System.Windows.Forms.Padding(0);
            this.fctb.Name = "fctb";
            this.fctb.Paddings = new System.Windows.Forms.Padding(0);
            this.fctb.RightBracket = ')';
            this.fctb.RightBracket2 = '}';
            this.fctb.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctb.Size = new System.Drawing.Size(504, 369);
            this.fctb.TabIndex = 0;
            this.fctb.WordWrap = true;
            this.fctb.Zoom = 100;
            this.fctb.ToolTipNeeded += new System.EventHandler<FastColoredTextBoxNS.ToolTipNeededEventArgs>(this.FctbToolTipNeeded);
            this.fctb.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.FctbTextChangedDelayed);
            this.fctb.SelectionChangedDelayed += new System.EventHandler(this.FctbSelectionChangedDelayed);
            this.fctb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FctbKeyDown);
            this.fctb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FctbMouseClick);
            // 
            // documentMap1
            // 
            this.documentMap1.BackColor = System.Drawing.Color.DimGray;
            this.documentMap1.Dock = System.Windows.Forms.DockStyle.Right;
            this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap1.Location = new System.Drawing.Point(504, 25);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.Size = new System.Drawing.Size(191, 390);
            this.documentMap1.TabIndex = 5;
            this.documentMap1.Target = this.fctb;
            this.documentMap1.Text = "documentMap1";
            this.documentMap1.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // CodeEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(695, 415);
            this.Controls.Add(this.fctb);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.documentMap1);
            this.Controls.Add(this.mainMenu);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "CodeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CodeEditFormFormClosing);
            this.Load += new System.EventHandler(this.CodeEditFormLoad);
            this.Enter += new System.EventHandler(this.CodeEditFormEnter);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripMenuItem menuitem_setcard;
		private System.Windows.Forms.ToolStripMenuItem menuitem_replace;
		private System.Windows.Forms.ToolStripMenuItem menuitem_find;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menuitem_showinput;
		private System.Windows.Forms.TextBox tb_input;
		private FastColoredTextBoxNS.DocumentMap documentMap1;
		private FastColoredTextBoxNS.FastColoredTextBoxEx fctb;
		private System.Windows.Forms.ToolStripMenuItem menuitem_showmap;
		private System.Windows.Forms.ToolStripMenuItem menuitem_about;
		private System.Windows.Forms.ToolStripMenuItem menuitem_help;
		private System.Windows.Forms.ToolStripMenuItem menuitem_setting;
		private System.Windows.Forms.ToolStripMenuItem menuitem_quit;
		private System.Windows.Forms.ToolStripMenuItem menuitem_saveas;
		private System.Windows.Forms.ToolStripMenuItem menuitem_save;
		private System.Windows.Forms.ToolStripMenuItem menuitem_open;
		private System.Windows.Forms.ToolStripMenuItem menuitem_file;
		private System.Windows.Forms.MenuStrip mainMenu;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
		

	}
}
