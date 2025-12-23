using System;
using System.Collections.Generic;
using System.Linq;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.Integrate;

namespace Inventec.Common.SignLibrary
{
	public class GlobalStore
	{
		internal const string appCode = "HIS";

		private static ApiConsumer integrateConsumer;

		private static List<EMR_CONFIG> emrConfigs;

		private static List<EMR_BUSINESS> emrBusiness;

		private static Dictionary<string, ApiConsumer> dicemrConsumer;

		private static ApiConsumer emrConsumer;

		private static ApiConsumer acsConsumer;

		private static string gemBoxPdf__LicKey;

		internal static OptionPrintType OptionPrintType = OptionPrintType.DevLib;

		private static string splitPdfHeaderKey;

		private static string splitPdfContentKey;

		internal static bool PrintUsingWaterMark = true;

		public static bool IsSignUsingUsbTokenDevice = true;

		internal static string EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION = "";

		internal static string EMR_SIGN_SIGNING_OPTION = "";

		internal static string EMR_SIGN_PATIENT_SIGN_OPTION = "";

		internal static string EMR_SIGN_BOARD__OPTION = "1";

		internal static string EMR_SIGN_SIGN_DESCRIPTION_INFO = "2";

		internal static string EMR_HSM_INTEGRATE_OPTION = "1";

		internal static string EMR_EMR_SIGNER_AUTO_UPDATE_SIGN_IMAGE = "";

		internal static string EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION = "";

		internal static string SIGN_CERTIFICATE_OPTION = "";

		internal static string EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES = "";

		internal static string EMR_EMR_SIGN_CONNECT_DEVICE_TYPE_OPTION = "1";

		internal static ApiConsumer IntegrateConsumer
		{
			get
			{
				if (integrateConsumer == null)
				{
					integrateConsumer = new ApiConsumer(INTERGRATE_SYS_BASE_URI, "HIS");
				}
				return integrateConsumer;
			}
			set
			{
				integrateConsumer = value;
			}
		}

		public static List<EMR_CONFIG> EmrConfigs
		{
			get
			{
				if (emrConfigs == null)
				{
					EmrConfig emrConfig = new EmrConfig();
					emrConfigs = emrConfig.Get();
				}
				return emrConfigs;
			}
			set
			{
				emrConfigs = value;
			}
		}

		public static List<EMR_BUSINESS> EmrBusiness
		{
			get
			{
				if (GlobalStore.emrBusiness == null)
				{
					EmrBusiness emrBusiness = new EmrBusiness();
					GlobalStore.emrBusiness = emrBusiness.Get();
				}
				return GlobalStore.emrBusiness;
			}
			set
			{
				emrBusiness = value;
			}
		}

		internal static Dictionary<string, ApiConsumer> DicEmrConsumer
		{
			get
			{
				if (dicemrConsumer == null)
				{
					dicemrConsumer = new Dictionary<string, ApiConsumer>();
				}
				return dicemrConsumer;
			}
			set
			{
				dicemrConsumer = value;
			}
		}

		internal static ApiConsumer EmrConsumer
		{
			get
			{
				if (emrConsumer == null)
				{
					emrConsumer = new ApiConsumer(EMR_BASE_URI, "HIS");
				}
				return emrConsumer;
			}
			set
			{
				emrConsumer = value;
			}
		}

		internal static ApiConsumer AcsConsumer
		{
			get
			{
				if (acsConsumer == null)
				{
					acsConsumer = new ApiConsumer(ConstanIG.ACS_BASE_URI, "HIS");
				}
				return acsConsumer;
			}
			set
			{
				acsConsumer = value;
			}
		}

		internal static string INTERGRATE_SYS_BASE_URI { get; set; }

		internal static string INTERGRATE_SYS_API { get; set; }

		internal static string EMR_BASE_URI { get; set; }

		internal static string HPS_BASE_URI { get; set; }

		internal static string TokenCode { get; set; }

		internal static string LoginName { get; set; }

		internal static string UserName { get; set; }

		internal static EMR_SIGNER Singer { get; set; }

		internal static bool IsUseTimespan { get; set; }

		internal static string Password { get; set; }

		internal static TokenData TokenData { get; set; }

		internal static bool IsUseSendDTI { get; set; }

		internal static string PIN { get; set; }

		internal static string GemBoxPdf__LicKey
		{
			get
			{
				if (string.IsNullOrEmpty(gemBoxPdf__LicKey))
				{
					gemBoxPdf__LicKey = "ARHC-LA4K-R49S-TR4L";
				}
				return gemBoxPdf__LicKey;
			}
			set
			{
				gemBoxPdf__LicKey = value;
			}
		}

		internal static string SplitPdfHeaderKey
		{
			get
			{
				if (string.IsNullOrEmpty(splitPdfHeaderKey))
				{
					splitPdfHeaderKey = "{SignLibrary.SplitPdfHeaderKey}";
				}
				return splitPdfHeaderKey;
			}
			set
			{
				splitPdfHeaderKey = value;
			}
		}

		internal static string SplitPdfContentKey
		{
			get
			{
				if (string.IsNullOrEmpty(splitPdfContentKey))
				{
					splitPdfContentKey = "{SignLibrary.SplitPdfContentKey}";
				}
				return splitPdfContentKey;
			}
			set
			{
				splitPdfContentKey = value;
			}
		}

		public static EMR_SIGNER GetByLoginName(string loginName)
		{
			EMR_SIGNER eMR_SIGNER = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(loginName))
				{
					CommonParam paramCommon = new CommonParam();
					EmrSignerFilter emrSignerFilter = new EmrSignerFilter();
					emrSignerFilter.LOGINNAMEs = new List<string>
					{
						loginName,
						loginName.ToLower(),
						loginName.ToUpper()
					};
					List<EMR_SIGNER> list = new EmrSigner().Get(ref paramCommon, emrSignerFilter);
					eMR_SIGNER = ((list != null) ? list.FirstOrDefault() : null);
					if (eMR_SIGNER == null)
					{
						MessageManager.Show(paramCommon, false);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				eMR_SIGNER = null;
			}
			return eMR_SIGNER;
		}

		public static ApiConsumer GetSetDicConsumer(string tokenCode)
		{
			ApiConsumer apiConsumer = null;
			if (!string.IsNullOrEmpty(tokenCode))
			{
				if (!DicEmrConsumer.ContainsKey(tokenCode))
				{
					apiConsumer = new ApiConsumer(EMR_BASE_URI, tokenCode, "HIS");
					DicEmrConsumer.Add(tokenCode, apiConsumer);
				}
				else
				{
					apiConsumer = DicEmrConsumer[tokenCode];
				}
			}
			return apiConsumer;
		}

		public static Dictionary<string, ApiConsumer> GetDicEmrConsumer()
		{
			return dicemrConsumer;
		}
	}
}
