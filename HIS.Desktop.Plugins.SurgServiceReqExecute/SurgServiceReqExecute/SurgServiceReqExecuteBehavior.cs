using System;
using HIS.Desktop.Plugins.SurgServiceReqExecute;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.SurgServiceReqExecute.SurgServiceReqExecute
{
	public sealed class SurgServiceReqExecuteBehavior : Tool<IDesktopToolContext>, ISurgServiceReqExecute
	{
		private long treatmentId;

		private long intructionTime;

		private long serviceReqId;

		private V_HIS_SERVICE_REQ serviceReq;

		private Inventec.Desktop.Common.Modules.Module moduleData;

		public SurgServiceReqExecuteBehavior()
		{
		}

		public SurgServiceReqExecuteBehavior(CommonParam param, V_HIS_SERVICE_REQ data, Inventec.Desktop.Common.Modules.Module moduleData)
		{
			serviceReq = data;
			this.moduleData = moduleData;
		}

		object ISurgServiceReqExecute.Run()
		{
			try
			{
				return new SurgServiceReqExecuteControl(moduleData, serviceReq);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
	}
}
