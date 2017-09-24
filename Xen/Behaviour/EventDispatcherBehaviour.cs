using System;
using UnityEngine;
using System.Collections.Generic;
using Xen.Interface;
using Xen.Event;

namespace Xen.Behaviour
{
	public class EventDispatcherBehaviour : MonoBehaviour , IDisposable
	{
		//		private Dictionary<string, List<Action<AppEvent>>> _dicHandler;
		protected EventDispatcher<IAppEvent> _eventDispatcher;
		public EventDispatcher<IAppEvent> eventDispatcher{ get{ return this._eventDispatcher; } }

		public EventDispatcherBehaviour():base()
		{
			_eventDispatcher = new EventDispatcher<IAppEvent>();
		}

		public virtual bool Awake ()
		{
			Debug.Log (String.Format("{0} : Awake!", this.GetType()) );
			//			this._dicHandler = new Dictionary<string, List<Action<AppEvent>>> ();
			//			this._eventDispatcher = new EventDispatcher<IAppEvent>();

			this.eventDispatcher.DispatchEvent (new AppEvent (AppEvent.AWAKE, this));

			return true;
		}

		public virtual void OnEnable()
		{
			Debug.Log (String.Format("{0} : OnEnable!", this.GetType()) );

			this.eventDispatcher.DispatchEvent (new AppEvent (AppEvent.ON_ENABLE, this));
		}

		// Use this for initialization
		public virtual void Start () 
		{
			Debug.Log (String.Format("{0} : Start!", this.GetType()) );

			this.eventDispatcher.DispatchEvent (new AppEvent (AppEvent.START, this));
		}

		// Update is called once per frame
		public virtual void Update () 
		{

		}

		public virtual void OnDisable()
		{
			Debug.Log (String.Format("{0} : OnDisable!", this.GetType()) );

			this.eventDispatcher.DispatchEvent (new AppEvent (AppEvent.ON_DISABLE, this));
		}

		public virtual void OnDestroy()
		{
			Debug.Log (String.Format("{0} : OnDestroy : clear up all listener", this.GetType()) );
			this.Dispose ();

			this.eventDispatcher.DispatchEvent (new AppEvent (AppEvent.ON_DESTROY, this));
		}

		public virtual void Dispose()
		{
			Debug.Log (String.Format("{0} : Dispose : clear up all listener", this.GetType()) );
			this.eventDispatcher.Dispose ();
		}

		/*public bool AddEventListener(string Type, Action<AppEvent> handler)
		{
			if (String.IsNullOrEmpty (Type) || handler == null) 
			{
				Debug.Log (String.Format("{0} : AddEventListener : unknown data!", this.GetType()) );
			}

			if (!this.HasEventListener (Type))
			{
				this._dicHandler.Add (Type, new List<Action<AppEvent>> ());
			}

			List<Action<AppEvent>> list = this._dicHandler [Type];

			if (!list.Contains (handler))
			{
				list.Add (handler);
				Debug.Log (String.Format("{0} : AddEventListener : added for Type {1}", this.GetType(), Type));
//				Debug.Log (list);

				return true;
			}

			Debug.Log (String.Format("EventDispatcher : AddEventListener : depulicated handler for Type {0}", Type));
			return false;
		}

		public bool RemoveEventListener(string Type, Action<AppEvent> handler)
		{
			if (this.HasEventListener(Type))
			{
				List<Action<AppEvent>> list = this._dicHandler [Type];

				if (list.Contains (handler))
				{
					if (list.Remove (handler))
					{
						if (list.Count == 0)
						{
							this._dicHandler.Remove (Type); 
						}

						return true;
					}
				}

			}

			Debug.Log (String.Format("{0} : RemoveEventListener : no matched listener found for Type {0}", this.GetType(), Type));
			return false;
		}

		public bool HasEventListener(string Type)
		{
			return this._dicHandler.ContainsKey (Type);
		}

		public void DispatchEvent (AppEvent e)
		{
			if (e == null || String.IsNullOrEmpty (e.type))
			{
				Debug.Log (String.Format("{0} : DispatchEvent : unknown data!", this.GetType()));
				return;
			}

			if (this.HasEventListener (e.type))
			{
				List<Action<AppEvent>> list = this._dicHandler [e.type];
				int numItem = list.Count;

				if (numItem > 0)
				{
					List<Action<AppEvent>> tempList = new List<Action<AppEvent>> (list.ToArray ());


					while (tempList.Count > 0)
					{
						tempList [0] (e);
						tempList.RemoveAt (0);
					}
				}

			}

		}

		public void RemoveAllEventListener()
		{
			if (this._dicHandler.Values != null) 
			{
				foreach (List<Action<AppEvent>> list in this._dicHandler.Values)
				{
					while(list.Count > 0)
					{
						list.RemoveAt (0);

					}
				}

				this._dicHandler.Clear ();
//				Debug.Log (this._dicHandler);
			}

		}*/
	}
}

