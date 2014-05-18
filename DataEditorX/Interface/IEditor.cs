/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 16:14
 * 
 */
using System;
using DataEditorX.Core;

namespace DataEditorX.Interface
{
    public interface IEditor
    {
        string m_dbfile{get;set;}
        bool Open(string file);
        bool Close();
        bool Save();
        bool Reload();
        
        Card[] Search(Card modelCard);
        Card[] GetCards();
        
        bool Add(Card card);
        bool AddCards(Card[] card);
        bool Delete(Card card);
        bool DeleteCards(Card[] card);
        bool Change(Card oldCard,Card nowCard);
        
        bool Undo();
        bool Redo();
    }
}
