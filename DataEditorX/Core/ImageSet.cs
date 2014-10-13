/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-13
 * 时间: 9:02
 * 
 */
using System;
using System.Configuration;

namespace DataEditorX.Core
{
	/// <summary>
	/// Description of ImageSet.
	/// </summary>
	public class ImageSet
	{
		bool isInit;
		public ImageSet(){
			isInit=false;
		}
		public void Init()
		{
			if(isInit)
				return;
			isInit=true;
			string temp=ConfigurationManager.AppSettings["image_other"];
			string[] ws=string.IsNullOrEmpty(temp)?null:temp.Split(',');
			if(ws!=null && ws.Length==4){
				int.TryParse(ws[0],out this.other_x);
				int.TryParse(ws[1],out this.other_y);
				int.TryParse(ws[2],out this.other_w);
				int.TryParse(ws[3],out this.other_h);
			}
			//MyMsg.Show(string.Format("other:{0},{1},{2},{3}",other_x,other_y,other_w,other_h));
			temp=ConfigurationManager.AppSettings["image_xyz"];
			ws=string.IsNullOrEmpty(temp)?null:temp.Split(',');
			if(ws!=null && ws.Length==4){
				int.TryParse(ws[0],out this.xyz_x);
				int.TryParse(ws[1],out this.xyz_y);
				int.TryParse(ws[2],out this.xyz_w);
				int.TryParse(ws[3],out this.xyz_h);
			}
			//MyMsg.Show(string.Format("xyz:{0},{1},{2},{3}",xyz_x,xyz_y,xyz_w,xyz_h));
			temp=ConfigurationManager.AppSettings["image_pendulum"];
			ws=string.IsNullOrEmpty(temp)?null:temp.Split(',');
			if(ws!=null && ws.Length==4){
				int.TryParse(ws[0],out this.pendulum_x);
				int.TryParse(ws[1],out this.pendulum_y);
				int.TryParse(ws[2],out this.pendulum_w);
				int.TryParse(ws[3],out this.pendulum_h);
			}
			//MyMsg.Show(string.Format("pendulum:{0},{1},{2},{3}",pendulum_x,pendulum_y,pendulum_w,pendulum_h));
			temp=ConfigurationManager.AppSettings["image"];
			ws=string.IsNullOrEmpty(temp)?null:temp.Split(',');
			if(ws!=null && ws.Length==4){
				int.TryParse(ws[0],out this.w);
				int.TryParse(ws[1],out this.h);
				int.TryParse(ws[2],out this.W);
				int.TryParse(ws[3],out this.H);
			}
			//MyMsg.Show(string.Format("image:{0},{1},{2},{3}",w,h,W,H));
			temp=ConfigurationManager.AppSettings["image_quilty"];
			if(!string.IsNullOrEmpty(temp)){
				int.TryParse(temp, out this.quilty);
			}
			//MyMsg.Show(string.Format("quilty:{0}",quilty));
		}
		public int quilty;
		public int w,h,W,H;
		public int other_x,other_y;
		public int other_w,other_h;
		public int xyz_x,xyz_y;
		public int xyz_w,xyz_h;
		public int pendulum_x,pendulum_y;
		public int pendulum_w,pendulum_h;
	}
}
