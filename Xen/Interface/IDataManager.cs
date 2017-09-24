using System;
using Xen.Event;
using UnityEngine;

namespace Xen.Interface
{
	public interface IDataManager<TKey, TValue> : IEventDispatcher<IAppEvent>
	{
		/**
		 * the last asset id which just added
		 */
		string lastAddedAssetID{ get; }

		/**
		 * the asset object which container a pack of data
		 * @param	inAsset - asset object
		 * @param	inAssetID - asset id (key) for retrieving data
		 * @return	true if successfully added
		 */
		bool AddAsset(TValue Asset, TKey AssetID);

		/**
		 * to get the added asset object
		 * @param	inAssetID - an identifier for retrieving asset object
		 * @return	asset object if successfully found
		 */
		TValue GetAsset(TKey AssetID);

		/**
		 * check whather asset object exist in list
		 * @param	inAssetID - an identifier for checking asset object
		 * @return	true if asset found
		 */
		bool HasAsset(TKey AssetID);

		/**
		 * to remove existing asset from list
		 * @param	inAssetID - an identifier for removing asset object
		 * @return	true if successfully removed
		 */
		bool RemoveAsset(TKey AssetID);

		/**
		 * to get data from a added asset object
		 * @param	inID - the data id
		 * @param	inAssetID - asset id
		 * @return
		 */
		//object GetData(string id, string AssetID);

		/**
		 * check whather an asset object containing the targeted data object
		 * @param	inDataID - data id
		 * @param	inAssetID - asset id
		 * @return	true if data object found
		 */
		//bool HasData(string DataID,  string AssetID);
	}
}

