using System;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
	internal class IcdUtil
	{
		internal const string seperator = ";";

		internal static string AddSeperateToKey(string key)
		{
			try
			{
				return string.Format("{0}{1}{2}", ";", key, ";");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return "";
		}
	}
}
