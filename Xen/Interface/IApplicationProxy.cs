using System;

namespace Xen.Interface
{
	public interface IApplicationProxy
	{
		IModuleMain host{ get; }
		void InitAsset(object Asset = null);
		bool Request(IServiceRequest request);
	}
}

