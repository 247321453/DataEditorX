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
using System.Text;

namespace DataEditorX
{
    /// <summary>
    /// Description of DataEditForm.
    /// </summary>
    public partial class DataEditForm : Form
    {
        string GAMEPATH,PICPATH,UPDATEURL;
        Card oldCard=new Card(0);
        ImageForm imgform=new ImageForm();
        string[] strs=null;
        string title;
        string nowCdbFile="";
        int MaxRow=20;
        int page=1,pageNum=1;
        int cardcount;
        string strSetname="卡片系列";
        List<Card> cardlist=new List<Card>();
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
            nowCdbFile=cdbfile;
        }
        public DataEditForm()
        {
            InitializeComponent();
        }
        
        #region 界面初始化
        void DataEditFormLoad(object sender, EventArgs e)
        {
            InitListRows();
            title=this.Text;
            #if DEBUG
            title=title+"(DEBUG)";
            #endif
                        //set null card
            oldCard=new Card(0);
            SetCard(oldCard);
            if(!string.IsNullOrEmpty(nowCdbFile))
                Open(nowCdbFile);
            imgform.VisibleChanged+=OnimgFormClosed;
            GAMEPATH=Application.StartupPath;
            if(File.Exists(Path.Combine(GAMEPATH,"DataEditorX.txt")))
            {
                string[] lines=File.ReadAllLines(Path.Combine(GAMEPATH,"DataEditorX.txt"),Encoding.Default);
                GAMEPATH=(lines.Length>0)?lines[0]:Application.StartupPath;
                UPDATEURL=(lines.Length>1)?lines[1]:"http://about:blank";
            }
            
            PICPATH=Path.Combine(GAMEPATH,"pics");
        }
        public void InitForm(string directory)
        {
            //初始化
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
            //setname
            string[] setnames=DataManager.GetValues(dicSetnames);
            cb_setname1.Items.Add(strSetname+"1");
            cb_setname2.Items.Add(strSetname+"2");
            cb_setname3.Items.Add(strSetname+"3");
            cb_setname4.Items.Add(strSetname+"4");
            cb_setname1.Items.AddRange(setnames);
            cb_setname2.Items.AddRange(setnames);
            cb_setname3.Items.AddRange(setnames);
            cb_setname4.Items.AddRange(setnames);
            //card types
            dicCardTypes=DataManager.Read(
                Path.Combine(directory, "card-type.txt"));
            InitCheckPanel(pl_cardtype, dicCardTypes);
            //card categorys
            dicCardcategorys=DataManager.Read(
                Path.Combine(directory, "card-category.txt"));
            InitCheckPanel(pl_category, dicCardcategorys);
            //   
            if(File.Exists(Path.Combine(directory, "cover.jpg")))
                imgform.SetDefault(Image.FromFile(Path.Combine(directory, "cover.jpg")));
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
                int n=( lv_cardlist.Height-headH-2 )/itemH;
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
            
            strs=c.str;
            lb_scripttext.Items.Clear();
            lb_scripttext.Items.AddRange(c.str);
            tb_edittext.Text="";

            SetSelect(dicCardRules,cb_cardrule,(long)c.ot,0);
            SetSelect(dicCardAttributes,cb_cardattribute,(long)c.attribute,0);
            SetSelect(dicCardLevels,cb_cardlevel,(long)(c.level&0xff),0);
            SetSelect(dicCardRaces,cb_cardrace,c.race,0);
            
            SetSelect(dicSetnames, cb_setname1, c.setcode&0xffff,1);
            SetSelect(dicSetnames, cb_setname2, (c.setcode>>0x10)&0xffff,1);
            SetSelect(dicSetnames, cb_setname3, (c.setcode>>0x20)&0xffff,1);
            SetSelect(dicSetnames, cb_setname4, (c.setcode>>0x30)&0xffff,1);
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
        
        void SetSelect(Dictionary<long, string> dic,ComboBox cb, long k,int start)
        {
            int index=0;
            bool isfind=false;
            foreach(long key in dic.Keys)
            {
                if(k==key)
                {
                    isfind=true;
                    break;
                }
                index++;
            }
            if(isfind)
                cb.SelectedIndex=index+start;
            else
                cb.SelectedIndex=0;
        }
        
        #endregion
        
        #region 获取卡片
        Card GetCard()
        {
            int temp;
            Card c=new Card(0);
            c.name=tb_cardname.Text;
            c.desc=tb_cardtext.Text;
            
            c.str=strs;
            int.TryParse(GetSelect(dicCardRules,cb_cardrule,0),out c.ot);
            int.TryParse(GetSelect(dicCardAttributes,cb_cardattribute,0),out c.attribute);
            long.TryParse(GetSelect(dicCardLevels,cb_cardlevel,0),out c.level);
            long.TryParse(GetSelect(dicCardRaces,cb_cardrace,0),out c.race);
            
            int.TryParse(GetSelect(dicSetnames, cb_setname1,1), out temp);
            c.setcode+=temp;
            int.TryParse(GetSelect(dicSetnames, cb_setname2,1), out temp);
            c.setcode+=(temp<<0x10);
            int.TryParse(GetSelect(dicSetnames, cb_setname3,1), out temp);
            c.setcode+=(temp<<0x20);
            int.TryParse(GetSelect(dicSetnames, cb_setname4,1), out temp);
            c.setcode+=(temp<<0x30);
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
        
        #region 消息显示
        void ShowMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void ShowWarning(string strWarn)
        {
            MessageBox.Show(strWarn, "警告",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        void ShowError(string strError)
        {
            MessageBox.Show(strError, "错误",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        bool ShowQuestion(string strQues)
        {
            if(MessageBox.Show(strQues, "询问",
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Question)==DialogResult.OK)
                return true;
            else
                return false;
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
                    imgform.SetImageFile(Path.Combine(PICPATH,c.id.ToString()+".jpg"),c.name);
                }
            }
        }
                
        void AddListView(int p)
        {
            int i,j,istart,iend;
            if(p<=0)
            {
                p=1;
                istart=(p-1)*MaxRow;
                iend=p*MaxRow;
            }
            else if(p>=pageNum)
            {
                p=pageNum;
                istart=(p-1)*MaxRow;
                iend=cardcount;
            }
            else
            {
                istart=(p-1)*MaxRow;
                iend=p*MaxRow;
            }
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
             
        void Btn_PageUpClick(object sender, EventArgs e)
        {
            page--;
            AddListView(page);
        }
        
        void Btn_PageDownClick(object sender, EventArgs e)
        {
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
        
        #region 卡片编辑
        public bool Open(string cdbFile)
        {
            if(!File.Exists(cdbFile))
            {
                ShowMsg("文件不存在！\n"+cdbFile);
                return false;
            }
            this.Text=cdbFile+"-"+title;
            cardlist.Clear();
            SetCards(DataBase.Read(cdbFile,true,""));

            return true;
        }

        public void SetCards(Card[] cards)
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
                AddListView(1);
            }
            else
            {
                ShowWarning("没有卡片!");
            }
        }
        public void Search(Card c)
        {
            string sql=DataBase.GetSelectSQL(c);
            #if DEBUG
            ShowMsg(sql);
            #endif
            SetCards(DataBase.Read(nowCdbFile, true, sql));
        }
        public void Reset()
        {
            oldCard=new Card(0);
            SetCard(oldCard);
        }
        public void Undo()
        {

        }
        
        public void Redo()
        {
            
        }
        public bool Add()
        {
            return false;
        }
        public bool Mod()
        {
            return false;
        }
        public bool Del()
        {
            return false;
        }
        public bool OpenScript()
        {
            return false;
        }
        public void OpanGame()
        {
            
        }

        #endregion
        
        #region 按钮
        //搜索卡片
        void Btn_serachClick(object sender, EventArgs e)
        {
            Search(GetCard());
        }
        //重置卡片
        void Btn_resetClick(object sender, EventArgs e)
        {
            Reset();
        }
        //还原
        void Btn_redoClick(object sender, EventArgs e)
        {
            Redo();
        }
        //撤销
        void Btn_undoClick(object sender, EventArgs e)
        {
            Undo();
        }
        //添加
        void Btn_addClick(object sender, EventArgs e)
        {
            Add();
        }
        //修改
        void Btn_modClick(object sender, EventArgs e)
        {
            Mod();
        }
        //打开脚本
        void Btn_luaClick(object sender, EventArgs e)
        {
            OpenScript();
        }
        //删除
        void Btn_delClick(object sender, EventArgs e)
        {
            Del();
        }
        #endregion
        
        #region 文本框
        //卡片密码搜索
        void Tb_cardcodeKeyPress(object sender, KeyPressEventArgs e)
        {
            Card c=new Card(0);
            long.TryParse(tb_cardcode.Text, out c.id);
            if(c.id>0)
            {
                string sql=DataBase.GetSelectSQL(c);
                SetCards(DataBase.Read(nowCdbFile, true, sql));
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
                SetCards(DataBase.Read(nowCdbFile, true, sql));
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
                ShowError("请选中脚本文本");
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
                ShowError("请选中脚本文本");
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
            
        }
        
        void Menuitem_checkupdateClick(object sender, EventArgs e)
        {
            
        }
        #endregion
        
        #region 文件菜单
        void Menuitem_openClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_saveClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_saveasClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_newClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_copytoClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_inportClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_exportClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_readydkClick(object sender, EventArgs e)
        {
            
        }
        
        void Menuitem_readimagesClick(object sender, EventArgs e)
        {
            
        }
        //关闭
        void Menuitem_quitClick(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        
        #region 编辑菜单
        void Menuitem_undoClick(object sender, EventArgs e)
        {
            Undo();
        }
        
        void Menuitem_redoClick(object sender, EventArgs e)
        {
            Redo();
        }
        
        void Menuitem_addClick(object sender, EventArgs e)
        {
            Add();
        }
        
        void Menuitem_modClick(object sender, EventArgs e)
        {
            Mod();
        }
        
        void Menuitem_delClick(object sender, EventArgs e)
        {
            Del();
        }
        
        void Menuitem_searchClick(object sender, EventArgs e)
        {
            Search(GetCard());
        }
        
        void Menuitem_resetClick(object sender, EventArgs e)
        {
            Reset();
        }
        
        void Menuitem_showimageClick(object sender, EventArgs e)
        {
            if(menuitem_showimage.Checked)
                imgform.Show();
           else
               imgform.Hide();
        }
        void OnimgFormClosed(object sender, EventArgs e)
        {
                menuitem_showimage.Checked=imgform.Visible;
                
        }
        void Menuitem_openscriptClick(object sender, EventArgs e)
        {
            OpenScript();
        }
        
        void Menuitem_opengameClick(object sender, EventArgs e)
        {
            OpanGame();
        }
        #endregion
    }
}
