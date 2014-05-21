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
        static string downloadURL="";
        static string Head="[DataEditorX]";
        static string Vhead="v";
        static string Thead="t";
        static string Dhead="d";
        public static bool CheckUpdate(string url)
        {
            string ver,lasttime,verurl;
            int t,w;
            string html=GetHtmlContentByUrl(url);
            if(string.IsNullOrEmpty(html))
            {
                MyMsg.Error("无法获取版本！");
                return false;
            }
            t=html.IndexOf(Head);
            w=(t>=0)?html.IndexOf(Head,t+1):0;
            if(w<=0)
            {
                MyMsg.Error("无法解释网址内容！\n"+url);
                return false;
            }
            verurl=html.Substring(t+Head.Length,w-t-Head.Length);
            if(!verurl.StartsWith("http://",StringComparison.OrdinalIgnoreCase))
                verurl="http://"+verurl;
            #if DEBUG
            MyMsg.Show(verurl);
            #endif
            html= GetHtmlContentByUrl(verurl);
            
            if(string.IsNullOrEmpty(html))
            {
                MyMsg.Error("无法解释网址内容！\n"+verurl);
                return false;
            }
            
            w=0;
            
            t=html.IndexOf("<"+Vhead+">",w);
            w=(t>=0)?html.IndexOf("<"+Vhead+">",t+1):0;
            ver=(w>0)?html.Substring(t+Vhead.Length+2,w-t-Vhead.Length-2):"?.?.?.?";

            t=html.IndexOf("<"+Thead+">",w);
            w=(t>=0)?html.IndexOf("<"+Thead+">",t+1):0;
            lasttime=(w>0)?html.Substring(t+Thead.Length+2,w-t-Thead.Length-2):"????-??-?? ??:??";
            
            
            t=html.IndexOf("<"+Dhead+">",w);
            w=(t>=0)?html.IndexOf("<"+Dhead+">",t+1):0;
            downloadURL=(w>0)?html.Substring(t+Dhead.Length+2,w-t-Dhead.Length-2):"";
            
            if(!string.IsNullOrEmpty(downloadURL))
            {
                downloadURL=downloadURL.Replace(" http://","\nhttp://");
                Clipboard.SetText(downloadURL);
            }
            MyMsg.Show("当前版本："+Application.ProductVersion
                       +"\n最新版本："+ver
                       +"\n最后更新时间："+lasttime
                       +"\n下载地址:(已经复制到粘贴板)\n"+downloadURL
                      );
            
            return false;
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
