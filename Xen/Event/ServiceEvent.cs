using System;
using Xen.Interface;
using Xen.Data;

namespace Xen.Event
{
	public class ServiceEvent : AppEvent
	{
		public const string BROADCAST = "broadcast";
		public const string RESPONSE = "response";
		public const string FAULT = "fault";
		public const string CONNECT_FAIL = "connectFail";
		public const string CONNECT_SUCCESS = "connectSuccess";
		public const string CONNECTING = "connecting";
		public const string NET_STATUS = "netStatus";
		public const string DISCONNECTED = "disconnected";

		protected IServiceResponse _response;
		public IServiceResponse response { get{ return this._response; } }

		protected ResultVO _result; 
		public ResultVO result{ get{ return this._result; } }

		public ServiceEvent (
								string type, 
								IServiceResponse serviceResponse = null, 
								object data = null, 
								bool bubbles = false, 
								bool cancelable = false
							) : base (type, null, bubbles, cancelable)
		{
			this._response = serviceResponse;
			if (data is ResultVO) 
			{
				this._result = data as ResultVO;
			}

				
		}

		public override IVO Clone ()
		{
			return new ServiceEvent (this.type, this._response, this._result, this.bubbles, this.cancelable);
		}
	}
}

