using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrBusiness : BussinessBase
	{
		internal EmrBusiness()
		{
		}

		internal EmrBusiness(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_BUSINESS> Get()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			return Get(new EmrBusinessFilter());
		}

		internal List<EMR_BUSINESS> Get(EmrBusinessFilter filter)
		{
			List<EMR_BUSINESS> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_BUSINESS>>("api/EmrBusiness/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_BUSINESS> GetByKey(string key)
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			List<EMR_BUSINESS> list = null;
			try
			{
				EmrBusinessFilter filter = new EmrBusinessFilter
				{
					BUSINESS_CODE__EXACT = key
				};
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_BUSINESS>>("api/EmrBusiness/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
