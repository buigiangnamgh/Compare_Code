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
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			List<EMR_SIGN_ORDER> list = null;
			try
			{
				EmrSignOrderFilter val = new EmrSignOrderFilter();
				((FilterBase)val).IS_ACTIVE = (short)1;
				val.SIGN_TEMP_ID = signTemId;
				((FilterBase)val).ORDER_DIRECTION = "ASC";
				((FilterBase)val).ORDER_FIELD = "NUM_ORDER";
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
