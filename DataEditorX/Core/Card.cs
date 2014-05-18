/*
 * CreateDate :2014-02-07
 * desc :卡片类
 * ModiftyDate :2014-02-12
 */
using System;

namespace DataEditorX.Core
{
    public struct Card : IEquatable<Card>
    {
        #region 构造
        /// <summary>
        /// 卡片
        /// </summary>
        /// <param name="cardCode">密码</param>
        /// <param name="cardName">名字</param>
        public Card(int cardCode,string cardName)
        {
            this.id = cardCode;
            this.ot = 0;
            this.alias = 0;
            this.name = cardName;
            this.setcode = 0;
            this.type = 0;
            this.atk = 0;
            this.def = 0;
            this.level = 0;
            this.race = 0;
            this.attribute = 0;
            this.category = 0;
            this.desc = "";
            this.str = new string[0x10];
        }
        #endregion

        #region 成员
        /// <summary>
        /// 卡片密码
        /// </summary>
        public int id;
        /// <summary>
        /// 卡片规则
        /// </summary>
        public int ot;
        /// <summary>
        /// 卡片同名卡
        /// </summary>
        public int alias;
        /// <summary>
        /// 卡片系列号
        /// </summary>
        public ulong setcode;
        /// <summary>
        /// 卡片种类
        /// </summary>
        public long type;
        /// <summary>
        /// 攻击力
        /// </summary>
        public int atk;
        /// <summary>
        /// 防御力
        /// </summary>
        public int def;
        /// <summary>
        /// 卡片等级
        /// </summary>
        public int level;
        /// <summary>
        /// 卡片种族
        /// </summary>
        public int race;
        /// <summary>
        /// 卡片属性
        /// </summary>
        public int attribute;
        /// <summary>
        /// 效果种类
        /// </summary>
        public long category;
        /// <summary>
        /// 卡片名称
        /// </summary>
        public string name;
        /// <summary>
        /// 描述文本
        /// </summary>
        public string desc;
        /// <summary>
        /// 脚本文件组
        /// </summary>
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
        /// 字符串化
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} [{1}]", id.ToString("00000000"), name);
        }
        /// <summary>
        /// 比较卡片是否一致？
        /// </summary>
        /// <param name="other">比较的卡片</param>
        /// <returns>结果</returns>
        public bool Equals(Card other)
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
        /// 卡片是否不相等
        /// </summary>
        public static bool operator !=(Card left, Card right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// 左边接近右边
        /// </summary>
        /// <param name="left">卡片1</param>
        /// <param name="right">卡片2</param>
        /// <returns></returns>
        public static bool operator >(Card left, Card right)
        {
            return left.Like(right);
        }      
        /// <summary>
        /// 左边接近右边
        /// </summary>
        /// <param name="left">卡片1</param>
        /// <param name="right">卡片2</param>
        /// <returns></returns>
        public static bool operator >=(Card left, Card right)
        {
            return left.Like(right);
        }
        /// <summary>
        /// 左边不接近右边
        /// </summary>
        /// <param name="left">卡片1</param>
        /// <param name="right">卡片2</param>
        /// <returns></returns>
        public static bool operator <(Card left, Card right)
        {
            return !left.Like(right);
        }
        /// <summary>
        /// 左边不接近右边
        /// </summary>
        /// <param name="left">卡片1</param>
        /// <param name="right">卡片2</param>
        /// <returns></returns>
        public static bool operator <=(Card left, Card right)
        {
            return !left.Like(right);
        }
        #endregion

        #region 卡片过滤
        /// <summary>
        /// 卡片是否符合searchCard卡片？
        /// 0请用-1代替
        /// ？用-2代替
        /// 密码大于0，同名小于0=搜索密码大于密码
        /// 密码大于0，同名等于0=搜索密码等于密码
        /// 密码小于0，同名大于0=搜索同名大于同名
        /// 密码等于0，同名大于0=搜索同名等于同名
        /// 密码大于0，同名大于0=搜索密码大于密码，小于同名
        /// </summary>
        /// <param name="searchCard">过滤卡片</param>
        /// <returns>是否符合</returns>
        public bool Like(Card searchCard)
        {
            if (!CheckCode(this.id, this.alias, searchCard.id, searchCard.alias))
                return false;
            if (!CheckString(this.name, searchCard.name))
                return false;
            if (!CheckString(this.desc, searchCard.desc))
                return false;
            if (!CheckNumber(this.atk, searchCard.atk))
                return false;
            if (!CheckNumber(this.def, searchCard.def))
                return false;
            if (!CheckNumber(this.ot, searchCard.ot))
                return false;
            if (!CheckNumber(this.attribute, searchCard.attribute))
                return false;
            if (!CheckNumber(this.race, searchCard.race))
                return false;
            if (!CheckNumber(this.level&0xff, searchCard.level&0xff))
                return false;
            if (!CheckNumber2(this.type, searchCard.type))
                return false;
            if (!CheckNumber2(this.category, searchCard.category))
                return false;
            if (!CheckSetcode(this.setcode, searchCard.setcode))
                return false;
            return true;
        }
       private  bool CheckString(string str1, string str2)
        {
            if (str2 == null || str2.Length == 0)
                return true;
            else if (str1.IndexOf(str2) >= 0)
                return true;
            else
                return false;
        }
       private bool CheckNumber(int num1, int num2)
        {
            if (num2 == 0)//default
                return true;
            else if (num2 == -1 && num1 == 0)//search 0
                return true;
            else if (num1 == num2)//search -2 && >0
                return true;
            else
                return false;
        }
       private bool CheckSetcode(ulong num1,ulong num2)
        {
            if (num2 <= 0)//default
                return true;
            else if (num1 == 0 && num2 > 0)
                return false;
            //setcode1[0-3]==setcode2[0-3]
            else if ((num1 & 0xffff) == num2 && num2 < 0xffff)
                return true;
            //setcode1[4-7]==setcode2[4-7]
            else if ((num1 >> 0x10) == num2 && num2 < 0xffff)
                return true;
            else if (num1 == num2)
                return true;
            else if (((num1 & 0xffff) == (num2 >> 0x10)) &&
                ((num1 >> 0x10) == (num2 & 0xffff)))
                return true;
            else
                return false;
        }
       private bool CheckNumber2(long num1, long num2)
        {
            if (num2 == 0)
                return true;
            else if ((num1 & num2) == num2)
                return true;
            else
                return false;
        }
       private bool CheckCode(int code1, int alias1, int code2, int alias2)
        {
            if (alias2 == 0 && code2 == 0)
                return true;
            else if (alias2 == 0 && code1 == code2)//code1=code2
                return true;
            else if (alias2 < 0 && code1 >= code2)//code1>=code2
                return true;
            else if (code2 == 0 && (alias1 == alias2 || code1 == alias2))//alias1==alias2
                return true;
            else if (code2 < 0 && code1 <= alias2)//alias1>=alias2
                return true;
            //cod1>=code2 && code1<=alias2
            else if (code2 > 0 && alias2 > 0 && code1 >= code2 && code1 <= alias2)
                return true;
            else
                return false;
        }
        #endregion
    }
}
