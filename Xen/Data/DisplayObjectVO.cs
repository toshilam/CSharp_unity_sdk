using System;
using UnityEngine;

namespace Xen.Data
{
	public class DisplayObjectVO : VO
	{
		public object data;
//		public XML ;
		public MonoBehaviour displayObject;

		public DisplayObjectVO (string id, MonoBehaviour displayObject, object data =null) : base(id)
		{
			this.displayObject = displayObject;
			this.data = data;
		}
	}
}

