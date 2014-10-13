/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-13
 * 时间: 9:08
 * 
 */
using System;

namespace DataEditorX.Core
{
	/// <summary>
	/// Description of CardType.
	/// </summary>
	public enum CardType : long
	{
		TYPE_MONSTER		=0x1		,//--怪兽卡
		TYPE_SPELL			=0x2		,//--魔法卡
		TYPE_TRAP			=0x4		,//--陷阱卡
		TYPE_NORMAL			=0x10		,//--通常
		TYPE_EFFECT			=0x20		,//--效果
		TYPE_FUSION			=0x40		,//--融合
		TYPE_RITUAL			=0x80		,//--仪式
		TYPE_TRAPMONSTER	=0x100		,//--陷阱怪兽
		TYPE_SPIRIT			=0x200		,//--灵魂
		TYPE_UNION			=0x400		,//--同盟
		TYPE_DUAL			=0x800		,//--二重
		TYPE_TUNER			=0x1000		,//--调整
		TYPE_SYNCHRO		=0x2000		,//--同调
		TYPE_TOKEN			=0x4000		,//--衍生物
		TYPE_QUICKPLAY		=0x10000	,//--速攻
		TYPE_CONTINUOUS		=0x20000	,//--永续
		TYPE_EQUIP			=0x40000	,//--装备
		TYPE_FIELD			=0x80000	,//--场地
		TYPE_COUNTER		=0x100000	,//--反击
		TYPE_FLIP			=0x200000	,//--翻转
		TYPE_TOON			=0x400000	,//--卡通
		TYPE_XYZ			=0x800000	,//--超量
		TYPE_PENDULUM		=0x1000000	,//--摇摆
	}
}
