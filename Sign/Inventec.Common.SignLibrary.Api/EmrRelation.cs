using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrRelation : BussinessBase
	{
		internal EmrRelation()
		{
		}

		internal EmrRelation(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_RELATION> Get()
		{
			return Get(new EmrRelationFilter());
		}

		internal List<EMR_RELATION> Get(EmrRelationFilter filter)
		{
			List<EMR_RELATION> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_RELATION>>("api/EmrRelation/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
