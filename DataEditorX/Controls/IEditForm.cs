using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditorX.Controls
{
    interface IEditForm
    {
        string GetOpenFile();
        bool Create(string file);
        bool Open(string file);
        bool CanOpen(string file);
        bool Save();
        void SetActived();
    }
}
