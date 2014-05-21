/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月21 星期三
 * 时间: 16:21
 * 
 */
namespace DataEditorX
{
    partial class UpdateForm
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
            this.btn_check = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_check
            // 
            this.btn_check.Location = new System.Drawing.Point(12, 12);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(75, 23);
            this.btn_check.TabIndex = 0;
            this.btn_check.Text = "检查更新";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.Btn_checkClick);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 315);
            this.Controls.Add(this.btn_check);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "UpdateForm";
            this.Text = "DataEditorX更新";
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Button btn_check;
    }
}
