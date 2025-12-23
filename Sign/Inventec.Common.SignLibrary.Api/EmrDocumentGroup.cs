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
			EMR_DOCUMENT_GROUP eMR_DOCUMENT_GROUP = null;
			try
			{
				EmrDocumentGroupFilter emrDocumentGroupFilter = new EmrDocumentGroupFilter();
				emrDocumentGroupFilter.DOCUMENT_GROUP_CODE__EXACT = code;
				EmrDocumentGroupFilter filter = emrDocumentGroupFilter;
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
