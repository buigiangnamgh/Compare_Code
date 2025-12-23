using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using EMR.EFMODEL.DataModels;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;
using Inventec.Common.SignLibrary.Popup;
using Inventec.Common.SignLibrary.SignHandler;

namespace Inventec.Common.SignLibrary
{
	internal class SignHandle : BussinessBase
	{
		private float x;

		private float y;

		private int pageNumberCurrent;

		private int totalPageNumber;

		private Stream stream;

		private Action dlgCancel;

		private Action<DocumentTDO> dlgOpenModuleConfig;

		private string signName;

		private string signDescription;

		private string documentName;

		private string treatmentCode;

		private List<VerifierADO> verifiers;

		private SignType signType;

		private List<SignTDO> tempSigns;

		private EMR_SIGN signSelected;

		private bool isPatientSign;

		private bool isHomeRelativeSign;

		private long? documentTypeId;

		private bool isMultiSign;

		private string hisCode;

		private Form parentForm;

		private DisplayConfigDTO displayConfigParam;

		private EMR_RELATION relation;

		private string relationShipName = "";

		private string SplitFileHeader = "";

		private string SplitFileContent = "";

		private double oginalHeight = 0.0;

		private FileType fileType;

		private bool isPatientConfirm;

		private Action<bool> delegateIssuanceCer;

		internal SignHandle(CommonParam param)
			: base(param)
		{
		}

		internal SignHandle(string _src, float _x, float _y, int _pageNumberCurrent, int _totalPageNumber, Action _dlgCancel, Action<DocumentTDO> _dlgOpenModuleConfig, string _documentName, string _treatmentCode, string _signName, string _signReason, List<VerifierADO> _verifiers, SignType _signType, List<SignTDO> _signTDOs, EMR_SIGN _signSelected, bool _isPatientSign, bool _isHomeRelativeSign, long? _documentTypeId, bool _isMultiSign, string hisCode, InputADO inputADOWorking, DisplayConfigDTO displayConfigDTO, CommonParam param, Form parentForm, bool isSignParanel, EMR_TREATMENT treatment, EMR_SIGNER singer, string tokenCode, Action<bool> _delegateIssuanceCer)
			: base(param)
		{
			x = _x;
			y = _y;
			pageNumberCurrent = _pageNumberCurrent;
			totalPageNumber = _totalPageNumber;
			dlgCancel = _dlgCancel;
			dlgOpenModuleConfig = _dlgOpenModuleConfig;
			base.Src = _src;
			signName = _signName;
			signDescription = _signReason;
			verifiers = _verifiers;
			signType = _signType;
			tempSigns = _signTDOs;
			documentName = _documentName;
			treatmentCode = _treatmentCode;
			signSelected = _signSelected;
			isPatientSign = _isPatientSign;
			isHomeRelativeSign = _isHomeRelativeSign;
			documentTypeId = _documentTypeId;
			isMultiSign = _isMultiSign;
			this.hisCode = hisCode;
			base.inputADOWorking = inputADOWorking;
			relationShipName = inputADOWorking.RelationPeopleName;
			relation = inputADOWorking.Relation;
			displayConfigParam = ((displayConfigDTO != null) ? displayConfigDTO : inputADOWorking.DisplayConfigDTO);
			this.parentForm = parentForm;
			base.IsSignParanel = isSignParanel;
			base.Treatment = treatment;
			base.Signer = singer;
			base.TokenCode = tokenCode;
			delegateIssuanceCer = _delegateIssuanceCer;
		}

