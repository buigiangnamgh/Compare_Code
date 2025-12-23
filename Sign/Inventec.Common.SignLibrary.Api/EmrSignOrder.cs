using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrSignOrder
	{
		internal EmrSignOrder()
		{
		}

		internal List<EMR_SIGN_ORDER> Get(EmrSignOrderFilter filter)
		{
			List<EMR_SIGN_ORDER> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_SIGN_ORDER>>("api/EmrSignOrder/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_SIGN_ORDER> GetByTemp(long signTemId)
		{
			List<EMR_SIGN_ORDER> list = null;
			try
			{
				EmrSignOrderFilter emrSignOrderFilter = new EmrSignOrderFilter();
				emrSignOrderFilter.IS_ACTIVE = 1;
				emrSignOrderFilter.SIGN_TEMP_ID = signTemId;
				emrSignOrderFilter.ORDER_DIRECTION = "ASC";
				emrSignOrderFilter.ORDER_FIELD = "NUM_ORDER";
				return Get(emrSignOrderFilter);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
