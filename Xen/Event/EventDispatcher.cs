using System;
using Xen.Interface;
using System.Collections.Generic;
using UnityEngine;
using Xen.Utils;

namespace Xen.Event
{
	public class EventDispatcher<TEvent> : IEventDispatcher<TEvent>, IDisposable where TEvent : IAppEvent
	{
		private Dictionary<string, List<Action<TEvent>>> _dicHandler;

		public EventDispatcher ()
		{
			Debug.Log (String.Format("{0} : Init!", this.GetType()) );
			this._dicHandler = new Dictionary<string, List<Action<TEvent>>> ();
		}



		public virtual void Dispose()
		{
			Debug.Log (String.Format("{0} : Dispose : clear up all listener", this.GetType()) );
			this.RemoveAllEventListener ();
		}

		public bool AddEventListener(string Type, Action<TEvent> handler)
		{
			if (String.IsNullOrEmpty (Type) || handler == null) 
			{
				Debug.Log (String.Format("{0} : AddEventListener : unknown data!", this.GetType()) );
			}

			if (!this.HasEventListener (Type))
			{
				this._dicHandler.Add (Type, new List<Action<TEvent>> ());
			}

			List<Action<TEvent>> list = this._dicHandler [Type];

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

		public bool RemoveEventListener(string Type, Action<TEvent> handler)
		{
			if (this.HasEventListener(Type))
			{
				List<Action<TEvent>> list = this._dicHandler [Type];

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

		public void DispatchEvent (TEvent e)
		{
			if (e == null || String.IsNullOrEmpty (e.type))
			{
				Debug.Log (String.Format("{0} : DispatchEvent : unknown data!", this.GetType()));
				Debug.Log (e);
				return;
			}

			if (this.HasEventListener (e.type))
			{
				List<Action<TEvent>> list = this._dicHandler [e.type];
				int numItem = list.Count;

				if (numItem > 0)
				{
					List<Action<TEvent>> tempList = new List<Action<TEvent>> (list.ToArray ());


					while (tempList.Count > 0)
					{
						try
						{
							tempList [0] (e);
						}
						catch(Exception err)
						{
							Tracer.Echo (String.Format("{0} : DispatchEvent : error : {1}",GetType(), err) );
						}
							
						
						tempList.RemoveAt (0);
					}
				}

			}

		}

		public void RemoveAllEventListener()
		{
			if (this._dicHandler.Values != null) 
			{
				foreach (List<Action<TEvent>> list in this._dicHandler.Values)
				{
					while(list.Count > 0)
					{
						list.RemoveAt (0);

					}
				}

				this._dicHandler.Clear ();
				//Debug.Log (this._dicHandler);
			}

		}
	}
}