		internal SignHandle(Stream _stream, float _x, float _y, int _pageNumberCurrent, int _totalPageNumber, Action _dlgCancel, Action<DocumentTDO> _dlgOpenModuleConfig, string _documentName, string _treatmentCode, string _signName, string _signReason, List<VerifierADO> _verifiers, SignType _signType, List<SignTDO> _signTDOs, EMR_SIGN _signSelected, bool _isPatientSign, bool _isHomeRelativeSign, long? _documentTypeId, bool _isMultiSign, string hisCode, InputADO inputADOWorking, DisplayConfigDTO displayConfigDTO, CommonParam param, Form parentForm, bool isSignParanel, EMR_TREATMENT treatment, EMR_SIGNER singer, string tokenCode, Action<bool> _delegateIssuanceCer)
			: base(param)
		{
			x = _x;
			y = _y;
			pageNumberCurrent = _pageNumberCurrent;
			totalPageNumber = _totalPageNumber;
			dlgCancel = _dlgCancel;
			dlgOpenModuleConfig = _dlgOpenModuleConfig;
			stream = _stream;
			signName = _signName;
			signDescription = _signReason;
			verifiers = _verifiers;
			signType = _signType;
			tempSigns = _signTDOs;
			documentName = _documentName;
			treatmentCode = _treatmentCode;
			signSelected = _signSelected;
			isPatientSign = _isPatientSign;
			isHomeRelativeSign = _isHomeRelativeSign;
			documentTypeId = _documentTypeId;
			isMultiSign = _isMultiSign;
			this.hisCode = hisCode;
			base.inputADOWorking = inputADOWorking;
			relationShipName = inputADOWorking.RelationPeopleName;
			relation = inputADOWorking.Relation;
			displayConfigParam = ((displayConfigDTO != null) ? displayConfigDTO : inputADOWorking.DisplayConfigDTO);
			this.parentForm = parentForm;
			base.IsSignParanel = isSignParanel;
			base.Treatment = treatment;
			base.Signer = singer;
			base.TokenCode = tokenCode;
			delegateIssuanceCer = _delegateIssuanceCer;
		}

		internal void SetUsingSignPad(bool isUsingSignPad)
		{
			base.IsUsingSignPad = isUsingSignPad;
		}

		public void SetSignPadBefore(byte[] SignPadImageData)
		{
			base.IsUsingSignPadBefore = SignPadImageData != null && SignPadImageData.Length != 0;
			base.SignPadImageData = SignPadImageData;
		}

		internal void SetFileType(FileType _FileType)
		{
			fileType = _FileType;
		}

