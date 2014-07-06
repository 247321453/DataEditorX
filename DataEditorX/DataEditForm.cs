/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 20:22
 * 
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

using DataEditorX.Core;

namespace DataEditorX
{
    public partial class DataEditForm : Form
    {
        #region 成员变量
        string GAMEPATH,PICPATH,LUAPTH;
        Card oldCard=new Card(0);
        Card srcCard=new Card(0);
        string[] strs=null;
        string title;
        string nowCdbFile="";
        int MaxRow=20;
        int page=1,pageNum=1;
        int cardcount;
        bool isdownload;
        bool isbgcheck;
        string NEWVER="0.0.0.0";
        List<Card> cardlist=new List<Card>();
        
        Image m_cover;
        Dictionary<long, string> dicCardRules=null;
        Dictionary<long, string> dicCardAttributes=null;
        Dictionary<long, string> dicCardRaces=null;
        Dictionary<long, string> dicCardLevels=null;
        Dictionary<long, string> dicSetnames=null;
        Dictionary<long, string> dicCardTypes=null;
        Dictionary<long, string> dicCardcategorys=null;
        string conflang, confrule, confattribute, confrace, conflevel;
        string confsetname, conftype, confcategory, confcover;
        public DataEditForm(string cdbfile)
        {
            InitializeComponent();
            nowCdbFile=cdbfile;
        }
        
        public DataEditForm()
        {
            InitializeComponent();
            nowCdbFile="";
        }
        
        #endregion
        
        #region 界面初始化  
        //窗体第一次加载
        void DataEditFormLoad(object sender, EventArgs e)
        {
            InitListRows();
            #if DEBUG
            this.Text=this.Text+"(DEBUG)";
            #endif

            this.Text=this.Text+" Ver:"+Application.ProductVersion;
            title=this.Text;
            
            //界面初始化
            string dir=ConfigurationManager.AppSettings["language"];
            if(string.IsNullOrEmpty(dir))
            {
                Application.Exit();
            }
            string datapath=Path.Combine(Application.StartupPath, dir);
            InitPath(datapath);
            
            MyMsg.Init(conflang);
            InitString();
            
            InitGameData();
            
            SetCDB(nowCdbFile);
            //设置空白卡片
            oldCard=new Card(0);
            SetCard(oldCard);
            isbgcheck=true;
            isdownload=false;
            //Menuitem_checkupdateClick(null,null);
            if(File.Exists(nowCdbFile))
                Open(nowCdbFile);
            
        }
        //窗体关闭
        void DataEditFormFormClosing(object sender, FormClosingEventArgs e)
        {

        }
        
        //设置界面文字
        void InitString()
        {
            btn_add.Text=MyMsg.GetString("Add");
            btn_serach.Text=MyMsg.GetString("Search");
            btn_reset.Text=MyMsg.GetString("Reset");
            btn_del.Text=MyMsg.GetString("Delete");
            btn_mod.Text=MyMsg.GetString("Modify");
            btn_lua.Text=MyMsg.GetString("Lua Script");
            
            lb_cardalias.Text= MyMsg.GetString("Alias Card");
            lb_cardcode.Text=MyMsg.GetString("Card Code");
            lb_types.Text=MyMsg.GetString("Card Types");
            lb_categorys.Text=MyMsg.GetString("Card Categorys");
            lb_tiptexts.Text=MyMsg.GetString("Tips Texts");
            
            menuitem_help.Text=MyMsg.GetString("Help");
            menuitem_about.Text=MyMsg.GetString("About");
            menuitem_checkupdate.Text=MyMsg.GetString("Check Update");
            menuitem_github.Text=MyMsg.GetString("Source");
            
            menuitem_file.Text=MyMsg.GetString("File");
            menuitem_open.Text=MyMsg.GetString("Open Database");
            menuitem_new.Text=MyMsg.GetString("New Database");
            menuitem_readydk.Text=MyMsg.GetString("Cards Form ydk file");
            menuitem_readimages.Text=MyMsg.GetString("Cards From Images");
            menuitem_copyselectto.Text=MyMsg.GetString("Select Copy To");
            menuitem_copyto.Text=MyMsg.GetString("All Now Copy To");
            menuitem_quit.Text=MyMsg.GetString("Quit");
            
            lv_cardlist.Columns[0].Text=MyMsg.GetString("Card Code");
            lv_cardlist.Columns[1].Text=MyMsg.GetString("Card Name");
        }
        
