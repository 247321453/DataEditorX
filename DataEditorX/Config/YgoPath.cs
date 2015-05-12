using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataEditorX.Config
{
    public class YgoPath
    {
        public YgoPath(string gamepath)
        {
            SetPath(gamepath);
        }
        public void SetPath(string gamepath)
        {
            this.gamepath = gamepath;
            picpath = MyPath.Combine(gamepath, "pics");
            fieldpath = MyPath.Combine(picpath, "field");
            picpath2 = MyPath.Combine(picpath, "thumbnail");
            luapath = MyPath.Combine(gamepath, "script");
            ydkpath = MyPath.Combine(gamepath, "deck");
            replaypath = MyPath.Combine(gamepath, "replay");
        }
        /// <summary>游戏目录</summary>
        public string gamepath;
        /// <summary>大图目录</summary>
        public string picpath;
        /// <summary>小图目录</summary>
        public string picpath2;
        /// <summary>场地图目录</summary>
        public string fieldpath;
        /// <summary>脚本目录</summary>
        public string luapath;
        /// <summary>卡组目录</summary>
        public string ydkpath;
        /// <summary>录像目录</summary>
        public string replaypath;

		public string GetImage(long id, bool bak = false)
        {
			return GetImage(id.ToString(), bak);
        }
		public string GetImageThum(long id, bool bak = false)
        {
			return GetImageThum(id.ToString(), bak);
        }
		public string GetImageField(long id, bool bak = false)
        {
			return GetImageField(id.ToString(), bak);//场地图
        }
		public string GetScript(long id, bool bak = false)
        {
			return GetScript(id.ToString(), bak);
        }
        public string GetYdk(string name)
        {
            return MyPath.Combine(ydkpath, name + ".ydk");
        }
        //字符串id
        public string GetImage(string id, bool bak = false)
        {
			if (bak)
				return MyPath.Combine(picpath, id + ".jpg.bak");
            return MyPath.Combine(picpath, id + ".jpg");
        }
		public string GetImageThum(string id, bool bak = false)
        {
			if (bak)
				return MyPath.Combine(picpath2, id + ".jpg.bak");
            return MyPath.Combine(picpath2, id + ".jpg");
        }
		public string GetImageField(string id, bool bak = false)
        {
			if (bak)
				return MyPath.Combine(fieldpath, id + ".png.bak");
            return MyPath.Combine(fieldpath, id+ ".png");//场地图
        }
		public string GetScript(string id, bool bak = false)
        {
			if (bak)
				return MyPath.Combine(luapath, "c" + id + ".lua.bak");
            return MyPath.Combine(luapath, "c" + id + ".lua");
        }

		public string[] GetCardfiles(long id, bool bak = false)
        {
            string[] files = new string[]{
                GetImage(id, bak),//大图
                GetImageThum(id, bak),//小图
                GetImageField(id, bak),//场地图
                GetScript(id, bak)
           };
            return files;
        }
		public string[] GetCardfiles(string id, bool bak = false)
        {
            string[] files = new string[]{
                GetImage(id, bak),//大图
                GetImageThum(id, bak),//小图
                GetImageField(id, bak),//场地图
                GetScript(id, bak)
           };
            return files;
        }
    }
}
