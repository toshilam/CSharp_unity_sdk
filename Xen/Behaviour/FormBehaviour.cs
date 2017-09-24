using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Xen.Interface;

namespace Xen.Behaviour
{
	public class FormBehaviour : AppBehaviour, IForm
	{

		protected IVO _vo;
		public IVO vo{ get{ return this._vo; } }

		public FormBehaviour () : base()
		{
			
		}
		public override bool Awake ()
		{
			base.Awake ();

			return true;
		}


		public override void Start ()
		{
			base.Start ();

		}

		public bool SetData(IVO vo)
		{
			this._vo = vo;
			return true;
		}

		public IVO GetData()
		{
			return this._vo;
		}

		public bool IsValid ()
		{
			return false;
		}

		public void Clear()
		{

		}
	}


}
