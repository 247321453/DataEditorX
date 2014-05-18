/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 20:53
 * 
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataEditorX.Core
{
    /// <summary>
    /// Description of CheckPanel.
    /// </summary>
    public class CheckPanel : FlowLayoutPanel
    {
        Dictionary<long, string> m_dic=null;
        List<CheckBox> checkBoxList;
        public CheckPanel()
        {
            checkBoxList=new List<CheckBox>();
            m_dic=new Dictionary<long, string>();
        }
        public void SetDic(Dictionary<long, string> dic)
        {
            m_dic=dic;
        }
        public bool Init()
        {
            if(m_dic==null)
                return false;
            this.Controls.Clear();
            checkBoxList.Clear();
            this.SuspendLayout();
            int i=0;
            foreach(string str in m_dic.Values)
            {
                i++;
                if(str=="N/A")
                    continue;
                CheckBox cbox=new CheckBox();
                cbox.Name = "cbox"+i.ToString();
                cbox.TabIndex =i;
                cbox.AutoSize = true;
                cbox.Margin=new Padding(1,2,1,2);
                cbox.Text=str;
                checkBoxList.Add(cbox);
            }
            this.Controls.AddRange(checkBoxList.ToArray());
            this.ResumeLayout(false);
            this.PerformLayout();
            return true;
        }
        public void SetNumber(long num)
        {
            
        }
        public long GetNumber()
        {
            return 0;
        }
    }
}
