using System;
using UnityEngine;
using Xen.Interface;

namespace Xen.Data
{
	public class VO : object, IVO
	{
		public readonly string id;

		public VO (string id)
		{
			this.id = id;
		}

		public virtual IVO Clone()
		{
			return new VO (this.id);
		}


		public virtual void Clear()
		{
			
		}

	}
}

