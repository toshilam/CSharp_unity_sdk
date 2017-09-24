using System;
using UnityEngine;

namespace Xen.Utils
{
	public class Tracer
	{
		public Tracer ()
		{
		}

		public static void Echo(object message, object target = null, int color = 0x111111)
		{
			Debug.Log(message);
			/*MonsterDebugger.trace
			(
				inTarget == null ? Tracer : inTarget, 
				inMessage,
				inColor
			);*/
		}
	}
}

