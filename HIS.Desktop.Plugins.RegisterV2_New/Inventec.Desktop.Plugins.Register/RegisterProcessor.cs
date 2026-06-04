using System;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.RegisterV2.Run2;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;

namespace Inventec.Desktop.Plugins.Register
{
	[ExtensionOf(typeof(DesktopRootExtensionPoint), "HIS.Desktop.Plugins.RegisterV2", "Đăng ký tiếp đón", "Common", 14, "tiep-don.png", "A", 1L, true, true)]
	public class RegisterProcessor : ModuleBase, IDesktopRoot
	{
		private CommonParam param;

		public RegisterProcessor()
		{
			param = new CommonParam();
		}

		public RegisterProcessor(CommonParam paramBusiness)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
		}

		public object Run(object[] args)
		{
			object obj = null;
			try
			{
				IRun run = RunFactory.MakeIRegister(param, args);
				return (run != null) ? run.Run() : null;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

		public override bool IsEnable()
		{
			bool flag = false;
			try
			{
				if (GlobalVariables.CurrentRoomTypeCodes.Contains(HisConfigs.Get<string>("DBCODE.HIS_RS.HIS_ROOM_TYPE.ROOM_TYPE_CODE.RECEPTION")))
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return false;
			}
		}
	}
}
