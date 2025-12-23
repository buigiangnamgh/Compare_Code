using System;
using System.Collections.Generic;
using System.Linq;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrDocumentGroup : BussinessBase
	{
		internal EmrDocumentGroup()
		{
		}

		internal EmrDocumentGroup(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_DOCUMENT_GROUP> Get(EmrDocumentGroupFilter filter)
		{
			List<EMR_DOCUMENT_GROUP> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_DOCUMENT_GROUP>>("api/EmrDocumentGroup/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_DOCUMENT_GROUP> Get()
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			List<EMR_DOCUMENT_GROUP> list = null;
			try
			{
				EmrDocumentGroupFilter filter = new EmrDocumentGroupFilter();
				return Get(filter);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal EMR_DOCUMENT_GROUP GetByCode(string code)
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			EMR_DOCUMENT_GROUP val = null;
			try
			{
				EmrDocumentGroupFilter filter = new EmrDocumentGroupFilter
				{
					DOCUMENT_GROUP_CODE__EXACT = code
				};
				return Get(filter).FirstOrDefault();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
