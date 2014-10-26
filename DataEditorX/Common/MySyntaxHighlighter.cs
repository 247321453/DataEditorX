/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-23
 * 时间: 23:14
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace FastColoredTextBoxNS
{
	/// <summary>
	/// Description of FastColoredTextBoxEx.
	/// </summary>
	public class MySyntaxHighlighter : SyntaxHighlighter
	{
		TextStyle mBoldStyle = new TextStyle(Brushes.MediumSlateBlue, null, FontStyle.Regular);
		TextStyle mNumberStyle = new TextStyle(Brushes.Orange, null, FontStyle.Regular);
		TextStyle mStrStyle = new TextStyle(Brushes.Gold, null, FontStyle.Regular);
		TextStyle ConStyle = new TextStyle(Brushes.YellowGreen, null, FontStyle.Regular);
		TextStyle mKeywordStyle = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
		TextStyle mGrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
		TextStyle mFunStyle = new TextStyle(Brushes.DeepSkyBlue, null, FontStyle.Regular);
		/// <summary>
		/// Highlights Lua code
		/// </summary>
		/// <param name="range"></param>
		public override void LuaSyntaxHighlight(Range range)
		{
			range.tb.CommentPrefix = "--";
			range.tb.LeftBracket = '(';
			range.tb.RightBracket = ')';
			range.tb.LeftBracket2 = '{';
			range.tb.RightBracket2 = '}';
			range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;

			range.tb.AutoIndentCharsPatterns
				= @"^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>.+)";

			//clear style of changed range
			range.ClearStyle(mStrStyle, mGrayStyle, ConStyle, mNumberStyle, mKeywordStyle, mFunStyle);
			//
			if (base.LuaStringRegex == null)
				base.InitLuaRegex();
			//string highlighting
			range.SetStyle(mStrStyle, base.LuaStringRegex);
			//comment highlighting
			range.SetStyle(mGrayStyle, base.LuaCommentRegex1);
			range.SetStyle(mGrayStyle, base.LuaCommentRegex2);
			range.SetStyle(mGrayStyle, base.LuaCommentRegex3);
			//number highlighting
			range.SetStyle(mNumberStyle, base.LuaNumberRegex);
			range.SetStyle(mNumberStyle, @"\bc\d+\b");
			//keyword highlighting
			range.SetStyle(mKeywordStyle, base.LuaKeywordRegex);
			//functions highlighting
			range.SetStyle(mFunStyle, base.LuaFunctionsRegex);
			
			//range.SetStyle(mBoldStyle, @"\b(?<range>[a-zA-Z0-9_]+?)[.|:|=|\s]");
			//constant
			range.SetStyle(ConStyle, @"[\s|\(|+|,]{0,1}(?<range>[A-Z_]+?)[\)|+|\s|,]");
			//function
			//range.SetStyle(FunStyle, @"[:|.|\s](?<range>[^\(]*?)[\(|\)|\s]");
			
			
			//clear folding markers
			range.ClearFoldingMarkers();
			//set folding markers
			range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
			range.SetFoldingMarkers(@"--\[\[", @"\]\]"); //allow to collapse comment block
		}
	}
}
