/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月19 星期一
 * 时间: 11:19
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditorX
{
    /// <summary>
    /// Description of ImageForm.
    /// </summary>
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }
        void ImageFormFormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel=true;
        }
        public void SetImage(Image img,string title)
        {
            this.Text=title;
            this.BackgroundImage=img;
        }
    }
}
