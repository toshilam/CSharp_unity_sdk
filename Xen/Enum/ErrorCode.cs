using System;

namespace Xen.Enum
{
	public class ErrorCode
	{
		public const string NONE = "0";
		public const string PARAMETER_EMPTY = "1";
		public const string PARAMETER_MISSING = "2";
		public const string PARAMETER_ERROR = "3";
		public const string TOKEN_EXPIRED = "4";
		public const string TOKEN_NOT_FOUND = "5";
		public const string NOT_IN_USE = "6";

		public const string DB_CONNECTION_ERROR = "20";
		public const string DB_INSERT_ERROR = "21";
		public const string DB_UPDATE_ERROR = "22";
		public const string DB_RECORD_NOT_FOUND = "23";
		public const string DB_RECORD_EXISTS = "24";

		public const string JSON_EXCEPTION = "30";
		public const string JSON_GENERAL_ERROR = "31";
		public const string JSON_METHOD_NAME_ERROR = "32";
		public const string JSON_PARAMETER_EMPTY = "33";
		public const string JSON_PARAMETER_MISSING = "34";
		public const string JSON_REQUEST_ID_DUPLICATED = "35";
		public const string JSON_REQUEST_ID_ERROR = "36";
		public const string JSON_KEY_NOT_FOUND = "37";

		public const string TIME_OUT = "110";

		public ErrorCode ()
		{
		}
	}
}

