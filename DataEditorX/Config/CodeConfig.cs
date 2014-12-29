using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using WeifenLuo.WinFormsUI.Docking;

using FastColoredTextBoxNS;
using DataEditorX.Language;
using DataEditorX.Core;
using System.Text;

namespace DataEditorX.Core
{
    class CodeConfig
    {
        public CodeConfig(string datapath)
        {
            funtxt = MyPath.Combine(datapath, "_functions.txt");
            conlua = MyPath.Combine(datapath, "constant.lua");
            confstring = MyPath.Combine(datapath, "strings.conf");
            tooltipDic = new Dictionary<string, string>();
            funList = new List<AutocompleteItem>();
            conList = new List<AutocompleteItem>();
        }
        //函数提示
        Dictionary<string, string> tooltipDic;
        public Dictionary<string, string> TooltipDic
        {
            get { return tooltipDic; }
        }
        //自动完成
        List<AutocompleteItem> funList;
        List<AutocompleteItem> conList;
        bool isInit;
        public bool IsInit
        {
            get { return isInit; }
        }
        public AutocompleteItem[] FunList
        {
            get { return funList.ToArray(); }
        }
        public AutocompleteItem[] ConList
        {
            get { return conList.ToArray(); }
        }
        public string funtxt, conlua, confstring;

        public void Init()
        {
            isInit = true;
            tooltipDic.Clear();
            funList.Clear();
            conList.Clear();
            AddFunction(funtxt);
            AddConstant(conlua);
        }

        #region setnames strings
        public void SetNames(Dictionary<long, string> dic)
        {
            foreach (long k in dic.Keys)
            {
                string key = "0x" + k.ToString("x");
                if (!tooltipDic.ContainsKey(key))
                {
                    AddConToolTip(key, dic[k]);
                }
            }
        }
        public void AddStrings(string str)
        {
            if (File.Exists(str))
            {
                string[] lines = File.ReadAllLines(str);
                foreach (string line in lines)
                {
                    if (line.StartsWith("!victory")
                       || line.StartsWith("!counter"))
                    {
                        string[] ws = line.Split(' ');
                        if (ws.Length > 2)
                        {
                            AddConToolTip(ws[1], ws[2]);
                        }
                    }
                }
            }
        }

        public void AddStrings()
        {
            AddStrings(confstring);
        }
        #endregion

        #region function
        void AddFunction(string funtxt)
        {
            if (File.Exists(funtxt))
            {
                string[] lines = File.ReadAllLines(funtxt);
                bool isFind = false;
                string name = "";
                string desc = "";
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line)
                       || line.StartsWith("==")
                       || line.StartsWith("#"))
                        continue;
                    if (line.StartsWith("●"))
                    {
                        //add
                        AddFuncTooltip(name, desc);
                        int w = line.IndexOf("(");
                        int t = line.IndexOf(" ");

                        if (t < w && t > 0)
                        {
                            name = line.Substring(t + 1, w - t - 1);
                            isFind = true;
                            desc = line;
                        }
                    }
                    else if (isFind)
                    {
                        desc += Environment.NewLine + line;
                    }
                }
                AddFuncTooltip(name, desc);
            }
        }
        string GetFunName(string str)
        {
            int t = str.IndexOf(".");
            if (t > 0)
                return str.Substring(t + 1);
            return str;
        }
        void AddFuncTooltip(string name, string desc)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string fname = GetFunName(name);
                AddAutoMenuItem(funList, fname, desc);
                if (!tooltipDic.ContainsKey(fname))
                {
                    tooltipDic.Add(fname, desc);
                }
                else
                    tooltipDic[fname] += Environment.NewLine
                        + Environment.NewLine + desc;
            }
        }
        #endregion

        #region constant
        void AddAutoMenuItem(List<AutocompleteItem> list, string key, string desc)
        {
            bool isExists = false;
            foreach (AutocompleteItem ai in list)
            {
                if (ai.Text == key)
                {
                    isExists = true;
                    ai.ToolTipText += Environment.NewLine
                        + Environment.NewLine + desc;
                }
            }
            if (!isExists)
            {
                AutocompleteItem aitem = new AutocompleteItem(key);
                aitem.ToolTipTitle = key;
                aitem.ToolTipText = desc;
                list.Add(aitem);
            }
        }

        void AddConToolTip(string key, string desc)
        {
            AddAutoMenuItem(conList, key, desc);
            if (tooltipDic.ContainsKey(key))
                tooltipDic[key] += Environment.NewLine
                    + Environment.NewLine + desc;
            else
            {
                tooltipDic.Add(key, desc);
            }
        }


        void AddConstant(string conlua)
        {
            //conList.Add("con");
            if (File.Exists(conlua))
            {
                string[] lines = File.ReadAllLines(conlua);
                foreach (string line in lines)
                {
                    if (line.StartsWith("--"))
                        continue;
                    string k = line, desc = line;
                    int t = line.IndexOf("=");
                    int t2 = line.IndexOf("--");
                    k = (t > 0) ? line.Substring(0, t).TrimEnd(new char[] { ' ', '\t' })
                        : line;
                    desc = (t > 0) ? line.Substring(t + 1).Replace("--", "\n")
                        : line;
                    if (!tooltipDic.ContainsKey(k))
                    {
                        AddConToolTip(k, desc);
                    }
                    else
                        tooltipDic[k] += Environment.NewLine
                            + Environment.NewLine + desc;
                }
            }
        }
        #endregion

    }
}
