
using System;
using System.Collections.Generic;

namespace DataEditorX
{
	class MyComparer<K> : IComparer<K>
	{
		public int Compare(K x, K y)
		{
			return 1;   //永远不等，允许重复
		}
	}
	
	public class MySortList<K,V> : SortedList<K,V>
	{

		public MySortList():base(new MyComparer<K>())
		{
		}
		
		public new void Add(K key, V value)
		{
			//falg用于跳出函数
			int flag = 0;
			//检查是否具备这个key，并且检查value是否重复
			foreach (KeyValuePair<K,V> item in this)
			{
				if (item.Key.ToString() == key.ToString() && item.Value.ToString() == value.ToString())
				{
					flag=1;
				}
			}
			if (flag == 1)
				return;  //跳出函数
			//否则就加入
			base.Add(key, value);
		}
	}
}