        //按cdb路径设置目录
        void SetCDB(string cdb)
        {
            this.nowCdbFile=cdb;
            if(cdb.Length>0)
            {
                this.Text=nowCdbFile+"-"+title;
                char SEP=Path.DirectorySeparatorChar;
                int l=nowCdbFile.LastIndexOf(SEP);
                GAMEPATH=(l>0)?nowCdbFile.Substring(0,l+1):cdb;
            }
            else
                GAMEPATH=Application.StartupPath;
            PICPATH=Path.Combine(GAMEPATH,"pics");
            LUAPTH=Path.Combine(GAMEPATH,"script");
        }
        //初始化文件路径
        void InitPath(string datapath)
        {
            conflang=Path.Combine(datapath, "language.txt");
            confrule=Path.Combine(datapath, "card-rule.txt");
            confattribute=Path.Combine(datapath, "card-attribute.txt");
            confrace=Path.Combine(datapath, "card-race.txt");
            conflevel=Path.Combine(datapath, "card-level.txt");
            confsetname=Path.Combine(datapath, "card-setname.txt");
            conftype=Path.Combine(datapath, "card-type.txt");
            confcategory=Path.Combine(datapath, "card-category.txt");
            confcover= Path.Combine(datapath, "cover.jpg");
        }
        
        //保存dic
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
        
        //初始化游戏数据
        public void InitGameData()
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
                m_cover=Image.FromFile(confcover);
            else
                m_cover=null;
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
            string f=Path.Combine(PICPATH, c.id.ToString()+".jpg");
            if(File.Exists(f))
                pl_image.BackgroundImage=Image.FromFile(f);
            else
                pl_image.BackgroundImage=m_cover;
        }
        
        //设置checkbox
        string SetCheck(FlowLayoutPanel fpl,long number)
        {
            long temp;
            string strType="";
            foreach(Control c in fpl.Controls)
            {
                if(c is CheckBox)
                {
                    CheckBox cbox=(CheckBox)c;
                    long.TryParse(cbox.Name.Substring(fpl.Name.Length), out temp);
                    
                    if((temp & number)==temp && temp!=0)
                    {
                        cbox.Checked=true;
                        strType+="/"+c.Text;
                    }
                    else
                        cbox.Checked=false;
                }
            }
            return strType;
        }
        
