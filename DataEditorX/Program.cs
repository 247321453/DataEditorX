/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月12 星期一
 * 时间: 12:00
 * 
 */
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DataEditorX.Config;

namespace DataEditorX
{
	internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
            string file = (args.Length > 0) ? args[0] : "";
            if (MyConfig.OpenOnExistForm(file))//在已经存在的窗口打开文件
                Environment.Exit(1);
            else//新建窗口
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm mainForm = new MainForm();
                //设置将要打开的文件
                mainForm.setOpenFile(file);
                //数据目录
                mainForm.SetDataPath(MyPath.Combine(Application.StartupPath, MyConfig.TAG_DATA));

                Application.Run(mainForm);
			}
		} 

	}
}
