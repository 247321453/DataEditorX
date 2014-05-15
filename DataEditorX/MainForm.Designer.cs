/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月12 星期一
 * 时间: 12:00
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
            this.lv_cardlist = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.ms_main = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存SCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还原YToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建NToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检查更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ms_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_cardlist
            // 
            this.lv_cardlist.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lv_cardlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                    this.columnHeader1,
                                    this.columnHeader2});
            this.lv_cardlist.FullRowSelect = true;
            this.lv_cardlist.GridLines = true;
            this.lv_cardlist.HideSelection = false;
            this.lv_cardlist.HotTracking = true;
            this.lv_cardlist.HoverSelection = true;
            this.lv_cardlist.Location = new System.Drawing.Point(0, 25);
            this.lv_cardlist.Name = "lv_cardlist";
            this.lv_cardlist.Size = new System.Drawing.Size(187, 388);
            this.lv_cardlist.TabIndex = 0;
            this.lv_cardlist.UseCompatibleStateImageBehavior = false;
            this.lv_cardlist.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 62;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 79;
            // 
            // ms_main
            // 
            this.ms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.文件FToolStripMenuItem,
                                    this.编辑EToolStripMenuItem,
                                    this.帮助ToolStripMenuItem});
            this.ms_main.Location = new System.Drawing.Point(0, 0);
            this.ms_main.Name = "ms_main";
            this.ms_main.Size = new System.Drawing.Size(681, 25);
            this.ms_main.TabIndex = 1;
            this.ms_main.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.打开OToolStripMenuItem,
                                    this.新建NToolStripMenuItem,
                                    this.保存SCToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(70, 21);
            this.文件FToolStripMenuItem.Text = "数据库(&F)";
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            // 
            // 新建NToolStripMenuItem
            // 
            this.新建NToolStripMenuItem.Name = "新建NToolStripMenuItem";
            this.新建NToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建NToolStripMenuItem.Text = "新建(&N)";
            // 
            // 保存SCToolStripMenuItem
            // 
            this.保存SCToolStripMenuItem.Name = "保存SCToolStripMenuItem";
            this.保存SCToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存SCToolStripMenuItem.Text = "保存(&S)";
            // 
            // 编辑EToolStripMenuItem
            // 
            this.编辑EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.撤销UToolStripMenuItem,
                                    this.还原YToolStripMenuItem,
                                    this.添加AToolStripMenuItem,
                                    this.修改MToolStripMenuItem,
                                    this.删除DToolStripMenuItem,
                                    this.新建NToolStripMenuItem1});
            this.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem";
            this.编辑EToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.编辑EToolStripMenuItem.Text = "编辑(&E)";
            // 
            // 撤销UToolStripMenuItem
            // 
            this.撤销UToolStripMenuItem.Name = "撤销UToolStripMenuItem";
            this.撤销UToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.撤销UToolStripMenuItem.Text = "撤销(&U)";
            // 
            // 还原YToolStripMenuItem
            // 
            this.还原YToolStripMenuItem.Name = "还原YToolStripMenuItem";
            this.还原YToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.还原YToolStripMenuItem.Text = "还原(&Y)";
            // 
            // 添加AToolStripMenuItem
            // 
            this.添加AToolStripMenuItem.Name = "添加AToolStripMenuItem";
            this.添加AToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加AToolStripMenuItem.Text = "添加(&A)";
            // 
            // 修改MToolStripMenuItem
            // 
            this.修改MToolStripMenuItem.Name = "修改MToolStripMenuItem";
            this.修改MToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改MToolStripMenuItem.Text = "修改(&M)";
            // 
            // 删除DToolStripMenuItem
            // 
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            // 
            // 新建NToolStripMenuItem1
            // 
            this.新建NToolStripMenuItem1.Name = "新建NToolStripMenuItem1";
            this.新建NToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.新建NToolStripMenuItem1.Text = "新建(&N)";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.关于ToolStripMenuItem,
                                    this.检查更新ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 检查更新ToolStripMenuItem
            // 
            this.检查更新ToolStripMenuItem.Name = "检查更新ToolStripMenuItem";
            this.检查更新ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.检查更新ToolStripMenuItem.Text = "检查更新";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 479);
            this.Controls.Add(this.lv_cardlist);
            this.Controls.Add(this.ms_main);
            this.MainMenuStrip = this.ms_main;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataEditorX";
            this.ms_main.ResumeLayout(false);
            this.ms_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.ToolStripMenuItem 检查更新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建NToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还原YToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤销UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存SCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.MenuStrip ms_main;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lv_cardlist;
    }
}
