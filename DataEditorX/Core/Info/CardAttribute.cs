/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-13
 * 时间: 9:47
 * 
 */
using System;

namespace DataEditorX.Core.Info
{
    /// <summary>
    /// 卡片属性
    /// </summary>
    public enum CardAttribute : int
    {
        /// <summary>
        /// 地
        /// </summary>
        ATTRIBUTE_EARTH = 0x01,
        /// <summary>
        /// 水
        /// </summary>
        ATTRIBUTE_WATER = 0x02,
        /// <summary>
        /// 炎
        /// </summary>
        ATTRIBUTE_FIRE = 0x04,
        /// <summary>
        /// 风
        /// </summary>
        ATTRIBUTE_WIND = 0x08,
        /// <summary>
        /// 光
        /// </summary>
        ATTRIBUTE_LIGHT = 0x10,
        /// <summary>
        /// 暗
        /// </summary>
        ATTRIBUTE_DARK = 0x20,
        /// <summary>
        /// 神
        /// </summary>
        ATTRIBUTE_DEVINE = 0x40,
    }
}
