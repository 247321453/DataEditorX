using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditorX
{
	public class DListBox : ListBox
	{
		public DListBox()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer |
		 ControlStyles.AllPaintingInWmPaint,
		 true);
			UpdateStyles();
		}
	}
}
