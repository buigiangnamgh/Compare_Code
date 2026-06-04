using System.Collections.Generic;
using Inventec.Desktop.Common.Modules;

namespace HIS.Desktop.Plugins.RegisterV2
{
	internal class GlobalStore
	{
		internal static List<long> PatientTypeIdAllows { get; set; }

		internal static Module CurrentModule { get; set; }

		internal static long DepartmentId { get; set; }

		internal static UCServiceRequestRegisterFactorySaveType currentFactorySaveType { get; set; }
	}
}
