using System;
using Xen.Event;
using Xen.Interface;
using UnityEngine;

namespace Xen.Net
{
	public class BaseService : EventDispatcher<ServiceEvent>, IService
	{
		protected IServiceConnection _connection;
		protected IServiceRequest _request;
		protected Responder _responder;

		public BaseService (IServiceConnection serviceConnection) : base()
		{
			this._connection = serviceConnection;
			this._responder = new Responder (this.Result, this.Fault);
		}

		public virtual bool Request(IServiceRequest request)
		{
			if (request == null || request.requester.Result == null  || request.requester.Fault == null)
			{
				Debug.Log (String.Format ("{0} : Request : unknown data object {1}", GetType(), request));
				return false;
			}

			this._request = request;
			return true;
		}

		protected virtual void Result(object data)
		{

		}

		protected virtual void Fault(object data)
		{

		}
	}
}

