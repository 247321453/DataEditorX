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

namespace DataEditorX
{
    /// <summary>
    /// Description of CheckUpdate.
    /// </summary>
    public class CheckUpdate
    {
        static string URL="";
        static string HEAD="[DataEditorX]",HEAD2="[URL]";
        public static void UpdateTip(string VERURL)
        {
            string newver=Check(VERURL);
            int iver,iver2;
            int.TryParse(Application.ProductVersion.Replace(".",""), out iver);
            int.TryParse(newver.Replace(".",""), out iver2);
            if(iver2>iver)
            {
                if(MyMsg.Question(string.Format(
                    MyMsg.GetString("have a new version.{0}version:{1}"),
                                    "\n",newver)))
                {
                    if(DownLoad(URL,Path.Combine(Application.StartupPath, newver+".update.zip"),null))
                    {
                        MyMsg.Show("Download succeed.");
                    }
                }
            }
            else if(iver2>0)
                MyMsg.Show(string.Format(MyMsg.GetString("Is Last Version.{0}Version:{1}"),
                                         "\n",newver));
            else
                MyMsg.Error(MyMsg.GetString("Check update fail!"));
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
                t=html.IndexOf(HEAD2);
                w=(t>0)?html.IndexOf(HEAD2,t+HEAD2.Length):0;
                if(w>0)
                {
                    URL=html.Substring(t+HEAD2.Length,w-t-HEAD2.Length);
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
        
        public static bool DownLoad(string URL,string filename,System.Windows.Forms.ProgressBar prog)
        {
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
    }
}
