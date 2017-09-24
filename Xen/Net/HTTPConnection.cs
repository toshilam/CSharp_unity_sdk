using System;
using Xen.Interface;
using Xen.Utils;
using Xen.Data;
using UnityEngine;
using System.Collections;
using Xen.Enum;
using System.Collections.Generic;
using Xen.Event;
using Xen.Behaviour;

namespace Xen.Net
{
	public class HTTPConnection : ServiceConnection
	{
		protected string _URL;
		public string URL { get{ return this._URL; } }

		protected Timer _timeOutTimer;
		protected WWW _www;
		protected AppBehaviour _behaviour;
		protected IServiceRequest _request;
		protected bool _isRequesting;

		//a list of requests that waiting for response
//		protected Dictionary<string, IServiceRequest> _requestList;

		//TODO : currently only support using for one time.
		//NOTE : NOT USED!!! it should only be ran in main thread
		public HTTPConnection (string url, AppBehaviour behaviour, double timeOutInterval = -1) : base(null)
		{
			if (String.IsNullOrEmpty (url) || url.IndexOf ("http") == -1 || !(behaviour is AppBehaviour))
			{
				throw new ArgumentException ("HTTPConnection : expecting url!");
			}

			if (timeOutInterval > 0)
			{
				this._timeOutTimer = new Timer (timeOutInterval, 1, behaviour);
				this._timeOutTimer.AddEventListener (TimerEvent.TIMER_COMPLETE, this._TimerEvnetHandler);
			}


//			this._requestList = new Dictionary<string, IServiceRequest> ();
			this._URL = url;
			this._behaviour = behaviour;
			this._isRequesting = false;
		}

		public override bool Request (IServiceRequest request)
		{
			if (!(request is IServiceRequest) || !(request.data is ResultVO) || request.requester == null)  
			{
				Tracer.Echo ("HTTPConnection : Request : unknown data!");
				return false;
			}

			ResultVO reqData = request.data as ResultVO;

			/*if (!(reqData.result is WWWForm))
			{
				Tracer.Echo ("HTTPConnection : Request : expecting WWWForm!");
				return false;
			}*/

			this._request = request;

			reqData.uniqueID = Tool.GetUniqueID (GetType().ToString());

			this._behaviour.AddUIThreadAction (new ServiceRequest ("", null, new Responder (this._StartProcess, null)));

			/*IEnumerator enumerator = this._Process (request);
			WWW w = null;
			while (enumerator.MoveNext ())
			{
				object currentObject = enumerator.Current;
				if (currentObject is WWW)
				{
					w = currentObject as WWW;
				}
				else
				{
					string dataToString = currentObject.ToString ().ToLower ();
					if (dataToString == "true" || dataToString == "false")
					{
						string resultCode = dataToString == "true" && w != null && string.IsNullOrEmpty(w.error) ? ErrorCode.NONE : ErrorCode.PARAMETER_ERROR;
						string resultData = resultCode == ErrorCode.NONE ? w.text : null;

						if (w != null && !string.IsNullOrEmpty(w.error))
						{
							Tracer.Echo ("HTTPConnection : _Process : error : " + w.error);
						}

						this.Result ( new ResultVO(reqData.id, reqData.service, resultCode, resultData, reqData.uniqueID) );

						break;
					}
				}

			}*/

			return true;
		}

		

		private void _StartProcess(object data)
		{
			this._behaviour.StartCoroutine (this._Process (this._request));
		}

		private IEnumerator _Process(IServiceRequest request)
		{
			if (request != null && request.data is ResultVO )
			{
				this._isRequesting = true;
				this._timeOutTimer.Start ();
				ResultVO requestVO = request.data as ResultVO;

				if (requestVO.result is WWWForm)
				{
					this._www = new WWW (this.URL, requestVO.result as WWWForm);
				}
				else
				{
					this._www = new WWW (this.URL);
				}

				yield return this._www;

				string resultCode = String.IsNullOrEmpty (this._www.error) ? ErrorCode.NONE : ErrorCode.PARAMETER_ERROR;
				string resultData = resultCode == ErrorCode.NONE ? this._www.text : this._www.error;

				if (this._www != null && !string.IsNullOrEmpty (this._www.error))
				{
					Tracer.Echo ("HTTPConnection : _Process : error : " + this._www.error);
				}

				this.Result (new ResultVO (requestVO.id, requestVO.service, resultCode, resultData, requestVO.uniqueID));
				yield return true;
			}
			else
			{
				Tracer.Echo ("HTTPConnection : _Process : unknown data!");
				yield return false;
			}


		}

		public override bool IsConnected()
		{
			//always return false as http is not a concurrent connection
			return false;
		}

		public override bool Connect(string URL = "", params string[] rest)
		{
			Tracer.Echo ("HTTPConnection : Connect : calling disabled function!");
			return false;
		}

		public override bool Disconnect()
		{
//			base.Disconnect ();
			Tracer.Echo ("HTTPConnection : Disconnect : calling disabled function!");
			return false;
		}

		public override void Result(object data)
		{
			this._isRequesting = false;
//			base.Result (data);
			if (!(data is ResultVO))
			{
				Tracer.Echo ("HTTPConnection : Result : unknown data!");
				return;
			}

			ResultVO resultVO = data as ResultVO;
//			IServiceRequest request = this._requestList[resultVO.uniqueID];
			IServiceRequest request = this._request;

			if (request != null)
			{
//				this._requestList.Remove (resultVO.uniqueID);
				Tracer.Echo ("HTTPConnection : Result : calling requester!");
				request.requester.Result (new ServiceResponse (request, resultVO));
			}
			else
			{
				Tracer.Echo ("HTTPConnection : Result : serviceRequest not found!");
			}
		}

		public override void Fault(object info)
		{
			base.Fault (info);
			this.DispatchEvent (new ServiceEvent (ServiceEvent.FAULT, null, info));
		}

		private void _TimerEvnetHandler(IAppEvent e)
		{ 
			if (e.type == TimerEvent.TIMER_COMPLETE && this._isRequesting && this._www != null && this._request != null)
			{
				this._timeOutTimer.Reset ();
				this._behaviour.StopAllCoroutines ();
				this._www.Dispose ();
				ResultVO requestVO = this._request.data as ResultVO;
				this.Result (new ResultVO (requestVO.id, requestVO.service, ErrorCode.TIME_OUT, null, requestVO.uniqueID));
			}
		}

	}
}

