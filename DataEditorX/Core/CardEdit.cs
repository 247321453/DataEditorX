using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DataEditorX.Config;
using DataEditorX.Language;
using DataEditorX.Core.Info;

namespace DataEditorX.Core
{
	public class CardEdit
	{
		IDataForm dataform;
		public AddCommand addCard;
		public ModCommand modCard;
		public DelCommand delCard;
		public CopyCommand copyCard;

		public CardEdit(IDataForm dataform)
		{
			this.dataform = dataform;
			this.addCard = new AddCommand(this);
			this.modCard = new ModCommand(this);
			this.delCard = new DelCommand(this);
			this.copyCard = new CopyCommand(this);
		}
		
		#region 添加
		//添加
		public class AddCommand: IBackableCommand
		{
			private string _undoSQL;

			CardEdit cardedit;
			IDataForm dataform;
			public AddCommand(CardEdit cardedit)
			{
				this.cardedit = cardedit;
				this.dataform = cardedit.dataform;
			}

			public bool Excute(params object[] args)
			{
				if (!dataform.CheckOpen())
					return false;
				Card c = dataform.GetCard();
				if (c.id <= 0)//卡片密码不能小于等于0
				{
					MyMsg.Error(LMSG.CodeCanNotIsZero);
					return false;
				}
				Card[] cards = dataform.GetCardList(false);
				foreach (Card ckey in cards)//卡片id存在
				{
					if (c.id == ckey.id)
					{
						MyMsg.Warning(LMSG.ItIsExists);
						return false;
					}
				}
				if (DataBase.Command(dataform.GetOpenFile(),
					DataBase.GetInsertSQL(c, true)) >= 2)
				{
					MyMsg.Show(LMSG.AddSucceed);
					_undoSQL = DataBase.GetDeleteSQL(c);
					dataform.Search(true);
					dataform.SetCard(c);
					return true;
				}
				MyMsg.Error(LMSG.AddFail);
				return false;
			}
			public void Undo()
			{
				DataBase.Command(dataform.GetOpenFile(), _undoSQL);
			}

			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}
		#endregion

		#region 修改
		//修改
		public class ModCommand: IBackableCommand
		{
			private string _undoSQL;
			private bool modifiled = false;
			private long oldid;
			private long newid;
			private bool delold;

			CardEdit cardedit;
			IDataForm dataform;
			public ModCommand(CardEdit cardedit)
			{
				this.cardedit = cardedit;
				this.dataform = cardedit.dataform;
			}

			public bool Excute(params object[] args)
			{
				if (!dataform.CheckOpen())
					return false;
				bool modfiles = (bool)args[0];

				Card c = dataform.GetCard();
				Card oldCard = dataform.GetOldCard();
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
					sql = DataBase.GetInsertSQL(c, false);//插入
					bool delold = MyMsg.Question(LMSG.IfDeleteCard);
					if (delold)//是否删除旧卡片
					{
						if (DataBase.Command(dataform.GetOpenFile(),
							DataBase.GetDeleteSQL(oldCard)) < 2)
						{
							//删除失败
							MyMsg.Error(LMSG.DeleteFail);
							delold = false;
						}
						else
						{//删除成功，添加还原sql
							_undoSQL = DataBase.GetDeleteSQL(c) + DataBase.GetInsertSQL(oldCard, false);
						}
					}
					else
						_undoSQL = DataBase.GetDeleteSQL(c);//还原就是删除
					//如果删除旧卡片，则把资源修改名字,否则复制资源
					if(modfiles)
					{
						if (delold)
							YGOUtil.CardRename(c.id, oldCard.id, dataform.GetPath());
						else
							YGOUtil.CardCopy(c.id, oldCard.id, dataform.GetPath());
						this.modifiled = true;
						this.oldid = oldCard.id;
						this.newid = c.id;
						this.delold = delold;
					}
				}
				else
				{//更新数据
					sql = DataBase.GetUpdateSQL(c);
					_undoSQL = DataBase.GetUpdateSQL(oldCard);
				}
				if (DataBase.Command(dataform.GetOpenFile(), sql) > 0)
				{
					MyMsg.Show(LMSG.ModifySucceed);
					dataform.Search(true);
					dataform.SetCard(c);
					return true;
				}
				else
					MyMsg.Error(LMSG.ModifyFail);
				return false;
			}

			public void Undo()
			{
				DataBase.Command(dataform.GetOpenFile(), _undoSQL);
				if (this.modifiled)
				{
					if (this.delold)
						YGOUtil.CardRename(this.oldid, this.newid, dataform.GetPath());
					else
						YGOUtil.CardDelete(this.newid, dataform.GetPath());
				}
			}

			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}
		#endregion

