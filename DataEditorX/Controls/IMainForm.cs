using System.Windows.Forms;

namespace DataEditorX.Controls
{
	public interface IMainForm
	{
		void CdbMenuClear();
		void LuaMenuClear();
		void AddCdbMenu(ToolStripItem item);
		void AddLuaMenu(ToolStripItem item);
		void Open(string file);
	}
}
