using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Xen.Interface;

namespace Xen.Behaviour
{
	public class AppBehaviour : EventDispatcherBehaviour
	{
		//storing list request request to be executed in main thread
		//usually it's added by asyc task
		private List<IServiceRequest> _updateActionList;

		public AppBehaviour () : base()
		{
			this._updateActionList = new List<IServiceRequest> ();
		}

		public override bool Awake ()
		{
			base.Awake ();
			//			Debug.Log (String.Format("{0} : Awake!", this.GetType()) );
			return true;
		}


		// Use this for initialization
		public override void Start ()
		{
			base.Start ();

		}

		// Update is called once per frame
		public override void Update ()
		{
			base.Update ();

			while (this._updateActionList.Count > 0)
			{
				IServiceRequest serviceRequest = this._updateActionList [0] ;
				this._updateActionList.Remove (serviceRequest);

				//this is the action(function) to be requested to call in UI(main) thread
				serviceRequest.requester.Result (serviceRequest.data);
			}
		}

		public override void OnDestroy ()
		{
			base.OnDestroy ();
			this._updateActionList.RemoveRange (0, this._updateActionList.Count);
		}

		//to be called from background thread that request changing things in UI(main) thread
		public virtual bool  AddUIThreadAction(IServiceRequest request)
		{
			if (request != null)
			{
				lock (this._updateActionList)
				{
					this._updateActionList.Add (request);

					return true;
				}
			}

			return false;
		}
	}


}
