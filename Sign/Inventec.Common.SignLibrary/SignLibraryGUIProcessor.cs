using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.Pdf;
using DevExpress.XtraEditors;
using DevExpress.XtraPdfViewer;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignFile;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.CacheClient;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;
using Inventec.Common.SignLibrary.License;
using Inventec.Common.SignLibrary.Popup;
using Inventec.Common.SignToolViewer.Integrate;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary
{
	public class SignLibraryGUIProcessor
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass43_0
		{
			public bool isPrintNow;

			public bool isPrintPreview;

			public bool isSignOnlyWithHasAutoPosition;

			public DocumentSignedResultDTO rsData;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass43_1
		{
			public string outputFile;

			public string inputFileWork;

			public bool success;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass44_1
		{
			public string outputFile;

			public string inputFileWork;

			public bool success;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass46_2
		{
			public string outputFile;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass46_4
		{
			public bool? vOptionSign;

			public SignerConfigDTO scf;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass42
		{
			public _003C_003Ec__DisplayClass43_0 CS_0024_003C_003E8__locals101;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass44
		{
			public _003C_003Ec__DisplayClass42 CS_0024_003C_003E8__locals43;

			public _003C_003Ec__DisplayClass43_1 CS_0024_003C_003E8__locals106;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass51
		{
			public DocumentSignedResultDTO rsData;

			public InputADO inputADO;

			public bool isPrintNow;

			public bool isPrintPreview;

			public bool isSignOnlyWithHasAutoPosition;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass53
		{
			public _003C_003Ec__DisplayClass51 CS_0024_003C_003E8__locals52;

			public _003C_003Ec__DisplayClass44_1 CS_0024_003C_003E8__locals44;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass61
		{
			public DocumentSignedResultDTO rsData;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass63
		{
			public _003C_003Ec__DisplayClass61 CS_0024_003C_003E8__locals62;

			public string inputFileWork;

			public bool success;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass65
		{
			public _003C_003Ec__DisplayClass63 CS_0024_003C_003E8__locals64;

			public _003C_003Ec__DisplayClass61 CS_0024_003C_003E8__locals62;

			public _003C_003Ec__DisplayClass46_2 CS_0024_003C_003E8__locals19;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass68
		{
			public _003C_003Ec__DisplayClass65 CS_0024_003C_003E8__locals66;

			public _003C_003Ec__DisplayClass63 CS_0024_003C_003E8__locals64;

			public _003C_003Ec__DisplayClass61 CS_0024_003C_003E8__locals62;

			public SignPositionADO nSp;

			public bool _003CSignWithListKeyAndUser_003Eb__5d(SignerConfigDTO o)
			{
				return o.NumOrder == VerifySign.GetNumOderByCommentText(nSp.Text);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6a
		{
			public _003C_003Ec__DisplayClass68 CS_0024_003C_003E8__locals69;

			public _003C_003Ec__DisplayClass65 CS_0024_003C_003E8__locals66;

			public _003C_003Ec__DisplayClass63 CS_0024_003C_003E8__locals64;

			public _003C_003Ec__DisplayClass61 CS_0024_003C_003E8__locals62;

			public _003C_003Ec__DisplayClass46_4 CS_0024_003C_003E8__locals18;
		}

		private string outputSignedFileResult;

		private bool iSPrintNow;

		private bool iSPrintPreview;

		private short printNumberCopies = 1;

		private InputADO inputADOWorking;

		private SignToken signToken = new SignToken();

		private PageSettings currentPageSettings;

		private PdfPrinterSettings pdfPrinterSettings = null;

		private PrinterSettings printerSettings;

		private int Width_;

		private int Height_;

		public SignLibraryGUIProcessor()
		{
			try
			{
				iSPrintNow = false;
				outputSignedFileResult = "";
				printNumberCopies = 1;
				inputADOWorking = null;
				signToken = new SignToken();
				ProcessMemoryUsageuser();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool ValidParam(InputADO inputADO)
		{
			bool isShowSignedFile = true;
			string base64FileGigned = "";
			V_EMR_DOCUMENT documentData = new V_EMR_DOCUMENT();
			return ValidParam(inputADO, isShowSignedFile, ref base64FileGigned, ref documentData);
		}

		private bool ValidParamCommon(InputADO inputADO)
		{
			bool result = false;
			try
			{
				if (inputADO == null)
				{
					MessageBox.Show(MessageUitl.GetMessage("TinhNangChiDanhChoBenhAnDienTu"));
					LogSystem.Warn(MessageUitl.GetMessage("TinhNangChiDanhChoBenhAnDienTu") + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
					return result;
				}
				if (inputADO.Treatment == null || string.IsNullOrEmpty(inputADO.Treatment.TREATMENT_CODE))
				{
					MessageBox.Show(string.Format(MessageUitl.GetMessage("TinhNangChiDanhChoBenhAnDienTuCoThamSo"), (inputADO.Treatment != null) ? inputADO.Treatment.TREATMENT_CODE : "", inputADO.DocumentName));
					LogSystem.Warn(string.Format(MessageUitl.GetMessage("TinhNangChiDanhChoBenhAnDienTuCoThamSo"), (inputADO.Treatment != null) ? inputADO.Treatment.TREATMENT_CODE : "", inputADO.DocumentName) + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
					return result;
				}
				InitUri();
				InitParam(inputADO.DTI);
				try
				{
					if (!string.IsNullOrEmpty(inputADO.HisUriUpdateSignedState))
					{
						string[] array = inputADO.HisUriUpdateSignedState.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
						if (array.Count() > 1)
						{
							GlobalStore.INTERGRATE_SYS_BASE_URI = array[0];
							GlobalStore.INTERGRATE_SYS_API = array[1];
						}
					}
					else
					{
						GlobalStore.INTERGRATE_SYS_BASE_URI = (ConfigurationManager.AppSettings["INTERGRATE_SYS_BASE_URI"] ?? "").ToString();
						GlobalStore.INTERGRATE_SYS_API = (ConfigurationManager.AppSettings["INTERGRATE_SYS_API"] ?? "").ToString();
					}
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				result = CheckLogin(inputADO) && Verify.VerifyTreatmentCode(inputADO, ref signToken);
				if (result)
				{
					InitConfig(inputADO);
				}
				inputADOWorking = inputADO;
			}
			catch (Exception ex2)
			{
				result = false;
				LogSystem.Warn(ex2);
			}
			return result;
		}

		private void InitConfig(InputADO inputADO)
		{
			try
			{
				EMR_SIGNER signerData = GetSignerData();
				if (inputADO.DisplayConfigDTO == null)
				{
					inputADO.DisplayConfigDTO = new DisplayConfigDTO();
				}
				if (signerData != null)
				{
					List<string> list = new List<string>();
					list.Add(signerData.TITLE);
					inputADO.DisplayConfigDTO.Titles = list.ToArray();
					if (!string.IsNullOrEmpty(inputADO.BusinessCode) && (string.IsNullOrEmpty(inputADO.RoomCode) || string.IsNullOrEmpty(inputADO.RoomTypeCode)))
					{
						EmrSignerFlow emrSignerFlow = new EmrSignerFlow();
						List<V_EMR_SIGNER_FLOW> view = emrSignerFlow.GetView(new EmrSignerFlowViewFilter
						{
							IS_ACTIVE = 1,
							BUSINESS_CODE__EXACT = inputADO.BusinessCode,
							LOGINNAME__EXACT = signerData.LOGINNAME
						});
						if (view != null && view.Count > 0)
						{
							inputADO.RoomCode = view.FirstOrDefault().ROOM_CODE;
							inputADO.RoomTypeCode = view.FirstOrDefault().ROOM_TYPE_CODE;
						}
					}
				}
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				if (emrConfigs == null || emrConfigs.Count <= 0)
				{
					return;
				}
				IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGN_DISPLAY_OPTION");
				EMR_CONFIG eMR_CONFIG = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
				if (eMR_CONFIG != null)
				{
					if (inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.TypeDisplay.HasValue)
					{
						LogSystem.Debug("InitConfig___His truyen vao loai hien thi chu ky so==>Emr su dung luon & bo qua cau hinh ben emr(neu co)");
					}
					else
					{
						string text = ((!string.IsNullOrEmpty(eMR_CONFIG.VALUE)) ? eMR_CONFIG.VALUE : eMR_CONFIG.DEFAULT_VALUE);
						if (string.IsNullOrEmpty(text))
						{
							LogSystem.Debug("InitConfig___Co thiet lap cau hinh hien thi chu ky so nhung gia tri dang null ==> bo qua cau hinh khong xu ly gi");
							if (signerData.SIGN_IMAGE == null || signerData.SIGN_IMAGE.Length == 0)
							{
								if (inputADO.DisplayConfigDTO == null)
								{
									inputADO.DisplayConfigDTO = new DisplayConfigDTO();
								}
								inputADO.DisplayConfigDTO.TypeDisplay = Constans.DISPLAY_RECTANGLE_TEXT;
								LogSystem.Debug("InitConfig___Truong hop khong co gia tri cau hinh EMR.EMR_SIGN.SIGN_DISPLAY_OPTION & nguoi ky khong co anh chu ky ==> tu dong chuyen doi loai hien thi chu ky thanh dang chi hien thi text: DisplayConfigDTO.TypeDisplay =" + Constans.DISPLAY_RECTANGLE_TEXT);
							}
						}
						else
						{
							if (inputADO.DisplayConfigDTO == null)
							{
								inputADO.DisplayConfigDTO = new DisplayConfigDTO();
							}
							switch (text)
							{
							case "1":
								inputADO.DisplayConfigDTO.TypeDisplay = Constans.DISPLAY_RECTANGLE_TEXT;
								break;
							case "2":
								inputADO.DisplayConfigDTO.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP;
								break;
							case "3":
								inputADO.DisplayConfigDTO.IsDisplaySignature = false;
								break;
							default:
								inputADO.DisplayConfigDTO.TypeDisplay = Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
								break;
							}
						}
					}
				}
				IEnumerable<EMR_CONFIG> enumerable2 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.PRINT_OPTION");
				EMR_CONFIG eMR_CONFIG2 = ((enumerable2 != null) ? enumerable2.FirstOrDefault() : null);
				if (eMR_CONFIG2 != null)
				{
					string text2 = ((!string.IsNullOrEmpty(eMR_CONFIG2.VALUE)) ? eMR_CONFIG2.VALUE : eMR_CONFIG2.DEFAULT_VALUE);
					if (text2 == "1")
					{
						inputADO.IsPrint = true;
					}
				}
				IEnumerable<EMR_CONFIG> enumerable3 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SPLIT_PDF_KEY");
				EMR_CONFIG eMR_CONFIG3 = ((enumerable3 != null) ? enumerable3.FirstOrDefault() : null);
				if (eMR_CONFIG3 != null)
				{
					string text3 = ((!string.IsNullOrEmpty(eMR_CONFIG3.VALUE)) ? eMR_CONFIG3.VALUE : eMR_CONFIG3.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text3))
					{
						string[] array = text3.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Count() == 2)
						{
							GlobalStore.SplitPdfHeaderKey = array[0];
							GlobalStore.SplitPdfContentKey = array[1];
						}
					}
				}
				IEnumerable<EMR_CONFIG> enumerable4 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.IS_NOT_SHOWING_SIGN_INFORMATION");
				EMR_CONFIG eMR_CONFIG4 = ((enumerable4 != null) ? enumerable4.FirstOrDefault() : null);
				if (eMR_CONFIG4 != null)
				{
					string text4 = ((!string.IsNullOrEmpty(eMR_CONFIG4.VALUE)) ? eMR_CONFIG4.VALUE : eMR_CONFIG4.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text4))
					{
						inputADO.IsPrintOnlyContent = text4 == "1";
					}
				}
				IEnumerable<EMR_CONFIG> enumerable5 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGNATURE_APPEARANCE_OPTION");
				EMR_CONFIG eMR_CONFIG5 = ((enumerable5 != null) ? enumerable5.FirstOrDefault() : null);
				if (eMR_CONFIG5 != null)
				{
					try
					{
						string text5 = ((!string.IsNullOrEmpty(eMR_CONFIG5.VALUE)) ? eMR_CONFIG5.VALUE : eMR_CONFIG5.DEFAULT_VALUE);
						if (inputADO.DisplayConfigDTO == null)
						{
							inputADO.DisplayConfigDTO = new DisplayConfigDTO();
						}
						string[] array2 = text5.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
						if (array2 != null && array2.Count() > 0)
						{
							string[] array3 = array2;
							string[] array4 = array3;
							foreach (string text6 in array4)
							{
								if (string.IsNullOrEmpty(text6))
								{
									continue;
								}
								string[] array5 = text6.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
								if (array5 == null || array5.Count() <= 1 || string.IsNullOrEmpty(array5[1]))
								{
									continue;
								}
								switch (array5[0].ToLower())
								{
								case "p":
								{
									int num6 = TypeConvertParse.ToInt32(array5[1]);
									if (num6 >= 0 && !inputADO.DisplayConfigDTO.TextPosition.HasValue)
									{
										inputADO.DisplayConfigDTO.TextPosition = num6;
									}
									break;
								}
								case "f":
								{
									int num5 = TypeConvertParse.ToInt32(array5[1]);
									if (num5 > 0 && !inputADO.DisplayConfigDTO.SizeFont.HasValue)
									{
										inputADO.DisplayConfigDTO.SizeFont = num5;
									}
									break;
								}
								case "w":
								{
									int num3 = TypeConvertParse.ToInt32(array5[1]);
									if (num3 > 0 && !inputADO.DisplayConfigDTO.WidthRectangle.HasValue)
									{
										inputADO.DisplayConfigDTO.WidthRectangle = num3;
									}
									break;
								}
								case "h":
								{
									int num4 = TypeConvertParse.ToInt32(array5[1]);
									if (num4 > 0 && !inputADO.DisplayConfigDTO.HeightRectangle.HasValue)
									{
										inputADO.DisplayConfigDTO.HeightRectangle = num4;
									}
									break;
								}
								case "a":
								{
									int num2 = TypeConvertParse.ToInt32(array5[1]);
									if (num2 > 0)
									{
										inputADO.DisplayConfigDTO.Alignment = num2;
									}
									break;
								}
								case "fs":
								{
									string text8 = array5[1].ToUpper();
									if (!string.IsNullOrEmpty(text8))
									{
										if (text8.Contains("B") && !inputADO.DisplayConfigDTO.IsBold.HasValue)
										{
											inputADO.DisplayConfigDTO.IsBold = true;
										}
										if (text8.Contains("I") && !inputADO.DisplayConfigDTO.IsItalic.HasValue)
										{
											inputADO.DisplayConfigDTO.IsItalic = true;
										}
										if (text8.Contains("U") && !inputADO.DisplayConfigDTO.IsUnderlined.HasValue)
										{
											inputADO.DisplayConfigDTO.IsUnderlined = true;
										}
									}
									break;
								}
								case "fn":
								{
									string text7 = array5[1];
									if (!string.IsNullOrEmpty(text7) && string.IsNullOrEmpty(inputADO.DisplayConfigDTO.FontName))
									{
										inputADO.DisplayConfigDTO.FontName = text7;
									}
									break;
								}
								}
							}
						}
					}
					catch (Exception ex)
					{
						LogSystem.Warn(ex);
					}
				}
				EMR_CONFIG eMR_CONFIG6 = null;
				IEnumerable<EMR_CONFIG> enumerable6 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "LICENSE_KEY__APOSE");
				eMR_CONFIG6 = ((enumerable6 != null) ? enumerable6.FirstOrDefault() : null);
				string text9 = ((eMR_CONFIG6 == null) ? "" : ((!string.IsNullOrEmpty(eMR_CONFIG6.VALUE)) ? eMR_CONFIG6.VALUE : eMR_CONFIG6.DEFAULT_VALUE));
				if (!string.IsNullOrEmpty(text9))
				{
					Licenses.Aspose_Key = text9;
				}
				EMR_CONFIG eMR_CONFIG7 = null;
				IEnumerable<EMR_CONFIG> enumerable7 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.DOCUMENT.PRINT_USING_WARTERMARK.OPTION");
				eMR_CONFIG7 = ((enumerable7 != null) ? enumerable7.FirstOrDefault() : null);
				string text10 = ((eMR_CONFIG7 == null) ? "" : ((!string.IsNullOrEmpty(eMR_CONFIG7.VALUE)) ? eMR_CONFIG7.VALUE : eMR_CONFIG7.DEFAULT_VALUE));
				if (!inputADO.IsShowWatermark.HasValue)
				{
					if (!string.IsNullOrEmpty(text10))
					{
						GlobalStore.PrintUsingWaterMark = text10 == "1";
					}
				}
				else
				{
					GlobalStore.PrintUsingWaterMark = inputADO.IsShowWatermark.Value;
				}
				IEnumerable<EMR_CONFIG> enumerable8 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGN_INFO_DISPLAY_OPTION");
				EMR_CONFIG eMR_CONFIG8 = ((enumerable8 != null) ? enumerable8.FirstOrDefault() : null);
				if (eMR_CONFIG8 != null)
				{
					string text11 = ((!string.IsNullOrEmpty(eMR_CONFIG8.VALUE)) ? eMR_CONFIG8.VALUE : eMR_CONFIG8.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text11))
					{
						switch (text11)
						{
						case "1":
							inputADO.DisplayConfigDTO.FormatRectangleText = Constans.SIGN_TEXT_FORMAT_3_1;
							break;
						case "2":
							inputADO.DisplayConfigDTO.FormatRectangleText = Constans.SIGN_TEXT_FORMAT_3__NO_DATE;
							break;
						case "3":
							inputADO.DisplayConfigDTO.FormatRectangleText = Constans.SIGN_TEXT_FORMAT_3__NO_TITLE;
							break;
						default:
							inputADO.DisplayConfigDTO.FormatRectangleText = text11;
							break;
						}
					}
				}
				IEnumerable<EMR_CONFIG> enumerable9 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.INTERGRATE_SYS_BASE_URI");
				EMR_CONFIG eMR_CONFIG9 = ((enumerable9 != null) ? enumerable9.FirstOrDefault() : null);
				if (eMR_CONFIG9 != null)
				{
					string text12 = ((!string.IsNullOrEmpty(eMR_CONFIG9.VALUE)) ? eMR_CONFIG9.VALUE : eMR_CONFIG9.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text12))
					{
						GlobalStore.INTERGRATE_SYS_BASE_URI = text12;
					}
				}
				IEnumerable<EMR_CONFIG> enumerable10 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.INTERGRATE_SYS_API");
				EMR_CONFIG eMR_CONFIG10 = ((enumerable10 != null) ? enumerable10.FirstOrDefault() : null);
				if (eMR_CONFIG10 != null)
				{
					string text13 = ((!string.IsNullOrEmpty(eMR_CONFIG10.VALUE)) ? eMR_CONFIG10.VALUE : eMR_CONFIG10.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text13))
					{
						GlobalStore.INTERGRATE_SYS_API = text13;
					}
				}
				IEnumerable<EMR_CONFIG> enumerable11 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.PRINT_LIBIARY_OPTION");
				EMR_CONFIG eMR_CONFIG11 = ((enumerable11 != null) ? enumerable11.FirstOrDefault() : null);
				if (eMR_CONFIG11 != null)
				{
					string text14 = ((!string.IsNullOrEmpty(eMR_CONFIG11.VALUE)) ? eMR_CONFIG11.VALUE : eMR_CONFIG11.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text14))
					{
						switch (text14)
						{
						case "1":
							GlobalStore.OptionPrintType = OptionPrintType.PdfAposeLib;
							break;
						case "2":
							GlobalStore.OptionPrintType = OptionPrintType.CallExeLib;
							break;
						case "0":
							GlobalStore.OptionPrintType = OptionPrintType.DevLib;
							break;
						default:
							GlobalStore.OptionPrintType = OptionPrintType.DevLib;
							break;
						}
					}
				}
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => GlobalStore.OptionPrintType), GlobalStore.OptionPrintType));
				IEnumerable<EMR_CONFIG> enumerable12 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.PLACE_SIGN_OPTION");
				EMR_CONFIG eMR_CONFIG12 = ((enumerable12 != null) ? enumerable12.FirstOrDefault() : null);
				if (eMR_CONFIG12 != null)
				{
					string text15 = ((!string.IsNullOrEmpty(eMR_CONFIG12.VALUE)) ? eMR_CONFIG12.VALUE : eMR_CONFIG12.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text15))
					{
						switch (text15)
						{
						case "1":
							inputADO.DisplayConfigDTO.Location = ((signerData != null) ? (signerData.DEPARTMENT_NAME + "|" + signerData.TITLE) : "");
							break;
						case "2":
							inputADO.DisplayConfigDTO.Location = ((signerData != null) ? (inputADO.DepartmentName + "|" + signerData.TITLE) : inputADO.DepartmentName);
							break;
						case "3":
							inputADO.DisplayConfigDTO.Location = ((!string.IsNullOrEmpty(inputADO.DepartmentName)) ? inputADO.DepartmentName : ((signerData != null) ? signerData.DEPARTMENT_NAME : "")) + ((signerData != null) ? ("|" + signerData.TITLE) : "");
							break;
						}
					}
				}
				IEnumerable<EMR_CONFIG> enumerable13 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_DOCUMENT.PATIENT_SIGN.OPTION");
				EMR_CONFIG eMR_CONFIG13 = ((enumerable13 != null) ? enumerable13.FirstOrDefault() : null);
				if (eMR_CONFIG13 != null)
				{
					string text16 = ((!string.IsNullOrEmpty(eMR_CONFIG13.VALUE)) ? eMR_CONFIG13.VALUE : eMR_CONFIG13.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text16))
					{
						GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION = text16;
					}
				}
				IEnumerable<EMR_CONFIG> enumerable14 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_DOCUMENT.PATIENT_SIGN_FIRST.OPTION");
				EMR_CONFIG eMR_CONFIG14 = ((enumerable14 != null) ? enumerable14.FirstOrDefault() : null);
				if (eMR_CONFIG14 != null)
				{
					string text17 = ((!string.IsNullOrEmpty(eMR_CONFIG14.VALUE)) ? eMR_CONFIG14.VALUE : eMR_CONFIG14.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text17))
					{
						GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION = text17;
					}
				}
				string value = CacheClientWorker.GetValue("SignTypeOption");
				IEnumerable<EMR_CONFIG> enumerable15 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "HIS.Desktop.Plugins.Library.EmrGenerate.SignType");
				EMR_CONFIG eMR_CONFIG15 = ((enumerable15 != null) ? enumerable15.FirstOrDefault() : null);
				if (eMR_CONFIG15 != null)
				{
					string text18 = ((!string.IsNullOrEmpty(eMR_CONFIG15.VALUE)) ? eMR_CONFIG15.VALUE : eMR_CONFIG15.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text18))
					{
						switch (text18)
						{
						case "1":
							inputADO.SignType = SignType.USB;
							break;
						case "2":
							inputADO.SignType = SignType.HMS;
							break;
						case "3":
							inputADO.SignType = SignType.OptionDefaultUsb;
							if (!string.IsNullOrEmpty(value))
							{
								inputADO.IsOptionSignType = value == "1";
							}
							break;
						case "4":
							inputADO.SignType = SignType.OptionDefaultHsm;
							if (!string.IsNullOrEmpty(value))
							{
								inputADO.IsOptionSignType = value == "1";
							}
							break;
						}
					}
				}
				IEnumerable<EMR_CONFIG> enumerable16 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.USING_USBTOKENDEVICE.OPTION");
				EMR_CONFIG eMR_CONFIG16 = ((enumerable16 != null) ? enumerable16.FirstOrDefault() : null);
				if (eMR_CONFIG16 != null)
				{
					string text19 = ((!string.IsNullOrEmpty(eMR_CONFIG16.VALUE)) ? eMR_CONFIG16.VALUE : eMR_CONFIG16.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text19))
					{
						GlobalStore.IsSignUsingUsbTokenDevice = text19 != "1";
					}
				}
				IEnumerable<EMR_CONFIG> enumerable17 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.PATIENT_SIGN.OPTION");
				EMR_CONFIG eMR_CONFIG17 = ((enumerable17 != null) ? enumerable17.FirstOrDefault() : null);
				if (eMR_CONFIG17 != null)
				{
					string text20 = ((!string.IsNullOrEmpty(eMR_CONFIG17.VALUE)) ? eMR_CONFIG17.VALUE : eMR_CONFIG16.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text20))
					{
						GlobalStore.EMR_SIGN_PATIENT_SIGN_OPTION = text20;
					}
				}
				IEnumerable<EMR_CONFIG> enumerable18 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGN_BOARD.OPTION");
				EMR_CONFIG eMR_CONFIG18 = ((enumerable18 != null) ? enumerable18.FirstOrDefault() : null);
				if (eMR_CONFIG18 != null)
				{
					string text21 = ((!string.IsNullOrEmpty(eMR_CONFIG18.VALUE)) ? eMR_CONFIG18.VALUE : eMR_CONFIG18.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text21))
					{
						GlobalStore.EMR_SIGN_BOARD__OPTION = text21;
					}
				}
				IEnumerable<EMR_CONFIG> enumerable19 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == EmrConfigKeys.EMR_EMR_SIGN_CONNECT_DEVICE_TYPE_OPTION);
				EMR_CONFIG eMR_CONFIG19 = ((enumerable19 != null) ? enumerable19.FirstOrDefault() : null);
				if (eMR_CONFIG19 != null)
				{
					string text22 = ((!string.IsNullOrEmpty(eMR_CONFIG19.VALUE)) ? eMR_CONFIG19.VALUE : eMR_CONFIG19.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text22))
					{
						GlobalStore.EMR_EMR_SIGN_CONNECT_DEVICE_TYPE_OPTION = text22;
					}
				}
				EMR_CONFIG eMR_CONFIG20 = GlobalStore.EmrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_PATIENT.SIGN_CERTIFICATE.OPTION").FirstOrDefault();
				string text23 = ((eMR_CONFIG20 == null) ? "" : ((!string.IsNullOrEmpty(eMR_CONFIG20.VALUE)) ? eMR_CONFIG20.VALUE : eMR_CONFIG20.DEFAULT_VALUE));
				if (eMR_CONFIG20 != null)
				{
					string text24 = ((!string.IsNullOrEmpty(eMR_CONFIG20.VALUE)) ? eMR_CONFIG20.VALUE : eMR_CONFIG20.DEFAULT_VALUE);
					if (!string.IsNullOrEmpty(text24))
					{
						GlobalStore.SIGN_CERTIFICATE_OPTION = text24;
					}
				}
				EMR_CONFIG eMR_CONFIG21 = GlobalStore.EmrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_BUSINESS.SHOW_ALL_TEMPLATES").FirstOrDefault();
				string text25 = ((eMR_CONFIG21 == null) ? "" : ((!string.IsNullOrEmpty(eMR_CONFIG21.VALUE)) ? eMR_CONFIG21.VALUE : eMR_CONFIG21.DEFAULT_VALUE));
				if (!string.IsNullOrEmpty(text25))
				{
					GlobalStore.EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES = text25;
				}
				IEnumerable<EMR_CONFIG> enumerable20 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGN_DESCRIPTION_INFO.OPTION");
				EMR_CONFIG eMR_CONFIG22 = ((enumerable20 != null) ? enumerable20.FirstOrDefault() : null);
				if (eMR_CONFIG22 != null)
				{
					string eMR_SIGN_SIGN_DESCRIPTION_INFO = ((!string.IsNullOrEmpty(eMR_CONFIG22.VALUE)) ? eMR_CONFIG22.VALUE : eMR_CONFIG22.DEFAULT_VALUE);
					GlobalStore.EMR_SIGN_SIGN_DESCRIPTION_INFO = eMR_SIGN_SIGN_DESCRIPTION_INFO;
				}
				IEnumerable<EMR_CONFIG> enumerable21 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.HSM.INTEGRATE_OPTION");
				EMR_CONFIG eMR_CONFIG23 = ((enumerable21 != null) ? enumerable21.FirstOrDefault() : null);
				if (eMR_CONFIG23 != null)
				{
					string eMR_HSM_INTEGRATE_OPTION = ((!string.IsNullOrEmpty(eMR_CONFIG23.VALUE)) ? eMR_CONFIG23.VALUE : eMR_CONFIG23.DEFAULT_VALUE);
					GlobalStore.EMR_HSM_INTEGRATE_OPTION = eMR_HSM_INTEGRATE_OPTION;
				}
				LogSystem.Info("EMR_HSM_INTEGRATE_OPTION: " + GlobalStore.EMR_HSM_INTEGRATE_OPTION);
				IEnumerable<EMR_CONFIG> enumerable22 = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGNER.AUTO_UPDATE_SIGN_IMAGE");
				EMR_CONFIG eMR_CONFIG24 = ((enumerable22 != null) ? enumerable22.FirstOrDefault() : null);
				if (eMR_CONFIG24 != null)
				{
					string eMR_EMR_SIGNER_AUTO_UPDATE_SIGN_IMAGE = ((!string.IsNullOrEmpty(eMR_CONFIG24.VALUE)) ? eMR_CONFIG24.VALUE : eMR_CONFIG24.DEFAULT_VALUE);
					GlobalStore.EMR_EMR_SIGNER_AUTO_UPDATE_SIGN_IMAGE = eMR_EMR_SIGNER_AUTO_UPDATE_SIGN_IMAGE;
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private bool ValidParam(InputADO inputADO, bool isShowSignedFile, ref string base64FileGigned, ref V_EMR_DOCUMENT documentData)
		{
			bool flag = true;
			try
			{
				flag = ValidParam(inputADO, isShowSignedFile, false, ref base64FileGigned, ref documentData);
			}
			catch (Exception ex)
			{
				flag = false;
				LogSystem.Warn(ex);
			}
			return flag;
		}

		private bool ValidParam(InputADO inputADO, bool isShowSignedFile, bool isValidExistsDoc, ref string base64FileGigned, ref V_EMR_DOCUMENT documentData)
		{
			bool flag = true;
			try
			{
				flag = flag && ValidParamCommon(inputADO);
				if (inputADO.IsSign && string.IsNullOrEmpty(inputADO.DocumentCode))
				{
					flag = flag && Verify.VerifyHisCode(inputADO, isShowSignedFile, isValidExistsDoc, ref base64FileGigned, ref documentData);
				}
			}
			catch (Exception ex)
			{
				flag = false;
				LogSystem.Warn(ex);
			}
			return flag;
		}

		private bool ValidParam(InputADO inputADO, bool isShowSignedFile, ref byte[] inputByte)
		{
			bool flag = true;
			try
			{
				flag = flag && ValidParamCommon(inputADO) && Verify.VerifyHisCode(inputADO, isShowSignedFile, ref inputByte);
			}
			catch (Exception ex)
			{
				flag = false;
				LogSystem.Warn(ex);
			}
			return flag;
		}

		private void InitUri()
		{
			if (!string.IsNullOrEmpty((string)RegistryProcessor.Read("ACS_BASE_URI")))
			{
				ConstanIG.ACS_BASE_URI = (string)RegistryProcessor.Read("ACS_BASE_URI");
			}
			if (!string.IsNullOrEmpty((string)RegistryProcessor.Read("EMR_BASE_URI")))
			{
				GlobalStore.EMR_BASE_URI = (string)RegistryProcessor.Read("EMR_BASE_URI");
			}
			if (!string.IsNullOrEmpty((string)RegistryProcessor.Read("FSS_BASE_URI")))
			{
				FssConstant.BASE_URI = (string)RegistryProcessor.Read("FSS_BASE_URI");
			}
			if (!string.IsNullOrEmpty((string)RegistryProcessor.Read("HPS_BASE_URI")))
			{
				GlobalStore.HPS_BASE_URI = (string)RegistryProcessor.Read("HPS_BASE_URI");
			}
		}

		private string GetTokenCodeData()
		{
			if (signToken != null && !string.IsNullOrEmpty(signToken.TokenCode))
			{
				return signToken.TokenCode;
			}
			return GlobalStore.TokenCode;
		}

		private EMR_SIGNER GetSignerData()
		{
			EMR_SIGNER eMR_SIGNER = null;
			eMR_SIGNER = ((signToken == null || signToken.Singer == null || signToken.Singer.ID <= 0) ? GlobalStore.Singer : signToken.Singer);
			if (eMR_SIGNER != null && GlobalStore.EMR_HSM_INTEGRATE_OPTION == "5" && string.IsNullOrWhiteSpace(eMR_SIGNER.HSM_USER_CODE))
			{
				frmUpdateSigner frmUpdateSigner = new frmUpdateSigner(eMR_SIGNER, 0);
				frmUpdateSigner.ShowDialog();
			}
			return eMR_SIGNER;
		}

		private EMR_TREATMENT GetTreatmentData()
		{
			if (signToken != null && signToken.Treatment != null && (inputADOWorking.IsOutsideTreatment == 1 || signToken.Treatment.ID > 0))
			{
				return signToken.Treatment;
			}
			return null;
		}

		public void ShowPopup(string inputFile, InputADO inputADO)
		{
			try
			{
				string base64FileGigned = "";
				V_EMR_DOCUMENT documentData = new V_EMR_DOCUMENT();
				if (!ValidParam(inputADO, true, ref base64FileGigned, ref documentData))
				{
					return;
				}
				if (!string.IsNullOrEmpty(base64FileGigned))
				{
					byte[] inputByte = Convert.FromBase64String(base64FileGigned);
					InputADO inputADO2 = CopyInputADO(inputADO);
					bool flag = (inputADO2.IsSign = false);
					bool flag3 = flag;
					flag = (inputADO2.IsSave = flag3);
					bool flag5 = flag;
					flag = (inputADO2.IsReject = flag5);
					bool isExport = flag;
					inputADO2.IsExport = isExport;
					inputADO2.DocumentCode = ((documentData != null) ? documentData.DOCUMENT_CODE : "");
					frmPdfViewer frmPdfViewer2 = new frmPdfViewer(inputByte, FileType.Pdf, inputADO2, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
					frmPdfViewer2.ShowDialog();
				}
				else if (!File.Exists(inputFile))
				{
					MessageBox.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					LogSystem.Warn("File không tồn tại. inputFile = " + LogUtil.TraceData(LogUtil.GetMemberName(() => inputFile), inputFile));
				}
				else
				{
					frmPdfViewer frmPdfViewer3 = new frmPdfViewer(inputFile, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
					frmPdfViewer3.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
		}

		public Form GetForm(string inputFile, InputADO inputADO)
		{
			try
			{
				if (!ValidParam(inputADO))
				{
					return null;
				}
				if (!File.Exists(inputFile))
				{
					LogSystem.Warn("File không tồn tại. inputFile = " + LogUtil.TraceData(LogUtil.GetMemberName(() => inputFile), inputFile));
					MessageBox.Show("File không tồn tại. inputFile = " + inputFile);
					return null;
				}
				return new frmPdfViewer(inputFile, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
			return null;
		}

		public UserControl GetUC(string inputFile, InputADO inputADO)
		{
			try
			{
				if (!ValidParam(inputADO))
				{
					return null;
				}
				if (!File.Exists(inputFile))
				{
					LogSystem.Warn("File không tồn tại. inputFile = " + LogUtil.TraceData(LogUtil.GetMemberName(() => inputFile), inputFile));
					MessageBox.Show("File không tồn tại. Đường dẫn file truyền vào: " + inputFile);
					return null;
				}
				return new UCViewer(inputFile, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
			return null;
		}

		public void ShowPopup(Stream inputStream, InputADO inputADO)
		{
			try
			{
				if (ValidParam(inputADO))
				{
					if (inputStream == null || inputStream.Length == 0)
					{
						LogSystem.Warn("inputStream không hợp lệ____inputStream.length=" + ((inputStream != null) ? inputStream.Length : 0));
						MessageBox.Show("FileStream không hợp lệ.");
						return;
					}
					inputStream.Position = 0L;
					byte[] inputByte = Utils.StreamToByte(inputStream);
					frmPdfViewer frmPdfViewer2 = new frmPdfViewer(inputByte, FileType.Xlsx, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
					frmPdfViewer2.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
		}

		public void ShowPopup(string base64FileContent, FileType fileType, InputADO inputADO)
		{
			try
			{
				if (string.IsNullOrEmpty(base64FileContent))
				{
					MessageBox.Show("Dữ liệu file không hợp lệ.");
					return;
				}
				V_EMR_DOCUMENT documentData = null;
				string base64FileGigned = "";
				if (ValidParam(inputADO, true, ref base64FileGigned, ref documentData))
				{
					if (!string.IsNullOrEmpty(base64FileGigned))
					{
						byte[] inputByte = Convert.FromBase64String(base64FileGigned);
						InputADO inputADO2 = CopyInputADO(inputADO);
						bool flag = (inputADO2.IsSign = false);
						bool flag3 = flag;
						flag = (inputADO2.IsSave = flag3);
						bool flag5 = flag;
						flag = (inputADO2.IsReject = flag5);
						bool isExport = flag;
						inputADO2.IsExport = isExport;
						inputADO2.DocumentCode = ((documentData != null) ? documentData.DOCUMENT_CODE : "");
						frmPdfViewer frmPdfViewer2 = new frmPdfViewer(inputByte, FileType.Pdf, inputADO2, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
						frmPdfViewer2.ShowDialog();
					}
					else
					{
						byte[] inputByte2 = Convert.FromBase64String(base64FileContent);
						frmPdfViewer frmPdfViewer3 = new frmPdfViewer(inputByte2, fileType, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
						frmPdfViewer3.ShowDialog();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
		}

		public void ShowPopup(List<FileADO> fileADOs, InputADO inputADO)
		{
			try
			{
				FileADO fileADO = null;
				FileADO fileADOJson = null;
				FileADO fileADOXml = null;
				foreach (FileADO fileADO2 in fileADOs)
				{
					if (string.IsNullOrEmpty(fileADO2.Base64FileContent))
					{
						LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe") + ". Du lieu file truyen vao khong hop le:____" + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
						MessageBox.Show("Dữ liệu file không hợp lệ.");
						return;
					}
					if (fileADO2.IsMain.HasValue && fileADO2.IsMain.Value)
					{
						fileADO = fileADO2;
					}
					else if (fileADO2.FileType == FileType.Json)
					{
						fileADOJson = fileADO2;
					}
					else if (fileADO2.FileType == FileType.Xml)
					{
						fileADOXml = fileADO2;
					}
				}
				if (fileADO == null && fileADOs.Count > 1)
				{
					fileADO = fileADOs.FirstOrDefault();
				}
				else if (fileADO == null && fileADOs.Count == 1)
				{
					fileADO = fileADOs.FirstOrDefault();
					fileADOJson = null;
					fileADOXml = null;
				}
				V_EMR_DOCUMENT documentData = null;
				string base64FileGigned = "";
				if (ValidParam(inputADO, true, ref base64FileGigned, ref documentData))
				{
					if (!string.IsNullOrEmpty(base64FileGigned))
					{
						byte[] inputByte = Convert.FromBase64String(base64FileGigned);
						InputADO inputADO2 = CopyInputADO(inputADO);
						bool flag = (inputADO2.IsSign = false);
						bool flag3 = flag;
						flag = (inputADO2.IsSave = flag3);
						bool flag5 = flag;
						flag = (inputADO2.IsReject = flag5);
						bool isExport = flag;
						inputADO2.IsExport = isExport;
						inputADO2.DocumentCode = ((documentData != null) ? documentData.DOCUMENT_CODE : "");
						frmPdfViewer frmPdfViewer2 = new frmPdfViewer(inputByte, FileType.Pdf, inputADO2, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
						frmPdfViewer2.ShowDialog();
					}
					else
					{
						byte[] inputByte2 = Convert.FromBase64String(fileADO.Base64FileContent);
						frmPdfViewer frmPdfViewer3 = new frmPdfViewer(inputByte2, fileADO.FileType, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
						frmPdfViewer3.UpdateExtFileType(fileADO, fileADOJson, fileADOXml);
						frmPdfViewer3.ShowDialog();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
		}

		public Form GetForm(Stream inputStream, InputADO inputADO)
		{
			try
			{
				if (!ValidParam(inputADO))
				{
					return null;
				}
				if (inputStream == null || inputStream.Length == 0)
				{
					LogSystem.Warn("inputStream không hợp lệ____inputStream.length=" + ((inputStream != null) ? inputStream.Length : 0) + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
					MessageBox.Show("FileStream không hợp lệ.");
					return null;
				}
				inputStream.Position = 0L;
				byte[] inputByte = Utils.StreamToByte(inputStream);
				return new frmPdfViewer(inputByte, FileType.Xlsx, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
			return null;
		}

		public UserControl GetUC(string base64FileContent, FileType fileType, InputADO inputADO)
		{
			return GetUC(base64FileContent, fileType, inputADO, "");
		}

		public UserControl GetUC(string base64FileContent, FileType fileType, InputADO inputADO, string pin)
		{
			try
			{
				GlobalStore.PIN = pin;
				if (string.IsNullOrEmpty(base64FileContent))
				{
					MessageBox.Show("Dữ liệu base64 file không hợp lệ.");
					LogSystem.Info("Du lieu dau vao khong hop le____" + LogUtil.TraceData("base64FileContent", base64FileContent) + "____" + LogUtil.TraceData("fileType", fileType) + "____" + LogUtil.TraceData("inputADO", inputADO));
					return null;
				}
				V_EMR_DOCUMENT documentData = new V_EMR_DOCUMENT();
				string base64FileGigned = "";
				if (!ValidParam(inputADO, true, ref base64FileGigned, ref documentData))
				{
					return null;
				}
				if (!string.IsNullOrEmpty(base64FileGigned))
				{
					byte[] inputByte = Convert.FromBase64String(base64FileGigned);
					InputADO inputADO2 = CopyInputADO(inputADO);
					bool flag = (inputADO2.IsSign = false);
					bool flag3 = flag;
					flag = (inputADO2.IsSave = flag3);
					bool flag5 = flag;
					flag = (inputADO2.IsReject = flag5);
					bool isExport = flag;
					inputADO2.IsExport = isExport;
					inputADO2.DocumentCode = ((documentData != null) ? documentData.DOCUMENT_CODE : "");
					return new UCViewer(inputByte, FileType.Pdf, inputADO2, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
				}
				byte[] inputByte2 = Convert.FromBase64String(base64FileContent);
				return new UCViewer(inputByte2, fileType, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
				LogSystem.Info("Co loi xay ra. Du lieu dau vao:____" + LogUtil.TraceData("base64FileContent", base64FileContent) + "____" + LogUtil.TraceData("fileType", fileType) + "____" + LogUtil.TraceData("inputADO", inputADO));
			}
			return null;
		}

		public UserControl GetUC(Stream inputStream, InputADO inputADO)
		{
			try
			{
				if (!ValidParam(inputADO))
				{
					return null;
				}
				if (inputStream == null || inputStream.Length == 0)
				{
					LogSystem.Warn("inputStream không hợp lệ____inputStream.length=" + ((inputStream != null) ? inputStream.Length : 0));
					MessageBox.Show("FileStream không hợp lệ.");
					return null;
				}
				byte[] inputByte = Utils.StreamToByte(inputStream);
				inputStream.Position = 0L;
				return new UCViewer(inputByte, FileType.Xlsx, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
			return null;
		}

		public void ShowPopup(byte[] inputByte, InputADO inputADO)
		{
			try
			{
				if (ValidParam(inputADO))
				{
					if (inputByte == null || inputByte.Length == 0)
					{
						LogSystem.Warn("FileByte không hợp lệ____");
						MessageBox.Show("FileByte không hợp lệ.");
					}
					else
					{
						frmPdfViewer frmPdfViewer2 = new frmPdfViewer(inputByte, FileType.Xlsx, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
						frmPdfViewer2.ShowDialog();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
		}

		public Form GetForm(byte[] inputByte, InputADO inputADO)
		{
			try
			{
				if (!ValidParam(inputADO))
				{
					return null;
				}
				if (inputByte == null || inputByte.Length == 0)
				{
					LogSystem.Warn("FileByte không hợp lệ____");
					MessageBox.Show("FileByte không hợp lệ.");
					return null;
				}
				return new frmPdfViewer(inputByte, FileType.Xlsx, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
			return null;
		}

		public UserControl GetUC(byte[] inputByte, InputADO inputADO)
		{
			try
			{
				if (!ValidParam(inputADO))
				{
					return null;
				}
				if (inputByte == null || inputByte.Length == 0)
				{
					LogSystem.Warn("FileByte không hợp lệ____");
					MessageBox.Show("FileByte không hợp lệ.");
					return null;
				}
				return new UCViewer(inputByte, FileType.Xlsx, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
			return null;
		}

		public UserControl GetUC(byte[] inputByte, FileType fileType, InputADO inputADO, string pin)
		{
			try
			{
				GlobalStore.PIN = pin;
				if (inputByte == null || inputByte.Length == 0)
				{
					LogSystem.Warn("FileByte không hợp lệ____");
					MessageBox.Show("FileByte không hợp lệ.");
					return null;
				}
				byte[] inputByte2 = null;
				if (!ValidParam(inputADO, true, ref inputByte2))
				{
					return null;
				}
				if (inputByte2 != null && inputByte2.Length != 0)
				{
					InputADO inputADO2 = CopyInputADO(inputADO);
					bool flag = (inputADO2.IsSign = false);
					bool flag3 = flag;
					flag = (inputADO2.IsSave = flag3);
					bool flag5 = flag;
					flag = (inputADO2.IsReject = flag5);
					bool flag7 = flag;
					flag = (inputADO2.IsPrint = flag7);
					bool isExport = flag;
					inputADO2.IsExport = isExport;
					return new UCViewer(inputByte2, FileType.Pdf, inputADO2, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
				}
				return new UCViewer(inputByte, fileType, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Info("Co loi xay ra. Du lieu dau vao:____" + LogUtil.TraceData("base64FileContent", Convert.ToBase64String(inputByte)) + "____" + LogUtil.TraceData("fileType", fileType) + "____" + LogUtil.TraceData("inputADO", inputADO));
				LogSystem.Warn(ex);
			}
			return null;
		}

		public UserControl GetUC(byte[] inputByte, FileType fileType, InputADO inputADO)
		{
			return GetUC(inputByte, fileType, inputADO, "");
		}

		public bool RefeshAfterLogout()
		{
			bool flag = false;
			try
			{
				GlobalStore.TokenCode = null;
				GlobalStore.TokenData = null;
				signToken = new SignToken();
				CommonParam commonParam = new CommonParam();
				ClientTokenManager clientTokenManager = new ClientTokenManager("HIS", ConstanIG.ACS_BASE_URI);
				clientTokenManager.UseRegistry(true);
				clientTokenManager.Logout(commonParam);
				flag = true;
			}
			catch (Exception ex)
			{
				flag = false;
				LogSystem.Warn(ex);
			}
			return flag;
		}

		public void SetPIN(string pin)
		{
			GlobalStore.PIN = pin;
		}

		public DocumentSignedResultDTO SignNow(string base64FileContent, FileType fileType, InputADO inputADO)
		{
			return ProcessSignPrintNow(base64FileContent, fileType, inputADO, false);
		}

		public DocumentSignedResultDTO SignNow(string base64FileContent, FileType fileType, InputADO inputADO, bool isSignOnlyWithHasAutoPosition = false)
		{
			return ProcessSignPrintNow(base64FileContent, fileType, inputADO, false, false, isSignOnlyWithHasAutoPosition);
		}

		public DocumentSignedResultDTO SignAndPrintNow(string base64FileContent, FileType fileType, InputADO inputADO)
		{
			return ProcessSignPrintNow(base64FileContent, fileType, inputADO, true);
		}

		public DocumentSignedResultDTO SignAndShowPrintPreview(string base64FileContent, FileType fileType, InputADO inputADO)
		{
			return ProcessSignPrintNow(base64FileContent, fileType, inputADO, false, true);
		}

		private DocumentSignedResultDTO ProcessSignPrintNow(string base64FileContent, FileType fileType, InputADO inputADO, bool isPrintNow, bool isPrintPreview = false, bool isSignOnlyWithHasAutoPosition = false)
		{
			_003C_003Ec__DisplayClass42 CS_0024_003C_003E8__locals111 = new _003C_003Ec__DisplayClass42();
			CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101 = new _003C_003Ec__DisplayClass43_0();
			CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintNow = isPrintNow;
			CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintPreview = isPrintPreview;
			CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isSignOnlyWithHasAutoPosition = isSignOnlyWithHasAutoPosition;
			LogSystem.Info("ProcessSignPrint" + (CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintPreview ? "Preview" : "Now") + ". 1");
			LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintNow), CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintNow) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintPreview), CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintPreview) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<bool>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals111), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isSignOnlyWithHasAutoPosition) + LogUtil.TraceData("fileType", fileType));
			outputSignedFileResult = "";
			iSPrintNow = CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintNow;
			iSPrintPreview = CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintPreview;
			int num = 0;
			CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData = new DocumentSignedResultDTO();
			try
			{
				_003C_003Ec__DisplayClass44 CS_0024_003C_003E8__locals116 = new _003C_003Ec__DisplayClass44();
				CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals43 = CS_0024_003C_003E8__locals111;
				CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106 = new _003C_003Ec__DisplayClass43_1();
				if (string.IsNullOrEmpty(base64FileContent))
				{
					CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = false;
					CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = MessageUitl.GetMessage("DuLieuKhongHopLe");
					LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData), CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData));
					MessageBox.Show(CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message);
					return CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData;
				}
				V_EMR_DOCUMENT documentData = new V_EMR_DOCUMENT();
				if (!ValidParam(inputADO, true, true, ref base64FileContent, ref documentData))
				{
					CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = false;
					CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = MessageUitl.GetMessage("DuLieuKhongHopLe");
					LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData), CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData));
					return CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData;
				}
				CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork = "";
				string extByFileType = Utils.GetExtByFileType(fileType);
				byte[] arrInFile = Convert.FromBase64String(base64FileContent);
				string text = Utils.GenerateTempFileWithin(extByFileType);
				Utils.ByteToFile(arrInFile, text);
				Utils.ProcessFileInput(text, extByFileType, ref CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork);
				CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success = false;
				bool flag = false;
				bool flag2 = false;
				bool isSignParanel = false;
				DocumentTDO documentTDO = null;
				int signedCount = 0;
				EMR_SIGN signSelected = null;
				List<SignPositionADO> signPositionADOs = null;
				PdfReader pdfReader = null;
				if (!string.IsNullOrEmpty(inputADO.DocumentCode))
				{
					signSelected = new EmrSign().GetSignDocumentFirst(inputADO.DocumentCode, GetSignerData(), GetTreatmentData(), flag, true);
				}
				printNumberCopies = (short)((!inputADO.PrintNumberCopies.HasValue) ? 1 : inputADO.PrintNumberCopies.Value);
				bool isPatientSign = inputADO.IsPatientSign;
				bool flag3 = inputADO.IsHomeRelativeSign.HasValue && inputADO.IsHomeRelativeSign.Value;
				if (fileType != FileType.Xml && fileType != FileType.Json)
				{
					pdfReader = new PdfReader(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork);
					ProcessCommentKey(inputADO, ref CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork, ref signPositionADOs);
					if (signPositionADOs != null && signPositionADOs.Count > 0)
					{
						pdfReader.Close();
						pdfReader = new PdfReader(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork);
					}
					signPositionADOs = GetNextPositionSigned(inputADO.IsPatientSign || inputADO.IsHomeRelativeSign == true, pdfReader, signSelected, ref signedCount);
					LogSystem.Debug("ProcessSignPrintNow____nextSignPositions.count=" + ((signPositionADOs != null) ? signPositionADOs.Count : 0));
				}
				List<SignTDO> list = null;
				CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile = "";
				if (((signPositionADOs != null && signPositionADOs.Count > 0) || fileType == FileType.Xml || fileType == FileType.Json) && inputADO.SignerConfigs != null && inputADO.SignerConfigs.Count > 0)
				{
					inputADO.SignerConfigs = inputADO.SignerConfigs.OrderBy((SignerConfigDTO o) => o.NumOrder).ToList();
					list = new List<SignTDO>();
					foreach (SignerConfigDTO signerConfig in inputADO.SignerConfigs)
					{
						SignTDO signTDO = new SignTDO();
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(signerConfig.Loginname);
						if (byLoginName != null)
						{
							signTDO.SignerId = byLoginName.ID;
							signTDO.Loginname = byLoginName.LOGINNAME;
							signTDO.Username = byLoginName.USERNAME;
							signTDO.FullName = byLoginName.USERNAME;
							signTDO.FirstName = byLoginName.USERNAME;
							if (signerConfig.NumOrder > 0)
							{
								signTDO.NumOrder = signerConfig.NumOrder;
							}
							else
							{
								signTDO.NumOrder = GetMaxNumOrder(list);
							}
							signTDO.Title = byLoginName.TITLE;
							signTDO.DepartmentCode = byLoginName.DEPARTMENT_CODE;
							signTDO.DepartmentName = byLoginName.DEPARTMENT_NAME;
							signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							list.Add(signTDO);
						}
					}
				}
				string signName = (string.IsNullOrEmpty(GlobalStore.UserName) ? GlobalStore.LoginName : (GlobalStore.LoginName + " (" + GlobalStore.UserName + ")"));
				long? documentTypeId = GetDocumentTypeId(inputADO.DocumentTypeCode);
				if (signPositionADOs != null && signPositionADOs.Count > 0)
				{
					flag = GetMultiSignDoc(inputADO, ref isSignParanel);
					if (!string.IsNullOrEmpty(inputADO.DocumentCode))
					{
						documentTDO = GenerateByDocumentCode(inputADO.DocumentCode, ref flag);
						isSignParanel = documentTDO != null && documentTDO.IsSignParallel.HasValue && documentTDO.IsSignParallel.Value;
					}
					SignPositionADO signPositionADO = signPositionADOs[0];
					if (signPositionADOs.Count > 1)
					{
						signPositionADO.SignPositionAutos = signPositionADOs;
					}
					bool? flag4 = VerifySign.VerifySignImageWithOption(inputADO, GetSignerData(), signPositionADOs != null && signPositionADOs.Count > 0, signPositionADO, flag);
					if (flag4.HasValue)
					{
						if (!flag4.Value)
						{
							CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = false;
							CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = MessageUitl.GetMessage("TaiKhoanThieuThongTinAnh");
							LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData), CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData));
							return CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData;
						}
						num = Constans.DISPLAY_RECTANGLE_TEXT;
					}
					flag2 = false;
					if (!flag && signPositionADOs != null && signPositionADOs.Count >= 2)
					{
						flag2 = true;
					}
					int num2 = 1;
					using (List<SignPositionADO>.Enumerator enumerator2 = signPositionADOs.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							_003C_003Ec__DisplayClass44 _003C_003Ec__DisplayClass = CS_0024_003C_003E8__locals116;
							_003C_003Ec__DisplayClass42 _003C_003Ec__DisplayClass2 = CS_0024_003C_003E8__locals111;
							SignPositionADO nSp = enumerator2.Current;
							if (documentTDO != null && !string.IsNullOrEmpty(documentTDO.DocumentCode) && signPositionADOs != null && signPositionADOs.Count >= 2)
							{
								signSelected = new EmrSign().GetSignDocumentFirst(documentTDO.DocumentCode, GetSignerData(), GetTreatmentData(), flag2 || flag, false);
							}
							LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.WidthRectangle), nSp.WidthRectangle) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.HeightRectangle), nSp.HeightRectangle) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.PageNUm), nSp.PageNUm) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.Text), nSp.Text));
							float left = nSp.Reactanle.Left;
							left = ((left < 0f) ? 0f : left);
							float bottom = nSp.Reactanle.Bottom;
							bottom = ((bottom < 0f) ? 0f : bottom);
							int numberOfPages = pdfReader.NumberOfPages;
							VerifyPdfFileHandle verifyPdfFileHandle = new VerifyPdfFileHandle();
							List<VerifierADO> verifiers = (from o in verifyPdfFileHandle.verify(pdfReader)
								orderby o.Date
								select o).ToList();
							SignHandle signHandle = null;
							CommonParam param = new CommonParam();
							DisplayConfigDTO displayConfigDTO = new DisplayConfigDTO();
							displayConfigDTO.HeightRectangle = ((nSp.HeightRectangle > 0f) ? new float?(nSp.HeightRectangle) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.HeightRectangle : ((float?)null)));
							displayConfigDTO.WidthRectangle = ((nSp.WidthRectangle > 0f) ? new float?(nSp.WidthRectangle) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.WidthRectangle : ((float?)null)));
							displayConfigDTO.SizeFont = ((nSp.SizeFont > 0) ? new int?(nSp.SizeFont) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.SizeFont : ((int?)null)));
							displayConfigDTO.TextPosition = ((nSp.TextPosition > Constans.TEXT_POSITON.x100) ? new int?((int)nSp.TextPosition) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.TextPosition : ((int?)null)));
							displayConfigDTO.TypeDisplay = ((num > 0) ? new int?(num) : ((nSp.TypeDisplay > 0) ? new int?(nSp.TypeDisplay) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.TypeDisplay : ((int?)null))));
							displayConfigDTO.IsDisplaySignature = (nSp.IsDisplaySignature.HasValue ? nSp.IsDisplaySignature : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.IsDisplaySignature : ((bool?)null)));
							displayConfigDTO.FormatRectangleText = ((inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADO.DisplayConfigDTO.FormatRectangleText)) ? inputADO.DisplayConfigDTO.FormatRectangleText : string.Empty);
							displayConfigDTO.Location = ((inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADO.DisplayConfigDTO.Location)) ? inputADO.DisplayConfigDTO.Location : string.Empty);
							displayConfigDTO.Alignment = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.Alignment.HasValue) ? inputADO.DisplayConfigDTO.Alignment : ((int?)null));
							displayConfigDTO.IsBold = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.IsBold.HasValue) ? inputADO.DisplayConfigDTO.IsBold : ((bool?)null));
							displayConfigDTO.IsItalic = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.IsItalic.HasValue) ? inputADO.DisplayConfigDTO.IsItalic : ((bool?)null));
							displayConfigDTO.IsUnderlined = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.IsUnderlined.HasValue) ? inputADO.DisplayConfigDTO.IsUnderlined : ((bool?)null));
							displayConfigDTO.FontName = ((inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADO.DisplayConfigDTO.FontName)) ? inputADO.DisplayConfigDTO.FontName : null);
							DisplayConfigDTO displayConfigDTO2 = displayConfigDTO;
							SignType signTypeConfig = GetSignTypeConfig(inputADO);
							bool flag5 = flag;
							bool isMultiSign = (!flag2 || num2 != signPositionADOs.Count) && ((!flag2) ? flag : flag2);
							signHandle = new SignHandle(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork, left, bottom, nSp.PageNUm, numberOfPages, null, inputADO.DlgOpenModuleConfig, inputADO.DocumentName, inputADO.Treatment.TREATMENT_CODE, signName, inputADO.SignReason, verifiers, signTypeConfig, list, signSelected, isPatientSign, flag3, documentTypeId, isMultiSign, inputADO.HisCode, inputADO, displayConfigDTO2, param, null, isSignParanel, GetTreatmentData(), GetSignerData(), GetTokenCodeData());
							bool flag6 = true;
							string message = "";
							if (!signHandle.VerifyFile(documentTDO, signedCount, ref message))
							{
								CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = false;
								CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = message;
							}
							else
							{
								_003C_003Ec__DisplayClass44 _003C_003Ec__DisplayClass3 = CS_0024_003C_003E8__locals116;
								_003C_003Ec__DisplayClass42 _003C_003Ec__DisplayClass4 = CS_0024_003C_003E8__locals111;
								if (documentTDO == null)
								{
									documentTDO = new DocumentTDO();
								}
								if (inputADO.IsUsingSignPad.HasValue && (isPatientSign || flag3))
								{
									signHandle.SetSignPadBefore(Utils.SignPadImageData);
									signHandle.SetUsingSignPad(inputADO.IsUsingSignPad.Value);
								}
								else
								{
									signHandle.SetSignPadBefore(null);
								}
								signHandle.SetFileType(fileType);
								bool rsSuccess = signHandle.SignFile(documentTDO, ref CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile);
								LogSystem.Info("SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => rsSuccess), rsSuccess) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile), CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork), CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork));
								if (inputADO.IsRemoveSignPadBefore)
								{
									Utils.SignPadImageData = null;
								}
								if (rsSuccess)
								{
									CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success = rsSuccess;
									try
									{
										if (pdfReader != null)
										{
											pdfReader.Close();
										}
										if (File.Exists(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork))
										{
											File.Delete(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork);
										}
									}
									catch
									{
									}
									CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = "OK";
									CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Base64FileSigned = Utils.FileToBase64String(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile);
									CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.DocumentCode = documentTDO.DocumentCode;
									CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork = CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile;
									pdfReader = new PdfReader(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork);
								}
								CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success;
							}
							num2++;
						}
					}
					if (((signPositionADOs != null && signPositionADOs.Count > 0) & CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success) && File.Exists(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile))
					{
						LogSystem.Info("SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success), CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<string>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals116), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile));
						try
						{
							if (pdfReader != null)
							{
								pdfReader.Close();
							}
						}
						catch
						{
						}
						if (!CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isSignOnlyWithHasAutoPosition)
						{
							CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = "OK";
							CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Base64FileSigned = Utils.FileToBase64String(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile);
							if (CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintNow)
							{
								PrintNowProcess(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile);
							}
							else if (CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintPreview)
							{
								PrintPreviewProcess(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile);
							}
						}
						CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success;
					}
				}
				else if (fileType == FileType.Xml || fileType == FileType.Json)
				{
					SignHandle signHandle2 = null;
					CommonParam param2 = new CommonParam();
					DisplayConfigDTO displayConfigDTO3 = inputADO.DisplayConfigDTO;
					SignType signTypeConfig2 = GetSignTypeConfig(inputADO);
					bool isMultiSign2 = flag;
					signHandle2 = new SignHandle(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork, 1f, 1f, 1, 1, null, inputADO.DlgOpenModuleConfig, inputADO.DocumentName, inputADO.Treatment.TREATMENT_CODE, signName, inputADO.SignReason, null, signTypeConfig2, list, signSelected, isPatientSign, flag3, documentTypeId, isMultiSign2, inputADO.HisCode, inputADO, displayConfigDTO3, param2, null, isSignParanel, GetTreatmentData(), GetSignerData(), GetTokenCodeData());
					bool flag7 = true;
					string message2 = "";
					if (!signHandle2.VerifyFile(documentTDO, signedCount, ref message2))
					{
						CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = false;
						CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = message2;
					}
					else
					{
						_003C_003Ec__DisplayClass44 _003C_003Ec__DisplayClass5 = CS_0024_003C_003E8__locals116;
						_003C_003Ec__DisplayClass42 _003C_003Ec__DisplayClass6 = CS_0024_003C_003E8__locals111;
						if (documentTDO == null)
						{
							documentTDO = new DocumentTDO();
						}
						signHandle2.SetFileType(fileType);
						bool rsSuccess2 = signHandle2.SignFile(documentTDO, ref CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile);
						LogSystem.Info("SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => rsSuccess2), rsSuccess2) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile), CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork), CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork));
						if (rsSuccess2)
						{
							CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success = rsSuccess2;
							try
							{
								if (File.Exists(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork))
								{
									File.Delete(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork);
								}
							}
							catch
							{
							}
							CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = "OK";
							CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Base64FileSigned = Utils.FileToBase64String(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile);
							CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.DocumentCode = documentTDO.DocumentCode;
							CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork = CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.outputFile;
						}
						CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.success;
					}
				}
				else if (!CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isSignOnlyWithHasAutoPosition)
				{
					frmPdfViewer frmPdfViewer2 = new frmPdfViewer(CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals106.inputFileWork, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData(), true, ActionAfterSignedProcess, CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintNow);
					frmPdfViewer2.ShowDialog();
					CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = !string.IsNullOrEmpty(outputSignedFileResult);
					if (CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success)
					{
						CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = "OK";
						CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Base64FileSigned = Utils.FileToBase64String(outputSignedFileResult);
						CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.DocumentCode = inputADO.DocumentCode;
					}
				}
				else
				{
					CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = false;
					CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Message = "Văn bản mã " + ((!string.IsNullOrWhiteSpace(inputADO.DocumentCode)) ? inputADO.DocumentCode : CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.DocumentCode) + " chưa được thiết lập vị trí ký";
				}
				LogSystem.Info("ProcessSignPrint" + (CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.isPrintPreview ? "Preview" : "Now") + ". 2");
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn("Co loi xay ra. Du lieu dau vao:____" + LogUtil.TraceData("base64FileContent", base64FileContent) + "____" + LogUtil.TraceData("fileType", fileType) + "____" + LogUtil.TraceData("inputADO", inputADO));
				LogSystem.Error(ex);
				CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData.Success = false;
			}
			try
			{
				outputSignedFileResult = null;
				inputADOWorking = null;
				signToken = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
			return CS_0024_003C_003E8__locals111.CS_0024_003C_003E8__locals101.rsData;
		}

		public DocumentSignedResultDTO ProcessSignPrintNow(List<FileADO> fileADOs, InputADO inputADO, bool isPrintNow, bool isPrintPreview = false, bool isSignOnlyWithHasAutoPosition = false)
		{
			_003C_003Ec__DisplayClass51 CS_0024_003C_003E8__locals188 = new _003C_003Ec__DisplayClass51();
			CS_0024_003C_003E8__locals188.inputADO = inputADO;
			CS_0024_003C_003E8__locals188.isPrintNow = isPrintNow;
			CS_0024_003C_003E8__locals188.isPrintPreview = isPrintPreview;
			CS_0024_003C_003E8__locals188.isSignOnlyWithHasAutoPosition = isSignOnlyWithHasAutoPosition;
			LogSystem.Info("ProcessSignPrint" + (CS_0024_003C_003E8__locals188.isPrintPreview ? "Preview" : "Now") + ". 1");
			LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals188.isPrintNow), CS_0024_003C_003E8__locals188.isPrintNow) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals188.isPrintPreview), CS_0024_003C_003E8__locals188.isPrintPreview) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals188.isSignOnlyWithHasAutoPosition), CS_0024_003C_003E8__locals188.isSignOnlyWithHasAutoPosition) + LogUtil.TraceData("fileADOs.Count", (fileADOs != null) ? fileADOs.Count : 0));
			outputSignedFileResult = "";
			iSPrintNow = CS_0024_003C_003E8__locals188.isPrintNow;
			iSPrintPreview = CS_0024_003C_003E8__locals188.isPrintPreview;
			int num = 0;
			CS_0024_003C_003E8__locals188.rsData = new DocumentSignedResultDTO();
			try
			{
				_003C_003Ec__DisplayClass53 CS_0024_003C_003E8__locals193 = new _003C_003Ec__DisplayClass53();
				CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals52 = CS_0024_003C_003E8__locals188;
				CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44 = new _003C_003Ec__DisplayClass44_1();
				string err = "";
				if (!Verify.VerifySignPrintNow(fileADOs, ref err))
				{
					CS_0024_003C_003E8__locals188.rsData.Success = false;
					CS_0024_003C_003E8__locals188.rsData.Message = MessageUitl.GetMessage("DuLieuKhongHopLe");
					LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals188.rsData), CS_0024_003C_003E8__locals188.rsData));
					MessageBox.Show(CS_0024_003C_003E8__locals188.rsData.Message);
					return CS_0024_003C_003E8__locals188.rsData;
				}
				FileADO fileADO = null;
				FileADO fileADO2 = null;
				FileADO fileADO3 = null;
				foreach (FileADO fileADO4 in fileADOs)
				{
					if (string.IsNullOrEmpty(fileADO4.Base64FileContent))
					{
						CS_0024_003C_003E8__locals188.rsData.Success = false;
						LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe") + ". Du lieu file truyen vao khong hop le:____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals188.inputADO), CS_0024_003C_003E8__locals188.inputADO));
						return CS_0024_003C_003E8__locals188.rsData;
					}
					if (fileADO4.IsMain.HasValue && fileADO4.IsMain.Value)
					{
						fileADO = fileADO4;
					}
					else if (fileADO4.FileType == FileType.Json)
					{
						fileADO2 = fileADO4;
					}
					else if (fileADO4.FileType == FileType.Xml)
					{
						fileADO3 = fileADO4;
					}
				}
				if (fileADO == null && fileADOs.Count > 1)
				{
					fileADO = fileADOs.FirstOrDefault();
				}
				else if (fileADO == null && fileADOs.Count == 1)
				{
					fileADO = fileADOs.FirstOrDefault();
					fileADO2 = null;
					fileADO3 = null;
				}
				string base64FileGigned = fileADO.Base64FileContent;
				V_EMR_DOCUMENT documentData = new V_EMR_DOCUMENT();
				if (!ValidParam(CS_0024_003C_003E8__locals188.inputADO, true, true, ref base64FileGigned, ref documentData))
				{
					CS_0024_003C_003E8__locals188.rsData.Success = false;
					CS_0024_003C_003E8__locals188.rsData.Message = MessageUitl.GetMessage("DuLieuKhongHopLe");
					LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals188.rsData), CS_0024_003C_003E8__locals188.rsData));
					return CS_0024_003C_003E8__locals188.rsData;
				}
				CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork = "";
				string extByFileType = Utils.GetExtByFileType(fileADO.FileType);
				byte[] arrInFile = Convert.FromBase64String(base64FileGigned);
				string text = Utils.GenerateTempFileWithin(extByFileType);
				Utils.ByteToFile(arrInFile, text);
				Utils.ProcessFileInput(text, extByFileType, ref CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork);
				CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success = false;
				bool flag = false;
				bool flag2 = false;
				bool isSignParanel = false;
				DocumentTDO documentTDO = null;
				int signedCount = 0;
				EMR_SIGN signSelected = null;
				List<SignPositionADO> signPositionADOs = null;
				PdfReader pdfReader = null;
				if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals188.inputADO.DocumentCode))
				{
					signSelected = new EmrSign().GetSignDocumentFirst(CS_0024_003C_003E8__locals188.inputADO.DocumentCode, GetSignerData(), GetTreatmentData(), flag, true);
				}
				printNumberCopies = (short)((!CS_0024_003C_003E8__locals188.inputADO.PrintNumberCopies.HasValue) ? 1 : CS_0024_003C_003E8__locals188.inputADO.PrintNumberCopies.Value);
				bool isPatientSign = CS_0024_003C_003E8__locals188.inputADO.IsPatientSign;
				bool isHomeRelativeSign = CS_0024_003C_003E8__locals188.inputADO.IsHomeRelativeSign.HasValue && CS_0024_003C_003E8__locals188.inputADO.IsHomeRelativeSign.Value;
				if (fileADO.FileType != FileType.Xml && fileADO.FileType != FileType.Json)
				{
					pdfReader = new PdfReader(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork);
					ProcessCommentKey(CS_0024_003C_003E8__locals188.inputADO, ref CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork, ref signPositionADOs);
					if (signPositionADOs != null && signPositionADOs.Count > 0)
					{
						pdfReader.Close();
						pdfReader = new PdfReader(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork);
					}
					signPositionADOs = GetNextPositionSigned(CS_0024_003C_003E8__locals188.inputADO.IsPatientSign || CS_0024_003C_003E8__locals188.inputADO.IsHomeRelativeSign == true, pdfReader, signSelected, ref signedCount);
					LogSystem.Debug("ProcessSignPrintNow____nextSignPositions.count=" + ((signPositionADOs != null) ? signPositionADOs.Count : 0));
				}
				List<SignTDO> list = null;
				CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile = "";
				if (((signPositionADOs != null && signPositionADOs.Count > 0) || fileADO.FileType == FileType.Xml || fileADO.FileType == FileType.Json) && CS_0024_003C_003E8__locals188.inputADO.SignerConfigs != null && CS_0024_003C_003E8__locals188.inputADO.SignerConfigs.Count > 0)
				{
					CS_0024_003C_003E8__locals188.inputADO.SignerConfigs = CS_0024_003C_003E8__locals188.inputADO.SignerConfigs.OrderBy((SignerConfigDTO o) => o.NumOrder).ToList();
					list = new List<SignTDO>();
					foreach (SignerConfigDTO signerConfig in CS_0024_003C_003E8__locals188.inputADO.SignerConfigs)
					{
						SignTDO signTDO = new SignTDO();
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(signerConfig.Loginname);
						if (byLoginName != null)
						{
							signTDO.SignerId = byLoginName.ID;
							signTDO.Loginname = byLoginName.LOGINNAME;
							signTDO.Username = byLoginName.USERNAME;
							signTDO.FullName = byLoginName.USERNAME;
							signTDO.FirstName = byLoginName.USERNAME;
							if (signerConfig.NumOrder > 0)
							{
								signTDO.NumOrder = signerConfig.NumOrder;
							}
							else
							{
								signTDO.NumOrder = GetMaxNumOrder(list);
							}
							signTDO.Title = byLoginName.TITLE;
							signTDO.DepartmentCode = byLoginName.DEPARTMENT_CODE;
							signTDO.DepartmentName = byLoginName.DEPARTMENT_NAME;
							signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							list.Add(signTDO);
						}
					}
				}
				string signName = (string.IsNullOrEmpty(GlobalStore.UserName) ? GlobalStore.LoginName : (GlobalStore.LoginName + " (" + GlobalStore.UserName + ")"));
				long? documentTypeId = GetDocumentTypeId(CS_0024_003C_003E8__locals188.inputADO.DocumentTypeCode);
				if (signPositionADOs != null && signPositionADOs.Count > 0)
				{
					flag = GetMultiSignDoc(CS_0024_003C_003E8__locals188.inputADO, ref isSignParanel);
					if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals188.inputADO.DocumentCode))
					{
						documentTDO = GenerateByDocumentCode(CS_0024_003C_003E8__locals188.inputADO.DocumentCode, ref flag);
						isSignParanel = documentTDO != null && documentTDO.IsSignParallel.HasValue && documentTDO.IsSignParallel.Value;
					}
					SignPositionADO signPositionADO = signPositionADOs[0];
					if (signPositionADOs.Count > 1)
					{
						signPositionADO.SignPositionAutos = signPositionADOs;
					}
					bool? flag3 = VerifySign.VerifySignImageWithOption(CS_0024_003C_003E8__locals188.inputADO, GetSignerData(), signPositionADOs != null && signPositionADOs.Count > 0, signPositionADO, flag);
					if (flag3.HasValue)
					{
						if (!flag3.Value)
						{
							CS_0024_003C_003E8__locals188.rsData.Success = false;
							CS_0024_003C_003E8__locals188.rsData.Message = MessageUitl.GetMessage("TaiKhoanThieuThongTinAnh");
							LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals188.rsData), CS_0024_003C_003E8__locals188.rsData));
							return CS_0024_003C_003E8__locals188.rsData;
						}
						num = Constans.DISPLAY_RECTANGLE_TEXT;
					}
					flag2 = false;
					if (!flag && signPositionADOs != null && signPositionADOs.Count >= 2)
					{
						flag2 = true;
					}
					int num2 = 1;
					using (List<SignPositionADO>.Enumerator enumerator3 = signPositionADOs.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							_003C_003Ec__DisplayClass53 _003C_003Ec__DisplayClass = CS_0024_003C_003E8__locals193;
							_003C_003Ec__DisplayClass51 _003C_003Ec__DisplayClass2 = CS_0024_003C_003E8__locals188;
							SignPositionADO nSp = enumerator3.Current;
							if (documentTDO != null && !string.IsNullOrEmpty(documentTDO.DocumentCode) && signPositionADOs != null && signPositionADOs.Count >= 2)
							{
								signSelected = new EmrSign().GetSignDocumentFirst(documentTDO.DocumentCode, GetSignerData(), GetTreatmentData(), flag2 || flag, false);
							}
							LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.WidthRectangle), nSp.WidthRectangle) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.HeightRectangle), nSp.HeightRectangle) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.PageNUm), nSp.PageNUm) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => nSp.Text), nSp.Text));
							float left = nSp.Reactanle.Left;
							left = ((left < 0f) ? 0f : left);
							float bottom = nSp.Reactanle.Bottom;
							bottom = ((bottom < 0f) ? 0f : bottom);
							int numberOfPages = pdfReader.NumberOfPages;
							VerifyPdfFileHandle verifyPdfFileHandle = new VerifyPdfFileHandle();
							List<VerifierADO> verifiers = (from o in verifyPdfFileHandle.verify(pdfReader)
								orderby o.Date
								select o).ToList();
							SignHandle signHandle = null;
							CommonParam param = new CommonParam();
							DisplayConfigDTO displayConfigDTO = new DisplayConfigDTO();
							displayConfigDTO.HeightRectangle = ((nSp.HeightRectangle > 0f) ? new float?(nSp.HeightRectangle) : ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.HeightRectangle : ((float?)null)));
							displayConfigDTO.WidthRectangle = ((nSp.WidthRectangle > 0f) ? new float?(nSp.WidthRectangle) : ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.WidthRectangle : ((float?)null)));
							displayConfigDTO.SizeFont = ((nSp.SizeFont > 0) ? new int?(nSp.SizeFont) : ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.SizeFont : ((int?)null)));
							displayConfigDTO.TextPosition = ((nSp.TextPosition > Constans.TEXT_POSITON.x100) ? new int?((int)nSp.TextPosition) : ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.TextPosition : ((int?)null)));
							displayConfigDTO.TypeDisplay = ((num > 0) ? new int?(num) : ((nSp.TypeDisplay > 0) ? new int?(nSp.TypeDisplay) : ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.TypeDisplay : ((int?)null))));
							displayConfigDTO.IsDisplaySignature = (nSp.IsDisplaySignature.HasValue ? nSp.IsDisplaySignature : ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.IsDisplaySignature : ((bool?)null)));
							displayConfigDTO.FormatRectangleText = ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.FormatRectangleText)) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.FormatRectangleText : string.Empty);
							displayConfigDTO.Location = ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.Location)) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.Location : string.Empty);
							displayConfigDTO.Alignment = ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null && CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.Alignment.HasValue) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.Alignment : ((int?)null));
							displayConfigDTO.IsBold = ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null && CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.IsBold.HasValue) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.IsBold : ((bool?)null));
							displayConfigDTO.IsItalic = ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null && CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.IsItalic.HasValue) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.IsItalic : ((bool?)null));
							displayConfigDTO.IsUnderlined = ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null && CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.IsUnderlined.HasValue) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.IsUnderlined : ((bool?)null));
							displayConfigDTO.FontName = ((CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.FontName)) ? CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO.FontName : null);
							DisplayConfigDTO displayConfigDTO2 = displayConfigDTO;
							SignType signTypeConfig = GetSignTypeConfig(CS_0024_003C_003E8__locals188.inputADO);
							bool flag4 = flag;
							bool isMultiSign = (!flag2 || num2 != signPositionADOs.Count) && ((!flag2) ? flag : flag2);
							signHandle = new SignHandle(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork, left, bottom, nSp.PageNUm, numberOfPages, null, CS_0024_003C_003E8__locals188.inputADO.DlgOpenModuleConfig, CS_0024_003C_003E8__locals188.inputADO.DocumentName, CS_0024_003C_003E8__locals188.inputADO.Treatment.TREATMENT_CODE, signName, CS_0024_003C_003E8__locals188.inputADO.SignReason, verifiers, signTypeConfig, list, signSelected, isPatientSign, isHomeRelativeSign, documentTypeId, isMultiSign, CS_0024_003C_003E8__locals188.inputADO.HisCode, CS_0024_003C_003E8__locals188.inputADO, displayConfigDTO2, param, null, isSignParanel, GetTreatmentData(), GetSignerData(), GetTokenCodeData());
							bool flag5 = true;
							string message = "";
							if (!signHandle.VerifyFile(documentTDO, signedCount, ref message))
							{
								CS_0024_003C_003E8__locals188.rsData.Success = false;
								CS_0024_003C_003E8__locals188.rsData.Message = message;
							}
							else
							{
								_003C_003Ec__DisplayClass53 _003C_003Ec__DisplayClass3 = CS_0024_003C_003E8__locals193;
								_003C_003Ec__DisplayClass51 _003C_003Ec__DisplayClass4 = CS_0024_003C_003E8__locals188;
								if (documentTDO == null)
								{
									documentTDO = new DocumentTDO();
								}
								documentTDO.OriginalVersion = new VersionTDO();
								if (fileADO != null && !string.IsNullOrEmpty(fileADO.Base64FileContent))
								{
									documentTDO.OriginalVersion.Base64Data = fileADO.Base64FileContent;
								}
								if (fileADO3 != null && !string.IsNullOrEmpty(fileADO3.Base64FileContent))
								{
									documentTDO.OriginalVersion.Base64DataXml = fileADO3.Base64FileContent;
								}
								if (fileADO2 != null && !string.IsNullOrEmpty(fileADO2.Base64FileContent))
								{
									documentTDO.OriginalVersion.Base64DataJson = fileADO2.Base64FileContent;
								}
								bool rsSuccess = signHandle.SignFile(documentTDO, ref CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile);
								LogSystem.Info("SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => rsSuccess), rsSuccess) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile), CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork), CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork));
								if (rsSuccess)
								{
									CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success = rsSuccess;
									try
									{
										if (pdfReader != null)
										{
											pdfReader.Close();
										}
										if (File.Exists(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork))
										{
											File.Delete(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork);
										}
									}
									catch
									{
									}
									CS_0024_003C_003E8__locals188.rsData.Message = "OK";
									CS_0024_003C_003E8__locals188.rsData.Base64FileSigned = Utils.FileToBase64String(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile);
									CS_0024_003C_003E8__locals188.rsData.DocumentCode = documentTDO.DocumentCode;
									CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork = CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile;
								}
								CS_0024_003C_003E8__locals188.rsData.Success = CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success;
							}
							num2++;
						}
					}
					if (((signPositionADOs != null && signPositionADOs.Count > 0) & CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success) && File.Exists(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile))
					{
						LogSystem.Info("SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success), CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<string>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals193), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile));
						try
						{
							if (pdfReader != null)
							{
								pdfReader.Close();
							}
						}
						catch
						{
						}
						if (!CS_0024_003C_003E8__locals188.isSignOnlyWithHasAutoPosition)
						{
							CS_0024_003C_003E8__locals188.rsData.Message = "OK";
							CS_0024_003C_003E8__locals188.rsData.Base64FileSigned = Utils.FileToBase64String(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile);
							if (fileADO.FileType != FileType.Xml && fileADO.FileType != FileType.Json)
							{
								if (CS_0024_003C_003E8__locals188.isPrintNow)
								{
									PrintNowProcess(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile);
								}
								else if (CS_0024_003C_003E8__locals188.isPrintPreview)
								{
									PrintPreviewProcess(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile);
								}
							}
						}
						CS_0024_003C_003E8__locals188.rsData.Success = CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success;
					}
				}
				else if (fileADO.FileType == FileType.Xml || fileADO.FileType == FileType.Json)
				{
					SignHandle signHandle2 = null;
					CommonParam param2 = new CommonParam();
					DisplayConfigDTO displayConfigDTO3 = CS_0024_003C_003E8__locals188.inputADO.DisplayConfigDTO;
					SignType signTypeConfig2 = GetSignTypeConfig(CS_0024_003C_003E8__locals188.inputADO);
					bool isMultiSign2 = flag;
					signHandle2 = new SignHandle(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork, 1f, 1f, 1, 1, null, CS_0024_003C_003E8__locals188.inputADO.DlgOpenModuleConfig, CS_0024_003C_003E8__locals188.inputADO.DocumentName, CS_0024_003C_003E8__locals188.inputADO.Treatment.TREATMENT_CODE, signName, CS_0024_003C_003E8__locals188.inputADO.SignReason, null, signTypeConfig2, list, signSelected, isPatientSign, isHomeRelativeSign, documentTypeId, isMultiSign2, CS_0024_003C_003E8__locals188.inputADO.HisCode, CS_0024_003C_003E8__locals188.inputADO, displayConfigDTO3, param2, null, isSignParanel, GetTreatmentData(), GetSignerData(), GetTokenCodeData());
					bool flag6 = true;
					string message2 = "";
					if (!signHandle2.VerifyFile(documentTDO, signedCount, ref message2))
					{
						CS_0024_003C_003E8__locals188.rsData.Success = false;
						CS_0024_003C_003E8__locals188.rsData.Message = message2;
					}
					else
					{
						_003C_003Ec__DisplayClass53 _003C_003Ec__DisplayClass5 = CS_0024_003C_003E8__locals193;
						_003C_003Ec__DisplayClass51 _003C_003Ec__DisplayClass6 = CS_0024_003C_003E8__locals188;
						if (documentTDO == null)
						{
							documentTDO = new DocumentTDO();
						}
						documentTDO.OriginalVersion = new VersionTDO();
						if (fileADO != null && !string.IsNullOrEmpty(fileADO.Base64FileContent))
						{
							documentTDO.OriginalVersion.Base64Data = fileADO.Base64FileContent;
						}
						if (fileADO3 != null && !string.IsNullOrEmpty(fileADO3.Base64FileContent))
						{
							documentTDO.OriginalVersion.Base64DataXml = fileADO3.Base64FileContent;
						}
						if (fileADO2 != null && !string.IsNullOrEmpty(fileADO2.Base64FileContent))
						{
							documentTDO.OriginalVersion.Base64DataJson = fileADO2.Base64FileContent;
						}
						signHandle2.SetFileType(fileADO.FileType);
						bool rsSuccess2 = signHandle2.SignFile(documentTDO, ref CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile);
						LogSystem.Info("SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => rsSuccess2), rsSuccess2) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile), CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork), CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork));
						if (rsSuccess2)
						{
							CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success = rsSuccess2;
							try
							{
								if (File.Exists(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork))
								{
									File.Delete(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork);
								}
							}
							catch
							{
							}
							CS_0024_003C_003E8__locals188.rsData.Message = "OK";
							CS_0024_003C_003E8__locals188.rsData.Base64FileSigned = Utils.FileToBase64String(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile);
							CS_0024_003C_003E8__locals188.rsData.DocumentCode = documentTDO.DocumentCode;
							CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork = CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.outputFile;
						}
						CS_0024_003C_003E8__locals188.rsData.Success = CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.success;
					}
				}
				else if (!CS_0024_003C_003E8__locals188.isSignOnlyWithHasAutoPosition)
				{
					frmPdfViewer frmPdfViewer2 = new frmPdfViewer(CS_0024_003C_003E8__locals193.CS_0024_003C_003E8__locals44.inputFileWork, CS_0024_003C_003E8__locals188.inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData(), true, ActionAfterSignedProcess, CS_0024_003C_003E8__locals188.isPrintNow);
					frmPdfViewer2.UpdateExtFileType(fileADO, fileADO2, fileADO3);
					frmPdfViewer2.ShowDialog();
					CS_0024_003C_003E8__locals188.rsData.Success = !string.IsNullOrEmpty(outputSignedFileResult);
					if (CS_0024_003C_003E8__locals188.rsData.Success)
					{
						CS_0024_003C_003E8__locals188.rsData.Message = "OK";
						CS_0024_003C_003E8__locals188.rsData.Base64FileSigned = Utils.FileToBase64String(outputSignedFileResult);
						CS_0024_003C_003E8__locals188.rsData.DocumentCode = CS_0024_003C_003E8__locals188.inputADO.DocumentCode;
					}
				}
				else
				{
					CS_0024_003C_003E8__locals188.rsData.Success = false;
					CS_0024_003C_003E8__locals188.rsData.Message = "Văn bản mã " + ((!string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals188.inputADO.DocumentCode)) ? CS_0024_003C_003E8__locals188.inputADO.DocumentCode : CS_0024_003C_003E8__locals188.rsData.DocumentCode) + " chưa được thiết lập vị trí ký";
				}
				LogSystem.Info("ProcessSignPrint" + (CS_0024_003C_003E8__locals188.isPrintPreview ? "Preview" : "Now") + ". 2");
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn("Co loi xay ra. Du lieu dau vao:____" + LogUtil.TraceData("fileADOs", fileADOs) + "____" + LogUtil.TraceData("inputADO", CS_0024_003C_003E8__locals188.inputADO));
				LogSystem.Error(ex);
				CS_0024_003C_003E8__locals188.rsData.Success = false;
			}
			try
			{
				outputSignedFileResult = null;
				inputADOWorking = null;
				signToken = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
			return CS_0024_003C_003E8__locals188.rsData;
		}

		private SignType GetSignTypeConfig(InputADO inputADO)
		{
			SignType signType = SignType.HMS;
			if (inputADO.SignType == SignType.OptionDefaultHsm || inputADO.SignType == SignType.OptionDefaultUsb)
			{
				string value = CacheClientWorker.GetValue("SignTypeOption");
				if (!string.IsNullOrEmpty(value))
				{
					return (value == "1") ? SignType.USB : SignType.HMS;
				}
				if (inputADO.IsOptionSignType.HasValue)
				{
					return inputADO.IsOptionSignType.Value ? SignType.USB : SignType.HMS;
				}
				if (inputADO.SignType == SignType.OptionDefaultUsb)
				{
					return SignType.USB;
				}
				return SignType.HMS;
			}
			return inputADO.SignType;
		}

		public DocumentSignedResultDTO SignWithListKeyAndUser(string base64FileContent, FileType fileType, InputADO inputADO)
		{
			_003C_003Ec__DisplayClass61 CS_0024_003C_003E8__locals107 = new _003C_003Ec__DisplayClass61();
			CS_0024_003C_003E8__locals107.rsData = new DocumentSignedResultDTO();
			outputSignedFileResult = "";
			iSPrintNow = false;
			iSPrintPreview = false;
			int num = 0;
			try
			{
				_003C_003Ec__DisplayClass63 CS_0024_003C_003E8__locals116 = new _003C_003Ec__DisplayClass63();
				CS_0024_003C_003E8__locals116.CS_0024_003C_003E8__locals62 = CS_0024_003C_003E8__locals107;
				if (string.IsNullOrEmpty(base64FileContent))
				{
					CS_0024_003C_003E8__locals107.rsData.Success = false;
					CS_0024_003C_003E8__locals107.rsData.Message = MessageUitl.GetMessage("DuLieuKhongHopLe");
					LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals107.rsData), CS_0024_003C_003E8__locals107.rsData));
					MessageBox.Show(CS_0024_003C_003E8__locals107.rsData.Message);
					return CS_0024_003C_003E8__locals107.rsData;
				}
				V_EMR_DOCUMENT documentData = new V_EMR_DOCUMENT();
				if (!ValidParam(inputADO, true, true, ref base64FileContent, ref documentData) || inputADO.SignerConfigs == null || inputADO.SignerConfigs.Count == 0)
				{
					CS_0024_003C_003E8__locals107.rsData.Success = false;
					CS_0024_003C_003E8__locals107.rsData.Message = MessageUitl.GetMessage("DuLieuKhongHopLe");
					LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals107.rsData), CS_0024_003C_003E8__locals107.rsData));
					return CS_0024_003C_003E8__locals107.rsData;
				}
				CS_0024_003C_003E8__locals116.inputFileWork = "";
				string extByFileType = Utils.GetExtByFileType(fileType);
				byte[] arrInFile = Convert.FromBase64String(base64FileContent);
				string text = Utils.GenerateTempFileWithin(extByFileType);
				Utils.ByteToFile(arrInFile, text);
				Utils.ProcessFileInput(text, extByFileType, ref CS_0024_003C_003E8__locals116.inputFileWork);
				CS_0024_003C_003E8__locals116.success = false;
				bool flag = false;
				bool flag2 = false;
				bool isSignParanel = false;
				DocumentTDO documentTDO = null;
				int signedCount = 0;
				EMR_SIGN signSelected = null;
				PdfReader pdfReader = new PdfReader(CS_0024_003C_003E8__locals116.inputFileWork);
				List<SignPositionADO> signPositionADOs = null;
				ProcessListSignWithUserKey(inputADO, ref CS_0024_003C_003E8__locals116.inputFileWork, ref signPositionADOs);
				if (signPositionADOs != null && signPositionADOs.Count > 0)
				{
					pdfReader.Close();
					pdfReader = new PdfReader(CS_0024_003C_003E8__locals116.inputFileWork);
				}
				signPositionADOs = GetNextPositionSigned(inputADO.IsPatientSign || inputADO.IsHomeRelativeSign == true, pdfReader, signSelected, ref signedCount);
				printNumberCopies = (short)((!inputADO.PrintNumberCopies.HasValue) ? 1 : inputADO.PrintNumberCopies.Value);
				if (signPositionADOs != null && signPositionADOs.Count > 0)
				{
					_003C_003Ec__DisplayClass65 CS_0024_003C_003E8__locals114 = new _003C_003Ec__DisplayClass65();
					CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals64 = CS_0024_003C_003E8__locals116;
					CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals62 = CS_0024_003C_003E8__locals107;
					CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19 = new _003C_003Ec__DisplayClass46_2();
					flag = GetMultiSignDoc(inputADO, ref isSignParanel);
					if (!string.IsNullOrEmpty(inputADO.DocumentCode))
					{
						documentTDO = GenerateByDocumentCode(inputADO.DocumentCode, ref flag);
						isSignParanel = documentTDO != null && documentTDO.IsSignParallel.HasValue && documentTDO.IsSignParallel.Value;
					}
					SignPositionADO signPositionADO = signPositionADOs[0];
					if (signPositionADOs.Count > 1)
					{
						signPositionADO.SignPositionAutos = signPositionADOs;
					}
					List<SignTDO> list = null;
					isSignParanel = true;
					if (inputADO.SignerConfigs != null && inputADO.SignerConfigs.Count > 0)
					{
						inputADO.SignerConfigs = inputADO.SignerConfigs.OrderBy((SignerConfigDTO o) => o.NumOrder).ToList();
						list = new List<SignTDO>();
						foreach (SignerConfigDTO signerConfig in inputADO.SignerConfigs)
						{
							SignTDO signTDO = new SignTDO();
							EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(signerConfig.Loginname);
							if (byLoginName != null)
							{
								signTDO.SignerId = byLoginName.ID;
								signTDO.Loginname = byLoginName.LOGINNAME;
								signTDO.Username = byLoginName.USERNAME;
								signTDO.FullName = byLoginName.USERNAME;
								signTDO.FirstName = byLoginName.USERNAME;
								if (signerConfig.NumOrder > 0)
								{
									signTDO.NumOrder = signerConfig.NumOrder;
								}
								else
								{
									signTDO.NumOrder = GetMaxNumOrder(list);
								}
								signTDO.Title = byLoginName.TITLE;
								signTDO.DepartmentCode = byLoginName.DEPARTMENT_CODE;
								signTDO.DepartmentName = byLoginName.DEPARTMENT_NAME;
								signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
								list.Add(signTDO);
							}
						}
					}
					bool isPatientSign = inputADO.IsPatientSign;
					bool isHomeRelativeSign = inputADO.IsHomeRelativeSign.HasValue && inputADO.IsHomeRelativeSign.Value;
					CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile = "";
					flag2 = false;
					if (!flag && signPositionADOs != null && signPositionADOs.Count >= 2)
					{
						flag2 = true;
					}
					int num2 = 1;
					using (List<SignPositionADO>.Enumerator enumerator2 = signPositionADOs.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							_003C_003Ec__DisplayClass68 CS_0024_003C_003E8__locals113 = new _003C_003Ec__DisplayClass68();
							CS_0024_003C_003E8__locals113.CS_0024_003C_003E8__locals66 = CS_0024_003C_003E8__locals114;
							CS_0024_003C_003E8__locals113.CS_0024_003C_003E8__locals64 = CS_0024_003C_003E8__locals116;
							CS_0024_003C_003E8__locals113.CS_0024_003C_003E8__locals62 = CS_0024_003C_003E8__locals107;
							CS_0024_003C_003E8__locals113.nSp = enumerator2.Current;
							_003C_003Ec__DisplayClass6a CS_0024_003C_003E8__locals109 = new _003C_003Ec__DisplayClass6a();
							CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals69 = CS_0024_003C_003E8__locals113;
							CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals66 = CS_0024_003C_003E8__locals114;
							CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals64 = CS_0024_003C_003E8__locals116;
							CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals62 = CS_0024_003C_003E8__locals107;
							CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18 = new _003C_003Ec__DisplayClass46_4();
							_003C_003Ec__DisplayClass46_4 _003C_003Ec__DisplayClass46_ = CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18;
							List<SignerConfigDTO> signerConfigs = inputADO.SignerConfigs;
							Func<SignerConfigDTO, bool> predicate = (SignerConfigDTO o) => o.NumOrder == VerifySign.GetNumOderByCommentText(CS_0024_003C_003E8__locals113.nSp.Text);
							_003C_003Ec__DisplayClass46_.scf = signerConfigs.Where(predicate).FirstOrDefault();
							EMR_SIGNER byLoginName2 = GlobalStore.GetByLoginName(CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18.scf.Loginname);
							CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18.vOptionSign = VerifySign.VerifySignImageWithOption(inputADO, byLoginName2, signPositionADOs != null && signPositionADOs.Count > 0, signPositionADO, flag);
							if (CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18.vOptionSign.HasValue)
							{
								if (!CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18.vOptionSign.Value)
								{
									LogSystem.Warn(MessageUitl.GetMessage("TaiKhoanThieuThongTinAnh") + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18.vOptionSign), CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18.vOptionSign) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<SignerConfigDTO>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals109), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals109.CS_0024_003C_003E8__locals18.scf));
									continue;
								}
								num = Constans.DISPLAY_RECTANGLE_TEXT;
							}
							if (documentTDO != null && !string.IsNullOrEmpty(documentTDO.DocumentCode) && signPositionADOs != null && signPositionADOs.Count >= 2)
							{
								signSelected = new EmrSign().GetSignDocumentFirst(documentTDO.DocumentCode, byLoginName2, GetTreatmentData(), flag2 || flag, false);
							}
							LogSystem.Debug("SignWithListKeyAndUser: Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals113.nSp.WidthRectangle), CS_0024_003C_003E8__locals113.nSp.WidthRectangle) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals113.nSp.HeightRectangle), CS_0024_003C_003E8__locals113.nSp.HeightRectangle) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals113.nSp.PageNUm), CS_0024_003C_003E8__locals113.nSp.PageNUm) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals113.nSp.Text), CS_0024_003C_003E8__locals113.nSp.Text));
							float left = CS_0024_003C_003E8__locals113.nSp.Reactanle.Left;
							left = ((left < 0f) ? 0f : left);
							float bottom = CS_0024_003C_003E8__locals113.nSp.Reactanle.Bottom;
							bottom = ((bottom < 0f) ? 0f : bottom);
							int numberOfPages = pdfReader.NumberOfPages;
							string signName = (string.IsNullOrEmpty(GlobalStore.UserName) ? GlobalStore.LoginName : (GlobalStore.LoginName + " (" + GlobalStore.UserName + ")"));
							VerifyPdfFileHandle verifyPdfFileHandle = new VerifyPdfFileHandle();
							List<VerifierADO> verifiers = (from o in verifyPdfFileHandle.verify(pdfReader)
								orderby o.Date
								select o).ToList();
							long? documentTypeId = GetDocumentTypeId(inputADO.DocumentTypeCode);
							SignHandle signHandle = null;
							CommonParam param = new CommonParam();
							DisplayConfigDTO displayConfigDTO = new DisplayConfigDTO();
							displayConfigDTO.HeightRectangle = ((CS_0024_003C_003E8__locals113.nSp.HeightRectangle > 0f) ? new float?(CS_0024_003C_003E8__locals113.nSp.HeightRectangle) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.HeightRectangle : ((float?)null)));
							displayConfigDTO.WidthRectangle = ((CS_0024_003C_003E8__locals113.nSp.WidthRectangle > 0f) ? new float?(CS_0024_003C_003E8__locals113.nSp.WidthRectangle) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.WidthRectangle : ((float?)null)));
							displayConfigDTO.SizeFont = ((CS_0024_003C_003E8__locals113.nSp.SizeFont > 0) ? new int?(CS_0024_003C_003E8__locals113.nSp.SizeFont) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.SizeFont : ((int?)null)));
							displayConfigDTO.TextPosition = ((CS_0024_003C_003E8__locals113.nSp.TextPosition > Constans.TEXT_POSITON.x100) ? new int?((int)CS_0024_003C_003E8__locals113.nSp.TextPosition) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.TextPosition : ((int?)null)));
							displayConfigDTO.TypeDisplay = ((num > 0) ? new int?(num) : ((CS_0024_003C_003E8__locals113.nSp.TypeDisplay > 0) ? new int?(CS_0024_003C_003E8__locals113.nSp.TypeDisplay) : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.TypeDisplay : ((int?)null))));
							displayConfigDTO.IsDisplaySignature = (CS_0024_003C_003E8__locals113.nSp.IsDisplaySignature.HasValue ? CS_0024_003C_003E8__locals113.nSp.IsDisplaySignature : ((inputADO.DisplayConfigDTO != null) ? inputADO.DisplayConfigDTO.IsDisplaySignature : ((bool?)null)));
							displayConfigDTO.FormatRectangleText = ((inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADO.DisplayConfigDTO.FormatRectangleText)) ? inputADO.DisplayConfigDTO.FormatRectangleText : string.Empty);
							displayConfigDTO.Location = ((inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADO.DisplayConfigDTO.Location)) ? inputADO.DisplayConfigDTO.Location : string.Empty);
							displayConfigDTO.Alignment = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.Alignment.HasValue) ? inputADO.DisplayConfigDTO.Alignment : ((int?)null));
							displayConfigDTO.IsBold = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.IsBold.HasValue) ? inputADO.DisplayConfigDTO.IsBold : ((bool?)null));
							displayConfigDTO.IsItalic = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.IsItalic.HasValue) ? inputADO.DisplayConfigDTO.IsItalic : ((bool?)null));
							displayConfigDTO.IsUnderlined = ((inputADO.DisplayConfigDTO != null && inputADO.DisplayConfigDTO.IsUnderlined.HasValue) ? inputADO.DisplayConfigDTO.IsUnderlined : ((bool?)null));
							displayConfigDTO.FontName = ((inputADO.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADO.DisplayConfigDTO.FontName)) ? inputADO.DisplayConfigDTO.FontName : null);
							DisplayConfigDTO displayConfigDTO2 = displayConfigDTO;
							SignType signType = SignType.HMS;
							if (inputADO.SignType == SignType.OptionDefaultHsm || inputADO.SignType == SignType.OptionDefaultUsb)
							{
								string value = CacheClientWorker.GetValue("SignTypeOption");
								signType = (string.IsNullOrEmpty(value) ? ((!inputADO.IsOptionSignType.HasValue) ? ((inputADO.SignType == SignType.OptionDefaultUsb) ? SignType.USB : SignType.HMS) : (inputADO.IsOptionSignType.Value ? SignType.USB : SignType.HMS)) : ((value == "1") ? SignType.USB : SignType.HMS));
							}
							else
							{
								signType = inputADO.SignType;
							}
							bool flag3 = flag;
							bool isMultiSign = (!flag2 || num2 != signPositionADOs.Count) && ((!flag2) ? flag : flag2);
							signHandle = new SignHandle(CS_0024_003C_003E8__locals116.inputFileWork, left, bottom, CS_0024_003C_003E8__locals113.nSp.PageNUm, numberOfPages, null, inputADO.DlgOpenModuleConfig, inputADO.DocumentName, inputADO.Treatment.TREATMENT_CODE, signName, inputADO.SignReason, verifiers, signType, list, signSelected, isPatientSign, isHomeRelativeSign, documentTypeId, isMultiSign, inputADO.HisCode, inputADO, displayConfigDTO2, param, null, isSignParanel, GetTreatmentData(), byLoginName2, GetTokenCodeData());
							bool flag4 = true;
							string message = "";
							if (!signHandle.VerifyFile(documentTDO, signedCount, ref message))
							{
								CS_0024_003C_003E8__locals107.rsData.Success = false;
								CS_0024_003C_003E8__locals107.rsData.Message = message;
							}
							else
							{
								_003C_003Ec__DisplayClass6a _003C_003Ec__DisplayClass6a = CS_0024_003C_003E8__locals109;
								_003C_003Ec__DisplayClass68 _003C_003Ec__DisplayClass = CS_0024_003C_003E8__locals113;
								_003C_003Ec__DisplayClass65 _003C_003Ec__DisplayClass2 = CS_0024_003C_003E8__locals114;
								_003C_003Ec__DisplayClass63 _003C_003Ec__DisplayClass3 = CS_0024_003C_003E8__locals116;
								_003C_003Ec__DisplayClass61 _003C_003Ec__DisplayClass4 = CS_0024_003C_003E8__locals107;
								if (documentTDO == null)
								{
									documentTDO = new DocumentTDO();
								}
								bool rsSuccess = signHandle.SignFile(documentTDO, ref CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile);
								LogSystem.Info("SignWithListKeyAndUser.SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => rsSuccess), rsSuccess) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile), CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals116.inputFileWork), CS_0024_003C_003E8__locals116.inputFileWork));
								if (rsSuccess)
								{
									CS_0024_003C_003E8__locals116.success = rsSuccess;
									try
									{
										if (pdfReader != null)
										{
											pdfReader.Close();
										}
										if (File.Exists(CS_0024_003C_003E8__locals116.inputFileWork))
										{
											File.Delete(CS_0024_003C_003E8__locals116.inputFileWork);
										}
									}
									catch
									{
									}
									CS_0024_003C_003E8__locals107.rsData.Message = "OK";
									CS_0024_003C_003E8__locals107.rsData.Base64FileSigned = Utils.FileToBase64String(CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile);
									CS_0024_003C_003E8__locals107.rsData.DocumentCode = documentTDO.DocumentCode;
									CS_0024_003C_003E8__locals116.inputFileWork = CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile;
								}
								CS_0024_003C_003E8__locals107.rsData.Success = CS_0024_003C_003E8__locals116.success;
							}
							num2++;
						}
					}
					if (signPositionADOs != null && signPositionADOs.Count > 0 && CS_0024_003C_003E8__locals116.success && File.Exists(CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile))
					{
						LogSystem.Info("SignWithListKeyAndUser.SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals116.success), CS_0024_003C_003E8__locals116.success) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<string>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals114), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals114.CS_0024_003C_003E8__locals19.outputFile));
						try
						{
							if (pdfReader != null)
							{
								pdfReader.Close();
							}
						}
						catch
						{
						}
						CS_0024_003C_003E8__locals107.rsData.Success = CS_0024_003C_003E8__locals116.success;
					}
				}
				else
				{
					CS_0024_003C_003E8__locals107.rsData.Success = false;
					CS_0024_003C_003E8__locals107.rsData.Message = "Văn bản mã " + ((!string.IsNullOrWhiteSpace(inputADO.DocumentCode)) ? inputADO.DocumentCode : CS_0024_003C_003E8__locals107.rsData.DocumentCode) + " chưa được thiết lập vị trí ký";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Error(ex);
				CS_0024_003C_003E8__locals107.rsData.Success = false;
			}
			return CS_0024_003C_003E8__locals107.rsData;
		}

		private void ProcessCommentKey(InputADO inputADOWorking, ref string inputFileWork, ref List<SignPositionADO> signPositionADOs)
		{
			try
			{
				if (inputADOWorking == null || !inputADOWorking.IsSign)
				{
					return;
				}
				string outFile = Utils.GenerateTempFileWithin();
				PdfCommentKeyProcess pdfCommentKeyProcess = new PdfCommentKeyProcess();
				List<SignPositionADO> list = pdfCommentKeyProcess.Run(inputFileWork, ref outFile);
				if (list != null && list.Count > 0 && !string.IsNullOrEmpty(outFile) && File.Exists(outFile))
				{
					try
					{
						File.Delete(inputFileWork);
					}
					catch
					{
					}
					inputFileWork = outFile;
					if (signPositionADOs == null)
					{
						signPositionADOs = new List<SignPositionADO>();
					}
					signPositionADOs.AddRange(list);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessListSignWithUserKey(InputADO inputADOWorking, ref string inputFileWork, ref List<SignPositionADO> signPositionADOs)
		{
			try
			{
				if (inputADOWorking == null || !inputADOWorking.IsSign)
				{
					return;
				}
				string outFile = Utils.GenerateTempFileWithin();
				PdfCommentKeyProcess pdfCommentKeyProcess = new PdfCommentKeyProcess();
				List<SignPositionADO> list = pdfCommentKeyProcess.RunWithUserKey(inputFileWork, ref outFile);
				if (list != null && list.Count > 0 && !string.IsNullOrEmpty(outFile) && File.Exists(outFile))
				{
					try
					{
						File.Delete(inputFileWork);
					}
					catch
					{
					}
					inputFileWork = outFile;
					if (signPositionADOs == null)
					{
						signPositionADOs = new List<SignPositionADO>();
					}
					signPositionADOs.AddRange(list);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public bool CreateDocument(InputADO inputADO, string base64FileContent)
		{
			bool result = false;
			try
			{
				if (!ValidParam(inputADO))
				{
					result = false;
					LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					return result;
				}
				if (string.IsNullOrEmpty(base64FileContent))
				{
					result = false;
					LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe") + ". Du lieu file truyen vao khong hop le:____");
					return result;
				}
				string inputFileWork = "";
				byte[] arrInFile = Convert.FromBase64String(base64FileContent);
				string extByFileType = Utils.GetExtByFileType(FileType.Pdf);
				string text = Utils.GenerateTempFileWithin(extByFileType);
				Utils.ByteToFile(arrInFile, text);
				Utils.ProcessFileInput(text, extByFileType, ref inputFileWork);
				string signName = (string.IsNullOrEmpty(GlobalStore.UserName) ? GlobalStore.LoginName : (GlobalStore.LoginName + " (" + GlobalStore.UserName + ")"));
				List<SignTDO> list = null;
				if (inputADO.SignerConfigs != null && inputADO.SignerConfigs.Count > 0)
				{
					inputADO.SignerConfigs = inputADO.SignerConfigs.OrderBy((SignerConfigDTO o) => o.NumOrder).ToList();
					list = new List<SignTDO>();
					foreach (SignerConfigDTO signerConfig in inputADO.SignerConfigs)
					{
						SignTDO signTDO = new SignTDO();
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(signerConfig.Loginname);
						if (byLoginName != null)
						{
							signTDO.SignerId = byLoginName.ID;
							signTDO.Loginname = byLoginName.LOGINNAME;
							signTDO.Username = byLoginName.USERNAME;
							signTDO.FullName = byLoginName.USERNAME;
							signTDO.FirstName = byLoginName.USERNAME;
							if (signerConfig.NumOrder > 0)
							{
								signTDO.NumOrder = signerConfig.NumOrder;
							}
							else
							{
								signTDO.NumOrder = GetMaxNumOrder(list);
							}
							signTDO.Title = byLoginName.TITLE;
							signTDO.DepartmentCode = byLoginName.DEPARTMENT_CODE;
							signTDO.DepartmentName = byLoginName.DEPARTMENT_NAME;
							signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							list.Add(signTDO);
						}
					}
				}
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				EMR_DOCUMENT_TYPE eMR_DOCUMENT_TYPE = new EmrDocumentType().DocumentTypeProperty(inputADO.DocumentTypeCode);
				flag2 = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_MULTI_SIGN == 1;
				flag = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_SIGN_PARALLEL == 1;
				flag2 = flag3;
				long? documentTypeId = ((eMR_DOCUMENT_TYPE != null) ? new long?(eMR_DOCUMENT_TYPE.ID) : ((long?)null));
				if (inputADO.IsMultiSign.HasValue)
				{
					flag2 = flag2 && inputADO.IsMultiSign.Value;
				}
				CommonParam commonParam = new CommonParam();
				SignHandle signHandle = new SignHandle(inputFileWork, 0f, 0f, 0, 0, null, null, inputADO.DocumentName, inputADO.Treatment.TREATMENT_CODE, signName, inputADO.SignReason, null, inputADO.SignType, list, null, inputADO.IsPatientSign, false, documentTypeId, flag2, inputADO.HisCode, inputADO, null, commonParam, null, flag, GetTreatmentData(), GetSignerData(), GetTokenCodeData());
				DocumentTDO documentTDO = new DocumentTDO();
				documentTDO.IsSignParallel = flag;
				DocumentTDO documentTDO2 = signHandle.SendDocument(documentTDO);
				if (documentTDO2 != null && !string.IsNullOrEmpty(documentTDO2.DocumentCode))
				{
					result = true;
					inputADO.DocumentCode = documentTDO2.DocumentCode;
				}
				else
				{
					commonParam.Messages.Add("Xử lý thất bại");
					MessageManager.Show(commonParam, false);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Co loi xay ra. Du lieu dau vao:____" + LogUtil.TraceData("inputADO", inputADO));
				LogSystem.Error(ex);
				result = false;
			}
			return result;
		}

		public bool CreateDocument(InputADO inputADO, FileType fileType, string base64FileContent)
		{
			bool result = false;
			try
			{
				if (!ValidParam(inputADO))
				{
					result = false;
					LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe") + ". Du lieu dau vao:____" + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
					return result;
				}
				if (string.IsNullOrEmpty(base64FileContent))
				{
					result = false;
					LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe") + ". Du lieu file truyen vao khong hop le:____" + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
					return result;
				}
				string inputFileWork = "";
				byte[] arrInFile = Convert.FromBase64String(base64FileContent);
				string extByFileType = Utils.GetExtByFileType(fileType);
				string text = Utils.GenerateTempFileWithin(extByFileType);
				Utils.ByteToFile(arrInFile, text);
				Utils.ProcessFileInput(text, extByFileType, ref inputFileWork);
				string signName = (string.IsNullOrEmpty(GlobalStore.UserName) ? GlobalStore.LoginName : (GlobalStore.LoginName + " (" + GlobalStore.UserName + ")"));
				List<SignTDO> list = null;
				if (inputADO.SignerConfigs != null && inputADO.SignerConfigs.Count > 0)
				{
					inputADO.SignerConfigs = inputADO.SignerConfigs.OrderBy((SignerConfigDTO o) => o.NumOrder).ToList();
					list = new List<SignTDO>();
					foreach (SignerConfigDTO signerConfig in inputADO.SignerConfigs)
					{
						SignTDO signTDO = new SignTDO();
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(signerConfig.Loginname);
						if (byLoginName != null)
						{
							signTDO.SignerId = byLoginName.ID;
							signTDO.Loginname = byLoginName.LOGINNAME;
							signTDO.Username = byLoginName.USERNAME;
							signTDO.FullName = byLoginName.USERNAME;
							signTDO.FirstName = byLoginName.USERNAME;
							if (signerConfig.NumOrder > 0)
							{
								signTDO.NumOrder = signerConfig.NumOrder;
							}
							else
							{
								signTDO.NumOrder = GetMaxNumOrder(list);
							}
							signTDO.Title = byLoginName.TITLE;
							signTDO.DepartmentCode = byLoginName.DEPARTMENT_CODE;
							signTDO.DepartmentName = byLoginName.DEPARTMENT_NAME;
							signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							list.Add(signTDO);
						}
					}
				}
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				EMR_DOCUMENT_TYPE eMR_DOCUMENT_TYPE = new EmrDocumentType().DocumentTypeProperty(inputADO.DocumentTypeCode);
				flag2 = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_MULTI_SIGN == 1;
				flag = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_SIGN_PARALLEL == 1;
				flag2 = flag3;
				long? documentTypeId = ((eMR_DOCUMENT_TYPE != null) ? new long?(eMR_DOCUMENT_TYPE.ID) : ((long?)null));
				if (inputADO.IsMultiSign.HasValue)
				{
					flag2 = flag2 && inputADO.IsMultiSign.Value;
				}
				CommonParam commonParam = new CommonParam();
				SignHandle signHandle = new SignHandle(inputFileWork, 0f, 0f, 0, 0, null, null, inputADO.DocumentName, inputADO.Treatment.TREATMENT_CODE, signName, inputADO.SignReason, null, inputADO.SignType, list, null, inputADO.IsPatientSign, false, documentTypeId, flag2, inputADO.HisCode, inputADO, null, commonParam, null, flag, GetTreatmentData(), GetSignerData(), GetTokenCodeData());
				DocumentTDO documentTDO = new DocumentTDO();
				documentTDO.IsSignParallel = flag;
				signHandle.SetFileType(fileType);
				DocumentTDO documentTDO2 = signHandle.SendDocument(documentTDO);
				if (documentTDO2 != null && !string.IsNullOrEmpty(documentTDO2.DocumentCode))
				{
					result = true;
					inputADO.DocumentCode = documentTDO2.DocumentCode;
				}
				else
				{
					commonParam.Messages.Add("Xử lý thất bại");
					MessageManager.Show(commonParam, false);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Co loi xay ra. Du lieu dau vao:____" + LogUtil.TraceData("inputADO", inputADO));
				LogSystem.Error(ex);
				result = false;
			}
			return result;
		}

		public bool CreateDocument(InputADO inputADO, List<FileADO> fileADOs)
		{
			return CreateDocument(inputADO, fileADOs, true);
		}

		public bool CreateDocument(InputADO inputADO, List<FileADO> fileADOs, bool isShowMessage)
		{
			bool flag = false;
			try
			{
				string err = "";
				if (!Verify.VerifySignPrintNow(fileADOs, ref err))
				{
					flag = false;
					LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					if (isShowMessage)
					{
						MessageManager.ShowAlert(Form.ActiveForm, MessageUitl.GetMessage("ThongBao"), MessageUitl.GetMessage("DuLieuKhongHopLe"));
					}
					return flag;
				}
				FileADO fileADO = null;
				FileADO fileADO2 = null;
				FileADO fileADO3 = null;
				if (!ValidParam(inputADO))
				{
					flag = false;
					LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe") + ". Du lieu dau vao:____" + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
					if (isShowMessage)
					{
						MessageManager.ShowAlert(Form.ActiveForm, MessageUitl.GetMessage("ThongBao"), MessageUitl.GetMessage("DuLieuKhongHopLe"));
					}
					return flag;
				}
				CommonParam commonParam = new CommonParam();
				foreach (FileADO fileADO4 in fileADOs)
				{
					if (string.IsNullOrEmpty(fileADO4.Base64FileContent))
					{
						flag = false;
						LogSystem.Warn(MessageUitl.GetMessage("DuLieuKhongHopLe") + ". Du lieu file truyen vao khong hop le:____" + LogUtil.TraceData(LogUtil.GetMemberName(() => inputADO), inputADO));
						return flag;
					}
					if (fileADO4.IsMain.HasValue && fileADO4.IsMain.Value)
					{
						fileADO = fileADO4;
					}
					else if (fileADO4.FileType == FileType.Json)
					{
						fileADO2 = fileADO4;
					}
					else if (fileADO4.FileType == FileType.Xml)
					{
						fileADO3 = fileADO4;
					}
				}
				if (fileADO == null && fileADOs.Count > 1)
				{
					fileADO = fileADOs.FirstOrDefault();
				}
				else if (fileADO == null && fileADOs.Count == 1)
				{
					fileADO = fileADOs.FirstOrDefault();
					fileADO2 = null;
					fileADO3 = null;
				}
				string inputFileWork = "";
				byte[] arrInFile = Convert.FromBase64String(fileADO.Base64FileContent);
				string extByFileType = Utils.GetExtByFileType(fileADO.FileType);
				string text = Utils.GenerateTempFileWithin(extByFileType);
				Utils.ByteToFile(arrInFile, text);
				Utils.ProcessFileInput(text, extByFileType, ref inputFileWork);
				string signName = (string.IsNullOrEmpty(GlobalStore.UserName) ? GlobalStore.LoginName : (GlobalStore.LoginName + " (" + GlobalStore.UserName + ")"));
				List<SignTDO> list = null;
				if (inputADO.SignerConfigs != null && inputADO.SignerConfigs.Count > 0)
				{
					inputADO.SignerConfigs = inputADO.SignerConfigs.OrderBy((SignerConfigDTO o) => o.NumOrder).ToList();
					list = new List<SignTDO>();
					foreach (SignerConfigDTO signerConfig in inputADO.SignerConfigs)
					{
						SignTDO signTDO = new SignTDO();
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(signerConfig.Loginname);
						if (byLoginName != null)
						{
							signTDO.SignerId = byLoginName.ID;
							signTDO.Loginname = byLoginName.LOGINNAME;
							signTDO.Username = byLoginName.USERNAME;
							signTDO.FullName = byLoginName.USERNAME;
							signTDO.FirstName = byLoginName.USERNAME;
							if (signerConfig.NumOrder > 0)
							{
								signTDO.NumOrder = signerConfig.NumOrder;
							}
							else
							{
								signTDO.NumOrder = GetMaxNumOrder(list);
							}
							signTDO.Title = byLoginName.TITLE;
							signTDO.DepartmentCode = byLoginName.DEPARTMENT_CODE;
							signTDO.DepartmentName = byLoginName.DEPARTMENT_NAME;
							signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							list.Add(signTDO);
						}
					}
				}
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				EMR_DOCUMENT_TYPE eMR_DOCUMENT_TYPE = new EmrDocumentType().DocumentTypeProperty(inputADO.DocumentTypeCode);
				flag3 = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_MULTI_SIGN == 1;
				flag2 = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_SIGN_PARALLEL == 1;
				flag3 = flag4;
				long? documentTypeId = ((eMR_DOCUMENT_TYPE != null) ? new long?(eMR_DOCUMENT_TYPE.ID) : ((long?)null));
				if (inputADO.IsMultiSign.HasValue)
				{
					flag3 = flag3 && inputADO.IsMultiSign.Value;
				}
				CommonParam commonParam2 = new CommonParam();
				SignHandle signHandle = new SignHandle(inputFileWork, 0f, 0f, 0, 0, null, null, inputADO.DocumentName, inputADO.Treatment.TREATMENT_CODE, signName, inputADO.SignReason, null, inputADO.SignType, list, null, inputADO.IsPatientSign, false, documentTypeId, flag3, inputADO.HisCode, inputADO, null, commonParam2, null, flag2, GetTreatmentData(), GetSignerData(), GetTokenCodeData());
				DocumentTDO documentTDO = new DocumentTDO();
				documentTDO.IsSignParallel = flag2;
				signHandle.SetFileType(fileADO.FileType);
				documentTDO.OriginalVersion = new VersionTDO();
				if (fileADO != null && !string.IsNullOrEmpty(fileADO.Base64FileContent))
				{
					documentTDO.OriginalVersion.Base64Data = fileADO.Base64FileContent;
				}
				if (fileADO3 != null && !string.IsNullOrEmpty(fileADO3.Base64FileContent))
				{
					documentTDO.OriginalVersion.Base64DataXml = fileADO3.Base64FileContent;
				}
				if (fileADO2 != null && !string.IsNullOrEmpty(fileADO2.Base64FileContent))
				{
					documentTDO.OriginalVersion.Base64DataJson = fileADO2.Base64FileContent;
				}
				DocumentTDO documentTDO2 = signHandle.SendDocument(documentTDO);
				if (documentTDO2 != null && !string.IsNullOrEmpty(documentTDO2.DocumentCode))
				{
					flag = true;
					inputADO.DocumentCode = documentTDO2.DocumentCode;
				}
				if (commonParam2.Messages != null && commonParam2.Messages.Count > 0)
				{
					commonParam.Messages.AddRange(commonParam2.Messages);
				}
				if (commonParam2.BugCodes != null && commonParam2.BugCodes.Count > 0)
				{
					commonParam.BugCodes.AddRange(commonParam2.BugCodes);
				}
				if (!flag)
				{
					commonParam.Messages.Add("Xử lý thất bại");
				}
				if (isShowMessage)
				{
					MessageManager.ShowAlert(Form.ActiveForm, commonParam, flag);
				}
				LogSystem.Info("param.GetMessage:" + commonParam.GetMessage() + "____param.GetBugCode:" + commonParam.GetBugCode());
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Co loi xay ra. Du lieu dau vao:____" + LogUtil.TraceData("inputADO", inputADO));
				LogSystem.Error(ex);
				flag = false;
			}
			return flag;
		}

		private long GetMaxNumOrder(List<SignTDO> listSign)
		{
			long result = 1L;
			if (listSign != null && listSign.Count > 0)
			{
				result = listSign.Max((SignTDO o) => o.NumOrder) + 1;
			}
			return result;
		}

		private bool CheckLogin(InputADO inputADO)
		{
			if (!string.IsNullOrEmpty(signToken.TokenCode) && !string.IsNullOrEmpty(signToken.LoginName))
			{
				GlobalStore.TokenData = new TokenData();
				GlobalStore.TokenData.TokenCode = signToken.TokenCode;
				GlobalStore.TokenData.User = new UserData();
				GlobalStore.TokenData.User.LoginName = signToken.LoginName;
				GlobalStore.TokenData.User.UserName = signToken.UserName;
				GlobalStore.TokenData.User.ApplicationCode = "HIS";
				GlobalStore.AcsConsumer.SetTokenCode(signToken.TokenCode);
				GlobalStore.EmrConsumer.SetTokenCode(signToken.TokenCode);
				GlobalStore.GetSetDicConsumer(signToken.TokenCode);
				signToken.TokenData = new TokenData();
				signToken.TokenData.User = new UserData();
				signToken.TokenData.User.LoginName = signToken.LoginName;
				signToken.TokenData.User.UserName = signToken.UserName;
				signToken.TokenData.User.ApplicationCode = "HIS";
				EMR_SIGNER singer = (GlobalStore.Singer = GlobalStore.GetByLoginName(signToken.LoginName));
				signToken.Singer = singer;
			}
			else if (!string.IsNullOrEmpty(GlobalStore.TokenCode))
			{
				GlobalStore.TokenData = new TokenData();
				GlobalStore.TokenData.TokenCode = GlobalStore.TokenCode;
				GlobalStore.TokenData.User = new UserData();
				GlobalStore.TokenData.User.LoginName = GlobalStore.LoginName;
				GlobalStore.TokenData.User.UserName = GlobalStore.UserName;
				GlobalStore.TokenData.User.ApplicationCode = "HIS";
				GlobalStore.AcsConsumer.SetTokenCode(GlobalStore.TokenData.TokenCode);
				GlobalStore.EmrConsumer.SetTokenCode(GlobalStore.TokenData.TokenCode);
				GlobalStore.GetSetDicConsumer(GlobalStore.TokenData.TokenCode);
				signToken.TokenData = GlobalStore.TokenData;
				signToken.UserName = GlobalStore.UserName;
				signToken.LoginName = GlobalStore.LoginName;
				EMR_SIGNER singer2 = (GlobalStore.Singer = GlobalStore.GetByLoginName(GlobalStore.LoginName));
				signToken.Singer = singer2;
			}
			else if (GlobalStore.TokenData == null)
			{
				frmLogin frmLogin = new frmLogin(ProcessSignTokenData);
				frmLogin.ShowDialog();
			}
			if (signToken.Singer != null)
			{
				return true;
			}
			if (inputADO.IsSign)
			{
				return ValidSignerByLogin();
			}
			if (signToken != null && !string.IsNullOrEmpty(signToken.TokenCode))
			{
				return true;
			}
			return false;
		}

		private bool ValidSignerByLogin()
		{
			if (GlobalStore.TokenData != null && !string.IsNullOrEmpty(GlobalStore.TokenData.TokenCode))
			{
				EMR_SIGNER singer = (GlobalStore.Singer = GlobalStore.GetByLoginName(GlobalStore.LoginName));
				signToken.Singer = singer;
				if (GlobalStore.Singer != null)
				{
					return true;
				}
				if (XtraMessageBox.Show(string.Format(MessageUitl.GetMessage("TaiKhoanChuaDuocTaoNguoiKyTrenHeThongEMR"), GlobalStore.LoginName), MessageUitl.GetMessage("ThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					RefeshAfterLogout();
					frmLogin frmLogin = new frmLogin(ProcessSignTokenData);
					frmLogin.ShowDialog();
					return ValidSignerByLogin();
				}
			}
			return false;
		}

		private void ProcessSignTokenData(SignToken _signToken)
		{
			signToken = _signToken;
			if (_signToken != null)
			{
				GlobalStore.TokenData = _signToken.TokenData;
				GlobalStore.LoginName = _signToken.LoginName;
				GlobalStore.UserName = _signToken.UserName;
				GlobalStore.TokenCode = _signToken.TokenCode;
				EMR_SIGNER singer = (GlobalStore.Singer = GlobalStore.GetByLoginName(GlobalStore.LoginName));
				signToken.Singer = singer;
			}
		}

		private void PrintNowProcess(string outputFile)
		{
			try
			{
				LogSystem.Info("PrintNowProcess.1" + LogUtil.TraceData(LogUtil.GetMemberName(() => outputFile), outputFile) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<bool>>(Expression.Field(null, FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), GlobalStore.PrintUsingWaterMark) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<OptionPrintType>>(Expression.Field(null, FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), GlobalStore.OptionPrintType));
				if (GlobalStore.PrintUsingWaterMark)
				{
					string text = Utils.GenerateTempFileWithin();
					V_EMR_DOCUMENT v_EMR_DOCUMENT = new EmrDocument().GetViewByCode(inputADOWorking.DocumentCode);
					if (v_EMR_DOCUMENT == null)
					{
						V_EMR_DOCUMENT v_EMR_DOCUMENT2 = new V_EMR_DOCUMENT();
						v_EMR_DOCUMENT2.DOCUMENT_CODE = inputADOWorking.DocumentCode;
						v_EMR_DOCUMENT2.DOCUMENT_NAME = inputADOWorking.DocumentName;
						v_EMR_DOCUMENT2.TREATMENT_CODE = ((inputADOWorking.Treatment != null) ? inputADOWorking.Treatment.TREATMENT_CODE : null);
						v_EMR_DOCUMENT2.CREATE_TIME = Utils.GetTimeNow();
						v_EMR_DOCUMENT = v_EMR_DOCUMENT2;
					}
					WaterMarkProcess.ProcessInsertWaterMark(outputFile, text, v_EMR_DOCUMENT);
					try
					{
						if (File.Exists(outputFile))
						{
							File.Delete(outputFile);
						}
					}
					catch
					{
					}
					outputFile = text;
					outputSignedFileResult = outputFile;
				}
				if (GlobalStore.OptionPrintType == OptionPrintType.PdfAposeLib)
				{
					PrintLibProcess.ExecutePrintNowJob(outputFile, printNumberCopies, inputADOWorking.PrinterDefault, inputADOWorking.PaperSizeDefault);
				}
				else if (GlobalStore.OptionPrintType == OptionPrintType.CallExeLib && PrintLibProcess.ValidExistsExecutePrintCallExeService())
				{
					PrintLibProcess.ExecutePrintCallExeService(outputFile, printNumberCopies, inputADOWorking.PrinterDefault, inputADOWorking.PaperSizeDefault);
				}
				else
				{
					PrintLibProcess.SimplePrintNowDevLib(outputFile, printNumberCopies, inputADOWorking.PrinterDefault, inputADOWorking.PaperSizeDefault);
				}
				LogSystem.Info("PrintNowProcess.2");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void PrintPreviewProcess(string outputFile)
		{
			try
			{
				LogSystem.Debug("PrintPreviewProcess.1" + LogUtil.TraceData(LogUtil.GetMemberName(() => outputFile), outputFile) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<bool>>(Expression.Field(null, FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), GlobalStore.PrintUsingWaterMark));
				if (GlobalStore.PrintUsingWaterMark)
				{
					string text = Utils.GenerateTempFileWithin();
					V_EMR_DOCUMENT v_EMR_DOCUMENT = new EmrDocument().GetViewByCode(inputADOWorking.DocumentCode);
					if (v_EMR_DOCUMENT == null)
					{
						V_EMR_DOCUMENT v_EMR_DOCUMENT2 = new V_EMR_DOCUMENT();
						v_EMR_DOCUMENT2.DOCUMENT_CODE = inputADOWorking.DocumentCode;
						v_EMR_DOCUMENT2.DOCUMENT_NAME = inputADOWorking.DocumentName;
						v_EMR_DOCUMENT2.TREATMENT_CODE = ((inputADOWorking.Treatment != null) ? inputADOWorking.Treatment.TREATMENT_CODE : null);
						v_EMR_DOCUMENT2.CREATE_TIME = Utils.GetTimeNow();
						v_EMR_DOCUMENT = v_EMR_DOCUMENT2;
					}
					WaterMarkProcess.ProcessInsertWaterMark(outputFile, text, v_EMR_DOCUMENT);
					try
					{
						if (File.Exists(outputFile))
						{
							File.Delete(outputFile);
						}
					}
					catch
					{
					}
					outputFile = text;
					outputSignedFileResult = outputFile;
				}
				frmShowAndPrintNow frmShowAndPrintNow2 = new frmShowAndPrintNow(outputFile, inputADOWorking, printNumberCopies);
				frmShowAndPrintNow2.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void OnQueryPageSettings(object sender, PdfQueryPageSettingsEventArgs e)
		{
			try
			{
				Width_ = (int)e.PageSize.Width;
				Height_ = (int)e.PageSize.Height;
				currentPageSettings.PaperSize.Width = (int)e.PageSize.Width;
				currentPageSettings.PaperSize.Height = (int)e.PageSize.Height;
				printerSettings.DefaultPageSettings.PaperSize = currentPageSettings.PaperSize;
				pdfPrinterSettings = new PdfPrinterSettings(printerSettings);
				pdfPrinterSettings.PageOrientation = ((!currentPageSettings.Landscape) ? PdfPrintPageOrientation.Portrait : PdfPrintPageOrientation.Landscape);
				pdfPrinterSettings.ScaleMode = PdfPrintScaleMode.ActualSize;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void pdfViewer1_PageSetupDialogShowing(object sender, PdfPageSetupDialogShowingEventArgs e)
		{
			try
			{
				LogSystem.Debug("pdfViewer1_PageSetupDialogShowing.1____" + LogUtil.TraceData(LogUtil.GetMemberName(() => printNumberCopies), printNumberCopies));
				e.FormStartPosition = FormStartPosition.CenterScreen;
				int width = 600;
				int height = 400;
				if (Screen.PrimaryScreen != null)
				{
					width = ((Screen.PrimaryScreen.WorkingArea.Width > 400) ? (Screen.PrimaryScreen.WorkingArea.Width - 400) : 100);
					height = ((Screen.PrimaryScreen.WorkingArea.Height > 100) ? (Screen.PrimaryScreen.WorkingArea.Height - 100) : 50);
				}
				e.FormSize = new Size(width, height);
				LogSystem.Debug("pdfViewer1_PageSetupDialogShowing.2");
				if (printNumberCopies > 1)
				{
					e.PrinterSettings.Settings.Copies = printNumberCopies;
					LogSystem.Debug("pdfViewer1_PageSetupDialogShowing.3");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ActionAfterSignedProcess(string outputFile)
		{
			try
			{
				if (string.IsNullOrEmpty(outputFile))
				{
					throw new ArgumentNullException("outputFile");
				}
				outputSignedFileResult = outputFile;
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => outputSignedFileResult), outputSignedFileResult) + LogUtil.TraceData(LogUtil.GetMemberName(() => iSPrintNow), iSPrintNow));
				if (iSPrintNow)
				{
					PrintNowProcess(outputFile);
				}
				else if (iSPrintPreview)
				{
					PrintPreviewProcess(outputFile);
				}
			}
			catch (Exception ex)
			{
				outputSignedFileResult = "";
				LogSystem.Warn(ex);
			}
		}

		private bool GetMultiSignDoc(InputADO inputADO, ref bool isSignParanel)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(inputADO.DocumentTypeCode))
			{
				try
				{
					inputADO.DocumentTypeCode = string.Format("{0:00}", inputADO.DocumentTypeCode);
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				inputADO.DocumentTypeCode = ((inputADO.DocumentTypeCode.Length == 1) ? ("0" + inputADO.DocumentTypeCode) : inputADO.DocumentTypeCode);
				bool isMultiSign = false;
				new EmrDocumentType().DocumentTypeProperty(inputADO.DocumentTypeCode, ref isMultiSign, ref isSignParanel);
				flag = isMultiSign;
				if (inputADO.IsMultiSign.HasValue)
				{
					flag = flag && inputADO.IsMultiSign.Value;
				}
			}
			return flag;
		}

		private long? GetDocumentTypeId(string code)
		{
			long? result = null;
			try
			{
				EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(code);
				if (byCode != null)
				{
					result = byCode.ID;
					return result;
				}
				LogSystem.Warn("Ma loai van ban truyen vao khong hop le. DocumentTypeCode = " + code);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private DocumentTDO GenerateByDocumentCode(string documentCode, ref bool isMultiSign)
		{
			DocumentTDO documentTDO = new DocumentTDO();
			try
			{
				EMR_DOCUMENT byCode = new EmrDocument().GetByCode(documentCode);
				documentTDO.DocumentCode = byCode.DOCUMENT_CODE;
				documentTDO.DocumentName = byCode.DOCUMENT_NAME;
				documentTDO.DocumentTypeId = byCode.DOCUMENT_TYPE_ID;
				documentTDO.TreatmentCode = byCode.TREATMENT_CODE;
				documentTDO.OriginalVersion = new VersionTDO();
				documentTDO.OriginalVersion.DocumentCode = byCode.DOCUMENT_CODE;
				documentTDO.AttachmentCount = byCode.ATTACHMENT_COUNT;
				documentTDO.ParentDependentCode = byCode.PARENT_DEPENDENT_CODE;
				isMultiSign = byCode.IS_MULTI_SIGN == 1;
			}
			catch
			{
				documentTDO = null;
			}
			return documentTDO;
		}

		private List<SignPositionADO> GetNextPositionSigned(bool isPatientSign, PdfReader readerWorking, EMR_SIGN signSelected, ref int signedCount)
		{
			List<SignPositionADO> list = null;
			List<SignPositionADO> list2 = null;
			SignPositionADO nextSignPosition = null;
			try
			{
				bool hasNextSignPosition = false;
				if (isPatientSign)
				{
					list2 = Utils.GetPdfPatientSignPosition(readerWorking);
					if (list2 != null && list2.Count > 0)
					{
						nextSignPosition = list2.FirstOrDefault();
						hasNextSignPosition = nextSignPosition != null;
						list = (hasNextSignPosition ? list2.Where((SignPositionADO o) => o.Text == nextSignPosition.Text).ToList() : null);
					}
					if (list != null && list.Count > 0)
					{
						return list;
					}
				}
				list2 = Utils.GetPdfSignPosition(readerWorking);
				signedCount = Utils.GetSignedCount(readerWorking);
				if (list2 != null && list2.Count > 0)
				{
					list2 = list2.OrderBy((SignPositionADO o) => VerifySign.GetNumOderByCommentText(o.Text)).ToList();
					List<SignPositionADO> list3 = list2.Where((SignPositionADO o) => VerifySign.GetNumOderByCommentText(o.Text) == VerifySign.GetNumOrderBySignOrDefault(signSelected, GetSignerData(), GetTreatmentData())).ToList();
					nextSignPosition = ((list3 != null && list3.Count > 0) ? list3.FirstOrDefault() : null);
					hasNextSignPosition = nextSignPosition != null;
					List<SignPositionADO> list4 = (hasNextSignPosition ? list2.Where((SignPositionADO o) => o.Text == nextSignPosition.Text).ToList() : null);
					if (list4 != null && list4.Count > 0)
					{
						list = new List<SignPositionADO>();
						list.AddRange(list4);
					}
					LogSystem.Debug(LogUtil.TraceData("nextSignPositions", list) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(() => hasNextSignPosition), hasNextSignPosition));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return list;
		}

		private void InitParam(string dti)
		{
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => dti), dti));
			if (string.IsNullOrEmpty(dti))
			{
				return;
			}
			string[] array = dti.Split(new string[1] { "|" }, StringSplitOptions.None);
			if (array != null && array.Length > 1)
			{
				GlobalStore.IsUseSendDTI = true;
				ConstanIG.ACS_BASE_URI = array[0];
				if (array.Length > 1)
				{
					GlobalStore.EMR_BASE_URI = array[1];
				}
				if (array.Length > 2)
				{
					FssConstant.BASE_URI = array[2];
				}
				if (array.Length > 3)
				{
					GlobalStore.TokenCode = array[3];
					signToken.TokenCode = array[3];
				}
				if (array.Length > 4)
				{
					GlobalStore.LoginName = array[4];
					signToken.LoginName = array[4];
				}
				if (array.Length > 5)
				{
					GlobalStore.UserName = array[5];
					signToken.UserName = array[5];
				}
				if (array.Length > 6)
				{
					GlobalStore.Password = array[6];
					signToken.Password = array[6];
					GlobalStore.HPS_BASE_URI = array[6];
				}
				if (array.Length > 7)
				{
					GlobalStore.HPS_BASE_URI = array[7];
				}
				RegistryProcessor.Write("ACS_BASE_URI", ConstanIG.ACS_BASE_URI);
				RegistryProcessor.Write("EMR_BASE_URI", GlobalStore.EMR_BASE_URI);
				RegistryProcessor.Write("FSS_BASE_URI", FssConstant.BASE_URI);
				RegistryProcessor.Write("HPS_BASE_URI", GlobalStore.HPS_BASE_URI);
			}
		}

		private InputADO CopyInputADO(InputADO data)
		{
			InputADO inputADO = new InputADO();
			try
			{
				inputADO.BusinessCode = data.BusinessCode;
				inputADO.DisplayConfigDTO = data.DisplayConfigDTO;
				inputADO.DlgChoosePoint = data.DlgChoosePoint;
				inputADO.DlgOpenModuleConfig = data.DlgOpenModuleConfig;
				inputADO.DlgGetTreatment = data.DlgGetTreatment;
				inputADO.DocumentCode = data.DocumentCode;
				inputADO.DocumentName = data.DocumentName;
				inputADO.DocumentTypeCode = data.DocumentTypeCode;
				inputADO.DTI = data.DTI;
				inputADO.HisCode = data.HisCode;
				inputADO.HisUriUpdateSignedState = data.HisUriUpdateSignedState;
				inputADO.IsExport = data.IsExport;
				inputADO.IsMultiSign = data.IsMultiSign;
				inputADO.IsPatientSign = data.IsPatientSign;
				inputADO.IsPrint = data.IsPrint;
				inputADO.IsPrintOnlyContent = data.IsPrintOnlyContent;
				inputADO.IsReject = data.IsReject;
				inputADO.IsSave = data.IsSave;
				inputADO.IsSelectRangeRectangle = data.IsSelectRangeRectangle;
				inputADO.IsSign = data.IsSign;
				inputADO.IsUseTimespan = data.IsUseTimespan;
				inputADO.RoomCode = data.RoomCode;
				inputADO.RoomName = data.RoomName;
				inputADO.RoomTypeCode = data.RoomTypeCode;
				inputADO.SignReason = data.SignReason;
				inputADO.SignType = data.SignType;
				inputADO.Treatment = data.Treatment;
				inputADO.Watermarks = data.Watermarks;
				inputADO.SignerConfigs = data.SignerConfigs;
				inputADO.IsAutoChooseBusiness = data.IsAutoChooseBusiness;
				inputADO.PrintNumberCopies = data.PrintNumberCopies;
				inputADO.PrintTypeBusinessCodes = data.PrintTypeBusinessCodes;
				inputADO.MergeCode = data.MergeCode;
				inputADO.DocumentTime = data.DocumentTime;
				inputADO.DocumentGroupCode = data.DocumentGroupCode;
				inputADO.DepartmentCode = data.DepartmentCode;
				inputADO.DepartmentName = data.DepartmentName;
				inputADO.DependentCode = data.DependentCode;
				inputADO.ParentDependentCode = data.ParentDependentCode;
				inputADO.PaperSizeDefault = data.PaperSizeDefault;
				inputADO.PrinterDefault = data.PrinterDefault;
				inputADO.IsEnableButtonPrint = data.IsEnableButtonPrint;
				inputADO.IsOutsideTreatment = data.IsOutsideTreatment;
				inputADO.MediOrgCode = data.MediOrgCode;
				inputADO.DeviceSignPadName = data.DeviceSignPadName;
				inputADO.ActSelectDevice = data.ActSelectDevice;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return inputADO;
		}

		private void ProcessMemoryUsageuser()
		{
			try
			{
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
