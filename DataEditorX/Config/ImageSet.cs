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
	/// <summary>
	/// 裁剪图片是、配置
	/// </summary>
	public class ImageSet
	{
		public ImageSet(){
			Init();
		}
		//初始化
		void Init()
		{
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
		/// <summary>
		/// jpeg质量
		/// </summary>
		public int quilty;
		/// <summary>
		/// 小图的宽
		/// </summary>
		public int w;
		/// <summary>
		/// 小图的高
		/// </summary>
		public int h;
		/// <summary>
		/// 大图的宽
		/// </summary>
		public int W;
		/// <summary>
		/// 大图的高
		/// </summary>
		public int H;
		/// <summary>
		/// 怪兽的中间图
		/// </summary>
		public Area normalArea;
		/// <summary>
		/// xyz怪兽的中间图
		/// </summary>
		public Area xyzArea;
		/// <summary>
		/// p怪的中间图
		/// </summary>
		public Area pendulumArea;
	}
}
