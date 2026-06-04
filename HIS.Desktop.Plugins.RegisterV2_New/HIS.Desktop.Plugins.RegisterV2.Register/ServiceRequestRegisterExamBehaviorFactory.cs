using System;
using HIS.Desktop.Plugins.RegisterV2.Run2;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV2.Register
{
	internal class ServiceRequestRegisterExamBehaviorFactory
	{
		internal static IServiceRequestRegisterExam MakeIServiceRequestRegister(CommonParam param, object data, object patient)
		{
			IServiceRequestRegisterExam serviceRequestRegisterExam = null;
			try
			{
				serviceRequestRegisterExam = new ServiceRequestRegisterExamBehavior(param, (UCRegister)data, (HisPatientSDO)patient);
				if (serviceRequestRegisterExam == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + LogUtil.TraceData(LogUtil.GetMemberName(() => data), data), ex);
				serviceRequestRegisterExam = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
				serviceRequestRegisterExam = null;
			}
			return serviceRequestRegisterExam;
		}
	}
}
