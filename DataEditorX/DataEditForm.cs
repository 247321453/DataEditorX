/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 20:22
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using DataEditorX.Core;

namespace DataEditorX
{
    /// <summary>
    /// Description of DataEditForm.
    /// </summary>
    public partial class DataEditForm : Form
    {
        string strSetname="卡片系列";
        Dictionary<long, string> dicCardRules=null;
        Dictionary<long, string> dicCardAttributes=null;
        Dictionary<long, string> dicCardRaces=null;
        Dictionary<long, string> dicCardLevels=null;
        Dictionary<long, string> dicSetnames=null;
        Dictionary<long, string> dicCardTypes=null;
        Dictionary<long, string> dicCardcategorys=null;
        
        public DataEditForm(string cdbfile)
        {
            InitializeComponent();
        }
        public DataEditForm()
        {
            InitializeComponent();
        }
        public bool InitForm(string directory)
        {
            dicCardRules=InitComboBox(
                cb_cardrule,Path.Combine(directory, "card-rule.txt"));
            dicCardAttributes=InitComboBox(
                cb_cardattribute,Path.Combine(directory, "card-attribute.txt"));
            dicCardRaces=InitComboBox(
                cb_cardrace,Path.Combine(directory, "card-race.txt"));
            dicCardLevels=InitComboBox(
                cb_cardlevel,Path.Combine(directory, "card-level.txt"));
            dicSetnames=DataManager.Read(
                Path.Combine(directory, "card-setname.txt"));
            string[] setnames=DataManager.GetValues(dicSetnames);
            cb_setname1.Items.Add(strSetname+"1");
            cb_setname2.Items.Add(strSetname+"2");
            cb_setname3.Items.Add(strSetname+"3");
            cb_setname4.Items.Add(strSetname+"4");
            cb_setname1.Items.AddRange(setnames);
            cb_setname2.Items.AddRange(setnames);
            cb_setname3.Items.AddRange(setnames);
            cb_setname4.Items.AddRange(setnames);
            dicCardTypes=DataManager.Read(
                Path.Combine(directory, "card-type.txt"));
            pl_cardtype.SetDic(dicCardTypes);
            pl_cardtype.Init();

            dicCardcategorys=DataManager.Read(
                Path.Combine(directory, "card-category.txt"));
            pl_category.SetDic(dicCardcategorys);
            pl_category.Init();
            
            SetCard(new Card(0,""));
            return true;
        }
        Dictionary<long, string> InitComboBox(ComboBox cb, string file)
        {
            Dictionary<long, string> tempdic=DataManager.Read(file);
            cb.Items.Clear();
            cb.Items.AddRange(DataManager.GetValues(tempdic));
            cb.SelectedIndex=0;
            return tempdic;
        }
        void SetCard(Card card)
        {
            cb_setname1.SelectedIndex=0;
            cb_setname2.SelectedIndex=0;
            cb_setname3.SelectedIndex=0;
            cb_setname4.SelectedIndex=0;
        }
        
        void DataEditFormLoad(object sender, EventArgs e)
        {
            
        }
    }
}
