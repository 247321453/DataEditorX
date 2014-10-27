/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-15
 * 时间: 15:46
 * 
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Configuration;

namespace DataEditorX.Core
{
	/// <summary>
	/// Description of MSEConvert.
	/// </summary>
	public class MSEConvert
	{
				/*
		 * 
		normal monster	通常怪兽
		effect monster	效果怪兽
		fusion monster	融合怪兽
		ritual monster	仪式怪兽
		synchro monster	同调怪兽
		token monster	衍生物
		xyz monster	超量怪兽
		spell card	魔法
		trap card	陷阱
		 */
		MSEConfig cfg;
		Dictionary<long,string> mTypedic=null;
		Dictionary<long,string> mRacedic=null;
		public MSEConvert(Dictionary<long,string> typedic,
		                        Dictionary<long,string> racedic,
		                       MSEConfig _cfg)
		{
			mTypedic = typedic;
			mRacedic = racedic;
			cfg=_cfg;
		}
		public string GetST(Card c,bool isSpell)
		{
			string level;
			if(c.IsType(CardType.TYPE_EQUIP))
				level="+";
			else if(c.IsType(CardType.TYPE_QUICKPLAY))
				level="$";
			else if(c.IsType(CardType.TYPE_FIELD))
				level="&";
			else if(c.IsType(CardType.TYPE_CONTINUOUS))
				level="%";
			else if(c.IsType(CardType.TYPE_RITUAL))
				level="#";
			else if(c.IsType(CardType.TYPE_COUNTER))
				level="!";
			else if(cfg.st_is_symbol)
				level="^";
			else
				level="";
			
			if(isSpell)
				level=cfg.str_spell.Replace("%%",level);
			else
				level=cfg.str_trap.Replace("%%",level);
			return level;
		}
		public string reItalic(string str)
		{
			str=cn2tw(str);
			foreach(RegStr rs in cfg.replaces)
			{
				str= Regex.Replace(str, rs.pstr, rs.rstr);
			}
			return str;
		}
		public string cn2tw(string str)
		{
			if(cfg.Iscn2tw){
				str= Strings.StrConv(str,VbStrConv.TraditionalChinese,0);
				str=str.Replace("巖","岩");
			}
			return str;
		}
		public string ReDesc(string desc)
		{
			desc=cn2tw(desc);
			StringBuilder sb=new StringBuilder(reItalic(desc));
			
			sb.Replace(Environment.NewLine, "\n");
			sb.Replace("\n\n","\n");
			sb.Replace("\n","\n\t\t");
			return sb.ToString();
		}
		public string[] GetTypes(Card c)
		{
			string[] types=new string[]{"normal monster","","",""};
			if(c.IsType(CardType.TYPE_MONSTER))
			{//卡片类型和第1效果
				if(c.IsType(CardType.TYPE_XYZ)){
					types[0]="xyz monster";
					types[1]=GetType(CardType.TYPE_XYZ);
				}
				else if(c.IsType(CardType.TYPE_TOKEN)){
					if(c.race==0)
						types[0]="token card";
					else
						types[0]="token monster";
					types[1]=GetType(CardType.TYPE_TOKEN);
				}
				else if(c.IsType(CardType.TYPE_RITUAL)){
					types[0]="ritual monster";
					types[1]=GetType(CardType.TYPE_RITUAL);
				}
				else if(c.IsType(CardType.TYPE_FUSION)){
					types[0]="fusion monster";
					types[1]=GetType(CardType.TYPE_FUSION);
				}
				else if(c.IsType(CardType.TYPE_SYNCHRO)){
					types[0]="synchro monster";
					types[1]=GetType(CardType.TYPE_SYNCHRO);
				}
				else if(c.IsType(CardType.TYPE_EFFECT)){
					types[0]="effect monster";
				}
				else
					types[0]="normal monster";
				//同调
				if(types[0]=="synchro monster" || types[0]=="token monster")
				{
					if(c.IsType(CardType.TYPE_TUNER)
					   && c.IsType(CardType.TYPE_EFFECT))
					{//调整效果
						types[2]=GetType(CardType.TYPE_TUNER);
						types[3]=GetType(CardType.TYPE_EFFECT);
					}
					else if(c.IsType(CardType.TYPE_TUNER))
					{
						types[2]=GetType(CardType.TYPE_TUNER);
					}
					else if(c.IsType(CardType.TYPE_EFFECT))
					{
						types[2]=GetType(CardType.TYPE_EFFECT);
					}
				}
				else if(types[0] == "normal monster")
				{
					if(c.IsType(CardType.TYPE_PENDULUM))//灵摆
						types[1]=GetType(CardType.TYPE_PENDULUM);
					else if(c.IsType(CardType.TYPE_TUNER))//调整
						types[1]=GetType(CardType.TYPE_TUNER);
				}
				else if(types[0] != "effect monster")
				{//效果
					if(c.IsType(CardType.TYPE_EFFECT))
						types[2]=GetType(CardType.TYPE_EFFECT);
				}
				else
				{//效果怪兽
					types[2]=GetType(CardType.TYPE_EFFECT);
					if(c.IsType(CardType.TYPE_PENDULUM))
						types[1]=GetType(CardType.TYPE_PENDULUM);
					else if(c.IsType(CardType.TYPE_TUNER))
						types[1]=GetType(CardType.TYPE_TUNER);
					else if(c.IsType(CardType.TYPE_SPIRIT))
						types[1]=GetType(CardType.TYPE_SPIRIT);
					else if(c.IsType(CardType.TYPE_TOON))
						types[1]=GetType(CardType.TYPE_TOON);
					else if(c.IsType(CardType.TYPE_UNION))
						types[1]=GetType(CardType.TYPE_UNION);
					else if(c.IsType(CardType.TYPE_DUAL))
						types[1]=GetType(CardType.TYPE_DUAL);
					else if(c.IsType(CardType.TYPE_FLIP))
						types[1]=GetType(CardType.TYPE_FLIP);
					else{
						types[1]=GetType(CardType.TYPE_EFFECT);
						types[2]="";
					}
				}
				
			}
			if(c.race==0)
			{
				types[0]="";
				types[1]="";
				types[2]="";
			}
			return types;
		}
		
