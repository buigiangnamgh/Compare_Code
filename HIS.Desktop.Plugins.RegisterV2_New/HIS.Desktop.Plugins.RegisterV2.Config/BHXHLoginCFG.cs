using System;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.RegisterV2.Config
{
	internal class BHXHLoginCFG
	{
		private const string CONFIG_KEY = "HIS.CHECK_HEIN_CARD.BHXH.LOGIN.USER_PASS";

		private static string username;

		private static string password;

		public static string USERNAME
		{
			get
			{
				if (string.IsNullOrEmpty(username))
				{
					username = Get(HisConfigs.Get<string>("HIS.CHECK_HEIN_CARD.BHXH.LOGIN.USER_PASS"), 0);
				}
				return username;
			}
			set
			{
				username = value;
			}
		}

		public static string PASSWORD
		{
			get
			{
				if (string.IsNullOrEmpty(password))
				{
					password = Get(HisConfigs.Get<string>("HIS.CHECK_HEIN_CARD.BHXH.LOGIN.USER_PASS"), 1);
				}
				return password;
			}
			set
			{
				password = value;
			}
		}

		private static string Get(string value, int index)
		{
			string result = "";
			try
			{
				if (!string.IsNullOrEmpty(value))
				{
					string[] array = value.Split(':');
					if (array != null && array.Length >= index)
					{
						result = array[index];
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = "";
			}
			return result;
		}
	}
}
