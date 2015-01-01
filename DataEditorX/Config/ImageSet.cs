/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-13
 * 时间: 9:02
 * 
 */
using System;
using System.Configuration;
using DataEditorX.Config;
using DataEditorX.Common;

namespace DataEditorX.Config
{

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
            this.normalArea = MyConfig.readArea(MyConfig.TAG_IMAGE_OTHER);

            this.xyzArea = MyConfig.readArea(MyConfig.TAG_IMAGE_XYZ);

            this.pendulumArea = MyConfig.readArea(MyConfig.TAG_IMAGE_PENDULUM);

            int[] ints = MyConfig.readIntegers(MyConfig.TAG_IMAGE_SIZE, 4);

            this.w = ints[0];
            this.h = ints[1];
            this.W = ints[2];
            this.H = ints[3];

            this.quilty = MyConfig.readInteger(MyConfig.TAG_IMAGE_QUILTY, 95);
		}
		public int quilty;
		public int w,h,W,H;
        public Area normalArea;
        public Area xyzArea;
        public Area pendulumArea;
	}
}
