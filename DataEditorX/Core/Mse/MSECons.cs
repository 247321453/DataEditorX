using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditorX.Core.Mse
{
    public class MseAttribute
    {
        /// <summary>无</summary>
        public const string NONE = "none";
        /// <summary>暗</summary>
        public const string DARK = "dark";
        /// <summary>神</summary>
        public const string DIVINE = "divine";
        /// <summary>地</summary>
        public const string EARTH = "earth";
        /// <summary>火</summary>
        public const string FIRE = "fire";
        /// <summary>光</summary>
        public const string LIGHT = "light";
        /// <summary>水</summary>
        public const string WATER = "water";
        /// <summary>风</summary>
        public const string WIND = "wind";
        /// <summary>魔法</summary>
        public const string SPELL = "spell";
        /// <summary>陷阱</summary>
        public const string TRAP = "trap";
    }
    public class MseSpellTrap
    {
        /// <summary>装备</summary>
        public const string EQUIP = "+";
        /// <summary>速攻</summary>
        public const string QUICKPLAY = "$";
        /// <summary>场地</summary>
        public const string FIELD = "&";
        /// <summary>永续</summary>
        public const string CONTINUOUS = "%";
        /// <summary>仪式</summary>
        public const string RITUAL = "#";
        /// <summary>反击</summary>
        public const string COUNTER = "!";
        /// <summary>通常</summary>
        public const string NORMAL = "^";
    }
    public class MseCardType
    {
        /// <summary>通常</summary>
        public const string CARD_NORMAL = "normal monster";
        /// <summary>效果</summary>
        public const string CARD_EFFECT = "effect monster";
        /// <summary>超量</summary>
        public const string CARD_XYZ = "xyz monster";
        /// <summary>仪式</summary>
        public const string CARD_RITUAL = "ritual monster";
        /// <summary>融合</summary>
        public const string CARD_FUSION = "fusion monster";
        /// <summary>衍生物</summary>
        public const string CARD_TOKEN = "token monster";
        /// <summary>衍生物无种族</summary>
        public const string CARD_TOKEN2 = "token card";
        /// <summary>同调</summary>
        public const string CARD_SYNCHRO = "synchro monster";
        /// <summary>魔法</summary>
        public const string CARD_SPELL = "spell card";
        /// <summary>陷阱</summary>
        public const string CARD_TRAP = "trap card";
    }
}