        //设置combobox
        int SetSelect(Dictionary<long, string> dic,ComboBox cb, long k)
        {
            int index=0;
            if(k==0)
            {
                cb.SelectedIndex=0;
                return index;
            }
            foreach(long key in dic.Keys)
            {
                if(k==key)
                    break;
                index++;
            }
            if(index==cb.Items.Count)
            {
                string word=k.ToString("x");
                if(!dic.ContainsKey(k))
                    dic.Add(k, word);
                if(cb.Name==cb_setname1.Name
                   ||cb.Name==cb_setname2.Name
                   ||cb.Name==cb_setname3.Name
                   ||cb.Name==cb_setname4.Name)
                {
                    cb_setname1.Items.Add(word);
                    cb_setname2.Items.Add(word);
                    cb_setname3.Items.Add(word);
                    cb_setname4.Items.Add(word);
                }
                else
                    cb.Items.Add(word);
            }
            cb.SelectedIndex=index;
            return index;
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
            int.TryParse(GetSelect(dicCardRules,cb_cardrule),out c.ot);
            int.TryParse(GetSelect(dicCardAttributes,cb_cardattribute),out c.attribute);
            long.TryParse(GetSelect(dicCardLevels,cb_cardlevel),out c.level);
            long.TryParse(GetSelect(dicCardRaces,cb_cardrace),out c.race);
            
            long.TryParse(GetSelect(dicSetnames, cb_setname1), out ltemp);
            c.setcode+=ltemp;
            long.TryParse(GetSelect(dicSetnames, cb_setname2), out ltemp);
            c.setcode+=(ltemp<<0x10);
            long.TryParse(GetSelect(dicSetnames, cb_setname3), out ltemp);
            c.setcode+=(ltemp<<0x20);
            long.TryParse(GetSelect(dicSetnames, cb_setname4), out ltemp);
            c.setcode+=(ltemp<<0x30);
            
            c.type=GetCheck(pl_cardtype);
            c.category=GetCheck(pl_category);
            
            int.TryParse(tb_pleft.Text,out temp);
            c.level+=(temp << 0x18);
            int.TryParse(tb_pright.Text,out temp);
            c.level+=(temp << 0x10);
            if(tb_atk.Text=="?"||tb_atk.Text=="？")
                c.atk=-2;
            else
                int.TryParse( tb_atk.Text,out c.atk);
            if(tb_def.Text=="?"||tb_def.Text=="？")
                c.def=-2;
            else
                int.TryParse( tb_def.Text,out c.def);
            long.TryParse( tb_cardcode.Text,out c.id);
            long.TryParse( tb_cardalias.Text,out c.alias);

            return c;
        }
        
        //得到所选值
        string GetSelect(Dictionary<long, string> dic,ComboBox cb)
        {
            long fkey=0;
            bool isfind=false;
            foreach(long key in dic.Keys)
            {
                if(cb.Text==dic[key])
                {
                    fkey=key;
                    isfind=true;
                    break;
                }
            }
            if(!isfind)
            {
                long.TryParse(cb.Text, NumberStyles.HexNumber, null, out fkey);
            }
            return fkey.ToString();
        }
        
