using System;
using System.Collections.Generic;
using System.IO;
using EMR.EFMODEL.DataModels;
using EMR.SDO;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.FingerPrint;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;
using Inventec.Common.SignLibrary.SignBoard;

namespace Inventec.Common.SignLibrary.SignHandler
{
	internal class SignHSMHandler : BussinessBase
	{
		internal SignHSMHandler()
		{
		}

		internal SignHSMHandler(CommonParam param, InputADO inputADOWorking)
			: base(param, inputADOWorking)
		{
		}

		internal SignHSMHandler(CommonParam param, InputADO inputADOWorking, string src)
			: base(param, inputADOWorking, src)
		{
		}

		internal SignHSMHandler(CommonParam param, InputADO inputADOWorking, string src, bool isSignParanel)
			: base(param, inputADOWorking, src, isSignParanel)
		{
		}

		internal SignHSMHandler(CommonParam param, InputADO inputADOWorking, string src, bool isSignParanel, EMR_TREATMENT treatment, EMR_SIGNER singer, string tokenCode)
			: base(param, inputADOWorking, src, isSignParanel, treatment, singer, tokenCode)
		{
		}

		internal bool SignWithCreateDoc(string outputFile, ref DocumentTDO document, string documentName, string treatmentCode, List<SignTDO> signStrategys, List<SignTDO> signTemps, string GetBase64OriginalFileData, string GetBase64HeaderFileData, PointSignTDO pointSignTDO, string signDescription, bool isPatientSignOrHomeRelativeSign, long? documentTypeId, bool isMultiSign, string hisCode, bool isCardAnonymous, ref Stream output, EMR_SIGN _signSelected, string mergeCode = "", double oginalHeight = 0.0, byte[] signedImageData = null)
		{
			bool result = false;
			if (ValidSignBoard(signedImageData))
			{
				LogSystem.Info("SignWithCreateDoc => 1");
				HsmSignCreateTDO hsmSignCreateTDO = new HsmSignCreateTDO();
				hsmSignCreateTDO.IsOutsideTreatment = base.inputADOWorking.IsOutsideTreatment == 1;
				hsmSignCreateTDO.MediOrgCode = base.inputADOWorking.MediOrgCode;
				hsmSignCreateTDO.Signs = (string.IsNullOrEmpty(base.inputADOWorking.BusinessCode) ? signTemps : null);
				if (string.IsNullOrEmpty(base.inputADOWorking.BusinessCode) && signStrategys != null && signStrategys.Count > 0)
				{
					if (hsmSignCreateTDO.Signs == null)
					{
						hsmSignCreateTDO.Signs = new List<SignTDO>();
					}
					hsmSignCreateTDO.Signs.AddRange(signStrategys);
				}
				LogSystem.Info("SignWithCreateDoc => 1.1");
				if (hsmSignCreateTDO.Signs != null)
				{
					foreach (SignTDO sign in hsmSignCreateTDO.Signs)
					{
						if (!string.IsNullOrEmpty(sign.PatientCode))
						{
							sign.SignedImageData = (base.IsUsingSignPad ? base.SignPadImageData : signedImageData);
							if (isPatientSignOrHomeRelativeSign)
							{
								sign.Description = signDescription;
							}
						}
						else if (sign.Loginname == base.Signer.LOGINNAME)
						{
							sign.Description = signDescription;
						}
					}
				}
				LogSystem.Info("SignWithCreateDoc => 1.2");
				if (isCardAnonymous)
				{
					hsmSignCreateTDO.IsSignElectronic = true;
				}
				hsmSignCreateTDO.DependentCode = base.inputADOWorking.DependentCode;
				hsmSignCreateTDO.ParentDependentCode = base.inputADOWorking.ParentDependentCode;
				hsmSignCreateTDO.RoomCode = base.inputADOWorking.RoomCode;
				hsmSignCreateTDO.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
				hsmSignCreateTDO.WorkingDepartmentName = base.inputADOWorking.DepartmentName;
				hsmSignCreateTDO.IsSignParallel = base.IsSignParanel;
				hsmSignCreateTDO.MergeCode = mergeCode;
				hsmSignCreateTDO.Base64Header = GetBase64HeaderFileData;
				if (oginalHeight > 0.0)
				{
					hsmSignCreateTDO.OriginalHigh = (decimal)oginalHeight;
				}
				hsmSignCreateTDO.PointSign = pointSignTDO;
				hsmSignCreateTDO.Description = signDescription;
				hsmSignCreateTDO.HisOrder = base.inputADOWorking.HisOrder;
				hsmSignCreateTDO.OriginalVersion = new VersionTDO();
				LogSystem.Info("SignWithCreateDoc => 1.3");
				if (document != null && !string.IsNullOrEmpty(document.DocumentCode))
				{
					hsmSignCreateTDO.DocumentCode = document.DocumentCode;
					hsmSignCreateTDO.DocumentName = document.DocumentName;
					if (document.DocumentTypeId.HasValue && document.DocumentTypeId.Value > 0)
					{
						hsmSignCreateTDO.DocumentTypeId = document.DocumentTypeId;
					}
					hsmSignCreateTDO.OriginalVersion.DocumentCode = document.DocumentCode;
					hsmSignCreateTDO.HisCode = document.HisCode;
				}
				else
				{
					hsmSignCreateTDO.DocumentName = (string.IsNullOrEmpty(documentName) ? ("Ký điện tử cho hồ sơ có mã " + treatmentCode + " ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) : documentName);
					if (documentTypeId.HasValue && documentTypeId.Value > 0)
					{
						hsmSignCreateTDO.DocumentTypeId = documentTypeId;
					}
					hsmSignCreateTDO.HisCode = hisCode;
					if (base.inputADOWorking.DocumentTime.HasValue && base.inputADOWorking.DocumentTime.Value != DateTime.MinValue)
					{
						hsmSignCreateTDO.DocumentTime = DateTimeConvert.SystemDateTimeToTimeNumber(base.inputADOWorking.DocumentTime);
					}
				}
				LogSystem.Info("SignWithCreateDoc => 1.4");
				if (!string.IsNullOrEmpty(base.inputADOWorking.DocumentGroupCode))
				{
					EMR_DOCUMENT_GROUP byCode = new EmrDocumentGroup().GetByCode(base.inputADOWorking.DocumentGroupCode);
					hsmSignCreateTDO.DocumentGroupId = ((byCode != null) ? new long?(byCode.ID) : ((long?)null));
				}
				hsmSignCreateTDO.IsFinishSign = !isMultiSign;
				hsmSignCreateTDO.IsSigning = isMultiSign;
				hsmSignCreateTDO.TreatmentCode = treatmentCode;
				if (document != null && document.OriginalVersion != null && !string.IsNullOrEmpty(document.OriginalVersion.Base64Data))
				{
					hsmSignCreateTDO.OriginalVersion.Base64Data = document.OriginalVersion.Base64Data;
					hsmSignCreateTDO.OriginalVersion.Base64DataJson = document.OriginalVersion.Base64DataJson;
					hsmSignCreateTDO.OriginalVersion.Base64DataXml = document.OriginalVersion.Base64DataXml;
				}
				else
				{
					hsmSignCreateTDO.OriginalVersion.Base64Data = GetBase64OriginalFileData;
				}
				hsmSignCreateTDO.BusinessCode = base.inputADOWorking.BusinessCode;
				LogSystem.Info("SignWithCreateDoc => 1.5");
				if (base.FileType == FileType.Xml)
				{
					hsmSignCreateTDO.FileType = EMR.TDO.FileType.XML;
				}
				else if (base.FileType == FileType.Json)
				{
					hsmSignCreateTDO.FileType = EMR.TDO.FileType.JSON;
				}
				else
				{
					hsmSignCreateTDO.FileType = EMR.TDO.FileType.PDF;
				}
				LogSystem.Info("SignWithCreateDoc => 1.6");
				if (base.inputADOWorking.PaperSizeDefault != null)
				{
					hsmSignCreateTDO.PaperName = base.inputADOWorking.PaperSizeDefault.PaperName;
					if (string.IsNullOrEmpty(hsmSignCreateTDO.PaperName))
					{
						hsmSignCreateTDO.PaperName = base.inputADOWorking.PaperSizeDefault.Kind.ToString();
					}
					hsmSignCreateTDO.Width = base.inputADOWorking.PaperSizeDefault.Width;
					hsmSignCreateTDO.Height = base.inputADOWorking.PaperSizeDefault.Height;
					hsmSignCreateTDO.RawKind = base.inputADOWorking.PaperSizeDefault.RawKind;
				}
				LogSystem.Info("SignWithCreateDoc => 1.7");
				if (!VerifyDataPreCallApi(hsmSignCreateTDO))
				{
					base.param.Messages.Add(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					MessageManager.Show(base.param, false);
					return false;
				}
				LogSystem.Info("SignWithCreateDoc => 2");
				CommonParam commonParam = new CommonParam();
				HsmSignCreateTDO rs = new EmrDocument(commonParam).CreateAndSignHsm(base.TokenCode, hsmSignCreateTDO);
				if (rs != null)
				{
					LogSystem.Info("SignWithCreateDoc => 3");
					result = true;
					document.DocumentCode = rs.DocumentCode;
					document.DocumentName = rs.DocumentName;
					document.DocumentTypeId = rs.DocumentTypeId;
					document.MergeCode = rs.MergeCode;
					document.TreatmentCode = rs.TreatmentCode;
					document.DependentCode = rs.DependentCode;
					document.ParentDependentCode = rs.ParentDependentCode;
					document.OriginalVersion = rs.OriginalVersion;
					document.PaperName = rs.PaperName;
					document.Width = rs.Width;
					document.Height = rs.Height;
					document.RawKind = rs.RawKind;
					if (rs.Signs != null && rs.Signs.Count > 0)
					{
						foreach (SignTDO sign2 in rs.Signs)
						{
							if (sign2 != null && sign2.Version != null && !string.IsNullOrEmpty(sign2.Version.Url))
							{
								output = FssFileDownload.GetFile(sign2.Version.Url);
								LogSystem.Info("SignWithCreateDoc => 4. output.Length =" + output.Length);
								break;
							}
						}
					}
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
					LogSystem.Info("SignWithCreateDoc => 5____" + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + LogUtil.TraceData(LogUtil.GetMemberName(() => param), base.param) + LogUtil.TraceData(LogUtil.GetMemberName(() => isCardAnonymous), isCardAnonymous));
				}
			}
			return result;
		}

		internal bool SignOnly(ref DocumentTDO document, List<SignTDO> signStrategys, List<SignTDO> signTemps, PointSignTDO pointSignTDO, string signDescription, bool isPatientSignOrHomeRelativeSign, bool isMultiSign, string cmnd, string cardCode, string serviceCode, bool isCardAnonymous, long? relationId, string relationName, string relationPeopleName, ref Stream outputStream, EMR_SIGN _signSelected, string mergeCode = "", string linkCode = "", byte[] signedImageData = null)
		{
			bool result = false;
			try
			{
				if (ValidSignBoard(signedImageData))
				{
					EmrSignHsmSDO emrSignHsmSDO = new EmrSignHsmSDO();
					V_EMR_DOCUMENT viewByCode = new EmrDocument().GetViewByCode(document.DocumentCode);
					emrSignHsmSDO.EmrDocumentId = viewByCode.ID;
					emrSignHsmSDO.CmndNumber = cmnd;
					emrSignHsmSDO.CardCode = cardCode;
					emrSignHsmSDO.ServiceCode = serviceCode;
					emrSignHsmSDO.IsFinishSign = !isMultiSign;
					emrSignHsmSDO.IsSigning = isMultiSign;
					if (isCardAnonymous)
					{
						emrSignHsmSDO.IsSignElectronic = true;
					}
					emrSignHsmSDO.RelationName = relationName;
					emrSignHsmSDO.RelationPeopleName = relationPeopleName;
					emrSignHsmSDO.RelationId = relationId;
					emrSignHsmSDO.LinkCode = linkCode;
					EMR_SIGN eMR_SIGN = ((_signSelected != null) ? _signSelected : new EmrSign().GetSignDocumentFirst(viewByCode, isPatientSignOrHomeRelativeSign ? null : base.Signer, base.Treatment, true));
					if (eMR_SIGN != null && eMR_SIGN.ID > 0)
					{
						emrSignHsmSDO.EmrSignId = eMR_SIGN.ID;
					}
					emrSignHsmSDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
					emrSignHsmSDO.PointSign = new EmrPointSignSDO
					{
						CoorXRectangle = pointSignTDO.CoorXRectangle,
						CoorYRectangle = pointSignTDO.CoorYRectangle,
						MaxPageNumber = pointSignTDO.MaxPageNumber,
						PageNumber = pointSignTDO.PageNumber,
						SizeFont = pointSignTDO.SizeFont,
						TextPosition = pointSignTDO.TextPosition,
						HeightRectangle = pointSignTDO.HeightRectangle,
						WidthRectangle = pointSignTDO.WidthRectangle,
						TypeDisplay = pointSignTDO.TypeDisplay,
						FormatRectangleText = pointSignTDO.FormatRectangleText,
						Alignment = pointSignTDO.Alignment,
						IsBold = pointSignTDO.IsBold,
						IsItalic = pointSignTDO.IsItalic,
						IsUnderlined = pointSignTDO.IsUnderlined,
						FontName = pointSignTDO.FontName
					};
					emrSignHsmSDO.Description = signDescription;
					emrSignHsmSDO.RoomCode = base.inputADOWorking.RoomCode;
					emrSignHsmSDO.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
					emrSignHsmSDO.WorkingDepartmentName = base.inputADOWorking.DepartmentName;
					emrSignHsmSDO.SignedImageData = (base.IsUsingSignPad ? base.SignPadImageData : signedImageData);
					CommonParam commonParam = new CommonParam();
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => emrSignHsmSDO), emrSignHsmSDO));
					EmrSignResultSDO emrSignResultSDO = new EmrDocument(commonParam).SignHsm(base.TokenCode, emrSignHsmSDO);
					if (emrSignResultSDO != null)
					{
						result = true;
						if (emrSignResultSDO.EmrVersion != null && emrSignResultSDO.EmrSign != null && !string.IsNullOrEmpty(emrSignResultSDO.EmrVersion.URL))
						{
							outputStream = FssFileDownload.GetFile(emrSignResultSDO.EmrVersion.URL);
						}
					}
					else
					{
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => emrSignHsmSDO), emrSignHsmSDO));
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
			}
			catch (Exception ex)
			{
				base.param.Messages.Add(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
				result = false;
			}
			return result;
		}

