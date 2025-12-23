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
			List<EMR_BUSINESS> list = null;
			try
			{
				EmrBusinessFilter emrBusinessFilter = new EmrBusinessFilter();
				emrBusinessFilter.BUSINESS_CODE__EXACT = key;
				EmrBusinessFilter filter = emrBusinessFilter;
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
