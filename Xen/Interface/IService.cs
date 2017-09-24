using System;

namespace Xen.Interface
{
	public interface IService
	{
		bool Request(IServiceRequest request);
	}
}

