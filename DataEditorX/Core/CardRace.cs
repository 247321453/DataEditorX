/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-13
 * 时间: 9:44
 * 
 */
using System;

namespace DataEditorX.Core
{
    /// <summary>
    /// 卡片种族
    /// </summary>
    public enum CardRace : long
    {
        RACE_NONE = 0,
        ///<summary>战士</summary>
        RACE_WARRIOR = 0x1,
        ///<summary>魔法师</summary>
        RACE_SPELLCASTER = 0x2,
        ///<summary>天使</summary>
        RACE_FAIRY = 0x4,
        ///<summary>恶魔</summary>
        RACE_FIEND = 0x8,
        ///<summary>不死</summary>
        RACE_ZOMBIE = 0x10,
        ///<summary>机械</summary>
        RACE_MACHINE = 0x20,
        ///<summary>水</summary>
        RACE_AQUA = 0x40,
        ///<summary>炎</summary>
        RACE_PYRO = 0x80,
        ///<summary>岩石</summary>
        RACE_ROCK = 0x100,
        ///<summary>鸟兽</summary>
        RACE_WINDBEAST = 0x200,
        ///<summary>植物</summary>
        RACE_PLANT = 0x400,
        ///<summary>昆虫</summary>
        RACE_INSECT = 0x800,
        ///<summary>雷</summary>
        RACE_THUNDER = 0x1000,
        ///<summary>龙</summary>
        RACE_DRAGON = 0x2000,
        ///<summary>兽</summary>
        RACE_BEAST = 0x4000,
        ///<summary>兽战士</summary>
        RACE_BEASTWARRIOR = 0x8000,
        ///<summary>恐龙</summary>
        RACE_DINOSAUR = 0x10000,
        ///<summary>鱼</summary>
        RACE_FISH = 0x20000,
        ///<summary>海龙</summary>
        RACE_SEASERPENT = 0x40000,
        ///<summary>爬虫</summary>
        RACE_REPTILE = 0x80000,
        ///<summary>念动力</summary>
        RACE_PSYCHO = 0x100000,
        ///<summary>幻神兽</summary>
        RACE_DEVINE = 0x200000,
        ///<summary>创造神</summary>
        RACE_CREATORGOD = 0x400000,
        ///<summary>幻龙</summary>
        RACE_WYRM = 0x800000,
    }
}
