﻿/*
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
using DataEditorX.Language;
using WeifenLuo.WinFormsUI.Docking;
using DataEditorX.Controls;

using DataEditorX.Config;
using DataEditorX.Core.Mse;

namespace DataEditorX
{
    public partial class DataEditForm : DockContent, IEditForm
    {
        #region 成员变量/构造
        TaskHelper tasker = null;
        string taskname;
        string GAMEPATH = "", PICPATH = "", PICPATH2 = "", LUAPTH = "";
        /// <summary>当前卡片</summary>
        Card oldCard = new Card(0);
        /// <summary>搜索条件</summary>
        Card srcCard = new Card(0);
        string[] strs = null;
        /// <summary>
        /// 对比的id集合
        /// </summary>
        List<string>tmpCodes;
        //初始标题
        string title;
        string nowCdbFile = "";
        int MaxRow = 20;
        int page = 1, pageNum = 1;
        /// <summary>
        /// 卡片总数
        /// </summary>
        int cardcount;
        /// <summary>
        /// 撤销的sql
        /// </summary>
        string undoString;
        List<Card> cardlist = new List<Card>();
        //setcode正在输入
        bool[] setcodeIsedit = new bool[5];

        Image m_cover;
        MSEConfig msecfg;

        string datapath, confcover;

        public DataEditForm(string datapath, string cdbfile)
        {
            InitPath(datapath);
            Initialize();
            nowCdbFile = cdbfile;
        }

        public DataEditForm(string datapath)
        {
            InitPath(datapath);
            Initialize();
        }
        public DataEditForm()
        {//默认启动
            string dir = MyConfig.readString(MyConfig.TAG_DATA);
            if (string.IsNullOrEmpty(dir))
            {
                Application.Exit();
            }
            datapath = MyPath.Combine(Application.StartupPath, dir);
            InitPath(datapath);
            Initialize();
        }
        void Initialize()
        {
            tmpCodes = new List<string>();
            InitializeComponent();
            title = this.Text;
            nowCdbFile = "";
        }

        #endregion

        #region 接口
        public void SetActived()
        {
            this.Activate();
        }
        public string GetOpenFile()
        {
            return nowCdbFile;
        }
        public bool CanOpen(string file)
        {
            return YGOUtil.isDataBase(file);
        }
        public bool Create(string file)
        {
            return Open(file);
        }
        public bool Save()
        {
            return true;
        }
        #endregion

        #region 窗体
        //窗体第一次加载
        void DataEditFormLoad(object sender, EventArgs e)
        {
            InitListRows();//调整卡片列表的函数
            HideMenu();//是否需要隐藏菜单
            SetTitle();//设置标题
            //加载
            msecfg = new MSEConfig(datapath);
            tasker = new TaskHelper(datapath, bgWorker1, msecfg);
            //设置空白卡片
            oldCard = new Card(0);
            SetCard(oldCard);

            if (nowCdbFile != null && File.Exists(nowCdbFile))
                Open(nowCdbFile);
            //获取MSE配菜单
            AddMenuItemFormMSE();
            //   CheckUpdate(false);//检查更新
        }
        //窗体关闭
        void DataEditFormFormClosing(object sender, FormClosingEventArgs e)
        {
            //当前有任务执行，是否结束
            if (tasker != null && tasker.IsRuning())
            {
                if (!CancelTask())
                {
                    e.Cancel = true;
                    return;
                }

            }
        }
        //窗体激活
        void DataEditFormEnter(object sender, EventArgs e)
        {
            SetTitle();
        }
        #endregion

        #region 初始化设置
        //隐藏菜单
        void HideMenu()
        {
            if (this.MdiParent == null)
                return;
            mainMenu.Visible = false;
            menuitem_file.Visible = false;
            menuitem_file.Enabled = false;
            //this.SuspendLayout();
            this.ResumeLayout(true);
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(MenuStrip))
                    continue;
                Point p = c.Location;
                c.Location = new Point(p.X, p.Y - 25);
            }
            this.ResumeLayout(false);
            //this.PerformLayout();
        }
        //移除Tag
        string RemoveTag(string text)
        {
            int t = text.LastIndexOf(" (");
            if (t > 0)
            {
                return text.Substring(0, t);
            }
            return text;
        }
        //设置标题
        void SetTitle()
        {
            string str = title;
            string str2 = RemoveTag(title);
            if (!string.IsNullOrEmpty(nowCdbFile))
            {
                str = nowCdbFile + "-" + str;
                str2 = Path.GetFileName(nowCdbFile);
            }
            if (this.MdiParent != null) //父容器不为空
            {
                this.Text = str2;
                if (tasker != null && tasker.IsRuning())
                {
                    if (DockPanel.ActiveContent == this)
                        this.MdiParent.Text = str;
                }
                else
                    this.MdiParent.Text = str;
            }
            else
                this.Text = str;

        }
        //按cdb路径设置目录
        void SetCDB(string cdb)
        {
            this.nowCdbFile = cdb;
            SetTitle();
            if (cdb.Length > 0)
            {
                char SEP = Path.DirectorySeparatorChar;
                int l = nowCdbFile.LastIndexOf(SEP);
                GAMEPATH = (l > 0) ? nowCdbFile.Substring(0, l + 1) : cdb;
            }
            else
                GAMEPATH = Application.StartupPath;
            PICPATH = MyPath.Combine(GAMEPATH, "pics");
            PICPATH2 = MyPath.Combine(PICPATH, "thumbnail");
            LUAPTH = MyPath.Combine(GAMEPATH, "script");
        }
        //初始化文件路径
        void InitPath(string datapath)
        {
            this.datapath = datapath;
            confcover = MyPath.Combine(datapath, "cover.jpg");
            if (File.Exists(confcover))
                m_cover = Image.FromFile(confcover);
            else
                m_cover = null;
        }
        #endregion

        #region 界面控件
        //初始化控件
        public void InitControl(DataConfig datacfg)
        {
            if (datacfg == null)
                return;
            //选择框
            InitComboBox(cb_cardrace, datacfg.dicCardRaces);
            InitComboBox(cb_cardattribute, datacfg.dicCardAttributes);
            InitComboBox(cb_cardrule, datacfg.dicCardRules);
            InitComboBox(cb_cardlevel, datacfg.dicCardLevels);
            //卡片类型
            InitCheckPanel(pl_cardtype, datacfg.dicCardTypes);
            //效果类型
            InitCheckPanel(pl_category, datacfg.dicCardcategorys);
            //系列名
            List<long> setcodes = DataManager.GetKeys(datacfg.dicSetnames);
            string[] setnames = DataManager.GetValues(datacfg.dicSetnames);
            InitComboBox(cb_setname1, setcodes, setnames);
            InitComboBox(cb_setname2, setcodes, setnames);
            InitComboBox(cb_setname3, setcodes, setnames);
            InitComboBox(cb_setname4, setcodes, setnames);
            //
        }
        //初始化FlowLayoutPanel
        void InitCheckPanel(FlowLayoutPanel fpanel, Dictionary<long, string> dic)
        {
            fpanel.SuspendLayout();
            fpanel.Controls.Clear();
            foreach (long key in dic.Keys)
            {
                CheckBox _cbox = new CheckBox();
                _cbox.Name = fpanel.Name + key.ToString("x");
                _cbox.Tag = key;//绑定值
                _cbox.Text = dic[key];
                _cbox.AutoSize = true;
                _cbox.Margin = fpanel.Margin;
                _cbox.Click += PanelOnCheckClick;
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
        void InitComboBox(ComboBox cb, Dictionary<long, string> tempdic)
        {
            InitComboBox(cb, DataManager.GetKeys(tempdic),
                DataManager.GetValues(tempdic));
        }
        //初始化ComboBox
        void InitComboBox(ComboBox cb, List<long> keys, string[] values)
        {
            cb.Items.Clear();
            cb.Tag = keys;
            cb.Items.AddRange(values);
            cb.SelectedIndex = 0;
        }
        //计算list最大行数
        void InitListRows()
        {
            if (lv_cardlist.Items.Count == 0)
            {
                ListViewItem item = new ListViewItem();
                item.Text = "Test";
                lv_cardlist.Items.Add(item);
            }
            int headH = lv_cardlist.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Y;
            int itemH = lv_cardlist.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Height;
            if (itemH > 0)
            {
                int n = (lv_cardlist.Height - headH - 4) / itemH;
                if (n > 0)
                    MaxRow = n;
            }
            lv_cardlist.Items.Clear();
            if (MaxRow < 10)
                MaxRow = 20;
        }
        //设置checkbox
        string SetCheck(FlowLayoutPanel fpl, long number)
        {
            long temp;
            string strType = "";
            foreach (Control c in fpl.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cbox = (CheckBox)c;
                    if (cbox.Tag == null)
                        temp = 0;
                    else
                        temp = (long)cbox.Tag;

                    if ((temp & number) == temp && temp != 0)
                    {
                        cbox.Checked = true;
                        strType += "/" + c.Text;
                    }
                    else
                        cbox.Checked = false;
                }
            }
            return strType;
        }
        //设置combobox
        void SetSelect(ComboBox cb, long k)
        {
            if (cb.Tag == null)
            {
                cb.SelectedIndex = 0;
                return;
            }
            List<long> keys = (List<long>)cb.Tag;
            int index = keys.IndexOf(k);
            if (index>=0 && index < cb.Items.Count)
                cb.SelectedIndex = index;
            else
                cb.SelectedIndex = 0;
        }
        //得到所选值
        long GetSelect(ComboBox cb)
        {
            if (cb.Tag == null)
            {
                return 0;
            }
            List<long> keys = (List<long>)cb.Tag;
            int index = cb.SelectedIndex;
            if (index >= keys.Count)
                return 0;
            else
                return keys[index];
        }
        //得到checkbox的总值
        long GetCheck(FlowLayoutPanel fpl)
        {
            long number = 0;
            long temp;
            foreach (Control c in fpl.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cbox = (CheckBox)c;
                    if (cbox.Tag == null)
                        temp = 0;
                    else
                        temp = (long)cbox.Tag;
                    if (cbox.Checked)
                        number += temp;
                }
            }
            return number;
        }
        //添加列表行
        void AddListView(int p)
        {
            int i, j, istart, iend;

            if (p <= 0)
                p = 1;
            else if (p >= pageNum)
                p = pageNum;
            istart = (p - 1) * MaxRow;
            iend = p * MaxRow;
            if (iend > cardcount)
                iend = cardcount;
            page = p;
            lv_cardlist.BeginUpdate();
            lv_cardlist.Items.Clear();
            if ((iend - istart) > 0)
            {
                ListViewItem[] items = new ListViewItem[iend - istart];
                Card mcard;
                for (i = istart, j = 0; i < iend; i++, j++)
                {
                    mcard = cardlist[i];
                    items[j] = new ListViewItem();
                    items[j].Text = mcard.id.ToString();
                    if (mcard.id == oldCard.id)
                        items[j].Checked = true;
                    if (i % 2 == 0)
                        items[j].BackColor = Color.GhostWhite;
                    else
                        items[j].BackColor = Color.White;
                    items[j].SubItems.Add(mcard.name);
                }
                lv_cardlist.Items.AddRange(items);
            }
            lv_cardlist.EndUpdate();
            tb_page.Text = page.ToString();

        }
        #endregion

        #region 设置卡片
        void SetCard(Card c)
        {
            oldCard = c;
            if (c.str == null)
                c.InitStrs();
            tb_cardname.Text = c.name;
            tb_cardtext.Text = c.desc;

            strs = new string[c.str.Length];
            Array.Copy(c.str, strs, c.str.Length);
            lb_scripttext.Items.Clear();
            lb_scripttext.Items.AddRange(c.str);
            tb_edittext.Text = "";
            //data
            SetSelect(cb_cardrule, c.ot);
            SetSelect(cb_cardattribute, c.attribute);
            SetSelect(cb_cardlevel, (c.level & 0xff));
            SetSelect(cb_cardrace, c.race);
            //setcode
            long sc1 = c.setcode & 0xffff;
            long sc2 = (c.setcode >> 0x10) & 0xffff;
            long sc3 = (c.setcode >> 0x20) & 0xffff;
            long sc4 = (c.setcode >> 0x30) & 0xffff;
            tb_setcode1.Text = sc1.ToString("x");
            tb_setcode2.Text = sc2.ToString("x");
            tb_setcode3.Text = sc3.ToString("x");
            tb_setcode4.Text = sc4.ToString("x");
            //SetSelect(cb_setname1, sc1);
            //SetSelect(cb_setname2, sc2);
            //SetSelect(cb_setname3, sc3);
            //SetSelect(cb_setname4, sc4);
            //type,category
            SetCheck(pl_cardtype, c.type);
            SetCheck(pl_category, c.category);
            //Pendulum
            tb_pleft.Text = ((c.level >> 0x18) & 0xff).ToString();
            tb_pright.Text = ((c.level >> 0x10) & 0xff).ToString();
            //atk，def
            tb_atk.Text = (c.atk < 0) ? "?" : c.atk.ToString();
            tb_def.Text = (c.def < 0) ? "?" : c.def.ToString();
            tb_cardcode.Text = c.id.ToString();
            tb_cardalias.Text = c.alias.ToString();
            setImage(c.id.ToString());
        }
        #endregion

        #region 获取卡片
        Card GetCard()
        {
            int temp;
            Card c = new Card(0);
            c.name = tb_cardname.Text;
            c.desc = tb_cardtext.Text;

            Array.Copy(strs, c.str, c.str.Length);

            c.ot = (int)GetSelect(cb_cardrule);
            c.attribute = (int)GetSelect(cb_cardattribute);
            c.level = (int)GetSelect(cb_cardlevel);
            c.race = (int)GetSelect(cb_cardrace);

            //setcode
            int.TryParse(tb_setcode1.Text, NumberStyles.HexNumber, null, out temp);
            c.setcode = temp;
            int.TryParse(tb_setcode2.Text, NumberStyles.HexNumber, null, out temp);
            c.setcode += ((long)temp << 0x10);
            int.TryParse(tb_setcode3.Text, NumberStyles.HexNumber, null, out temp);
            c.setcode += ((long)temp << 0x20);
            int.TryParse(tb_setcode4.Text, NumberStyles.HexNumber, null, out temp);
            c.setcode += ((long)temp << 0x30);
            //c.setcode = getSetcodeByText();

            c.type = GetCheck(pl_cardtype);
            c.category = GetCheck(pl_category);

            int.TryParse(tb_pleft.Text, out temp);
            c.level += (temp << 0x18);
            int.TryParse(tb_pright.Text, out temp);
            c.level += (temp << 0x10);
            if (tb_atk.Text == "?" || tb_atk.Text == "？")
                c.atk = -2;
            else if (tb_atk.Text == ".")
                c.atk = -1;
            else
                int.TryParse(tb_atk.Text, out c.atk);
            if (tb_def.Text == "?" || tb_def.Text == "？")
                c.def = -2;
            else if (tb_def.Text == ".")
                c.def = -1;
            else
                int.TryParse(tb_def.Text, out c.def);
            long.TryParse(tb_cardcode.Text, out c.id);
            long.TryParse(tb_cardalias.Text, out c.alias);

            return c;
        }
        #endregion

        #region 卡片列表
        //列表选择
        void Lv_cardlistSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_cardlist.SelectedItems.Count > 0)
            {
                int sel = lv_cardlist.SelectedItems[0].Index;
                int index = (page - 1) * MaxRow + sel;
                if (index < cardlist.Count)
                {
                    Card c = cardlist[index];
                    SetCard(c);
                }
            }
        }
        //列表按键
        void Lv_cardlistKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete: DelCards(); break;
                case Keys.Right: Btn_PageDownClick(null, null); break;
                case Keys.Left: Btn_PageUpClick(null, null); break;
            }
        }
        //上一页
        void Btn_PageUpClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            page--;
            AddListView(page);
        }
        //下一页
        void Btn_PageDownClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            page++;
            AddListView(page);
        }
        //跳转到指定页数
        void Tb_pageKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int p;
                int.TryParse(tb_page.Text, out p);
                if (p > 0)
                    AddListView(p);
            }
        }
        #endregion

        #region 卡片搜索，打开
        //检查是否打开数据库
        public bool Check()
        {
            if (File.Exists(nowCdbFile))
                return true;
            else
            {
                MyMsg.Warning(LMSG.NotSelectDataBase);
                return false;
            }
        }
        //打开数据库
        public bool Open(string file)
        {
            SetCDB(file);
            if (!File.Exists(file))
            {
                MyMsg.Error(LMSG.FileIsNotExists);
                return false;
            }
            //清空
            tmpCodes.Clear();
            cardlist.Clear();
            //检查表是否存在
            DataBase.CheckTable(file);
            srcCard = new Card();
            SetCards(DataBase.Read(file, true, ""), false);

            return true;
        }
        //设置卡片列表的结果
        public void SetCards(Card[] cards, bool isfresh)
        {
            if (cards != null)
            {
                cardlist.Clear();
                foreach (Card c in cards)
                {
                    if (srcCard.setcode == 0)
                        cardlist.Add(c);//setcode搜索在这里进行
                    else if (c.IsSetCode(srcCard.setcode & 0xffff))
                        cardlist.Add(c);
                }
                cardcount = cardlist.Count;
                pageNum = cardcount / MaxRow;
                if (cardcount % MaxRow > 0)
                    pageNum++;
                else if (cardcount == 0)
                    pageNum = 1;
                tb_pagenum.Text = pageNum.ToString();

                if (isfresh)//是否跳到之前页数
                    AddListView(page);
                else
                    AddListView(1);
            }
            else
            {
                cardcount = 0;
                page = 1;
                pageNum = 1;
                tb_page.Text = page.ToString();
                tb_pagenum.Text = pageNum.ToString();
                cardlist.Clear();
                lv_cardlist.Items.Clear();
                SetCard(new Card(0));
            }
        }
        //搜索卡片
        public void Search(Card c, bool isfresh)
        {
            if (!Check())
                return;
            //如果临时卡片不为空，则更新，这个在搜索的时候清空
            if (tmpCodes.Count > 0)
            {
                Card[] mcards = DataBase.Read(nowCdbFile,
                true, tmpCodes.ToArray());
                SetCards(getCompCards(), true);
            }
            else
            {
                srcCard = c;
                string sql = DataBase.GetSelectSQL(c);
                SetCards(DataBase.Read(nowCdbFile, true, sql), isfresh);
            }
        }
        //更新临时卡片
        public void Reset()
        {
            oldCard = new Card(0);
            SetCard(oldCard);
        }
        #endregion

        #region 卡片编辑
        //添加
        public bool AddCard()
        {
            if (!Check())
                return false;
            Card c = GetCard();
            if (c.id <= 0)//卡片密码不能小于等于0
            {
                MyMsg.Error(LMSG.CodeCanNotIsZero);
                return false;
            }
            foreach (Card ckey in cardlist)//卡片id存在
            {
                if (c.id == ckey.id)
                {
                    MyMsg.Warning(LMSG.ItIsExists);
                    return false;
                }
            }
            if (DataBase.Command(nowCdbFile, DataBase.GetInsertSQL(c, true)) >= 2)
            {
                MyMsg.Show(LMSG.AddSucceed);
                undoString = DataBase.GetDeleteSQL(c);
                Search(srcCard, true);
                return true;
            }
            MyMsg.Error(LMSG.AddFail);
            return false;
        }
        //修改
        public bool ModCard()
        {
            if (!Check())
                return false;
            Card c = GetCard();

            if (c.Equals(oldCard))//没有修改
            {
                MyMsg.Show(LMSG.ItIsNotChanged);
                return false;
            }
            if (c.id <= 0)
            {
                MyMsg.Error(LMSG.CodeCanNotIsZero);
                return false;
            }
            string sql;
            if (c.id != oldCard.id)//修改了id
            {
                if (MyMsg.Question(LMSG.IfDeleteCard))//是否删除旧卡片
                {
                    if (DataBase.Command(nowCdbFile, DataBase.GetDeleteSQL(oldCard)) < 2)
                    {
                        MyMsg.Error(LMSG.DeleteFail);
                        return false;
                    }
                }
                sql = DataBase.GetInsertSQL(c, false);
            }
            else
                sql = DataBase.GetUpdateSQL(c);
            if (DataBase.Command(nowCdbFile, sql) > 0)
            {
                MyMsg.Show(LMSG.ModifySucceed);
                undoString = DataBase.GetDeleteSQL(c);
                undoString += DataBase.GetInsertSQL(oldCard, false);
                Search(srcCard, true);
                SetCard(c);
            }
            else
                MyMsg.Error(LMSG.ModifyFail);
            return false;
        }
        //删除
        public bool DelCards()
        {
            if (!Check())
                return false;
            int ic = lv_cardlist.SelectedItems.Count;
            if (ic == 0)
                return false;
            if (!MyMsg.Question(LMSG.IfDeleteCard))
                return false;
            List<string> sql = new List<string>();
            foreach (ListViewItem lvitem in lv_cardlist.SelectedItems)
            {
                int index = lvitem.Index + (page - 1) * MaxRow;
                if (index < cardlist.Count)
                {
                    Card c = cardlist[index];
                    undoString += DataBase.GetInsertSQL(c, true);
                    sql.Add(DataBase.GetDeleteSQL(c));
                }
            }
            if (DataBase.Command(nowCdbFile, sql.ToArray()) >= (sql.Count * 2))
            {
                MyMsg.Show(LMSG.DeleteSucceed);
                Search(srcCard, true);
                return true;
            }
            else
            {
                MyMsg.Error(LMSG.DeleteFail);
                Search(srcCard, true);
            }

            return false;
        }
        //打开脚本
        public bool OpenScript()
        {
            if (!Check())
                return false;
            string lua = MyPath.Combine(LUAPTH, "c" + tb_cardcode.Text + ".lua");
            if (!File.Exists(lua))
            {
                if (!Directory.Exists(LUAPTH))//创建脚本目录
                    Directory.CreateDirectory(LUAPTH);
                if (MyMsg.Question(LMSG.IfCreateScript))//是否创建脚本
                {
                    using (FileStream fs = new FileStream(
                        lua,
                        FileMode.OpenOrCreate,
                        FileAccess.Write))
                    {
                        StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false));
                        sw.WriteLine("--" + tb_cardname.Text);
                        sw.Close();
                        fs.Close();
                    }
                }
            }
            if (File.Exists(lua))//如果存在，则打开文件
            {
                 System.Diagnostics.Process.Start(lua);
            }
            return false;
        }
        //撤销
        public void Undo()
        {
            if (string.IsNullOrEmpty(undoString))
            {
                return;
            }
            DataBase.Command(nowCdbFile, undoString);
            Search(srcCard, true);
        }

        #endregion

        #region 按钮
        //搜索卡片
        void Btn_serachClick(object sender, EventArgs e)
        {
            tmpCodes.Clear();//清空临时的结果
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
        void Btn_undoClick(object sender, EventArgs e)
        {
            Undo();
        }
        void Btn_imgClick(object sender, EventArgs e)
        {
            string tid = tb_cardcode.Text;
            if (tid == "0" || tid.Length == 0)
                return;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = LANG.GetMsg(LMSG.SelectImage) + "-" + tb_cardname.Text;
                dlg.Filter = LANG.GetMsg(LMSG.ImageType);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //dlg.FileName;
                    ImportImage(dlg.FileName, tid);
                }
            }
        }
        #endregion

        #region 文本框
        //卡片密码搜索
        void Tb_cardcodeKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Card c = new Card(0);
                long.TryParse(tb_cardcode.Text, out c.id);
                if (c.id > 0)
                {
                    tmpCodes.Clear();//清空临时的结果
                    Search(c, false);
                }
            }
        }
        //卡片名称搜索、编辑
        void Tb_cardnameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Card c = new Card(0);
                c.name = tb_cardname.Text;
                if (c.name.Length > 0)
                {
                    tmpCodes.Clear();//清空临时的结果
                    Search(c, false);
                }
            }
        }
        //卡片描述编辑
        void Setscripttext(string str)
        {
            int index = -1;
            try
            {
                index = lb_scripttext.SelectedIndex;
            }
            catch
            {
                index = -1;
                MyMsg.Error(LMSG.NotSelectScriptText);
            }
            if (index >= 0)
            {
                strs[index] = str;

                lb_scripttext.Items.Clear();
                lb_scripttext.Items.AddRange(strs);
                lb_scripttext.SelectedIndex = index;
            }
        }

        string Getscripttext()
        {
            int index = -1;
            try
            {
                index = lb_scripttext.SelectedIndex;
            }
            catch
            {
                index = -1;
                MyMsg.Error(LMSG.NotSelectScriptText);
            }
            if (index >= 0)
                return strs[index];
            else
                return "";
        }
        //脚本文本
        void Lb_scripttextSelectedIndexChanged(object sender, EventArgs e)
        {
            tb_edittext.Text = Getscripttext();
        }

        //脚本文本
        void Tb_edittextTextChanged(object sender, EventArgs e)
        {
            Setscripttext(tb_edittext.Text);
        }
        #endregion

        #region 帮助菜单
        void Menuitem_aboutClick(object sender, EventArgs e)
        {
            MyMsg.Show(
                LANG.GetMsg(LMSG.About) + "\t" + Application.ProductName + "\n"
                + LANG.GetMsg(LMSG.Version) + "\t" + Application.ProductVersion + "\n"
                + LANG.GetMsg(LMSG.Author) + "\t柯永裕\n"
                + "Email:\t247321453@qq.com");
        }

        void Menuitem_checkupdateClick(object sender, EventArgs e)
        {
            CheckUpdate(true);
        }
        public void CheckUpdate(bool showNew)
        {
            if (!isRun())
            {
                tasker.SetTask(MyTask.CheckUpdate, null, showNew.ToString());
                Run(LANG.GetMsg(LMSG.checkUpdate));
            }
        }
        bool CancelTask()
        {
            bool bl = false;
            if (tasker != null && tasker.IsRuning())
            {
                bl = MyMsg.Question(LMSG.IfCancelTask);
                if (bl)
                {
                    if (tasker != null)
                        tasker.Cancel();
                    if (bgWorker1.IsBusy)
                        bgWorker1.CancelAsync();
                }
            }
            return bl;
        }
        void Menuitem_cancelTaskClick(object sender, EventArgs e)
        {
            CancelTask();
        }
        void Menuitem_githubClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MyConfig.readString(MyConfig.TAG_SOURCE_URL));
        }
        #endregion

        #region 文件菜单
        //打开文件
        void Menuitem_openClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = LANG.GetMsg(LMSG.SelectDataBasePath);
                dlg.Filter = LANG.GetMsg(LMSG.CdbType);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Open(dlg.FileName);
                }
            }
        }
       //新建文件
        void Menuitem_newClick(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = LANG.GetMsg(LMSG.SelectDataBasePath);
                dlg.Filter = LANG.GetMsg(LMSG.CdbType);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (DataBase.Create(dlg.FileName))
                    {
                        if (MyMsg.Question(LMSG.IfOpenDataBase))
                            Open(dlg.FileName);
                    }
                }
            }
        }
        //读取ydk
        void Menuitem_readydkClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = LANG.GetMsg(LMSG.SelectYdkPath);
                dlg.Filter = LANG.GetMsg(LMSG.ydkType);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tmpCodes.Clear();
                    string[] ids = YGOUtil.ReadYDK(dlg.FileName);
                    tmpCodes.AddRange(ids);
                    SetCards(DataBase.Read(nowCdbFile, true,
                        ids), false);
                }
            }
        }
        //从图片文件夹读取
        void Menuitem_readimagesClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            using (FolderBrowserDialog fdlg = new FolderBrowserDialog())
            {
                fdlg.Description = LANG.GetMsg(LMSG.SelectImagePath);
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    tmpCodes.Clear();
                    string[] ids = YGOUtil.ReadImage(fdlg.SelectedPath);
                    tmpCodes.AddRange(ids);
                    SetCards(DataBase.Read(nowCdbFile, true,
                        ids), false);
                }
            }
        }
        //关闭
        void Menuitem_quitClick(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 线程
        //是否在执行
        bool isRun()
        {
            if (tasker != null && tasker.IsRuning())
            {
                MyMsg.Warning(LMSG.RunError);
                return true;
            }
            return false;
        }
        //执行任务
        void Run(string name)
        {
            if (isRun())
                return;
            taskname = name;
            title = title + " (" + taskname + ")";
            SetTitle();
            bgWorker1.RunWorkerAsync();
        }
        //线程任务
        void BgWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            tasker.Run();
        }
        void BgWorker1ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            title = string.Format("{0} ({1}-{2})",
                                RemoveTag(title),
                                taskname,
                // e.ProgressPercentage,
                                e.UserState);
            SetTitle();
        }
        //任务完成
        void BgWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //还原标题
            int t = title.LastIndexOf(" (");
            if (t > 0)
            {
                title = title.Substring(0, t);
                SetTitle();
            }
            if (e.Error != null)
            {//出错
                if (tasker != null)
                    tasker.Cancel();
                if (bgWorker1.IsBusy)
                    bgWorker1.CancelAsync();
                MyMsg.Show(LANG.GetMsg(LMSG.TaskError) + "\n" + e.Error);
            }
            else if (tasker.IsCancel() || e.Cancelled)
            {//取消任务
                MyMsg.Show(LMSG.CancelTask);
            }
            else
            {
                MyTask mt = tasker.getLastTask();
                switch (mt)
                {
                    case MyTask.CheckUpdate:
                        break;
                    case MyTask.ExportData:
                        MyMsg.Show(LMSG.ExportDataOK);
                        break;
                    case MyTask.CutImages:
                        MyMsg.Show(LMSG.CutImageOK);
                        break;
                    case MyTask.SaveAsMSE:
                        MyMsg.Show(LMSG.SaveMseOK);
                        break;
                    case MyTask.ConvertImages:
                        MyMsg.Show(LMSG.ConvertImageOK);
                        break;
                    case MyTask.ReadMSE:
                        //保存读取的卡片
                        SaveCards(tasker.CardList);
                        MyMsg.Show(LMSG.ReadMSEisOK);
                        break;
                }
            }
        }
        #endregion

        #region 复制卡片
        //得到卡片列表，是否是选中的
        public Card[] getCardList(bool onlyselect)
        {
            if (!Check())
                return null;
            List<Card> cards = new List<Card>();
            if (onlyselect)
            {
                foreach (ListViewItem lvitem in lv_cardlist.SelectedItems)
                {
                    int index = lvitem.Index + (page - 1) * MaxRow;
                    if (index < cardlist.Count)
                        cards.Add(cardlist[index]);
                }
            }
            else
                cards.AddRange(cardlist.ToArray());
            if (cards.Count == 0)
            {
                MyMsg.Show(LMSG.NoSelectCard);
                return null;
            }
            return cards.ToArray();
        }
        void Menuitem_copytoClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            CopyTo(getCardList(false));
        }

        void Menuitem_copyselecttoClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            CopyTo(getCardList(true));
        }
        //保存卡片到当前数据库
        public void SaveCards(Card[] cards)
        {
            if (!Check())
                return;
            if (cards == null || cards.Length == 0)
                return;
            bool replace = MyMsg.Question(LMSG.IfReplaceExistingCard);
            DataBase.CopyDB(nowCdbFile, !replace, cards);
            Search(srcCard, true);
        }
        //卡片另存为
        void CopyTo(Card[] cards)
        {
            if (cards == null || cards.Length == 0)
                return;
            //select file
            bool replace = false;
            string filename = null;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = LANG.GetMsg(LMSG.SelectDataBasePath);
                dlg.Filter = LANG.GetMsg(LMSG.CdbType);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    replace = MyMsg.Question(LMSG.IfReplaceExistingCard);
                }
            }
            if (!string.IsNullOrEmpty(filename))
            {
                DataBase.CopyDB(filename, !replace, cards);
                MyMsg.Show(LMSG.CopyCardsToDBIsOK);
            }

        }
        #endregion

        #region MSE存档
        //裁剪图片
        void Menuitem_cutimagesClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            if (isRun())
                return;
            bool isreplace = MyMsg.Question(LMSG.IfReplaceExistingImage);
            tasker.SetTask(MyTask.CutImages, cardlist.ToArray(),
                           PICPATH, isreplace.ToString());
            Run(LANG.GetMsg(LMSG.CutImage));
        }
        void Menuitem_saveasmse_selectClick(object sender, EventArgs e)
        {
            SaveAsMSE(true);
        }

        void Menuitem_saveasmseClick(object sender, EventArgs e)
        {
            SaveAsMSE(false);
        }
        void SaveAsMSE(bool onlyselect)
        {
            if (!Check())
                return;
            if (isRun())
                return;
            Card[] cards = getCardList(onlyselect);
            if (cards == null)
                return;
            //select save mse-set
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = LANG.GetMsg(LMSG.selectMseset);
                dlg.Filter = LANG.GetMsg(LMSG.MseType);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool isUpdate = false;
#if DEBUG
					isUpdate=MyMsg.Question(LMSG.OnlySet);
#endif
                    tasker.SetTask(MyTask.SaveAsMSE, cards,
                                   dlg.FileName, isUpdate.ToString());
                    Run(LANG.GetMsg(LMSG.SaveMse));
                }
            }
        }
        #endregion

        #region 导入卡图
        void Pl_imageDragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (File.Exists(files[0]))
                ImportImage(files[0], tb_cardcode.Text);
        }

        void Pl_imageDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }
        private void menuitem_importmseimg_Click(object sender, EventArgs e)
        {
            string tid = tb_cardcode.Text;
            menuitem_importmseimg.Checked = !menuitem_importmseimg.Checked;
            setImage(tid);
        }
        void ImportImage(string file, string tid)
        {
            string f;
            if (pl_image.BackgroundImage != null
               && pl_image.BackgroundImage != m_cover)
            {
                pl_image.BackgroundImage.Dispose();
                pl_image.BackgroundImage = m_cover;
            }
            if (menuitem_importmseimg.Checked)
            {
                if (!Directory.Exists(tasker.MSEImagePath))
                    Directory.CreateDirectory(tasker.MSEImagePath);
                f = MyPath.Combine(tasker.MSEImagePath, tid + ".jpg");
                File.Copy(file, f, true);
            }
            else
            {
                f = MyPath.Combine(PICPATH, tid + ".jpg");
                tasker.ToImg(file, f,
                             MyPath.Combine(PICPATH2, tid + ".jpg"));
            }
            setImage(tid);
        }
        void setImage(string id)
        {
            long t;
            long.TryParse(id, out t);
            setImage(t);
        }
        void setImage(long id)
        {
            if (pl_image.BackgroundImage != null
               && pl_image.BackgroundImage != m_cover)
                pl_image.BackgroundImage.Dispose();
            Bitmap temp;
            string pic = MyPath.Combine(PICPATH, id + ".jpg");
            string pic2 = MyPath.Combine(tasker.MSEImagePath, id + ".jpg");
            string pic3 = MyPath.Combine(tasker.MSEImagePath, new Card(id).idString + ".jpg");
            if (menuitem_importmseimg.Checked && File.Exists(pic2))
            {
                temp = new Bitmap(pic2);
                pl_image.BackgroundImage = temp;
            }
            else if (menuitem_importmseimg.Checked && File.Exists(pic3))
            {
                temp = new Bitmap(pic3);
                pl_image.BackgroundImage = temp;
            }
            else if (File.Exists(pic))
            {
                temp = new Bitmap(pic);
                pl_image.BackgroundImage = temp;
            }
            else
                pl_image.BackgroundImage = m_cover;
        }
        void Menuitem_convertimageClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            if (isRun())
                return;
            using (FolderBrowserDialog fdlg = new FolderBrowserDialog())
            {
                fdlg.Description = LANG.GetMsg(LMSG.SelectImagePath);
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    bool isreplace = MyMsg.Question(LMSG.IfReplaceExistingImage);
                    tasker.SetTask(MyTask.ConvertImages, null,
                                   fdlg.SelectedPath, GAMEPATH, isreplace.ToString());
                    Run(LANG.GetMsg(LMSG.ConvertImage));
                }
            }
        }
        #endregion

        #region 导出数据包
        void Menuitem_exportdataClick(object sender, EventArgs e)
        {
            if (!Check())
                return;
            if (isRun())
                return;
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Zip|(*.zip|All Files(*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tasker.SetTask(MyTask.ExportData, getCardList(false), dlg.FileName);
                    Run(LANG.GetMsg(LMSG.ExportData));
                }
            }

        }
        #endregion

        #region 对比数据
        /// <summary>
        /// 数据一致，返回true，不存在和数据不同，则返回false
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        bool CheckCard(Card[] cards, Card card, bool checkinfo)
        {
            foreach (Card c in cards)
            {
                if (c.id != card.id)
                    continue;
                //data数据不一样
                if (checkinfo)
                    return card.EqualsData(c);
                else
                    return true;
            }
            return false;
        }
        //读取将要对比的数据
        Card[] getCompCards()
        {
            if (tmpCodes.Count == 0)
                return null;
            if (!Check())
                return null;
            return DataBase.Read(nowCdbFile, true, tmpCodes.ToArray());
        }
        public void CompareCards(string cdbfile, bool checktext)
        {
            if (!Check())
                return;
            tmpCodes.Clear();
            srcCard = new Card();
            Card[] mcards = DataBase.Read(nowCdbFile, true, "");
            Card[] cards = DataBase.Read(cdbfile, true, "");
            foreach (Card card in mcards)
            {
                if (!CheckCard(cards, card, checktext))//添加到id集合
                    tmpCodes.Add(card.id.ToString());
            }
            if (tmpCodes.Count == 0)
            {
                SetCards(null, false);
                return;
            }
            SetCards(getCompCards(), false);
        }
        #endregion

        #region MSE配置菜单
        //把文件添加到菜单
        void AddMenuItemFormMSE()
        {
            if(!Directory.Exists(datapath))
                return;
            menuitem_mseconfig.DropDownItems.Clear();//清空
            string[] files = Directory.GetFiles(datapath);
            foreach (string file in files)
            {
                string name = MyPath.getFullFileName(MSEConfig.TAG, file);
                //是否是MSE配置文件
                if (string.IsNullOrEmpty(name))
                    continue;
                //菜单文字是语言
                ToolStripMenuItem tsmi = new ToolStripMenuItem(name);
                tsmi.ToolTipText = file;//提示文字为真实路径
                tsmi.Click += SetMseConfig_Click;
                if (msecfg.configName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    tsmi.Checked = true;//如果是当前，则打勾
                menuitem_mseconfig.DropDownItems.Add(tsmi);
            }
        }
        void SetMseConfig_Click(object sender, EventArgs e)
        {
            if (isRun())//正在执行任务
                return;
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                //读取新的配置
                msecfg.SetConfig(tsmi.ToolTipText, datapath);
                //刷新菜单
                AddMenuItemFormMSE();
                //保存配置
                MyConfig.Save(MyConfig.TAG_MSE, tsmi.Text);
            }
        }
        #endregion

        #region 查找lua函数
        private void menuitem_findluafunc_Click(object sender, EventArgs e)
        {
            string funtxt = MyPath.Combine(datapath, MyConfig.FILE_FUNCTION);
            using (FolderBrowserDialog fd = new FolderBrowserDialog())
            {
                fd.Description = "Folder Name: ocgcore";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    LuaFunction.Read(funtxt);//先读取旧函数列表
                    LuaFunction.Find(fd.SelectedPath);//查找新函数，并保存
                    MessageBox.Show("OK");
                }
            }
        }

        #endregion

        #region 系列名textbox
        //系列名输入时
        void setCode_InputText(int index, ComboBox cb, TextBox tb)
        {
            if(index>=0 && index < setcodeIsedit.Length)
            {
                if (setcodeIsedit[index])//如果正在编辑
                    return;
                setcodeIsedit[index] = true;
                int temp;
                int.TryParse(tb.Text, NumberStyles.HexNumber, null, out temp);
                //tb.Text = temp.ToString("x");
                if (temp == 0 && (tb.Text != "0" || tb.Text.Length == 0))
                    temp = -1;
                SetSelect(cb, temp);
                setcodeIsedit[index] = false;
            }
        }
        private void tb_setcode1_TextChanged(object sender, EventArgs e)
        {
            setCode_InputText(1, cb_setname1, tb_setcode1);
        }

        private void tb_setcode2_TextChanged(object sender, EventArgs e)
        {
            setCode_InputText(2, cb_setname2, tb_setcode2);
        }

        private void tb_setcode3_TextChanged(object sender, EventArgs e)
        {
            setCode_InputText(3, cb_setname3, tb_setcode3);
        }

        private void tb_setcode4_TextChanged(object sender, EventArgs e)
        {
            setCode_InputText(4, cb_setname4, tb_setcode4);
        }
        #endregion

        #region 系列名comobox
        //系列选择框 选择时
        void setCode_Selected(int index, ComboBox cb, TextBox tb)
        {
            if (index >= 0 && index < setcodeIsedit.Length)
            {
                if (setcodeIsedit[index])//如果正在编辑
                    return;
                setcodeIsedit[index] = true;
                long tmp = GetSelect(cb);
                tb.Text = tmp.ToString("x");
                setcodeIsedit[index] = false;
            }
        }
        private void cb_setname1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCode_Selected(1, cb_setname1, tb_setcode1);
        }

        private void cb_setname2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCode_Selected(2, cb_setname2, tb_setcode2);
        }

        private void cb_setname3_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCode_Selected(3, cb_setname3, tb_setcode3);
        }

        private void cb_setname4_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCode_Selected(4, cb_setname4, tb_setcode4);
        }
        #endregion

        #region 读取MSE存档
        private void menuitem_readmse_Click(object sender, EventArgs e)
        {
            if (!Check())
                return;
            if (isRun())
                return;
            //select open mse-set
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = LANG.GetMsg(LMSG.selectMseset);
                dlg.Filter = LANG.GetMsg(LMSG.MseType);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool isUpdate = false;//是否替换存在的图片
                    isUpdate = MyMsg.Question(LMSG.IfReplaceExistingImage);
                    tasker.SetTask(MyTask.ReadMSE, null,
                                   dlg.FileName, isUpdate.ToString());
                    Run(LANG.GetMsg(LMSG.ReadMSE));
                }
            }
        }
        #endregion

        private void menuitem_compdb_Click(object sender, EventArgs e)
        {
            if (!Check())
                return;
            DataBase.Compression(nowCdbFile);
            MyMsg.Show(LMSG.CompDBOK);
        }
    }
}
