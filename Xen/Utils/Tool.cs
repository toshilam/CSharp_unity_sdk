using System;

namespace Xen.Utils
{
	public class Tool
	{
		public Tool ()
		{
			throw new Exception ("Tool : cannot be initialized!");
		}

		private static double _currentCount = 0;
		public static string GetUniqueID(string prefix = "", string suffix = "")
		{
			return string.Format ("{0}{1}_{2}{3}", prefix, Tool.GetTimeStamp (), ++Tool._currentCount, suffix);
		}

		public static int GetTimeStamp()
		{
			return Convert.ToInt32(DateTime.UtcNow.AddHours(8).Subtract(new DateTime(1970, 1, 1)).TotalSeconds); 
		}
	}
}

