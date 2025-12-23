using System;
using System.Collections.Generic;
using System.Linq;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrVersion : BussinessBase
	{
		internal EmrVersion()
		{
		}

		internal EmrVersion(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_VERSION> Get(EmrVersionFilter filter)
		{
			List<EMR_VERSION> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				list = GlobalStore.EmrConsumer.Get<List<EMR_VERSION>>("api/EmrVersion/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				list = null;
				LogSystem.Warn(ex);
			}
			return list;
		}

		internal EMR_VERSION GetSignedDocumentLast(long documentId)
		{
			EMR_VERSION eMR_VERSION = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				EmrVersionFilter emrVersionFilter = new EmrVersionFilter();
				emrVersionFilter.DOCUMENT_ID = documentId;
				List<EMR_VERSION> list = GlobalStore.EmrConsumer.Get<List<EMR_VERSION>>("api/EmrVersion/Get", commonParam, emrVersionFilter, new object[0]);
				eMR_VERSION = ((list != null && list.Count > 0) ? list.OrderByDescending((EMR_VERSION o) => o.ID).FirstOrDefault() : null);
			}
			catch (Exception ex)
			{
				eMR_VERSION = null;
				LogSystem.Warn(ex);
			}
			return eMR_VERSION;
		}
	}
}
