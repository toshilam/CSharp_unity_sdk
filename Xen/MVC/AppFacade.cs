using System;
using PureMVC.Patterns;
using Xen.Interface;
using Xen.MVC.Enum;
using UnityEngine;
using Xen.Data;
using System.Collections.Generic;
using Xen.Event;

namespace Xen.MVC
{
	public class AppFacade : Facade
	{
		
		static AppFacade ()
		{
			m_instance = new AppFacade();

		}

		protected IModuleMain _moduleMain;

		protected IEventDispatcher<AppEvent> _eventDispatcher;
		public IEventDispatcher<AppEvent> eventDispatcher{get{ return this._eventDispatcher;}}

		protected override void InitializeController() 
		{
			base.InitializeController();

//			RegisterCommand( NotificationType.STARTUP, typeof( TSTARTUP )  );
		}

		public virtual void Startup(IModuleMain moduleMain)
		{
			if (moduleMain == null )
			{
				throw new ArgumentException ("ModuleFacade : Startup : unknown data!");
			}

			Debug.Log (String.Format("{0} : Startup : ", GetType()));

			this._eventDispatcher = new EventDispatcher<AppEvent> ();
			this._moduleMain = moduleMain;

			this.eventDispatcher.AddEventListener (AppEvent.CONNECT_MEDIATOR, this._ConnectMediatorHandler);
			this.eventDispatcher.AddEventListener (AppEvent.DISCONNECT_MEDIATOR, this._DisconnectMediatorHandler);

			Dictionary<string, Type> commandList = this._moduleMain.setupVO.commandList;
			foreach (KeyValuePair<string, Type> command in commandList)
			{
				if (!String.IsNullOrEmpty (command.Key) && command.Value != null)
				{
					RegisterCommand( command.Key, command.Value  );
				}

			}

			if (this.HasCommand (NotificationType.STARTUP))
			{
				SendNotification (NotificationType.STARTUP, moduleMain);
			}
			else
			{
				throw new Exception (String.Format ("{0} : Startup : command STARTUP is not registered, failed starting application", GetType ()));
			}

		}

		private void _ConnectMediatorHandler( AppEvent e )
		{
			if (this.HasCommand (NotificationType.CONNECT_MEDIATOR))
			{
				SendNotification (NotificationType.CONNECT_MEDIATOR, new DisplayObjectVO (this._moduleMain.moduleName, e.target as MonoBehaviour, this._moduleMain));
			}
			else
			{
				Debug.Log (String.Format ("{0} : ConnectMediator : command CONNECT_MEDIATOR is not registered!", GetType ()));
			}

		}

		private void _DisconnectMediatorHandler( AppEvent e )
		{
			if (this.HasCommand (NotificationType.DISCONNECT_MEDIATOR))
			{
				SendNotification (NotificationType.DISCONNECT_MEDIATOR, new DisplayObjectVO (this._moduleMain.moduleName, e.target as MonoBehaviour, this._moduleMain));
			}
			else
			{
				Debug.Log (String.Format ("{0} : ConnectMediator : command DISCONNECT_MEDIATOR is not registered!", GetType ()));
			}
		}
	}
}

