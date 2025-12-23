using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrSignTemplate
	{
		internal EmrSignTemplate()
		{
		}

		internal List<EMR_SIGN_TEMP> Get(EmrSignTempFilter filter)
		{
			List<EMR_SIGN_TEMP> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_SIGN_TEMP>>("api/EmrSignTemp/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_SIGN_TEMP> Get()
		{
			List<EMR_SIGN_TEMP> list = null;
			try
			{
				EmrSignTempFilter emrSignTempFilter = new EmrSignTempFilter();
				emrSignTempFilter.IS_ACTIVE = 1;
				emrSignTempFilter.ORDER_DIRECTION = "ASC";
				emrSignTempFilter.ORDER_FIELD = "SIGN_TEMP_CODE";
				return Get(emrSignTempFilter);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
