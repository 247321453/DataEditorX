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
    public static class CheckUpdate
    {
        static CheckUpdate()
        {
            ServicePointManager.DefaultConnectionLimit = 255;
        }
        public static string URL = "";
        static string HEAD = "[DataEditorX]", HEAD2 = "[URL]";
        public static bool isOK = false;

        #region 检查版本
        public static string Check(string VERURL)
        {
            string urlver = "0.0.0.0";
            string html = GetHtmlContentByUrl(VERURL);
            if (!string.IsNullOrEmpty(html))
            {
                int t, w;
                t = html.IndexOf(HEAD);
                w = (t > 0) ? html.IndexOf(HEAD, t + HEAD.Length) : 0;
                if (w > 0)
                {
                    urlver = html.Substring(t + HEAD.Length, w - t - HEAD.Length);
                }
                t = html.IndexOf(HEAD2);
                w = (t > 0) ? html.IndexOf(HEAD2, t + HEAD2.Length) : 0;
                if (w > 0)
                {
                    URL = html.Substring(t + HEAD2.Length, w - t - HEAD2.Length);
                }
            }
            return urlver;
        }
        #endregion

        #region 获取网址内容
        public static string GetHtmlContentByUrl(string url)
        {
            string htmlContent = string.Empty;
            try
            {
                HttpWebRequest httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Timeout = 15000;
                using (HttpWebResponse httpWebResponse =
                      (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader =
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
            catch
            {

            }
            return "";
        }
        #endregion

        #region 下载文件
        public static bool DownLoad(string filename)
        {
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                Stream st = myrp.GetResponseStream();
                Stream so = new System.IO.FileStream(filename + ".tmp", FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[2048];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                File.Move(filename + ".tmp", filename);
            }
            catch (System.Exception)
            {
                isOK = false;
            }
            isOK = true;
            return isOK;
        }
        #endregion
    }
}
