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
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			List<EMR_SIGN_TEMP> list = null;
			try
			{
				EmrSignTempFilter val = new EmrSignTempFilter();
				((FilterBase)val).IS_ACTIVE = (short)1;
				((FilterBase)val).ORDER_DIRECTION = "ASC";
				((FilterBase)val).ORDER_FIELD = "SIGN_TEMP_CODE";
				return Get(val);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
