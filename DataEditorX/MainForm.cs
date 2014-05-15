/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月12 星期一
 * 时间: 12:00
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditorX
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		string m_filename;
		string m_title;
		public MainForm(string filename)
		{
			InitFromData();
			OpenFile(filename);
		}
		public MainForm()
		{
			InitFromData();
		}
		void InitFromData()
		{
			InitializeComponent();
			m_title=this.Text;
		}
		void OpenFile(string filename)
		{
			m_filename=filename;
			this.Text=filename+" - "+m_title;
			//
		}
	}
}
