/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-23
 * 时间: 7:54
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace DataEditorX.Core
{
	/// <summary>
	/// Description of DataConfig.
	/// </summary>
	public class DataConfig
	{
		public DataConfig()
		{
            InitMember(System.Windows.Forms.Application.StartupPath);
		}
		public DataConfig(string datapath)
		{
            InitMember(datapath);
		}
		void InitMember(string datapath)
        {
            this.datapath = datapath;
            confrule = Path.Combine(datapath, "card-rule.txt");
            confattribute = Path.Combine(datapath, "card-attribute.txt");
            confrace = Path.Combine(datapath, "card-race.txt");
            conflevel = Path.Combine(datapath, "card-level.txt");
            confsetname = Path.Combine(datapath, "card-setname.txt");
            conftype = Path.Combine(datapath, "card-type.txt");
            confcategory = Path.Combine(datapath, "card-category.txt");

			dicCardRules=new Dictionary<long, string>();
			dicSetnames=new Dictionary<long, string>();
			dicCardTypes=new Dictionary<long, string>();
			dicCardcategorys=new Dictionary<long, string>();
			dicCardAttributes=new Dictionary<long, string>();
			dicCardRaces=new Dictionary<long, string>();
			dicCardLevels=new Dictionary<long, string>();
		}
		Dictionary<long, string> CopyDic(Dictionary<long, string> dic)
		{
			Dictionary<long, string> ndic=new Dictionary<long, string>();
			foreach(long k in dic.Keys)
			{
				ndic.Add(k, dic[k]);
			}
			return ndic;
		}
		public void Init()
		{
			dicCardRules=DataManager.Read(confrule);
			dicSetnames=DataManager.Read(confsetname);
			dicCardTypes=DataManager.Read(conftype);
			dicCardcategorys=DataManager.Read(confcategory);
			dicCardAttributes=DataManager.Read(confattribute);
			dicCardRaces=DataManager.Read(confrace);
			dicCardLevels=DataManager.Read(conflevel);
		}
        string datapath;
		public string confrule, confattribute, confrace, conflevel;
		public string confsetname, conftype, confcategory;

		public Dictionary<long, string> dicCardRules=null;
		public Dictionary<long, string> dicCardAttributes=null;
		public Dictionary<long, string> dicCardRaces=null;
		public Dictionary<long, string> dicCardLevels=null;
		public Dictionary<long, string> dicSetnames=null;
		public Dictionary<long, string> dicCardTypes=null;
		public Dictionary<long, string> dicCardcategorys=null;
	}
}
