using System;
using Xen.Interface;

namespace Xen.Resource
{
	public class ServiceManager : DataManager<string, IService>
	{
		public ServiceManager () : base()
		{
		}
	}
}

