using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrAttachment : BussinessBase
	{
		internal EmrAttachment()
		{
		}

		internal EmrAttachment(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_ATTACHMENT> Get(EmrAttachmentFilter filter)
		{
			List<EMR_ATTACHMENT> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				list = GlobalStore.EmrConsumer.Get<List<EMR_ATTACHMENT>>("api/EmrAttachment/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				list = null;
				LogSystem.Warn(ex);
			}
			return list;
		}
	}
}
