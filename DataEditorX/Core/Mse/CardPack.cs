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
			if(rarity.Equals("common")){
				return "common";
			}
			if(rarity.Equals("rare")){
				return "rare";
			}
			if(rarity.Equals("super") ||rarity.Equals("super rare")){
				return "super rare";
			}
			if(rarity.Contains("secret")){
				return "secret rare";
			}
			if(rarity.Contains("parallel")){
				return "parallel rare";
			}
			if(rarity.Contains("ultimate")){
				return "ultimate rare";
			}
			if(rarity.Contains("ultra")){
				return "ultra rare";
			}
			if(rarity.Contains("gold")){
				return "gold tech";
			}
			if(rarity.Contains("promo")){
				return "promo";
			}
			return this.rarity.Split('/')[0];
		}
	}
}
