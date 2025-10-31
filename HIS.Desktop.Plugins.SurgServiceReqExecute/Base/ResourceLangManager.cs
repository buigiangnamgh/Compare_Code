using System;
using System.Resources;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
	internal class ResourceLangManager
	{
		internal static ResourceManager LanguageUCSurgServiceReqExecute { get; set; }

		internal static void InitResourceLanguageManager()
		{
			try
			{
				LanguageUCSurgServiceReqExecute = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(SurgServiceReqExecuteControl).Assembly);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}
	}
}