		string GetType(CardType type)
		{
			long key=(long)type;
			if(mTypedic==null)
				return "";
			if(mTypedic.ContainsKey(key))
				return cn2tw(mTypedic[key].Trim());
			return "";
		}
		
		public string GetStar(long level)
		{
			long j=level&0xff;
			string star="";
			for(int i=0;i<j;i++)
			{
				star+="*";
			}
			return star;
		}
		
		public string GetRace(long race)
		{
			if(race==0)
				return "";
			if(mRacedic==null)
				return "";
			if(mRacedic.ContainsKey(race))
				return cn2tw(mRacedic[race]);
			return "";
		}

		public string GetDesc(string desc,string regx)
		{
			desc=desc.Replace(Environment.NewLine,"\n");
			Regex regex=new Regex(regx);
			Match mc=regex.Match(desc);
			if(mc.Success)
				return (mc.Groups.Count>1)?
					mc.Groups[1].Value:mc.Groups[0].Value;
			return "";
		}
		
		public string GetAttribute(int attr)
		{
			CardAttribute cattr= (CardAttribute)attr;
			string sattr="none";
			switch(cattr)
			{
				case CardAttribute.ATTRIBUTE_DARK:
					sattr="dark";
					break;
				case CardAttribute.ATTRIBUTE_DEVINE:
					sattr="divine";
					break;
				case CardAttribute.ATTRIBUTE_EARTH:
					sattr="earth";
					break;
				case CardAttribute.ATTRIBUTE_FIRE:
					sattr="fire";
					break;
				case CardAttribute.ATTRIBUTE_LIGHT:
					sattr="light";
					break;
				case CardAttribute.ATTRIBUTE_WATER:
					sattr="water";
					break;
				case CardAttribute.ATTRIBUTE_WIND:
					sattr="wind";
					break;
			}
			return sattr;
		}
	}
	
}
