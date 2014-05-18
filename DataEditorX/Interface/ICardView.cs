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
    public interface ICardView
    {
        Card m_oldcard{set;get;}
        
        Card GetCard();
        bool SetCard();
    }
}
