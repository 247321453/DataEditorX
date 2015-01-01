using System;
using System.Xml;
using System.IO;
using DataEditorX.Common;
using System.Windows.Forms;

namespace DataEditorX.Config
{
    /// <summary>
    /// 配置
    /// </summary>
    public class MyConfig
    {
        #region 常量
        /// <summary>
        /// 窗口消息 打开文件
        /// </summary>
        public const int WM_OPEN = 0x0401;
        /// <summary>
        /// 最大历史数量
        /// </summary>
        public const int MAX_HISTORY = 0x10;
        /// <summary>
        /// 数据目录
        /// </summary>
        public const string TAG_DATA = "data";
        /// <summary>
        /// 将要打开
        /// </summary>
        //public const string TAG_OPEN = "open";
        /// <summary>
        /// MSE
        /// </summary>
        public const string TAG_MSE = "mse";
        /// <summary>
        /// 卡片信息
        /// </summary>
        public const string TAG_CARDINFO = "cardinfo";
        /// <summary>
        /// 语言
        /// </summary>
        public const string TAG_LANGUAGE = "language";
        /// <summary>
        /// 临时文件
        /// </summary>
        public const string FILE_TEMP = "open.tmp";
        /// <summary>
        /// 历史记录
        /// </summary>
        public const string FILE_HISTORY = "history.txt";
        /// <summary>
        /// 函数
        /// </summary>
        public const string FILE_FUNCTION = "_functions.txt";
        /// <summary>
        /// 常量
        /// </summary>
        public const string FILE_CONSTANT = "constant.lua";
        /// <summary>
        /// 指示物，胜利提示
        /// </summary>
        public const string FILE_STRINGS = "strings.conf";
        /// <summary>
        /// 源码链接
        /// </summary>
        public const string TAG_SOURCE_URL = "sourceURL";
        /// <summary>
        /// 升级链接
        /// </summary>
        public const string TAG_UPDATE_URL = "updateURL";
        /// <summary>
        /// 一般的裁剪
        /// </summary>
        public const string TAG_IMAGE_OTHER = "image_other";
        /// <summary>
        /// xyz的裁剪
        /// </summary>
        public const string TAG_IMAGE_XYZ = "image_xyz";
        /// <summary>
        /// Pendulum的裁剪
        /// </summary>
        public const string TAG_IMAGE_PENDULUM = "image_pendulum";
        /// <summary>
        /// 图片的宽高，小图w,h大图W,H，共4个
        /// </summary>
        public const string TAG_IMAGE_SIZE = "image";
        /// <summary>
        /// 图片质量
        /// </summary>
        public const string TAG_IMAGE_QUILTY = "image_quilty";
        //CodeEditor
        /// <summary>
        /// 字体名
        /// </summary>
        public const string TAG_FONT_NAME = "fontname";
        /// <summary>
        /// 字体大小
        /// </summary>
        public const string TAG_FONT_SIZE = "fontsize";
        /// <summary>
        /// 支持中文
        /// </summary>
        public const string TAG_IME = "IME";
        /// <summary>
        /// 自动换行
        /// </summary>
        public const string TAG_WORDWRAP = "wordwrap";
        /// <summary>
        /// tab替换为空格
        /// </summary>
        public const string TAG_TAB2SPACES = "tabisspace";
        /// <summary>
        /// 规则
        /// </summary>
        public const string TAG_RULE = "rule";
        /// <summary>
        /// 种族
        /// </summary>
        public const string TAG_RACE = "race";
        /// <summary>
        /// 属性
        /// </summary>
        public const string TAG_ATTRIBUTE = "attribute";
        /// <summary>
        /// 等级
        /// </summary>
        public const string TAG_LEVEL = "level";
        /// <summary>
        /// 效果分类
        /// </summary>
        public const string TAG_CATEGORY = "category";
        /// <summary>
        /// 类型
        /// </summary>
        public const string TAG_TYPE = "type";
        /// <summary>
        /// 系列名
        /// </summary>
        public const string TAG_SETNAME = "setname";
        #endregion
        
        #region 读取内容
        /// <summary>
        /// 读取字符串值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string readString(string key)
        {
            return GetAppConfig(key);
        }
        /// <summary>
        /// 读取int值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int readInteger(string key, int def)
        {
            int i;
            if (int.TryParse(readString(key), out i))
                return i;
            return def;
        }
        /// <summary>
        /// 读取float值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static float readFloat(string key, float def)
        {
            float i;
            if (float.TryParse(readString(key), out i))
                return i;
            return def;
        }
        /// <summary>
        /// 读取int数组
        /// </summary>
        /// <param name="key"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int[] readIntegers(string key, int length)
        {
            string temp = readString(key);
            int[] ints = new int[length];
            string[] ws = string.IsNullOrEmpty(temp) ? null : temp.Split(',');

            if (ws != null && ws.Length > 0 && ws.Length <= length)
            {
                for (int i = 0; i < ws.Length; i++)
                {
                    int.TryParse(ws[i], out ints[i]);
                }
            }
            return ints;
        }
        /// <summary>
        /// 读取区域
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Area readArea(string key)
        {
            int[] ints = readIntegers(key, 4);
            Area a = new Area();
            if (ints != null)
            {
                a.left = ints[0];
                a.top = ints[1];
                a.width = ints[2];
                a.height = ints[3];
            }
            return a;
        }
        /// <summary>
        /// 读取boolean
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool readBoolean(string key)
        {
            if (readString(key).ToLower() == "true")
                return true;
            else
                return false;
        }
        #endregion 
        
        #region XML操作config
        /// <summary>
        /// 保存值
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appValue"></param>
        public static void Save(string appKey, string appValue)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode xNode = xDoc.SelectSingleNode("//appSettings");

            XmlElement xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
            if (xElem != null) //存在，则更新
                xElem.SetAttribute("value", appValue);
            else//不存在，则插入
            {
                XmlElement xNewElem = xDoc.CreateElement("add");
                xNewElem.SetAttribute("key", appKey);
                xNewElem.SetAttribute("value", appValue);
                xNode.AppendChild(xNewElem);
            }
            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode xNode = xDoc.SelectSingleNode("//appSettings");

            XmlElement xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");

            if (xElem != null)
            {
                return xElem.Attributes["value"].Value;
            }
            return string.Empty;
        }
        #endregion
        
        /// <summary>
        /// 语言配置文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetLanguageFile(string path)
        {
            return MyPath.Combine(path, MyPath.getFileName(MyConfig.TAG_LANGUAGE, GetAppConfig(TAG_LANGUAGE)));
        }
        /// <summary>
        /// 卡片信息配置文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetCardInfoFile(string path)
        {
            return MyPath.Combine(path,  MyPath.getFileName(MyConfig.TAG_CARDINFO, GetAppConfig(TAG_LANGUAGE)));
        }
        /// <summary>
        /// 发送消息打开文件
        /// </summary>
        /// <param name="file"></param>
        public static void Open(IntPtr win, string file)
        {
            //把需要打开的文件写入临时文件
            string tmpfile = Path.Combine(Application.StartupPath, MyConfig.FILE_TEMP);
            File.WriteAllText(tmpfile, file);
            //发送消息
            User32.SendMessage(win, MyConfig.WM_OPEN, 0, 0);
            Environment.Exit(1);
        }
    }

}
