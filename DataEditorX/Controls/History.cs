using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DataEditorX.Core;
using DataEditorX.Config;
using System.Windows.Forms;
using DataEditorX.Language;

namespace DataEditorX.Controls
{
    interface IMainForm
    {
        void CdbMenuClear();
        void LuaMenuClear();
        void AddCdbMenu(ToolStripItem item);
        void AddLuaMenu(ToolStripItem item);
        void Open(string file);
    }

    class History
    {
        IMainForm mainForm;
        string historyFile;
        List<string> cdbhistory;
        List<string> luahistory;
        public string[] GetcdbHistory()
        {
            return cdbhistory.ToArray();
        }
        public string[] GetluaHistory()
        {
            return luahistory.ToArray();
        }
        public History(IMainForm mainForm)
        {
            this.mainForm = mainForm;
            cdbhistory = new List<string>();
            luahistory = new List<string>();
        }
        public void ReadHistory(string historyFile)
        {
            if (!File.Exists(historyFile))
                return;
            this.historyFile = historyFile;
            string[] lines = File.ReadAllLines(historyFile);
            AddHistorys(lines);
        }
        void AddHistorys(string[] lines)
        {
            luahistory.Clear();
            cdbhistory.Clear();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;
                if (File.Exists(line))
                {
                    if (YGOUtil.isScript(line))
                    {
                        if (luahistory.Count < MyConfig.MAX_HISTORY
                            && luahistory.IndexOf(line) < 0)
                            luahistory.Add(line);
                    }
                    else
                    {
                        if (cdbhistory.Count < MyConfig.MAX_HISTORY
                            && cdbhistory.IndexOf(line) < 0)
                            cdbhistory.Add(line);
                    }
                }
            }
        }
        public void AddHistory(string file)
        {
            List<string> tmplist = new List<string>();
            //添加到开始
            tmplist.Add(file);
            //添加旧记录
            tmplist.AddRange(cdbhistory.ToArray());
            tmplist.AddRange(luahistory.ToArray());
            //
            AddHistorys(tmplist.ToArray());
            SaveHistory();
            MenuHistory();
        }
        void SaveHistory()
        {
            string texts = "# database history";
            foreach (string str in cdbhistory)
            {
                if (File.Exists(str))
                    texts += Environment.NewLine + str;
            }
            texts += Environment.NewLine + "# script history";
            foreach (string str in luahistory)
            {
                if (File.Exists(str))
                    texts += Environment.NewLine + str;
            }
            File.Delete(historyFile);
            File.WriteAllText(historyFile, texts);
        }
        public void MenuHistory()
        {
            //cdb历史
            mainForm.CdbMenuClear();
            foreach (string str in cdbhistory)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(str);
                tsmi.Click += MenuHistoryItem_Click;
                mainForm.AddCdbMenu(tsmi);
            }
            mainForm.AddCdbMenu(new ToolStripSeparator());
            ToolStripMenuItem tsmiclear = new ToolStripMenuItem(LANG.GetMsg(LMSG.ClearHistory));
            tsmiclear.Click += MenuHistoryClear_Click;
            mainForm.AddCdbMenu(tsmiclear);
            //lua历史
            mainForm.LuaMenuClear();
            foreach (string str in luahistory)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(str);
                tsmi.Click += MenuHistoryItem_Click;
                mainForm.AddLuaMenu(tsmi);
            }
            mainForm.AddLuaMenu(new ToolStripSeparator());
            ToolStripMenuItem tsmiclear2 = new ToolStripMenuItem(LANG.GetMsg(LMSG.ClearHistory));
            tsmiclear2.Click += MenuHistoryClear2_Click;
            mainForm.AddLuaMenu(tsmiclear2);
        }
        void MenuHistoryClear2_Click(object sender, EventArgs e)
        {
            luahistory.Clear();
            MenuHistory();
            SaveHistory();
        }
        void MenuHistoryClear_Click(object sender, EventArgs e)
        {
            cdbhistory.Clear();
            MenuHistory();
            SaveHistory();
        }
        void MenuHistoryItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi != null)
            {
                string file = tsmi.Text;
                mainForm.Open(file);
            }
        }
    }
}
