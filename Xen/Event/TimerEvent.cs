using System;
using Xen.Interface;

namespace Xen.Event
{
	public class TimerEvent : AppEvent
	{
		public const string TIMER = "timer";
		public const string TIMER_COMPLETE = "timerComplete";

		public TimerEvent (string type, object target) : base(type, target)
		{
			
		}

		public override IVO Clone ()
		{
			return new TimerEvent (type, this.target);
		}

		public override string ToString ()
		{
			return String.Format ("[TimerEvent(type={0})]", type);
		}
	}
}

