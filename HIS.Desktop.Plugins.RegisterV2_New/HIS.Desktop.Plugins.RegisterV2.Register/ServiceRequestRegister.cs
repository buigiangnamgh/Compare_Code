using System;
using HIS.Desktop.Common;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV2.Register
{
	public class ServiceRequestRegister : BusinessBase, IAppDelegacyT
	{
		private object entity;

		private object patient;

		internal ServiceRequestRegister(CommonParam param, object data, object patient)
			: base(param)
		{
			entity = data;
			this.patient = patient;
		}

		T IAppDelegacyT.Execute<T>()
		{
			T result = default(T);
			try
			{
				if (typeof(T) == typeof(HisServiceReqExamRegisterResultSDO))
				{
					IServiceRequestRegisterExam serviceRequestRegisterExam = ServiceRequestRegisterExamBehaviorFactory.MakeIServiceRequestRegister(base.param, entity, patient);
					return (serviceRequestRegisterExam != null) ? ((T)Convert.ChangeType(serviceRequestRegisterExam.Run(), typeof(T))) : default(T);
				}
				if (typeof(T) == typeof(HisPatientProfileSDO))
				{
					IServiceRequestRegisterPatientProfile serviceRequestRegisterPatientProfile = ServiceRequestRegisterPatientProfileBehaviorFactory.MakeIServiceRequestRegister(base.param, entity, patient);
					return (serviceRequestRegisterPatientProfile != null) ? ((T)Convert.ChangeType(serviceRequestRegisterPatientProfile.Run(), typeof(T))) : default(T);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = default(T);
			}
			return result;
		}
	}
}
