using System;
using PureMVC.Interfaces;
using UnityEngine;

namespace Xen.Interface
{
	public interface IApplicationMediator : IMediator
	{
		GameObject root{ get; }

		void StartListener();
		void StopListener();
	}
}

