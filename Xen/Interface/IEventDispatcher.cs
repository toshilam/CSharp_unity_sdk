using System;
using Xen.Event;

namespace Xen.Interface
{
	public interface IEventDispatcher<TEvent>
	{

		bool AddEventListener(string type, Action<TEvent> handler);

		bool RemoveEventListener(string type, Action<TEvent> handler);

		bool HasEventListener(string type);

		void DispatchEvent (TEvent e);

		void RemoveAllEventListener();
	}
}
