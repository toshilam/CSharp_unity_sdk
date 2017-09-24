using System;
using WebSocketSharp;
using UnityEngine;
using Xen.Interface;
using Xen.Event;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xen.Data;
using Xen.Utils;
using Newtonsoft.Json.Linq;
using Xen.Enum;

namespace Xen.Net
{
	public class WebSocketConnection : ServiceConnection
	{
//		protected static int _numRequested;
//		protected static string GetUniqueID()
//		{
//			return Tool.GetUniqueID ();
//		}

		protected double _reconnectInterval = 2000;
		public double reconnectInterval { get { return this._reconnectInterval; } }

//		protected double _pingInterval = 2000;
//		public double pingInterval { get { return this._pingInterval; } }

		protected bool _isConnecting = false;
		public bool isConnecting { get { return this._isConnecting; } }

		public bool autoReconnect { get; set; }

		//TODO implement reconnect ws server
		protected Timer _reconnectTimer;
//		protected Timer _pingTimer;

		protected WebSocket _webSocket;

		//a list of pending request to be called once connected
		protected List<IServiceRequest> _pendingRequestList;

		//a list of requests that waiting for response
		protected Dictionary<string, IServiceRequest> _requestList;

		public override object connection { get { return this._webSocket; } }

		public WebSocketConnection () : base( null )
		{
			this._requestList = new Dictionary<string, IServiceRequest> ();
			this._pendingRequestList = new List<IServiceRequest> ();

			this.autoReconnect = true;
			this._reconnectTimer = new Timer (_reconnectInterval, 1);
			this._reconnectTimer.AddEventListener (TimerEvent.TIMER_COMPLETE, this._ReconnectEventHandler);

			//ping server when connected
//			this._pingTimer = new Timer (_pingInterval, 1);
//			this._pingTimer.AddEventListener (TimerEvent.TIMER_COMPLETE, this._PingEventHandler);
		}

		protected void _ReconnectEventHandler(TimerEvent e)
		{
			this._reconnectTimer.Reset ();

			if (!this.IsConnected () && !this.isConnecting && this.autoReconnect && this._webSocket != null)
			{
				this.Connect ();
			}
			else
			{
				Tracer.Echo (String.Format("WebSocketConnection : _ReconnectEventHandler : fail : {0}{1}{2}{3}", this.IsConnected(),this.isConnecting, this.autoReconnect, this._webSocket ));

			}
		}

		/*protected void _PingEventHandler(TimerEvent e)
		{
			this._pingTimer.Reset ();

			if (this.IsConnected ())
			{
			}
		}*/

		public override bool Connect (string URL = "", params string[] rest)
		{
//			base.connect (URL, rest);

			if (this._webSocket == null)
			{
				if (String.IsNullOrEmpty (URL) ) 
				{
					return false;
				}

				this._webSocket = new WebSocket (URL, rest);

				this._webSocket.OnClose += (sender, e) => {
					Tracer.Echo("WebSocketConnect : OnClose : " + e.Reason );

					this._isConnected = false;
					this.DispatchEvent(new ServiceEvent(ServiceEvent.DISCONNECTED, null, e));

					if(this.autoReconnect)
					{
						Tracer.Echo("WebSocketConnect : OnClose : auto reconnect!" );
						this.DispatchEvent(new ServiceEvent(ServiceEvent.CONNECTING, null, e));
						this.Connect();
					}
				};

				this._webSocket.OnError += (object sender, ErrorEventArgs e) => {
					Tracer.Echo("WebSocketConnect : OnError : " + e.Message);

					//				this.Fault(e);
				};

				this._webSocket.OnMessage += (object sender, MessageEventArgs e) => {
					Tracer.Echo("WebSocketConnect : OnMessage : ");
					Tracer.Echo(e.Data);

					this.Result(e.Data);
				};

				this._webSocket.OnOpen += (object sender, EventArgs e) => {
					Tracer.Echo("WebSocketConnect : OnOpen : ");

					this._isConnecting = false;
					this._isConnected = true;
					this.DispatchEvent(new ServiceEvent(ServiceEvent.CONNECT_SUCCESS, null, e));

					Tracer.Echo(String.Format("WebSocketConnect : OnOpen : making {0} pending request", this._pendingRequestList.Count));
					while(this._pendingRequestList.Count > 0)
					{
						IServiceRequest targetRequest = this._pendingRequestList[0];
						this._pendingRequestList.Remove(targetRequest);

						this.Request(targetRequest);
					}
				};
			}

			if (this.isConnecting)
			{
				Tracer.Echo ("WebSocketConnect : Connect : connection is being made please wait!");
				return false;
			}

			this._isConnecting = true;
			//set auto reconnect
			this.autoReconnect = true;

			this._webSocket.Connect ();

			return true;
		}

