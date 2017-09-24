using System;
using UnityEngine;
using Xen.Interface;

namespace Xen.Data
{
	public class ResultVO : VO
	{
		public string service;
		public string code;
		public object result;
		public string uniqueID; //this unique created by ServiceConnection, and returned by server

		public ResultVO 
		(
			string id,
			string service,
			string code,
			object result,
			string uniqueID = ""
		): base(id)
		{
			this.service = service;
			this.code = code;
			this.result = result;
			this.uniqueID = uniqueID;
		}

		public override void Clear ()
		{
			this.service = this.code = this.uniqueID = "";
			this.result = null;
		}

		public override IVO Clone() 
		{
			return new ResultVO(
								this.id, 
								this.service, 
								this.code, 
								this.result,
								this.uniqueID
								);
		}

		public override string ToString ()
		{
			return String.Format ("[{0} (id={1}, service={2}, code={3}, result={4}, uniqueID={5})]", this.GetType(), this.id, this.service, this.code, this.result, this.uniqueID);
		}

	}
}

