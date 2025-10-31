using HIS.Desktop.LocalStorage.HisConfig;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Config
{
	internal class HisConfigKeys
	{
		internal const string HIS_CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";

		internal const string HIS_CONFIG_KEY__PATIENT_TYPE_CODE__VP = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";

		internal const string IS_CHECKING_PERMISSON = "MOS.HIS_SERE_SERV_PTTT.IS_CHECKING_PERMISSON";

		internal const string CHECKING_PERMISSON_OPTION = "HIS.Desktop.Plugins.SurgServiceReqExecute.CheckingPermissionOption";

		internal const string ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR = "MOS.HIS_SERVICE_REQ.ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR";

		internal const string HIS_CONFIG_KEY__CHECK_SIMULTANEITY_OPTION = "MOS.HIS_SERVICE_REQ.CHECK_SIMULTANEITY_OPTION";

		internal const string HIS_CONFIG_KEY__ASSIGN_SERVICE_SIMULTANEITY_OPTION = "MOS.HIS_SERVICE_REQ.ASSIGN_SERVICE_SIMULTANEITY_OPTION";

		internal static string CheckPermisson
		{
			get
			{
				return HisConfigs.Get<string>("MOS.HIS_SERE_SERV_PTTT.IS_CHECKING_PERMISSON");
			}
		}

		internal static string allowFinishWhenAccountIsDoctor
		{
			get
			{
				return HisConfigs.Get<string>("MOS.HIS_SERVICE_REQ.ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR");
			}
		}

		internal static string CheckPermissonOption
		{
			get
			{
				return HisConfigs.Get<string>("HIS.Desktop.Plugins.SurgServiceReqExecute.CheckingPermissionOption");
			}
		}

		internal static string IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT
		{
			get
			{
				return HisConfigs.Get<string>("MOS.HIS_SERVICE_REQ.IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT");
			}
		}

		internal static string CHECK_SIMULTANEITY_OPTION
		{
			get
			{
				return HisConfigs.Get<string>("MOS.HIS_SERVICE_REQ.CHECK_SIMULTANEITY_OPTION");
			}
		}

		internal static string ASSIGN_SERVICE_SIMULTANEITY_OPTION
		{
			get
			{
				return HisConfigs.Get<string>("MOS.HIS_SERVICE_REQ.ASSIGN_SERVICE_SIMULTANEITY_OPTION");
			}
		}
	}
}
