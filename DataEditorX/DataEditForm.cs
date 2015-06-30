/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 20:22
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using DataEditorX.Common;
using DataEditorX.Core;
using DataEditorX.Language;
using WeifenLuo.WinFormsUI.Docking;

using DataEditorX.Config;
using DataEditorX.Core.Mse;

namespace DataEditorX
{
	public partial class DataEditForm : DockContent, IDataForm
	{
		#region 成员变量/构造
		TaskHelper tasker = null;
		string taskname;
		//目录
		YgoPath ygopath;
		/// <summary>当前卡片</summary>
		Card oldCard = new Card(0);
		/// <summary>搜索条件</summary>
		Card srcCard = new Card(0);
		//卡片编辑
		CardEdit cardedit;
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
		/// 搜索结果
		/// </summary>
		List<Card> cardlist = new List<Card>();
		//setcode正在输入
		bool[] setcodeIsedit = new bool[5];

		Image m_cover;
		MSEConfig msecfg;

		string datapath, confcover;

		public DataEditForm(string datapath, string cdbfile)
		{
			Initialize(datapath);
			nowCdbFile = cdbfile;
		}

		public DataEditForm(string datapath)
		{
			Initialize(datapath);
		}
		public DataEditForm()
		{//默认启动
			string dir = MyConfig.readString(MyConfig.TAG_DATA);
			if (string.IsNullOrEmpty(dir))
			{
				Application.Exit();
			}
			datapath = MyPath.Combine(Application.StartupPath, dir);

			Initialize(datapath);
		}
		void Initialize(string datapath)
		{
			cardedit = new CardEdit(this);
			tmpCodes = new List<string>();
			ygopath = new YgoPath(Application.StartupPath);
			InitPath(datapath);
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
			//删除资源
			menuitem_operacardsfile.Checked = MyConfig.readBoolean(MyConfig.TAG_DELETE_WITH);
			//用CodeEditor打开脚本
			menuitem_openfileinthis.Checked = MyConfig.readBoolean(MyConfig.TAG_OPEN_IN_THIS);
			//自动检查更新
			menuitem_autocheckupdate.Checked = MyConfig.readBoolean(MyConfig.TAG_AUTO_CHECK_UPDATE);
			if (nowCdbFile != null && File.Exists(nowCdbFile))
				Open(nowCdbFile);
			//获取MSE配菜单
			AddMenuItemFormMSE();
			//
			GetLanguageItem();
			//   CheckUpdate(false);//检查更新
		}
		//窗体关闭
		void DataEditFormFormClosing(object sender, FormClosingEventArgs e)
		{
			//清理备份文件
			List<long> delids = new List<long>();
			foreach (CardEdit.FileDeleted deleted in cardedit.undoDeleted)
			{
				if (deleted != null && deleted.deleted)
					delids.AddRange(deleted.ids);
			}
			if (delids.Count != 0)
			{
				foreach (long id in delids)
					YGOUtil.CardDelete(id, GetPath(), YGOUtil.DeleteOption.CLEAN);
			}
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
			string path = Application.StartupPath;
			if (cdb.Length > 0)
			{
				path = Path.GetDirectoryName(cdb);
			}
			ygopath.SetPath(path);
		}
		//初始化文件路径
		void InitPath(string datapath)
		{
			this.datapath = datapath;
			confcover = MyPath.Combine(datapath, "cover.jpg");
			if (File.Exists(confcover))
				m_cover = MyBitmap.readImage(confcover);
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
				//_cbox.Name = fpanel.Name + key.ToString("x");
				_cbox.Tag = key;//绑定值
				_cbox.Text = dic[key];
				_cbox.AutoSize = true;
				_cbox.Margin = fpanel.Margin;
				//_cbox.Click += PanelOnCheckClick;
				fpanel.Controls.Add(_cbox);
			}
			fpanel.ResumeLayout(false);
			fpanel.PerformLayout();
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
		void SetCheck(FlowLayoutPanel fpl, long number)
		{
			long temp;
			//string strType = "";
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
						//strType += "/" + c.Text;
					}
					else
						cbox.Checked = false;
				}
			}
			//return strType;
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
			if (index >= 0 && index < cb.Items.Count)
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
					items[j].Tag = i;
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
		public YgoPath GetPath()
		{
			return ygopath;
		}
		public Card GetOldCard()
		{
			return oldCard;
		}
		public void SetCard(Card c)
		{
			oldCard = c;

			tb_cardname.Text = c.name;
			tb_cardtext.Text = c.desc;

			strs = new string[c.Str.Length];
			Array.Copy(c.Str, strs, Card.STR_MAX);
			lb_scripttext.Items.Clear();
			lb_scripttext.Items.AddRange(c.Str);
			tb_edittext.Text = "";
			//data
			SetSelect(cb_cardrule, c.ot);
			SetSelect(cb_cardattribute, c.attribute);
			SetSelect(cb_cardlevel, (c.level & 0xff));
			SetSelect(cb_cardrace, c.race);
			//setcode
			long[] setcodes = c.GetSetCode();
			tb_setcode1.Text = setcodes[0].ToString("x");
			tb_setcode2.Text = setcodes[1].ToString("x");
			tb_setcode3.Text = setcodes[2].ToString("x");
			tb_setcode4.Text = setcodes[3].ToString("x");
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
			SetImage(c.id.ToString());
		}
		#endregion

		#region 获取卡片
		public Card GetCard()
		{
			int temp;
			Card c = new Card(0);
			c.name = tb_cardname.Text;
			c.desc = tb_cardtext.Text;

			Array.Copy(strs, c.Str, Card.STR_MAX);

			c.ot = (int)GetSelect(cb_cardrule);
			c.attribute = (int)GetSelect(cb_cardattribute);
			c.level = (int)GetSelect(cb_cardlevel);
			c.race = (int)GetSelect(cb_cardrace);
			//系列
			c.SetSetCode(
				tb_setcode1.Text,
				tb_setcode2.Text,
				tb_setcode3.Text,
				tb_setcode4.Text);

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
				case Keys.Delete:
					cardedit.DelCards(menuitem_operacardsfile.Checked);
					break;
				case Keys.Right:
					Btn_PageDownClick(null, null);
					break;
				case Keys.Left:
					Btn_PageUpClick(null, null);
					break;
			}
		}
		//上一页
		void Btn_PageUpClick(object sender, EventArgs e)
		{
			if (!CheckOpen())
				return;
			page--;
			AddListView(page);
		}
		//下一页
		void Btn_PageDownClick(object sender, EventArgs e)
		{
			if (!CheckOpen())
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
		public bool CheckOpen()
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
		//setcode, 灵摆刻度的搜索
		public bool CardFilter(Card c, Card sc)
		{
			bool res = true;
			if (sc.setcode != 0)
				res &= c.IsSetCode(sc.setcode & 0xffff);
			if (sc.GetLeftScale() != 0 )
				res &= (c.GetLeftScale() == sc.GetLeftScale());
			if (sc.GetRightScale() != 0 )
				res &= (c.GetRightScale() == sc.GetRightScale());
			return res;
		}
		//设置卡片列表的结果
		public void SetCards(Card[] cards, bool isfresh)
		{
			if (cards != null)
			{
				cardlist.Clear();
				foreach (Card c in cards)
				{
					if (CardFilter(c, srcCard))
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
			{//结果为空
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
		public void Search(bool isfresh)
		{
			Search(srcCard, isfresh);
		}
		void Search(Card c, bool isfresh)
		{
			if (!CheckOpen())
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
			if(cardedit != null)
				cardedit.AddCard();
			if (cardedit.undoSQL.Count != 0)
				btn_undo.Enabled = true;
		}
		//修改
		void Btn_modClick(object sender, EventArgs e)
		{
			if (cardedit != null)
				cardedit.ModCard(menuitem_operacardsfile.Checked);
			if (cardedit.undoSQL.Count != 0)
				btn_undo.Enabled = true;
		}
		//打开脚本
		void Btn_luaClick(object sender, EventArgs e)
		{
			if (cardedit != null)
				cardedit.OpenScript(menuitem_openfileinthis.Checked);
		}
		//删除
		void Btn_delClick(object sender, EventArgs e)
		{
			if (cardedit != null)
				cardedit.DelCards(menuitem_operacardsfile.Checked);
			if (cardedit.undoSQL.Count != 0)
				btn_undo.Enabled = true;
		}
		void Btn_undoClick(object sender, EventArgs e)
		{
			if (cardedit != null)
				cardedit.Undo();
			if (cardedit.undoSQL.Count == 0)
				btn_undo.Enabled = false;
		}
		//导入卡图
		void Btn_imgClick(object sender, EventArgs e)
		{
			ImportImageFromSelect();
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
				LanguageHelper.GetMsg(LMSG.About) + "\t" + Application.ProductName + "\n"
				+ LanguageHelper.GetMsg(LMSG.Version) + "\t" + Application.ProductVersion + "\n"
				+ LanguageHelper.GetMsg(LMSG.Author) + "\t柯永裕\n"
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
				Run(LanguageHelper.GetMsg(LMSG.checkUpdate));
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
				dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
				dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
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
				dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
				dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
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
			if (!CheckOpen())
				return;
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Title = LanguageHelper.GetMsg(LMSG.SelectYdkPath);
				dlg.Filter = LanguageHelper.GetMsg(LMSG.ydkType);
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
			if (!CheckOpen())
				return;
			using (FolderBrowserDialog fdlg = new FolderBrowserDialog())
			{
				fdlg.Description = LanguageHelper.GetMsg(LMSG.SelectImagePath);
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
				MyMsg.Show(LanguageHelper.GetMsg(LMSG.TaskError) + "\n" + e.Error);
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
		public Card[] GetCardList(bool onlyselect)
		{
			if (!CheckOpen())
				return null;
			List<Card> cards = new List<Card>();
			if (onlyselect)
			{
				foreach (ListViewItem lvitem in lv_cardlist.SelectedItems)
				{
					int index;
					if (lvitem.Tag != null)
						index = (int)lvitem.Tag;
					else
						index = lvitem.Index + (page - 1) * MaxRow;
					if (index>=0 && index < cardlist.Count)
						cards.Add(cardlist[index]);
				}
			}
			else
				cards.AddRange(cardlist.ToArray());
			if (cards.Count == 0)
			{
				//MyMsg.Show(LMSG.NoSelectCard);
			}
			return cards.ToArray();
		}
		void Menuitem_copytoClick(object sender, EventArgs e)
		{
			if (!CheckOpen())
				return;
			CopyTo(GetCardList(false));
		}

		void Menuitem_copyselecttoClick(object sender, EventArgs e)
		{
			if (!CheckOpen())
				return;
			CopyTo(GetCardList(true));
		}
		//保存卡片到当前数据库
		public void SaveCards(Card[] cards)
		{
			if (!CheckOpen())
				return;
			if (cards == null || cards.Length == 0)
				return;
			bool replace = false;
			Card[] oldcards = DataBase.Read(nowCdbFile, true, "");
			if (oldcards != null && oldcards.Length != 0)
			{
				int i = 0;
				foreach (Card oc in oldcards)
				{
					foreach (Card c in cards)
					{
						if (c.id == oc.id)
						{
							i += 1;
							if (i == 1)
							{
								replace = MyMsg.Question(LMSG.IfReplaceExistingCard);
								break;
							}
						}
					}
					if (i > 0)
						break;
				}
			}
			cardedit.undoSQL.Add("");
			cardedit.undoModified.Add(new CardEdit.FileModified());
			cardedit.undoDeleted.Add(new CardEdit.FileDeleted());
			DataBase.CopyDB(nowCdbFile, !replace, cards);
			CardEdit.DBcopied copied = new CardEdit.DBcopied();
			copied.copied = true;
			copied.NewCards = cards;
			copied.replace = replace;
			copied.OldCards = oldcards;
			cardedit.undoCopied.Add(copied);
			Search(srcCard, true);
			btn_undo.Enabled = true;
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
				dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
				dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
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

		#region MSE存档/裁剪图片
		//裁剪图片
		void Menuitem_cutimagesClick(object sender, EventArgs e)
		{
			if (!CheckOpen())
				return;
			if (isRun())
				return;
			bool isreplace = MyMsg.Question(LMSG.IfReplaceExistingImage);
			tasker.SetTask(MyTask.CutImages, cardlist.ToArray(),
			               ygopath.picpath, isreplace.ToString());
			Run(LanguageHelper.GetMsg(LMSG.CutImage));
		}
		void Menuitem_saveasmse_selectClick(object sender, EventArgs e)
		{
			//选择
			SaveAsMSE(true);
		}

		void Menuitem_saveasmseClick(object sender, EventArgs e)
		{
			//全部
			SaveAsMSE(false);
		}
		void SaveAsMSE(bool onlyselect)
		{
			if (!CheckOpen())
				return;
			if (isRun())
				return;
			Card[] cards = GetCardList(onlyselect);
			if (cards == null)
				return;
			//select save mse-set
			using (SaveFileDialog dlg = new SaveFileDialog())
			{
				dlg.Title = LanguageHelper.GetMsg(LMSG.selectMseset);
				dlg.Filter = LanguageHelper.GetMsg(LMSG.MseType);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					bool isUpdate = false;
					#if DEBUG
					isUpdate=MyMsg.Question(LMSG.OnlySet);
					#endif
					tasker.SetTask(MyTask.SaveAsMSE, cards,
					               dlg.FileName, isUpdate.ToString());
					Run(LanguageHelper.GetMsg(LMSG.SaveMse));
				}
			}
		}
		#endregion

		#region 导入卡图
		void ImportImageFromSelect()
		{
			string tid = tb_cardcode.Text;
			if (tid == "0" || tid.Length == 0)
				return;
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Title = LanguageHelper.GetMsg(LMSG.SelectImage) + "-" + tb_cardname.Text;
				dlg.Filter = LanguageHelper.GetMsg(LMSG.ImageType);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					//dlg.FileName;
					ImportImage(dlg.FileName, tid);
				}
			}
		}
		private void pl_image_DoubleClick(object sender, EventArgs e)
		{
			ImportImageFromSelect();
		}
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
			SetImage(tid);
		}
		void ImportImage(string file, string tid)
		{
			string f;
			if (pl_image.BackgroundImage != null
			    && pl_image.BackgroundImage != m_cover)
			{//释放图片资源
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
				tasker.ToImg(file, ygopath.GetImage(tid),
				             ygopath.GetImageThum(tid));
			}
			SetImage(tid);
		}
		public void SetImage(string id)
		{
			long t;
			long.TryParse(id, out t);
			SetImage(t);
		}
		public void SetImage(long id)
		{
			string pic = ygopath.GetImage(id);
			if (menuitem_importmseimg.Checked)//显示MSE图片
			{
				string msepic = MseMaker.GetCardImagePath(tasker.MSEImagePath, oldCard);
				if(File.Exists(msepic))
				{
					pl_image.BackgroundImage = MyBitmap.readImage(msepic);
				}
			}
			else if (File.Exists(pic))
			{
				pl_image.BackgroundImage = MyBitmap.readImage(pic);
			}
			else
				pl_image.BackgroundImage = m_cover;
		}
		void Menuitem_convertimageClick(object sender, EventArgs e)
		{
			if (!CheckOpen())
				return;
			if (isRun())
				return;
			using (FolderBrowserDialog fdlg = new FolderBrowserDialog())
			{
				fdlg.Description = LanguageHelper.GetMsg(LMSG.SelectImagePath);
				if (fdlg.ShowDialog() == DialogResult.OK)
				{
					bool isreplace = MyMsg.Question(LMSG.IfReplaceExistingImage);
					tasker.SetTask(MyTask.ConvertImages, null,
					               fdlg.SelectedPath, ygopath.gamepath, isreplace.ToString());
					Run(LanguageHelper.GetMsg(LMSG.ConvertImage));
				}
			}
		}
		#endregion

		#region 导出数据包
		void Menuitem_exportdataClick(object sender, EventArgs e)
		{
			if (!CheckOpen())
				return;
			if (isRun())
				return;
			using (SaveFileDialog dlg = new SaveFileDialog())
			{
				dlg.InitialDirectory = ygopath.gamepath;
				dlg.Filter = "Zip|(*.zip|All Files(*.*)|*.*";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					tasker.SetTask(MyTask.ExportData,
					               GetCardList(false),
					               ygopath.gamepath, dlg.FileName);
					Run(LanguageHelper.GetMsg(LMSG.ExportData));
				}
			}

		}
		#endregion

		#region 对比数据
		/// <summary>
		/// 数据一致，返回true，不存在和数据不同，则返回false
		/// </summary>
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
			if (!CheckOpen())
				return null;
			return DataBase.Read(nowCdbFile, true, tmpCodes.ToArray());
		}
		public void CompareCards(string cdbfile, bool checktext)
		{
			if (!CheckOpen())
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
			if (!CheckOpen())
				return;
			if (isRun())
				return;
			//select open mse-set
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Title = LanguageHelper.GetMsg(LMSG.selectMseset);
				dlg.Filter = LanguageHelper.GetMsg(LMSG.MseType);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					bool isUpdate = false;//是否替换存在的图片
					isUpdate = MyMsg.Question(LMSG.IfReplaceExistingImage);
					tasker.SetTask(MyTask.ReadMSE, null,
					               dlg.FileName, isUpdate.ToString());
					Run(LanguageHelper.GetMsg(LMSG.ReadMSE));
				}
			}
		}
		#endregion
		
		#region 压缩数据库
		private void menuitem_compdb_Click(object sender, EventArgs e)
		{
			if (!CheckOpen())
				return;
			DataBase.Compression(nowCdbFile);
			MyMsg.Show(LMSG.CompDBOK);
		}
		#endregion

		#region 设置
		//删除卡片的时候，是否要删除图片和脚本
		private void menuitem_deletecardsfile_Click(object sender, EventArgs e)
		{
			menuitem_operacardsfile.Checked = !menuitem_operacardsfile.Checked;
			MyConfig.Save(MyConfig.TAG_DELETE_WITH, menuitem_operacardsfile.Checked.ToString().ToLower());
		}
		//用CodeEditor打开lua
		private void menuitem_openfileinthis_Click(object sender, EventArgs e)
		{
			menuitem_openfileinthis.Checked = !menuitem_openfileinthis.Checked;
			MyConfig.Save(MyConfig.TAG_OPEN_IN_THIS, menuitem_openfileinthis.Checked.ToString().ToLower());
		}
		//自动检查更新
		private void menuitem_autocheckupdate_Click(object sender, EventArgs e)
		{
			menuitem_autocheckupdate.Checked = !menuitem_autocheckupdate.Checked;
			MyConfig.Save(MyConfig.TAG_AUTO_CHECK_UPDATE, menuitem_autocheckupdate.Checked.ToString().ToLower());
		}
		#endregion

		#region 语言菜单
		void GetLanguageItem()
		{
			if (!Directory.Exists(datapath))
				return;
			menuitem_language.DropDownItems.Clear();
			string[] files = Directory.GetFiles(datapath);
			foreach (string file in files)
			{
				string name = MyPath.getFullFileName(MyConfig.TAG_LANGUAGE, file);
				if (string.IsNullOrEmpty(name))
					continue;
				TextInfo txinfo = new CultureInfo(CultureInfo.InstalledUICulture.Name).TextInfo;
				ToolStripMenuItem tsmi = new ToolStripMenuItem(txinfo.ToTitleCase(name));
				tsmi.ToolTipText = file;
				tsmi.Click += SetLanguage_Click;
				if (MyConfig.readString(MyConfig.TAG_LANGUAGE).Equals(name, StringComparison.OrdinalIgnoreCase))
					tsmi.Checked = true;
				menuitem_language.DropDownItems.Add(tsmi);
			}
		}
		void SetLanguage_Click(object sender, EventArgs e)
		{
			if (isRun())
				return;
			if (sender is ToolStripMenuItem)
			{
				ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
				MyConfig.Save(MyConfig.TAG_LANGUAGE, tsmi.Text);
				GetLanguageItem();
				MyMsg.Show(LMSG.PlzRestart);
			}
		}
		#endregion
		
		//把mse存档导出为图片
		void Menuitem_exportMSEimageClick(object sender, EventArgs e)
		{
			if (isRun())
				return;
			//select open mse-set
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Title = LanguageHelper.GetMsg(LMSG.selectMseset);
				dlg.Filter = LanguageHelper.GetMsg(LMSG.MseType);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string mseset=dlg.FileName;
					string msepath=MyConfig.readString(MyConfig.TAG_MSE_PATH);
					MseMaker.exportSet(msepath, mseset, MyPath.Combine(Application.StartupPath, "cache"));
				}
			}
		}
	}
}
