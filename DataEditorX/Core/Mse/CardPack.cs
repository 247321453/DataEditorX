/*
 * 由SharpDevelop创建。
 * 用户： Hasee
 * 日期: 2016/2/27
 * 时间: 7:55
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace DataEditorX.Core
{
	/// <summary>
	/// Description of CardPack.
	/// </summary>
	public class CardPack
	{
		public CardPack(long id)
		{
			this.card_id=id;
		}
		
		public long card_id{
			get;
			private set;
		}
		public string pack_id;
		public string pack_name;
		public string rarity;
		public string date;
		
		public string getMseRarity(){
			if(this.rarity==null)
				return "common";
			string rarity=this.rarity.Trim().ToLower();
			if(rarity.Equals("common") || rarity.Equals("short print"))
			{
				return "common";
			}
			if(rarity.Equals("rare") ||rarity.Equals("normal rare"))
			{
				return "rare";
			}
			else if(rarity.Contains("parallel") || rarity.Contains("Kaiba") || rarity.Contains("duel terminal"))
			{
				return "parallel rare";
			}
			else if(rarity.Contains("super") ||rarity.Contains("holofoil"))
			{
				return "super rare";
			}
			else if(rarity.Contains("ultra"))
			{
				return "ultra rare";
			}
			else if(rarity.Contains("secret"))
			{
				return "secret rare";
			}
			else if(rarity.Contains("gold"))
			{
				return "gold rare";
			}
			else if(rarity.Contains("ultimate"))
			{
				return "ultimate rare";
			}
			else if(rarity.Contains("prismatic"))
			{
				return "prismatic rare";
			}
			else if(rarity.Contains("star"))
			{
				return "star rare";
			}
			else if(rarity.Contains("mosaic"))
			{
				return "mosaic rare";
			}
			else if(rarity.Contains("platinum"))
			{
				return "platinum rare";
			}
			else if(rarity.Contains("ghost") || rarity.Contains("holographic"))
			{
				return "ghost rare";
			}
			else if(rarity.Contains("millenium"))
			{
				return "millenium rare";
			}
			return this.rarity.Split('/')[0];
		}
	}
}
