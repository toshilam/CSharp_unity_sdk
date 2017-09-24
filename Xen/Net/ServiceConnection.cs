using System;
using Xen.Event;
using Xen.Interface;

namespace Xen.Net
{
	public class ServiceConnection : EventDispatcher<ServiceEvent>, IServiceConnection //, IResponder
	{
		public const string UNIQUE_ID_PN = "uniqueID";

		protected Responder _responder;
		public Responder responder{ get{ return this._responder; }}

		protected bool _isConnected = false;

		//to be implemented in child class
		public virtual object connection{ get{ return null;} }

		public ServiceConnection (object connection) : base()
		{
			_responder = new Responder (Result, Fault);
		}

		public virtual bool Request(IServiceRequest request)
		{
			return false;
		}

		public virtual bool IsConnected()
		{
			return this._isConnected;
		}

		public virtual bool Connect(string URL = "", params string[] rest)
		{
			return false;
		}

		public virtual bool Disconnect()
		{
			this._isConnected = false;
			return true;
		}

		public virtual void Result(object data)
		{
			
		}

		public virtual void Fault(object info)
		{

		}
	}
}