        //得到checkbox的总值
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
                }
            }
        }
        
        //添加行
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
                Card mcard;
                for(i=istart,j=0;i < iend;i++,j++)
                {
                    mcard=cardlist[i];
                    items[j]=new ListViewItem();
                    items[j].Text=mcard.id.ToString();
                    if ( i % 2 == 0 )
                        items[j].BackColor = Color.GhostWhite;
                    else
                        items[j].BackColor = Color.White;
                    items[j].SubItems.Add(mcard.name);
                }
                lv_cardlist.Items.AddRange(items);
            }
            lv_cardlist.EndUpdate();
            tb_page.Text=page.ToString();
            
        }
        
        //列表按键
        void Lv_cardlistKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                    case Keys.Delete:DelCards();break;
                    case Keys.Right:Btn_PageDownClick(null,null);break;
                    case Keys.Left:Btn_PageUpClick(null,null);break;
            }
        }
        
        //上一页
        void Btn_PageUpClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            page--;
            AddListView(page);
        }
        
        //下一页
        void Btn_PageDownClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            page++;
            AddListView(page);
        }
        
        //跳转到指定页数
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
        //检查是否打开数据库
        public bool Check()
        {
            if(File.Exists(nowCdbFile))
                return true;
            else
            {
                MyMsg.Warning(MyMsg.GetString("Please select a DataBase!"));
                return false;
            }
        }
        
        //打开数据库
        public bool Open(string cdbFile)
        {
            if(!File.Exists(cdbFile))
            {
                MyMsg.Error(MyMsg.GetString("File is not exists!"));
                return false;
            }
            SetCDB(cdbFile);
            cardlist.Clear();
            DataBase.CheckTable(cdbFile);
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
                cardcount=0;
                page=1;
                pageNum=1;
                tb_page.Text=page.ToString();
                tb_pagenum.Text=pageNum.ToString();
                cardlist.Clear();
                lv_cardlist.Items.Clear();
                SetCard(new Card(0));
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
                MyMsg.Error(MyMsg.GetString("Card Code is zero!"));
                return false;
            }
            foreach(Card ckey in cardlist)
            {
                if(c.id==ckey.id)
                {
                    MyMsg.Warning(MyMsg.GetString("Card is exists!"));
                    return false;
                }
            }
            if(DataBase.Command(nowCdbFile, DataBase.GetInsertSQL(c,true))>=2)
            {
                MyMsg.Show(MyMsg.GetString("Add Card succeed"));
                Search(srcCard, true);
                return true;
            }
            MyMsg.Error(MyMsg.GetString("Add Card fail"));
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
                MyMsg.Show(MyMsg.GetString("Card is not changed!"));
                return false;
            }
            if(c.id<=0)
            {
                MyMsg.Error(MyMsg.GetString("Card Code is zero"));
                return false;
            }
            string sql;
            if(c.id!=oldCard.id)
            {
                if(MyMsg.Question(string.Format(
                    MyMsg.GetString("Delete Card {0} ?"),oldCard.ToString())))
                {
                    if(DataBase.Command(nowCdbFile, DataBase.GetDeleteSQL(oldCard))<2)
                    {
                        MyMsg.Error(MyMsg.GetString("Delete Card fail!"));
                        return false;
                    }
                }
                sql=DataBase.GetInsertSQL(c,false);
            }
            else
                sql=DataBase.GetUpdateSQL(c);
            if(DataBase.Command(nowCdbFile, sql)>0)
            {
                MyMsg.Show(MyMsg.GetString("Modify Card succeed!"));
                Search(srcCard, true);
            }
            else
                MyMsg.Error(MyMsg.GetString("Modify Card fail!"));
            return false;
        }
        //删除
        public bool DelCards()
        {
            if(!Check())
                return false;
            int ic=lv_cardlist.SelectedItems.Count;
            if(ic==0)
                return false;
            if(!MyMsg.Question(string.Format(MyMsg.GetString("Delete {0} Cards ?"),ic)))
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
                MyMsg.Show(MyMsg.GetString("Delete Card succeed!"));
                Search(srcCard, true);
                return true;
            }
            else
            {
                MyMsg.Error(MyMsg.GetString("Delete Card fail!"));
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
                if(MyMsg.Question(
                    string.Format(MyMsg.GetString("create script file?{0}"),"\n"+lua)))
                {
                    if(!Directory.Exists(LUAPTH))
                        Directory.CreateDirectory(LUAPTH);
                    using(FileStream fs=new FileStream(
                        lua,
                        FileMode.OpenOrCreate,
                        FileAccess.Write))
                    {
                        StreamWriter sw=new StreamWriter(fs,new UTF8Encoding(false));
                        sw.WriteLine("--"+tb_cardname.Text);
                        sw.Close();
                        fs.Close();
                    }
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
                MyMsg.Error(MyMsg.GetString("Please select script text!"));
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
                MyMsg.Error(MyMsg.GetString("Please select script text!"));
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
            MyMsg.Show(string.Format(
                MyMsg.GetString("About:{0}Version:{1}Author:{2}Email:{3}")
                ,"\t"+Application.ProductName+"\n"
                ,"\t"+Application.ProductVersion+"\n"
                ,"\t247321453\n"
                ,"\t247321453@qq.com"));
        }
        
        void Menuitem_checkupdateClick(object sender, EventArgs e)
        {
            if(!backgroundWorker1.IsBusy)
            {
                isdownload=false;
                isbgcheck=false;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        void Menuitem_githubClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["sourceURL"]);
        }
        #endregion
        
        #region 文件菜单
        void Menuitem_openClick(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg=new OpenFileDialog())
            {
                dlg.Title=MyMsg.GetString("Please select database.");
                dlg.Filter=MyMsg.GetString("cdb file(*.cdb)|*.cdb|all files(*.*)|*.*");
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
                dlg.Title=MyMsg.GetString("Please select database.");
                dlg.Filter=MyMsg.GetString("cdb file(*.cdb)|*.cdb|all files(*.*)|*.*");
                if(dlg.ShowDialog()==DialogResult.OK)
                {
                    if(DataBase.Create(dlg.FileName))
                    {
                        if(MyMsg.Question(MyMsg.GetString("if open this new database?")))
                            Open(dlg.FileName);
                    }
                }
            }
        }
       
        void Menuitem_copytoClick(object sender, EventArgs e)
        {
            CopyTo(false);
        }
        
        void Menuitem_copyselecttoClick(object sender, EventArgs e)
        {
            CopyTo(true);
        }
        
        void CopyTo(bool onlyselect)
        {
            if(!Check())
                return;
            List<Card> cards=new List<Card>();
            if(onlyselect)
            {
                foreach(ListViewItem lvitem in lv_cardlist.SelectedItems)
                {
                    int index=lvitem.Index+(page-1)*MaxRow;
                    if(index<cardlist.Count)
                        cards.Add(cardlist[index]);
                }
            }
            else
                cards.AddRange(cardlist.ToArray());
            using(OpenFileDialog dlg=new OpenFileDialog())
            {
                dlg.Title=MyMsg.GetString("Please select database.");
                dlg.Filter=MyMsg.GetString("cdb file(*.cdb)|*.cdb|all files(*.*)|*.*");
                if(dlg.ShowDialog()==DialogResult.OK)
                {
                    if(MyMsg.Question(MyMsg.GetString("Overwrite existing card?")))
                        DataBase.CopyDB(dlg.FileName,false,cards.ToArray());
                    else
                        DataBase.CopyDB(dlg.FileName,true,cards.ToArray());
                }
            }
        }
        void Menuitem_readydkClick(object sender, EventArgs e)
        {
            if(!Check())
                return;
            using(OpenFileDialog dlg=new OpenFileDialog())
            {
                dlg.Title=MyMsg.GetString("Please select ydk file.");
                dlg.Filter=MyMsg.GetString("ydk file(*.ydk)|*.ydk|all files(*.*)|*.*");
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
                fdlg.Description= MyMsg.GetString("Please select images directory");
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
        
        #region 线程
        //线程任务
        void BackgroundWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if(isdownload)
                CheckUpdate.DownLoad(Path.Combine(Application.StartupPath, NEWVER+".zip"));
            else
            {
                NEWVER=CheckUpdate.Check(ConfigurationManager.AppSettings["updateURL"]);
            }
        }
        //任务完成
        void BackgroundWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if(isdownload)
            {
                if(CheckUpdate.isOK)
                    MyMsg.Show(MyMsg.GetString("Download succeed."));
                else
                    MyMsg.Show(MyMsg.GetString("Download fail.")+"\n"+CheckUpdate.URL);
            }
            else
            {
                string newver=NEWVER;
                int iver,iver2;
                int.TryParse(Application.ProductVersion.Replace(".",""), out iver);
                int.TryParse(newver.Replace(".",""), out iver2);
                if(iver2>iver)
                {
                    if(MyMsg.Question(string.Format(
                        MyMsg.GetString("have a new version.{0}version:{1}"),
                        "\n",newver)))
                    {
                        if(!backgroundWorker1.IsBusy)
                        {
                            isdownload=true;
                            isbgcheck=false;
                            backgroundWorker1.RunWorkerAsync();
                        }
                    }
                }
                else if(iver2>0)
                    
                {
                    if(!isbgcheck)
                    {
                        if( MyMsg.Question(string.Format(
                            MyMsg.GetString("Is Last Version.{0}Version:{1}"),
                            "\n",newver+"\n")))
                        {
                            
                            if(!backgroundWorker1.IsBusy)
                            {
                                isdownload=true;
                                isbgcheck=false;
                                backgroundWorker1.RunWorkerAsync();
                            }
                        }
                    }
                }
                else
                {
                    if(!isbgcheck)
                        MyMsg.Error(MyMsg.GetString("Check update fail!"));
                }
            }
        }
        #endregion
    }
}