		internal bool PatientOrHomeRelativeSignOnly(ref DocumentTDO document, List<SignTDO> signStrategys, List<SignTDO> signTemps, PointSignTDO pointSignTDO, string signDescription, bool isPatientSign, bool isHomeRelativeSign, bool isMultiSign, string cmnd, string cardCode, string serviceCode, bool isCardAnonymous, long? relationId, string relationName, string relationPeopleName, ref Stream outputStream, EMR_SIGN _signSelected, string mergeCode = "", string linkCode = "", byte[] signedImageData = null, bool IsHasBusinessCode = false)
		{
			bool result = false;
			try
			{
				if (ValidSignBoard(signedImageData))
				{
					EmrSignHsmSDO emrSignHsmSDO = new EmrSignHsmSDO();
					V_EMR_DOCUMENT viewByCode = new EmrDocument().GetViewByCode(document.DocumentCode);
					emrSignHsmSDO.EmrDocumentId = viewByCode.ID;
					emrSignHsmSDO.CmndNumber = cmnd;
					emrSignHsmSDO.CardCode = cardCode;
					emrSignHsmSDO.ServiceCode = serviceCode;
					emrSignHsmSDO.IsFinishSign = !isMultiSign;
					emrSignHsmSDO.IsSigning = isMultiSign;
					if (isCardAnonymous)
					{
						emrSignHsmSDO.IsSignElectronic = true;
					}
					emrSignHsmSDO.RelationName = relationName;
					emrSignHsmSDO.RelationPeopleName = relationPeopleName;
					emrSignHsmSDO.RelationId = relationId;
					emrSignHsmSDO.LinkCode = linkCode;
					EMR_SIGN eMR_SIGN = ((_signSelected != null) ? _signSelected : new EmrSign().GetSignDocumentFirst(viewByCode, (isPatientSign || isHomeRelativeSign) ? null : base.Signer, base.Treatment, true));
					if (eMR_SIGN != null && eMR_SIGN.ID > 0 && ((_signSelected == null && !IsHasBusinessCode) || (_signSelected != null && IsHasBusinessCode)))
					{
						emrSignHsmSDO.EmrSignId = eMR_SIGN.ID;
					}
					emrSignHsmSDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
					emrSignHsmSDO.PointSign = new EmrPointSignSDO
					{
						CoorXRectangle = pointSignTDO.CoorXRectangle,
						CoorYRectangle = pointSignTDO.CoorYRectangle,
						MaxPageNumber = pointSignTDO.MaxPageNumber,
						PageNumber = pointSignTDO.PageNumber,
						SizeFont = pointSignTDO.SizeFont,
						TextPosition = pointSignTDO.TextPosition,
						HeightRectangle = pointSignTDO.HeightRectangle,
						WidthRectangle = pointSignTDO.WidthRectangle,
						TypeDisplay = pointSignTDO.TypeDisplay,
						FormatRectangleText = pointSignTDO.FormatRectangleText,
						Alignment = pointSignTDO.Alignment,
						IsBold = pointSignTDO.IsBold,
						IsItalic = pointSignTDO.IsItalic,
						IsUnderlined = pointSignTDO.IsUnderlined,
						FontName = pointSignTDO.FontName
					};
					emrSignHsmSDO.Description = signDescription;
					emrSignHsmSDO.RoomCode = base.inputADOWorking.RoomCode;
					emrSignHsmSDO.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
					emrSignHsmSDO.WorkingDepartmentName = base.inputADOWorking.DepartmentName;
					emrSignHsmSDO.SignedImageData = (base.IsUsingSignPad ? base.SignPadImageData : signedImageData);
					CommonParam commonParam = new CommonParam();
					EmrSignResultSDO emrSignResultSDO = new EmrDocument(commonParam).PatientOrHomeRelativeSignHsm(base.TokenCode, emrSignHsmSDO);
					if (emrSignResultSDO != null)
					{
						result = true;
						if (emrSignResultSDO.EmrVersion != null && emrSignResultSDO.EmrSign != null && !string.IsNullOrEmpty(emrSignResultSDO.EmrVersion.URL))
						{
							outputStream = FssFileDownload.GetFile(emrSignResultSDO.EmrVersion.URL);
						}
					}
					else
					{
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => emrSignHsmSDO), emrSignHsmSDO));
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
			}
			catch (Exception ex)
			{
				base.param.Messages.Add(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
				result = false;
			}
			return result;
		}

