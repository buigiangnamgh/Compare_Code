using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrFlow
	{
		internal EmrFlow()
		{
		}

		internal List<EMR_FLOW> Get(EmrFlowFilter filter)
		{
			List<EMR_FLOW> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_FLOW>>("api/EmrFlow/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<V_EMR_FLOW> GetView(EmrFlowViewFilter filter)
		{
			List<V_EMR_FLOW> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<V_EMR_FLOW>>("api/EmrFlow/GetView", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
