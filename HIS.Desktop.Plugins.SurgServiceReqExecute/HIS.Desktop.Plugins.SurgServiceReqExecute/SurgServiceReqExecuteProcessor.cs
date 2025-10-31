using System;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Plugins.SurgServiceReqExecute.SurgServiceReqExecute;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute
{
	[ExtensionOf(typeof(DesktopRootExtensionPoint), "HIS.Desktop.Plugins.SurgServiceReqExecute", "Xử lý dịch vụ", "Common", 14, "pivot_32x32.png", "A", 1L, true, true)]
	public class SurgServiceReqExecuteProcessor : ModuleBase, IDesktopRoot
	{
		private CommonParam param;

		public SurgServiceReqExecuteProcessor()
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			param = new CommonParam();
		}

		public SurgServiceReqExecuteProcessor(CommonParam paramBusiness)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			param = (CommonParam)((paramBusiness != null) ? ((object)paramBusiness) : ((object)new CommonParam()));
		}

		public object Run(object[] args)
		{
			object obj = null;
			try
			{
				ISurgServiceReqExecute surgServiceReqExecute = SurgServiceReqExecuteFactory.MakeISurgServiceReqExecute(param, args);
				return (surgServiceReqExecute != null) ? surgServiceReqExecute.Run() : null;
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
				if (GlobalVariables.CurrentRoomTypeCode == SdaConfigs.Get<string>("DBCODE.HIS_RS.HIS_ROOM_TYPE.ROOM_TYPE_CODE.STOCK"))
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
