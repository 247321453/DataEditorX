﻿/*
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
using System.Text;

namespace DataEditorX
{
    /// <summary>
    /// Description of DataEditForm.
    /// </summary>
    public partial class DataEditForm : Form
    {
        #region 成员变量
        string GAMEPATH,PICPATH,UPDATEURL,GITURL,LUAPTH,INIPATH;
        Card oldCard=new Card(0);
        Card srcCard=new Card(0);
        ImageForm imgform=new ImageForm();
        string[] strs=null;
        string title;
        string nowCdbFile="";
        int MaxRow=20;
        int page=1,pageNum=1;
        int cardcount;
        
        List<Card> cardlist=new List<Card>();
        Dictionary<long, string> dicCardRules=null;
        Dictionary<long, string> dicCardAttributes=null;
        Dictionary<long, string> dicCardRaces=null;
        Dictionary<long, string> dicCardLevels=null;
        Dictionary<long, string> dicSetnames=null;
        Dictionary<long, string> dicCardTypes=null;
        Dictionary<long, string> dicCardcategorys=null;
        string confrule="card-rule.txt";
        string confattribute="card-attribute.txt";
        string confrace="card-race.txt";
        string conflevel="card-level.txt";
        string confsetname="card-setname.txt";
        string conftype="card-type.txt";
        string confcategory="card-category.txt";
        string confcover= "cover.jpg";
        #endregion
        
        #region 界面初始化
        public DataEditForm(string cdbfile)
        {
            InitializeComponent();
            nowCdbFile=cdbfile;
        }
        
        public DataEditForm()
        {
            InitializeComponent();
        }
        
        void DataEditFormLoad(object sender, EventArgs e)
        {
            InitListRows();
            title=this.Text;
            #if DEBUG
            title=title+"(DEBUG)";
            #endif

            imgform.VisibleChanged+=OnimgFormClosed;
            InitPath();
            InitForm();
            
            //set null card
            oldCard=new Card(0);
            SetCard(oldCard);
            if(!string.IsNullOrEmpty(nowCdbFile))
                Open(nowCdbFile);
        }
        
        void InitPath()
        {
            GAMEPATH=Application.StartupPath;
            INIPATH=Path.Combine(GAMEPATH,"DataEditorX.txt");
            if(File.Exists(INIPATH))
            {
                string[] lines=File.ReadAllLines(INIPATH, Encoding.UTF8);
                GAMEPATH=(lines.Length>0)?lines[0]:Application.StartupPath;
                UPDATEURL=(lines.Length>1)?lines[1]:"http://247321453@ys168.com";
                GITURL=(lines.Length>2)?lines[2]:"https://github.com/247321453/DataEditorX";
            }
            else
            {
                GAMEPATH=Application.StartupPath;
                UPDATEURL="http://247321453@ys168.com";
                GITURL="https://github.com/247321453/DataEcitorX";
            }
            PICPATH=Path.Combine(GAMEPATH,"pics");
            LUAPTH=Path.Combine(GAMEPATH,"script");
            
            string datapath=Path.Combine(Application.StartupPath,"data");
            
            confrule=Path.Combine(datapath,"card-rule.txt");
            confattribute=Path.Combine(datapath,"card-attribute.txt");
            confrace=Path.Combine(datapath,"card-race.txt");
            conflevel=Path.Combine(datapath,"card-level.txt");
            confsetname=Path.Combine(datapath,"card-setname.txt");
            conftype=Path.Combine(datapath,"card-type.txt");
            confcategory=Path.Combine(datapath,"card-category.txt");
            confcover= Path.Combine(datapath,"cover.jpg");
        }
        void DataEditFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if(!File.Exists(INIPATH))
                File.WriteAllLines(INIPATH,new string[]{GAMEPATH,UPDATEURL,GITURL},Encoding.UTF8);
        }
        
        void SaveDic(string file, Dictionary<long, string> dic)
        {
            using(FileStream fs=new FileStream(file,FileMode.OpenOrCreate,FileAccess.Write))
            {
                StreamWriter sw=new StreamWriter(fs,Encoding.UTF8);
                foreach(long k in dic.Keys)
                {
                    sw.WriteLine("0x"+k.ToString("x")+" "+dic[k]);
                }
                sw.Close();
                fs.Close();
            }
        }
        public void InitForm()
        {
            //初始化
            dicCardRules=InitComboBox(cb_cardrule,confrule);
            dicCardAttributes=InitComboBox(cb_cardattribute,confattribute);
            dicCardRaces=InitComboBox(cb_cardrace, confrace);
            dicCardLevels=InitComboBox(cb_cardlevel, conflevel);
            dicSetnames=DataManager.Read(confsetname);
            //setname
            string[] setnames=DataManager.GetValues(dicSetnames);
            cb_setname1.Items.AddRange(setnames);
            cb_setname2.Items.AddRange(setnames);
            cb_setname3.Items.AddRange(setnames);
            cb_setname4.Items.AddRange(setnames);
            //card types
            dicCardTypes=DataManager.Read(conftype);
            InitCheckPanel(pl_cardtype, dicCardTypes);
            //card categorys
            dicCardcategorys=DataManager.Read(confcategory);
            InitCheckPanel(pl_category, dicCardcategorys);
            //
            if(File.Exists(confcover))
                imgform.SetDefault(Image.FromFile(confcover));
        }
        //初始化FlowLayoutPanel
        void InitCheckPanel(FlowLayoutPanel fpanel, Dictionary<long, string> dic)
        {
            fpanel.SuspendLayout();
            fpanel.Controls.Clear();
            foreach(long key in dic.Keys)
            {
                CheckBox _cbox=new CheckBox();
                _cbox.Name=fpanel.Name+key.ToString();
                _cbox.Text=dic[key];
                _cbox.AutoSize=true;
                _cbox.Margin=fpanel.Margin;
                _cbox.Click+= PanelOnCheckClick;
                fpanel.Controls.Add(_cbox);
            }
            fpanel.ResumeLayout(false);
            fpanel.PerformLayout();
        }
        
        //FlowLayoutPanel点击CheckBox
        void PanelOnCheckClick(object sender, EventArgs e)
        {
            
        }

        //初始化ComboBox
        Dictionary<long, string> InitComboBox(ComboBox cb, string file)
        {
            Dictionary<long, string> tempdic=DataManager.Read(file);
            cb.Items.Clear();
            cb.Items.AddRange(DataManager.GetValues(tempdic));
            cb.SelectedIndex=0;
            return tempdic;
        }
        
        //计算list最大行数
        void InitListRows()
        {
            if ( lv_cardlist.Items.Count==0 )
            {
                ListViewItem item=new ListViewItem();
                item.Text="Test";
                lv_cardlist.Items.Add(item);
            }
            int headH=lv_cardlist.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Y;
            int itemH=lv_cardlist.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Height;
            if ( itemH>0 )
            {
                int n=( lv_cardlist.Height-headH-4 )/itemH;
                if ( n>0 )
                    MaxRow=n;
            }
            lv_cardlist.Items.Clear();
            if ( MaxRow<10 )
                MaxRow=20;
        }
        
        #endregion
        
        #region 设置卡片
        void SetCard(Card c)
        {
            oldCard=c;
            tb_cardname.Text=c.name;
            tb_cardtext.Text=c.desc;
            
            strs=new string[c.str.Length];
            Array.Copy(c.str,strs,c.str.Length);
            lb_scripttext.Items.Clear();
            lb_scripttext.Items.AddRange(c.str);
            tb_edittext.Text="";

            SetSelect(dicCardRules,cb_cardrule,(long)c.ot);
            SetSelect(dicCardAttributes,cb_cardattribute,(long)c.attribute);
            SetSelect(dicCardLevels,cb_cardlevel,(long)(c.level&0xff));
            SetSelect(dicCardRaces,cb_cardrace,c.race);
            
            SetSelect(dicSetnames, cb_setname1, c.setcode&0xffff);
            SetSelect(dicSetnames, cb_setname2, (c.setcode>>0x10)&0xffff);
            SetSelect(dicSetnames, cb_setname3, (c.setcode>>0x20)&0xffff);
            SetSelect(dicSetnames, cb_setname4, (c.setcode>>0x30)&0xffff);
            SetCheck(pl_cardtype,c.type);
            SetCheck(pl_category,c.category);
            
            tb_pleft.Text=((c.level >> 0x18) & 0xff).ToString();
            tb_pright.Text=((c.level >> 0x10) & 0xff).ToString();
            tb_atk.Text=(c.atk<0)?"?":c.atk.ToString();
            tb_def.Text=(c.def<0)?"?":c.def.ToString();
            tb_cardcode.Text=c.id.ToString();
            tb_cardalias.Text=c.alias.ToString();

        }
        void SetCheck(FlowLayoutPanel fpl,long number)
        {
            long temp;
            foreach(Control c in fpl.Controls)
            {
                if(c is CheckBox)
                {
                    CheckBox cbox=(CheckBox)c;
                    long.TryParse(cbox.Name.Substring(fpl.Name.Length), out temp);
                    
                    if((temp & number)==temp && temp!=0)
                        cbox.Checked=true;
                    else
                        cbox.Checked=false;
                }
            }
        }
        
        void SetSelect(Dictionary<long, string> dic,ComboBox cb, long k)
        {
            int index=0;
            if(k==0)
            {
                cb.SelectedIndex=0;
                return;
            }
            foreach(long key in dic.Keys)
            {
                if(k==key)
                    break;
                index++;
            }
            if(index==cb.Items.Count)
            {
                string word="0x"+k.ToString("x");
                if(!dic.ContainsKey(k))
                    dic.Add(k, word);
                cb.Items.Add(word);
            }
            cb.SelectedIndex=index;
        }
        
        #endregion
        
        #region 获取卡片
        Card GetCard()
        {
            int temp;
            long ltemp;
            Card c=new Card(0);
            c.name=tb_cardname.Text;
            c.desc=tb_cardtext.Text;
            
            Array.Copy(strs,c.str, c.str.Length);
            int.TryParse(GetSelect(dicCardRules,cb_cardrule,0),out c.ot);
            int.TryParse(GetSelect(dicCardAttributes,cb_cardattribute,0),out c.attribute);
            long.TryParse(GetSelect(dicCardLevels,cb_cardlevel,0),out c.level);
            long.TryParse(GetSelect(dicCardRaces,cb_cardrace,0),out c.race);
            
            long.TryParse(GetSelect(dicSetnames, cb_setname1,1), out ltemp);
            c.setcode+=ltemp;
            long.TryParse(GetSelect(dicSetnames, cb_setname2,1), out ltemp);
            c.setcode+=(ltemp<<0x10);
            long.TryParse(GetSelect(dicSetnames, cb_setname3,1), out ltemp);
            c.setcode+=(ltemp<<0x20);
            long.TryParse(GetSelect(dicSetnames, cb_setname4,1), out ltemp);
            c.setcode+=(ltemp<<0x30);
            
            c.type=GetCheck(pl_cardtype);
            c.category=GetCheck(pl_category);
            
            int.TryParse(tb_pleft.Text,out temp);
            c.level+=(temp << 0x18);
            int.TryParse(tb_pright.Text,out temp);
            c.level+=(temp << 0x10);
            
            int.TryParse( tb_atk.Text,out c.atk);
            int.TryParse( tb_def.Text,out c.def);
            long.TryParse( tb_cardcode.Text,out c.id);
            long.TryParse( tb_cardalias.Text,out c.alias);

            return c;
        }
        string GetSelect(Dictionary<long, string> dic,ComboBox cb, int start)
        {
            long fkey=0;
            foreach(long key in dic.Keys)
            {
                if(cb.Text==dic[key])
                {
                    fkey=key;
                    break;
                }
            }
            return fkey.ToString();
        }
        long GetCheck(FlowLayoutPanel fpl)
        {
            long number=0;
            long temp;
            foreach(Control c in fpl.Controls)
            {
                if(c is CheckBox)
                {
                    CheckBox cbox=(CheckBox)c;
                    long.TryParse(cbox.Name.Substring(fpl.Name.Length), out temp);
                    if(cbox.Checked)
                        number+=temp;
                }
            }
            return number;
        }
        #endregion
            
        #region 卡片列表
        void Lv_cardlistSelectedIndexChanged(object sender, EventArgs e)
        {
            if(lv_cardlist.SelectedItems.Count>0)
            {
                int sel=lv_cardlist.SelectedItems[0].Index;
                int index=(page-1)*MaxRow+sel;
                if(index<cardlist.Count)
                {
                    Card c=cardlist[index];
                    SetCard(c);
                    //设置卡片图像
                    if(imgform.Visible)
                        imgform.SetImageFile(Path.Combine(PICPATH,c.id.ToString()+".jpg"),c.name);
                }
            }
        }
        
        void AddListView(int p)
        {
            int i,j,istart,iend;

            if(p<=0)
                p=1;
            else if(p>=pageNum)
                p=pageNum;
            istart=(p-1)*MaxRow;
            iend=p*MaxRow;
            if(iend>cardcount)
                iend=cardcount;
            page=p;
            lv_cardlist.BeginUpdate();
            lv_cardlist.Items.Clear();
            if((iend-istart)>0)
            {
                ListViewItem[] items=new ListViewItem[iend-istart];
                
                for(i=istart,j=0;i < iend;i++,j++)
                {
                    items[j]=new ListViewItem();
                    items[j].Text=cardlist[i].id.ToString();
                    if ( i % 2 == 0 )
                        items[j].BackColor = Color.GhostWhite;
                    else
                        items[j].BackColor = Color.White;
                    items[j].SubItems.Add(cardlist[i].name);
                }
                lv_cardlist.Items.AddRange(items);
            }
            lv_cardlist.EndUpdate();
            tb_page.Text=page.ToString();
            
        }
        
        void Lv_cardlistKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                    case Keys.Delete:MyMsg.Show("del");break;
                    case Keys.Right:Btn_PageDownClick(null,null);break;
                    case Keys.Left:Btn_PageUpClick(null,null);break;
            }
        }
        
        void Btn_PageUpClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            page--;
            AddListView(page);
        }
        
        void Btn_PageDownClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            page++;
            AddListView(page);
        }
        
        void Tb_pageKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Enter)
            {
                int p;
                int.TryParse(tb_page.Text,out p);
                if(p>0)
                    AddListView(p);
            }
        }
        #endregion
        
        #region 卡片搜索，打开
        public bool Check()
        {
            if(File.Exists(nowCdbFile))
                return true;
            else
            {
                MyMsg.Warning(MyMsg.WARN_NoSelectCDB);
                return false;
            }
        }
        
        public bool Open(string cdbFile)
        {
            if(!File.Exists(cdbFile))
            {
                MyMsg.Error(string.Format(MyMsg.ERROR_FileNotExisit,cdbFile));
                return false;
            }
            nowCdbFile=cdbFile;
            this.Text=cdbFile+"-"+title;
            cardlist.Clear();
            SetCards(DataBase.Read(cdbFile,true,""),false);

            return true;
        }

        public void SetCards(Card[] cards, bool isfresh)
        {
            if(cards!=null)
            {
                cardlist.Clear();
                cardcount=cards.Length;
                pageNum=cardcount/MaxRow;
                if(cardcount%MaxRow > 0)
                    pageNum++;
                tb_pagenum.Text=pageNum.ToString();
                cardlist.AddRange(cards);
                if(isfresh)
                    AddListView(page);
                else
                    AddListView(1);
            }
            else
            {
                #if DEBUG
                MyMsg.Warning(MyMsg.ERROR_NoCard);
                #endif
                cardcount=0;
                page=1;
                pageNum=1;
                tb_page.Text=page.ToString();
                tb_pagenum.Text=pageNum.ToString();
            }
        }
        
        public void Search(Card c, bool isfresh)
        {
            if(!Check())
                return;
            srcCard=c;
            string sql=DataBase.GetSelectSQL(c);
            #if DEBUG
            MyMsg.Show(sql);
            #endif
            SetCards(DataBase.Read(nowCdbFile, true, sql),isfresh);
        }
        
        public void Reset()
        {
            oldCard=new Card(0);
            SetCard(oldCard);
        }
        
        #endregion
        
        #region 卡片编辑
        //添加
        public bool AddCard()
        {
            if(!Check())
                return false;
            Card c=GetCard();
            if(c.id<=0)
            {
                MyMsg.Error(MyMsg.ERROR_CardCodeIsZero);
                return false;
            }
            if(DataBase.Command(nowCdbFile, DataBase.GetInsertSQL(c,true))>=2)
            {
                MyMsg.Show(string.Format(MyMsg.INFO_Addition,c.ToString()));
                Search(srcCard, true);
                return true;
            }
            MyMsg.Error(MyMsg.ERROR_AdditionFail);
            return false;
        }
        //修改
        public bool ModCard()
        {
            if(!Check())
                return false;
            Card c=GetCard();

            if(c.Equals(oldCard))
            {
                MyMsg.Show(MyMsg.INFO_NoChanged);
                return false;
            }
            if(c.id<=0)
            {
                MyMsg.Error(MyMsg.ERROR_CardCodeIsZero);
                return false;
            }
            
            if(DataBase.Command(nowCdbFile, DataBase.GetUpdateSQL(c))>0)
            {
                MyMsg.Show(string.Format(MyMsg.INFO_Modifty,c.ToString()));
                Search(srcCard, true);
            }
            else
                MyMsg.Error(MyMsg.ERROR_ModiftyFail);
            return false;
        }
        //删除
        public bool DelCards()
        {
            if(!Check())
                return false;
            int ic=lv_cardlist.SelectedItems.Count;
            if(ic>=0)
                return false;
            if(!MyMsg.Question(string.Format(MyMsg.QUES_DeleteCard,ic)))
                return false;
            List<string> sql=new List<string>();
            foreach(ListViewItem lvitem in lv_cardlist.SelectedItems)
            {
                int index=lvitem.Index+(page-1)*MaxRow;
                if(index<cardlist.Count)
                {
                    Card c=cardlist[index];
                    sql.Add(DataBase.GetDeleteSQL(c));
                }
            }
            if(DataBase.Command(nowCdbFile, sql.ToArray())>=(sql.Count*2))
            {
                MyMsg.Show(MyMsg.INFO_Delete);
                Search(srcCard, true);
                return true;
            }
            else
            {
                MyMsg.Error(MyMsg.ERROR_DeleteFail);
                Search(srcCard, true);
            }
            
            return false;
        }
        //打开脚本
        public bool OpenScript()
        {
            if(!Check())
                return false;
            string lua=Path.Combine(LUAPTH,"c"+tb_cardcode.Text+".lua");
            if(!File.Exists(lua))
            {
                if(MyMsg.Question(string.Format(MyMsg.QUES_CreateLua,lua)))
                {
                    File.Create(lua);
                }
            }
            if(File.Exists(lua))
                System.Diagnostics.Process.Start(lua);
            return false;
        }
        #endregion
        
        #region 按钮
        //搜索卡片
        void Btn_serachClick(object sender, EventArgs e)
        {
            Search(GetCard(), false);
        }
        //重置卡片
        void Btn_resetClick(object sender, EventArgs e)
        {
            Reset();
        }
        //添加
        void Btn_addClick(object sender, EventArgs e)
        {
            AddCard();
        }
        //修改
        void Btn_modClick(object sender, EventArgs e)
        {
            ModCard();
        }
        //打开脚本
        void Btn_luaClick(object sender, EventArgs e)
        {
            OpenScript();
        }
        //删除
        void Btn_delClick(object sender, EventArgs e)
        {
            DelCards();
        }
        #endregion
        
        #region 文本框
        //卡片密码搜索
        void Tb_cardcodeKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Enter)
            {
                Card c=new Card(0);
                long.TryParse(tb_cardcode.Text, out c.id);
                if(c.id>0)
                {
                    string sql=DataBase.GetSelectSQL(c);
                    SetCards(DataBase.Read(nowCdbFile, true, sql), false);
                }
            }
        }
        //卡片名称搜索、编辑
        void Tb_cardnameKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Card c=new Card(0);
                c.name=tb_cardname.Text;
                string sql=DataBase.GetSelectSQL(c);
                SetCards(DataBase.Read(nowCdbFile, true, sql), false);
            }
        }
        //卡片描述编辑
        void Tb_cardtextKeyDown(object sender, KeyEventArgs e)
        {
            
        }
        void Setscripttext(string str)
        {
            int index=-1;
            try{
                index=lb_scripttext.SelectedIndex;
            }
            catch{
                index=-1;
                MyMsg.Error(MyMsg.ERROR_NoSeclectScriptText);
            }
            if(index>=0)
            {
                strs[index]=str;
                
                lb_scripttext.Items.Clear();
                lb_scripttext.Items.AddRange(strs);
                lb_scripttext.SelectedIndex=index;
            }
        }
        string Getscripttext()
        {
            int index=-1;
            try{
                index=lb_scripttext.SelectedIndex;
            }
            catch{
                index=-1;
                MyMsg.Error(MyMsg.ERROR_NoSeclectScriptText);
            }
            if(index>=0)
                return strs[index];
            else
                return "";
        }
        //脚本文本
        void Lb_scripttextSelectedIndexChanged(object sender, EventArgs e)
        {
            tb_edittext.Text=Getscripttext();
        }
        
        //脚本文本
        void Tb_edittextKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Enter)
                Setscripttext(tb_edittext.Text);
        }
        #endregion
        
        #region 帮助菜单
        void Menuitem_aboutClick(object sender, EventArgs e)
        {
            MyMsg.Show(MyMsg.ABOUT);
        }
        
        void Menuitem_checkupdateClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(UPDATEURL);
        }
        void Menuitem_githubClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(GITURL);
        }
        #endregion
        
        #region 文件菜单(chs)
        void Menuitem_openClick(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg=new OpenFileDialog())
            {
                dlg.Title="选择卡片数据库(cdb文件)";
                dlg.Filter="cdb文件(*.cdb)|*.cdb|所有文件(*.*)|*.*";
                if(dlg.ShowDialog()==DialogResult.OK)
                {
                    Open(dlg.FileName);
                }
            }
        }
        void Menuitem_newClick(object sender, EventArgs e)
        {
            using(SaveFileDialog dlg=new SaveFileDialog())
            {
                dlg.Title="选择卡片数据库(cdb文件)保存位置";
                dlg.Filter="cdb文件(*.cdb)|*.cdb|所有文件(*.*)|*.*";
                if(dlg.ShowDialog()==DialogResult.OK)
                {
                    if(DataBase.Create(dlg.FileName))
                    {
                        Open(dlg.FileName);
                    }
                }
            }
        }
        void Menuitem_copytoClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            using(OpenFileDialog dlg=new OpenFileDialog())
            {
                dlg.Title="选择卡片数据库(cdb文件)";
                dlg.Filter="cdb文件(*.cdb)|*.cdb|所有文件(*.*)|*.*";
                if(dlg.ShowDialog()==DialogResult.OK)
                {
                    if(MyMsg.Question("是否覆盖已经存在的卡片？"))
                        DataBase.CopyDB(dlg.FileName,false,cardlist.ToArray());
                    else
                        DataBase.CopyDB(dlg.FileName,true,cardlist.ToArray());
                }
            }
        }

        void Menuitem_readydkClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            using(OpenFileDialog dlg=new OpenFileDialog())
            {
                dlg.Title="选择卡组文件(ydk文件)";
                dlg.Filter="ydk文件(*.ydk)|*.ydk|所有文件(*.*)|*.*";
                if(dlg.ShowDialog()==DialogResult.OK)
                {
                    SetCards(DataBase.ReadYdk(nowCdbFile, dlg.FileName), false);
                }
            }
        }
        
        void Menuitem_readimagesClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            using(FolderBrowserDialog fdlg=new FolderBrowserDialog())
            {
                fdlg.Description="请选择卡片图像目录";
                if(fdlg.ShowDialog()==DialogResult.OK)
                {
                    SetCards(DataBase.ReadImage(nowCdbFile, fdlg.SelectedPath), false);
                }
            }
        }
        //关闭
        void Menuitem_quitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        
        #region 设置菜单
        void Menuitem_showimageClick(object sender, EventArgs e)
        {
            if(menuitem_showimage.Checked)
            {
                imgform.SetImageFile(
                    Path.Combine(PICPATH, tb_cardcode.Text+".jpg"),
                    tb_cardname.Text);
                imgform.Show();
            }
            else
                imgform.Hide();
        }
        void OnimgFormClosed(object sender, EventArgs e)
        {
            menuitem_showimage.Checked=imgform.Visible;
            
        }
        #endregion

       
    }
}