		private bool ValidSignBoard(byte[] signedImageData)
		{
			bool result = true;
			try
			{
				if (base.IsUsingSignPad && GlobalStore.EMR_SIGN_BOARD__OPTION == "2")
				{
					if (base.IsUsingSignPadBefore && signedImageData != null && signedImageData.Length != 0)
					{
						base.SignPadImageData = signedImageData;
						return result;
					}
					if (EmrConfigKeys.EMR_EMR_SIGN_CONNECT_DEVICE_TYPE_OPTION == "2")
					{
						IFingerPrint fingerPrint = FingerPrintFactory.MakeISignBoard(base.param, base.inputADOWorking, SignBoardOption.Use);
						base.SignPadImageData = ((fingerPrint != null) ? fingerPrint.Run() : null);
						Utils.SignPadImageData = base.SignPadImageData;
					}
					else
					{
						ISignBoard signBoard = SignBoardFactory.MakeISignBoard(base.param, base.inputADOWorking, SignBoardOption.Use);
						base.SignPadImageData = ((signBoard != null) ? signBoard.Run() : null);
						Utils.SignPadImageData = signedImageData;
					}
					if (base.SignPadImageData == null || base.SignPadImageData.Length == 0)
					{
						base.param.Messages.Add(MessageUitl.GetMessage("KhongTimThayChuKyKhiSuDungBangKy"));
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				base.param.Messages.Add(MessageUitl.GetMessage("TinhNangKySuDungBangKyChuaDuocHoTro"));
				result = false;
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal void SetSigmImageData(byte[] bmpSignImage)
		{
			base.SignPadImageData = bmpSignImage;
		}

		internal void SetUsingSignPadData(bool isUsingSignPad)
		{
			base.IsUsingSignPad = isUsingSignPad;
		}

		internal void SetSignPadBefore(bool IsUsingSignPadBefore)
		{
			base.IsUsingSignPadBefore = IsUsingSignPadBefore;
		}

		internal void SetFileType(FileType _fileType)
		{
			base.FileType = _fileType;
		}

		private bool VerifyDataPreCallApi(HsmSignCreateTDO doc)
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
