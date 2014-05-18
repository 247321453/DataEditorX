/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月12 星期一
 * 时间: 12:00
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditorX
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        DataEditForm editForm;
        string datadir=".\\data\\";
        public MainForm(string filename)
        {
            Init(filename);
        }
        public MainForm()
        {
            Init(null);
        }
        
        void Init(string filename)
        {
            if(filename==null)
                editForm=new DataEditForm();
            else
                editForm=new DataEditForm(filename);
            InitializeComponent();
            timer1.Interval=2000;
            timer1.Enabled=true;
            editForm.InitForm(datadir);
        }
        
        void Timer1Tick(object sender, EventArgs e)
        {
            timer1.Enabled=false;
            this.Hide();
            editForm.ShowDialog();
            this.Close();
        }
    }
}
