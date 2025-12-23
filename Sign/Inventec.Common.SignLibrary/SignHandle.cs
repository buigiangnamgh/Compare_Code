using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

		private short? IsOutsideTreatment { get; set; }

		private string MediOrgCode { get; set; }

		internal SignHandle(string _src, float _x, float _y, int _pageNumberCurrent, int _totalPageNumber, Action _dlgCancel, Action<DocumentTDO> _dlgOpenModuleConfig, string _documentName, string _treatmentCode, string _signName, string _signReason, List<VerifierADO> _verifiers, SignType _signType, List<SignTDO> _signTDOs, EMR_SIGN _signSelected, bool _isPatientSign, bool _isHomeRelativeSign, long? _documentTypeId, bool _isMultiSign, string hisCode, InputADO inputADOWorking, DisplayConfigDTO displayConfigDTO, CommonParam param, Form parentForm, bool isSignParanel, EMR_TREATMENT treatment, EMR_SIGNER singer, string tokenCode)
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
		}

		internal SignHandle(Stream _stream, float _x, float _y, int _pageNumberCurrent, int _totalPageNumber, Action _dlgCancel, Action<DocumentTDO> _dlgOpenModuleConfig, string _documentName, string _treatmentCode, string _signName, string _signReason, List<VerifierADO> _verifiers, SignType _signType, List<SignTDO> _signTDOs, EMR_SIGN _signSelected, bool _isPatientSign, bool _isHomeRelativeSign, long? _documentTypeId, bool _isMultiSign, string hisCode, InputADO inputADOWorking, DisplayConfigDTO displayConfigDTO, CommonParam param, Form parentForm, bool isSignParanel, EMR_TREATMENT treatment, EMR_SIGNER singer, string tokenCode)
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
		}

		internal void SetUsingSignPad(bool isUsingSignPad)
		{
			base.IsUsingSignPad = isUsingSignPad;
		}

		internal void SetSignPadBefore(byte[] SignPadImageData)
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
						LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => err), err));
						return false;
					}
					if (string.IsNullOrEmpty(base.Treatment.CARD_CODE) && base.inputADOWorking.DlgGetTreatment != null)
					{
						TreatmentDTO treatmentDTOHis = base.inputADOWorking.DlgGetTreatment(base.Treatment.TREATMENT_CODE);
						if (treatmentDTOHis != null && !string.IsNullOrEmpty(treatmentDTOHis.CARD_CODE))
						{
							LogSystem.Info("Truong hop khong co thong tin CardCode trong EMR_TREATMENT & trong input HIS gui sang ==> goi delegate lay cardcode theo treatmentCode:" + base.Treatment.TREATMENT_CODE + ", " + LogUtil.TraceData(LogUtil.GetMemberName(() => treatmentDTOHis), treatmentDTOHis));
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
						LogSystem.Info("Vào TH BN hoặc người nhà ký & BN không có thẻ kcb => không kiểm tra xác thực vân tay:" + LogUtil.TraceData(LogUtil.GetMemberName(() => Treatment.CARD_CODE), base.Treatment.CARD_CODE) + LogUtil.TraceData(LogUtil.GetMemberName(() => relationName), relationName) + LogUtil.TraceData(LogUtil.GetMemberName(() => relationPeopleName), relationPeopleName));
						signHSMHandler.SetUsingSignPadData(base.IsUsingSignPad);
						signHSMHandler.SetSignPadBefore(base.IsUsingSignPadBefore);
						WaitingManager.Show();
					}
					else if (isPatientSign && GlobalStore.SIGN_CERTIFICATE_OPTION == "1")
					{
						isPatientConfirm = false;
						frmPatientAuthenticationSigner frmPatientAuthenticationSigner = new frmPatientAuthenticationSigner(base.inputADOWorking, base.Treatment, PatientConfirm);
						frmPatientAuthenticationSigner.ShowDialog();
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
									tempSign2.CmndNumber = cmnd;
									tempSign2.CardCode = cardCode;
									tempSign2.ServiceCode = serviceCode;
									tempSign2.LinkCode = linkCode;
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
				flag = ((document == null || string.IsNullOrEmpty(document.DocumentCode)) ? signHSMHandler.SignWithCreateDoc(outputFile, ref document, documentName, treatmentCode, list, tempSigns, GetBase64FileData(), GetBase64HeaderFileData(), GetPointSign(), signDescription, isPatientSign || isHomeRelativeSign, documentTypeId, isMultiSign, hisCode, isCardAnonymous, ref output, signSelected, base.inputADOWorking.MergeCode, oginalHeight, signedImageData) : ((!(GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION == "3") || !(GlobalStore.SIGN_CERTIFICATE_OPTION != "1") || (!isPatientSign && !isHomeRelativeSign) || (signSelected != null && !string.IsNullOrEmpty(signSelected.PATIENT_CODE))) ? signHSMHandler.SignOnly(ref document, list, tempSigns, GetPointSign(), signDescription, isPatientSign || isHomeRelativeSign, isMultiSign, cmnd, cardCode, serviceCode, isCardAnonymous, relationId, relationName, relationPeopleName, ref output, signSelected, base.inputADOWorking.MergeCode, linkCode, signedImageData) : signHSMHandler.PatientOrHomeRelativeSignOnly(ref document, list, tempSigns, GetPointSign(), signDescription, isPatientSign, isHomeRelativeSign, isMultiSign, cmnd, cardCode, serviceCode, isCardAnonymous, relationId, relationName, relationPeopleName, ref output, signSelected, base.inputADOWorking.MergeCode, linkCode, signedImageData, !string.IsNullOrEmpty(base.inputADOWorking.BusinessCode))));
				if (!isMultiSign || !flag)
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
				if (((GlobalStore.IsSignUsingUsbTokenDevice || !signUSBHandler.VerifyServiceSignProcessorIsRunning()) ? signUSBHandler.SignFileWithUSBTokenTSAWithUsingUsbTokenDevice(ref outputFile, base.Src, stream, x, y, pageNumberCurrent, totalPageNumber, signDescription, displayConfigParam, dlgCancel) : signUSBHandler.SignFileWithUSBTokenTSAWithCallService(ref outputFile, base.Src, stream, x, y, pageNumberCurrent, totalPageNumber, signDescription, displayConfigParam, dlgCancel)) && !string.IsNullOrEmpty(outputFile) && File.Exists(outputFile))
				{
					List<SignTDO> signStrategys = ((signSelected == null) ? SignStrategy(SignType.USB, document) : null);
					flag = ((document == null || string.IsNullOrEmpty(document.DocumentCode)) ? signUSBHandler.SignWithCreateDoc(outputFile, ref document, documentName, treatmentCode, signStrategys, tempSigns, GetBase64FileData(outputFile), GetBase64FileData(), documentTypeId, isMultiSign, hisCode, signSelected, signDescription, base.inputADOWorking.MergeCode) : signUSBHandler.SignOnly(ref document, isMultiSign, signStrategys, tempSigns, GetBase64FileData(outputFile), signSelected, signDescription, base.inputADOWorking.MergeCode));
					if (!isMultiSign || !flag)
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
			DocumentTDO documentTDO = new DocumentTDO();
			documentTDO.TreatmentCode = treatmentCode;
			documentTDO.DocumentName = (string.IsNullOrEmpty(documentName) ? ("Ký điện tử cho hồ sơ có mã " + treatmentCode + " ngày " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) : documentName);
			List<SignTDO> list = null;
			documentTDO.Signs = tempSigns;
			if (list != null && list.Count > 0)
			{
				if (documentTDO.Signs == null)
				{
					documentTDO.Signs = new List<SignTDO>();
				}
				documentTDO.Signs.AddRange(list);
			}
			if (documentTDO.Signs != null && documentTDO.Signs.Count > 0)
			{
				foreach (SignTDO sign in documentTDO.Signs)
				{
					sign.SignTime = null;
				}
			}
			documentTDO.BusinessCode = base.inputADOWorking.BusinessCode;
			documentTDO.HisCode = hisCode;
			documentTDO.DocumentTypeId = documentTypeId;
			documentTDO.DependentCode = base.inputADOWorking.DependentCode;
			documentTDO.ParentDependentCode = base.inputADOWorking.ParentDependentCode;
			documentTDO.HisOrder = base.inputADOWorking.HisOrder;
			documentTDO.IsOutsideTreatment = base.inputADOWorking.IsOutsideTreatment == 1;
			documentTDO.MediOrgCode = base.inputADOWorking.MediOrgCode;
			if (!string.IsNullOrEmpty(base.inputADOWorking.DocumentGroupCode))
			{
				EMR_DOCUMENT_GROUP byCode = new EmrDocumentGroup().GetByCode(base.inputADOWorking.DocumentGroupCode);
				documentTDO.DocumentGroupId = ((byCode != null) ? new long?(byCode.ID) : ((long?)null));
			}
			documentTDO.MergeCode = base.inputADOWorking.MergeCode;
			if (base.inputADOWorking.DocumentTime.HasValue && base.inputADOWorking.DocumentTime.Value != DateTime.MinValue)
			{
				documentTDO.DocumentTime = DateTimeConvert.SystemDateTimeToTimeNumber(base.inputADOWorking.DocumentTime);
			}
			if (base.inputADOWorking.PaperSizeDefault != null)
			{
				documentTDO.PaperName = base.inputADOWorking.PaperSizeDefault.PaperName;
				if (string.IsNullOrEmpty(documentTDO.PaperName))
				{
					documentTDO.PaperName = base.inputADOWorking.PaperSizeDefault.Kind.ToString();
				}
				documentTDO.Width = base.inputADOWorking.PaperSizeDefault.Width;
				documentTDO.Height = base.inputADOWorking.PaperSizeDefault.Height;
				documentTDO.RawKind = base.inputADOWorking.PaperSizeDefault.RawKind;
			}
			documentTDO.IsSignParallel = ((document != null) ? document.IsSignParallel : new bool?(false));
			SplitFileProcess();
			if (!string.IsNullOrEmpty(SplitFileHeader) && File.Exists(SplitFileHeader))
			{
				documentTDO.Base64Header = Utils.FileToBase64String(SplitFileHeader);
			}
			documentTDO.OriginalVersion = new VersionTDO();
			if (document.OriginalVersion != null && !string.IsNullOrEmpty(document.OriginalVersion.Base64Data))
			{
				documentTDO.OriginalVersion.Base64Data = document.OriginalVersion.Base64Data;
				documentTDO.OriginalVersion.Base64DataJson = document.OriginalVersion.Base64DataJson;
				documentTDO.OriginalVersion.Base64DataXml = document.OriginalVersion.Base64DataXml;
			}
			else
			{
				documentTDO.OriginalVersion.Base64Data = GetBase64FileData();
			}
			if (base.FileType == FileType.Xml)
			{
				documentTDO.FileType = EMR.TDO.FileType.XML;
			}
			else if (base.FileType == FileType.Json)
			{
				documentTDO.FileType = EMR.TDO.FileType.JSON;
			}
			else
			{
				documentTDO.FileType = EMR.TDO.FileType.PDF;
			}
			CommonParam commonParam = new CommonParam();
			EmrDocument emrDocument = new EmrDocument(commonParam);
			DocumentTDO result = emrDocument.CreateByTdo(base.TokenCode, documentTDO);
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
			SignTDO signTDO = null;
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
					signTDO = (from o in tempSigns
						where o.Loginname == base.Signer.LOGINNAME
						orderby o.NumOrder
						select o).FirstOrDefault();
					if (signTDO != null)
					{
						signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
						signTDO.SignerId = base.Signer.ID;
						signTDO.NumOrder = ((signTDO.NumOrder == 0) ? 1 : signTDO.NumOrder);
					}
				}
				else
				{
					signTDO = new EmrSign().GetSignDocumentFirstByLoginName(documentCode, base.Signer);
					if (signTDO != null)
					{
						signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
						signTDO.SignerId = base.Signer.ID;
						signTDO.NumOrder = ((signTDO.NumOrder == 0) ? 1 : signTDO.NumOrder);
						list.Add(signTDO);
					}
				}
				if (signTDO == null)
				{
					signTDO = new SignTDO();
					if (isPatientSign || isHomeRelativeSign)
					{
						if (tempSigns == null || tempSigns.Count == 0)
						{
							signTDO.Username = base.Treatment.VIR_PATIENT_NAME;
							signTDO.NumOrder = 1L;
							signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							signTDO.PatientCode = base.Treatment.PATIENT_CODE;
							signTDO.FirstName = base.Treatment.FIRST_NAME;
							signTDO.LastName = base.Treatment.LAST_NAME;
							signTDO.FullName = base.Treatment.VIR_PATIENT_NAME;
							list.Add(signTDO);
						}
					}
					else
					{
						signTDO.Loginname = base.Signer.LOGINNAME;
						signTDO.Username = base.Signer.USERNAME;
						signTDO.FirstName = base.Signer.USERNAME;
						signTDO.Title = base.Signer.TITLE;
						signTDO.DepartmentCode = base.Signer.DEPARTMENT_CODE;
						signTDO.DepartmentName = base.Signer.DEPARTMENT_NAME;
						signTDO.NumOrder = 1L;
						signTDO.SignerId = base.Signer.ID;
						signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
						list.Add(signTDO);
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
			return doc != null && doc.OriginalVersion != null && doc.TreatmentCode != null;
		}

		private bool VerifyDataPreCallApi(UsbSignCreateTDO doc)
		{
			return doc != null && doc.OriginalVersion != null && doc.TreatmentCode != null;
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
			PointSignTDO pointSignTDO = new PointSignTDO();
			pointSignTDO.CoorXRectangle = x;
			pointSignTDO.CoorYRectangle = y;
			pointSignTDO.MaxPageNumber = totalPageNumber;
			pointSignTDO.PageNumber = pageNumberCurrent;
			if (displayConfigParam != null)
			{
				if (displayConfigParam.WidthRectangle.HasValue)
				{
					pointSignTDO.WidthRectangle = displayConfigParam.WidthRectangle.Value;
				}
				if (displayConfigParam.HeightRectangle.HasValue)
				{
					pointSignTDO.HeightRectangle = displayConfigParam.HeightRectangle.Value;
				}
				if (displayConfigParam.SizeFont.HasValue)
				{
					pointSignTDO.SizeFont = displayConfigParam.SizeFont.Value;
				}
				if (displayConfigParam.TextPosition.HasValue)
				{
					pointSignTDO.TextPosition = displayConfigParam.TextPosition.Value;
				}
				if (displayConfigParam.TypeDisplay.HasValue)
				{
					pointSignTDO.TypeDisplay = displayConfigParam.TypeDisplay.Value;
				}
				if (displayConfigParam.IsDisplaySignature.HasValue)
				{
					pointSignTDO.IsDisplaySignature = displayConfigParam.IsDisplaySignature.Value;
				}
				if (!string.IsNullOrEmpty(displayConfigParam.FormatRectangleText))
				{
					pointSignTDO.FormatRectangleText = displayConfigParam.FormatRectangleText;
				}
				if (displayConfigParam.Alignment.HasValue)
				{
					pointSignTDO.Alignment = displayConfigParam.Alignment.Value;
				}
				if (displayConfigParam.IsBold.HasValue)
				{
					pointSignTDO.IsBold = displayConfigParam.IsBold.Value;
				}
				if (displayConfigParam.IsUnderlined.HasValue)
				{
					pointSignTDO.IsUnderlined = displayConfigParam.IsUnderlined.Value;
				}
				if (displayConfigParam.IsItalic.HasValue)
				{
					pointSignTDO.IsItalic = displayConfigParam.IsItalic.Value;
				}
				if (!string.IsNullOrEmpty(displayConfigParam.FontName))
				{
					pointSignTDO.FontName = displayConfigParam.FontName;
				}
			}
			return pointSignTDO;
		}
	}
}