		public override bool Disconnect ()
		{
			base.Disconnect ();

			//when requesting disconnection set auto reconnect to false
			this.autoReconnect = false;

			if (this._webSocket != null) 
			{
				this._webSocket.Close ();
			}
			return true;
		}

		public override void Dispose ()
		{
			base.Dispose ();

			RemoveAllEventListener ();

			if (this._webSocket != null) 
			{
				this._webSocket.Close ();
				this._webSocket = null;
			}
		}

		public override bool IsConnected ()
		{
			return this._isConnected && this._webSocket.IsAlive;
		}

		public override bool Request (IServiceRequest request)
		{
			if 
			(
				!(request is IServiceRequest) || 
				!(request.data is JObject) || 
				request.requester == null
			) 
			{
				Tracer.Echo ("WebSocketConnection : Request : unknown data!");
				return false;
			}

			JObject reqData = request.data as JObject;
			if ( reqData [AssetID.UNIQUE_ID] == null || String.IsNullOrEmpty (reqData [AssetID.UNIQUE_ID].ToString()))
			{
				reqData[AssetID.UNIQUE_ID] = Tool.GetUniqueID (this.GetType().ToString());
			}

			if ( reqData [AssetID.SERVICE] == null || String.IsNullOrEmpty (reqData [AssetID.SERVICE].ToString()))
			{
				reqData[AssetID.SERVICE] = request.type;
			}

			if (!this.IsConnected ())
			{
				this._pendingRequestList.Add (request);
				Tracer.Echo ("WebSocketConnection : Request : server is not yet connected, request will be made once is connected!");
			} 
			else 
			{
				this._requestList.Add (reqData[AssetID.UNIQUE_ID].ToString(), request);
				try
				{
					
					this._webSocket.Send (JsonConvert.SerializeObject (reqData));
				}
				catch (Exception ex)
				{
					this._requestList.Remove (reqData[AssetID.UNIQUE_ID].ToString());
					Tracer.Echo ("WebSocketConnection : Request : fail sending request : " + ex.Message);
				}

			}

			return true;
		}

		public override void Result (object data)
		{
			ResultVO resultVO;
			try 
			{
				resultVO = JsonConvert.DeserializeObject<ResultVO>(data as string);
			} 
			catch (Exception ex) 
			{
				Tracer.Echo ("WebSocketConnection : Result : unknown data!");
				Tracer.Echo (ex.Message);
				Tracer.Echo (data);
				return;
			}

			if (String.IsNullOrEmpty (resultVO.uniqueID)) 
			{
				//this can be broadcast message if no uniqueID set
				if (resultVO.service == Xen.Enum.ServiceType.BROADCAST)
				{
					Tracer.Echo ("WebSocketConnection : Result : received BROADCAST!");
					this.DispatchEvent (new ServiceEvent (ServiceEvent.BROADCAST, null, resultVO));
				}
				else
				{
					Tracer.Echo ("WebSocketConnection : Result : no uniqueID found!");
				}


				return;
			}

			IServiceRequest request = this._requestList [resultVO.uniqueID];

			if (request != null) 
			{
				this._requestList.Remove (resultVO.uniqueID);
				Tracer.Echo ("WebSocketConnection : Result : calling requester!");
				request.requester.Result (new ServiceResponse(request, resultVO));
			}
			else
			{
				Tracer.Echo ("WebSocketConnection : Result : serviceRequest not found!");
			}
		}

		public override void Fault (object info)
		{
			base.Fault (info);
			this.DispatchEvent(new ServiceEvent(ServiceEvent.FAULT, null, info));
		}


	}
}

