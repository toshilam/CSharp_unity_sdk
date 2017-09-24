using System;
using Xen.Interface;
using Xen.Utils;
using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace Xen.MVC.Pattern
{
	public class ApplicationProxy : PureMVC.Patterns.Proxy, IApplicationProxy, IProxy
	{

//		public static string NAME = "ApplicationProxy";

		protected IModuleMain _host;
		public IModuleMain host { get{ return this._host; } }

		//		public string GetPlatform()
		//		{
		//			return platform;
		//		}


		protected IDataManager<string, object> assetManager
		{
			get{ return this.IsModuleMain () ? this._host.assetManager : null; }
		}

		protected IDataManager<string, object> XMLManager
		{
			get{ return this.IsModuleMain () ? this._host.XMLManager : null; }
		}

		protected IDataManager<string, string> settingManager
		{
			get{ return this.IsModuleMain () ? this._host.settingManager : null; }
		}

		protected IDataManager<string, IVO> VOManager
		{
			get{ return this.IsModuleMain () ? this._host.VOManager : null; }
		}

		protected IDataManager<string, IService> serviceManager
		{
			get{ return this.IsModuleMain () ? this._host.serviceManager : null; }
		}


		public ApplicationProxy(string name, IModuleMain data = null) : base (name, data)
		{
			if (!(data is IModuleMain)) 
			{
				Tracer.Echo(String.Format("{0} : invaild data object set : expecting IModuleMain ", this.ProxyName), this, 0xff0000);
			}

			this._host = data;
		}

		public override void OnRegister()
		{
			base.OnRegister();
			Echo("onRegister.");
		}

		public override void OnRemove()
		{
			base.OnRemove();
			Echo("onRemove.");
		}

		public virtual void SetData(object value)
		{
			base.m_data = value;
//			if (value is IModuleMain) this._host = value as IModuleMain;
//			else this.m_data = value;
		}

		/**
		 * send a notification to a targeted mediator
		 * @param	inTargetMediatorName - the target mediator's name
		 * @param	notificationName 
		 * @param	body
		 * @param	type
		 * @return  true if successfully send
		 */
		public bool SendNotificationToMediator(string targetMediatorName, string notificationName, object body = null, string type = null)
		{
			if (Facade.HasMediator(targetMediatorName))
			{
				Facade.RetrieveMediator(targetMediatorName).HandleNotification(new Notification(notificationName, body, type));
				return true;
			}

			Echo(String.Format("sendNotificationToMediator : target mediator is not found : {0}", targetMediatorName));
			return false;
		}


		public virtual void InitAsset(object asset = null)
		{

		}

		public virtual bool Request(IServiceRequest request)
		{
			return request != null;
		}

		protected bool IsModuleMain()
		{
			return this._host is IModuleMain;
		}

		protected void Echo(object message, object target = null, int color = 0xff0000)
		{
			Tracer.Echo
			(
				this.ProxyName + " : " + message, 
				target ?? this,
				color
			);
		}

		/**
		 * NOTE : xml formet is supposed to be : (data / string)
		 * @param	inID - the targeted node attribute id
		 * @param	inNodeName - target node name
		 * @param	inAssetLibID - aseet lib id 
		 * @return message if found else null
		 */
//		public function getMessage(inID:String, inNodeName:String = 'string', inAssetLibID:String = 'langCommon'):String
//		{
//			if (!xmlManager) return null;
//
//			return (xmlManager as XMLManager).getString(inID, inNodeName, inAssetLibID);
//
//			/*var xmlList:XMLList = xmlManager.getData(inNodeName, inAssetLibID);
//			if (xmlList && xmlList.length())
//			{
//				var xml:XML = xmlList.(@id == inID)[0];
//				if (xml)
//				{
//					return xml.toString();
//				}
//				
//				echo('getMessage : target xml node not found!', this, 0xff0000);
//				
//			}
//			else
//			{
//				echo('getMessage : no data found!', this, 0xff0000);
//			}
//			
//			return '';*/
//		}
	}
}

