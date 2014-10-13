/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-12
 * 时间: 19:43
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

namespace DataEditorX.Core
{
	public enum MyTask{
		NONE,
		CheckUpdate,
		CopyDataBase,
		SaveAsMSE,
		CutImages,
	}
	/// <summary>
	/// Description of TaskHelper.
	/// </summary>
	public class TaskHelper
	{
		private static MyTask nowTask=MyTask.NONE;
		private static Card[] cardlist;
		private static string[] mArgs;
		private static ImageSet imgSet=new ImageSet();
		
		public static MyTask getTask(){
			return nowTask;
		}
		public static void SetTask(MyTask myTask,Card[] cards,params string[] args){
			nowTask=myTask;
			cardlist=cards;
			mArgs=args;
		}
		public static void OnCheckUpdate(bool showNew){
			string newver=CheckUpdate.Check(
				ConfigurationManager.AppSettings["updateURL"]);
			int iver,iver2;
			int.TryParse(Application.ProductVersion.Replace(".",""), out iver);
			int.TryParse(newver.Replace(".",""), out iver2);
			if(iver2>iver)
			{//has new version
				if(!MyMsg.Question(LMSG.HaveNewVersion))
					return;
			}
			else if(iver2>0)
			{//now is last version
				if(!showNew)
					return;
				if(!MyMsg.Question(LMSG.NowIsNewVersion))
					return;
			}
			else
			{
				if(!showNew)
					return;
				MyMsg.Error(LMSG.CheckUpdateFail);
				return;
			}
			if(CheckUpdate.DownLoad(
				Path.Combine(Application.StartupPath, newver+".zip")))
				MyMsg.Show(LMSG.DownloadSucceed);
			else
				MyMsg.Show(LMSG.DownloadFail);
		}
		
		public static void CutImages(string imgpath,string savepath)
		{
			imgSet.Init();
			foreach(Card c in cardlist)
			{
				string jpg=Path.Combine(imgpath, c.id+".jpg");
				string savejpg=Path.Combine(savepath, c.id+".jpg");
				if(File.Exists(jpg)){
					Bitmap bp=new Bitmap(jpg);
					Bitmap bmp=null;
					if(c.IsType(CardType.TYPE_XYZ)){
						bmp = MyBitmap.Cut(bp,
						                  imgSet.xyz_x,imgSet.xyz_y,
						                  imgSet.xyz_w,imgSet.xyz_h);
					}
					else if(c.IsType(CardType.TYPE_PENDULUM)){
						bmp = MyBitmap.Cut(bp,
						                  imgSet.pendulum_x,imgSet.pendulum_y,
						                  imgSet.pendulum_w,imgSet.pendulum_h);
					}
					else{
						bmp = MyBitmap.Cut(bp,
						                  imgSet.other_x,imgSet.other_y,
						                  imgSet.other_w,imgSet.other_h);
					}
					MyBitmap.SaveAsJPEG(bmp, savejpg, imgSet.quilty);
				}
			}
		}
		public static void ToImg(string img,string saveimg1,string saveimg2){
			imgSet.Init();
			if(!File.Exists(img))
				return;
			Bitmap bmp=new Bitmap(img);
			MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.W, imgSet.H),
			                    saveimg1, imgSet.quilty);
			MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.w, imgSet.h),
			                    saveimg2, imgSet.quilty);
		}
		
		public static void Run(){
			switch(nowTask){
				case MyTask.CheckUpdate:
					bool showNew=false;
					if(mArgs!=null && mArgs.Length>=1){
						showNew=(mArgs[0]==Boolean.TrueString)?true:false;
					}
					OnCheckUpdate(showNew);
					break;
				case MyTask.CopyDataBase:
					if(mArgs!=null && mArgs.Length>=2){
						string filename=mArgs[0];
						bool replace=(mArgs[1]==Boolean.TrueString)?true:false;
						DataBase.CopyDB(filename, replace,cardlist);
						//
						MyMsg.Show(LMSG.copyDBIsOK);
					}
					break;
				case MyTask.CutImages:
					if(mArgs!=null && mArgs.Length>=2){
						CutImages(mArgs[0],mArgs[1]);
						MyMsg.Show(LMSG.CutImageOK);
					}
					break;
				case MyTask.SaveAsMSE:
					if(mArgs!=null && mArgs.Length>=2){
						MSE.Save(mArgs[0], cardlist, mArgs[1]);
						MyMsg.Show(LMSG.SaveMseOK);
					}
					break;
			}

			nowTask=MyTask.NONE;
			cardlist=null;
			mArgs=null;
		}
	}

}
