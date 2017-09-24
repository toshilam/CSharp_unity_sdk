using System;
using UnityEngine;
using Xen.Interface;
using System.Collections.Generic;

namespace Xen.Data
{
	public class SetupVO : VO
	{
		protected IDataManager<string, object> _assetManager;
		public IDataManager<string, object> assetManager{get{ return this._assetManager; }}

		protected IDataManager<string, object> _XMLManager;
		public IDataManager<string, object> XMLManager{get{ return this._XMLManager; }}

		protected IDataManager<string, string> _settingManager;
		public IDataManager<string, string> settingManager{get{ return this._settingManager; }}

		protected IDataManager<string, IService> _serviceManager;
		public IDataManager<string, IService> serviceManager{get{ return this._serviceManager; }}

		protected IDataManager<string, object> _soundManager;
		public IDataManager<string, object> soundManager{get{ return this._soundManager; }}

		protected IDataManager<string, IVO> _VOManager;
		public IDataManager<string, IVO> VOManager{get{ return this._VOManager; }}

		protected string _language;
		public string language{get{ return this._language; }}
//		string _prefixURL:String;
//		public var parameters:Object;

		protected Dictionary<string, Type> _commandList;
		public Dictionary<string, Type> commandList{ get{ return this._commandList;}}


		public SetupVO 
		(
			string id,
			IDataManager<string, object> assetManager, 
			IDataManager<string, object> XMLManager, 
			IDataManager<string, string> settingManager,
			IDataManager<string, IService> serviceManager,
			IDataManager<string, IVO> VOManager,
			IDataManager<string, object> soundManager = null,
			string language = "cht",
			Dictionary<string, Type> commandList = null
		): base(id)
		{
			this._assetManager = assetManager;
			this._XMLManager = XMLManager;
			this._settingManager = settingManager;
			this._serviceManager = serviceManager;
			this._soundManager = soundManager;
			this._VOManager = VOManager;
			this._language = language;

			this._commandList = commandList; 
//			_prefixURL = inPrefixURL;
		}


		public override IVO Clone() 
		{
			return new SetupVO(
								this.id, 
								this.assetManager, 
								this.XMLManager, 
								this.settingManager, 
								this.serviceManager,
								this.VOManager,
								this.soundManager, 
								this.language,
								this.commandList
								);
		}

	}
}

