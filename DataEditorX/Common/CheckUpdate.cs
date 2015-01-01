/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 6月10 星期二
 * 时间: 9:58
 * 
 */
using System.IO;
using System.Net;
using System.Text;

namespace DataEditorX.Common
{
    /// <summary>
    /// 检查更新
    /// </summary>
    public static class CheckUpdate
    {
        static CheckUpdate()
        {
            //连接数
            ServicePointManager.DefaultConnectionLimit = 255;
        }
        /// <summary>
        /// 下载URL
        /// </summary>
        public static string URL = "";
        /// <summary>
        /// 从HEAD获取版本号
        /// </summary>
        const string HEAD = "[DataEditorX]";
        const string HEAD2 = "[URL]";
        public const string DEFALUT = "0.0.0.0";
        const int VER_LENGTH = 4;

        #region 检查版本
        /// <summary>
        /// 获取新版本
        /// </summary>
        /// <param name="VERURL">链接</param>
        /// <returns>版本号</returns>
        public static string GetNewVersion(string VERURL)
        {
            string urlver = DEFALUT;
            string html = GetHtmlContentByUrl(VERURL);
            if (!string.IsNullOrEmpty(html))
            {
                int t, w;
                t = html.IndexOf(HEAD);
                w = (t > 0) ? html.IndexOf(HEAD, t + HEAD.Length) : 0;
                if (w > 0)
                {
                    //获取版本
                    urlver = html.Substring(t + HEAD.Length, w - t - HEAD.Length);
                }
                t = html.IndexOf(HEAD2);
                w = (t > 0) ? html.IndexOf(HEAD2, t + HEAD2.Length) : 0;
                if (w > 0)
                {
                    //获取下载地址
                    URL = html.Substring(t + HEAD2.Length, w - t - HEAD2.Length);
                }
            }
            return urlver;
        }
        /// <summary>
        /// 检查版本号，格式0.0.0.0
        /// </summary>
        /// <param name="ver">0.0.0.0</param>
        /// <param name="oldver">0.0.0.0</param>
        /// <returns>是否有新版本</returns>
        public static bool CheckVersion(string ver, string oldver)
        {
            bool hasNew = false;
            string[] vers = ver.Split('.');
            string[] oldvers = oldver.Split('.');
            if (vers.Length == oldvers.Length && vers.Length == VER_LENGTH)
            {
                int j, k;//从左到右比较数字
                for (int i = 0; i < VER_LENGTH; i++)
                {
                    int.TryParse(vers[i], out j);
                    int.TryParse(oldvers[i], out k);
                    if (j > k)//新的版本号大于旧的
                    {
                        hasNew = true;
                        break;
                    }
                }
            }
            
#if DEBUG
            MessageBox.Show("new:" + ver + ",oldver:" + oldver + ",hasnew:" + hasNew.ToString());
#endif
            return hasNew;
        }
        #endregion

        #region 获取网址内容
        /// <summary>
        /// 获取网址内容
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns>内容</returns>
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
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filename">保存文件路径</param>
        /// <returns>是否下载成功</returns>
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
                return false;
            }
            return true;
        }
        #endregion
    }
}
