using System;

namespace Xen.Interface
{
	public interface IServiceResponse
	{
		IServiceRequest request { get; }
		object data { get; }
	}
}

