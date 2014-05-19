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
        Image deimg=null;
        void ImageFormFormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel=true;
        }
        public void SetDefault(Image img)
        {
            deimg=img;
            SetImage(img,"默认");
        }
        public void SetImage(Image img,string title)
        {
            this.Text=title;
            this.BackgroundImage=img;
        }
        public void SetImageFile(string file,string title)
        {
             this.Text=title;
             if(System.IO.File.Exists(file))
                 this.BackgroundImage=Image.FromFile(file);
             else
                 this.BackgroundImage=deimg;
        }
    }
}
