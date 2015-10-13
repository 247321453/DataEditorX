using System;
using System.Xml;
using System.IO;
using System.Globalization;
using DataEditorX.Common;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace DataEditorX.Config
{
    /// <summary>
    /// 配置
    /// </summary>
    public class MyConfig : XMLReader
    {
        #region 常量
        public const string TAG_SAVE_LAGN = "-savelanguage";
        public const string TAG_SAVE_LAGN2 = "-sl";
        public const string TAG_MSE_PATH="mse_path";
        public const string TAG_MSE_EXPORT="mse_exprotpath";
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
        /// 删除卡片的时候，删除图片脚本
        /// </summary>
        public const string TAG_DELETE_WITH = "opera_with_cards_file";
        /// <summary>
        /// 异步加载数据
        /// </summary>
        public const string TAG_ASYNC = "async";
        /// <summary>
        /// 用本程序打开文件
        /// </summary>
        public const string TAG_OPEN_IN_THIS = "open_file_in_this";
		/// <summary>
		/// 自动检查更新
		/// </summary>
		public const string TAG_AUTO_CHECK_UPDATE = "auto_check_update";
		/// <summary>
		/// 检查系统语言
		/// </summary>
		public const string TAG_CHECK_SYSLANG = "check_system_language";
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

        
        /// <summary>
        /// 语言配置文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetLanguageFile(string path)
        {
			if (readBoolean(TAG_CHECK_SYSLANG) && Directory.Exists(path))
			{
				Save(TAG_CHECK_SYSLANG, "false");
				string[] words = CultureInfo.InstalledUICulture.EnglishName.Split(' ');
				string syslang = words[0];
				string[] files = Directory.GetFiles(path);
				foreach (string file in files)
				{
					string name = MyPath.getFullFileName(MyConfig.TAG_LANGUAGE, file);
					if (string.IsNullOrEmpty(name))
						continue;
					if (syslang.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						Save(MyConfig.TAG_LANGUAGE, syslang);
						break;
					}
				}
			}
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
        public static bool OpenOnExistForm(string file)
        {
            Process instance = RunningInstance(Assembly.GetExecutingAssembly().Location.
                        Replace('/', Path.DirectorySeparatorChar));
            if (instance == null)
            {
                return false;
            }
            else
            {
                //把需要打开的文件写入临时文件
                string tmpfile = Path.Combine(Application.StartupPath, MyConfig.FILE_TEMP);
                File.WriteAllText(tmpfile, file);
                //发送消息
                User32.SendMessage(instance.MainWindowHandle, MyConfig.WM_OPEN, 0, 0);
                return true;
            }
        }
        public static void OpenFileInThis(string file)
        {
            //把需要打开的文件写入临时文件
            string tmpfile = Path.Combine(Application.StartupPath, MyConfig.FILE_TEMP);
            File.WriteAllText(tmpfile, file);
            //发送消息
            User32.SendMessage(Process.GetCurrentProcess().MainWindowHandle, MyConfig.WM_OPEN, 0, 0);
        }
        public static Process RunningInstance(string filename)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (filename == current.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }
    }

}
