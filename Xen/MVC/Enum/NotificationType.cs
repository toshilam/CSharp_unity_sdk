using System;

namespace Xen.MVC.Enum
{
	public class NotificationType
	{
		public const string STARTUP 							= "startup";
		public const string TEARDOWN							= "teardown";

		public const string SEND_MESSAGE_TO_SHELL				= "sendMessageToShell";
		public const string SEND_MESSAGE_TO_MODULE				= "sendMessageToModule";

		public const string CONNECT_MODULE_TO_SHELL				= "connectModuleToShell";
		public const string CONNECT_SHELL_TO_MODULE				= "connectShellToModule";

		public const string CONNECT_MEDIATOR 					= "connectMediator";
		public const string DISCONNECT_MEDIATOR 				= "disconnectMediator";

		public const string ADD_HOST							= "addHost";
		public const string ADD_CHILD							= "addChild";

		public const string MODULE_ON							= "moduleOn";
		public const string MODULE_OFF							= "moduleOff";

		public NotificationType ()
		{
		}
	}
}

