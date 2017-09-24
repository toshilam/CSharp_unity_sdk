using UnityEngine;
using System.Collections;
using Xen.Interface;
using Xen.Data;
using Xen.Resource;
using System;

namespace Xen.Behaviour
{
	public class ModuleMainBehaviour : AppBehaviour, IModuleMain
	{
		protected static IModuleMain _instance;
		public static IModuleMain instance{ get{ return ModuleMainBehaviour._instance; } }

		protected MonoBehaviour _view;
		public MonoBehaviour view{get{ return this._view; }}

		protected string _moduleName = "ModuleMain";
		public string moduleName {get{ return this._moduleName; }}

		protected SetupVO _setupVO;
		public SetupVO setupVO{get{ return this._setupVO; }}

		public IDataManager<string, object> assetManager{get{ return setupVO != null ? setupVO.assetManager : null; }}

		public IDataManager<string, object> XMLManager{get{ return setupVO != null ? setupVO.XMLManager : null; }}

		public IDataManager<string, string> settingManager{get{ return setupVO != null ? setupVO.settingManager : null; }}

		public IDataManager<string, IService> serviceManager{get{ return setupVO != null ? setupVO.serviceManager : null; }}

		public IDataManager<string, object> soundManager{get{ return setupVO != null ? setupVO.soundManager : null; }}

		public IDataManager<string, IVO> VOManager{get{ return setupVO != null ? setupVO.VOManager : null; }}

		public ModuleMainBehaviour() :base()
		{


		}

		public override bool Awake ()
		{
			if (_instance == null)
			{
				base.Awake ();

				_instance = this;
				DontDestroyOnLoad (this);

				return true;
			}
			else
			{
				Debug.Log (String.Format ("{0} : Awake : being Destroyed", GetType ()));
				Destroy (this.gameObject);
			}

			Debug.Log (String.Format ("{0} : Awake : object has already been initialized!", GetType ()));
			return false;
		}

		// Use this for initialization
		public override void Start ()
		{
			base.Start ();

		}

		public virtual bool Setup(MonoBehaviour rootContainer, IVO vo)
		{
			if (this._view == null && this._setupVO == null && rootContainer != null && vo is SetupVO)
			{
				this._view = rootContainer;
				this._setupVO = vo as SetupVO;
				return true;
			}

			return false;
		}



		// Update is called once per frame
		//		public override void Update ()
		//		{
		//			base.Update ();
		//		}
	}


}
