using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Expected O, but got Unknown
			//IL_025c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0266: Expected O, but got Unknown
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0157: Expected O, but got Unknown
			bool result = false;
			UsbSignCreateTDO val = new UsbSignCreateTDO();
			((DocumentTDO)val).IsOutsideTreatment = base.inputADOWorking.IsOutsideTreatment == 1;
			((DocumentTDO)val).MediOrgCode = base.inputADOWorking.MediOrgCode;
			((DocumentTDO)val).Signs = (string.IsNullOrEmpty(base.inputADOWorking.BusinessCode) ? signTemps : null);
			if (string.IsNullOrEmpty(base.inputADOWorking.BusinessCode) && signStrategys != null && signStrategys.Count > 0)
			{
				if (((DocumentTDO)val).Signs == null)
				{
					((DocumentTDO)val).Signs = new List<SignTDO>();
				}
				((DocumentTDO)val).Signs.AddRange(signStrategys);
			}
			if (((DocumentTDO)val).Signs != null)
			{
				if (((DocumentTDO)val).Signs.FirstOrDefault() != null && !string.IsNullOrEmpty(((DocumentTDO)val).Signs[0].PatientCode))
				{
					base.param.Messages.Add("Bạn chưa đến lượt ký.");
					return result;
				}
				foreach (SignTDO sign in ((DocumentTDO)val).Signs)
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
				((DocumentTDO)val).FileType = (FileType)1;
			}
			else if (base.FileType == FileType.Json)
			{
				((DocumentTDO)val).FileType = (FileType)2;
			}
			else
			{
				((DocumentTDO)val).FileType = (FileType)0;
			}
			((DocumentTDO)val).MergeCode = mergeCode;
			val.RoomCode = base.inputADOWorking.RoomCode;
			val.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
			((DocumentTDO)val).DependentCode = base.inputADOWorking.DependentCode;
			((DocumentTDO)val).ParentDependentCode = base.inputADOWorking.ParentDependentCode;
			((DocumentTDO)val).IsSignParallel = base.IsSignParanel;
			((DocumentTDO)val).OriginalVersion = new VersionTDO();
			if (document != null && !string.IsNullOrEmpty(document.DocumentCode))
			{
				((DocumentTDO)val).DocumentCode = document.DocumentCode;
				((DocumentTDO)val).DocumentName = document.DocumentName;
				if (document.DocumentTypeId.HasValue && document.DocumentTypeId.Value > 0)
				{
					((DocumentTDO)val).DocumentTypeId = document.DocumentTypeId;
				}
				((DocumentTDO)val).OriginalVersion.DocumentCode = document.DocumentCode;
			}
			else
			{
				((DocumentTDO)val).DocumentName = (string.IsNullOrEmpty(documentName) ? ("Ký điện tử cho hồ sơ có mã " + treatmentCode + " ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) : documentName);
				if (documentTypeId.HasValue && documentTypeId.Value > 0)
				{
					((DocumentTDO)val).DocumentTypeId = documentTypeId;
				}
				((DocumentTDO)val).HisCode = hisCode;
				if (base.inputADOWorking.DocumentTime.HasValue && base.inputADOWorking.DocumentTime.Value != DateTime.MinValue)
				{
					((DocumentTDO)val).DocumentTime = DateTimeConvert.SystemDateTimeToTimeNumber(base.inputADOWorking.DocumentTime);
				}
			}
			if (!string.IsNullOrEmpty(base.inputADOWorking.DocumentGroupCode))
			{
				EMR_DOCUMENT_GROUP byCode = new EmrDocumentGroup().GetByCode(base.inputADOWorking.DocumentGroupCode);
				((DocumentTDO)val).DocumentGroupId = ((byCode != null) ? new long?(byCode.ID) : ((long?)null));
			}
			((DocumentTDO)val).TreatmentCode = treatmentCode;
			((DocumentTDO)val).OriginalVersion.Base64Data = GetBase64OriginalFileData;
			val.IsFinishSign = !isMultiSign;
			val.IsSigning = isMultiSign;
			((DocumentTDO)val).BusinessCode = base.inputADOWorking.BusinessCode;
			((DocumentTDO)val).HisOrder = base.inputADOWorking.HisOrder;
			if (base.inputADOWorking.PaperSizeDefault != null)
			{
				((DocumentTDO)val).PaperName = base.inputADOWorking.PaperSizeDefault.PaperName;
				if (string.IsNullOrEmpty(((DocumentTDO)val).PaperName))
				{
					((DocumentTDO)val).PaperName = base.inputADOWorking.PaperSizeDefault.Kind.ToString();
				}
				((DocumentTDO)val).Width = base.inputADOWorking.PaperSizeDefault.Width;
				((DocumentTDO)val).Height = base.inputADOWorking.PaperSizeDefault.Height;
				((DocumentTDO)val).RawKind = base.inputADOWorking.PaperSizeDefault.RawKind;
			}
			if (!VerifyDataPreCallApi(val))
			{
				base.param.Messages.Add(MessageUitl.GetMessage("DuLieuKhongHopLe"));
				MessageManager.Show(base.param, false);
				return false;
			}
			CommonParam commonParam = new CommonParam();
			UsbSignCreateTDO val2 = new EmrDocument(commonParam).CreateAndSignUsb(base.TokenCode, val);
			if (val2 != null)
			{
				result = true;
				document.DocumentCode = ((DocumentTDO)val2).DocumentCode;
				document.DocumentName = ((DocumentTDO)val2).DocumentName;
				document.DocumentTypeId = ((DocumentTDO)val2).DocumentTypeId;
				document.TreatmentCode = ((DocumentTDO)val2).TreatmentCode;
				document.DependentCode = ((DocumentTDO)val2).DependentCode;
				document.ParentDependentCode = ((DocumentTDO)val2).ParentDependentCode;
				document.PaperName = ((DocumentTDO)val2).PaperName;
				document.Width = ((DocumentTDO)val2).Width;
				document.Height = ((DocumentTDO)val2).Height;
				document.RawKind = ((DocumentTDO)val2).RawKind;
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
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			bool result = false;
			try
			{
				EmrSignUsbSDO val = new EmrSignUsbSDO();
				V_EMR_DOCUMENT viewByCode = new EmrDocument().GetViewByCode(document.DocumentCode);
				val.EmrDocumentId = viewByCode.ID;
				EMR_SIGN val2 = ((_signSelected != null) ? _signSelected : new EmrSign().GetSignDocumentFirst(viewByCode, base.Signer, base.Treatment, true));
				val.EmrSignId = val2.ID;
				val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
				val.Base64FileData = base64SignedFileData;
				val.IsFinishSign = !isMultiSign;
				val.IsSigning = isMultiSign;
				val.RoomCode = base.inputADOWorking.RoomCode;
				val.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
				val.Description = signReason;
				CommonParam commonParam = new CommonParam();
				EmrSignResultSDO val3 = new EmrDocument(commonParam).SignUsb(base.TokenCode, val);
				if (val3 != null)
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
			//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03be: Expected O, but got Unknown
			bool flag = false;
			try
			{
				LogSystem.Info("SignFileWithUSBTokenTSAWithCallService.1");
				string text = "";
				string text2 = Utils.GenerateTempFileWithin();
				TimestampConfig timestampConfig = new TimestampConfig();
				timestampConfig.UseTimestamp = GlobalStore.IsUseTimespan;
				timestampConfig.TsaUrl = "http://ca.gov.vn/tsa";
				DisplayConfig displayConfig = new DisplayConfig
				{
					CoorXRectangle = x,
					CoorYRectangle = y,
					NumberPageSign = pageNumberCurrent,
					MaxPageSign = totalPageNumber,
					Location = ((displayConfigParam != null && !string.IsNullOrEmpty(displayConfigParam.Location)) ? displayConfigParam.Location : ((base.Signer != null) ? (base.Signer.DEPARTMENT_NAME + "|" + base.Signer.TITLE) : ""))
				};
				if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
				{
					displayConfig.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
					displayConfig.BImage = base.Signer.SIGN_IMAGE;
				}
				if (displayConfigParam != null)
				{
					if (displayConfigParam.WidthRectangle.HasValue)
					{
						displayConfig.WidthRectangle = displayConfigParam.WidthRectangle.Value;
					}
					if (displayConfigParam.HeightRectangle.HasValue)
					{
						displayConfig.HeightRectangle = displayConfigParam.HeightRectangle.Value;
					}
					if (displayConfigParam.SizeFont.HasValue)
					{
						displayConfig.SizeFont = displayConfigParam.SizeFont.Value;
					}
					if (displayConfigParam.TextPosition.HasValue)
					{
						displayConfig.TextPosition = (Constans.TEXT_POSITON)displayConfigParam.TextPosition.Value;
					}
					if (displayConfigParam.TypeDisplay.HasValue)
					{
						displayConfig.TypeDisplay = displayConfigParam.TypeDisplay.Value;
					}
					if (displayConfigParam.IsDisplaySignature.HasValue)
					{
						displayConfig.IsDisplaySignature = displayConfigParam.IsDisplaySignature.Value;
					}
					if (!string.IsNullOrEmpty(displayConfigParam.FormatRectangleText))
					{
						displayConfig.FormatRectangleText = displayConfigParam.FormatRectangleText;
					}
					if (displayConfigParam.Titles != null)
					{
						displayConfig.Titles = displayConfigParam.Titles;
					}
					if (displayConfig.TextFormat == null)
					{
						displayConfig.TextFormat = new FontConfig();
					}
					if (displayConfigParam.Alignment.HasValue)
					{
						displayConfig.TextFormat.Alignment = (ALIGNMENT_OPTION)displayConfigParam.Alignment.Value;
					}
					if (displayConfigParam.IsBold.HasValue)
					{
						displayConfig.TextFormat.IsBold = displayConfigParam.IsBold.Value;
					}
					if (displayConfigParam.IsItalic.HasValue)
					{
						displayConfig.TextFormat.IsItalic = displayConfigParam.IsItalic.Value;
					}
					if (displayConfigParam.IsUnderlined.HasValue)
					{
						displayConfig.TextFormat.IsUnderlined = displayConfigParam.IsUnderlined.Value;
					}
					if (!string.IsNullOrEmpty(displayConfigParam.FontName))
					{
						displayConfig.TextFormat.FontName = displayConfigParam.FontName;
					}
				}
				displayConfig.PageSize = null;
				if (GlobalStore.EMR_SIGN_SIGN_DESCRIPTION_INFO == "1")
				{
					displayConfig.IsDisplaySignNote = true;
				}
				WcfSignDCO val = new WcfSignDCO();
				val.IsUseTimespan = GlobalStore.IsUseTimespan;
				val.OutputFile = StringCompressorParse.CompressString(text2);
				val.PIN = GlobalStore.PIN;
				val.SignReason = StringCompressorParse.CompressString(signReason);
				val.SourceFile = StringCompressorParse.CompressString(src);
				val.HwndParent = ActiveTopFormHandle();
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				if (emrConfigs != null && emrConfigs.Count > 0)
				{
					IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGNING_OPTION");
					EMR_CONFIG val2 = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
					if (val2 != null)
					{
						string emrSigningOption = ((!string.IsNullOrEmpty(val2.VALUE)) ? val2.VALUE : val2.DEFAULT_VALUE);
						val.EmrSigningOption = emrSigningOption;
					}
				}
				if (val.EmrSigningOption == "2")
				{
					if (base.Signer != null && base.Signer.SIGN_IMAGE != null && string.IsNullOrEmpty(base.Signer.PCA_SERIAL))
					{
						displayConfig.Contact = ((base.Signer != null) ? base.Signer.USERNAME : "");
					}
					else
					{
						val.EmrSigningOption = "";
					}
				}
				if (base.Signer != null && base.Signer.SIGNATURE_DISPLAY_TYPE.HasValue)
				{
					long value = base.Signer.SIGNATURE_DISPLAY_TYPE.Value;
					long num = value;
					long num2 = num;
					if ((ulong)num2 <= 3uL)
					{
						switch (num2)
						{
						case 0L:
							displayConfig.IsDisplaySignature = false;
							break;
						case 1L:
							displayConfig.TypeDisplay = Constans.DISPLAY_RECTANGLE_TEXT;
							break;
						case 2L:
							if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
							{
								displayConfig.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP;
							}
							else
							{
								displayConfig.IsDisplaySignature = false;
							}
							break;
						case 3L:
							if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
							{
								displayConfig.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
							}
							else
							{
								displayConfig.IsDisplaySignature = false;
							}
							break;
						}
					}
				}
				if (base.Signer != null && base.Signer.SIGNALTURE_IMAGE_WIDTH.HasValue)
				{
					decimal? sIGNALTURE_IMAGE_WIDTH = base.Signer.SIGNALTURE_IMAGE_WIDTH;
					if ((sIGNALTURE_IMAGE_WIDTH.GetValueOrDefault() > default(decimal)) & sIGNALTURE_IMAGE_WIDTH.HasValue)
					{
						displayConfig.SignaltureImageWidth = (float)base.Signer.SIGNALTURE_IMAGE_WIDTH.Value;
					}
				}
				val.DisplayConfig = StringCompressorParse.CompressString(JsonConvert.SerializeObject((object)displayConfig));
				string data = JsonConvert.SerializeObject((object)val);
				SignProcessorClient signProcessorClient = new SignProcessorClient();
				WcfSignResultDCO val3 = signProcessorClient.SignExecute(data);
				if (val3 != null)
				{
					flag = val3.Success;
					outFileName = (val3.Success ? StringCompressorParse.DecompressString(val3.OutputFile) : "");
					text = StringCompressorParse.DecompressString(val3.Message);
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
						EMR_CONFIG val = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
						if (val != null)
						{
							string text = ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : val.DEFAULT_VALUE);
							if (text == "2")
							{
								flag2 = true;
							}
						}
					}
					DisplayConfig displayConfig = new DisplayConfig
					{
						CoorXRectangle = x,
						CoorYRectangle = y,
						NumberPageSign = pageNumberCurrent,
						MaxPageSign = totalPageNumber,
						Location = ((displayConfigParam != null && !string.IsNullOrEmpty(displayConfigParam.Location)) ? displayConfigParam.Location : ((base.Signer != null) ? (base.Signer.DEPARTMENT_NAME + "|" + base.Signer.TITLE) : ""))
					};
					if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
					{
						displayConfig.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
						displayConfig.BImage = base.Signer.SIGN_IMAGE;
					}
					if (displayConfigParam != null)
					{
						if (displayConfigParam.WidthRectangle.HasValue)
						{
							displayConfig.WidthRectangle = displayConfigParam.WidthRectangle.Value;
						}
						if (displayConfigParam.HeightRectangle.HasValue)
						{
							displayConfig.HeightRectangle = displayConfigParam.HeightRectangle.Value;
						}
						if (displayConfigParam.SizeFont.HasValue)
						{
							displayConfig.SizeFont = displayConfigParam.SizeFont.Value;
						}
						if (displayConfigParam.TextPosition.HasValue)
						{
							displayConfig.TextPosition = (Constans.TEXT_POSITON)displayConfigParam.TextPosition.Value;
						}
						if (displayConfigParam.TypeDisplay.HasValue)
						{
							displayConfig.TypeDisplay = displayConfigParam.TypeDisplay.Value;
						}
						if (displayConfigParam.IsDisplaySignature.HasValue)
						{
							displayConfig.IsDisplaySignature = displayConfigParam.IsDisplaySignature.Value;
						}
						if (!string.IsNullOrEmpty(displayConfigParam.FormatRectangleText))
						{
							displayConfig.FormatRectangleText = displayConfigParam.FormatRectangleText;
						}
						if (displayConfigParam.Titles != null)
						{
							displayConfig.Titles = displayConfigParam.Titles;
						}
						if (displayConfig.TextFormat == null)
						{
							displayConfig.TextFormat = new FontConfig();
						}
						if (displayConfigParam.Alignment.HasValue)
						{
							displayConfig.TextFormat.Alignment = (ALIGNMENT_OPTION)displayConfigParam.Alignment.Value;
						}
						if (displayConfigParam.IsBold.HasValue)
						{
							displayConfig.TextFormat.IsBold = displayConfigParam.IsBold.Value;
						}
						if (displayConfigParam.IsItalic.HasValue)
						{
							displayConfig.TextFormat.IsItalic = displayConfigParam.IsItalic.Value;
						}
						if (displayConfigParam.IsUnderlined.HasValue)
						{
							displayConfig.TextFormat.IsUnderlined = displayConfigParam.IsUnderlined.Value;
						}
						if (!string.IsNullOrEmpty(displayConfigParam.FontName))
						{
							displayConfig.TextFormat.FontName = displayConfigParam.FontName;
						}
					}
					if (base.Signer != null && base.Signer.SIGNATURE_DISPLAY_TYPE.HasValue)
					{
						long value = base.Signer.SIGNATURE_DISPLAY_TYPE.Value;
						long num = value;
						long num2 = num;
						if ((ulong)num2 <= 3uL)
						{
							switch (num2)
							{
							case 0L:
								displayConfig.IsDisplaySignature = false;
								break;
							case 1L:
								displayConfig.TypeDisplay = Constans.DISPLAY_RECTANGLE_TEXT;
								break;
							case 2L:
								if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
								{
									displayConfig.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP;
								}
								else
								{
									displayConfig.IsDisplaySignature = false;
								}
								break;
							case 3L:
								if (base.Signer != null && base.Signer.SIGN_IMAGE != null)
								{
									displayConfig.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
								}
								else
								{
									displayConfig.IsDisplaySignature = false;
								}
								break;
							}
						}
					}
					if (base.Signer != null && base.Signer.SIGNALTURE_IMAGE_WIDTH.HasValue)
					{
						decimal? sIGNALTURE_IMAGE_WIDTH = base.Signer.SIGNALTURE_IMAGE_WIDTH;
						if ((sIGNALTURE_IMAGE_WIDTH.GetValueOrDefault() > default(decimal)) & sIGNALTURE_IMAGE_WIDTH.HasValue)
						{
							displayConfig.SignaltureImageWidth = (float)base.Signer.SIGNALTURE_IMAGE_WIDTH.Value;
						}
					}
					string text2 = Utils.GenerateTempFileWithin();
					if (GlobalStore.EMR_SIGN_SIGN_DESCRIPTION_INFO == "1")
					{
						displayConfig.IsDisplaySignNote = true;
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
										flag = signPdfFile.SignJson(byDialog, src, outStream, signReason, "", displayConfig, null, ref errMessage, GlobalStore.PIN);
									}
								}
								else
								{
									flag = signPdfFile.SignPDF(byDialog, src, text2, signReason, "", timestampConfig, displayConfig, null, ref errMessage, GlobalStore.PIN);
								}
							}
							else
							{
								flag = signPdfFile.SignPDF(byDialog, stream, text2, signReason, "", timestampConfig, displayConfig, null, ref errMessage);
							}
							if (flag)
							{
								outFileName = text2;
							}
						}
					}
					else if (flag2 && base.Signer != null && base.Signer.SIGN_IMAGE != null && string.IsNullOrEmpty(base.Signer.PCA_SERIAL))
					{
						displayConfig.Contact = ((base.Signer != null) ? base.Signer.USERNAME : "");
						flag = (string.IsNullOrEmpty(src) ? signPdfFile.SignPDF(null, stream, text2, signReason, "", null, displayConfig, null, ref errMessage, flag2) : signPdfFile.SignPDF(null, src, text2, signReason, "", null, displayConfig, null, ref errMessage, GlobalStore.PIN, flag2));
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
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => exeSignPath)), (object)exeSignPath));
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
			foreach (Process process in processes)
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
			return true && doc != null && ((DocumentTDO)doc).OriginalVersion != null && ((DocumentTDO)doc).TreatmentCode != null;
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
