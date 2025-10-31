using System;
using System.Linq;
using System.Linq.Expressions;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.SurgServiceReqExecute.SurgServiceReqExecute
{
	internal class SurgServiceReqExecuteFactory
	{
		internal static ISurgServiceReqExecute MakeISurgServiceReqExecute(CommonParam param, object[] data)
		{
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Expected O, but got Unknown
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Expected O, but got Unknown
			ISurgServiceReqExecute surgServiceReqExecute = null;
			Inventec.Desktop.Common.Modules.Module moduleData = null;
			V_HIS_SERVICE_REQ data2 = null;
			try
			{
				if (data.GetType() == typeof(object[]) && data != null && data.Count() > 0)
				{
					for (int i = 0; i < data.Count(); i++)
					{
						if (data[i] is V_HIS_SERVICE_REQ)
						{
							data2 = (V_HIS_SERVICE_REQ)data[i];
						}
						else if (data[i] is Inventec.Desktop.Common.Modules.Module)
						{
							moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
						}
					}
					surgServiceReqExecute = new SurgServiceReqExecuteBehavior(param, data2, moduleData);
				}
				if (surgServiceReqExecute == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + LogUtil.TraceData(LogUtil.GetMemberName<object[]>((Expression<Func<object[]>>)(() => data)), (object)data), (Exception)ex);
				surgServiceReqExecute = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
				surgServiceReqExecute = null;
			}
			return surgServiceReqExecute;
		}
	}
}
