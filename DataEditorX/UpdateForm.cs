/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月21 星期三
 * 时间: 16:21
 * 
 */
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace DataEditorX
{
    /// <summary>
    /// Description of UpdateForm.
    /// </summary>
    public partial class UpdateForm : Form
    {
        string Ver;
        public UpdateForm()
        {
            Ver=Application.ProductVersion;
            InitializeComponent();
        }
        public UpdateForm(string ver)
        {
            Ver=ver;
            InitializeComponent();
        }
        
        void Btn_checkClick(object sender, EventArgs e)
        {
            
        }
        
        #region 获取网址内容
        string GetHtmlContentByUrl(string url)
        {
            string htmlContent = string.Empty;
            try {
                HttpWebRequest httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Timeout = 5000;
                using(HttpWebResponse httpWebResponse =
                      (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using(Stream stream = httpWebResponse.GetResponseStream())
                    {
                        using(StreamReader streamReader =
                              new StreamReader(stream, Encoding.UTF8))
                        {
                            htmlContent = streamReader.ReadToEnd();
                            streamReader.Close();
                        }
                        stream.Close();
                    }
                    httpWebResponse.Close();
                }
                return htmlContent;
            }
            catch{
                
            }
            return "";
        }
        #endregion
    }
}
