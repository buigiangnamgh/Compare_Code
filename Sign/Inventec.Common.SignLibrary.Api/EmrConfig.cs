using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrConfig : BussinessBase
	{
		internal EmrConfig()
		{
		}

		internal EmrConfig(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_CONFIG> Get()
		{
			return Get(new EmrConfigFilter());
		}

		internal List<EMR_CONFIG> Get(EmrConfigFilter filter)
		{
			List<EMR_CONFIG> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_CONFIG>>("api/EmrConfig/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_CONFIG> GetByKey(string key)
		{
			List<EMR_CONFIG> list = null;
			try
			{
				EmrConfigFilter emrConfigFilter = new EmrConfigFilter();
				emrConfigFilter.KEY__EXACT = key;
				EmrConfigFilter filter = emrConfigFilter;
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_CONFIG>>("api/EmrConfig/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
