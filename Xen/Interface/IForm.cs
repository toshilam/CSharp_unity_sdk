using System;

namespace Xen.Interface
{
	public interface IForm
	{
		IVO vo{ get; }

		bool SetData(IVO vo);

		IVO GetData();

		bool IsValid ();

		void Clear();
	}
}

