using System;
using System.Threading.Tasks;
using EMR.SDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.DTO;

namespace Inventec.Common.SignLibrary.Api
{
	internal class IGDocumentState : BussinessBase
	{
		internal IGDocumentState()
		{
		}

		internal IGDocumentState(CommonParam param)
			: base(param)
		{
		}

		internal async Task<bool> SendSignedInfoToIGSys(DocumentSignedUpdateIGSysResultDTO dataSigned)
		{
			new CommonParam();
			bool success = false;
			try
			{
				EmrSignResultSDO rs = await GlobalStore.IntegrateConsumer.PostWithouApiParamAsync<EmrSignResultSDO>(GlobalStore.INTERGRATE_SYS_API, dataSigned, 0, new object[0]);
				if (rs != null)
				{
					success = true;
				}
				LogSystem.Debug("SendSignedInfoToIGSys:" + LogUtil.TraceData(LogUtil.GetMemberName(() => dataSigned), dataSigned) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => GlobalStore.INTERGRATE_SYS_BASE_URI), GlobalStore.INTERGRATE_SYS_BASE_URI) + "____" + LogUtil.TraceData("GetBaseUri()", GlobalStore.IntegrateConsumer.GetBaseUri()) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => GlobalStore.INTERGRATE_SYS_API), GlobalStore.INTERGRATE_SYS_API) + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => success), success));
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
				success = false;
			}
			return success;
		}
	}
}
