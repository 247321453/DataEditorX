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
        public const string FILE_INFO = "card-info.txt";
        public const string TAG_RULE = "rule";
        public const string TAG_RACE = "race";
        public const string TAG_ATTRIBUTE = "attribute";
        public const string TAG_LEVEL = "level";
        public const string TAG_CATEGORY = "category";
        public const string TAG_TYPE = "type";
        public const string TAG_SETNAME = "setname";

        public MSEConfig msecfg;
        public DataConfig()
        {
            InitMember(MyPath.Combine(Application.StartupPath, FILE_INFO)
                , Application.StartupPath);
        }
        public DataConfig(string conf, string datapath)
        {
            InitMember(conf, datapath);
        }
        public void InitMember(string conf, string datapath)
        {
            msecfg = new MSEConfig(datapath);
            //conf = MyPath.Combine(datapath, MyConfig.FILE_INFO);
            if(!File.Exists(conf))
            {
                dicCardRules = new Dictionary<long, string>();
			    dicSetnames =new Dictionary<long, string>();
			    dicCardTypes =new Dictionary<long, string>();
			    dicCardcategorys =new Dictionary<long, string>();
			    dicCardAttributes =new Dictionary<long, string>();
			    dicCardRaces =new Dictionary<long, string>();
			    dicCardLevels =new Dictionary<long, string>();
                return;
            }
            //提取内容
            string text = File.ReadAllText(conf);
            dicCardRules = DataManager.Read(text, TAG_RULE);
            dicSetnames = DataManager.Read(text, TAG_SETNAME);
            dicCardTypes = DataManager.Read(text, TAG_TYPE);
            dicCardcategorys = DataManager.Read(text, TAG_CATEGORY);
            dicCardAttributes = DataManager.Read(text, TAG_ATTRIBUTE);
            dicCardRaces = DataManager.Read(text, TAG_RACE);
            dicCardLevels = DataManager.Read(text, TAG_LEVEL);

		}

        public Dictionary<long, string> dicCardRules = null;
        public Dictionary<long, string> dicCardAttributes = null;
        public Dictionary<long, string> dicCardRaces = null;
        public Dictionary<long, string> dicCardLevels = null;
        public Dictionary<long, string> dicSetnames = null;
        public Dictionary<long, string> dicCardTypes = null;
        public Dictionary<long, string> dicCardcategorys = null;
    }
}
