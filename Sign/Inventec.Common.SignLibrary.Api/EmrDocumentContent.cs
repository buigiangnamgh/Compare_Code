using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrDocumentContent : BussinessBase
	{
		internal EmrDocumentContent()
		{
		}

		internal EmrDocumentContent(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_DOCUMENT_CONTENT> Get(EmrDocumentContentFilter filter)
		{
			List<EMR_DOCUMENT_CONTENT> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_DOCUMENT_CONTENT>>("/api/EmrDocumentContent/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_DOCUMENT_CONTENT> Get()
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			List<EMR_DOCUMENT_CONTENT> list = null;
			try
			{
				EmrDocumentContentFilter filter = new EmrDocumentContentFilter();
				return Get(filter);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
