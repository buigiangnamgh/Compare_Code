using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
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
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Expected O, but got Unknown
			//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d8: Expected O, but got Unknown
			bool result = false;
			if (ValidSignBoard(signedImageData))
			{
				LogSystem.Info("SignWithCreateDoc => 1");
				HsmSignCreateTDO val = new HsmSignCreateTDO();
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
				LogSystem.Info("SignWithCreateDoc => 1.1");
				if (((DocumentTDO)val).Signs != null)
				{
					foreach (SignTDO sign in ((DocumentTDO)val).Signs)
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
					val.IsSignElectronic = true;
				}
				((DocumentTDO)val).DependentCode = base.inputADOWorking.DependentCode;
				((DocumentTDO)val).ParentDependentCode = base.inputADOWorking.ParentDependentCode;
				val.RoomCode = base.inputADOWorking.RoomCode;
				val.RoomTypeCode = base.inputADOWorking.RoomTypeCode;
				val.WorkingDepartmentName = base.inputADOWorking.DepartmentName;
				((DocumentTDO)val).IsSignParallel = base.IsSignParanel;
				((DocumentTDO)val).MergeCode = mergeCode;
				((DocumentTDO)val).Base64Header = GetBase64HeaderFileData;
				if (oginalHeight > 0.0)
				{
					((DocumentTDO)val).OriginalHigh = (decimal)oginalHeight;
				}
				val.PointSign = pointSignTDO;
				val.Description = signDescription;
				((DocumentTDO)val).HisOrder = base.inputADOWorking.HisOrder;
				((DocumentTDO)val).OriginalVersion = new VersionTDO();
				LogSystem.Info("SignWithCreateDoc => 1.3");
				if (document != null && !string.IsNullOrEmpty(document.DocumentCode))
				{
					((DocumentTDO)val).DocumentCode = document.DocumentCode;
					((DocumentTDO)val).DocumentName = document.DocumentName;
					if (document.DocumentTypeId.HasValue && document.DocumentTypeId.Value > 0)
					{
						((DocumentTDO)val).DocumentTypeId = document.DocumentTypeId;
					}
					((DocumentTDO)val).OriginalVersion.DocumentCode = document.DocumentCode;
					((DocumentTDO)val).HisCode = document.HisCode;
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
				LogSystem.Info("SignWithCreateDoc => 1.4");
				if (!string.IsNullOrEmpty(base.inputADOWorking.DocumentGroupCode))
				{
					EMR_DOCUMENT_GROUP byCode = new EmrDocumentGroup().GetByCode(base.inputADOWorking.DocumentGroupCode);
					((DocumentTDO)val).DocumentGroupId = ((byCode != null) ? new long?(byCode.ID) : ((long?)null));
				}
				val.IsFinishSign = !isMultiSign;
				val.IsSigning = isMultiSign;
				((DocumentTDO)val).TreatmentCode = treatmentCode;
				if (document != null && document.OriginalVersion != null && !string.IsNullOrEmpty(document.OriginalVersion.Base64Data))
				{
					((DocumentTDO)val).OriginalVersion.Base64Data = document.OriginalVersion.Base64Data;
					((DocumentTDO)val).OriginalVersion.Base64DataJson = document.OriginalVersion.Base64DataJson;
					((DocumentTDO)val).OriginalVersion.Base64DataXml = document.OriginalVersion.Base64DataXml;
				}
				else
				{
					((DocumentTDO)val).OriginalVersion.Base64Data = GetBase64OriginalFileData;
				}
				((DocumentTDO)val).BusinessCode = base.inputADOWorking.BusinessCode;
				LogSystem.Info("SignWithCreateDoc => 1.5");
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
				LogSystem.Info("SignWithCreateDoc => 1.6");
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
				LogSystem.Info("SignWithCreateDoc => 1.7");
				if (!VerifyDataPreCallApi(val))
				{
					base.param.Messages.Add(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					MessageManager.Show(base.param, false);
					return false;
				}
				LogSystem.Info("SignWithCreateDoc => 2");
				CommonParam commonParam = new CommonParam();
				HsmSignCreateTDO rs = new EmrDocument(commonParam).CreateAndSignHsm(base.TokenCode, val);
				if (rs != null)
				{
					LogSystem.Info("SignWithCreateDoc => 3");
					result = true;
					document.DocumentCode = ((DocumentTDO)rs).DocumentCode;
					document.DocumentName = ((DocumentTDO)rs).DocumentName;
					document.DocumentTypeId = ((DocumentTDO)rs).DocumentTypeId;
					document.MergeCode = ((DocumentTDO)rs).MergeCode;
					document.TreatmentCode = ((DocumentTDO)rs).TreatmentCode;
					document.DependentCode = ((DocumentTDO)rs).DependentCode;
					document.ParentDependentCode = ((DocumentTDO)rs).ParentDependentCode;
					document.OriginalVersion = ((DocumentTDO)rs).OriginalVersion;
					document.PaperName = ((DocumentTDO)rs).PaperName;
					document.Width = ((DocumentTDO)rs).Width;
					document.Height = ((DocumentTDO)rs).Height;
					document.RawKind = ((DocumentTDO)rs).RawKind;
					if (((DocumentTDO)rs).Signs != null && ((DocumentTDO)rs).Signs.Count > 0)
					{
						foreach (SignTDO sign2 in ((DocumentTDO)rs).Signs)
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
					LogSystem.Info("SignWithCreateDoc => 5____" + LogUtil.TraceData(LogUtil.GetMemberName<HsmSignCreateTDO>((Expression<Func<HsmSignCreateTDO>>)(() => rs)), (object)rs) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>((Expression<Func<CommonParam>>)(() => param)), (object)base.param) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isCardAnonymous)), (object)isCardAnonymous));
				}
			}
			return result;
		}

		internal bool SignOnly(ref DocumentTDO document, List<SignTDO> signStrategys, List<SignTDO> signTemps, PointSignTDO pointSignTDO, string signDescription, bool isPatientSignOrHomeRelativeSign, bool isMultiSign, string cmnd, string cardCode, string serviceCode, bool isCardAnonymous, long? relationId, string relationName, string relationPeopleName, ref Stream outputStream, EMR_SIGN _signSelected, string mergeCode = "", string linkCode = "", byte[] signedImageData = null)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0204: Unknown result type (might be due to invalid IL or missing references)
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			//IL_0220: Unknown result type (might be due to invalid IL or missing references)
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_023c: Unknown result type (might be due to invalid IL or missing references)
			//IL_024f: Expected O, but got Unknown
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
					EMR_SIGN val = ((_signSelected != null) ? _signSelected : new EmrSign().GetSignDocumentFirst(viewByCode, isPatientSignOrHomeRelativeSign ? null : base.Signer, base.Treatment, true));
					if (val != null && val.ID > 0)
					{
						emrSignHsmSDO.EmrSignId = val.ID;
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
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<EmrSignHsmSDO>((Expression<Func<EmrSignHsmSDO>>)(() => emrSignHsmSDO)), (object)emrSignHsmSDO));
					EmrSignResultSDO val2 = new EmrDocument(commonParam).SignHsm(base.TokenCode, emrSignHsmSDO);
					if (val2 != null)
					{
						result = true;
						if (val2.EmrVersion != null && val2.EmrSign != null && !string.IsNullOrEmpty(val2.EmrVersion.URL))
						{
							outputStream = FssFileDownload.GetFile(val2.EmrVersion.URL);
						}
					}
					else
					{
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<EmrSignHsmSDO>((Expression<Func<EmrSignHsmSDO>>)(() => emrSignHsmSDO)), (object)emrSignHsmSDO));
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
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_021a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0228: Unknown result type (might be due to invalid IL or missing references)
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_0244: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Unknown result type (might be due to invalid IL or missing references)
			//IL_0265: Expected O, but got Unknown
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
					EMR_SIGN val = ((_signSelected != null) ? _signSelected : new EmrSign().GetSignDocumentFirst(viewByCode, (isPatientSign || isHomeRelativeSign) ? null : base.Signer, base.Treatment, true));
					if (val != null && val.ID > 0 && ((_signSelected == null && !IsHasBusinessCode) || (_signSelected != null && IsHasBusinessCode)))
					{
						emrSignHsmSDO.EmrSignId = val.ID;
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
					EmrSignResultSDO val2 = new EmrDocument(commonParam).PatientOrHomeRelativeSignHsm(base.TokenCode, emrSignHsmSDO);
					if (val2 != null)
					{
						result = true;
						if (val2.EmrVersion != null && val2.EmrSign != null && !string.IsNullOrEmpty(val2.EmrVersion.URL))
						{
							outputStream = FssFileDownload.GetFile(val2.EmrVersion.URL);
						}
					}
					else
					{
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<EmrSignHsmSDO>((Expression<Func<EmrSignHsmSDO>>)(() => emrSignHsmSDO)), (object)emrSignHsmSDO));
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
					if (GlobalStore.EMR_EMR_SIGN_CONNECT_DEVICE_TYPE_OPTION == "2")
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
