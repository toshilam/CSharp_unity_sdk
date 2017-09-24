using System;

namespace Xen.Interface
{
	public interface IAppEvent : IVO
	{
		string type { get;}
		object target { get; }
		object currentTarget { get; }
		int eventPhase { get; set; }
		bool bubbles { get; set; }
		bool cancelable { get; set; }
		int timeStamp { get; set; }
		bool defaultPrevented { get; set; }
	}
}

