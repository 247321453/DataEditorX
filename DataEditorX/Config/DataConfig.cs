/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-23
 * 时间: 7:54
 * 
 */
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DataEditorX.Config
{
    /// <summary>
    /// DataEditor的数据
    /// </summary>
    public class DataConfig
    {
        public DataConfig()
        {
            InitMember(MyPath.Combine(Application.StartupPath, MyConfig.TAG_CARDINFO+".txt"));
        }
        public DataConfig(string conf)
        {
            InitMember(conf);
        }
        /// <summary>
        /// 初始化成员
        /// </summary>
        /// <param name="conf"></param>
        public void InitMember(string conf)
        {
            //conf = MyPath.Combine(datapath, MyConfig.FILE_INFO);
            if(!File.Exists(conf))
            {
                dicCardRules = new SortedList<long, string>();
                dicSetnames = new SortedList<long, string>();
                dicCardTypes = new SortedList<long, string>();
                dicCardcategorys = new SortedList<long, string>();
                dicCardAttributes = new SortedList<long, string>();
                dicCardRaces = new SortedList<long, string>();
                dicCardLevels = new SortedList<long, string>();
                return;
            }
            //提取内容
            string text = File.ReadAllText(conf);
            dicCardRules = DataManager.Read(text, MyConfig.TAG_RULE);
            dicSetnames = DataManager.Read(text, MyConfig.TAG_SETNAME);
            dicCardTypes = DataManager.Read(text, MyConfig.TAG_TYPE);
            dicCardcategorys = DataManager.Read(text, MyConfig.TAG_CATEGORY);
            dicCardAttributes = DataManager.Read(text, MyConfig.TAG_ATTRIBUTE);
            dicCardRaces = DataManager.Read(text, MyConfig.TAG_RACE);
            dicCardLevels = DataManager.Read(text, MyConfig.TAG_LEVEL);

		}
        /// <summary>
        /// 规则
        /// </summary>
        public SortedList<long, string> dicCardRules = null;
        /// <summary>
        /// 属性
        /// </summary>
        public SortedList<long, string> dicCardAttributes = null;
        /// <summary>
        /// 种族
        /// </summary>
        public SortedList<long, string> dicCardRaces = null;
        /// <summary>
        /// 等级
        /// </summary>
        public SortedList<long, string> dicCardLevels = null;
        /// <summary>
        /// 系列名
        /// </summary>
        public SortedList<long, string> dicSetnames = null;
        /// <summary>
        /// 卡片类型
        /// </summary>
        public SortedList<long, string> dicCardTypes = null;
        /// <summary>
        /// 效果类型
        /// </summary>
        public SortedList<long, string> dicCardcategorys = null;
    }
}
