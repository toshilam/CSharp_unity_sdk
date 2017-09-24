using System;
using Xen.Interface;
using System.Collections;

namespace Xen.Net
{
	public class ServiceRequest : IServiceRequest
	{
		public string type { get; set; }
		public object data { get; set; }

		protected Responder _requester;
		public Responder requester { get{ return this._requester; } }

		public ServiceRequest (string type, object data = null, Responder responder = null)
		{
			this.type = type;
			this.data = data;
			this._requester = responder;
		}

	}
}

