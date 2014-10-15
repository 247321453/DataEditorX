/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-15
 * 时间: 15:47
 * 
 */
using System;
using System.Configuration;
using System.IO;
using DataEditorX.Language;

namespace DataEditorX.Core
{
	/// <summary>
	/// Description of MSEConfig.
	/// </summary>
	public class MSEConfig
	{
		public MSEConfig(string path)
		{
			regx_pendulum=ConfigurationManager.AppSettings["mse-pendulum-text"];
			regx_monster=ConfigurationManager.AppSettings["mse-monster-text"];
			if(regx_monster==null)
				regx_monster="(\\s\\S*?)";
			else
				regx_monster=regx_monster.Replace("\\n","\n");
			if(regx_pendulum==null)
				regx_pendulum="(\\s\\S*?)";
			else
				regx_pendulum=regx_pendulum.Replace("\\n","\n");

			head = read(path, "mse-head.txt");
			monster = read(path, "mse-monster.txt");
			pendulum = read(path, "mse-pendulum.txt");
			spelltrap = read(path, "mse-spelltrap.txt");
		}
		string read(string path,string name)
		{
			string tmp=Path.Combine(path, name);
			return File.Exists(tmp)?File.ReadAllText(tmp):"";
		}
		public string regx_pendulum;
		public string regx_monster;
		public string head;
		public string monster;
		public string pendulum;
		public string spelltrap;
	}
}
