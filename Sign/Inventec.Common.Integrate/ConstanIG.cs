using System;
using System.Configuration;

namespace Inventec.Common.Integrate
{
	internal class ConstanIG
	{
		internal static string ACS_BASE_URI = "";

		internal static readonly string LOGIN_URI = "api/Token/Login";

		internal static string GET_AUTHENTICATED_URI = "api/Token/GetAuthenticated";

		internal static readonly string RENEW_URI = "api/Token/Renew";

		internal static readonly string CHANGE_PASS_URI = "api/Token/ChangePassword";

		internal static readonly string LOGOUT_URI = "api/Token/Logout";

		private static int timeOut = 0;

		internal static string REGISTRY_TOKEN_CODE = "TKC";

		internal static string REGISTRY_RENEW_CODE = "RNC";

		internal static string REGISTRY_EXPIRE_TIME = "EXP";

		internal static string REGISTRY_SUBFOLDER = "TokenForIntegrate";

		internal static int TIME_OUT
		{
			get
			{
				if (timeOut <= 0)
				{
					try
					{
						timeOut = int.Parse(ConfigurationManager.AppSettings["Inventec.Token.ClientSystem.Timeout"] ?? "300");
					}
					catch (Exception)
					{
					}
				}
				return timeOut;
			}
		}
	}
}
