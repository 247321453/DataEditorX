using DataEditorX.Config;
using DataEditorX.Controls;

namespace DataEditorX.Core
{
	public interface IDataForm : IEditForm
	{
		YgoPath GetPath();
		Card[] GetCardList(bool onlyselect);
		bool CheckOpen();
		void Reset();
		void SetImage(long id);
		void SetImage(string id);
		void Search(bool isfresh);
		Card GetOldCard();
		Card GetCard();
		void SetCard(Card c);
		void SaveCards(Card[] cards);
		void CompareCards(string cdbfile, bool checktext);
	}
}
