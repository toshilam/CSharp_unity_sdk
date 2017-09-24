using System;
using UnityEngine;
using Xen.Interface;
using Xen.Data;
using Xen.Utils;

namespace Xen.Event
{
	public class AppEvent : VO, IVO, IAppEvent
	{
		public const string AWAKE = "awake";
		public const string ON_ENABLE = "onEnable";
		public const string START = "start";
		public const string ON_DISABLE = "onDisable";
		public const string ON_DESTROY = "onDestroy";

		public const string SCENE_CHANGE = "sceneChange";

		public const string CONNECT_MEDIATOR = "connectMediator";
		public const string DISCONNECT_MEDIATOR = "disconnectMediator";

		protected string _type;
		public string type{get{ return this._type; }}

		public object _target;
		public object target { get{ return this._target;}}

		public object _currentTarget;
		public object currentTarget {get{ return this._currentTarget;}}

		public int eventPhase { get; set; }
		public bool bubbles { get; set; }
		public bool cancelable { get; set; }
		public int timeStamp { get; set; }
		public bool defaultPrevented { get; set; }

		public AppEvent (string type, object target, bool bubbles = false, bool cancelable = false ):base(type)
		{
			this._type = type;
			this._target = target;
			this.bubbles = bubbles;
			this.cancelable = cancelable;
			this.timeStamp = Tool.GetTimeStamp();
		}

//		public void PreventDefault () 
//		{
//			this.DefaultPrevented = true;
//		}

		public override IVO Clone ()
		{
			return new AppEvent(this.type, this.target, this.bubbles, this.cancelable);
		} 

		public override string ToString ()
		{
			return String.Format ("[AppEvent: type={0}, target={1}, currentTarget={2}, eventPhase={3}, bubbles={4}, cancelable={5}, timeStamp={6}, defaultPrevented={7}]", type, target, currentTarget, eventPhase, bubbles, cancelable, timeStamp, defaultPrevented);
		}
	}
}