		#region 删除
		//删除
		public class DelCommand : IBackableCommand
		{
			private string _undoSQL;

			CardEdit cardedit;
			IDataForm dataform;
			public DelCommand(CardEdit cardedit)
			{
				this.cardedit = cardedit;
				this.dataform = cardedit.dataform;
			}

			public bool Excute(params object[] args)
			{
				if (!dataform.CheckOpen())
					return false;
				bool deletefiles = (bool)args[0];

				Card[] cards = dataform.GetCardList(true);
				if (cards == null || cards.Length == 0)
					return false;
				string undo = "";
				if (!MyMsg.Question(LMSG.IfDeleteCard))
					return false;
				List<string> sql = new List<string>();
				foreach (Card c in cards)
				{
					sql.Add(DataBase.GetDeleteSQL(c));//删除
					undo += DataBase.GetInsertSQL(c, true);
					//删除资源
					if (deletefiles)
					{
						YGOUtil.CardDelete(c.id, dataform.GetPath());
					}
				}
				if (DataBase.Command(dataform.GetOpenFile(), sql.ToArray()) >= (sql.Count * 2))
				{
					MyMsg.Show(LMSG.DeleteSucceed);
					dataform.Search(true);
					_undoSQL = undo;
					return true;
				}
				else
				{
					MyMsg.Error(LMSG.DeleteFail);
					dataform.Search(true);
				}
				return false;
			}
			public void Undo()
			{
				DataBase.Command(dataform.GetOpenFile(), _undoSQL);
			}

			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}
		#endregion

		#region 打开脚本
		//打开脚本
		public bool OpenScript(bool openinthis, string addrequire)
		{
			if (!dataform.CheckOpen())
				return false;
			Card c = dataform.GetCard();
			long id = c.id;
			string lua;
			if (c.id > 0) {
				lua = dataform.GetPath().GetScript(id);
			} else if (addrequire.Length > 0) {
				lua = dataform.GetPath().GetModuleScript(addrequire);
			} else {
				return false;
			}
			if (!File.Exists(lua))
			{
				MyPath.CreateDirByFile(lua);
				if (MyMsg.Question(LMSG.IfCreateScript))//是否创建脚本
				{
					using (FileStream fs = new FileStream(lua,
						FileMode.OpenOrCreate,FileAccess.Write))
					{
						StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false));
						if (c.id > 0) { //card script
							sw.WriteLine("--" + c.name);
							sw.WriteLine("local m=" + id.ToString());
							sw.WriteLine("local cm=_G[\"c\"..m]");
							if (addrequire.Length > 0)
								sw.WriteLine("xpcall(function() require(\"expansions/script/" + addrequire + "\") end,function() require(\"script/" + addrequire + "\") end)");
							sw.WriteLine("function cm.initial_effect(c)");
							sw.WriteLine("\t");
							sw.WriteLine("end");
						} else { //module script
							sw.WriteLine("--Module script \"" + addrequire + "\"");
						}
						sw.Close();
						fs.Close();
					}
				}
			}
			if (File.Exists(lua))//如果存在，则打开文件
			{
				if (openinthis)//是否用本程序打开
					MyConfig.OpenFileInThis(lua);
				else
					System.Diagnostics.Process.Start(lua);
				return true;
			}
			return false;
		}
		#endregion

		#region 复制卡片
		public class CopyCommand : IBackableCommand
		{
			bool copied = false;
			Card[] NewCards;
			bool replace;
			Card[] OldCards;

			CardEdit cardedit;
			IDataForm dataform;
			public CopyCommand(CardEdit cardedit)
			{
				this.cardedit = cardedit;
				this.dataform = cardedit.dataform;
			}

			public bool Excute(params object[] args)
			{
				if (!dataform.CheckOpen())
					return false;
				Card[] cards = (Card[])args[0];

				if (cards == null || cards.Length == 0)
					return false;
				bool replace = false;
				Card[] oldcards = DataBase.Read(dataform.GetOpenFile(), true, "");
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
				DataBase.CopyDB(dataform.GetOpenFile(), !replace, cards);
				this.copied = true;
				this.NewCards = cards;
				this.replace = replace;
				this.OldCards = oldcards;
				return true;
			}
			public void Undo()
			{
				DataBase.DeleteDB(dataform.GetOpenFile(), this.NewCards);
				DataBase.CopyDB(dataform.GetOpenFile(), !this.replace, this.OldCards);
			}

			public object Clone()
			{
				CopyCommand replica = new CopyCommand(cardedit);
				replica.copied = this.copied;
				replica.NewCards = (Card[])this.NewCards.Clone();
				replica.replace = this.replace;
				if (this.OldCards != null)
					replica.OldCards = (Card[])this.OldCards.Clone();
				return replica;
			}
		}
		#endregion
	}
}
