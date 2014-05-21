/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月21 星期三
 * 时间: 10:10
 * 
 */
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace DataEditorX
{
    /// <summary>
    /// Description of MyUpdate.
    /// </summary>
    public static class MyUpdate
    {
        static readonly string VERURL="https://github.com/247321453/DataEditorX/raw/master/DataEditorX/Update/version.txt";
        static readonly string DOWNURL="https://github.com/247321453/DataEditorX/raw/master/DataEditorX/Update/Release.zip";
        public static bool CheckUpdate()
        {
            string ver,lasttime;
            int l=-1;
            string html=GetHtmlContentByUrl(VERURL);
            if(string.IsNullOrEmpty(html))
            {
                MyMsg.Error("无法获取版本！");
                return false;
            }
            l=html.IndexOf(" time:");
            ver=(l>=0)?html.Substring(4,l):"?.?.?.?";
            lasttime=(l>=0)?html.Substring(l):"????-??-?? ??:??";
            MyMsg.Show("当前版本："+Application.ProductVersion
                       +"\n最新版本："+ver
                       +"\n最后更新时间："+lasttime);
            
            return false;
        }
        public static void Download()
        {
            
        }
        /// <summary>
        ///根据url获取网站html内容 /// </summary>
        /// <param name="url">网址</param> /// <returns>获取网站html内容</returns>
        public static string GetHtmlContentByUrl(string url)
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
    }
}
