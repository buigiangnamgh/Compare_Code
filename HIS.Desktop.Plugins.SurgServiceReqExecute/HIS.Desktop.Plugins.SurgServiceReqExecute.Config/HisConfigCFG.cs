using System;
using System.Linq;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Config
{
	internal class HisConfigCFG
	{
		internal static bool IsCheckDepartmentInTimeWhenPresOrAssign;

		private const string CONFIG_KEY_CheckDepartmentInTimeWhenPresOrAssign = "HIS.Desktop.Plugins.IsCheckDepartmentInTimeWhenPresOrAssign";

		internal static long PatientTypeId__BHYT
		{
			get
			{
				HIS_PATIENT_TYPE val = (from o in BackendDataWorker.Get<HIS_PATIENT_TYPE>()
					where o.PATIENT_TYPE_CODE == HisConfigs.Get<string>("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT")
					select o).FirstOrDefault();
				return (val != null) ? val.ID : 0;
			}
		}

		internal static string TAKE_INTRUCTION_TIME_BY_SERVICE_REQ
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.SurgServiceReqExecute.TakeIntrucionTimeByServiceReq");
			}
		}

		internal static string PrescriptionTypeOption
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.AssignPrescriptionPK.PrescriptionTypeOption");
			}
		}

		internal static string REQUIRED_GROUPPTTT_OPTION
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.SurgServiceReqExecute.RequiredGroupPTTTOption");
			}
		}

		internal static string RequiredEmotionlessMethodOption
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.SurgServiceReqExecute.RequiredEmotionlessMethodOption");
			}
		}

		internal static string IS_NOT_REQUIRED_PTTT_EXECUTE_ROLE
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.SurgServiceReqExecute.IsNotRequiredPtttExecuteRole");
			}
		}

		internal static string PROCESS_TIME_MUST_BE_LESS_THAN_MAX_TOTAL_PROCESS_TIME
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.ProcessTimeMustBeLessThanMaxTotalProcessTime");
			}
		}

		internal static string SURG_SERVICE_REQ_EXECUTE_REQUIRE_ICD_OPTION
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.SurgServiceReqExecute.RequiredIcdCmOption");
			}
		}

		internal static string SURG_SERVICE_REQ_EXECUTE_ROLE_USER_OPTION
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.SurgServiceReqExecute.ExecuteRoleUserOption");
			}
		}

		internal static void LoadConfig()
		{
			try
			{
				IsCheckDepartmentInTimeWhenPresOrAssign = GetValue("HIS.Desktop.Plugins.IsCheckDepartmentInTimeWhenPresOrAssign") == "1";
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private static string GetValue(string code)
		{
			string text = null;
			try
			{
				return HisConfigs.Get<string>(code);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				text = null;
			}
			return text;
		}
	}
}
