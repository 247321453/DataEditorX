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
            this.menuitem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_saveas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_copyto = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_inport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_export = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_readydk = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_readimages = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_quit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_undo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_redo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_mod = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_del = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_search = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_reset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_showimage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_openscript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_opengame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_checkupdate = new System.Windows.Forms.ToolStripMenuItem();
            this.lv_cardlist = new System.Windows.Forms.DListView();
            this.ch_cardcode = new System.Windows.Forms.ColumnHeader();
            this.ch_cardname = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.label4 = new System.Windows.Forms.Label();
            this.tb_page = new System.Windows.Forms.TextBox();
            this.tb_pagenum = new System.Windows.Forms.TextBox();
            this.btn_PageUp = new System.Windows.Forms.Button();
            this.btn_PageDown = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_redo = new System.Windows.Forms.Button();
            this.btn_undo = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pl_cardtype = new System.Windows.Forms.DFlowLayoutPanel();
            this.pl_category = new System.Windows.Forms.DFlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.menuitem_file,
                                    this.menuitem_edit,
                                    this.menuitem_help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(814, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuitem_file
            // 
            this.menuitem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.menuitem_open,
                                    this.menuitem_new,
                                    this.menuitem_save,
                                    this.menuitem_saveas,
                                    this.menuitem_copyto,
                                    this.tsep1,
                                    this.menuitem_inport,
                                    this.menuitem_export,
                                    this.tsep2,
                                    this.menuitem_readydk,
                                    this.menuitem_readimages,
                                    this.tsep3,
                                    this.menuitem_quit});
            this.menuitem_file.Name = "menuitem_file";
            this.menuitem_file.Size = new System.Drawing.Size(58, 21);
            this.menuitem_file.Text = "文件(&F)";
            // 
            // menuitem_open
            // 
            this.menuitem_open.Name = "menuitem_open";
            this.menuitem_open.Size = new System.Drawing.Size(124, 22);
            this.menuitem_open.Text = "打开(&O)";
            this.menuitem_open.Click += new System.EventHandler(this.Menuitem_openClick);
            // 
            // menuitem_new
            // 
            this.menuitem_new.Name = "menuitem_new";
            this.menuitem_new.Size = new System.Drawing.Size(124, 22);
            this.menuitem_new.Text = "新建(&N)";
            this.menuitem_new.Click += new System.EventHandler(this.Menuitem_newClick);
            // 
            // menuitem_save
            // 
            this.menuitem_save.Name = "menuitem_save";
            this.menuitem_save.Size = new System.Drawing.Size(124, 22);
            this.menuitem_save.Text = "保存(&S)";
            this.menuitem_save.Click += new System.EventHandler(this.Menuitem_saveClick);
            // 
            // menuitem_saveas
            // 
            this.menuitem_saveas.Name = "menuitem_saveas";
            this.menuitem_saveas.Size = new System.Drawing.Size(124, 22);
            this.menuitem_saveas.Text = "另存为";
            this.menuitem_saveas.Click += new System.EventHandler(this.Menuitem_saveasClick);
            // 
            // menuitem_copyto
            // 
            this.menuitem_copyto.Name = "menuitem_copyto";
            this.menuitem_copyto.Size = new System.Drawing.Size(124, 22);
            this.menuitem_copyto.Text = "复制到";
            this.menuitem_copyto.Click += new System.EventHandler(this.Menuitem_copytoClick);
            // 
            // tsep1
            // 
            this.tsep1.Name = "tsep1";
            this.tsep1.Size = new System.Drawing.Size(121, 6);
            // 
            // menuitem_inport
            // 
            this.menuitem_inport.Name = "menuitem_inport";
            this.menuitem_inport.Size = new System.Drawing.Size(124, 22);
            this.menuitem_inport.Text = "导出";
            this.menuitem_inport.Click += new System.EventHandler(this.Menuitem_inportClick);
            // 
            // menuitem_export
            // 
            this.menuitem_export.Name = "menuitem_export";
            this.menuitem_export.Size = new System.Drawing.Size(124, 22);
            this.menuitem_export.Text = "导入";
            this.menuitem_export.Click += new System.EventHandler(this.Menuitem_exportClick);
            // 
            // tsep2
            // 
            this.tsep2.Name = "tsep2";
            this.tsep2.Size = new System.Drawing.Size(121, 6);
            // 
            // menuitem_readydk
            // 
            this.menuitem_readydk.Name = "menuitem_readydk";
            this.menuitem_readydk.Size = new System.Drawing.Size(124, 22);
            this.menuitem_readydk.Text = "读取ydk";
            this.menuitem_readydk.Click += new System.EventHandler(this.Menuitem_readydkClick);
            // 
            // menuitem_readimages
            // 
            this.menuitem_readimages.Name = "menuitem_readimages";
            this.menuitem_readimages.Size = new System.Drawing.Size(124, 22);
            this.menuitem_readimages.Text = "读取图片";
            this.menuitem_readimages.Click += new System.EventHandler(this.Menuitem_readimagesClick);
            // 
            // tsep3
            // 
            this.tsep3.Name = "tsep3";
            this.tsep3.Size = new System.Drawing.Size(121, 6);
            // 
            // menuitem_quit
            // 
            this.menuitem_quit.Name = "menuitem_quit";
            this.menuitem_quit.Size = new System.Drawing.Size(124, 22);
            this.menuitem_quit.Text = "退出";
            this.menuitem_quit.Click += new System.EventHandler(this.Menuitem_quitClick);
            // 
            // menuitem_edit
            // 
            this.menuitem_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.menuitem_undo,
                                    this.menuitem_redo,
                                    this.tsep4,
                                    this.menuitem_add,
                                    this.menuitem_mod,
                                    this.menuitem_del,
                                    this.tsep5,
                                    this.menuitem_search,
                                    this.menuitem_reset,
                                    this.tsep6,
                                    this.menuitem_showimage,
                                    this.menuitem_openscript,
                                    this.menuitem_opengame});
            this.menuitem_edit.Name = "menuitem_edit";
            this.menuitem_edit.Size = new System.Drawing.Size(59, 21);
            this.menuitem_edit.Text = "编辑(&E)";
            // 
            // menuitem_undo
            // 
            this.menuitem_undo.Name = "menuitem_undo";
            this.menuitem_undo.Size = new System.Drawing.Size(152, 22);
            this.menuitem_undo.Text = "撤销";
            this.menuitem_undo.Click += new System.EventHandler(this.Menuitem_undoClick);
            // 
            // menuitem_redo
            // 
            this.menuitem_redo.Name = "menuitem_redo";
            this.menuitem_redo.Size = new System.Drawing.Size(152, 22);
            this.menuitem_redo.Text = "还原";
            this.menuitem_redo.Click += new System.EventHandler(this.Menuitem_redoClick);
            // 
            // tsep4
            // 
            this.tsep4.Name = "tsep4";
            this.tsep4.Size = new System.Drawing.Size(149, 6);
            // 
            // menuitem_add
            // 
            this.menuitem_add.Name = "menuitem_add";
            this.menuitem_add.Size = new System.Drawing.Size(152, 22);
            this.menuitem_add.Text = "添加";
            this.menuitem_add.Click += new System.EventHandler(this.Menuitem_addClick);
            // 
            // menuitem_mod
            // 
            this.menuitem_mod.Name = "menuitem_mod";
            this.menuitem_mod.Size = new System.Drawing.Size(152, 22);
            this.menuitem_mod.Text = "修改";
            this.menuitem_mod.Click += new System.EventHandler(this.Menuitem_modClick);
            // 
            // menuitem_del
            // 
            this.menuitem_del.Name = "menuitem_del";
            this.menuitem_del.Size = new System.Drawing.Size(152, 22);
            this.menuitem_del.Text = "删除";
            this.menuitem_del.Click += new System.EventHandler(this.Menuitem_delClick);
            // 
            // tsep5
            // 
            this.tsep5.Name = "tsep5";
            this.tsep5.Size = new System.Drawing.Size(149, 6);
            // 
            // menuitem_search
            // 
            this.menuitem_search.Name = "menuitem_search";
            this.menuitem_search.Size = new System.Drawing.Size(152, 22);
            this.menuitem_search.Text = "搜索";
            this.menuitem_search.Click += new System.EventHandler(this.Menuitem_searchClick);
            // 
            // menuitem_reset
            // 
            this.menuitem_reset.Name = "menuitem_reset";
            this.menuitem_reset.Size = new System.Drawing.Size(152, 22);
            this.menuitem_reset.Text = "重置";
            this.menuitem_reset.Click += new System.EventHandler(this.Menuitem_resetClick);
            // 
            // tsep6
            // 
            this.tsep6.Name = "tsep6";
            this.tsep6.Size = new System.Drawing.Size(149, 6);
            // 
            // menuitem_showimage
            // 
            this.menuitem_showimage.CheckOnClick = true;
            this.menuitem_showimage.Name = "menuitem_showimage";
            this.menuitem_showimage.Size = new System.Drawing.Size(152, 22);
            this.menuitem_showimage.Text = "显示卡片图像";
            this.menuitem_showimage.Click += new System.EventHandler(this.Menuitem_showimageClick);
            // 
            // menuitem_openscript
            // 
            this.menuitem_openscript.Name = "menuitem_openscript";
            this.menuitem_openscript.Size = new System.Drawing.Size(152, 22);
            this.menuitem_openscript.Text = "打开脚本";
            this.menuitem_openscript.Click += new System.EventHandler(this.Menuitem_openscriptClick);
            // 
            // menuitem_opengame
            // 
            this.menuitem_opengame.Name = "menuitem_opengame";
            this.menuitem_opengame.Size = new System.Drawing.Size(152, 22);
            this.menuitem_opengame.Text = "启动游戏";
            this.menuitem_opengame.Click += new System.EventHandler(this.Menuitem_opengameClick);
            // 
            // menuitem_help
            // 
            this.menuitem_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.menuitem_about,
                                    this.menuitem_checkupdate});
            this.menuitem_help.Name = "menuitem_help";
            this.menuitem_help.Size = new System.Drawing.Size(61, 21);
            this.menuitem_help.Text = "帮助(&H)";
            // 
            // menuitem_about
            // 
            this.menuitem_about.Name = "menuitem_about";
            this.menuitem_about.Size = new System.Drawing.Size(124, 22);
            this.menuitem_about.Text = "关于";
            this.menuitem_about.Click += new System.EventHandler(this.Menuitem_aboutClick);
            // 
            // menuitem_checkupdate
            // 
            this.menuitem_checkupdate.Name = "menuitem_checkupdate";
            this.menuitem_checkupdate.Size = new System.Drawing.Size(124, 22);
            this.menuitem_checkupdate.Text = "检查更新";
            this.menuitem_checkupdate.Click += new System.EventHandler(this.Menuitem_checkupdateClick);
            // 
            // lv_cardlist
            // 
            this.lv_cardlist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                                    | System.Windows.Forms.AnchorStyles.Left)));
            this.lv_cardlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                    this.ch_cardcode,
                                    this.ch_cardname});
            this.lv_cardlist.ContextMenuStrip = this.contextMenuStrip1;
            this.lv_cardlist.FullRowSelect = true;
            this.lv_cardlist.GridLines = true;
            this.lv_cardlist.HideSelection = false;
            this.lv_cardlist.LabelWrap = false;
            this.lv_cardlist.Location = new System.Drawing.Point(0, 27);
            this.lv_cardlist.Name = "lv_cardlist";
            this.lv_cardlist.ShowItemToolTips = true;
            this.lv_cardlist.Size = new System.Drawing.Size(218, 495);
            this.lv_cardlist.TabIndex = 1;
            this.lv_cardlist.UseCompatibleStateImageBehavior = false;
            this.lv_cardlist.View = System.Windows.Forms.View.Details;
            this.lv_cardlist.SelectedIndexChanged += new System.EventHandler(this.Lv_cardlistSelectedIndexChanged);
            // 
            // ch_cardcode
            // 
            this.ch_cardcode.Text = "卡片密码";
            this.ch_cardcode.Width = 70;
            // 
            // ch_cardname
            // 
            this.ch_cardname.Text = "卡片名称";
            this.ch_cardname.Width = 140;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.importToolStripMenuItem,
                                    this.exportToolStripMenuItem,
                                    this.copyToToolStripMenuItem,
                                    this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 92);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // copyToToolStripMenuItem
            // 
            this.copyToToolStripMenuItem.Name = "copyToToolStripMenuItem";
            this.copyToToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.copyToToolStripMenuItem.Text = "CopyTo";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // cb_cardattribute
            // 
            this.cb_cardattribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardattribute.FormattingEnabled = true;
            this.cb_cardattribute.Location = new System.Drawing.Point(375, 49);
            this.cb_cardattribute.Name = "cb_cardattribute";
            this.cb_cardattribute.Size = new System.Drawing.Size(140, 20);
            this.cb_cardattribute.TabIndex = 2;
            // 
            // tb_cardname
            // 
            this.tb_cardname.Location = new System.Drawing.Point(530, 51);
            this.tb_cardname.Name = "tb_cardname";
            this.tb_cardname.Size = new System.Drawing.Size(281, 21);
            this.tb_cardname.TabIndex = 4;
            this.tb_cardname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_cardname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_cardnameKeyDown);
            // 
            // cb_cardrule
            // 
            this.cb_cardrule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardrule.FormattingEnabled = true;
            this.cb_cardrule.Location = new System.Drawing.Point(223, 50);
            this.cb_cardrule.Name = "cb_cardrule";
            this.cb_cardrule.Size = new System.Drawing.Size(147, 20);
            this.cb_cardrule.TabIndex = 2;
            // 
            // cb_cardlevel
            // 
            this.cb_cardlevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardlevel.FormattingEnabled = true;
            this.cb_cardlevel.Location = new System.Drawing.Point(375, 75);
            this.cb_cardlevel.Name = "cb_cardlevel";
            this.cb_cardlevel.Size = new System.Drawing.Size(140, 20);
            this.cb_cardlevel.TabIndex = 2;
            // 
            // cb_cardrace
            // 
            this.cb_cardrace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardrace.FormattingEnabled = true;
            this.cb_cardrace.Location = new System.Drawing.Point(223, 75);
            this.cb_cardrace.Name = "cb_cardrace";
            this.cb_cardrace.Size = new System.Drawing.Size(146, 20);
            this.cb_cardrace.TabIndex = 2;
            // 
            // cb_setname2
            // 
            this.cb_setname2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname2.FormattingEnabled = true;
            this.cb_setname2.Location = new System.Drawing.Point(375, 101);
            this.cb_setname2.Name = "cb_setname2";
            this.cb_setname2.Size = new System.Drawing.Size(140, 20);
            this.cb_setname2.TabIndex = 2;
            // 
            // cb_setname1
            // 
            this.cb_setname1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname1.FormattingEnabled = true;
            this.cb_setname1.Location = new System.Drawing.Point(223, 101);
            this.cb_setname1.Name = "cb_setname1";
            this.cb_setname1.Size = new System.Drawing.Size(146, 20);
            this.cb_setname1.TabIndex = 2;
            // 
            // cb_setname4
            // 
            this.cb_setname4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname4.FormattingEnabled = true;
            this.cb_setname4.Location = new System.Drawing.Point(375, 127);
            this.cb_setname4.Name = "cb_setname4";
            this.cb_setname4.Size = new System.Drawing.Size(140, 20);
            this.cb_setname4.TabIndex = 2;
            // 
            // cb_setname3
            // 
            this.cb_setname3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname3.FormattingEnabled = true;
            this.cb_setname3.Location = new System.Drawing.Point(223, 127);
            this.cb_setname3.Name = "cb_setname3";
            this.cb_setname3.Size = new System.Drawing.Size(146, 20);
            this.cb_setname3.TabIndex = 2;
            // 
            // tb_cardtext
            // 
            this.tb_cardtext.AcceptsReturn = true;
            this.tb_cardtext.AcceptsTab = true;
            this.tb_cardtext.Location = new System.Drawing.Point(530, 96);
            this.tb_cardtext.Multiline = true;
            this.tb_cardtext.Name = "tb_cardtext";
            this.tb_cardtext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_cardtext.Size = new System.Drawing.Size(281, 236);
            this.tb_cardtext.TabIndex = 4;
            this.tb_cardtext.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_cardtextKeyDown);
            // 
            // lb_scripttext
            // 
            this.lb_scripttext.FormattingEnabled = true;
            this.lb_scripttext.IntegralHeight = false;
            this.lb_scripttext.ItemHeight = 12;
            this.lb_scripttext.Location = new System.Drawing.Point(532, 357);
            this.lb_scripttext.Name = "lb_scripttext";
            this.lb_scripttext.ScrollAlwaysVisible = true;
            this.lb_scripttext.Size = new System.Drawing.Size(281, 120);
            this.lb_scripttext.TabIndex = 6;
            this.lb_scripttext.SelectedIndexChanged += new System.EventHandler(this.Lb_scripttextSelectedIndexChanged);
            // 
            // tb_edittext
            // 
            this.tb_edittext.HideSelection = false;
            this.tb_edittext.Location = new System.Drawing.Point(533, 478);
            this.tb_edittext.MaxLength = 2000;
            this.tb_edittext.Multiline = true;
            this.tb_edittext.Name = "tb_edittext";
            this.tb_edittext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_edittext.Size = new System.Drawing.Size(279, 41);
            this.tb_edittext.TabIndex = 4;
            this.tb_edittext.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_edittextKeyPress);
            // 
            // lb_pleft_right
            // 
            this.lb_pleft_right.AutoSize = true;
            this.lb_pleft_right.Location = new System.Drawing.Point(226, 156);
            this.lb_pleft_right.Name = "lb_pleft_right";
            this.lb_pleft_right.Size = new System.Drawing.Size(35, 12);
            this.lb_pleft_right.TabIndex = 7;
            this.lb_pleft_right.Text = "摇摆:";
            // 
            // tb_pleft
            // 
            this.tb_pleft.Location = new System.Drawing.Point(274, 151);
            this.tb_pleft.MaxLength = 12;
            this.tb_pleft.Name = "tb_pleft";
            this.tb_pleft.Size = new System.Drawing.Size(38, 21);
            this.tb_pleft.TabIndex = 8;
            this.tb_pleft.Text = "0";
            this.tb_pleft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_pright
            // 
            this.tb_pright.Location = new System.Drawing.Point(332, 151);
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
            this.lb_atkdef.Location = new System.Drawing.Point(373, 157);
            this.lb_atkdef.Name = "lb_atkdef";
            this.lb_atkdef.Size = new System.Drawing.Size(47, 12);
            this.lb_atkdef.TabIndex = 7;
            this.lb_atkdef.Text = "ATK/DEF";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 533);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "/";
            // 
            // tb_page
            // 
            this.tb_page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_page.Location = new System.Drawing.Point(67, 528);
            this.tb_page.MaxLength = 12;
            this.tb_page.Name = "tb_page";
            this.tb_page.Size = new System.Drawing.Size(34, 21);
            this.tb_page.TabIndex = 8;
            this.tb_page.Text = "0";
            this.tb_page.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_page.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_pageKeyPress);
            // 
            // tb_pagenum
            // 
            this.tb_pagenum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_pagenum.Location = new System.Drawing.Point(118, 528);
            this.tb_pagenum.MaxLength = 12;
            this.tb_pagenum.Name = "tb_pagenum";
            this.tb_pagenum.ReadOnly = true;
            this.tb_pagenum.Size = new System.Drawing.Size(34, 21);
            this.tb_pagenum.TabIndex = 8;
            this.tb_pagenum.Text = "0";
            this.tb_pagenum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_PageUp
            // 
            this.btn_PageUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_PageUp.Location = new System.Drawing.Point(0, 525);
            this.btn_PageUp.Name = "btn_PageUp";
            this.btn_PageUp.Size = new System.Drawing.Size(64, 24);
            this.btn_PageUp.TabIndex = 5;
            this.btn_PageUp.Text = "< <";
            this.btn_PageUp.UseVisualStyleBackColor = true;
            this.btn_PageUp.Click += new System.EventHandler(this.Btn_PageUpClick);
            // 
            // btn_PageDown
            // 
            this.btn_PageDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_PageDown.Location = new System.Drawing.Point(155, 526);
            this.btn_PageDown.Name = "btn_PageDown";
            this.btn_PageDown.Size = new System.Drawing.Size(64, 24);
            this.btn_PageDown.TabIndex = 5;
            this.btn_PageDown.Text = "> >";
            this.btn_PageDown.UseVisualStyleBackColor = true;
            this.btn_PageDown.Click += new System.EventHandler(this.Btn_PageDownClick);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(532, 524);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(68, 26);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "添加";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.Btn_addClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(463, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "/";
            // 
            // tb_atk
            // 
            this.tb_atk.Location = new System.Drawing.Point(421, 152);
            this.tb_atk.MaxLength = 12;
            this.tb_atk.Name = "tb_atk";
            this.tb_atk.Size = new System.Drawing.Size(38, 21);
            this.tb_atk.TabIndex = 8;
            this.tb_atk.Text = "0";
            this.tb_atk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_def
            // 
            this.tb_def.Location = new System.Drawing.Point(477, 152);
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
            this.lb_cardcode.Location = new System.Drawing.Point(224, 180);
            this.lb_cardcode.Name = "lb_cardcode";
            this.lb_cardcode.Size = new System.Drawing.Size(53, 12);
            this.lb_cardcode.TabIndex = 7;
            this.lb_cardcode.Text = "卡片密码";
            // 
            // tb_cardcode
            // 
            this.tb_cardcode.Location = new System.Drawing.Point(296, 176);
            this.tb_cardcode.MaxLength = 12;
            this.tb_cardcode.Name = "tb_cardcode";
            this.tb_cardcode.Size = new System.Drawing.Size(74, 21);
            this.tb_cardcode.TabIndex = 8;
            this.tb_cardcode.Text = "0";
            this.tb_cardcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_cardcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_cardcodeKeyPress);
            // 
            // lb_cardalias
            // 
            this.lb_cardalias.AutoSize = true;
            this.lb_cardalias.Location = new System.Drawing.Point(375, 183);
            this.lb_cardalias.Name = "lb_cardalias";
            this.lb_cardalias.Size = new System.Drawing.Size(41, 12);
            this.lb_cardalias.TabIndex = 7;
            this.lb_cardalias.Text = "同名卡";
            // 
            // tb_cardalias
            // 
            this.tb_cardalias.Location = new System.Drawing.Point(439, 177);
            this.tb_cardalias.MaxLength = 12;
            this.tb_cardalias.Name = "tb_cardalias";
            this.tb_cardalias.Size = new System.Drawing.Size(74, 21);
            this.tb_cardalias.TabIndex = 8;
            this.tb_cardalias.Text = "0";
            this.tb_cardalias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_mod
            // 
            this.btn_mod.Location = new System.Drawing.Point(603, 524);
            this.btn_mod.Name = "btn_mod";
            this.btn_mod.Size = new System.Drawing.Size(68, 26);
            this.btn_mod.TabIndex = 5;
            this.btn_mod.Text = "修改";
            this.btn_mod.UseVisualStyleBackColor = true;
            this.btn_mod.Click += new System.EventHandler(this.Btn_modClick);
            // 
            // btn_del
            // 
            this.btn_del.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_del.Location = new System.Drawing.Point(743, 524);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(68, 26);
            this.btn_del.TabIndex = 5;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.Btn_delClick);
            // 
            // btn_lua
            // 
            this.btn_lua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btn_lua.Location = new System.Drawing.Point(673, 524);
            this.btn_lua.Name = "btn_lua";
            this.btn_lua.Size = new System.Drawing.Size(68, 26);
            this.btn_lua.TabIndex = 5;
            this.btn_lua.Text = "脚本";
            this.btn_lua.UseVisualStyleBackColor = true;
            this.btn_lua.Click += new System.EventHandler(this.Btn_luaClick);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(307, 523);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(78, 27);
            this.btn_reset.TabIndex = 5;
            this.btn_reset.Text = "重置";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.Btn_resetClick);
            // 
            // btn_serach
            // 
            this.btn_serach.Location = new System.Drawing.Point(222, 524);
            this.btn_serach.Name = "btn_serach";
            this.btn_serach.Size = new System.Drawing.Size(83, 25);
            this.btn_serach.TabIndex = 5;
            this.btn_serach.Text = "搜索";
            this.btn_serach.UseVisualStyleBackColor = true;
            this.btn_serach.Click += new System.EventHandler(this.Btn_serachClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(221, 202);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "卡片种类";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(220, 336);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "效果种类";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label6.Location = new System.Drawing.Point(532, 337);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(278, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "脚本提示文本";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "/";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label7.Location = new System.Drawing.Point(220, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(293, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "卡片信息";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_redo
            // 
            this.btn_redo.Location = new System.Drawing.Point(390, 522);
            this.btn_redo.Name = "btn_redo";
            this.btn_redo.Size = new System.Drawing.Size(68, 26);
            this.btn_redo.TabIndex = 5;
            this.btn_redo.Text = "还原";
            this.btn_redo.UseVisualStyleBackColor = true;
            this.btn_redo.Click += new System.EventHandler(this.Btn_redoClick);
            // 
            // btn_undo
            // 
            this.btn_undo.Location = new System.Drawing.Point(460, 522);
            this.btn_undo.Name = "btn_undo";
            this.btn_undo.Size = new System.Drawing.Size(68, 26);
            this.btn_undo.TabIndex = 5;
            this.btn_undo.Text = "撤销";
            this.btn_undo.UseVisualStyleBackColor = true;
            this.btn_undo.Click += new System.EventHandler(this.Btn_undoClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(530, -168);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(281, 21);
            this.textBox1.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label8.Location = new System.Drawing.Point(532, 76);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(278, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "卡片描述文本";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(534, -235);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(281, 21);
            this.textBox2.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label9.Location = new System.Drawing.Point(533, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(278, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "卡片名称";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pl_cardtype
            // 
            this.pl_cardtype.Location = new System.Drawing.Point(220, 219);
            this.pl_cardtype.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pl_cardtype.Name = "pl_cardtype";
            this.pl_cardtype.Padding = new System.Windows.Forms.Padding(2);
            this.pl_cardtype.Size = new System.Drawing.Size(293, 112);
            this.pl_cardtype.TabIndex = 12;
            // 
            // pl_category
            // 
            this.pl_category.Location = new System.Drawing.Point(220, 353);
            this.pl_category.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pl_category.Name = "pl_category";
            this.pl_category.Padding = new System.Windows.Forms.Padding(2);
            this.pl_category.Size = new System.Drawing.Size(310, 169);
            this.pl_category.TabIndex = 13;
            // 
            // DataEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 553);
            this.Controls.Add(this.pl_category);
            this.Controls.Add(this.pl_cardtype);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_edittext);
            this.Controls.Add(this.tb_pagenum);
            this.Controls.Add(this.tb_def);
            this.Controls.Add(this.tb_pright);
            this.Controls.Add(this.tb_page);
            this.Controls.Add(this.tb_cardalias);
            this.Controls.Add(this.tb_cardcode);
            this.Controls.Add(this.tb_atk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_cardalias);
            this.Controls.Add(this.tb_pleft);
            this.Controls.Add(this.lb_cardcode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_atkdef);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_pleft_right);
            this.Controls.Add(this.lb_scripttext);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_lua);
            this.Controls.Add(this.btn_serach);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_undo);
            this.Controls.Add(this.btn_mod);
            this.Controls.Add(this.btn_redo);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.btn_PageDown);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_PageUp);
            this.Controls.Add(this.textBox1);
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
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DataEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataEditor";
            this.Load += new System.EventHandler(this.DataEditFormLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.ToolStripMenuItem menuitem_new;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuitem_showimage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_undo;
        private System.Windows.Forms.Button btn_redo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_PageDown;
        private System.Windows.Forms.Button btn_PageUp;
        private System.Windows.Forms.TextBox tb_pagenum;
        private System.Windows.Forms.TextBox tb_page;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_atkdef;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.ToolStripMenuItem menuitem_opengame;
        private System.Windows.Forms.ToolStripMenuItem menuitem_openscript;
        private System.Windows.Forms.ToolStripSeparator tsep6;
        private System.Windows.Forms.ToolStripMenuItem menuitem_reset;
        private System.Windows.Forms.ToolStripMenuItem menuitem_search;
        private System.Windows.Forms.ToolStripSeparator tsep5;
        private System.Windows.Forms.ToolStripMenuItem menuitem_del;
        private System.Windows.Forms.ToolStripMenuItem menuitem_mod;
        private System.Windows.Forms.ToolStripMenuItem menuitem_add;
        private System.Windows.Forms.ToolStripSeparator tsep4;
        private System.Windows.Forms.ToolStripMenuItem menuitem_redo;
        private System.Windows.Forms.ToolStripMenuItem menuitem_undo;
        private System.Windows.Forms.ToolStripMenuItem menuitem_edit;
        private System.Windows.Forms.ToolStripMenuItem menuitem_quit;
        private System.Windows.Forms.ToolStripSeparator tsep3;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readimages;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readydk;
        private System.Windows.Forms.ToolStripSeparator tsep2;
        private System.Windows.Forms.ToolStripMenuItem menuitem_export;
        private System.Windows.Forms.ToolStripMenuItem menuitem_inport;
        private System.Windows.Forms.ToolStripSeparator tsep1;
        private System.Windows.Forms.ToolStripMenuItem menuitem_copyto;
        private System.Windows.Forms.ToolStripMenuItem menuitem_saveas;
        private System.Windows.Forms.ToolStripMenuItem menuitem_save;
        private System.Windows.Forms.ToolStripMenuItem menuitem_open;
        private System.Windows.Forms.ToolStripMenuItem menuitem_file;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}
