using System;
using System.Linq;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
	public sealed class RunBehavior : Tool<IDesktopToolContext>, IRun
	{
		private object[] entity;

		public RunBehavior()
		{
		}

		public RunBehavior(CommonParam param, object[] filter)
		{
			entity = filter;
		}

		object IRun.Run()
		{
			try
			{
				Module module = null;
				if (entity != null && entity.Count() > 0)
				{
					for (int i = 0; i < entity.Count(); i++)
					{
						if (entity[i] is Module)
						{
							module = (Module)entity[i];
						}
					}
				}
				GlobalStore.CurrentModule = module;
				if (module == null)
				{
					throw new NullReferenceException("moduleData");
				}
				if (module.RoomId <= 0)
				{
					throw new NullReferenceException("moduleData.RoomId = " + module.RoomId);
				}
				return new UCRegister(module);
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Error("Factory khong khoi tao duoc doi tuong." + entity.GetType().ToString() + LogUtil.TraceData(LogUtil.GetMemberName(() => entity), entity), ex);
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
			}
			return null;
		}
	}
}
