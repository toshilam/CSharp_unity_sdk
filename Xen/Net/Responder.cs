using System;
using Xen.Interface;

namespace Xen.Net
{
	public class Responder : IResponder
	{
		private Action<object> _Result;
		public Action<object> Result{get{ return this._Result; }}

		private Action<object> _Fault;
		public Action<object> Fault{get{ return this._Fault; }}


		public Responder (Action<object> result, Action<object> fault)
		{
			this._Result = result;
			this._Fault = fault;
		}
	}
}

