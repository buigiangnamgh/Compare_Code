using System;
using System.Collections.Generic;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrPatientCertificate
	{
		internal EmrPatientCertificate()
		{
		}

		internal List<EMR_PATIENT_CERTIFICATE> Get(EmrPatientCertificateFilter filter)
		{
			List<EMR_PATIENT_CERTIFICATE> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_PATIENT_CERTIFICATE>>("api/EmrPatientCertificate/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}
	}
}
