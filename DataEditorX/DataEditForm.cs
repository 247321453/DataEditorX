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
using DataEditorX.Language;
using WeifenLuo.WinFormsUI.Docking;

namespace DataEditorX
{
	public partial class DataEditForm : DockContent
	{
		#region 成员变量
		TaskHelper tasker=null;
		string taskname;
		string ydkfile=null;
		string imagepath=null;
		string GAMEPATH,PICPATH,PICPATH2,LUAPTH;
		/// <summary>当前卡片</summary>
		Card oldCard=new Card(0);
		/// <summary>搜索条件</summary>
		Card srcCard=new Card(0);
		string[] strs=null;
		Dictionary<long,Card> complist;
		string title;
		string nowCdbFile="";
		int MaxRow=20;
		int page=1,pageNum=1;
		int cardcount;
		string undoString;
		List<Card> cardlist=new List<Card>();
		bool setcodeIsedit1;
		bool setcodeIsedit2;
		bool setcodeIsedit3;
		bool setcodeIsedit4;
		
		Image m_cover;
		DataConfig datacfg;
		string datapath, confcover;
		
		public string getNowCDB()
		{
			return nowCdbFile;
		}
		public DataEditForm(string datapath,string cdbfile)
		{
			InitPath(datapath);
			Initialize();
			nowCdbFile=cdbfile;
		}
		
		public DataEditForm(string datapath)
		{
			InitPath(datapath);
			Initialize();
		}
		public DataEditForm()
		{
			string dir=ConfigurationManager.AppSettings["language"];
			if(string.IsNullOrEmpty(dir))
			{
				Application.Exit();
			}
			datapath=MyPath.Combine(Application.StartupPath, dir);
			InitPath(datapath);
			Initialize();
		}
		void Initialize()
		{
			datacfg=null;
			complist=new Dictionary<long, Card>();
			InitializeComponent();
			title=this.Text;
			nowCdbFile="";
		}
		
		#endregion
		
		#region 界面初始化
		//窗体第一次加载
		void DataEditFormLoad(object sender, EventArgs e)
		{
			InitListRows();
			//界面初始化

			HideMenu();
			
			#if DEBUG
			title=title+"(DEBUG)";
			#endif
			SetTitle();
			
			if(datacfg==null){
				datacfg=new DataConfig(datapath);
				datacfg.Init();
			}
			tasker=new TaskHelper(datapath, bgWorker1,
			                      datacfg.dicCardTypes,
			                      datacfg.dicCardRaces);
			
			SetCDB(nowCdbFile);
			//设置空白卡片
			oldCard=new Card(0);
			SetCard(oldCard);
			
			if(File.Exists(nowCdbFile))
				Open(nowCdbFile);
			//checkupdate(false);
		}
		//窗体关闭
		void DataEditFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if(tasker!=null && tasker.IsRuning())
			{
				if(!CancelTask())
				{
					e.Cancel=true;
					return;
				}
				
			}
		}
		void DataEditFormEnter(object sender, EventArgs e)
		{
			SetTitle();
		}
		void HideMenu()
		{
			if(this.MdiParent ==null)
				return;
			menuStrip1.Visible=false;
			menuitem_file.Visible=false;
			menuitem_file.Enabled=false;
			//this.SuspendLayout();
			this.ResumeLayout(true);
			foreach(Control c in this.Controls)
			{
				if(c.GetType()==typeof(MenuStrip))
					continue;
				Point p=c.Location;
				c.Location=new Point(p.X, p.Y-25);
			}
			this.ResumeLayout(false);
			//this.PerformLayout();
		}
		
