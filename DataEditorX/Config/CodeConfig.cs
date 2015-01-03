using System;
using System.Collections.Generic;
using System.IO;

using FastColoredTextBoxNS;

namespace DataEditorX.Config
{
    /// <summary>
    /// CodeEditor的配置
    /// </summary>
    public class CodeConfig
    {

        #region 成员
        public CodeConfig()
        {
            tooltipDic = new SortedList<string, string>();
            longTooltipDic = new SortedList<string, string>();
            items = new List<AutocompleteItem>();
        }

        //函数提示
        SortedList<string, string> tooltipDic;
        SortedList<string, string> longTooltipDic;
        List<AutocompleteItem> items;
        /// <summary>
        /// 输入提示
        /// </summary>
        public SortedList<string, string> TooltipDic
        {
            get { return tooltipDic; }
        }
        public SortedList<string, string> LongTooltipDic
        {
            get { return longTooltipDic; }
        }
        public AutocompleteItem[] Items
        {
            get { return items.ToArray(); }
        }
        #endregion

        #region 系列名/指示物
        /// <summary>
        /// 设置系列名
        /// </summary>
        /// <param name="dic"></param>
        public void SetNames(Dictionary<long, string> dic)
        {
            foreach (long k in dic.Keys)
            {
                string key = "0x" + k.ToString("x");
                if (!tooltipDic.ContainsKey(key))
                {
                    AddToolIipDic(key, dic[k]);
                }
            }
        }
        /// <summary>
        /// 读取指示物
        /// </summary>
        /// <param name="file"></param>
        public void AddStrings(string file)
        {
            if (File.Exists(file))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    //特殊胜利和指示物
                    if (line.StartsWith("!victory")
                       || line.StartsWith("!counter"))
                    {
                        string[] ws = line.Split(' ');
                        if (ws.Length > 2)
                        {
                            AddToolIipDic(ws[1], ws[2]);
                        }
                    }
                }
            }
        }

        #endregion

        #region function
        public void AddFunction(string funtxt)
        {
            if (!File.Exists(funtxt))
                return;
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
                    AddToolIipDic(name, desc);
                    int w = line.IndexOf("(");
                    int t = line.IndexOf(" ");

                    if (t < w && t > 0)
                    {
                        //找到函数
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
            AddToolIipDic(name, desc);
        }
        #endregion

        #region 常量
        public void AddConstant(string conlua)
        {
            //conList.Add("con");
            if (!File.Exists(conlua))
                return;
            string[] lines = File.ReadAllLines(conlua);
            foreach (string line in lines)
            {
                if (line.StartsWith("--"))
                    continue;
                string k = line, desc = line;
                int t = line.IndexOf("=");
                int t2 = line.IndexOf("--");
                //常量 = 0x1 ---注释
                k = (t > 0) ? line.Substring(0, t).TrimEnd(new char[] { ' ', '\t' })
                    : line;
                desc = (t > 0) ? line.Substring(t + 1).Replace("--", "\n")
                    : line;
                AddToolIipDic(k, desc);
            }
        }
        #endregion

        #region 处理
        public void InitAutoMenus()
        {
            items.Clear();
            foreach (string k in tooltipDic.Keys)
            {
                AutocompleteItem item = new AutocompleteItem(k);
                item.ToolTipTitle = k;
                item.ToolTipText = tooltipDic[k];
                items.Add(item);
            }
            foreach (string k in longTooltipDic.Keys)
            {
                if (tooltipDic.ContainsKey(k))
                    continue;
                AutocompleteItem item = new AutocompleteItem(k);
                item.ToolTipTitle = k;
                item.ToolTipText = longTooltipDic[k];
                items.Add(item);
            }
        }
        string GetShortName(string name)
        {
            int t = name.IndexOf(".");
            if (t > 0)
                return name.Substring(t + 1);
            else
                return name;
        }
        void AddToolIipDic(string key, string val)
        {
            string skey = GetShortName(key);
            if (tooltipDic.ContainsKey(skey))//存在
            {
                string nval = tooltipDic[skey];
                if (!nval.EndsWith(Environment.NewLine))
                    nval += Environment.NewLine;
                nval += Environment.NewLine +val;
                tooltipDic[skey] = nval;
            }
            else
                tooltipDic.Add(skey, val);
            //
            AddLongToolIipDic(key, val);
        }
        void AddLongToolIipDic(string key, string val)
        {
            if (longTooltipDic.ContainsKey(key))//存在
            {
                string nval = longTooltipDic[key];
                if (!nval.EndsWith(Environment.NewLine))
                    nval += Environment.NewLine;
                nval += Environment.NewLine + val;
                longTooltipDic[key] = nval;
            }
            else
                longTooltipDic.Add(key, val);
        }
        #endregion
    }
}
