/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 7月8 星期二
 * 时间: 9:52
 * 
 */
using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataEditorX.Language
{
    /// <summary>
    /// Description of Language.
    /// </summary>
    public static class LanguageHelper
    {
        static Dictionary<Form, SortedList<string, string>> wordslist;
        static SortedList<LMSG, string> msglist;
        static string SEP="->";
        
        static LanguageHelper()
        {
            wordslist=new Dictionary<Form, SortedList<string, string>>();
            msglist=new SortedList<LMSG, string>();
        }
        
        public static void InitForm(Form fm, string langfile)
        {
            if(!wordslist.ContainsKey(fm))
            {
                wordslist.Add(fm, LoadLanguage(langfile));
            }
        }
        
        /// <summary>
        /// 获取消息文字
        /// </summary>
        /// <param name="lMsg"></param>
        /// <returns></returns>
        public static string GetMsg(LMSG lMsg)
        {
            if(msglist.IndexOfKey(lMsg)>=0)
                return msglist[lMsg];
            return lMsg.ToString();
        }
        
        #region 设置控件信息
        /// <summary>
        /// 设置控件文字
        /// </summary>
        /// <param name="fm"></param>
        public static bool SetLanguage(Form fm)
        {
            if(wordslist.ContainsKey(fm))
            {
                SortedList<string, string> list=wordslist[fm];
                SetText(fm, list);
                return true;
            }
            return false;
        }
        static void SetText(Control c, SortedList<string, string> list)
        {
            if ( c is ListView )
            {
                ListView lv = (ListView)c;
                int i,count=lv.Columns.Count;
                for(i=0;i<count;i++)
                {
                    ColumnHeader cn=lv.Columns[i];
                    string v;
                    list.TryGetValue(lv.Name+i.ToString(), out v);
                    if(!string.IsNullOrEmpty(v))
                        cn.Text = v;
                }
            }
            else if ( c is ToolStrip)
            {
                ToolStrip ms = (ToolStrip)c;
                foreach ( ToolStripItem tsi in ms.Items )
                {
                    SetMenuItem(tsi, list);
                }
            }
            else
            {
                string v;
                list.TryGetValue(c.Name, out v);
                if(!string.IsNullOrEmpty(v))
                    c.Text = v;
            }

            if ( c.Controls.Count > 0 )
            {
                foreach ( Control sc in c.Controls )
                {
                    SetText(sc, list);
                }
            }
            ContextMenuStrip conms=c.ContextMenuStrip;
            if ( conms!=null )
            {
                foreach ( ToolStripItem ts in conms.Items )
                {
                    SetMenuItem(ts, list);
                }
            }
        }
        

        static void SetMenuItem(ToolStripItem tsi, SortedList<string, string> list)
        {
            if ( tsi is ToolStripMenuItem )
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsi;
                string v;
                list.TryGetValue(tsmi.Name, out v);
                if(!string.IsNullOrEmpty(v))
                    tsmi.Text = v;
                if(tsmi.HasDropDownItems)
                {
                    foreach ( ToolStripItem subtsi in tsmi.DropDownItems )
                    {
                        if ( subtsi is ToolStripMenuItem )
                        {
                            ToolStripMenuItem ts2 = (ToolStripMenuItem)subtsi;
                            SetMenuItem(ts2, list);
                        }
                    }
                }
            }
            else if ( tsi is ToolStripLabel )
            {
                ToolStripLabel tlbl=(ToolStripLabel)tsi;
                string v;
                list.TryGetValue(tlbl.Name, out v);
                if(!string.IsNullOrEmpty(v))
                    tlbl.Text = v;
            }
        }

        #endregion
        
        #region 获取控件信息
        /// <summary>
        /// 获取控件名
        /// </summary>
        /// <param name="fm"></param>
        public static void GetLanguage(Form fm)
        {
            SortedList<string, string> list=new SortedList<string, string>();
            GetText(fm, list);
            if(wordslist.ContainsKey(fm))
                wordslist[fm]=list;
            else
                wordslist.Add(fm, list);
        }
        static void GetText(Control c, SortedList<string, string> list)
        {
            if ( c is ListView )
            {
                ListView lv = (ListView)c;
                int i,count=lv.Columns.Count;
                for(i=0;i<count;i++)
                {
                    ColumnHeader cn=lv.Columns[i];
                    if ( list.ContainsKey(lv.Name+i.ToString()) )
                        list[cn.Name]=cn.Text;
                    else
                        list.Add(lv.Name+i.ToString(), cn.Text);
                }
            }
            else if ( c is ToolStrip)
            {
                ToolStrip ms = (ToolStrip)c;
                foreach ( ToolStripItem tsi in ms.Items )
                {
                    GetMenuItem(tsi, list);
                }
            }
            else
            {
                if(list.ContainsKey(c.Name))
                    list[c.Name]=c.Text;
                else
                    list.Add(c.Name, c.Text);
            }

            if ( c.Controls.Count > 0 )
            {
                foreach ( Control sc in c.Controls )
                {
                    GetText(sc, list);
                }
            }
            ContextMenuStrip conms=c.ContextMenuStrip;
            if ( conms!=null )
            {
                foreach ( ToolStripItem ts in conms.Items )
                {
                    GetMenuItem(ts, list);
                }
            }
        }
        

        static void GetMenuItem(ToolStripItem tsi, SortedList<string, string> list)
        {
            if ( tsi is ToolStripMenuItem )
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsi;
                if(list.ContainsKey(tsmi.Name))
                    list[tsi.Name] = tsmi.Text;
                else
                    list.Add(tsi.Name, tsmi.Text);
                if(tsmi.HasDropDownItems)
                {
                    foreach ( ToolStripItem subtsi in tsmi.DropDownItems )
                    {
                        if ( subtsi is ToolStripMenuItem )
                        {
                            ToolStripMenuItem ts2 = (ToolStripMenuItem)subtsi;
                            GetMenuItem(ts2, list);
                        }
                    }
                }
            }
            else if ( tsi is ToolStripLabel )
            {
                ToolStripLabel tlbl=(ToolStripLabel)tsi;
                if(list.ContainsKey(tlbl.Name))
                    list[tlbl.Name] = tlbl.Text;
                else
                    list.Add(tlbl.Name, tlbl.Text);
            }
        }
        #endregion
        
        #region 保存语言文件
        public static bool SaveLanguage(Form fm, string f)
        {
            if(!wordslist.ContainsKey(fm))
                return false;
            SortedList<string, string> fmlist=wordslist[fm];
            using(FileStream fs=new FileStream(f, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw=new StreamWriter(fs, Encoding.UTF8);
                foreach(string k in fmlist.Keys)
                {
                    sw.WriteLine(fm.Name+SEP+k+" "+fmlist[k]);
                }
                sw.Close();
                fs.Close();
            }
            return true;
        }
        
        public static bool SaveMessage(string f)
        {
            using(FileStream fs=new FileStream(f, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw=new StreamWriter(fs, Encoding.UTF8);
                foreach(LMSG k in msglist.Keys)
                {
                    sw.WriteLine("0x"+((uint)k).ToString("x")+" "+msglist[k].Replace("\n","/n"));
                }
                foreach ( LMSG k in Enum.GetValues(typeof(LMSG)))
                {
                    if(!msglist.ContainsKey(k))
                        sw.WriteLine("0x"+((uint)k).ToString("x")+" "+k.ToString());
                }
                sw.Close();
                fs.Close();
            }
            return true;
        }
        #endregion
        
        #region 加载语言文件
        public static SortedList<string, string> LoadLanguage(string f)
        {
            SortedList<string, string> list=new SortedList<string, string>();
            if(File.Exists(f))
            {
                using(FileStream fs=new FileStream(f, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr=new StreamReader(fs, Encoding.UTF8);
                    string line,sk,v;
                    while((line=sr.ReadLine())!=null)
                    {
                        if(!line.StartsWith("#"))
                        {
                            int ss=line.IndexOf(SEP);
                            int si=(ss>0)?line.IndexOf(" "):-1;
                            if(si>0)
                            {
                                sk=line.Substring(ss+SEP.Length,si-ss-SEP.Length);
                                v=line.Substring(si+1);

                                if(!list.ContainsKey(sk))
                                    list.Add(sk,v);
                            }
                        }
                    }
                    sr.Close();
                    fs.Close();
                }
            }
            return list;
        }
        
        
        public static void LoadMessage(string f)
        {
            if(File.Exists(f))
            {
                msglist.Clear();
                using(FileStream fs=new FileStream(f, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr=new StreamReader(fs, Encoding.UTF8);
                    string line,sk,v;
                    uint utemp;
                    LMSG ltemp;
                    while((line=sr.ReadLine())!=null)
                    {
                        if(!line.StartsWith("#"))
                        {
                            int si=line.IndexOf(" ");
                            if(si>0)
                            {
                                sk=line.Substring(0,si);
                                v=line.Substring(si+1);
                                if(sk.StartsWith("0x"))
                                    uint.TryParse(sk.Replace("0x",""), NumberStyles.HexNumber, null, out utemp);
                                else
                                    uint.TryParse(sk, out utemp);
                                ltemp=(LMSG)utemp;
                                if(msglist.IndexOfKey(ltemp)<0)
                                    msglist.Add(ltemp, v.Replace("/n","\n"));
                            }
                        }
                    }
                    sr.Close();
                    fs.Close();
                }
            }
        }
        #endregion
    }

}
