using System;
using Xen.Interface;
using Xen.Utils;
using PureMVC.Patterns;
using Xen.Behaviour;
using UnityEngine;
using Xen.MVC.Enum;
using System.Collections.Generic;
using System.Collections;
using PureMVC.Interfaces;

namespace Xen.MVC.Pattern
{
	public class ApplicationMediator : PureMVC.Patterns.Mediator, IApplicationMediator, IMediator
	{
		//		public const string NAME = "ApplicationMediator";

		protected IModuleMain _host;
		public IModuleMain host { get{ return _host; } }

		public GameObject root //canvas 
		{
			get
			{
				return this.ViewComponent is MonoBehaviour ? (this.ViewComponent as MonoBehaviour).transform.root.gameObject : null;
			} 
		}

		//		public string GetPlatform()
		//		{
		//			return platform;
		//		}


		//		protected IList<string> _interestedList;

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


		public ApplicationMediator(string name, IModuleMain rootContainer = null) : base (name, rootContainer)
		{
			if (!(rootContainer is IModuleMain))
			{
				Tracer.Echo (String.Format ("{0} : invaild data object set : only IModuleMain is excepted ", this.MediatorName), this, 0xff0000);
			}
			else
			{
				_host = rootContainer;
			}
		}

		public override void OnRegister()
		{
			base.OnRegister();
			Echo("onRegister.");

			//			this._interestedList = new List<string> ();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			Echo("onRemove.");
		}

		public override IList<string> ListNotificationInterests ()
		{
			//			return this._interestedList;
			IList<string> list = new List<string>();
			//		list.Add(CountProxy.UPDATED);
			return list;
		}

		public virtual void StartListener() 
		{

		}

		public virtual void StopListener() 
		{

		}

		protected bool IsModuleMain()
		{
			return this._host is IModuleMain;
		}

		protected void Echo(object message, object target = null, int color = 0xff0000)
		{
			Tracer.Echo
			(
				this.MediatorName + " : " + message, 
				target ?? this,
				color
			);
		}

	}
}

