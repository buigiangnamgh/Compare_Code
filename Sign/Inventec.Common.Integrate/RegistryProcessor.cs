using System;
using Inventec.Common.Logging;
using Microsoft.Win32;

namespace Inventec.Common.Integrate
{
	public class RegistryProcessor
	{
		private const string SOFTWARE_FOLDER = "SOFTWARE";

		private const string INVENTEC_FOLDER = "INVENTEC";

		internal static readonly string APP_FOLDER = "EMR.INTERGRATE";

		public static void Write(string key, object value, params string[] subFolders)
		{
			try
			{
				RegistryKey register = GetRegister(subFolders);
				if (register == null)
				{
					LogSystem.Error("Không thể mở Registry key.");
				}
				else if (value == null)
				{
					LogSystem.Warn("Không ghi giá trị null vào key: " + key);
				}
				else
				{
					register.SetValue(key, value);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public static object Read(string key, params string[] subFolders)
		{
			try
			{
				RegistryKey register = GetRegister(subFolders);
				return register.GetValue(key);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return string.Empty;
		}

		public static void DeleteValue(string key, params string[] subFolders)
		{
			try
			{
				RegistryKey register = GetRegister(subFolders);
				register.DeleteValue(key);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private static RegistryKey GetRegister(params string[] subFolders)
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("INVENTEC").CreateSubKey(APP_FOLDER);
			if (subFolders != null && subFolders.Length != 0)
			{
				foreach (string subkey in subFolders)
				{
					registryKey = registryKey.CreateSubKey(subkey);
				}
			}
			return registryKey;
		}
	}
}
