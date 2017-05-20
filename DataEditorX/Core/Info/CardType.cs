/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-13
 * 时间: 9:08
 * 
 */
using System;
using System.Collections.Generic;

namespace DataEditorX.Core.Info
{
	/// <summary>
	/// 卡片类型
	/// </summary>
	public enum CardType : long
	{
		///<summary>怪兽卡</summary>
		TYPE_MONSTER		=0x1		,
		///<summary>魔法卡</summary>
		TYPE_SPELL			=0x2		,
		///<summary>陷阱卡</summary>
		TYPE_TRAP			=0x4		,
		
		///<summary>通常</summary>
		TYPE_NORMAL			=0x10		,
		///<summary>效果</summary>
		TYPE_EFFECT			=0x20		,
		///<summary>融合</summary>
		TYPE_FUSION			=0x40		,
		///<summary>仪式</summary>
		TYPE_RITUAL			=0x80		,
		///<summary>陷阱怪兽</summary>
		TYPE_TRAPMONSTER	=0x100		,
		///<summary>灵魂</summary>
		TYPE_SPIRIT			=0x200		,
		///<summary>同盟</summary>
		TYPE_UNION			=0x400		,
		///<summary>二重</summary>
		TYPE_DUAL			=0x800		,
		///<summary>调整</summary>
		TYPE_TUNER			=0x1000		,
		///<summary>同调</summary>
		TYPE_SYNCHRO		=0x2000		,
		///<summary>衍生物</summary>
		TYPE_TOKEN			=0x4000		,
		
		///<summary>速攻</summary>
		TYPE_QUICKPLAY		=0x10000	,
		///<summary>永续</summary>
		TYPE_CONTINUOUS		=0x20000	,
		///<summary>装备</summary>
		TYPE_EQUIP			=0x40000	,
		///<summary>场地</summary>
		TYPE_FIELD			=0x80000	,
		///<summary>反击</summary>
		TYPE_COUNTER		=0x100000	,
		
		///<summary>反转</summary>
		TYPE_FLIP			=0x200000	,
		///<summary>卡通</summary>
		TYPE_TOON			=0x400000	,
		///<summary>超量</summary>
		TYPE_XYZ			=0x800000	,
		///<summary>灵摆</summary>
		TYPE_PENDULUM		=0x1000000  ,
		///<summary>特殊召唤</summary>
		TYPE_SPSUMMON       =0x2000000  ,
		///<summary>连接</summary>
		TYPE_LINK           =0x4000000  ,

	}
	public static class CardTypes{
		public static readonly CardType[] TYPE1 = {
			CardType.TYPE_LINK,
			CardType.TYPE_XYZ,
			CardType.TYPE_SYNCHRO,
			CardType.TYPE_PENDULUM,
			CardType.TYPE_RITUAL,
			CardType.TYPE_FUSION,
			CardType.TYPE_SPIRIT,
			CardType.TYPE_UNION,
			CardType.TYPE_DUAL,
			CardType.TYPE_FLIP,
			CardType.TYPE_TOON,
		};
		public static readonly CardType[] TYPE2 = {
			CardType.TYPE_PENDULUM,
			CardType.TYPE_SYNCHRO,
			CardType.TYPE_SPIRIT,
			CardType.TYPE_UNION,
			CardType.TYPE_DUAL,
			CardType.TYPE_FLIP,
			CardType.TYPE_TOON
		};
		public static readonly CardType[] TYPE3 ={
			CardType.TYPE_TUNER,
			CardType.TYPE_EFFECT
		};
		public static CardType[] GetMonsterTypes(long type){
			var list = new List<CardType>(4);
			var typeList=new List<CardType[]>(4);
			typeList.Add(TYPE1);
			typeList.Add(TYPE2);
			typeList.Add(TYPE3);
			typeList.Add(TYPE3);
			typeList.Add(TYPE3);
			int count = typeList.Count;
			for(int i=0;i<count;i++){
				CardType[] types = typeList[i];
				foreach(var t in types){
					if((type & (long)t)==(long)t){
						if(!list.Contains(t)){
							list.Add(t);
							break;
						}
					}
				}
			}
			return list.ToArray();
		}
	}
}
