/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-24
 * 时间: 7:19
 * 
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FastColoredTextBoxNS
{
	public class FastColoredTextBoxEx : FastColoredTextBox
	{
		Point lastMouseCoord;
        string mFile;
		public FastColoredTextBoxEx() : base()
		{
            this.SyntaxHighlighter = new MySyntaxHighlighter();
		}
		public new event EventHandler<ToolTipNeededEventArgs> ToolTipNeeded;
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			lastMouseCoord = e.Location;
		}
		
		protected override void OnToolTip()
		{
			if (ToolTip == null)
				return;
			if (ToolTipNeeded == null)
				return;

			//get place under mouse
			Place place = PointToPlace(lastMouseCoord);

			//check distance
			Point p = PlaceToPoint(place);
			if (Math.Abs(p.X - lastMouseCoord.X) > CharWidth*2 ||
			    Math.Abs(p.Y - lastMouseCoord.Y) > CharHeight*2)
				return;
			//get word under mouse
			var r = new Range(this, place, place);
			string hoveredWord = r.GetFragment("[a-zA-Z0-9_]").Text;
			//event handler
			var ea = new ToolTipNeededEventArgs(place, hoveredWord);
			ToolTipNeeded(this, ea);

			if (ea.ToolTipText != null)
			{
				//show tooltip
				ToolTip.ToolTipTitle = ea.ToolTipTitle;
				ToolTip.ToolTipIcon = ea.ToolTipIcon;
				//ToolTip.SetToolTip(this, ea.ToolTipText);
				ToolTip.Show(ea.ToolTipText, this, new Point(lastMouseCoord.X, lastMouseCoord.Y + CharHeight));
			}
		}
	}
}
