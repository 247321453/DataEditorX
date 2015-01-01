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
			Process instance = RunningInstance();
            //判断是否已经运行
			if (instance == null)
			{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm mainForm = new MainForm();
                //数据目录
                mainForm.SetDataPath(MyPath.Combine(Application.StartupPath, MyConfig.TAG_DATA));

                mainForm.Open(file);
                Application.Run(mainForm);
			}
			else
			{
                //把需要打开的文件写入临时文件
                //string tmpfile = Path.Combine(Application.StartupPath, MyConfig.FILE_TEMP);
                //File.WriteAllText(tmpfile, file);
                //发送消息
                //User32.SendMessage(instance.MainWindowHandle, MyConfig.WM_OPEN, 0, 0);
                MyConfig.Open(instance.MainWindowHandle, file);
                Environment.Exit(1);
			}
		} 
		static Process RunningInstance()
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
					if (Assembly.GetExecutingAssembly().Location.
					    Replace('/', Path.DirectorySeparatorChar)
					    == current.MainModule.FileName)
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
