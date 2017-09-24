using System;
using UnityEngine;
using Xen.Data;
using System.Collections.Generic;

namespace Xen.Interface
{
	public interface IModuleMain
	{
		MonoBehaviour view{get;}

		string moduleName {get;}

		IDataManager<string, object> assetManager{get;}

		IDataManager<string, object> XMLManager{get;}

		IDataManager<string, string> settingManager{get;}

		IDataManager<string, IService> serviceManager{get;}

		IDataManager<string, IVO> VOManager{get;}

		IDataManager<string, object> soundManager{get;}

		SetupVO setupVO{get;}

		bool Setup(MonoBehaviour rootContainer, IVO vo);
		//function tearDown(inStage:DisplayObjectContainer, inVO:IVO = null):Boolean;


	}
}

