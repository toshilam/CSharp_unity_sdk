using System;
using Xen.Event;
using Xen.Net;

namespace Xen.Interface
{
	public interface IResponder
	{
//		Responder responder{get;}

		Action<object> Result{ get;}

		Action<object> Fault{ get;}
	}
}

