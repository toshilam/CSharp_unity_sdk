using System;
using Xen.Interface;
using System.Collections;

namespace Xen.Net
{
	public class ServiceResponse : IServiceResponse
	{
		protected IServiceRequest _request;
		public IServiceRequest request { get{ return this._request; } }

		protected object _data;
		public object data { get{ return this._data; } }

		public ServiceResponse (IServiceRequest request, object data)
		{
			this._request = request;
			this._data = data;
		}

	}
}