		string RemoveTag(string text)
		{
			int t=text.LastIndexOf(" (");
			if(t>0)
			{
				return text.Substring(0,t);
			}
			return text;
		}
		void SetTitle()
		{
			string str=title;
			string str2=RemoveTag(title);
			if(!string.IsNullOrEmpty(nowCdbFile)){
				str=nowCdbFile+"-"+str;
				str2=Path.GetFileName(nowCdbFile);
			}
			if(this.MdiParent !=null)
			{
				this.Text=str2;
				if(tasker!=null && tasker.IsRuning()){
					if(DockPanel.ActiveContent == this)
						this.MdiParent.Text=str;
				}
				else
					this.MdiParent.Text=str;
			}
			else
				this.Text=str;
			
		}
		//按cdb路径设置目录
		void SetCDB(string cdb)
		{
			this.nowCdbFile=cdb;
			SetTitle();
			if(cdb.Length>0)
			{
				char SEP=Path.DirectorySeparatorChar;
				int l=nowCdbFile.LastIndexOf(SEP);
				GAMEPATH=(l>0)?nowCdbFile.Substring(0,l+1):cdb;
			}
			else
				GAMEPATH=Application.StartupPath;
			PICPATH=MyPath.Combine(GAMEPATH,"pics");
			PICPATH2=MyPath.Combine(PICPATH,"thumbnail");
			LUAPTH=MyPath.Combine(GAMEPATH,"script");
		}
		//初始化文件路径
		void InitPath(string datapath)
		{
			this.datapath=datapath;
			confcover= MyPath.Combine(datapath, "cover.jpg");			
			if(File.Exists(confcover))
				m_cover=Image.FromFile(confcover);
			else
				m_cover=null;
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
		public void InitGameData(DataConfig dataconfig)
		{
			//初始化
			this.datacfg=dataconfig.Clone();
			InitControl();
		}
		void InitControl()
		{
			InitComboBox(cb_cardrace, datacfg.dicCardRaces);
			InitComboBox(cb_cardattribute, datacfg.dicCardAttributes);
			InitComboBox(cb_cardrule, datacfg.dicCardRules);
			InitComboBox(cb_cardlevel, datacfg.dicCardLevels);
			//card types
			InitCheckPanel(pl_cardtype, datacfg.dicCardTypes);
			//card categorys
			InitCheckPanel(pl_category, datacfg.dicCardcategorys);
			//setname
			string[] setnames=DataManager.GetValues(datacfg.dicSetnames);
			cb_setname1.Items.AddRange(setnames);
			cb_setname2.Items.AddRange(setnames);
			cb_setname3.Items.AddRange(setnames);
			cb_setname4.Items.AddRange(setnames);
			//
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
		void InitComboBox(ComboBox cb, Dictionary<long, string> tempdic)
		{
			cb.Items.Clear();
			cb.Items.AddRange(DataManager.GetValues(tempdic));
			cb.SelectedIndex=0;
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

			SetSelect(datacfg.dicCardRules,cb_cardrule,(long)c.ot);
			SetSelect(datacfg.dicCardAttributes,cb_cardattribute,(long)c.attribute);
			SetSelect(datacfg.dicCardLevels,cb_cardlevel,(long)(c.level&0xff));
			SetSelect(datacfg.dicCardRaces,cb_cardrace,c.race);
			
			long sc1=c.setcode&0xffff;
			long sc2=(c.setcode>>0x10)&0xffff;
			long sc3=(c.setcode>>0x20)&0xffff;
			long sc4=(c.setcode>>0x30)&0xffff;
			tb_setcode1.Text=sc1.ToString("x");
			tb_setcode2.Text=sc2.ToString("x");
			tb_setcode3.Text=sc3.ToString("x");
			tb_setcode4.Text=sc4.ToString("x");
			SetSelect(datacfg.dicSetnames, cb_setname1, sc1);
			SetSelect(datacfg.dicSetnames, cb_setname2, sc2);
			SetSelect(datacfg.dicSetnames, cb_setname3, sc3);
			SetSelect(datacfg.dicSetnames, cb_setname4, sc4);
			
			SetCheck(pl_cardtype,c.type);
			SetCheck(pl_category,c.category);
			
			tb_pleft.Text=((c.level >> 0x18) & 0xff).ToString();
			tb_pright.Text=((c.level >> 0x10) & 0xff).ToString();
			tb_atk.Text=(c.atk<0)?"?":c.atk.ToString();
			tb_def.Text=(c.def<0)?"?":c.def.ToString();
			tb_cardcode.Text=c.id.ToString();
			tb_cardalias.Text=c.alias.ToString();
			setImage(c.id.ToString());
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
			Card c=new Card(0);
			c.name=tb_cardname.Text;
			c.desc=tb_cardtext.Text;
			
			Array.Copy(strs,c.str, c.str.Length);
			int.TryParse(GetSelect(datacfg.dicCardRules,cb_cardrule),out c.ot);
			int.TryParse(GetSelect(datacfg.dicCardAttributes,cb_cardattribute),out c.attribute);
			long.TryParse(GetSelect(datacfg.dicCardLevels,cb_cardlevel),out c.level);
			long.TryParse(GetSelect(datacfg.dicCardRaces,cb_cardrace),out c.race);
			
			int.TryParse(tb_setcode1.Text, NumberStyles.HexNumber,null,out temp);
			c.setcode =temp;
			int.TryParse(tb_setcode2.Text, NumberStyles.HexNumber,null,out temp);
			c.setcode +=((long)temp<<0x10);
			int.TryParse(tb_setcode3.Text, NumberStyles.HexNumber,null,out temp);
			c.setcode +=((long)temp<<0x20);
			int.TryParse(tb_setcode4.Text, NumberStyles.HexNumber,null,out temp);
			c.setcode +=((long)temp<<0x30);
			//c.setcode = getSetcodeByText();
			
			c.type=GetCheck(pl_cardtype);
			c.category=GetCheck(pl_category);
			
			int.TryParse(tb_pleft.Text,out temp);
			c.level+=(temp << 0x18);
			int.TryParse(tb_pright.Text,out temp);
			c.level+=(temp << 0x10);
			if(tb_atk.Text=="?"||tb_atk.Text=="？")
				c.atk=-2;
			else if(tb_atk.Text==".")
				c.atk=-1;
			else
				int.TryParse( tb_atk.Text,out c.atk);
			if(tb_def.Text=="?"||tb_def.Text=="？")
				c.def=-2;
			else if(tb_def.Text==".")
				c.def=-1;
			else
				int.TryParse( tb_def.Text,out c.def);
			long.TryParse( tb_cardcode.Text,out c.id);
			long.TryParse( tb_cardalias.Text,out c.alias);

			return c;
		}
		
		//得到所选值
		string GetSelectHex(Dictionary<long, string> dic,ComboBox cb)
		{
			long temp;
			long.TryParse(GetSelect(dic,cb),out temp);
			return temp.ToString("x");
		}
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
					if(mcard.id==oldCard.id)
						items[j].Checked=true;
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
			if(datacfg == null)
				return false;
			if(File.Exists(nowCdbFile))
				return true;
			else
			{
				MyMsg.Warning(LMSG.NotSelectDataBase);
				return false;
			}
		}
		
		//打开数据库
		public bool Open(string cdbFile)
		{
			if(!File.Exists(cdbFile))
			{
				MyMsg.Error(LMSG.FileIsNotExists);
				return false;
			}
			ydkfile=null;
			imagepath=null;
			complist=null;
			SetCDB(cdbFile);
			cardlist.Clear();
			DataBase.CheckTable(cdbFile);
			srcCard=new Card();
			SetCards(DataBase.Read(cdbFile,true,""),false);

			return true;
		}

		public void SetCards(Card[] cards, bool isfresh)
		{
			if(cards!=null)
			{
				cardlist.Clear();
				foreach(Card c in cards){
					if(srcCard.setcode==0)
						cardlist.Add(c);
					else if(c.IsSetCode(srcCard.setcode&0xffff))
						cardlist.Add(c);
				}
				cardcount=cardlist.Count;
				pageNum=cardcount/MaxRow;
				if(cardcount%MaxRow > 0)
					pageNum++;
				else if(cardcount==0)
					pageNum=1;
				tb_pagenum.Text=pageNum.ToString();
				
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
			if(!string.IsNullOrEmpty(ydkfile))
				SetCards(DataBase.ReadYdk(nowCdbFile, ydkfile), false);
			else if(!string.IsNullOrEmpty(imagepath))
				SetCards(DataBase.ReadImage(nowCdbFile, imagepath), false);
			else if(complist !=null){
				UpdateCompCards();
				SetCards(getCompCards(), false);
			}
			else{
				srcCard=c;
				string sql=DataBase.GetSelectSQL(c);
				#if DEBUG
				MyMsg.Show(sql);
				#endif
				SetCards(DataBase.Read(nowCdbFile, true, sql),isfresh);
			}
		}
		
		void UpdateCompCards()
		{
			
			Card[] mcards=DataBase.Read(nowCdbFile,true,"");
			if(mcards==null){
				complist.Clear();
				return;
			}
			foreach(Card c in mcards)
			{
				if(complist.ContainsKey(c.id))
					complist[c.id]=c;
			}
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
				MyMsg.Error(LMSG.CodeCanNotIsZero);
				return false;
			}
			foreach(Card ckey in cardlist)
			{
				if(c.id==ckey.id)
				{
					MyMsg.Warning(LMSG.ItIsExists);
					return false;
				}
			}
			if(DataBase.Command(nowCdbFile, DataBase.GetInsertSQL(c,true))>=2)
			{
				MyMsg.Show(LMSG.AddSucceed);
				undoString=DataBase.GetDeleteSQL(c);
				Search(srcCard, true);
				return true;
			}
			MyMsg.Error(LMSG.AddFail);
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
				MyMsg.Show(LMSG.ItIsNotChanged);
				return false;
			}
			if(c.id<=0)
			{
				MyMsg.Error(LMSG.CodeCanNotIsZero);
				return false;
			}
			string sql;
			if(c.id!=oldCard.id)
			{
				if(MyMsg.Question(LMSG.IfDeleteCard))
				{
					if(DataBase.Command(nowCdbFile, DataBase.GetDeleteSQL(oldCard))<2)
					{
						MyMsg.Error(LMSG.DeleteFail);
						return false;
					}
				}
				sql=DataBase.GetInsertSQL(c,false);
			}
			else
				sql=DataBase.GetUpdateSQL(c);
			if(DataBase.Command(nowCdbFile, sql)>0)
			{
				MyMsg.Show(LMSG.ModifySucceed);
				undoString=DataBase.GetDeleteSQL(c);
				undoString+=DataBase.GetInsertSQL(oldCard,false);
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
			if(!Check())
				return false;
			int ic=lv_cardlist.SelectedItems.Count;
			if(ic==0)
				return false;
			if(!MyMsg.Question(LMSG.IfDeleteCard))
				return false;
			List<string> sql=new List<string>();
			foreach(ListViewItem lvitem in lv_cardlist.SelectedItems)
			{
				int index=lvitem.Index+(page-1)*MaxRow;
				if(index<cardlist.Count)
				{
					Card c=cardlist[index];
					undoString+=DataBase.GetInsertSQL(c, true);
					sql.Add(DataBase.GetDeleteSQL(c));
				}
			}
			if(DataBase.Command(nowCdbFile, sql.ToArray())>=(sql.Count*2))
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
			if(!Check())
				return false;
			string lua=MyPath.Combine(LUAPTH,"c"+tb_cardcode.Text+".lua");
			if(!File.Exists(lua))
			{
				if(! Directory.Exists(LUAPTH))
					Directory.CreateDirectory(LUAPTH);
				if(MyMsg.Question(LMSG.IfCreateScript))
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
			{
				System.Diagnostics.Process.Start(lua);
			}
			return false;
		}
		//撤销
		public void Undo()
		{
			if(string.IsNullOrEmpty(undoString))
			{
				return;
			}
			DataBase.Command(nowCdbFile,undoString);
			Search(srcCard, true);
		}
		
		#endregion
		
		#region 按钮
		//搜索卡片
		void Btn_serachClick(object sender, EventArgs e)
		{
			ydkfile=null;
			imagepath=null;
			complist=null;
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
			string tid=tb_cardcode.Text;
			if(tid=="0" || tid.Length==0)
				return;
			using(OpenFileDialog dlg=new OpenFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.SelectImage)+"-"+tb_cardname.Text;
				dlg.Filter=LANG.GetMsg(LMSG.ImageType);
				if(dlg.ShowDialog()==DialogResult.OK)
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
			if(e.KeyChar==(char)Keys.Enter)
			{
				Card c=new Card(0);
				long.TryParse(tb_cardcode.Text, out c.id);
				if(c.id>0)
				{
					ydkfile=null;
					imagepath=null;
					complist=null;
					Search(c, false);
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
				if(c.name.Length>0){
					ydkfile=null;
					imagepath=null;
					complist=null;
					Search(c, false);
				}
			}
		}
		//卡片描述编辑
		void Setscripttext(string str)
		{
			int index=-1;
			try{
				index=lb_scripttext.SelectedIndex;
			}
			catch{
				index=-1;
				MyMsg.Error(LMSG.NotSelectScriptText);
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
				MyMsg.Error(LMSG.NotSelectScriptText);
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
			MyMsg.Show(
				LANG.GetMsg(LMSG.About)+"\t"+Application.ProductName+"\n"
				+LANG.GetMsg(LMSG.Version)+"\t"+Application.ProductVersion+"\n"
				+LANG.GetMsg(LMSG.Author)+"\t247321453\n"
				+"Email:\tkeyoyu@foxmail.com");
		}
		
		void Menuitem_checkupdateClick(object sender, EventArgs e)
		{
			checkupdate(true);
		}
		void checkupdate(bool showNew)
		{
			if(!isRun())
			{
				tasker.SetTask(MyTask.CheckUpdate,null,showNew.ToString());
				Run(LANG.GetMsg(LMSG.checkUpdate));
			}
		}
		bool CancelTask()
		{
			bool bl=false;
			if(tasker !=null && tasker.IsRuning()){
				bl=MyMsg.Question(LMSG.IfCancelTask);
				if(bl){
					if(tasker!=null)
						tasker.Cancel();
					if(bgWorker1.IsBusy)
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
			System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["sourceURL"]);
		}
		#endregion
		
		#region 文件菜单
		void Menuitem_openClick(object sender, EventArgs e)
		{
			using(OpenFileDialog dlg=new OpenFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.SelectDataBasePath);
				dlg.Filter=LANG.GetMsg(LMSG.CdbType);
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
				dlg.Title=LANG.GetMsg(LMSG.SelectDataBasePath);
				dlg.Filter=LANG.GetMsg(LMSG.CdbType);
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					if(DataBase.Create(dlg.FileName))
					{
						if(MyMsg.Question(LMSG.IfOpenDataBase))
							Open(dlg.FileName);
					}
				}
			}
		}

		void Menuitem_readydkClick(object sender, EventArgs e)
		{
			if(!Check())
				return;
			using(OpenFileDialog dlg=new OpenFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.SelectYdkPath);
				dlg.Filter=LANG.GetMsg(LMSG.ydkType);
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					ydkfile=dlg.FileName;
					SetCards(DataBase.ReadYdk(nowCdbFile, ydkfile), false);
				}
			}
		}
		
		void Menuitem_readimagesClick(object sender, EventArgs e)
		{
			if(!Check())
				return;
			using(FolderBrowserDialog fdlg=new FolderBrowserDialog())
			{
				fdlg.Description= LANG.GetMsg(LMSG.SelectImagePath);
				if(fdlg.ShowDialog()==DialogResult.OK)
				{
					imagepath=fdlg.SelectedPath;
					SetCards(DataBase.ReadImage(nowCdbFile, imagepath), false);
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
		bool isRun(){
			if(tasker !=null && tasker.IsRuning()){
				MyMsg.Warning(LMSG.RunError);
				return true;
			}
			return false;
		}
		
		void Run(string name){
			if(isRun())
				return;
			taskname=name;
			title=title+" ("+taskname+")";
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
			title=string.Format("{0} ({1}-{2})",
			                    RemoveTag(title),
			                    taskname,
			                    // e.ProgressPercentage,
			                    e.UserState);
			SetTitle();
		}
		//任务完成
		void BgWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			//
			int t=title.LastIndexOf(" (");
			if(t>0)
			{
				title=title.Substring(0,t);
				SetTitle();
			}
			if ( e.Error != null){
				
				MyMsg.Show(LANG.GetMsg(LMSG.TaskError)+"\n"+e.Error);
			}
			else if(tasker.IsCancel() || e.Cancelled){
				MyMsg.Show(LMSG.CancelTask);
			}
			else
			{
				MyTask mt=tasker.getLastTask();
				switch(mt){
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
				}
			}
		}
		#endregion
		
		#region setcode
		void Cb_setname2SelectedIndexChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit2)
				return;
			setcodeIsedit2=true;
			tb_setcode2.Text=GetSelectHex(datacfg.dicSetnames, cb_setname2);
			setcodeIsedit2=false;
		}
		
		void Cb_setname1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit1)
				return;
			setcodeIsedit1=true;
			tb_setcode1.Text=GetSelectHex(datacfg.dicSetnames, cb_setname1);
			setcodeIsedit1=false;
		}
		
		void Cb_setname3SelectedIndexChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit3)
				return;
			setcodeIsedit3=true;
			tb_setcode3.Text=GetSelectHex(datacfg.dicSetnames, cb_setname3);
			setcodeIsedit3=false;
		}
		
		void Cb_setname4SelectedIndexChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit4)
				return;
			setcodeIsedit4=true;
			tb_setcode4.Text=GetSelectHex(datacfg.dicSetnames, cb_setname4);
			setcodeIsedit4=false;
		}

		void Tb_setcode4TextChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit4)
				return;
			setcodeIsedit4=true;
			long temp;
			long.TryParse(tb_setcode4.Text,NumberStyles.HexNumber, null ,out temp);
			SetSelect(datacfg.dicSetnames, cb_setname4, temp);
			setcodeIsedit4=false;
		}
		
		void Tb_setcode3TextChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit3)
				return;
			setcodeIsedit3=true;
			long temp;
			long.TryParse(tb_setcode3.Text,NumberStyles.HexNumber, null ,out temp);
			SetSelect(datacfg.dicSetnames, cb_setname3, temp);
			setcodeIsedit3=false;
		}
		
		void Tb_setcode2TextChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit2)
				return;
			setcodeIsedit2=true;
			long temp;
			long.TryParse(tb_setcode2.Text,NumberStyles.HexNumber, null ,out temp);
			SetSelect(datacfg.dicSetnames, cb_setname2, temp);
			setcodeIsedit2=false;
		}
		
		void Tb_setcode1TextChanged(object sender, EventArgs e)
		{
			if(setcodeIsedit1)
				return;
			setcodeIsedit1=true;
			long temp;
			long.TryParse(tb_setcode1.Text,NumberStyles.HexNumber, null ,out temp);
			SetSelect(datacfg.dicSetnames, cb_setname1, temp);
			setcodeIsedit1=false;
		}
		#endregion
		
		#region 复制卡片
		public Card[] getCardList(bool onlyselect){
			if(!Check())
				return null;
			List<Card> cards=new List<Card>();
			if(onlyselect)
			{
				#if DEBUG
				MessageBox.Show("select");
				#endif
				foreach(ListViewItem lvitem in lv_cardlist.SelectedItems)
				{
					int index=lvitem.Index+(page-1)*MaxRow;
					if(index<cardlist.Count)
						cards.Add(cardlist[index]);
				}
			}
			else
				cards.AddRange(cardlist.ToArray());
			if(cards.Count==0){
				MyMsg.Show(LMSG.NoSelectCard);
				return null;
			}
			return cards.ToArray();
		}
		void Menuitem_copytoClick(object sender, EventArgs e)
		{
			CopyTo(false);
		}
		
		void Menuitem_copyselecttoClick(object sender, EventArgs e)
		{
			CopyTo(true);
		}
		public void SaveCards(Card[] cards)
		{
			if(!Check())
				return;
			bool replace=MyMsg.Question(LMSG.IfReplaceExistingCard);
			DataBase.CopyDB(nowCdbFile, !replace, cards);
			Search(srcCard, true);
		}
		void CopyTo(bool onlyselect)
		{
			if(!Check())
				return;
			Card[] cards=getCardList(onlyselect);
			if(cards==null)
				return;
			//select file
			bool replace=false;
			string filename=null;
			using(OpenFileDialog dlg=new OpenFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.SelectDataBasePath);
				dlg.Filter=LANG.GetMsg(LMSG.CdbType);
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					filename=dlg.FileName;
					replace=MyMsg.Question(LMSG.IfReplaceExistingCard);
				}
			}
			if(!string.IsNullOrEmpty(filename)){
				DataBase.CopyDB(filename, !replace, cards);
				MyMsg.Show(LMSG.CopyCardsToDBIsOK);
			}
			
		}
		#endregion
		
		#region MSE存档
		void Menuitem_cutimagesClick(object sender, EventArgs e)
		{
			if(!Check())
				return;
			if(isRun())
				return;
			bool isreplace=MyMsg.Question(LMSG.IfReplaceExistingImage);
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
		void SaveAsMSE(bool onlyselect){
			if(!Check())
				return;
			if(isRun())
				return;
			Card[] cards=getCardList(onlyselect);
			if(cards==null)
				return;
			//select save mse-set
			using(SaveFileDialog dlg=new SaveFileDialog())
			{
				dlg.Title=LANG.GetMsg(LMSG.selectMseset);
				dlg.Filter=LANG.GetMsg(LMSG.MseType);
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					bool isUpdate=false;
					#if DEBUG
					isUpdate=MyMsg.Question(LMSG.OnlySet);
					#endif
					tasker.SetTask(MyTask.SaveAsMSE,cards,
					               dlg.FileName,isUpdate.ToString());
					Run(LANG.GetMsg(LMSG.SaveMse));
				}
			}
		}
		#endregion
		
		#region 导入卡图
		void Pl_imageDragDrop(object sender, DragEventArgs e)
		{
			string[] files=e.Data.GetData(DataFormats.FileDrop) as string[];
			#if DEBUG
			MessageBox.Show(files[0]);
			#endif
			if(File.Exists(files[0]))
				ImportImage(files[0], tb_cardcode.Text);
		}
		
		void Pl_imageDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
			else
				e.Effect = DragDropEffects.None;
		}
		void Menuitem_importmseimgClick(object sender, EventArgs e)
		{
			string tid=tb_cardcode.Text;
			menuitem_importmseimg.Checked=!menuitem_importmseimg.Checked;
			setImage(tid);
		}
		void ImportImage(string file,string tid)
		{
			string f;
			pl_image.BackgroundImage.Dispose();
			pl_image.BackgroundImage=m_cover;
			if(menuitem_importmseimg.Checked){
				if(!Directory.Exists(tasker.MSEImage))
					Directory.CreateDirectory(tasker.MSEImage);
				f=MyPath.Combine(tasker.MSEImage, tid+".jpg");
				File.Copy(file, f, true);
			}
			else{
				f=MyPath.Combine(PICPATH,tid+".jpg");
				tasker.ToImg(file,f,
				             MyPath.Combine(PICPATH2,tid+".jpg"));
			}
			setImage(tid);
		}
		void setImage(string id)
		{
			long t;
			long.TryParse(id, out t);
			setImage(t);
		}
		void setImage(long id){
			if(pl_image.BackgroundImage != null
			   && pl_image.BackgroundImage!=m_cover)
				pl_image.BackgroundImage.Dispose();
			Bitmap temp;
			string pic=MyPath.Combine(PICPATH, id+".jpg");
			string pic2=MyPath.Combine(tasker.MSEImage, id+".jpg");
			string pic3=MyPath.Combine(tasker.MSEImage, new Card(id).idString+".jpg");
			if(menuitem_importmseimg.Checked && File.Exists(pic2))
			{
				temp=new Bitmap(pic2);
				pl_image.BackgroundImage=temp;
			}
			else if(menuitem_importmseimg.Checked && File.Exists(pic3))
			{
				temp=new Bitmap(pic3);
				pl_image.BackgroundImage=temp;
			}
			else if(File.Exists(pic)){
				temp=new Bitmap(pic);
				pl_image.BackgroundImage=temp;
			}
			else
				pl_image.BackgroundImage=m_cover;
		}
		void Menuitem_compdbClick(object sender, EventArgs e)
		{
			if(!Check())
				return;
			DataBase.Compression(nowCdbFile);
			MyMsg.Show(LMSG.CompDBOK);
		}
		void Menuitem_convertimageClick(object sender, EventArgs e)
		{
			if(!Check())
				return;
			if(isRun())
				return;
			using(FolderBrowserDialog fdlg=new FolderBrowserDialog())
			{
				fdlg.Description= LANG.GetMsg(LMSG.SelectImagePath);
				if(fdlg.ShowDialog()==DialogResult.OK)
				{
					bool isreplace=MyMsg.Question(LMSG.IfReplaceExistingImage);
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
			if(!Check())
				return;
			if(isRun())
				return;
			tasker.SetTask(MyTask.ExportData, null, nowCdbFile);
			Run(LANG.GetMsg(LMSG.ExportData));
		}
		#endregion
		
		#region 对比数据
		/// <summary>
		/// 数据一致，返回true，不存在和数据不同，则返回false
		/// </summary>
		/// <param name="cards"></param>
		/// <param name="card"></param>
		/// <returns></returns>
		bool CheckCard(Card[] cards,Card card,bool checkinfo)
		{
			foreach(Card c in cards)
			{
				if(c.id!=card.id)
					continue;
				//data数据不一样
				if(checkinfo)
					return card.EqualsData(c);
				else
					return true;
			}
			return false;
		}
		Card[] getCompCards()
		{
			if(complist.Count==0)
				return null;
			Card[] tmps=new Card[complist.Count];
			complist.Values.CopyTo(tmps,0);
			return tmps;
		}
		public void CompareCards(string cdbfile,bool checktext)
		{
			if(!Check())
				return;
			ydkfile=null;
			imagepath=null;
			srcCard=new Card();
			Card[] mcards=DataBase.Read(nowCdbFile,true,"");
			Card[] cards=DataBase.Read(cdbfile,true,"");
			complist =new Dictionary<long, Card>();
			foreach(Card card in mcards)
			{
				if(!CheckCard(cards, card, checktext))
					complist.Add(card.id, card);
			}
			if(complist.Count==0){
				SetCards(null, false);
				return;
			}
			SetCards(getCompCards(), false);
		}
		#endregion
		
	}
}
