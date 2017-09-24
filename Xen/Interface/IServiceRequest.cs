using System;
using Xen.Net;

namespace Xen.Interface
{
	public interface IServiceRequest
	{
		string type { get; set; }
		object data { get; set; }

		Responder requester { get; }
	}
}

