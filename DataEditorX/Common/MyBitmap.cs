/*
 * date :2014-02-07
 * desc :图像处理，裁剪，缩放，保存
 */
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DataEditorX.Common
{
    /// <summary>
    /// 图片裁剪，缩放，保存高质量jpg
    /// </summary>
    public static class MyBitmap
    {
        #region 缩放
        /// <summary>
        /// 缩放图像
        /// </summary>
        /// <param name="img">源图像</param>
        /// <param name="newW">新宽度</param>
        /// <param name="newH">新高度</param>
        /// <returns>处理好的图像</returns>
        public static Bitmap Zoom(Bitmap sourceBitmap, int newWidth, int newHeight)
        {
            if ( sourceBitmap != null )
            {
                Bitmap b = new Bitmap(newWidth, newHeight);
                Graphics graphics = Graphics.FromImage(b);
                //合成：高质量，低速度
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                //去除锯齿
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                //偏移：高质量，低速度
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                //插补算法
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle newRect = new Rectangle(0, 0, newWidth, newHeight);
                Rectangle srcRect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);
                graphics.DrawImage(sourceBitmap, newRect, srcRect, GraphicsUnit.Pixel);
                graphics.Dispose();
                return b;
            }
            return sourceBitmap;
        }
        #endregion

        #region 裁剪
        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="sourceBitmap">图片源</param>
        /// <param name="area">区域</param>
        /// <returns></returns>
        public static Bitmap Cut(Bitmap sourceBitmap, Area area)
        {
            return Cut(sourceBitmap, area.left, area.top, area.width, area.height);
        }
        /// <summary>
        /// 裁剪图像
        /// </summary>
        /// <param name="img">源图像</param>
        /// <param name="StartX">开始x</param>
        /// <param name="StartY">开始y</param>
        /// <param name="iWidth">裁剪宽</param>
        /// <param name="iHeight">裁剪高</param>
        /// <returns>处理好的图像</returns>
        public static Bitmap Cut(Bitmap sourceBitmap, int StartX, int StartY, int cutWidth, int cutHeight)
        {
            if ( sourceBitmap != null )
            {
                int w = sourceBitmap.Width;
                int h = sourceBitmap.Height;
                //裁剪的区域宽度调整
                if ( ( StartX + cutWidth ) > w )
                {
                    cutWidth = w - StartX;
                }
                //裁剪的区域高度调整
                if ( ( StartY + cutHeight ) > h )
                {
                    cutHeight = h - StartY;
                }
                Bitmap bitmap = new Bitmap(cutWidth, cutHeight);
                Graphics graphics = Graphics.FromImage(bitmap);
                //合成：高质量，低速度
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                //去除锯齿
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                //偏移：高质量，低速度
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                //插补算法
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle cutRect = new Rectangle(0, 0, cutWidth, cutHeight);
                Rectangle srcRect = new Rectangle(StartX, StartY, cutWidth, cutHeight);
                graphics.DrawImage(sourceBitmap, cutRect, srcRect, GraphicsUnit.Pixel);
                graphics.Dispose();
                return bitmap;
            }
            return sourceBitmap;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存jpg图像
        /// </summary>
        /// <param name="bmp">源图像</param>
        /// <param name="filename">保存路径</param>
        /// <param name="quality">质量</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveAsJPEG(Bitmap bitmap, string filename, int quality)
        {
            if ( bitmap != null )
            {
            	string path=Path.GetDirectoryName(filename);
            	if(!Directory.Exists(path))//创建文件夹
            		Directory.CreateDirectory(path);
            	if(File.Exists(filename))//删除旧文件
            		File.Delete(filename);
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach ( ImageCodecInfo codec in codecs )
                {
                    if ( codec.MimeType.IndexOf("jpeg") > -1 )
                    {
                        ici = codec;
                    }
                    if ( quality < 0 || quality > 100 )
                        quality = 60;
                    EncoderParameters encoderParams = new EncoderParameters();
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
                    if ( ici != null )
                        bitmap.Save(filename, ici, encoderParams);
                    else
                        bitmap.Save(filename);
                }
                return true;
            }
            return false;
        }
        #endregion      
    }

}

