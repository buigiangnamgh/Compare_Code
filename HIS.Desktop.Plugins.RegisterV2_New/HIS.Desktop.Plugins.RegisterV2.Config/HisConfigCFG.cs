using System;
using System.Collections.Generic;
using System.Linq;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using Inventec.Common.TypeConvert;
using MOS.EFMODEL.DataModels;
using SDA.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterV2.Config
{
	internal class HisConfigCFG
	{
		private const string CONFIG_KEY__IS_CHECK_EXAM_HISTORY_TODAY = "HIS.Desktop.Plugins.Register.IS_CHECK_EXAM_HISTORY_TODAY";

		private const string CONFIG_KEY__IS_CHECK_HEIN_CARD = "HIS.Desktop.Plugins.Register.IsCheckHeinCard";

		private const string CONFIG_KEY__IS_CHECK_PREVIOUS_DEBT = "MOS.HIS_TREATMENT.IS_CHECK_PREVIOUS_DEBT";

		private const string CONFIG_KEY__IS_CHECK_PREVIOUS_PRESCRIPTION = "MOS.HIS_TREATMENT.IS_CHECK_PREVIOUS_PRESCRIPTION";

		private const string CONFIG_KEY__IS_DEFAULT_RIGHT_ROUTE_TYPE = "HIS.Desktop.Plugins.Register.IsDefaultRightRouteType";

		private const string CONFIG_KEY__VISIBILITY_CONTROL = "HIS.HIS_DESKTOP_REGISTER.VISIBILITY_CONTROL_FOR_TIM";

		private const string CONFIG_KEY__NOT_CHECK_EXPIRED_IS_SHOW = "HIS.DESKTOP.REGISTER.HEIN_CARD.NOT_CHECK_EXPIRED.IS_SHOW";

		private const string CONFIG_KEY__ICD_GENERATE = "HIS.Desktop.Plugins.AutoCheckIcd";

		private const string CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";

		private const string CONFIG_KEY__PATIENT_TYPE_CODE__QN = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.QN";

		private const string CONFIG_KEY__IS_PRINT_AFTER_SAVE__KEY = "EXE.SERVICE_REQUEST_REGISTER.IS_PRINT_AFTER_SAVE";

		private const string CONFIG_KEY__IS_VISIBLE_BILL__KEY = "EXE.SERVICE_REQUEST_REGISTER.IS_VISIBLE_BILL";

		private const string CONFIG_KEY__MOS__HIS_PATIENT__MUST_HAVE_NCS_INFO_FOR_CHILD = "MOS.HIS_PATIENT.MUST_HAVE_NCS_INFO_FOR_CHILD";

		private const string CONFIG_KEY__HIS_DEPOSIT__DEFAULT_PRICE_FOR_BHYT_OUT_PATIENT = "HIS_RS.HIS_DEPOSIT.DEFAULT_PRICE_FOR_BHYT_OUT_PATIENT";

		private const string CONFIG_KEY__GENDER_CODE_BASE = "RAE.HIS_GENDER_CODE__BASE";

		private const string CONFIG_KEY__HIS_CAREER_CODE__BASE = "EXE.HIS_CAREER_CODE__BASE";

		private const string CONFIG_KEY__ETHNIC_CODE_BASE = "EXE.ETHNIC_CODE_BASE";

		private const string CONFIG_KEY__NATIONAL_CODE_BASE = "EXE.NATIONAL_CODE_BASE";

		private const string CONFIG_KEY__CAREER_CODE__UNDER_6_AGE = "EXE.HIS_CAREER_CODE__UNDER_6_AGE";

		private const string CONFIG_KEY__CAREER_CODE__HOC_SINH = "HIS.DESKTOP.REGISTER.HIS_CAREER.CARRER_CODE_HS";

		private const string HIS_DESKTOP_REGISER__EXECUTE_ROOM_SHOW = "HIS.HIS_DESKTOP_REGISTER.EXECUTE_ROOM_CODE.SHOW";

		private const string HIS_UC_UCHein_IS_OBLIGATORY_TRANFER_MEDI_ORG = "HIS.UC.UCHein.IS_OBLIGATORY_TRANFER_MEDI_ORG";

		private const string CONFIG_KEY__IS_USE_HID_SYNC = "CONFIG_KEY__IS_USE_HID_SYNC";

		private const string CONFIG_KEY__WarningOverExamBhyt = "HIS.Desktop.WarningOverExamBhyt";

		private const string CONFIG_KEY__WarningHeinPatientTypeCode = "HIS.Desktop.Plugins.RegisterV2.WarningHeinPatientTypeCode";

		private const string CONFIG_KEY__WarningOverMonth = "HIS.Desktop.Plugins.RegisterV2.WarningOverMonthsTransfer";

		private const string valueString__true = "1";

		private const int valueInt__true = 1;

		internal static string IsShowCheckExpired;

		internal static bool IsCheckHeinCard;

		internal static bool IsCheckPreviousDebt;

		internal static bool IsCheckPreviousPrescription;

		internal static string IsDefaultRightRouteType;

		internal static bool VisibilityControl;

		internal static bool IsObligatoryTranferMediOrg;

		internal static bool IsWarningOverExamBhyt;

		internal static bool IsCheckExamHistory;

		internal static string AutoCheckIcd;

		internal static string PatientTypeCode__BHYT;

		internal static long PatientTypeId__BHYT;

		internal static string PatientTypeCode__QN;

		internal static long PatientTypeId__QN;

		internal static string IsPrintAfterSave;

		internal static string IsVisibleBill;

		internal static bool IsSetDefaultDepositPrice;

		internal static List<string> ExecuteRoomShow;

		internal static long WarnOverMonthsTransfer;

		internal static bool MustHaveNCSInfoForChild;

		internal static HIS_GENDER GenderBase;

		internal static HIS_CAREER CareerBase;

		internal static HIS_CAREER CareerHS;

		internal static HIS_CAREER CareerUnder6Age;

		internal static SDA_NATIONAL NationalBase;

		internal static SDA_ETHNIC EthinicBase;

		public static bool IsSyncHID { get; set; }

		internal static void LoadConfig()
		{
			try
			{
				LogSystem.Debug("LoadConfig => 1");
				ExecuteRoomShow = GetListValue("HIS.HIS_DESKTOP_REGISTER.EXECUTE_ROOM_CODE.SHOW");
				IsSetDefaultDepositPrice = Parse.ToInt32(GetValue("HIS_RS.HIS_DEPOSIT.DEFAULT_PRICE_FOR_BHYT_OUT_PATIENT")) == 1;
				MustHaveNCSInfoForChild = GetValue("MOS.HIS_PATIENT.MUST_HAVE_NCS_INFO_FOR_CHILD") == "1";
				IsPrintAfterSave = GetValue("EXE.SERVICE_REQUEST_REGISTER.IS_PRINT_AFTER_SAVE");
				IsVisibleBill = GetValue("EXE.SERVICE_REQUEST_REGISTER.IS_VISIBLE_BILL");
				IsObligatoryTranferMediOrg = GetValue("HIS.UC.UCHein.IS_OBLIGATORY_TRANFER_MEDI_ORG") == "1";
				PatientTypeCode__BHYT = GetValue("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT");
				PatientTypeId__BHYT = GetPatientTypeByCode(PatientTypeCode__BHYT).ID;
				PatientTypeCode__QN = GetValue("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.QN");
				PatientTypeId__QN = GetPatientTypeByCode(PatientTypeCode__QN).ID;
				AutoCheckIcd = GetValue("HIS.Desktop.Plugins.AutoCheckIcd");
				IsShowCheckExpired = GetValue("HIS.DESKTOP.REGISTER.HEIN_CARD.NOT_CHECK_EXPIRED.IS_SHOW");
				IsCheckHeinCard = GetValue("HIS.Desktop.Plugins.Register.IsCheckHeinCard") == "1";
				IsCheckPreviousDebt = GetValue("MOS.HIS_TREATMENT.IS_CHECK_PREVIOUS_DEBT") == "1";
				IsCheckPreviousPrescription = GetValue("MOS.HIS_TREATMENT.IS_CHECK_PREVIOUS_PRESCRIPTION") == "1";
				IsDefaultRightRouteType = GetValue("HIS.Desktop.Plugins.Register.IsDefaultRightRouteType");
				VisibilityControl = GetValue("HIS.HIS_DESKTOP_REGISTER.VISIBILITY_CONTROL_FOR_TIM") == "1";
				IsCheckExamHistory = GetValue("HIS.Desktop.Plugins.Register.IS_CHECK_EXAM_HISTORY_TODAY") == "1";
				GenderBase = GetGenderByCode(GetValue("RAE.HIS_GENDER_CODE__BASE"));
				CareerBase = GetCareerByCode(GetValue("EXE.HIS_CAREER_CODE__BASE"));
				CareerHS = GetCareerByCode(GetValue("HIS.DESKTOP.REGISTER.HIS_CAREER.CARRER_CODE_HS"));
				CareerUnder6Age = GetCareerByCode(GetValue("EXE.HIS_CAREER_CODE__UNDER_6_AGE"));
				EthinicBase = GetEthnicByCode(GetValue("EXE.ETHNIC_CODE_BASE"));
				NationalBase = GetNationalByCode(GetValue("EXE.NATIONAL_CODE_BASE"));
				IsSyncHID = GetValue("CONFIG_KEY__IS_USE_HID_SYNC") == "1";
				IsWarningOverExamBhyt = GetValue("HIS.Desktop.WarningOverExamBhyt") == "1";
				WarnOverMonthsTransfer = HisConfigs.Get<long>("HIS.Desktop.Plugins.RegisterV2.WarningOverMonthsTransfer");
				LogSystem.Debug("LoadConfig => 2");
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private static HIS_CAREER GetCareerByCode(string code)
		{
			HIS_CAREER hIS_CAREER = new HIS_CAREER();
			try
			{
				hIS_CAREER = BackendDataWorker.Get<HIS_CAREER>().FirstOrDefault((HIS_CAREER o) => o.CAREER_CODE == code);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return hIS_CAREER ?? new HIS_CAREER();
		}

		private static SDA_ETHNIC GetEthnicByCode(string code)
		{
			SDA_ETHNIC sDA_ETHNIC = new SDA_ETHNIC();
			try
			{
				sDA_ETHNIC = (from o in BackendDataWorker.Get<SDA_ETHNIC>()
					where o.IS_ACTIVE == 1
					select o).ToList().FirstOrDefault((SDA_ETHNIC o) => o.ETHNIC_CODE == code);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return sDA_ETHNIC ?? new SDA_ETHNIC();
		}

		private static SDA_NATIONAL GetNationalByCode(string code)
		{
			SDA_NATIONAL sDA_NATIONAL = new SDA_NATIONAL();
			try
			{
				sDA_NATIONAL = (from o in BackendDataWorker.Get<SDA_NATIONAL>()
					where o.IS_ACTIVE == 1
					select o).ToList().FirstOrDefault((SDA_NATIONAL o) => o.NATIONAL_CODE == code);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return sDA_NATIONAL ?? new SDA_NATIONAL();
		}

		private static HIS_GENDER GetGenderByCode(string code)
		{
			HIS_GENDER hIS_GENDER = new HIS_GENDER();
			try
			{
				hIS_GENDER = BackendDataWorker.Get<HIS_GENDER>().FirstOrDefault((HIS_GENDER o) => o.GENDER_CODE == code);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return hIS_GENDER ?? new HIS_GENDER();
		}

		private static HIS_PATIENT_TYPE GetPatientTypeByCode(string code)
		{
			HIS_PATIENT_TYPE hIS_PATIENT_TYPE = new HIS_PATIENT_TYPE();
			try
			{
				hIS_PATIENT_TYPE = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault((HIS_PATIENT_TYPE o) => o.PATIENT_TYPE_CODE == code);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return hIS_PATIENT_TYPE ?? new HIS_PATIENT_TYPE();
		}

		private static string GetValue(string key)
		{
			try
			{
				return HisConfigs.Get<string>(key);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return "";
		}

		private static List<string> GetListValue(string key)
		{
			try
			{
				return HisConfigs.Get<List<string>>(key);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return null;
		}
	}
}
