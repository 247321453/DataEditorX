/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月12 星期一
 * 时间: 12:00
 * 
 */
using System;
using System.Windows.Forms;

namespace DataEditorX
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length==1)
            {
                if(args[0]=="update")//进入更新模式
                {
                    Application.Run(new UpdateForm(Application.ProductVersion));
                }
                else if(args[0]=="updated")//更新完成，删除更新文件
                {
                    if(args.Length>1)
                        System.IO.File.Delete(args[1]);
                    Application.Run(new DataEditForm());
                }
                else
                    Application.Run(new DataEditForm(args[0]));
            }
            else
                Application.Run(new DataEditForm());
        }     
    }
}
