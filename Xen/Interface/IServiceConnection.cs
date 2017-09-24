using System;

namespace Xen.Interface
{
	public interface IServiceConnection : IService
	{
		object connection{ get; }

		bool IsConnected();

		bool Connect(string URL = "", string[] rest = null);

		bool Disconnect();

	}
}

