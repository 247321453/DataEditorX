/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-15
 * 时间: 15:47
 * 
 */
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataEditorX.Language;
using System.Globalization;
using DataEditorX.Common;
using DataEditorX.Config;

namespace DataEditorX.Core.Mse
{
	/// <summary>
	/// Description of MSEConfig.
	/// </summary>
	public class MSEConfig
	{
		#region  常量
		public const string TAG = "mse";
		/// <summary>存档头部</summary>
		public const string TAG_HEAD = "head";
		/// <summary>存档尾部</summary>
		public const string TAG_END = "end";
		/// <summary>简体转繁体</summary>
		public const string TAG_CN2TW = "cn2tw";
		/// <summary>魔法标志格式</summary>
		public const string TAG_SPELL = "spell";
		/// <summary>陷阱标志格式</summary>
		public const string TAG_TRAP = "trap";
		public const string TAG_REG_PENDULUM = "pendulum-text";
		public const string TAG_REG_MONSTER = "monster-text";
		public const string TAG_MAXCOUNT = "maxcount";
		public const string TAG_RACE = "race";
		public const string TAG_TYPE = "type";
		public const string TAG_WIDTH="width";
		public const string TAG_HEIGHT="height";
		
		
		public const string TAG_PEND_WIDTH="pwidth";
		public const string TAG_PEND_HEIGHT="pheight";

		public const string TAG_IMAGE = "imagepath";
		public const string TAG_REPALCE = "replace";
		public const string TAG_TEXT = "text";

		public const string TAG_REP = "%%";
		public const string SEP_LINE = " ";
		//默认的配置
		public const string FILE_CONFIG_NAME = "Chinese-Simplified";
		public const string PATH_IMAGE = "Images";
		public string configName = FILE_CONFIG_NAME;
		#endregion
		public MSEConfig(string path)
		{
			init(path);
		}
		public void SetConfig(string config, string path)
		{
			if (!File.Exists(config))
				return;
			regx_monster = "(\\s\\S*?)";
			regx_pendulum = "(\\s\\S*?)";
			//设置文件名
			configName = MyPath.getFullFileName(MSEConfig.TAG, config);

			replaces = new SortedList<string, string>();

			typeDic = new SortedList<long, string>();
			raceDic = new SortedList<long, string>();
			string[] lines = File.ReadAllLines(config, Encoding.UTF8);
			foreach (string line in lines)
			{
				if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
					continue;
				if (line.StartsWith(TAG_CN2TW))
					Iscn2tw = ConfHelper.getBooleanValue(line);
				else if (line.StartsWith(TAG_SPELL))
					str_spell = ConfHelper.getValue(line);
				else if (line.StartsWith(TAG_HEAD))
					head = ConfHelper.getMultLineValue(line);
				else if (line.StartsWith(TAG_END))
					end = ConfHelper.getMultLineValue(line);
				else if (line.StartsWith(TAG_TEXT))
					temp_text = ConfHelper.getMultLineValue(line);
				else if (line.StartsWith(TAG_TRAP))
					str_trap = ConfHelper.getValue(line);
				else if (line.StartsWith(TAG_REG_PENDULUM))
					regx_pendulum = ConfHelper.getValue(line);
				else if (line.StartsWith(TAG_REG_MONSTER))
					regx_monster = ConfHelper.getValue(line);
				else if (line.StartsWith(TAG_MAXCOUNT))
					maxcount = ConfHelper.getIntegerValue(line, 0);
				else if (line.StartsWith(TAG_WIDTH)){
					width=ConfHelper.getIntegerValue(line,0);
				}
				else if (line.StartsWith(TAG_HEIGHT)){
					height=ConfHelper.getIntegerValue(line,0);
				}
				else if (line.StartsWith(TAG_PEND_WIDTH)){
					pwidth=ConfHelper.getIntegerValue(line,0);
				}
				else if (line.StartsWith(TAG_PEND_HEIGHT)){
					pheight=ConfHelper.getIntegerValue(line,0);
				}
				else if (line.StartsWith(TAG_IMAGE))
				{
					//如果路径不合法，则为后面的路径
					imagepath = MyPath.CheckDir(ConfHelper.getValue(line), MyPath.Combine(path, PATH_IMAGE));
					//图片缓存目录
					imagecache = MyPath.Combine(imagepath, "cache");
					MyPath.CreateDir(imagecache);
				}
				else if (line.StartsWith(TAG_REPALCE))
				{//特数字替换
					string word = ConfHelper.getValue(line);
					string p = ConfHelper.getRegex(ConfHelper.getValue1(word));
					string r = ConfHelper.getRegex(ConfHelper.getValue2(word));
					if (!string.IsNullOrEmpty(p))
						replaces.Add(p, r);

				}
				else if (line.StartsWith(TAG_RACE))
				{//种族
					ConfHelper.DicAdd(raceDic, line);
				}
				else if (line.StartsWith(TAG_TYPE))
				{//类型
					ConfHelper.DicAdd(typeDic, line);
				}
			}
		}
		public void init(string path)
		{
			Iscn2tw = false;
			
			//读取配置
			string tmp = MyPath.Combine(path, MyPath.getFileName(MSEConfig.TAG, MyConfig.readString(MyConfig.TAG_MSE)));
			
			if (!File.Exists(tmp))
			{
				tmp = MyPath.Combine(path, MyPath.getFileName(MSEConfig.TAG, FILE_CONFIG_NAME));
				if(!File.Exists(tmp))
					return;//如果默认的也不存在
			}
			SetConfig(tmp, path);
		}
		/// <summary>
		/// 中间图宽度
		/// </summary>
		public int width;
		/// <summary>
		/// 中间图高度
		/// </summary>
		public int height;
		
		public int pwidth;
		public int pheight;
		
		//每个存档最大数
		public int maxcount;
		//图片路径
		public string imagepath;
		/// <summary>
		/// 图片缓存路径
		/// </summary>
		public string imagecache;
		//魔法标志
		public string str_spell;
		//陷阱标志
		public string str_trap;
		//效果格式
		public string temp_text;
		//简体转繁体？
		public bool Iscn2tw;
		//特数字替换
		public SortedList<string, string> replaces;
		//效果文正则提取
		public string regx_pendulum;
		public string regx_monster;
		//存档头部
		public string head;
		//存档结尾
		public string end;
		public SortedList<long, string> typeDic;
		public SortedList<long, string> raceDic;
	}
}
