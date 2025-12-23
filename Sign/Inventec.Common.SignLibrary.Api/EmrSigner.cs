using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrSigner
	{
		internal EmrSigner()
		{
		}

		internal List<EMR_SIGNER> Get(EmrSignerFilter filter)
		{
			List<EMR_SIGNER> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_SIGNER>>("api/EmrSigner/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_SIGNER> Get(ref CommonParam paramCommon, EmrSignerFilter filter)
		{
			List<EMR_SIGNER> list = null;
			try
			{
				return GlobalStore.EmrConsumer.Get<List<EMR_SIGNER>>("api/EmrSigner/Get", paramCommon, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_SIGNER> Get()
		{
			List<EMR_SIGNER> list = null;
			try
			{
				EmrSignerFilter filter = new EmrSignerFilter();
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
