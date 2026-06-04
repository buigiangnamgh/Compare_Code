using System;
using HIS.Desktop.Plugins.RegisterV2.Run2;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV2.Register
{
	internal class ServiceRequestRegisterPatientProfileBehaviorFactory
	{
		internal static IServiceRequestRegisterPatientProfile MakeIServiceRequestRegister(CommonParam param, object data, object patient)
		{
			IServiceRequestRegisterPatientProfile serviceRequestRegisterPatientProfile = null;
			try
			{
				serviceRequestRegisterPatientProfile = new ServiceRequestRegisterPatientProfileBehavior(param, (UCRegister)data, (HisPatientSDO)patient);
				if (serviceRequestRegisterPatientProfile == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + LogUtil.TraceData(LogUtil.GetMemberName(() => data), data), ex);
				serviceRequestRegisterPatientProfile = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
				serviceRequestRegisterPatientProfile = null;
			}
			return serviceRequestRegisterPatientProfile;
		}
	}
}
