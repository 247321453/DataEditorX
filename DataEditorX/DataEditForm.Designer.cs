/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 20:22
 * 
 */
namespace DataEditorX
{
    partial class DataEditForm
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
        	this.components = new System.ComponentModel.Container();
        	this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        	this.menuitem_file = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_open = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_new = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_copyselectto = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_saveasmse_select = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_copyto = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_saveasmse = new System.Windows.Forms.ToolStripMenuItem();
        	this.tsep4 = new System.Windows.Forms.ToolStripSeparator();
        	this.menuitem_cutimages = new System.Windows.Forms.ToolStripMenuItem();
        	this.tsep1 = new System.Windows.Forms.ToolStripSeparator();
        	this.menuitem_readydk = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_readimages = new System.Windows.Forms.ToolStripMenuItem();
        	this.tsep3 = new System.Windows.Forms.ToolStripSeparator();
        	this.menuitem_openLastDataBase = new System.Windows.Forms.ToolStripMenuItem();
        	this.tsep2 = new System.Windows.Forms.ToolStripSeparator();
        	this.menuitem_quit = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_help = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_about = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_checkupdate = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuitem_github = new System.Windows.Forms.ToolStripMenuItem();
        	this.lv_cardlist = new System.Windows.Forms.DListView();
        	this.ch_cardcode = new System.Windows.Forms.ColumnHeader();
        	this.ch_cardname = new System.Windows.Forms.ColumnHeader();
        	this.cb_cardattribute = new System.Windows.Forms.ComboBox();
        	this.tb_cardname = new System.Windows.Forms.TextBox();
        	this.cb_cardrule = new System.Windows.Forms.ComboBox();
        	this.cb_cardlevel = new System.Windows.Forms.ComboBox();
        	this.cb_cardrace = new System.Windows.Forms.ComboBox();
        	this.cb_setname2 = new System.Windows.Forms.ComboBox();
        	this.cb_setname1 = new System.Windows.Forms.ComboBox();
        	this.cb_setname4 = new System.Windows.Forms.ComboBox();
        	this.cb_setname3 = new System.Windows.Forms.ComboBox();
        	this.tb_cardtext = new System.Windows.Forms.TextBox();
        	this.lb_scripttext = new System.Windows.Forms.DListBox();
        	this.tb_edittext = new System.Windows.Forms.TextBox();
        	this.lb_pleft_right = new System.Windows.Forms.Label();
        	this.tb_pleft = new System.Windows.Forms.TextBox();
        	this.tb_pright = new System.Windows.Forms.TextBox();
        	this.lb_atkdef = new System.Windows.Forms.Label();
        	this.lb4 = new System.Windows.Forms.Label();
        	this.tb_page = new System.Windows.Forms.TextBox();
        	this.tb_pagenum = new System.Windows.Forms.TextBox();
        	this.btn_PageUp = new System.Windows.Forms.Button();
        	this.btn_PageDown = new System.Windows.Forms.Button();
        	this.btn_add = new System.Windows.Forms.Button();
        	this.lb5 = new System.Windows.Forms.Label();
        	this.tb_atk = new System.Windows.Forms.TextBox();
        	this.tb_def = new System.Windows.Forms.TextBox();
        	this.lb_cardcode = new System.Windows.Forms.Label();
        	this.tb_cardcode = new System.Windows.Forms.TextBox();
        	this.lb_cardalias = new System.Windows.Forms.Label();
        	this.tb_cardalias = new System.Windows.Forms.TextBox();
        	this.btn_mod = new System.Windows.Forms.Button();
        	this.btn_del = new System.Windows.Forms.Button();
        	this.btn_lua = new System.Windows.Forms.Button();
        	this.btn_reset = new System.Windows.Forms.Button();
        	this.btn_serach = new System.Windows.Forms.Button();
        	this.lb_categorys = new System.Windows.Forms.Label();
        	this.lb2 = new System.Windows.Forms.Label();
        	this.pl_cardtype = new System.Windows.Forms.DFlowLayoutPanel();
        	this.pl_category = new System.Windows.Forms.DFlowLayoutPanel();
        	this.pl_image = new System.Windows.Forms.Panel();
        	this.lb_types = new System.Windows.Forms.Label();
        	this.lb_tiptexts = new System.Windows.Forms.Label();
        	this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
        	this.btn_undo = new System.Windows.Forms.Button();
        	this.tb_setcode = new System.Windows.Forms.TextBox();
        	this.lb_setcode = new System.Windows.Forms.Label();
        	this.btn_img = new System.Windows.Forms.Button();
        	this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        	this.menuitem_convertimage = new System.Windows.Forms.ToolStripMenuItem();
        	this.menuStrip1.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// menuStrip1
        	// 
        	this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.menuitem_file,
        	        	        	this.menuitem_help});
        	this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        	this.menuStrip1.Name = "menuStrip1";
        	this.menuStrip1.Size = new System.Drawing.Size(860, 25);
        	this.menuStrip1.TabIndex = 0;
        	this.menuStrip1.Text = "menuStrip1";
        	// 
        	// menuitem_file
        	// 
        	this.menuitem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.menuitem_open,
        	        	        	this.menuitem_new,
        	        	        	this.menuitem_copyselectto,
        	        	        	this.menuitem_saveasmse_select,
        	        	        	this.menuitem_copyto,
        	        	        	this.menuitem_saveasmse,
        	        	        	this.tsep4,
        	        	        	this.menuitem_cutimages,
        	        	        	this.menuitem_convertimage,
        	        	        	this.tsep1,
        	        	        	this.menuitem_readydk,
        	        	        	this.menuitem_readimages,
        	        	        	this.tsep3,
        	        	        	this.menuitem_openLastDataBase,
        	        	        	this.tsep2,
        	        	        	this.menuitem_quit});
        	this.menuitem_file.Name = "menuitem_file";
        	this.menuitem_file.Size = new System.Drawing.Size(53, 21);
        	this.menuitem_file.Text = "File(&F)";
        	// 
        	// menuitem_open
        	// 
        	this.menuitem_open.Name = "menuitem_open";
        	this.menuitem_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
        	this.menuitem_open.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_open.Text = "Open Database(&O)";
        	this.menuitem_open.Click += new System.EventHandler(this.Menuitem_openClick);
        	// 
        	// menuitem_new
        	// 
        	this.menuitem_new.Name = "menuitem_new";
        	this.menuitem_new.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
        	this.menuitem_new.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_new.Text = "New Database(&N)";
        	this.menuitem_new.Click += new System.EventHandler(this.Menuitem_newClick);
        	// 
        	// menuitem_copyselectto
        	// 
        	this.menuitem_copyselectto.Name = "menuitem_copyselectto";
        	this.menuitem_copyselectto.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_copyselectto.Text = "Select Copy To...";
        	this.menuitem_copyselectto.Click += new System.EventHandler(this.Menuitem_copyselecttoClick);
        	// 
        	// menuitem_saveasmse_select
        	// 
        	this.menuitem_saveasmse_select.Enabled = false;
        	this.menuitem_saveasmse_select.Name = "menuitem_saveasmse_select";
        	this.menuitem_saveasmse_select.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_saveasmse_select.Text = "Select Save As MSE";
        	this.menuitem_saveasmse_select.Click += new System.EventHandler(this.Menuitem_saveasmse_selectClick);
        	// 
        	// menuitem_copyto
        	// 
        	this.menuitem_copyto.Name = "menuitem_copyto";
        	this.menuitem_copyto.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_copyto.Text = "All Now Copy To...";
        	this.menuitem_copyto.Click += new System.EventHandler(this.Menuitem_copytoClick);
        	// 
        	// menuitem_saveasmse
        	// 
        	this.menuitem_saveasmse.Enabled = false;
        	this.menuitem_saveasmse.Name = "menuitem_saveasmse";
        	this.menuitem_saveasmse.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_saveasmse.Text = "All Now Save As MSE";
        	this.menuitem_saveasmse.Click += new System.EventHandler(this.Menuitem_saveasmseClick);
        	// 
        	// tsep4
        	// 
        	this.tsep4.Name = "tsep4";
        	this.tsep4.Size = new System.Drawing.Size(229, 6);
        	// 
        	// menuitem_cutimages
        	// 
        	this.menuitem_cutimages.Name = "menuitem_cutimages";
        	this.menuitem_cutimages.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_cutimages.Text = "Cut Images";
        	this.menuitem_cutimages.Click += new System.EventHandler(this.Menuitem_cutimagesClick);
        	// 
        	// tsep1
        	// 
        	this.tsep1.Name = "tsep1";
        	this.tsep1.Size = new System.Drawing.Size(229, 6);
        	// 
        	// menuitem_readydk
        	// 
        	this.menuitem_readydk.Name = "menuitem_readydk";
        	this.menuitem_readydk.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_readydk.Text = "Cards Form ydk file(&Y)";
        	this.menuitem_readydk.Click += new System.EventHandler(this.Menuitem_readydkClick);
        	// 
        	// menuitem_readimages
        	// 
        	this.menuitem_readimages.Name = "menuitem_readimages";
        	this.menuitem_readimages.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_readimages.Text = "Cards From Images(&I)";
        	this.menuitem_readimages.Click += new System.EventHandler(this.Menuitem_readimagesClick);
        	// 
        	// tsep3
        	// 
        	this.tsep3.Name = "tsep3";
        	this.tsep3.Size = new System.Drawing.Size(229, 6);
        	// 
        	// menuitem_openLastDataBase
        	// 
        	this.menuitem_openLastDataBase.Name = "menuitem_openLastDataBase";
        	this.menuitem_openLastDataBase.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_openLastDataBase.Text = "Open Last DataBase";
        	this.menuitem_openLastDataBase.Click += new System.EventHandler(this.Menuitem_openLastDataBaseClick);
        	// 
        	// tsep2
        	// 
        	this.tsep2.Name = "tsep2";
        	this.tsep2.Size = new System.Drawing.Size(229, 6);
        	// 
        	// menuitem_quit
        	// 
        	this.menuitem_quit.Name = "menuitem_quit";
        	this.menuitem_quit.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_quit.Text = "Quit(&Q)";
        	this.menuitem_quit.Click += new System.EventHandler(this.Menuitem_quitClick);
        	// 
        	// menuitem_help
        	// 
        	this.menuitem_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.menuitem_about,
        	        	        	this.menuitem_checkupdate,
        	        	        	this.menuitem_github});
        	this.menuitem_help.Name = "menuitem_help";
        	this.menuitem_help.Size = new System.Drawing.Size(64, 21);
        	this.menuitem_help.Text = "Help(&H)";
        	// 
        	// menuitem_about
        	// 
        	this.menuitem_about.Name = "menuitem_about";
        	this.menuitem_about.ShortcutKeys = System.Windows.Forms.Keys.F1;
        	this.menuitem_about.Size = new System.Drawing.Size(158, 22);
        	this.menuitem_about.Text = "About";
        	this.menuitem_about.Click += new System.EventHandler(this.Menuitem_aboutClick);
        	// 
        	// menuitem_checkupdate
        	// 
        	this.menuitem_checkupdate.Name = "menuitem_checkupdate";
        	this.menuitem_checkupdate.Size = new System.Drawing.Size(158, 22);
        	this.menuitem_checkupdate.Text = "Check Update";
        	this.menuitem_checkupdate.Click += new System.EventHandler(this.Menuitem_checkupdateClick);
        	// 
        	// menuitem_github
        	// 
        	this.menuitem_github.Name = "menuitem_github";
        	this.menuitem_github.Size = new System.Drawing.Size(158, 22);
        	this.menuitem_github.Text = "Source Code";
        	this.menuitem_github.Click += new System.EventHandler(this.Menuitem_githubClick);
        	// 
        	// lv_cardlist
        	// 
        	this.lv_cardlist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left)));
        	this.lv_cardlist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        	this.lv_cardlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
        	        	        	this.ch_cardcode,
        	        	        	this.ch_cardname});
        	this.lv_cardlist.FullRowSelect = true;
        	this.lv_cardlist.GridLines = true;
        	this.lv_cardlist.HideSelection = false;
        	this.lv_cardlist.LabelWrap = false;
        	this.lv_cardlist.Location = new System.Drawing.Point(0, 27);
        	this.lv_cardlist.Name = "lv_cardlist";
        	this.lv_cardlist.Scrollable = false;
        	this.lv_cardlist.ShowItemToolTips = true;
        	this.lv_cardlist.Size = new System.Drawing.Size(216, 510);
        	this.lv_cardlist.TabIndex = 1;
        	this.lv_cardlist.UseCompatibleStateImageBehavior = false;
        	this.lv_cardlist.View = System.Windows.Forms.View.Details;
        	this.lv_cardlist.SelectedIndexChanged += new System.EventHandler(this.Lv_cardlistSelectedIndexChanged);
        	this.lv_cardlist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lv_cardlistKeyDown);
        	// 
        	// ch_cardcode
        	// 
        	this.ch_cardcode.Text = "Card Code";
        	this.ch_cardcode.Width = 70;
        	// 
        	// ch_cardname
        	// 
        	this.ch_cardname.Text = "Card Name";
        	this.ch_cardname.Width = 140;
        	// 
        	// cb_cardattribute
        	// 
        	this.cb_cardattribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_cardattribute.FormattingEnabled = true;
        	this.cb_cardattribute.Location = new System.Drawing.Point(402, 80);
        	this.cb_cardattribute.Name = "cb_cardattribute";
        	this.cb_cardattribute.Size = new System.Drawing.Size(140, 20);
        	this.cb_cardattribute.TabIndex = 2;
        	// 
        	// tb_cardname
        	// 
        	this.tb_cardname.Location = new System.Drawing.Point(217, 29);
        	this.tb_cardname.Name = "tb_cardname";
        	this.tb_cardname.Size = new System.Drawing.Size(325, 21);
        	this.tb_cardname.TabIndex = 4;
        	this.tb_cardname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	this.tb_cardname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_cardnameKeyDown);
        	// 
        	// cb_cardrule
        	// 
        	this.cb_cardrule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_cardrule.FormattingEnabled = true;
        	this.cb_cardrule.Location = new System.Drawing.Point(402, 54);
        	this.cb_cardrule.Name = "cb_cardrule";
        	this.cb_cardrule.Size = new System.Drawing.Size(140, 20);
        	this.cb_cardrule.TabIndex = 2;
        	// 
        	// cb_cardlevel
        	// 
        	this.cb_cardlevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_cardlevel.FormattingEnabled = true;
        	this.cb_cardlevel.Location = new System.Drawing.Point(402, 132);
        	this.cb_cardlevel.Name = "cb_cardlevel";
        	this.cb_cardlevel.Size = new System.Drawing.Size(140, 20);
        	this.cb_cardlevel.TabIndex = 2;
        	// 
        	// cb_cardrace
        	// 
        	this.cb_cardrace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_cardrace.FormattingEnabled = true;
        	this.cb_cardrace.Location = new System.Drawing.Point(402, 106);
        	this.cb_cardrace.Name = "cb_cardrace";
        	this.cb_cardrace.Size = new System.Drawing.Size(140, 20);
        	this.cb_cardrace.TabIndex = 2;
        	// 
        	// cb_setname2
        	// 
        	this.cb_setname2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_setname2.FormattingEnabled = true;
        	this.cb_setname2.Location = new System.Drawing.Point(402, 183);
        	this.cb_setname2.Name = "cb_setname2";
        	this.cb_setname2.Size = new System.Drawing.Size(140, 20);
        	this.cb_setname2.TabIndex = 2;
        	this.cb_setname2.SelectedIndexChanged += new System.EventHandler(this.Cb_setname2SelectedIndexChanged);
        	// 
        	// cb_setname1
        	// 
        	this.cb_setname1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_setname1.FormattingEnabled = true;
        	this.cb_setname1.Location = new System.Drawing.Point(402, 158);
        	this.cb_setname1.Name = "cb_setname1";
        	this.cb_setname1.Size = new System.Drawing.Size(140, 20);
        	this.cb_setname1.TabIndex = 2;
        	this.cb_setname1.SelectedIndexChanged += new System.EventHandler(this.Cb_setname1SelectedIndexChanged);
        	// 
        	// cb_setname4
        	// 
        	this.cb_setname4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_setname4.FormattingEnabled = true;
        	this.cb_setname4.Location = new System.Drawing.Point(402, 233);
        	this.cb_setname4.Name = "cb_setname4";
        	this.cb_setname4.Size = new System.Drawing.Size(140, 20);
        	this.cb_setname4.TabIndex = 2;
        	this.cb_setname4.SelectedIndexChanged += new System.EventHandler(this.Cb_setname4SelectedIndexChanged);
        	// 
        	// cb_setname3
        	// 
        	this.cb_setname3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cb_setname3.FormattingEnabled = true;
        	this.cb_setname3.Location = new System.Drawing.Point(402, 208);
        	this.cb_setname3.Name = "cb_setname3";
        	this.cb_setname3.Size = new System.Drawing.Size(140, 20);
        	this.cb_setname3.TabIndex = 2;
        	this.cb_setname3.SelectedIndexChanged += new System.EventHandler(this.Cb_setname3SelectedIndexChanged);
        	// 
        	// tb_cardtext
        	// 
        	this.tb_cardtext.AcceptsReturn = true;
        	this.tb_cardtext.AcceptsTab = true;
        	this.tb_cardtext.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        	this.tb_cardtext.Location = new System.Drawing.Point(220, 365);
        	this.tb_cardtext.Multiline = true;
        	this.tb_cardtext.Name = "tb_cardtext";
        	this.tb_cardtext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        	this.tb_cardtext.Size = new System.Drawing.Size(326, 172);
        	this.tb_cardtext.TabIndex = 4;
        	this.tb_cardtext.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_cardtextKeyDown);
        	// 
        	// lb_scripttext
        	// 
        	this.lb_scripttext.BorderStyle = System.Windows.Forms.BorderStyle.None;
        	this.lb_scripttext.FormattingEnabled = true;
        	this.lb_scripttext.IntegralHeight = false;
        	this.lb_scripttext.ItemHeight = 12;
        	this.lb_scripttext.Location = new System.Drawing.Point(548, 384);
        	this.lb_scripttext.Name = "lb_scripttext";
        	this.lb_scripttext.ScrollAlwaysVisible = true;
        	this.lb_scripttext.Size = new System.Drawing.Size(310, 126);
        	this.lb_scripttext.TabIndex = 6;
        	this.lb_scripttext.SelectedIndexChanged += new System.EventHandler(this.Lb_scripttextSelectedIndexChanged);
        	// 
        	// tb_edittext
        	// 
        	this.tb_edittext.HideSelection = false;
        	this.tb_edittext.Location = new System.Drawing.Point(548, 515);
        	this.tb_edittext.MaxLength = 2000;
        	this.tb_edittext.Name = "tb_edittext";
        	this.tb_edittext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        	this.tb_edittext.Size = new System.Drawing.Size(310, 21);
        	this.tb_edittext.TabIndex = 4;
        	this.tb_edittext.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_edittextKeyPress);
        	// 
        	// lb_pleft_right
        	// 
        	this.lb_pleft_right.AutoSize = true;
        	this.lb_pleft_right.Location = new System.Drawing.Point(222, 315);
        	this.lb_pleft_right.Name = "lb_pleft_right";
        	this.lb_pleft_right.Size = new System.Drawing.Size(41, 12);
        	this.lb_pleft_right.TabIndex = 7;
        	this.lb_pleft_right.Text = "PScale";
        	// 
        	// tb_pleft
        	// 
        	this.tb_pleft.Location = new System.Drawing.Point(275, 311);
        	this.tb_pleft.MaxLength = 12;
        	this.tb_pleft.Name = "tb_pleft";
        	this.tb_pleft.Size = new System.Drawing.Size(38, 21);
        	this.tb_pleft.TabIndex = 8;
        	this.tb_pleft.Text = "0";
        	this.tb_pleft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	// 
        	// tb_pright
        	// 
        	this.tb_pright.Location = new System.Drawing.Point(331, 311);
        	this.tb_pright.MaxLength = 12;
        	this.tb_pright.Name = "tb_pright";
        	this.tb_pright.Size = new System.Drawing.Size(38, 21);
        	this.tb_pright.TabIndex = 8;
        	this.tb_pright.Text = "0";
        	this.tb_pright.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	// 
        	// lb_atkdef
        	// 
        	this.lb_atkdef.AutoSize = true;
        	this.lb_atkdef.Location = new System.Drawing.Point(399, 315);
        	this.lb_atkdef.Name = "lb_atkdef";
        	this.lb_atkdef.Size = new System.Drawing.Size(47, 12);
        	this.lb_atkdef.TabIndex = 7;
        	this.lb_atkdef.Text = "ATK/DEF";
        	// 
        	// lb4
        	// 
        	this.lb4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.lb4.AutoSize = true;
        	this.lb4.Location = new System.Drawing.Point(102, 548);
        	this.lb4.Name = "lb4";
        	this.lb4.Size = new System.Drawing.Size(11, 12);
        	this.lb4.TabIndex = 7;
        	this.lb4.Text = "/";
        	// 
        	// tb_page
        	// 
        	this.tb_page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.tb_page.Location = new System.Drawing.Point(66, 544);
        	this.tb_page.MaxLength = 12;
        	this.tb_page.Name = "tb_page";
        	this.tb_page.Size = new System.Drawing.Size(34, 21);
        	this.tb_page.TabIndex = 8;
        	this.tb_page.Text = "1";
        	this.tb_page.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	this.tb_page.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_pageKeyPress);
        	// 
        	// tb_pagenum
        	// 
        	this.tb_pagenum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.tb_pagenum.Location = new System.Drawing.Point(117, 544);
        	this.tb_pagenum.MaxLength = 12;
        	this.tb_pagenum.Name = "tb_pagenum";
        	this.tb_pagenum.ReadOnly = true;
        	this.tb_pagenum.Size = new System.Drawing.Size(34, 21);
        	this.tb_pagenum.TabIndex = 8;
        	this.tb_pagenum.Text = "1";
        	this.tb_pagenum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	// 
        	// btn_PageUp
        	// 
        	this.btn_PageUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.btn_PageUp.Location = new System.Drawing.Point(0, 540);
        	this.btn_PageUp.Name = "btn_PageUp";
        	this.btn_PageUp.Size = new System.Drawing.Size(64, 27);
        	this.btn_PageUp.TabIndex = 5;
        	this.btn_PageUp.Text = "< <";
        	this.btn_PageUp.UseVisualStyleBackColor = true;
        	this.btn_PageUp.Click += new System.EventHandler(this.Btn_PageUpClick);
        	// 
        	// btn_PageDown
        	// 
        	this.btn_PageDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.btn_PageDown.Location = new System.Drawing.Point(153, 540);
        	this.btn_PageDown.Name = "btn_PageDown";
        	this.btn_PageDown.Size = new System.Drawing.Size(64, 27);
        	this.btn_PageDown.TabIndex = 5;
        	this.btn_PageDown.Text = "> >";
        	this.btn_PageDown.UseVisualStyleBackColor = true;
        	this.btn_PageDown.Click += new System.EventHandler(this.Btn_PageDownClick);
        	// 
        	// btn_add
        	// 
        	this.btn_add.Location = new System.Drawing.Point(548, 539);
        	this.btn_add.Name = "btn_add";
        	this.btn_add.Size = new System.Drawing.Size(75, 28);
        	this.btn_add.TabIndex = 5;
        	this.btn_add.Text = "&Add";
        	this.btn_add.UseVisualStyleBackColor = true;
        	this.btn_add.Click += new System.EventHandler(this.Btn_addClick);
        	// 
        	// lb5
        	// 
        	this.lb5.AutoSize = true;
        	this.lb5.Location = new System.Drawing.Point(491, 314);
        	this.lb5.Name = "lb5";
        	this.lb5.Size = new System.Drawing.Size(11, 12);
        	this.lb5.TabIndex = 7;
        	this.lb5.Text = "/";
        	// 
        	// tb_atk
        	// 
        	this.tb_atk.Location = new System.Drawing.Point(449, 310);
        	this.tb_atk.MaxLength = 12;
        	this.tb_atk.Name = "tb_atk";
        	this.tb_atk.Size = new System.Drawing.Size(38, 21);
        	this.tb_atk.TabIndex = 8;
        	this.tb_atk.Text = "0";
        	this.tb_atk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        	// 
        	// tb_def
        	// 
        	this.tb_def.Location = new System.Drawing.Point(504, 310);
        	this.tb_def.MaxLength = 12;
        	this.tb_def.Name = "tb_def";
        	this.tb_def.Size = new System.Drawing.Size(38, 21);
        	this.tb_def.TabIndex = 8;
        	this.tb_def.Text = "0";
        	this.tb_def.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        	// 
        	// lb_cardcode
        	// 
        	this.lb_cardcode.AutoSize = true;
        	this.lb_cardcode.Location = new System.Drawing.Point(399, 344);
        	this.lb_cardcode.Name = "lb_cardcode";
        	this.lb_cardcode.Size = new System.Drawing.Size(59, 12);
        	this.lb_cardcode.TabIndex = 7;
        	this.lb_cardcode.Text = "Card Code";
        	// 
        	// tb_cardcode
        	// 
        	this.tb_cardcode.Location = new System.Drawing.Point(474, 339);
        	this.tb_cardcode.MaxLength = 12;
        	this.tb_cardcode.Name = "tb_cardcode";
        	this.tb_cardcode.Size = new System.Drawing.Size(68, 21);
        	this.tb_cardcode.TabIndex = 8;
        	this.tb_cardcode.Text = "0";
        	this.tb_cardcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        	this.tb_cardcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_cardcodeKeyPress);
        	// 
        	// lb_cardalias
        	// 
        	this.lb_cardalias.AutoSize = true;
        	this.lb_cardalias.Location = new System.Drawing.Point(221, 345);
        	this.lb_cardalias.Name = "lb_cardalias";
        	this.lb_cardalias.Size = new System.Drawing.Size(65, 12);
        	this.lb_cardalias.TabIndex = 7;
        	this.lb_cardalias.Text = "Alias Card";
        	// 
        	// tb_cardalias
        	// 
        	this.tb_cardalias.Location = new System.Drawing.Point(301, 340);
        	this.tb_cardalias.MaxLength = 12;
        	this.tb_cardalias.Name = "tb_cardalias";
        	this.tb_cardalias.Size = new System.Drawing.Size(67, 21);
        	this.tb_cardalias.TabIndex = 8;
        	this.tb_cardalias.Text = "0";
        	this.tb_cardalias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        	// 
        	// btn_mod
        	// 
        	this.btn_mod.Location = new System.Drawing.Point(627, 539);
        	this.btn_mod.Name = "btn_mod";
        	this.btn_mod.Size = new System.Drawing.Size(75, 28);
        	this.btn_mod.TabIndex = 5;
        	this.btn_mod.Text = "&Modify";
        	this.btn_mod.UseVisualStyleBackColor = true;
        	this.btn_mod.Click += new System.EventHandler(this.Btn_modClick);
        	// 
        	// btn_del
        	// 
        	this.btn_del.ForeColor = System.Drawing.Color.DarkRed;
        	this.btn_del.Location = new System.Drawing.Point(783, 539);
        	this.btn_del.Name = "btn_del";
        	this.btn_del.Size = new System.Drawing.Size(75, 28);
        	this.btn_del.TabIndex = 5;
        	this.btn_del.Text = "&Delete";
        	this.btn_del.UseVisualStyleBackColor = true;
        	this.btn_del.Click += new System.EventHandler(this.Btn_delClick);
        	// 
        	// btn_lua
        	// 
        	this.btn_lua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
        	this.btn_lua.Location = new System.Drawing.Point(465, 539);
        	this.btn_lua.Name = "btn_lua";
        	this.btn_lua.Size = new System.Drawing.Size(80, 28);
        	this.btn_lua.TabIndex = 5;
        	this.btn_lua.Text = "&Lua Script";
        	this.btn_lua.UseVisualStyleBackColor = true;
        	this.btn_lua.Click += new System.EventHandler(this.Btn_luaClick);
        	// 
        	// btn_reset
        	// 
        	this.btn_reset.Location = new System.Drawing.Point(301, 539);
        	this.btn_reset.Name = "btn_reset";
        	this.btn_reset.Size = new System.Drawing.Size(80, 28);
        	this.btn_reset.TabIndex = 5;
        	this.btn_reset.Text = "&Reset";
        	this.btn_reset.UseVisualStyleBackColor = true;
        	this.btn_reset.Click += new System.EventHandler(this.Btn_resetClick);
        	// 
        	// btn_serach
        	// 
        	this.btn_serach.Location = new System.Drawing.Point(219, 539);
        	this.btn_serach.Name = "btn_serach";
        	this.btn_serach.Size = new System.Drawing.Size(80, 28);
        	this.btn_serach.TabIndex = 0;
        	this.btn_serach.Text = "&Search";
        	this.btn_serach.UseVisualStyleBackColor = true;
        	this.btn_serach.Click += new System.EventHandler(this.Btn_serachClick);
        	// 
        	// lb_categorys
        	// 
        	this.lb_categorys.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
        	this.lb_categorys.Location = new System.Drawing.Point(548, 171);
        	this.lb_categorys.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
        	this.lb_categorys.Name = "lb_categorys";
        	this.lb_categorys.Size = new System.Drawing.Size(310, 16);
        	this.lb_categorys.TabIndex = 11;
        	this.lb_categorys.Text = "Card Categorys";
        	this.lb_categorys.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// lb2
        	// 
        	this.lb2.AutoSize = true;
        	this.lb2.Location = new System.Drawing.Point(317, 315);
        	this.lb2.Name = "lb2";
        	this.lb2.Size = new System.Drawing.Size(11, 12);
        	this.lb2.TabIndex = 7;
        	this.lb2.Text = "/";
        	// 
        	// pl_cardtype
        	// 
        	this.pl_cardtype.AutoScroll = true;
        	this.pl_cardtype.Location = new System.Drawing.Point(548, 46);
        	this.pl_cardtype.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
        	this.pl_cardtype.Name = "pl_cardtype";
        	this.pl_cardtype.Padding = new System.Windows.Forms.Padding(2);
        	this.pl_cardtype.Size = new System.Drawing.Size(310, 123);
        	this.pl_cardtype.TabIndex = 12;
        	// 
        	// pl_category
        	// 
        	this.pl_category.AutoScroll = true;
        	this.pl_category.Location = new System.Drawing.Point(548, 189);
        	this.pl_category.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
        	this.pl_category.Name = "pl_category";
        	this.pl_category.Padding = new System.Windows.Forms.Padding(2);
        	this.pl_category.Size = new System.Drawing.Size(310, 174);
        	this.pl_category.TabIndex = 13;
        	// 
        	// pl_image
        	// 
        	this.pl_image.BackColor = System.Drawing.SystemColors.ButtonHighlight;
        	this.pl_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.pl_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        	this.pl_image.Location = new System.Drawing.Point(218, 52);
        	this.pl_image.Name = "pl_image";
        	this.pl_image.Size = new System.Drawing.Size(179, 256);
        	this.pl_image.TabIndex = 14;
        	// 
        	// lb_types
        	// 
        	this.lb_types.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
        	this.lb_types.Location = new System.Drawing.Point(548, 28);
        	this.lb_types.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
        	this.lb_types.Name = "lb_types";
        	this.lb_types.Size = new System.Drawing.Size(310, 16);
        	this.lb_types.TabIndex = 11;
        	this.lb_types.Text = "Card Types";
        	this.lb_types.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// lb_tiptexts
        	// 
        	this.lb_tiptexts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
        	this.lb_tiptexts.Location = new System.Drawing.Point(548, 365);
        	this.lb_tiptexts.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
        	this.lb_tiptexts.Name = "lb_tiptexts";
        	this.lb_tiptexts.Size = new System.Drawing.Size(310, 16);
        	this.lb_tiptexts.TabIndex = 11;
        	this.lb_tiptexts.Text = "Tips Texts";
        	this.lb_tiptexts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// backgroundWorker1
        	// 
        	this.backgroundWorker1.WorkerReportsProgress = true;
        	this.backgroundWorker1.WorkerSupportsCancellation = true;
        	this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1DoWork);
        	this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1RunWorkerCompleted);
        	// 
        	// btn_undo
        	// 
        	this.btn_undo.Location = new System.Drawing.Point(705, 539);
        	this.btn_undo.Name = "btn_undo";
        	this.btn_undo.Size = new System.Drawing.Size(75, 28);
        	this.btn_undo.TabIndex = 5;
        	this.btn_undo.Text = "&Undo";
        	this.btn_undo.UseVisualStyleBackColor = true;
        	this.btn_undo.Click += new System.EventHandler(this.Btn_undoClick);
        	// 
        	// tb_setcode
        	// 
        	this.tb_setcode.Location = new System.Drawing.Point(403, 283);
        	this.tb_setcode.MaxLength = 19;
        	this.tb_setcode.Name = "tb_setcode";
        	this.tb_setcode.Size = new System.Drawing.Size(139, 21);
        	this.tb_setcode.TabIndex = 15;
        	this.tb_setcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        	this.tb_setcode.TextChanged += new System.EventHandler(this.Tb_setcodeTextChanged);
        	// 
        	// lb_setcode
        	// 
        	this.lb_setcode.AutoSize = true;
        	this.lb_setcode.Location = new System.Drawing.Point(405, 264);
        	this.lb_setcode.Name = "lb_setcode";
        	this.lb_setcode.Size = new System.Drawing.Size(113, 12);
        	this.lb_setcode.TabIndex = 16;
        	this.lb_setcode.Text = "SetCode    (Max 4)";
        	// 
        	// btn_img
        	// 
        	this.btn_img.Location = new System.Drawing.Point(383, 539);
        	this.btn_img.Name = "btn_img";
        	this.btn_img.Size = new System.Drawing.Size(80, 28);
        	this.btn_img.TabIndex = 17;
        	this.btn_img.Text = "&Image";
        	this.btn_img.UseVisualStyleBackColor = true;
        	this.btn_img.Click += new System.EventHandler(this.Btn_imgClick);
        	// 
        	// menuitem_convertimage
        	// 
        	this.menuitem_convertimage.Name = "menuitem_convertimage";
        	this.menuitem_convertimage.Size = new System.Drawing.Size(232, 22);
        	this.menuitem_convertimage.Text = "Convert Images";
        	this.menuitem_convertimage.Click += new System.EventHandler(this.Menuitem_convertimageClick);
        	// 
        	// DataEditForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(860, 568);
        	this.Controls.Add(this.btn_img);
        	this.Controls.Add(this.lb_setcode);
        	this.Controls.Add(this.tb_setcode);
        	this.Controls.Add(this.pl_image);
        	this.Controls.Add(this.pl_category);
        	this.Controls.Add(this.pl_cardtype);
        	this.Controls.Add(this.lb_types);
        	this.Controls.Add(this.lb_tiptexts);
        	this.Controls.Add(this.lb_categorys);
        	this.Controls.Add(this.tb_edittext);
        	this.Controls.Add(this.tb_pagenum);
        	this.Controls.Add(this.tb_def);
        	this.Controls.Add(this.tb_pright);
        	this.Controls.Add(this.tb_page);
        	this.Controls.Add(this.tb_cardalias);
        	this.Controls.Add(this.tb_cardcode);
        	this.Controls.Add(this.tb_atk);
        	this.Controls.Add(this.lb4);
        	this.Controls.Add(this.lb_cardalias);
        	this.Controls.Add(this.tb_pleft);
        	this.Controls.Add(this.lb_cardcode);
        	this.Controls.Add(this.lb5);
        	this.Controls.Add(this.lb_atkdef);
        	this.Controls.Add(this.lb2);
        	this.Controls.Add(this.lb_pleft_right);
        	this.Controls.Add(this.lb_scripttext);
        	this.Controls.Add(this.btn_reset);
        	this.Controls.Add(this.btn_lua);
        	this.Controls.Add(this.btn_serach);
        	this.Controls.Add(this.btn_del);
        	this.Controls.Add(this.btn_undo);
        	this.Controls.Add(this.btn_mod);
        	this.Controls.Add(this.btn_add);
        	this.Controls.Add(this.btn_PageDown);
        	this.Controls.Add(this.btn_PageUp);
        	this.Controls.Add(this.tb_cardtext);
        	this.Controls.Add(this.tb_cardname);
        	this.Controls.Add(this.cb_setname3);
        	this.Controls.Add(this.cb_setname1);
        	this.Controls.Add(this.cb_cardrace);
        	this.Controls.Add(this.cb_cardrule);
        	this.Controls.Add(this.cb_setname4);
        	this.Controls.Add(this.cb_setname2);
        	this.Controls.Add(this.cb_cardlevel);
        	this.Controls.Add(this.cb_cardattribute);
        	this.Controls.Add(this.lv_cardlist);
        	this.Controls.Add(this.menuStrip1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        	this.MainMenuStrip = this.menuStrip1;
        	this.MaximizeBox = false;
        	this.Name = "DataEditForm";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "DataEditorX";
        	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataEditFormFormClosing);
        	this.Load += new System.EventHandler(this.DataEditFormLoad);
        	this.menuStrip1.ResumeLayout(false);
        	this.menuStrip1.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.ToolStripMenuItem menuitem_convertimage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator tsep2;
        private System.Windows.Forms.ToolStripMenuItem menuitem_openLastDataBase;
        private System.Windows.Forms.ToolStripMenuItem menuitem_cutimages;
        private System.Windows.Forms.ToolStripMenuItem menuitem_saveasmse;
        private System.Windows.Forms.ToolStripMenuItem menuitem_saveasmse_select;
        private System.Windows.Forms.ToolStripSeparator tsep4;
        private System.Windows.Forms.Button btn_img;
        private System.Windows.Forms.Label lb_setcode;
        private System.Windows.Forms.TextBox tb_setcode;
        private System.Windows.Forms.Button btn_undo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pl_image;
        private System.Windows.Forms.ToolStripMenuItem menuitem_copyselectto;
        private System.Windows.Forms.ToolStripMenuItem menuitem_github;
        private System.Windows.Forms.ToolStripMenuItem menuitem_new;
        private System.Windows.Forms.Label lb_tiptexts;
        private System.Windows.Forms.Label lb_categorys;
        private System.Windows.Forms.Label lb_types;
        private System.Windows.Forms.DFlowLayoutPanel pl_category;
        private System.Windows.Forms.DFlowLayoutPanel pl_cardtype;
        private System.Windows.Forms.Button btn_serach;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_lua;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_mod;
        private System.Windows.Forms.TextBox tb_cardalias;
        private System.Windows.Forms.Label lb_cardalias;
        private System.Windows.Forms.TextBox tb_cardcode;
        private System.Windows.Forms.Label lb_cardcode;
        private System.Windows.Forms.TextBox tb_def;
        private System.Windows.Forms.TextBox tb_atk;
        private System.Windows.Forms.Label lb5;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_PageDown;
        private System.Windows.Forms.Button btn_PageUp;
        private System.Windows.Forms.TextBox tb_pagenum;
        private System.Windows.Forms.TextBox tb_page;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.Label lb_atkdef;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.TextBox tb_pright;
        private System.Windows.Forms.TextBox tb_pleft;
        private System.Windows.Forms.Label lb_pleft_right;
        private System.Windows.Forms.TextBox tb_edittext;
        private System.Windows.Forms.DListBox lb_scripttext;
        private System.Windows.Forms.TextBox tb_cardtext;
        private System.Windows.Forms.ComboBox cb_setname3;
        private System.Windows.Forms.ComboBox cb_setname4;
        private System.Windows.Forms.ComboBox cb_setname1;
        private System.Windows.Forms.ComboBox cb_setname2;
        private System.Windows.Forms.ComboBox cb_cardrace;
        private System.Windows.Forms.ComboBox cb_cardlevel;
        private System.Windows.Forms.ComboBox cb_cardrule;
        private System.Windows.Forms.TextBox tb_cardname;
        private System.Windows.Forms.ComboBox cb_cardattribute;
        private System.Windows.Forms.ColumnHeader ch_cardname;
        private System.Windows.Forms.ColumnHeader ch_cardcode;
        private System.Windows.Forms.DListView lv_cardlist;
        private System.Windows.Forms.ToolStripMenuItem menuitem_checkupdate;
        private System.Windows.Forms.ToolStripMenuItem menuitem_about;
        private System.Windows.Forms.ToolStripMenuItem menuitem_help;
        private System.Windows.Forms.ToolStripMenuItem menuitem_quit;
        private System.Windows.Forms.ToolStripSeparator tsep3;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readimages;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readydk;
        private System.Windows.Forms.ToolStripSeparator tsep1;
        private System.Windows.Forms.ToolStripMenuItem menuitem_copyto;
        private System.Windows.Forms.ToolStripMenuItem menuitem_open;
        private System.Windows.Forms.ToolStripMenuItem menuitem_file;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}
