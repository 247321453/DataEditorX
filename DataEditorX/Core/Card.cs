/*
 * CreateDate :2014-02-07
 * desc :卡片类
 * ModiftyDate :2014-02-12
 */
using System;
using System.Text.RegularExpressions;
using DataEditorX.Core.Info;

namespace DataEditorX.Core
{
	public struct Card : IEquatable<Card>
	{
        public const int STR_MAX = 0x10;
		#region 构造
		/// <summary>
		/// 卡片
		/// </summary>
		/// <param name="cardCode">密码</param>
		/// <param name="cardName">名字</param>
		public Card(long cardCode)
		{
            
            this.id = cardCode;
            this.name = "";
            this.ot = 0;
            this.alias = 0;
            this.setcode = 0;
            this.type = 0;
            this.atk = 0;
            this.def = 0;
            this.level = 0;
            this.race = 0;
            this.attribute = 0;
            this.category = 0;
            this.desc = "";
            int i;
            this.str = new string[STR_MAX];
            for (i = 0; i < STR_MAX; i++)
                str[i] = "";
		}
        public void InitStrs()
        {
            int i;
            this.str = new string[STR_MAX];
            for (i = 0; i < STR_MAX; i++)
                str[i] = "";
        }
		#endregion

		#region 成员
		/// <summary>卡片密码</summary>
		public long id;
		/// <summary>卡片规则</summary>
		public int ot;
		/// <summary>卡片同名卡</summary>
		public long alias;
		/// <summary>卡片系列号</summary>
		public long setcode;
		/// <summary>卡片种类</summary>
		public long type;
		/// <summary>攻击力</summary>
		public int atk;
		/// <summary>防御力</summary>
		public int def;
		/// <summary>卡片等级</summary>
		public long level;
		/// <summary>卡片种族</summary>
		public long race;
		/// <summary>卡片属性</summary>
		public int attribute;
		/// <summary>效果种类</summary>
		public long category;
		/// <summary>卡片名称</summary>
		public string name;
		/// <summary>描述文本</summary>
		public string desc;
		/// <summary>脚本文件组</summary>
		public string[] str;
		#endregion

		#region 比较、哈希值、操作符
		/// <summary>
		/// 比较
		/// </summary>
		/// <param name="obj">对象</param>
		/// <returns>结果</returns>
		public override bool Equals(object obj)
		{
			if (obj is Card)
				return Equals((Card)obj); // use Equals method below
			else
				return false;
		}
		/// <summary>
		/// 比较卡片，除脚本提示文本
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
        public bool EqualsData(Card other)
		{
			bool equalBool = true;
			if (this.id != other.id)
				equalBool = false;
			else if (this.ot != other.ot)
				equalBool = false;
			else if (this.alias != other.alias)
				equalBool = false;
			else if (this.setcode != other.setcode)
				equalBool = false;
			else if (this.type != other.type)
				equalBool = false;
			else if (this.atk != other.atk)
				equalBool = false;
			else if (this.def != other.def)
				equalBool = false;
			else if (this.level != other.level)
				equalBool = false;
			else if (this.race != other.race)
				equalBool = false;
			else if (this.attribute != other.attribute)
				equalBool = false;
			else if (this.category != other.category)
				equalBool = false;
			else if (!this.name.Equals(other.name))
				equalBool = false;
			else if (!this.desc.Equals(other.desc))
				equalBool = false;
			return equalBool;
		}
		/// <summary>
		/// 比较卡片是否一致？
		/// </summary>
		/// <param name="other">比较的卡片</param>
		/// <returns>结果</returns>
		public bool Equals(Card other)
		{
			bool equalBool=EqualsData(other);
			if(!equalBool)
				return false;
			else if (this.str.Length != other.str.Length)
				equalBool = false;
			else
			{
				int l = this.str.Length;
				for (int i = 0; i < l; i++)
				{
					if (!this.str[i].Equals(other.str[i]))
					{
						equalBool = false;
						break;
					}
				}
			}
			return equalBool;

		}
		/// <summary>
		/// 得到哈希值
		/// </summary>
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			int hashCode = id.GetHashCode() + name.GetHashCode();
			return hashCode;//member.GetHashCode();
		}
		/// <summary>
		/// 比较卡片是否相等
		/// </summary>
		public static bool operator ==(Card left, Card right)
		{
			return left.Equals(right);
		}
        /// <summary>
        /// 是否是某类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public bool IsType(CardType type){
			if((this.type & (long)type) == (long)type)
				return true;
			return false;
		}
        /// <summary>
        /// 是否是某系列
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
		public bool IsSetCode(long sc)
		{
            long settype = sc & 0xfff;
            long setsubtype = sc & 0xf000;
            long setcode = this.setcode;
            while (setcode != 0)
            {
                if ((setcode & 0xfff) == settype && (setcode & 0xf000 & setsubtype) == setsubtype)
                    return true;
                setcode = setcode >> 0x10;
            }
            return false;
		}
		/// <summary>
		/// 卡片是否不相等
		/// </summary>
		public static bool operator !=(Card left, Card right)
		{
			return !left.Equals(right);
		}
		#endregion

        #region 卡片文字信息
        /// <summary>
        /// 密码字符串
        /// </summary>
        public string idString
        {
            get { return id.ToString("00000000"); }
        }
        /// <summary>
        /// 字符串化
        /// </summary>
        public override string ToString()
        {
            string str = "";
            if (IsType(CardType.TYPE_MONSTER)){
                str = name + "[" + idString + "]\n["
                    + YGOUtil.GetTypeString(type) + "] "
                    + YGOUtil.GetRace(race) + "/" + YGOUtil.GetAttributeString(attribute)
                    + "\n" + levelString() + " " + atk + "/" + def + "\n" + redesc();
            }else
                str = name +"[" +idString +"]\n["+YGOUtil.GetTypeString(type)+"]\n"+redesc();
            return str;
        }
        public string ToShortString(){
            return this.name+" ["+idString+"]";
        }
        public string ToLongString(){
            return ToString();
        }

        string levelString()
        {
            string star = "[";
            long i = 0, j = level & 0xff;
            for (i = 0; i < j; i++)
            {
                if (i >= 0 && (i % 4) == 0)
                    star += " ";
                star += "★";
            }
            return star + "]";
        }
        string redesc()
        {
            string str = desc.Replace(Environment.NewLine, "\n");
            str = Regex.Replace(str, "([。|？|?])", "$1\n");
            str = str.Replace("\n\n", "\n");
            return str;
        }
        #endregion
    }
	
}
