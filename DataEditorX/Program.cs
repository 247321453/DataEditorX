/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月12 星期一
 * 时间: 12:00
 * 
 */
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace DataEditorX
{
	
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		private static extern int SendMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);
		/// <summary>
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			string arg="";
			if(args.Length>0)
			{
				arg=args[0];
			}
			Process instance = RunningInstance();
			if (instance == null)
			{
				ShowForm(arg);
			}
			else
			{
				File.WriteAllText(Path.Combine(Application.StartupPath, MainForm.TMPFILE), arg);
				SendMessage(instance.MainWindowHandle, MainForm.WM_OPEN, 0 ,0);
				//Thread.Sleep(1000);
				Environment.Exit(1);
			}
		}
		private static Process RunningInstance()
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
		static void ShowForm(string file)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			string dir=ConfigurationManager.AppSettings["language"];
			if(string.IsNullOrEmpty(dir))
			{
				Application.Exit();
			}
			string datapath=Path.Combine(Application.StartupPath, dir);
			
			Application.Run(new MainForm(datapath, file));
		}
	}
}
