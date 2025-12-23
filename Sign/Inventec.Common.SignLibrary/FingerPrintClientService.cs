using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CARD.WCF.Client.FingerprintClient;
using CARD.WCF.DCO;
using EMR.EFMODEL.DataModels;
using EMR.TDO;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	internal class FingerPrintClientService
	{
		private static EMR_RELATION Relation;

		private static bool? isMe;

		internal static bool Valid(bool isHomeRelativeSign, ref string cmnd, ref string cardCode, ref string serviceCode, ref string linkCode, ref string relativeName, ref string relationPeopleName, ref byte[] signedImageData, ref bool isCardAnonymous, ref List<SignTDO> tempSigns, EMR_TREATMENT treatment)
		{
			try
			{
				if (!isHomeRelativeSign && (treatment == null || string.IsNullOrEmpty(treatment.CARD_CODE)))
				{
					MessageManager.Show(MessageUitl.GetMessage("BenhNhanKhongCoTheKCB"));
					LogSystem.Warn(MessageUitl.GetMessage("BenhNhanKhongCoTheKCB"));
					return false;
				}
				WcfFingerprintDCO wcfFingerprintDCO = new WcfFingerprintDCO();
				wcfFingerprintDCO.CardCode = ((treatment != null) ? treatment.CARD_CODE : "");
				if (isHomeRelativeSign)
				{
					wcfFingerprintDCO.IsHomieSign = true;
				}
				if (GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION == "1")
				{
					wcfFingerprintDCO.AuthenType = 1;
				}
				else if (GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION == "2")
				{
					wcfFingerprintDCO.AuthenType = 2;
				}
				int demLanGoi = 0;
				WcfFingerprintDCO wcfRs = VerifyFingerAuthen(wcfFingerprintDCO, ref demLanGoi);
				if (wcfRs == null || wcfRs.ResultCode != "00" || string.IsNullOrEmpty(wcfRs.CmndNumber) || string.IsNullOrEmpty(wcfRs.CardCode))
				{
					LogSystem.Warn("FingerPrintClientService.Valid = false. " + LogUtil.TraceData(LogUtil.GetMemberName(() => wcfRs), wcfRs));
					return false;
				}
				isCardAnonymous = wcfRs.IsCardAnonymous;
				relationPeopleName = ((!string.IsNullOrEmpty(wcfRs.PeopleNameBase64)) ? Utils.Base64Decode(wcfRs.PeopleNameBase64) : "");
				string _relationName = ((!string.IsNullOrEmpty(wcfRs.RelationNameBase64)) ? Utils.Base64Decode(wcfRs.RelationNameBase64) : "");
				relativeName = _relationName;
				cmnd = wcfRs.CmndNumber;
				cardCode = wcfRs.CardCode;
				serviceCode = wcfRs.ServiceCode;
				linkCode = wcfRs.LinkCode;
				signedImageData = ((!string.IsNullOrEmpty(wcfRs.FingerImageBase64)) ? Convert.FromBase64String(wcfRs.FingerImageBase64) : null);
				if (isHomeRelativeSign)
				{
					List<EMR_RELATION> list = new EmrRelation().Get();
					Relation = ((list != null && list.Count > 0) ? list.FirstOrDefault((EMR_RELATION o) => o.RELATION_NAME == _relationName) : null);
					if (tempSigns != null && tempSigns.Count > 0)
					{
						foreach (SignTDO tempSign in tempSigns)
						{
							if (!string.IsNullOrEmpty(tempSign.PatientCode))
							{
								tempSign.CmndNumber = cmnd;
								tempSign.CardCode = cardCode;
								tempSign.ServiceCode = serviceCode;
								tempSign.LinkCode = linkCode;
								tempSign.RelationId = ((Relation != null) ? new long?(Relation.ID) : ((long?)null));
								tempSign.RelationName = _relationName;
								tempSign.RelationPeopleName = relationPeopleName;
								tempSign.SignedImageData = signedImageData;
							}
						}
					}
				}
				else if (tempSigns != null && tempSigns.Count > 0)
				{
					foreach (SignTDO tempSign2 in tempSigns)
					{
						if (!string.IsNullOrEmpty(tempSign2.PatientCode))
						{
							tempSign2.CardCode = cardCode;
							tempSign2.ServiceCode = serviceCode;
							tempSign2.CmndNumber = cmnd;
							tempSign2.LinkCode = linkCode;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Loi goi service xac thuc van tay", ex);
				MessageBox.Show(MessageUitl.GetMessage("LoiGoiServiceXacThucVanTay"));
				return false;
			}
			return true;
		}

		private static WcfFingerprintDCO VerifyFingerAuthen(WcfFingerprintDCO fingerprintDCO, ref int demLanGoi)
		{
			WcfFingerprintDCO wcfRs = null;
			FingerprintClientManager fingerprintClientManager = new FingerprintClientManager();
			wcfRs = fingerprintClientManager.Fingerprint(fingerprintDCO);
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => wcfRs), wcfRs));
			if (wcfRs.ResultCode != "00")
			{
				string messageFinger = "";
				if (!string.IsNullOrEmpty(wcfRs.ResultDescBase64) && !string.IsNullOrEmpty(Utils.Base64Decode(wcfRs.ResultDescBase64)))
				{
					messageFinger += Utils.Base64Decode(wcfRs.ResultDescBase64);
				}
				LogSystem.Warn("Goi service xac thuc van tay. Ket qua tra ve that bai___" + LogUtil.TraceData(LogUtil.GetMemberName(() => messageFinger), messageFinger));
				demLanGoi++;
				if (wcfRs.ResultCode == "45" && demLanGoi <= 3)
				{
					messageFinger += string.Format("({0}). ", wcfRs.ResultCode);
					if (MessageBox.Show(messageFinger + "Bạn có muốn thử lại?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						return VerifyFingerAuthen(fingerprintDCO, ref demLanGoi);
					}
					return null;
				}
				messageFinger += string.Format("({0}). Xử lý thất bại.", wcfRs.ResultCode);
				MessageBox.Show(messageFinger);
				return null;
			}
			return wcfRs;
		}
	}
}
