using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;
using EMR.EFMODEL.DataModels;
using EMR.SDO;
using EMR.TDO;
using EMR.WCF.DCO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignFile;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;
using Inventec.Common.SignLibrary.ServiceSign;
using Newtonsoft.Json;

namespace Inventec.Common.SignLibrary.SignHandler
{
	internal class SignUSBHandler : BussinessBase
	{
		internal SignUSBHandler()
		{
		}

		internal SignUSBHandler(CommonParam param, InputADO inputADOWorking, bool isSignParanel, EMR_TREATMENT treatment, EMR_SIGNER singer, string tokenCode)
			: base(param, inputADOWorking, "", isSignParanel, treatment, singer, tokenCode)
		{
		}

		internal bool SignWithCreateDoc(string outputFile, ref DocumentTDO document, string documentName, string treatmentCode, List<SignTDO> signStrategys, List<SignTDO> signTemps, string base64SignedFileData, string GetBase64OriginalFileData, long? documentTypeId, bool isMultiSign, string hisCode, EMR_SIGN _signSelected, string signDescription, string mergeCode = "")
		{
			bool result = false;
			UsbSignCreateTDO usbSignCreateTDO = new UsbSignCreateTDO();
			usbSignCreateTDO.IsOutsideTreatment = base.inputADOWorking.IsOutsideTreatment == 1;
			usbSignCreateTDO.MediOrgCode = base.inputADOWorking.MediOrgCode;
			usbSignCreateTDO.Signs = (string.IsNullOrEmpty(base.inputADOWorking.BusinessCode) ? signTemps : null);
			if (string.IsNullOrEmpty(base.inputADOWorking.BusinessCode) && signStrategys != null && signStrategys.Count > 0)
			{
				if (usbSignCreateTDO.Signs == null)
				{
					usbSignCreateTDO.Signs = new List<SignTDO>();
				}
				usbSignCreateTDO.Signs.AddRange(signStrategys);
			}
			if (usbSignCreateTDO.Signs != null)
			{
				if (usbSignCreateTDO.Signs.FirstOrDefault() != null && !string.IsNullOrEmpty(usbSignCreateTDO.Signs[0].PatientCode))
				{
					base.param.Messages.Add("Bạn chưa đến lượt ký.");
					return result;
				}
				foreach (SignTDO sign in usbSignCreateTDO.Signs)
				{
					sign.Version = new VersionTDO();
					sign.Version.Base64Data = base64SignedFileData;
					if (base.Signer != null && sign.Loginname == base.Signer.LOGINNAME)
					{
						sign.Description = signDescription;
					}
				}
			}
			if (base.FileType == FileType.Xml)
			{
				usbSignCreateTDO.FileType = EMR.TDO.FileType.XML;
			}
			else if (base.FileType == FileType.Json)
			{
				usbSignCreateTDO.FileType = EMR.TDO.FileType.JSON;
			}
			else
			{
				usbSignCreateTDO.FileType = EMR.TDO.FileType.PDF;
			}
			usbSignCreateTDO.MergeCode = mergeCode;
			usbSignCreateTDO.RoomCode = base.inputADOWorking.RoomCode;
			usbSignCreateTDO.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
			usbSignCreateTDO.DependentCode = base.inputADOWorking.DependentCode;
			usbSignCreateTDO.ParentDependentCode = base.inputADOWorking.ParentDependentCode;
			usbSignCreateTDO.IsSignParallel = base.IsSignParanel;
			usbSignCreateTDO.OriginalVersion = new VersionTDO();
			if (document != null && !string.IsNullOrEmpty(document.DocumentCode))
			{
				usbSignCreateTDO.DocumentCode = document.DocumentCode;
				usbSignCreateTDO.DocumentName = document.DocumentName;
				if (document.DocumentTypeId.HasValue && document.DocumentTypeId.Value > 0)
				{
					usbSignCreateTDO.DocumentTypeId = document.DocumentTypeId;
				}
				usbSignCreateTDO.OriginalVersion.DocumentCode = document.DocumentCode;
			}
			else
			{
				usbSignCreateTDO.DocumentName = (string.IsNullOrEmpty(documentName) ? ("Ký điện tử cho hồ sơ có mã " + treatmentCode + " ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) : documentName);
				if (documentTypeId.HasValue && documentTypeId.Value > 0)
				{
					usbSignCreateTDO.DocumentTypeId = documentTypeId;
				}
				usbSignCreateTDO.HisCode = hisCode;
				if (base.inputADOWorking.DocumentTime.HasValue && base.inputADOWorking.DocumentTime.Value != DateTime.MinValue)
				{
					usbSignCreateTDO.DocumentTime = DateTimeConvert.SystemDateTimeToTimeNumber(base.inputADOWorking.DocumentTime);
				}
			}
			if (!string.IsNullOrEmpty(base.inputADOWorking.DocumentGroupCode))
			{
				EMR_DOCUMENT_GROUP byCode = new EmrDocumentGroup().GetByCode(base.inputADOWorking.DocumentGroupCode);
				usbSignCreateTDO.DocumentGroupId = ((byCode != null) ? new long?(byCode.ID) : ((long?)null));
			}
			usbSignCreateTDO.TreatmentCode = treatmentCode;
			usbSignCreateTDO.OriginalVersion.Base64Data = GetBase64OriginalFileData;
			usbSignCreateTDO.IsFinishSign = !isMultiSign;
			usbSignCreateTDO.IsSigning = isMultiSign;
			usbSignCreateTDO.BusinessCode = base.inputADOWorking.BusinessCode;
			usbSignCreateTDO.HisOrder = base.inputADOWorking.HisOrder;
			if (base.inputADOWorking.PaperSizeDefault != null)
			{
				usbSignCreateTDO.PaperName = base.inputADOWorking.PaperSizeDefault.PaperName;
				if (string.IsNullOrEmpty(usbSignCreateTDO.PaperName))
				{
					usbSignCreateTDO.PaperName = base.inputADOWorking.PaperSizeDefault.Kind.ToString();
				}
				usbSignCreateTDO.Width = base.inputADOWorking.PaperSizeDefault.Width;
				usbSignCreateTDO.Height = base.inputADOWorking.PaperSizeDefault.Height;
				usbSignCreateTDO.RawKind = base.inputADOWorking.PaperSizeDefault.RawKind;
			}
			if (!VerifyDataPreCallApi(usbSignCreateTDO))
			{
				base.param.Messages.Add(MessageUitl.GetMessage("DuLieuKhongHopLe"));
				MessageManager.Show(base.param, false);
				return false;
			}
			CommonParam commonParam = new CommonParam();
			UsbSignCreateTDO usbSignCreateTDO2 = new EmrDocument(commonParam).CreateAndSignUsb(base.TokenCode, usbSignCreateTDO);
			if (usbSignCreateTDO2 != null)
			{
				result = true;
				document.DocumentCode = usbSignCreateTDO2.DocumentCode;
				document.DocumentName = usbSignCreateTDO2.DocumentName;
				document.DocumentTypeId = usbSignCreateTDO2.DocumentTypeId;
				document.TreatmentCode = usbSignCreateTDO2.TreatmentCode;
				document.DependentCode = usbSignCreateTDO2.DependentCode;
				document.ParentDependentCode = usbSignCreateTDO2.ParentDependentCode;
				document.PaperName = usbSignCreateTDO2.PaperName;
				document.Width = usbSignCreateTDO2.Width;
				document.Height = usbSignCreateTDO2.Height;
				document.RawKind = usbSignCreateTDO2.RawKind;
			}
			else
			{
				if (commonParam.Messages != null && commonParam.Messages.Count > 0)
				{
					base.param.Messages.AddRange(commonParam.Messages);
				}
				if (commonParam.BugCodes != null && commonParam.BugCodes.Count > 0)
				{
					base.param.BugCodes.AddRange(commonParam.BugCodes);
				}
			}
			return result;
		}

		internal bool SignOnly(ref DocumentTDO document, bool isMultiSign, List<SignTDO> signStrategys, List<SignTDO> signTemps, string base64SignedFileData, EMR_SIGN _signSelected, string signReason, string mergeCode = "")
		{
			bool result = false;
			try
			{
				EmrSignUsbSDO emrSignUsbSDO = new EmrSignUsbSDO();
				V_EMR_DOCUMENT viewByCode = new EmrDocument().GetViewByCode(document.DocumentCode);
				emrSignUsbSDO.EmrDocumentId = viewByCode.ID;
				EMR_SIGN eMR_SIGN = ((_signSelected != null) ? _signSelected : new EmrSign().GetSignDocumentFirst(viewByCode, base.Signer, base.Treatment, true));
				emrSignUsbSDO.EmrSignId = eMR_SIGN.ID;
				emrSignUsbSDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
				emrSignUsbSDO.Base64FileData = base64SignedFileData;
				emrSignUsbSDO.IsFinishSign = !isMultiSign;
				emrSignUsbSDO.IsSigning = isMultiSign;
				emrSignUsbSDO.RoomCode = base.inputADOWorking.RoomCode;
				emrSignUsbSDO.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
				emrSignUsbSDO.Description = signReason;
				CommonParam commonParam = new CommonParam();
				EmrSignResultSDO emrSignResultSDO = new EmrDocument(commonParam).SignUsb(base.TokenCode, emrSignUsbSDO);
				if (emrSignResultSDO != null)
				{
					result = true;
				}
				else
				{
					if (commonParam.Messages != null && commonParam.Messages.Count > 0)
					{
						base.param.Messages.AddRange(commonParam.Messages);
					}
					if (commonParam.BugCodes != null && commonParam.BugCodes.Count > 0)
					{
						base.param.BugCodes.AddRange(commonParam.BugCodes);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				base.param.Messages.Add(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				result = false;
			}
			return result;
		}

		private IntPtr? ActiveTopFormHandle()
		{
			IntPtr? result = null;
			try
			{
				Form form = null;
				List<Form> list = Application.OpenForms.Cast<Form>().ToList();
				if (list != null && list.Count > 0)
				{
					for (int num = Application.OpenForms.Count - 1; num >= 0; num--)
					{
						if (!(Application.OpenForms[num].Name == "frmWaitForm") && !string.IsNullOrEmpty(Application.OpenForms[num].Name))
						{
							form = Application.OpenForms[num];
							break;
						}
					}
				}
				if (form != null)
				{
					form.Activate();
					result = form.Handle;
					return result;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		internal bool SignFileWithUSBTokenTSAWithCallService(ref string outFileName, string src, Stream stream, float x, float y, int pageNumberCurrent, int totalPageNumber, string signReason, DisplayConfigDTO displayConfigParam, Action dlgCancel)
		{
			bool flag = false;
			try
			{
				LogSystem.Info("SignFileWithUSBTokenTSAWithCallService.1");
				string text = "";
				string text2 = Utils.GenerateTempFileWithin();
				TimestampConfig timestampConfig = new TimestampConfig();
				timestampConfig.UseTimestamp = GlobalStore.IsUseTimespan;
				timestampConfig.TsaUrl = "http://ca.gov.vn/tsa";
				DisplayConfig displayConfig = new DisplayConfig();
				displayConfig.CoorXRectangle = x;
				displayConfig.CoorYRectangle = y;
				displayConfig.NumberPageSign = pageNumberCurrent;
				displayConfig.MaxPageSign = totalPageNumber;
				displayConfig.Location = ((displayConfigParam != null && !string.IsNullOrEmpty(displayConfigParam.Location)) ? displayConfigParam.Location : ((base.Signer != null) ? (base.Signer.DEPARTMENT_NAME + "|" + base.Signer.TITLE) : ""));
				DisplayConfig displayConfig2 = displayConfig;
				if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
				{
					displayConfig2.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
					displayConfig2.BImage = base.Signer.SIGN_IMAGE;
				}
				if (displayConfigParam != null)
				{
					if (displayConfigParam.WidthRectangle.HasValue)
					{
						displayConfig2.WidthRectangle = displayConfigParam.WidthRectangle.Value;
					}
					if (displayConfigParam.HeightRectangle.HasValue)
					{
						displayConfig2.HeightRectangle = displayConfigParam.HeightRectangle.Value;
					}
					if (displayConfigParam.SizeFont.HasValue)
					{
						displayConfig2.SizeFont = displayConfigParam.SizeFont.Value;
					}
					if (displayConfigParam.TextPosition.HasValue)
					{
						displayConfig2.TextPosition = (Constans.TEXT_POSITON)displayConfigParam.TextPosition.Value;
					}
					if (displayConfigParam.TypeDisplay.HasValue)
					{
						displayConfig2.TypeDisplay = displayConfigParam.TypeDisplay.Value;
					}
					if (displayConfigParam.IsDisplaySignature.HasValue)
					{
						displayConfig2.IsDisplaySignature = displayConfigParam.IsDisplaySignature.Value;
					}
					if (!string.IsNullOrEmpty(displayConfigParam.FormatRectangleText))
					{
						displayConfig2.FormatRectangleText = displayConfigParam.FormatRectangleText;
					}
					if (displayConfigParam.Titles != null)
					{
						displayConfig2.Titles = displayConfigParam.Titles;
					}
					if (displayConfig2.TextFormat == null)
					{
						displayConfig2.TextFormat = new FontConfig();
					}
					if (displayConfigParam.Alignment.HasValue)
					{
						displayConfig2.TextFormat.Alignment = (ALIGNMENT_OPTION)displayConfigParam.Alignment.Value;
					}
					if (displayConfigParam.IsBold.HasValue)
					{
						displayConfig2.TextFormat.IsBold = displayConfigParam.IsBold.Value;
					}
					if (displayConfigParam.IsItalic.HasValue)
					{
						displayConfig2.TextFormat.IsItalic = displayConfigParam.IsItalic.Value;
					}
					if (displayConfigParam.IsUnderlined.HasValue)
					{
						displayConfig2.TextFormat.IsUnderlined = displayConfigParam.IsUnderlined.Value;
					}
					if (!string.IsNullOrEmpty(displayConfigParam.FontName))
					{
						displayConfig2.TextFormat.FontName = displayConfigParam.FontName;
					}
				}
				displayConfig2.PageSize = null;
				if (GlobalStore.EMR_SIGN_SIGN_DESCRIPTION_INFO == "1")
				{
					displayConfig2.IsDisplaySignNote = true;
				}
				WcfSignDCO wcfSignDCO = new WcfSignDCO();
				wcfSignDCO.IsUseTimespan = GlobalStore.IsUseTimespan;
				wcfSignDCO.OutputFile = StringCompressorParse.CompressString(text2);
				wcfSignDCO.PIN = GlobalStore.PIN;
				wcfSignDCO.SignReason = StringCompressorParse.CompressString(signReason);
				wcfSignDCO.SourceFile = StringCompressorParse.CompressString(src);
				wcfSignDCO.HwndParent = ActiveTopFormHandle();
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				if (emrConfigs != null && emrConfigs.Count > 0)
				{
					IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGNING_OPTION");
					EMR_CONFIG eMR_CONFIG = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
					if (eMR_CONFIG != null)
					{
						string emrSigningOption = ((!string.IsNullOrEmpty(eMR_CONFIG.VALUE)) ? eMR_CONFIG.VALUE : eMR_CONFIG.DEFAULT_VALUE);
						wcfSignDCO.EmrSigningOption = emrSigningOption;
					}
				}
				if (wcfSignDCO.EmrSigningOption == "2")
				{
					if (base.Signer != null && base.Signer.SIGN_IMAGE != null && string.IsNullOrEmpty(base.Signer.PCA_SERIAL))
					{
						displayConfig2.Contact = ((base.Signer != null) ? base.Signer.USERNAME : "");
					}
					else
					{
						wcfSignDCO.EmrSigningOption = "";
					}
				}
				if (base.Signer != null && base.Signer.SIGNATURE_DISPLAY_TYPE.HasValue)
				{
					long value = base.Signer.SIGNATURE_DISPLAY_TYPE.Value;
					long num = value;
					long num2 = num;
					if ((ulong)num2 <= 3uL)
					{
						long num3 = num2;
						if (num3 <= 3 && num3 >= 0)
						{
							switch (num3)
							{
							case 0L:
								displayConfig2.IsDisplaySignature = false;
								break;
							case 1L:
								displayConfig2.TypeDisplay = Constans.DISPLAY_RECTANGLE_TEXT;
								break;
							case 2L:
								if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
								{
									displayConfig2.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP;
								}
								else
								{
									displayConfig2.IsDisplaySignature = false;
								}
								break;
							case 3L:
								if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
								{
									displayConfig2.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
								}
								else
								{
									displayConfig2.IsDisplaySignature = false;
								}
								break;
							}
						}
					}
				}
				if (base.Signer != null && base.Signer.SIGNALTURE_IMAGE_WIDTH.HasValue)
				{
					decimal? sIGNALTURE_IMAGE_WIDTH = base.Signer.SIGNALTURE_IMAGE_WIDTH;
					if ((sIGNALTURE_IMAGE_WIDTH.GetValueOrDefault() > 0m) & sIGNALTURE_IMAGE_WIDTH.HasValue)
					{
						displayConfig2.SignaltureImageWidth = (float)base.Signer.SIGNALTURE_IMAGE_WIDTH.Value;
					}
				}
				wcfSignDCO.DisplayConfig = StringCompressorParse.CompressString(JsonConvert.SerializeObject(displayConfig2));
				string data = JsonConvert.SerializeObject(wcfSignDCO);
				SignProcessorClient signProcessorClient = new SignProcessorClient();
				WcfSignResultDCO wcfSignResultDCO = signProcessorClient.SignExecute(data);
				if (wcfSignResultDCO != null)
				{
					flag = wcfSignResultDCO.Success;
					outFileName = (wcfSignResultDCO.Success ? StringCompressorParse.DecompressString(wcfSignResultDCO.OutputFile) : "");
					text = StringCompressorParse.DecompressString(wcfSignResultDCO.Message);
				}
				if (!flag)
				{
					CommonParam commonParam = new CommonParam();
					commonParam.Messages = new List<string>();
					if (!string.IsNullOrEmpty(text))
					{
						commonParam.Messages.Add(text);
					}
					commonParam.Messages.Add(MessageUitl.GetMessage("KySuDungUSBTokenThatBai"));
					MessageManager.Show(commonParam, false);
				}
				if (dlgCancel != null)
				{
					dlgCancel();
				}
				LogSystem.Info("SignFileWithUSBTokenTSAWithCallService.2");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return flag;
		}

		internal bool SignFileWithUSBTokenTSAWithUsingUsbTokenDevice(ref string outFileName, string src, Stream stream, float x, float y, int pageNumberCurrent, int totalPageNumber, string signReason, DisplayConfigDTO displayConfigParam, Action dlgCancel)
		{
			bool flag = false;
			try
			{
				SignPdfFile signPdfFile = new SignPdfFile();
				string errMessage = "";
				if (signPdfFile != null)
				{
					bool flag2 = false;
					List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
					if (emrConfigs != null && emrConfigs.Count > 0)
					{
						IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGNING_OPTION");
						EMR_CONFIG eMR_CONFIG = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
						if (eMR_CONFIG != null)
						{
							string text = ((!string.IsNullOrEmpty(eMR_CONFIG.VALUE)) ? eMR_CONFIG.VALUE : eMR_CONFIG.DEFAULT_VALUE);
							if (text == "2")
							{
								flag2 = true;
							}
						}
					}
					DisplayConfig displayConfig = new DisplayConfig();
					displayConfig.CoorXRectangle = x;
					displayConfig.CoorYRectangle = y;
					displayConfig.NumberPageSign = pageNumberCurrent;
					displayConfig.MaxPageSign = totalPageNumber;
					displayConfig.Location = ((displayConfigParam != null && !string.IsNullOrEmpty(displayConfigParam.Location)) ? displayConfigParam.Location : ((base.Signer != null) ? (base.Signer.DEPARTMENT_NAME + "|" + base.Signer.TITLE) : ""));
					DisplayConfig displayConfig2 = displayConfig;
					if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
					{
						displayConfig2.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
						displayConfig2.BImage = base.Signer.SIGN_IMAGE;
					}
					if (displayConfigParam != null)
					{
						if (displayConfigParam.WidthRectangle.HasValue)
						{
							displayConfig2.WidthRectangle = displayConfigParam.WidthRectangle.Value;
						}
						if (displayConfigParam.HeightRectangle.HasValue)
						{
							displayConfig2.HeightRectangle = displayConfigParam.HeightRectangle.Value;
						}
						if (displayConfigParam.SizeFont.HasValue)
						{
							displayConfig2.SizeFont = displayConfigParam.SizeFont.Value;
						}
						if (displayConfigParam.TextPosition.HasValue)
						{
							displayConfig2.TextPosition = (Constans.TEXT_POSITON)displayConfigParam.TextPosition.Value;
						}
						if (displayConfigParam.TypeDisplay.HasValue)
						{
							displayConfig2.TypeDisplay = displayConfigParam.TypeDisplay.Value;
						}
						if (displayConfigParam.IsDisplaySignature.HasValue)
						{
							displayConfig2.IsDisplaySignature = displayConfigParam.IsDisplaySignature.Value;
						}
						if (!string.IsNullOrEmpty(displayConfigParam.FormatRectangleText))
						{
							displayConfig2.FormatRectangleText = displayConfigParam.FormatRectangleText;
						}
						if (displayConfigParam.Titles != null)
						{
							displayConfig2.Titles = displayConfigParam.Titles;
						}
						if (displayConfig2.TextFormat == null)
						{
							displayConfig2.TextFormat = new FontConfig();
						}
						if (displayConfigParam.Alignment.HasValue)
						{
							displayConfig2.TextFormat.Alignment = (ALIGNMENT_OPTION)displayConfigParam.Alignment.Value;
						}
						if (displayConfigParam.IsBold.HasValue)
						{
							displayConfig2.TextFormat.IsBold = displayConfigParam.IsBold.Value;
						}
						if (displayConfigParam.IsItalic.HasValue)
						{
							displayConfig2.TextFormat.IsItalic = displayConfigParam.IsItalic.Value;
						}
						if (displayConfigParam.IsUnderlined.HasValue)
						{
							displayConfig2.TextFormat.IsUnderlined = displayConfigParam.IsUnderlined.Value;
						}
						if (!string.IsNullOrEmpty(displayConfigParam.FontName))
						{
							displayConfig2.TextFormat.FontName = displayConfigParam.FontName;
						}
					}
					if (base.Signer != null && base.Signer.SIGNATURE_DISPLAY_TYPE.HasValue)
					{
						long value = base.Signer.SIGNATURE_DISPLAY_TYPE.Value;
						long num = value;
						long num2 = num;
						if ((ulong)num2 <= 3uL)
						{
							long num3 = num2;
							if (num3 <= 3 && num3 >= 0)
							{
								switch (num3)
								{
								case 0L:
									displayConfig2.IsDisplaySignature = false;
									break;
								case 1L:
									displayConfig2.TypeDisplay = Constans.DISPLAY_RECTANGLE_TEXT;
									break;
								case 2L:
									if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
									{
										displayConfig2.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP;
									}
									else
									{
										displayConfig2.IsDisplaySignature = false;
									}
									break;
								case 3L:
									if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
									{
										displayConfig2.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
									}
									else
									{
										displayConfig2.IsDisplaySignature = false;
									}
									break;
								}
							}
						}
					}
					if (base.Signer != null && base.Signer.SIGNALTURE_IMAGE_WIDTH.HasValue)
					{
						decimal? sIGNALTURE_IMAGE_WIDTH = base.Signer.SIGNALTURE_IMAGE_WIDTH;
						if ((sIGNALTURE_IMAGE_WIDTH.GetValueOrDefault() > 0m) & sIGNALTURE_IMAGE_WIDTH.HasValue)
						{
							displayConfig2.SignaltureImageWidth = (float)base.Signer.SIGNALTURE_IMAGE_WIDTH.Value;
						}
					}
					string text2 = Utils.GenerateTempFileWithin();
					if (GlobalStore.EMR_SIGN_SIGN_DESCRIPTION_INFO == "1")
					{
						displayConfig2.IsDisplaySignNote = true;
					}
					if (CertUtil.CheckHasCertificate(StoreName.My, StoreLocation.CurrentUser, false, true))
					{
						X509Certificate2 byDialog = CertUtil.GetByDialog(StoreName.My, StoreLocation.CurrentUser, false, true);
						if (byDialog != null)
						{
							TimestampConfig timestampConfig = new TimestampConfig();
							timestampConfig.UseTimestamp = GlobalStore.IsUseTimespan;
							timestampConfig.TsaUrl = "http://ca.gov.vn/tsa";
							if (!string.IsNullOrEmpty(src))
							{
								if (base.FileType == FileType.Xml)
								{
									byte[] bOutFile = null;
									XmlConfig xmlConfig = new XmlConfig();
									xmlConfig.Reason = signReason;
									flag = signPdfFile.SignXml(byDialog, File.ReadAllBytes(src), ref bOutFile, xmlConfig, null, ref errMessage, GlobalStore.PIN);
								}
								else if (base.FileType == FileType.Json)
								{
									using (MemoryStream outStream = new MemoryStream())
									{
										flag = signPdfFile.SignJson(byDialog, src, outStream, signReason, "", displayConfig2, null, ref errMessage, GlobalStore.PIN);
									}
								}
								else
								{
									flag = signPdfFile.SignPDF(byDialog, src, text2, signReason, "", timestampConfig, displayConfig2, null, ref errMessage, GlobalStore.PIN);
								}
							}
							else
							{
								flag = signPdfFile.SignPDF(byDialog, stream, text2, signReason, "", timestampConfig, displayConfig2, null, ref errMessage);
							}
							if (flag)
							{
								outFileName = text2;
							}
						}
					}
					else if (flag2 && base.Signer != null && base.Signer.SIGN_IMAGE != null && string.IsNullOrEmpty(base.Signer.PCA_SERIAL))
					{
						displayConfig2.Contact = ((base.Signer != null) ? base.Signer.USERNAME : "");
						flag = (string.IsNullOrEmpty(src) ? signPdfFile.SignPDF(null, stream, text2, signReason, "", null, displayConfig2, null, ref errMessage, flag2) : signPdfFile.SignPDF(null, src, text2, signReason, "", null, displayConfig2, null, ref errMessage, GlobalStore.PIN, flag2));
						if (flag)
						{
							outFileName = text2;
						}
					}
					else
					{
						MessageBox.Show("Not found Certificate");
						LogSystem.Warn("Not found Certificate");
					}
				}
				else
				{
					MessageBox.Show("Only documents of type pdf, docx, xlsx or pptx can be digitally signed");
					LogSystem.Warn("Only documents of type pdf, docx, xlsx or pptx can be digitally signed");
				}
				if (dlgCancel != null)
				{
					dlgCancel();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return flag;
		}

		internal void SetFileType(FileType _fileType)
		{
			base.FileType = _fileType;
		}

		internal bool VerifyServiceSignProcessorIsRunning()
		{
			bool result = false;
			try
			{
				LogSystem.Debug("VerifyServiceSignProcessorIsRunning.1");
				string exeSignPath = Utils.AppFilePathSignService();
				if (File.Exists(exeSignPath))
				{
					if (IsProcessOpen("EMR.SignProcessor"))
					{
						LogSystem.Debug("VerifyServiceSignProcessorIsRunning.2");
						result = true;
					}
					else
					{
						LogSystem.Debug("VerifyServiceSignProcessorIsRunning.3");
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => exeSignPath), exeSignPath));
						ProcessStartInfo processStartInfo = new ProcessStartInfo();
						processStartInfo.FileName = exeSignPath;
						try
						{
							Process.Start(processStartInfo);
							LogSystem.Debug("VerifyServiceSignProcessorIsRunning.4");
							Thread.Sleep(500);
							result = true;
							LogSystem.Debug("VerifyServiceSignProcessorIsRunning.5");
						}
						catch (Exception ex)
						{
							LogSystem.Warn(ex);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
			return result;
		}

		private bool IsProcessOpen(string name)
		{
			Process[] processes = Process.GetProcesses();
			Process[] array = processes;
			foreach (Process process in array)
			{
				if (process.ProcessName == name || process.ProcessName == string.Format("{0}.exe", name) || process.ProcessName == string.Format("{0} (32 bit)", name) || process.ProcessName == string.Format("{0}.exe (32 bit)", name))
				{
					return true;
				}
			}
			return false;
		}

		private bool VerifyDataPreCallApi(UsbSignCreateTDO doc)
		{
			return doc != null && doc.OriginalVersion != null && doc.TreatmentCode != null;
		}

		private string GetBase64FileData(string outFile)
		{
			string text = "";
			MemoryStream memoryStream = new MemoryStream();
			if (!string.IsNullOrEmpty(outFile))
			{
				using (FileStream fileStream = new FileStream(outFile, FileMode.Open, FileAccess.Read))
				{
					byte[] buffer = new byte[fileStream.Length];
					fileStream.Read(buffer, 0, (int)fileStream.Length);
					memoryStream.Write(buffer, 0, (int)fileStream.Length);
				}
			}
			memoryStream.Position = 0L;
			return Convert.ToBase64String(memoryStream.ToArray());
		}
	}
}
