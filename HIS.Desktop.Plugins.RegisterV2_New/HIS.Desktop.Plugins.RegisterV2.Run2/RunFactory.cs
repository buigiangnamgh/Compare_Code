using System;
using Inventec.Common.Logging;
using Inventec.Core;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
	internal class RunFactory
	{
		internal static IRun MakeIRegister(CommonParam param, object data)
		{
			IRun run = null;
			try
			{
				if (data.GetType() == typeof(object[]))
				{
					run = new RunBehavior(param, (object[])data);
				}
				if (run == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + LogUtil.TraceData(LogUtil.GetMemberName(() => data), data), ex);
				run = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
				run = null;
			}
			return run;
		}
	}
}
