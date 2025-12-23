using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrSignerFlow
	{
		internal EmrSignerFlow()
		{
		}

		internal List<EMR_SIGNER_FLOW> Get(EmrSignerFlowFilter filter)
		{
			List<EMR_SIGNER_FLOW> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_SIGNER_FLOW>>("api/EmrSignerFlow/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<V_EMR_SIGNER_FLOW> GetView(EmrSignerFlowViewFilter filter)
		{
			List<V_EMR_SIGNER_FLOW> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<V_EMR_SIGNER_FLOW>>("api/EmrSignerFlow/GetView", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
