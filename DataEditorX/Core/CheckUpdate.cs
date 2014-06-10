/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 6月10 星期二
 * 时间: 9:58
 * 
 */
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DataEditorX.Core
{
    /// <summary>
    /// Description of CheckUpdate.
    /// </summary>
    public class CheckUpdate
    {
        static string HEAD="[DataEditorX]";
        public static void UpdateTip(string VERURL)
        {
            string newver=Check(VERURL);
            int iver,iver2;
            int.TryParse(Application.ProductVersion.Replace(".",""), out iver);
            int.TryParse(newver.Replace(".",""), out iver2);
            if(iver2>iver)
            {
                if(MyMsg.Question("发现新版本："+newver
                                  +"\n是否打开下载页面？"))
                {
                    System.Diagnostics.Process.Start(VERURL);
                }
            }
            else if(iver2>0)
                MyMsg.Show("已经是最新版本！\n版本号："+newver);
            else
                MyMsg.Error("查询失败！\n请检查计算机的网络连接。");
        }
        public static string Check(string VERURL)
        {
            string urlver="0.0.0.0";
            string html=GetHtmlContentByUrl(VERURL);
            if(!string.IsNullOrEmpty(html))
            {
                int t,w;
                t=html.IndexOf(HEAD);
                w=(t>0)?html.IndexOf(HEAD,t+HEAD.Length):0;
                if(w>0)
                {
                    urlver=html.Substring(t+HEAD.Length,w-t-HEAD.Length);
                }
            }
            return urlver;
        }
        
        #region 获取网址内容
        public static string GetHtmlContentByUrl(string url)
        {
            string htmlContent = string.Empty;
            try {
                HttpWebRequest httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Timeout = 5000;
                using(HttpWebResponse httpWebResponse =
                      (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using(Stream stream = httpWebResponse.GetResponseStream())
                    {
                        using(StreamReader streamReader =
                              new StreamReader(stream, Encoding.UTF8))
                        {
                            htmlContent = streamReader.ReadToEnd();
                            streamReader.Close();
                        }
                        stream.Close();
                    }
                    httpWebResponse.Close();
                }
                return htmlContent;
            }
            catch{
                
            }
            return "";
        }
        #endregion
    }
}
