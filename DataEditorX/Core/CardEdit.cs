using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DataEditorX.Config;
using DataEditorX.Language;

namespace DataEditorX.Core
{
    public class CardEdit
    {
        IDataForm dataform;
        public List<string> undoSQL;
		public class FileModified
		{
			public bool modifiled = false;
			public long oldid;
			public long newid;
			public bool delold;
		}
		public class FileDeleted
		{
			public bool deleted = false;
			public List<long> ids = new List<long>();
		}
		public class DBcopied
		{
			public bool copied = false;
			public Card[] NewCards;
			public bool replace;
			public Card[] OldCards;
		}
		public List<FileModified> undoModified;
		public List<FileDeleted> undoDeleted;
		public List<DBcopied> undoCopied;
        public CardEdit(IDataForm dataform)
        {
            this.dataform = dataform;
			this.undoSQL = new List<string>();
			this.undoModified = new List<FileModified>();
			this.undoDeleted = new List<FileDeleted>();
			this.undoCopied = new List<DBcopied>();
        }

        #region 添加
        //添加
        public bool AddCard()
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
                undoSQL.Add(DataBase.GetDeleteSQL(c));
				undoModified.Add(new FileModified());
				undoDeleted.Add(new FileDeleted());
				undoCopied.Add(new DBcopied());
                dataform.Search(true);
                dataform.SetCard(c);
                return true;
            }
            MyMsg.Error(LMSG.AddFail);
            return false;
        }
        #endregion

        #region 修改
        //修改
        public bool ModCard(bool modfiles)
        {
            if (!dataform.CheckOpen())
                return false;
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
                    }
                    else
                    {//删除成功，添加还原sql
                        undoSQL.Add(DataBase.GetDeleteSQL(c)+DataBase.GetInsertSQL(oldCard, false));
                    }
				} else
					undoSQL.Add(DataBase.GetDeleteSQL(c));//还原就是删除
                //如果删除旧卡片，则把资源修改名字,否则复制资源
				if (modfiles)
				{
					YGOUtil.CardRename(c.id, oldCard.id, dataform.GetPath(), delold);
					FileModified modify = new FileModified();
					modify.modifiled = true;
					modify.oldid = oldCard.id;
					modify.newid = c.id;
					modify.delold = delold;
					undoModified.Add(modify);
					undoDeleted.Add(new FileDeleted());
					undoCopied.Add(new DBcopied());
				}
				else
				{
					undoModified.Add(new FileModified());
					undoDeleted.Add(new FileDeleted());
					undoCopied.Add(new DBcopied());
				}
            }
            else
            {//更新数据
                sql = DataBase.GetUpdateSQL(c);
                undoSQL.Add(DataBase.GetUpdateSQL(oldCard));
				undoModified.Add(new FileModified());
				undoDeleted.Add(new FileDeleted());
				undoCopied.Add(new DBcopied());
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
        #endregion

        #region 删除
        //删除
        public bool DelCards(bool deletefiles)
        {
            if (!dataform.CheckOpen())
                return false;
            Card[] cards = dataform.GetCardList(true);
            if (cards == null || cards.Length == 0)
                return false;
			string undo = "";
            if (!MyMsg.Question(LMSG.IfDeleteCard))
                return false;
            List<string> sql = new List<string>();
			FileDeleted delete = new FileDeleted();
            foreach (Card c in cards)
            {
                sql.Add(DataBase.GetDeleteSQL(c));//删除
                undo += DataBase.GetInsertSQL(c, true);
                //删除资源
                if (deletefiles)
                {
                    YGOUtil.CardDelete(c.id, dataform.GetPath(), YGOUtil.DeleteOption.BACKUP);
					delete.deleted = true;
					delete.ids.Add(c.id);
                }
            }
            if (DataBase.Command(dataform.GetOpenFile(), sql.ToArray()) >= (sql.Count * 2))
            {
                MyMsg.Show(LMSG.DeleteSucceed);
                dataform.Search(true);
				undoSQL.Add(undo);
				undoDeleted.Add(delete);
				undoModified.Add(new FileModified());
				undoCopied.Add(new DBcopied());
                return true;
            }
            else
            {
                MyMsg.Error(LMSG.DeleteFail);
                dataform.Search(true);
            }
            return false;
        }
        #endregion

        #region 打开脚本
        //打开脚本
        public bool OpenScript(bool openinthis)
        {
            if (!dataform.CheckOpen())
                return false;
            Card c = dataform.GetCard();
            if (c.id <= 0)//卡片密码不能小于等于0
                return false;
            string lua = dataform.GetPath().GetScript(c.id);
            if (!File.Exists(lua))
            {
                MyPath.CreateDirByFile(lua);
                if (MyMsg.Question(LMSG.IfCreateScript))//是否创建脚本
                {
                    using (FileStream fs = new FileStream(lua,
                        FileMode.OpenOrCreate,FileAccess.Write))
                    {
                        StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false));
                        sw.WriteLine("--" + c.name);
                        sw.WriteLine("function c"+c.id.ToString()+".initial_effect(c)");
                        sw.WriteLine("\t");
                        sw.WriteLine("end");
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

        #region 撤销
        //撤销
        public void Undo()
        {
            if (undoSQL.Count == 0)
            {
				return;
            }
			DataBase.Command(dataform.GetOpenFile(), undoSQL[undoSQL.Count - 1]);
			undoSQL.RemoveAt(undoSQL.Count - 1);
			if (undoModified[undoModified.Count - 1].modifiled)
			{
				FileModified lastmodify = undoModified[undoModified.Count - 1];
				YGOUtil.CardRename(lastmodify.oldid, lastmodify.newid, dataform.GetPath(), lastmodify.delold);
			}
			undoModified.RemoveAt(undoModified.Count - 1);
			if (undoDeleted[undoDeleted.Count - 1].deleted)
			{
				FileDeleted lastdelete = undoDeleted[undoDeleted.Count - 1];
				foreach (long id in lastdelete.ids)
					YGOUtil.CardDelete(id, dataform.GetPath(), YGOUtil.DeleteOption.RESTORE);
			}
			undoDeleted.RemoveAt(undoDeleted.Count - 1);
			if (undoCopied[undoCopied.Count - 1].copied)
			{
				DBcopied lastcopied = undoCopied[undoCopied.Count - 1];
				DataBase.DeleteDB(dataform.GetOpenFile(), lastcopied.NewCards);
				DataBase.CopyDB(dataform.GetOpenFile(), !lastcopied.replace, lastcopied.OldCards);
			}
			undoCopied.RemoveAt(undoCopied.Count - 1);
			dataform.Search(true);
        }
        #endregion
    }
}
