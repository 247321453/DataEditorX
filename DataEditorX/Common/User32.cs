﻿﻿/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-25
 * 时间: 21:30
 * 
 */
using System;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Description of User32.
    /// </summary>
    public class User32
    {
        /// <summary>
        /// 发送窗口消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int msg, uint wParam, uint lParam);
        /// <summary>
        /// 设置窗口的位置
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        /// <summary>
        /// 得到当前活动的窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern System.IntPtr GetForegroundWindow();
    }
}