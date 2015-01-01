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
            Init();
        }

        public void Init()
        {
            tooltipDic = new Dictionary<string, string>();
            funList = new List<AutocompleteItem>();
            conList = new List<AutocompleteItem>();
        }

        //函数提示
        Dictionary<string, string> tooltipDic;
        //函数列表
        List<AutocompleteItem> funList;
        //常量列表
        List<AutocompleteItem> conList;

        /// <summary>
        /// 输入提示
        /// </summary>
        public Dictionary<string, string> TooltipDic
        {
            get { return tooltipDic; }
        }
        /// <summary>
        /// 函数列表
        /// </summary>
        public AutocompleteItem[] FunList
        {
            get { return funList.ToArray(); }
        }
        /// <summary>
        /// 常量列表
        /// </summary>
        public AutocompleteItem[] ConList
        {
            get { return conList.ToArray(); }
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
                    AddConToolTip(key, dic[k]);
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
                            AddConToolTip(ws[1], ws[2]);
                        }
                    }
                }
            }
        }

        #endregion

        #region function
        /// <summary>
        /// 添加函数
        /// </summary>
        /// <param name="funtxt"></param>
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
                    AddFuncTooltip(name, desc);
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
            AddFuncTooltip(name, desc);
        }
        //获取不带类名的函数名
        string GetFunName(string str)
        {
            int t = str.IndexOf(".");
            if (t > 0)
                return str.Substring(t + 1);
            return str;
        }
        //添加提示
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
        //常量提示
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
        //常量
        public void AddConstant(string conlua)
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
                    //常量 = 0x1 ---注释
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

        #region 提示
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
        #endregion
    }
}
