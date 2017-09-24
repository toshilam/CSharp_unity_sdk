using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xen.Event;
using Xen.Interface;
using Xen.Utils;
using System;

namespace Xen.Resource
{
	public class DataManager<TKey, TValue> : EventDispatcher<IAppEvent>, IDataManager<TKey, TValue>
	{
		protected string _lastAddedAssetID;
		public virtual string lastAddedAssetID{ get{ return this._lastAddedAssetID;} }

		protected Dictionary<TKey, TValue> _objAsset;

		public DataManager() : base()
		{
			this._objAsset = new Dictionary<TKey, TValue> ();
		}

		public virtual bool AddAsset(TValue asset, TKey assetID)
		{
			if (this._objAsset.ContainsKey (assetID))
			{
				Tracer.Echo (String.Format("{0} : AddAsset : assetID already existed!", GetType()));
				return false;
			}

			this._objAsset.Add (assetID, asset);
			return true;
		}

		public virtual TValue GetAsset(TKey assetID)
		{
			if (this.HasAsset(assetID))
			{
				return this._objAsset [assetID];
			}

			return default(TValue);
		}

		public virtual bool HasAsset(TKey assetID)
		{
			return this._objAsset.ContainsKey (assetID);
		}

		public virtual bool RemoveAsset(TKey assetID)
		{
			if (this.HasAsset (assetID))
			{
				this._objAsset.Remove (assetID);
				return true;
			}

			return false;
		}

		//to get a data from an asset object
		/*public virtual TDataValue GetData(string id, string assetID)
	{
		return null;
	}

	public virtual bool HasData(string dataID,  string assetID)
	{
		//to be implemented in child class
		return false;
	}*/
	}

}
