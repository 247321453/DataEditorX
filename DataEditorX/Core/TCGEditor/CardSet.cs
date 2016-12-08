

using System;
using System.IO;
using Newtonsoft.Json;

namespace DataEditorX
{
	public class CardSet{
		public int game;
		public int version;
		public MySortList<string, CardInfo> cards;
	}
	public class CardInfo{
		public string title;
		public string artwork;
		public int[] artwork_crop;
		/// <summary>
		/// ?
		/// </summary>
		public int background;
		/// <summary>
		/// ?
		/// </summary>
		public int rarity;
		public int attribute;
		public int level;
		public int icon;
		public string description;
		public string pendulum_description;
		public int[] pendulum_scales ;
		public string[] subtypes;
		public string atk;
		public string def;
		public string edition;
		public string @set;
		public string card_number;
		public string limitation;
		/// <summary>
		/// 0,1,1
		/// </summary>
		public int sticker;
		public string copyright;
		public override string ToString()
		{
			return string.Format("[CardInfo Title={0}, Artwork={1}, Artwork_crop={2}, Background={3}, Rarity={4}, Attribute={5}, Level={6}, Icon={7}, Description={8}, Pendulum_description={9}, Pendulum_scales={10}, Subtypes={11}, Atk={12}, Def={13}, Edition={14}, Set={15}, Card_number={16}, Limitation={17}, Sticker={18}, Copyright={19}]", title, artwork, artwork_crop, background, rarity, attribute, level, icon, description, pendulum_description, pendulum_scales, subtypes, atk, def, edition, set, card_number, limitation, sticker, copyright);
		}
	}
	
	public class CardJson{
		public static void test(){
			 string json = File.ReadAllText(@"F:\TCGEditor_v1.2\t.tcgb");
			 CardSet cardset = JsonConvert.DeserializeObject<CardSet>(json);
			 if(cardset.cards!=null){
			 	int index=0;
			 	foreach(string key in cardset.cards.Keys){
			 		Console.WriteLine(key);
			 		CardInfo card = cardset.cards.Values[index];
			 		Console.WriteLine(card);
			 		index++;
			 	}
			 }
			 Console.ReadKey();
		}
	}
}