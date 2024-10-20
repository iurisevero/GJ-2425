using System;
using UnityEngine;

namespace GJ
{
	[Serializable]
	public class ObjectPoolItem
	{
		public ObjectPoolItem(string t, GameObject obj, int amt, bool exp = true)
		{
			this.tag = t;
			this.objectToPool = obj;
			this.amountToPool = Mathf.Max(amt, 2);
			this.shouldExpand = exp;
		}

		public string tag;

		public GameObject objectToPool;

		public int amountToPool;

		public bool shouldExpand = true;
	}
}