		internal bool SignFile(DocumentTDO document, ref string outputFile)
		{
			bool result = false;
			bool flag = false;
			string cmnd = base.Treatment.CCCD_NUMBER;
			string cardCode = "";
			string serviceCode = "";
			string linkCode = "";
			string relationName = "";
			string relationPeopleName = "";
			byte[] signedImageData = base.SignPadImageData;
			long? relationId = null;
			SplitFileProcess();
			if (isPatientSign || isHomeRelativeSign || signType == SignType.HMS)
			{
				List<SignTDO> list = null;
				bool isCardAnonymous = false;
				SignHSMHandler signHSMHandler = new SignHSMHandler(base.param, base.inputADOWorking, base.Src, base.IsSignParanel, base.Treatment, base.Signer, base.TokenCode);
				if (isPatientSign || isHomeRelativeSign)
				{
					string err = "";
					if (tempSigns == null || tempSigns.Count == 0)
					{
						tempSigns = ((signSelected == null || (base.inputADOWorking != null && string.IsNullOrEmpty(base.inputADOWorking.BusinessCode))) ? SignStrategy(SignType.HMS, document) : null);
					}
					if (!Verify.VerifySigner(tempSigns, document, base.Treatment, ref err, true, base.IsUsingSignPad))
					{
						if (!string.IsNullOrEmpty(err))
						{
							MessageBox.Show(err);
						}
						LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => err)), (object)err));
						return false;
					}
					if (string.IsNullOrEmpty(base.Treatment.CARD_CODE) && base.inputADOWorking.DlgGetTreatment != null)
					{
						TreatmentDTO treatmentDTOHis = base.inputADOWorking.DlgGetTreatment(base.Treatment.TREATMENT_CODE);
						if (treatmentDTOHis != null && !string.IsNullOrEmpty(treatmentDTOHis.CARD_CODE))
						{
							LogSystem.Info("Truong hop khong co thong tin CardCode trong EMR_TREATMENT & trong input HIS gui sang ==> goi delegate lay cardcode theo treatmentCode:" + base.Treatment.TREATMENT_CODE + ", " + LogUtil.TraceData(LogUtil.GetMemberName<TreatmentDTO>((Expression<Func<TreatmentDTO>>)(() => treatmentDTOHis)), (object)treatmentDTOHis));
							base.Treatment.CARD_CODE = treatmentDTOHis.CARD_CODE;
						}
					}
					if (GlobalStore.EMR_SIGN_BOARD__OPTION == "2" && base.IsUsingSignPad)
					{
						WaitingManager.Hide();
						isCardAnonymous = true;
						if (isHomeRelativeSign)
						{
							if (string.IsNullOrWhiteSpace(relationShipName) || relation == null || (relation != null && string.IsNullOrEmpty(relation.RELATION_NAME)))
							{
								frmChoicePatientRelation frmChoicePatientRelation2 = new frmChoicePatientRelation(ActSelectRelationShip);
								frmChoicePatientRelation2.ShowDialog();
							}
							if (string.IsNullOrWhiteSpace(relationShipName) || relation == null)
							{
								MessageManager.Show(MessageUitl.GetMessage("KhongNhapTTDinhDanhHoacChonTheKy"));
								return false;
							}
							relationName = ((relation != null) ? relation.RELATION_NAME : "");
							relationId = ((relation != null) ? new long?(relation.ID) : ((long?)null));
							relationPeopleName = relationShipName;
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
										tempSign.RelationId = ((relation != null) ? new long?(relation.ID) : ((long?)null));
										tempSign.RelationName = ((relation != null) ? relation.RELATION_NAME : "");
										tempSign.RelationPeopleName = relationShipName;
									}
								}
							}
							else if (list != null && list.Count > 0)
							{
								foreach (SignTDO item in list)
								{
									if (!string.IsNullOrEmpty(item.PatientCode))
									{
										item.CmndNumber = cmnd;
										item.CardCode = cardCode;
										item.ServiceCode = serviceCode;
										item.LinkCode = linkCode;
										item.RelationId = ((relation != null) ? new long?(relation.ID) : ((long?)null));
										item.RelationName = ((relation != null) ? relation.RELATION_NAME : "");
										item.RelationPeopleName = relationShipName;
									}
								}
							}
						}
						LogSystem.Info("Vào TH BN hoặc người nhà ký & BN không có thẻ kcb => không kiểm tra xác thực vân tay:" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => Treatment.CARD_CODE)), (object)base.Treatment.CARD_CODE) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => relationName)), (object)relationName) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => relationPeopleName)), (object)relationPeopleName));
						signHSMHandler.SetUsingSignPadData(base.IsUsingSignPad);
						signHSMHandler.SetSignPadBefore(base.IsUsingSignPadBefore);
						WaitingManager.Show();
					}
					else if (isPatientSign && GlobalStore.SIGN_CERTIFICATE_OPTION == "1")
					{
						WaitingManager.Hide();
						isPatientConfirm = false;
						frmPatientAuthenticationSigner frmPatientAuthenticationSigner = new frmPatientAuthenticationSigner(base.inputADOWorking, base.Treatment, PatientConfirm, delegate(string rs)
						{
							signedImageData = Convert.FromBase64String(rs);
						}, true);
						frmPatientAuthenticationSigner.ShowDialog();
						string verifiedIdentifyNumber = frmPatientAuthenticationSigner.VerifiedIdentifyNumber;
						LogSystem.Info("IdentifyNumber after frmPatientAuthenticationSigner: " + verifiedIdentifyNumber);
						if (!isPatientConfirm)
						{
							return false;
						}
						if (tempSigns != null && tempSigns.Count > 0)
						{
							foreach (SignTDO tempSign2 in tempSigns)
							{
								if (!string.IsNullOrEmpty(tempSign2.PatientCode))
								{
									tempSign2.CmndNumber = verifiedIdentifyNumber;
									LogSystem.Info("tempSigns PatientCode: " + verifiedIdentifyNumber);
									tempSign2.CardCode = cardCode;
									tempSign2.ServiceCode = serviceCode;
									tempSign2.LinkCode = linkCode;
								}
							}
						}
					}
					else if (isHomeRelativeSign && GlobalStore.SIGN_CERTIFICATE_OPTION == "1")
					{
						isPatientConfirm = false;
						WaitingManager.Hide();
						if (string.IsNullOrWhiteSpace(relationShipName) || relation == null || (relation != null && string.IsNullOrEmpty(relation.RELATION_NAME)))
						{
							frmChoicePatientRelation frmChoicePatientRelation3 = new frmChoicePatientRelation(ActSelectRelationShip);
							frmChoicePatientRelation3.ShowDialog();
						}
						relationName = ((relation != null) ? relation.RELATION_NAME : "");
						relationId = ((relation != null) ? new long?(relation.ID) : ((long?)null));
						relationPeopleName = relationShipName;
						frmPatientAuthenticationSigner frmPatientAuthenticationSigner2 = new frmPatientAuthenticationSigner(base.inputADOWorking, base.Treatment, PatientConfirm, delegate(string rs)
						{
							signedImageData = Convert.FromBase64String(rs);
						}, false);
						frmPatientAuthenticationSigner2.ShowDialog();
						string verifiedIdentifyNumber2 = frmPatientAuthenticationSigner2.VerifiedIdentifyNumber;
						LogSystem.Info("Vao TH Nha nguoi benh ky & su dung the dinh danh => so CMND nguoi nha ky la: " + verifiedIdentifyNumber2);
						if (!isPatientConfirm)
						{
							return false;
						}
						if (tempSigns != null && tempSigns.Count > 0)
						{
							foreach (SignTDO tempSign3 in tempSigns)
							{
								if (!string.IsNullOrEmpty(tempSign3.PatientCode))
								{
									tempSign3.CmndNumber = verifiedIdentifyNumber2;
									LogSystem.Info("tempSigns PatientCode: " + verifiedIdentifyNumber2);
									tempSign3.CardCode = cardCode;
									tempSign3.ServiceCode = serviceCode;
									tempSign3.LinkCode = linkCode;
									tempSign3.RelationId = ((relation != null) ? new long?(relation.ID) : ((long?)null));
									tempSign3.RelationName = ((relation != null) ? relation.RELATION_NAME : "");
									tempSign3.RelationPeopleName = relationShipName;
								}
							}
						}
					}
					else if (!FingerPrintClientService.Valid(isHomeRelativeSign, ref cmnd, ref cardCode, ref serviceCode, ref linkCode, ref relationName, ref relationPeopleName, ref signedImageData, ref isCardAnonymous, ref tempSigns, base.Treatment))
					{
						return false;
					}
				}
				else
				{
					list = ((signSelected == null || (base.inputADOWorking != null && string.IsNullOrEmpty(base.inputADOWorking.BusinessCode))) ? SignStrategy(SignType.HMS, document) : null);
				}
				Stream output = null;
				signHSMHandler.SetFileType(fileType);
				flag = ((document == null || string.IsNullOrEmpty(document.DocumentCode)) ? signHSMHandler.SignWithCreateDoc(outputFile, ref document, documentName, treatmentCode, list, tempSigns, GetBase64FileData(), GetBase64HeaderFileData(), GetPointSign(), signDescription, isPatientSign || isHomeRelativeSign, documentTypeId, isMultiSign, hisCode, isCardAnonymous, ref output, signSelected, base.inputADOWorking.MergeCode, oginalHeight, signedImageData) : ((!(GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION == "3") || (!base.IsUsingSignPad && !(GlobalStore.SIGN_CERTIFICATE_OPTION != "1")) || (!isPatientSign && !isHomeRelativeSign) || (signSelected != null && !string.IsNullOrEmpty(signSelected.PATIENT_CODE))) ? signHSMHandler.SignOnly(ref document, list, tempSigns, GetPointSign(), signDescription, isPatientSign || isHomeRelativeSign, isMultiSign, cmnd, cardCode, serviceCode, isCardAnonymous, relationId, relationName, relationPeopleName, ref output, signSelected, base.inputADOWorking.MergeCode, linkCode, signedImageData) : signHSMHandler.PatientOrHomeRelativeSignOnly(ref document, list, tempSigns, GetPointSign(), signDescription, isPatientSign, isHomeRelativeSign, isMultiSign, cmnd, cardCode, serviceCode, isCardAnonymous, relationId, relationName, relationPeopleName, ref output, signSelected, base.inputADOWorking.MergeCode, linkCode, signedImageData, !string.IsNullOrEmpty(base.inputADOWorking.BusinessCode))));
				if (!(isMultiSign && flag))
				{
					WaitingManager.Hide();
					MessageManager.Show(parentForm, base.param, flag);
				}
				if (flag && output != null && output.Length > 0)
				{
					output.Position = 0L;
					outputFile = Utils.GenerateTempFileWithin();
					using (FileStream destination = File.OpenWrite(outputFile))
					{
						output.Seek(0L, SeekOrigin.Begin);
						output.CopyTo(destination);
					}
					try
					{
						output.Close();
						output.Dispose();
						output = null;
					}
					catch (Exception ex)
					{
						LogSystem.Warn(ex);
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else if (signType == SignType.USB)
			{
				bool flag2 = false;
				SignUSBHandler signUSBHandler = new SignUSBHandler(base.param, base.inputADOWorking, base.IsSignParanel, base.Treatment, base.Signer, base.TokenCode);
				signUSBHandler.SetFileType(fileType);
				flag2 = ((GlobalStore.IsSignUsingUsbTokenDevice || !signUSBHandler.VerifyServiceSignProcessorIsRunning()) ? signUSBHandler.SignFileWithUSBTokenTSAWithUsingUsbTokenDevice(ref outputFile, base.Src, stream, x, y, pageNumberCurrent, totalPageNumber, signDescription, displayConfigParam, dlgCancel) : signUSBHandler.SignFileWithUSBTokenTSAWithCallService(ref outputFile, base.Src, stream, x, y, pageNumberCurrent, totalPageNumber, signDescription, displayConfigParam, dlgCancel));
				if (flag2 && !string.IsNullOrEmpty(outputFile) && File.Exists(outputFile))
				{
					List<SignTDO> signStrategys = ((signSelected == null) ? SignStrategy(SignType.USB, document) : null);
					flag = ((document == null || string.IsNullOrEmpty(document.DocumentCode)) ? signUSBHandler.SignWithCreateDoc(outputFile, ref document, documentName, treatmentCode, signStrategys, tempSigns, GetBase64FileData(outputFile), GetBase64FileData(), documentTypeId, isMultiSign, hisCode, signSelected, signDescription, base.inputADOWorking.MergeCode) : signUSBHandler.SignOnly(ref document, isMultiSign, signStrategys, tempSigns, GetBase64FileData(outputFile), signSelected, signDescription, base.inputADOWorking.MergeCode));
					if (!(isMultiSign && flag))
					{
						WaitingManager.Hide();
						MessageManager.Show(parentForm, base.param, flag);
					}
					if (flag)
					{
						try
						{
						}
						catch (Exception ex2)
						{
							LogSystem.Warn(ex2);
						}
						if (stream != null)
						{
							stream.Close();
						}
						return true;
					}
					return false;
				}
			}
			else
			{
				MessageBox.Show("Loại ký này chưa được hỗ trợ (SignType: " + signType.ToString() + ")");
				LogSystem.Warn("Loại ký này chưa được hỗ trợ (SignType: " + signType.ToString() + ")");
			}
			return result;
		}

		private void PatientConfirm(bool success)
		{
			try
			{
				isPatientConfirm = success;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ActSelectRelationShip(EMR_RELATION _relation, string _relationShipName)
		{
			try
			{
				relation = _relation;
				relationShipName = _relationShipName;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ActSelectDevice(string deviceName)
		{
			try
			{
				deviceSignPadName = deviceName;
				if (base.inputADOWorking.ActSelectDevice != null)
				{
					base.inputADOWorking.ActSelectDevice(deviceName);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void GetSignImageFIle(Bitmap bmpSignImage)
		{
			try
			{
				if (bmpSignImage != null)
				{
					base.SignPadImageData = Utils.ImageToByte(bmpSignImage);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SplitFileProcess()
		{
			try
			{
				SplitFileHeader = "";
				SplitFileContent = "";
				oginalHeight = 0.0;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal DocumentTDO SendDocument(DocumentTDO document)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			//IL_039c: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a6: Expected O, but got Unknown
			DocumentTDO val = new DocumentTDO();
			val.TreatmentCode = treatmentCode;
			val.DocumentName = (string.IsNullOrEmpty(documentName) ? ("Ký điện tử cho hồ sơ có mã " + treatmentCode + " ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) : documentName);
			List<SignTDO> list = null;
			val.Signs = tempSigns;
			if (list != null && list.Count > 0)
			{
				if (val.Signs == null)
				{
					val.Signs = new List<SignTDO>();
				}
				val.Signs.AddRange(list);
			}
			if (val.Signs != null && val.Signs.Count > 0)
			{
				foreach (SignTDO sign in val.Signs)
				{
					sign.SignTime = null;
				}
			}
			val.BusinessCode = base.inputADOWorking.BusinessCode;
			val.HisCode = hisCode;
			val.DocumentTypeId = documentTypeId;
			val.DependentCode = base.inputADOWorking.DependentCode;
			val.ParentDependentCode = base.inputADOWorking.ParentDependentCode;
			val.HisOrder = base.inputADOWorking.HisOrder;
			val.IsOutsideTreatment = base.inputADOWorking.IsOutsideTreatment == 1;
			val.MediOrgCode = base.inputADOWorking.MediOrgCode;
			if (!string.IsNullOrEmpty(base.inputADOWorking.DocumentGroupCode))
			{
				EMR_DOCUMENT_GROUP byCode = new EmrDocumentGroup().GetByCode(base.inputADOWorking.DocumentGroupCode);
				val.DocumentGroupId = ((byCode != null) ? new long?(byCode.ID) : ((long?)null));
			}
			val.MergeCode = base.inputADOWorking.MergeCode;
			if (base.inputADOWorking.DocumentTime.HasValue && base.inputADOWorking.DocumentTime.Value != DateTime.MinValue)
			{
				val.DocumentTime = DateTimeConvert.SystemDateTimeToTimeNumber(base.inputADOWorking.DocumentTime);
			}
			if (base.inputADOWorking.PaperSizeDefault != null)
			{
				val.PaperName = base.inputADOWorking.PaperSizeDefault.PaperName;
				if (string.IsNullOrEmpty(val.PaperName))
				{
					val.PaperName = base.inputADOWorking.PaperSizeDefault.Kind.ToString();
				}
				val.Width = base.inputADOWorking.PaperSizeDefault.Width;
				val.Height = base.inputADOWorking.PaperSizeDefault.Height;
				val.RawKind = base.inputADOWorking.PaperSizeDefault.RawKind;
			}
			val.IsSignParallel = ((document != null) ? document.IsSignParallel : new bool?(false));
			SplitFileProcess();
			if (!string.IsNullOrEmpty(SplitFileHeader) && File.Exists(SplitFileHeader))
			{
				val.Base64Header = Utils.FileToBase64String(SplitFileHeader);
			}
			val.OriginalVersion = new VersionTDO();
			if (document.OriginalVersion != null && !string.IsNullOrEmpty(document.OriginalVersion.Base64Data))
			{
				val.OriginalVersion.Base64Data = document.OriginalVersion.Base64Data;
				val.OriginalVersion.Base64DataJson = document.OriginalVersion.Base64DataJson;
				val.OriginalVersion.Base64DataXml = document.OriginalVersion.Base64DataXml;
			}
			else
			{
				val.OriginalVersion.Base64Data = GetBase64FileData();
			}
			if (base.FileType == FileType.Xml)
			{
				val.FileType = (FileType)1;
			}
			else if (base.FileType == FileType.Json)
			{
				val.FileType = (FileType)2;
			}
			else
			{
				val.FileType = (FileType)0;
			}
			CommonParam commonParam = new CommonParam();
			EmrDocument emrDocument = new EmrDocument(commonParam);
			DocumentTDO result = emrDocument.CreateByTdo(base.TokenCode, val);
			base.param.Messages = commonParam.Messages;
			base.param.BugCodes = commonParam.BugCodes;
			return result;
		}

		internal bool VerifyFile(DocumentTDO document, int signedCount, ref string message)
		{
			try
			{
				if (signedCount > 0 && pageNumberCurrent < 1)
				{
					WaitingManager.Hide();
					message = MessageUitl.GetMessage("BanKhongTheThucHienKyTaiTrangKyVuiLongChonKyTuTrangThu2TroDi");
					MessageManager.Show(message);
					return false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return true;
		}

		private List<SignTDO> SignStrategy(SignType signType, DocumentTDO document)
		{
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Expected O, but got Unknown
			SignTDO val = null;
			List<SignTDO> list = new List<SignTDO>();
			try
			{
				if (base.Signer == null)
				{
					MessageBox.Show("Dữ liệu người dùng không hợp lệ, không tìm thấy thông tin người ký trên hệ thống EMR");
					LogSystem.Warn("Dữ liệu người dùng không hợp lệ, không tìm thấy thông tin người ký trên hệ thống EMR");
					return null;
				}
				string documentCode = "";
				if (document != null)
				{
					documentCode = document.DocumentCode;
				}
				if (tempSigns != null && tempSigns.Count > 0)
				{
					val = (from o in tempSigns
						where o.Loginname == base.Signer.LOGINNAME
						orderby o.NumOrder
						select o).FirstOrDefault();
					if (val != null)
					{
						val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
						val.SignerId = base.Signer.ID;
						val.NumOrder = ((val.NumOrder == 0L) ? 1 : val.NumOrder);
					}
				}
				else
				{
					val = new EmrSign().GetSignDocumentFirstByLoginName(documentCode, base.Signer);
					if (val != null)
					{
						val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
						val.SignerId = base.Signer.ID;
						val.NumOrder = ((val.NumOrder == 0L) ? 1 : val.NumOrder);
						list.Add(val);
					}
				}
				if (val == null)
				{
					val = new SignTDO();
					if (isPatientSign || isHomeRelativeSign)
					{
						if (tempSigns == null || tempSigns.Count == 0)
						{
							val.Username = base.Treatment.VIR_PATIENT_NAME;
							val.NumOrder = 1L;
							val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							val.PatientCode = base.Treatment.PATIENT_CODE;
							val.FirstName = base.Treatment.FIRST_NAME;
							val.LastName = base.Treatment.LAST_NAME;
							val.FullName = base.Treatment.VIR_PATIENT_NAME;
							list.Add(val);
						}
					}
					else
					{
						val.Loginname = base.Signer.LOGINNAME;
						val.Username = base.Signer.USERNAME;
						val.FirstName = base.Signer.USERNAME;
						val.Title = base.Signer.TITLE;
						val.DepartmentCode = base.Signer.DEPARTMENT_CODE;
						val.DepartmentName = base.Signer.DEPARTMENT_NAME;
						val.NumOrder = 1L;
						val.SignerId = base.Signer.ID;
						val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
						list.Add(val);
					}
				}
			}
			catch (Exception)
			{
				list = null;
			}
			return list;
		}

		private bool VerifyDataPreCallApi(HsmSignCreateTDO doc)
		{
			return true && doc != null && ((DocumentTDO)doc).OriginalVersion != null && ((DocumentTDO)doc).TreatmentCode != null;
		}

		private bool VerifyDataPreCallApi(UsbSignCreateTDO doc)
		{
			return true && doc != null && ((DocumentTDO)doc).OriginalVersion != null && ((DocumentTDO)doc).TreatmentCode != null;
		}

		private string GetBase64FileData()
		{
			string text = "";
			MemoryStream memoryStream = new MemoryStream();
			if (!string.IsNullOrEmpty(SplitFileContent) && File.Exists(SplitFileContent))
			{
				return GetBase64FileData(SplitFileContent);
			}
			if (!string.IsNullOrEmpty(base.Src))
			{
				return GetBase64FileData(base.Src);
			}
			stream.CopyTo(memoryStream);
			memoryStream.Position = 0L;
			return Convert.ToBase64String(memoryStream.ToArray());
		}

		private string GetBase64HeaderFileData()
		{
			string result = "";
			if (!string.IsNullOrEmpty(SplitFileHeader) && File.Exists(SplitFileHeader))
			{
				result = GetBase64FileData(SplitFileHeader);
			}
			return result;
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

		private PointSignTDO GetPointSign()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			PointSignTDO val = new PointSignTDO();
			val.CoorXRectangle = x;
			val.CoorYRectangle = y;
			val.MaxPageNumber = totalPageNumber;
			val.PageNumber = pageNumberCurrent;
			if (displayConfigParam != null)
			{
				if (displayConfigParam.WidthRectangle.HasValue)
				{
					val.WidthRectangle = displayConfigParam.WidthRectangle.Value;
				}
				if (displayConfigParam.HeightRectangle.HasValue)
				{
					val.HeightRectangle = displayConfigParam.HeightRectangle.Value;
				}
				if (displayConfigParam.SizeFont.HasValue)
				{
					val.SizeFont = displayConfigParam.SizeFont.Value;
				}
				if (displayConfigParam.TextPosition.HasValue)
				{
					val.TextPosition = displayConfigParam.TextPosition.Value;
				}
				if (displayConfigParam.TypeDisplay.HasValue)
				{
					val.TypeDisplay = displayConfigParam.TypeDisplay.Value;
				}
				if (displayConfigParam.IsDisplaySignature.HasValue)
				{
					val.IsDisplaySignature = displayConfigParam.IsDisplaySignature.Value;
				}
				if (!string.IsNullOrEmpty(displayConfigParam.FormatRectangleText))
				{
					val.FormatRectangleText = displayConfigParam.FormatRectangleText;
				}
				if (displayConfigParam.Alignment.HasValue)
				{
					val.Alignment = displayConfigParam.Alignment.Value;
				}
				if (displayConfigParam.IsBold.HasValue)
				{
					val.IsBold = displayConfigParam.IsBold.Value;
				}
				if (displayConfigParam.IsUnderlined.HasValue)
				{
					val.IsUnderlined = displayConfigParam.IsUnderlined.Value;
				}
				if (displayConfigParam.IsItalic.HasValue)
				{
					val.IsItalic = displayConfigParam.IsItalic.Value;
				}
				if (!string.IsNullOrEmpty(displayConfigParam.FontName))
				{
					val.FontName = displayConfigParam.FontName;
				}
			}
			return val;
		}
	}
}
