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
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.ComponentModel;

using DataEditorX.Language;

namespace DataEditorX.Core
{
	public enum MyTask{
		NONE,
		CheckUpdate,
		CopyDataBase,
		SaveAsMSE,
		CutImages,
		ConvertImages,
	}
	/// <summary>
	/// Description of TaskHelper.
	/// </summary>
	public class TaskHelper
	{
		private MyTask nowTask=MyTask.NONE;
		private MyTask lastTask=MyTask.NONE;
		private Card[] cardlist;
		private string[] mArgs;
		private ImageSet imgSet=new ImageSet();
		private MSE mseHelper;
		private bool isCancel=false;
		private BackgroundWorker worker;

		public TaskHelper(string datapath,BackgroundWorker worker,
		                  Dictionary<long,string> typedic,
		                  Dictionary<long,string> racedic)
		{
			this.worker=worker;
			mseHelper=new MSE(datapath,typedic,racedic);
			imgSet.Init();
		}
		public bool IsCancel()
		{
			return isCancel;
		}
		public void Cancel()
		{
			isCancel=true;
		}
		public MyTask getLastTask(){
			return lastTask;
		}
		public void SetTask(MyTask myTask,Card[] cards,params string[] args){
			nowTask=myTask;
			cardlist=cards;
			mArgs=args;
		}
		public void OnCheckUpdate(bool showNew){
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
		public void CutImages(string imgpath,string savepath,bool isreplace)
		{
			int count=cardlist.Length;
			int i=0;
			foreach(Card c in cardlist)
			{
				if(isCancel)
					break;
				i++;
				worker.ReportProgress((i/count), string.Format("{0}/{1}",i,count));
				string jpg=Path.Combine(imgpath, c.id+".jpg");
				string savejpg=Path.Combine(savepath, c.id+".jpg");
				if(File.Exists(jpg) && (isreplace || !File.Exists(savejpg))){
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
		public void ToImg(string img,string saveimg1,string saveimg2){
			if(!File.Exists(img))
				return;
			Bitmap bmp=new Bitmap(img);
			MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.W, imgSet.H),
			                    saveimg1, imgSet.quilty);
			MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.w, imgSet.h),
			                    saveimg2, imgSet.quilty);
		}
		public void ConvertImages(string imgpath,string gamepath,bool isreplace)
		{
			string picspath=Path.Combine(gamepath,"pics");
			string thubpath=Path.Combine(picspath,"thumbnail");
			string[] files=Directory.GetFiles(imgpath);
			int i=0;
			int count=files.Length;
			
			foreach(string f in files){
				if(isCancel)
					break;
				i++;
				worker.ReportProgress(i/count, string.Format("{0}/{1}",i,count));
				string ex=Path.GetExtension(f).ToLower();
				string name=Path.GetFileNameWithoutExtension(f);
				string jpg_b=Path.Combine(picspath,name+".jpg");
				string jpg_s=Path.Combine(thubpath,name+".jpg");
				if(ex==".jpg"||ex==".png"||ex==".bmp"){
					if(File.Exists(f)){
						//存在大图
						Bitmap bmp=new Bitmap(f);
						if(isreplace || !File.Exists(jpg_b)){
							
							MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.W, imgSet.H),
							                    jpg_b, imgSet.quilty);
						}
						if(isreplace || !File.Exists(jpg_s)){
							MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.w, imgSet.h),
							                    jpg_s, imgSet.quilty);
							
						}
					}
				}
			}
		}
		public void SaveMSE(string file, Card[] cards,string pic,bool isUpdate){
			string setFile=file+".txt";
			string[] images=mseHelper.WriteSet(setFile, cards, pic);
			if(isUpdate)//仅更新文字
				return;
			int i=0;
			int count=images.Length;
			using(ZipStorer zips=ZipStorer.Create(file, ""))
			{
				zips.AddFile(setFile,"set","");
				foreach ( string img in images )
				{
					if(isCancel)
						break;
					i++;
					worker.ReportProgress(i/count, string.Format("{0}/{1}",i,count));
					zips.AddFile(img, Path.GetFileName(img),"");
				}
			}
			File.Delete(setFile);
		}
		public void Run(){
			isCancel=false;
			bool replace;
			bool showNew;
			switch(nowTask){
				case MyTask.CheckUpdate:
					showNew=false;
					if(mArgs!=null && mArgs.Length>=1){
						showNew=(mArgs[0]==Boolean.TrueString)?true:false;
					}
					OnCheckUpdate(showNew);
					break;
				case MyTask.CopyDataBase:
					if(mArgs!=null && mArgs.Length>=2){
						string filename=mArgs[0];
						replace=(mArgs[1]==Boolean.TrueString)?true:false;
						DataBase.CopyDB(filename, !replace,cardlist);
					}
					break;
				case MyTask.CutImages:
					if(mArgs!=null && mArgs.Length>=2){
						replace=true;
						if(mArgs.Length>=3){
							if(mArgs[2]==Boolean.FalseString)
								replace=false;
						}
						CutImages(mArgs[0],mArgs[1],replace);
					}
					break;
				case MyTask.SaveAsMSE:
					if(mArgs!=null && mArgs.Length>=2){
						replace=false;
						if(mArgs.Length>=3){
							if(mArgs[2]==Boolean.TrueString)
								replace=true;
						}
						SaveMSE(mArgs[0], cardlist, mArgs[1], replace);
					}
					break;
				case MyTask.ConvertImages:
					if(mArgs!=null && mArgs.Length>=2){
						replace=true;
						if(mArgs.Length>=3){
							if(mArgs[2]==Boolean.FalseString)
								replace=false;
						}
						ConvertImages(mArgs[0],mArgs[1],replace);
					}
					break;
			}
			lastTask=nowTask;
			nowTask=MyTask.NONE;
			cardlist=null;
			mArgs=null;
		}
	}

}
