using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMR.EFMODEL.DataModels;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;
using Inventec.Common.SignLibrary.Popup;
using Inventec.Common.SignToolViewer.Integrate;

namespace Inventec.Common.SignLibrary
{
	public class DocumentManager
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public DocumentSignedDTO documentSignedDTO;

			public bool isSigned;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public string mergeCode;

			public decimal oginalHeight;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass22_0
		{
			public V_EMR_DOCUMENT document;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public DocumentSignedDTO documentSignedDTO;

			public bool isSigned;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9_0
		{
			public DocumentSignedDTO documentSignedDTO;

			public bool isSigned;
		}

		private SignToken signToken;

		public DocumentManager()
		{
			signToken = new SignToken();
		}

		public DocumentManager(string dti)
		{
			signToken = new SignToken();
			InitParam(dti);
		}

		private void InitParam(string dti)
		{
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => dti)), (object)dti));
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
			EMR_SIGNER val = null;
			val = ((signToken == null || signToken.Singer == null || signToken.Singer.ID <= 0) ? GlobalStore.Singer : signToken.Singer);
			if (val != null && GlobalStore.EMR_HSM_INTEGRATE_OPTION == "5" && string.IsNullOrWhiteSpace(val.HSM_USER_CODE))
			{
				frmUpdateSigner frmUpdateSigner = new frmUpdateSigner(val, 0);
				frmUpdateSigner.ShowDialog();
			}
			return val;
		}

		private EMR_TREATMENT GetTreatmentData()
		{
			if (signToken != null && signToken.Treatment != null && signToken.Treatment.ID > 0)
			{
				return signToken.Treatment;
			}
			return null;
		}

		public bool IsDocumentSigned(DocumentSignedDTO documentSignedDTO)
		{
			string base64FileGigned = "";
			return IsDocumentSigned(documentSignedDTO, ref base64FileGigned);
		}

		public bool IsDocumentSigned(DocumentSignedDTO documentSignedDTO, ref string base64FileGigned)
		{
			_003C_003Ec__DisplayClass8_0 CS_0024_003C_003E8__locals29 = new _003C_003Ec__DisplayClass8_0();
			CS_0024_003C_003E8__locals29.documentSignedDTO = documentSignedDTO;
			CS_0024_003C_003E8__locals29.isSigned = false;
			try
			{
				if (CS_0024_003C_003E8__locals29.documentSignedDTO == null)
				{
					throw new ArgumentNullException("documentSignedDTO");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode))
				{
					throw new ArgumentNullException("DocumentTypeCode");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.TreatmentCode) && string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.HisCode))
				{
					throw new ArgumentNullException("TreatmentCode && HisCode");
				}
				try
				{
					CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode = string.Format("{0:00}", CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
				}
				catch (Exception ex)
				{
					CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode = ((CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode.Length == 1) ? ("0" + CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode) : CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
					LogSystem.Warn(ex);
				}
				InitUri();
				InputADO inputADO = new InputADO();
				inputADO.Treatment = new TreatmentDTO();
				inputADO.Treatment.TREATMENT_CODE = CS_0024_003C_003E8__locals29.documentSignedDTO.TreatmentCode;
				if (CheckLogin())
				{
					Verify.VerifyTreatmentCode(inputADO, ref signToken);
					EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
					if (byCode != null)
					{
						base64FileGigned = "";
						inputADO.DocumentTypeCode = CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode;
						inputADO.HisCode = CS_0024_003C_003E8__locals29.documentSignedDTO.HisCode;
						V_EMR_DOCUMENT documentData = null;
						Verify.VerifyHisCode(inputADO, false, ref base64FileGigned, ref documentData);
						if (!string.IsNullOrEmpty(base64FileGigned))
						{
							CS_0024_003C_003E8__locals29.isSigned = true;
						}
						else
						{
							MessageManager.Show(MessageUitl.GetMessage("KhongTimThayDuLieuDaKyCuaHoSo"));
							LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName<InputADO>((Expression<Func<InputADO>>)(() => inputADO)), (object)inputADO) + "____" + LogUtil.TraceData("base64FileGigned", (object)base64FileGigned));
							CS_0024_003C_003E8__locals29.isSigned = false;
						}
					}
					else
					{
						MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					}
					LogSystem.Debug("IsDocumentSigned. Kiem tra van ban da ky chua____Input" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO) + "____output____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => CS_0024_003C_003E8__locals29.isSigned)), (object)CS_0024_003C_003E8__locals29.isSigned));
				}
				else
				{
					LogSystem.Debug("IsDocumentSigned - Kiem tra trang thai van ban da ky chua. Kiem tra thong tin dang nhap - token phien lam viec that bai. ____Input" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO) + "____output____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>(Expression.Lambda<Func<bool>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals29, typeof(_003C_003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals29.isSigned));
				}
			}
			catch (Exception ex2)
			{
				MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
				LogSystem.Debug("Loai van ban truyen vao khong hop le____" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO), ex2);
			}
			return CS_0024_003C_003E8__locals29.isSigned;
		}

		public bool IsDocumentSigned(DocumentSignedDTO documentSignedDTO, ref string base64FileGigned, ref string documentCode)
		{
			_003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals29 = new _003C_003Ec__DisplayClass9_0();
			CS_0024_003C_003E8__locals29.documentSignedDTO = documentSignedDTO;
			CS_0024_003C_003E8__locals29.isSigned = false;
			try
			{
				if (CS_0024_003C_003E8__locals29.documentSignedDTO == null)
				{
					throw new ArgumentNullException("documentSignedDTO");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode))
				{
					throw new ArgumentNullException("DocumentTypeCode");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.TreatmentCode) && string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.HisCode))
				{
					throw new ArgumentNullException("TreatmentCode && HisCode");
				}
				try
				{
					CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode = string.Format("{0:00}", CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
				}
				catch (Exception ex)
				{
					CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode = ((CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode.Length == 1) ? ("0" + CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode) : CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
					LogSystem.Warn(ex);
				}
				InitUri();
				InputADO inputADO = new InputADO();
				inputADO.Treatment = new TreatmentDTO();
				inputADO.Treatment.TREATMENT_CODE = CS_0024_003C_003E8__locals29.documentSignedDTO.TreatmentCode;
				if (CheckLogin())
				{
					Verify.VerifyTreatmentCode(inputADO, ref signToken);
					EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
					if (byCode != null)
					{
						base64FileGigned = "";
						inputADO.DocumentTypeCode = CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode;
						inputADO.HisCode = CS_0024_003C_003E8__locals29.documentSignedDTO.HisCode;
						V_EMR_DOCUMENT documentData = null;
						Verify.VerifyHisCode(inputADO, false, ref base64FileGigned, ref documentData);
						if (!string.IsNullOrEmpty(base64FileGigned))
						{
							CS_0024_003C_003E8__locals29.isSigned = true;
							documentCode = ((documentData != null) ? documentData.DOCUMENT_CODE : "");
						}
						else
						{
							MessageManager.Show(MessageUitl.GetMessage("KhongTimThayDuLieuDaKyCuaHoSo"));
							CS_0024_003C_003E8__locals29.isSigned = false;
						}
					}
					else
					{
						MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					}
					LogSystem.Debug("IsDocumentSigned. Kiem tra van ban da ky chua____Input" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO) + "____output____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => CS_0024_003C_003E8__locals29.isSigned)), (object)CS_0024_003C_003E8__locals29.isSigned));
				}
				else
				{
					LogSystem.Debug("IsDocumentSigned - Kiem tra trang thai van ban da ky chua. Kiem tra thong tin dang nhap - token phien lam viec that bai. ____Input" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO) + "____output____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>(Expression.Lambda<Func<bool>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals29, typeof(_003C_003Ec__DisplayClass9_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals29.isSigned));
				}
			}
			catch (Exception ex2)
			{
				MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
				LogSystem.Debug("Loai van ban truyen vao khong hop le____" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO), ex2);
			}
			return CS_0024_003C_003E8__locals29.isSigned;
		}

		public bool IsDocumentSigned(DocumentSignedDTO documentSignedDTO, ref string base64FileGigned, ref string documentCode, ref InputADO inputADO)
		{
			_003C_003Ec__DisplayClass10_0 CS_0024_003C_003E8__locals29 = new _003C_003Ec__DisplayClass10_0();
			CS_0024_003C_003E8__locals29.documentSignedDTO = documentSignedDTO;
			CS_0024_003C_003E8__locals29.isSigned = false;
			try
			{
				if (CS_0024_003C_003E8__locals29.documentSignedDTO == null)
				{
					throw new ArgumentNullException("documentSignedDTO");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode))
				{
					throw new ArgumentNullException("DocumentTypeCode");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.TreatmentCode) && string.IsNullOrEmpty(CS_0024_003C_003E8__locals29.documentSignedDTO.HisCode))
				{
					throw new ArgumentNullException("TreatmentCode && HisCode");
				}
				try
				{
					CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode = string.Format("{0:00}", CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
				}
				catch (Exception ex)
				{
					CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode = ((CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode.Length == 1) ? ("0" + CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode) : CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
					LogSystem.Warn(ex);
				}
				InitUri();
				inputADO.Treatment = new TreatmentDTO();
				inputADO.Treatment.TREATMENT_CODE = CS_0024_003C_003E8__locals29.documentSignedDTO.TreatmentCode;
				if (CheckLogin())
				{
					Verify.VerifyTreatmentCode(inputADO, ref signToken);
					EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode);
					if (byCode != null)
					{
						base64FileGigned = "";
						inputADO.DocumentTypeCode = CS_0024_003C_003E8__locals29.documentSignedDTO.DocumentTypeCode;
						inputADO.HisCode = CS_0024_003C_003E8__locals29.documentSignedDTO.HisCode;
						V_EMR_DOCUMENT documentData = null;
						Verify.VerifyHisCode(inputADO, false, ref base64FileGigned, ref documentData);
						if (!string.IsNullOrEmpty(base64FileGigned))
						{
							CS_0024_003C_003E8__locals29.isSigned = true;
							documentCode = ((documentData != null) ? documentData.DOCUMENT_CODE : "");
							if (documentData.WIDTH.HasValue && documentData.HEIGHT.HasValue && documentData.RAW_KIND.HasValue)
							{
								inputADO.PaperSizeDefault = new PaperSize(documentData.PAPER_NAME, (int)documentData.WIDTH.Value, (int)documentData.HEIGHT.Value);
								if (documentData.RAW_KIND.HasValue)
								{
									inputADO.PaperSizeDefault.RawKind = documentData.RAW_KIND.Value;
								}
							}
						}
						else
						{
							MessageManager.Show(MessageUitl.GetMessage("KhongTimThayDuLieuDaKyCuaHoSo"));
							CS_0024_003C_003E8__locals29.isSigned = false;
						}
					}
					else
					{
						MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					}
					LogSystem.Debug("IsDocumentSigned. Kiem tra van ban da ky chua____Input" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO) + "____output____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => CS_0024_003C_003E8__locals29.isSigned)), (object)CS_0024_003C_003E8__locals29.isSigned));
				}
				else
				{
					LogSystem.Debug("IsDocumentSigned - Kiem tra trang thai van ban da ky chua. Kiem tra thong tin dang nhap - token phien lam viec that bai. ____Input" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO) + "____output____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>(Expression.Lambda<Func<bool>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals29, typeof(_003C_003Ec__DisplayClass10_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals29.isSigned));
				}
			}
			catch (Exception ex2)
			{
				MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
				LogSystem.Debug("Loai van ban truyen vao khong hop le____" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => CS_0024_003C_003E8__locals29.documentSignedDTO)), (object)CS_0024_003C_003E8__locals29.documentSignedDTO), ex2);
			}
			return CS_0024_003C_003E8__locals29.isSigned;
		}

		internal bool CheckLogin()
		{
			if (!string.IsNullOrEmpty(GlobalStore.TokenCode))
			{
				GlobalStore.TokenData = new TokenData();
				GlobalStore.TokenData.TokenCode = GlobalStore.TokenCode;
				GlobalStore.TokenData.User = new UserData();
				GlobalStore.TokenData.User.LoginName = GlobalStore.LoginName;
				GlobalStore.TokenData.User.UserName = GlobalStore.UserName;
				GlobalStore.TokenData.User.ApplicationCode = "HIS";
				GlobalStore.AcsConsumer.SetTokenCode(GlobalStore.TokenData.TokenCode);
				GlobalStore.EmrConsumer.SetTokenCode(GlobalStore.TokenData.TokenCode);
				signToken.TokenData = GlobalStore.TokenData;
				signToken.UserName = GlobalStore.UserName;
				signToken.LoginName = GlobalStore.LoginName;
				EMR_SIGNER singer = (GlobalStore.Singer = GlobalStore.GetByLoginName(GlobalStore.LoginName));
				signToken.Singer = singer;
			}
			else if (GlobalStore.TokenData == null)
			{
				frmLogin frmLogin = new frmLogin(ProcessSignTokenData);
				frmLogin.ShowDialog();
			}
			if (signToken != null && (signToken.TokenData != null || !string.IsNullOrEmpty(signToken.TokenCode)))
			{
				return true;
			}
			return false;
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

		public void ShowDocumentSigned(string documentTypeCode, string hisCode)
		{
			ShowDocumentSigned(new DocumentSignedDTO
			{
				DocumentTypeCode = documentTypeCode,
				HisCode = hisCode,
				IsPrintOnlyContent = true
			});
		}

		public void ShowDocumentSigned(DocumentSignedDTO documentSignedDTO)
		{
			try
			{
				string base64FileGigned = "";
				string documentCode = "";
				InputADO inputADO = new InputADO();
				if (!IsDocumentSigned(documentSignedDTO, ref base64FileGigned, ref documentCode, ref inputADO))
				{
					return;
				}
				inputADO.Treatment = new TreatmentDTO();
				inputADO.DocumentTypeCode = documentSignedDTO.DocumentTypeCode;
				inputADO.HisCode = documentSignedDTO.HisCode;
				inputADO.Treatment.TREATMENT_CODE = documentSignedDTO.TreatmentCode;
				inputADO.DocumentCode = documentCode;
				inputADO.IsPrint = true;
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				if (emrConfigs != null && emrConfigs.Count > 0)
				{
					IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.IS_NOT_SHOWING_SIGN_INFORMATION");
					EMR_CONFIG val = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
					if (val != null)
					{
						string text = ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : val.DEFAULT_VALUE);
						if (!string.IsNullOrEmpty(text))
						{
							inputADO.IsPrintOnlyContent = text == "1";
						}
					}
				}
				else
				{
					inputADO.IsPrintOnlyContent = documentSignedDTO.IsPrintOnlyContent ?? true;
				}
				byte[] inputByte = Convert.FromBase64String(base64FileGigned);
				frmPdfViewer frmPdfViewer2 = new frmPdfViewer(inputByte, FileType.Pdf, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
				frmPdfViewer2.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
		}

		public UserControl GetUcDocumentSigned(string documentTypeCode, string hisCode)
		{
			return GetUcDocumentSigned(new DocumentSignedDTO
			{
				DocumentTypeCode = documentTypeCode,
				HisCode = hisCode,
				IsPrintOnlyContent = true
			});
		}

		public UserControl GetUcDocumentSigned(DocumentSignedDTO documentSignedDTO)
		{
			try
			{
				string base64FileGigned = "";
				if (IsDocumentSigned(documentSignedDTO, ref base64FileGigned))
				{
					InputADO inputADO = new InputADO();
					inputADO.Treatment = new TreatmentDTO();
					inputADO.Treatment.TREATMENT_CODE = documentSignedDTO.TreatmentCode;
					inputADO.DocumentTypeCode = documentSignedDTO.DocumentTypeCode;
					inputADO.HisCode = documentSignedDTO.HisCode;
					inputADO.IsPrint = true;
					List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
					if (emrConfigs != null && emrConfigs.Count > 0)
					{
						IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.IS_NOT_SHOWING_SIGN_INFORMATION");
						EMR_CONFIG val = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
						if (val != null)
						{
							string text = ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : val.DEFAULT_VALUE);
							if (!string.IsNullOrEmpty(text))
							{
								inputADO.IsPrintOnlyContent = text == "1";
							}
						}
					}
					else
					{
						inputADO.IsPrintOnlyContent = documentSignedDTO.IsPrintOnlyContent ?? true;
					}
					byte[] inputByte = Convert.FromBase64String(base64FileGigned);
					return new UCViewer(inputByte, FileType.Pdf, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Xem lai file da ky that bai____" + LogUtil.TraceData(LogUtil.GetMemberName<DocumentSignedDTO>((Expression<Func<DocumentSignedDTO>>)(() => documentSignedDTO)), (object)documentSignedDTO), ex);
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
			}
			return null;
		}

		public void GetFileDocumentMergeWithUri(V_EMR_DOCUMENT document, List<long> documentIds, ref string outPdfFile)
		{
			try
			{
				if (document == null)
				{
					throw new ArgumentNullException("document");
				}
				if (string.IsNullOrEmpty(document.MERGE_CODE))
				{
					throw new ArgumentNullException("mergeCode");
				}
				if (string.IsNullOrEmpty(document.TREATMENT_CODE))
				{
					throw new ArgumentNullException("treatmentCode");
				}
				InitUri();
				if (CheckLogin())
				{
					outPdfFile = GetFileDocumentMerge(document.ORIGINAL_HIGH.GetValueOrDefault(), document.TREATMENT_CODE, document.MERGE_CODE, document.DOCUMENT_NAME, "", documentIds);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void GetFileDocumentMergeWithUri(V_EMR_DOCUMENT document, List<long> documentIds, ref byte[] outPdfByte)
		{
			try
			{
				if (document == null)
				{
					throw new ArgumentNullException("document");
				}
				if (string.IsNullOrEmpty(document.MERGE_CODE))
				{
					throw new ArgumentNullException("mergeCode");
				}
				if (string.IsNullOrEmpty(document.TREATMENT_CODE))
				{
					throw new ArgumentNullException("treatmentCode");
				}
				InitUri();
				if (!CheckLogin())
				{
					return;
				}
				byte[] documentByMergeCode = new EmrDocument().GetDocumentByMergeCode(document.MERGE_CODE, document.DOCUMENT_NAME, documentIds);
				if (documentByMergeCode != null && documentByMergeCode.Length != 0)
				{
					outPdfByte = documentByMergeCode;
					return;
				}
				LogSystem.Warn("Khong tim thay van ban nao theo mergecode truyen vao____" + LogUtil.TraceData(LogUtil.GetMemberName<V_EMR_DOCUMENT>((Expression<Func<V_EMR_DOCUMENT>>)(() => document)), (object)document));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public string GetFileDocumentMerge(decimal oginalHeight, string treatmentCode, string mergeCode, string documentName = "", string documentCode = "", List<long> documentIds = null)
		{
			string text = "";
			try
			{
				byte[] documentByMergeCode = new EmrDocument().GetDocumentByMergeCode(mergeCode, documentName, documentIds);
				if (documentByMergeCode != null && documentByMergeCode.Length != 0)
				{
					text = Utils.GenerateTempFileWithin();
					Utils.ByteToFile(documentByMergeCode, text);
				}
				else
				{
					LogSystem.Warn("Khong tim thay van ban nao theo mergecode truyen vao____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => mergeCode)), (object)mergeCode));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return text;
		}

		public UserControl GetUcDocumentMerge(decimal oginalHeight, string treatmentCode, string mergeCode, ref string outPdfFile)
		{
			_003C_003Ec__DisplayClass21_0 CS_0024_003C_003E8__locals14 = new _003C_003Ec__DisplayClass21_0();
			CS_0024_003C_003E8__locals14.mergeCode = mergeCode;
			CS_0024_003C_003E8__locals14.oginalHeight = oginalHeight;
			try
			{
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals14.mergeCode))
				{
					throw new ArgumentNullException("mergeCode");
				}
				if (string.IsNullOrEmpty(treatmentCode))
				{
					throw new ArgumentNullException("treatmentCode");
				}
				if (CS_0024_003C_003E8__locals14.oginalHeight < 0m)
				{
					throw new ArgumentNullException("oginalHeight");
				}
				InitUri();
				if (CheckLogin())
				{
					outPdfFile = GetFileDocumentMerge(CS_0024_003C_003E8__locals14.oginalHeight, treatmentCode, CS_0024_003C_003E8__locals14.mergeCode);
					if (!string.IsNullOrEmpty(outPdfFile) && File.Exists(outPdfFile))
					{
						InputADO inputADO = new InputADO();
						inputADO.Treatment = new TreatmentDTO();
						inputADO.Treatment.TREATMENT_CODE = treatmentCode;
						inputADO.DocumentTypeCode = "";
						inputADO.HisCode = "";
						inputADO.IsPrint = true;
						inputADO.IsPrintOnlyContent = true;
						inputADO.IsSignConfig = false;
						return new UCViewer(outPdfFile, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
					}
				}
				else
				{
					LogSystem.Debug("GetUcDocumentMerge - Xem van ban gop theo mergeCode that bai. Kiem tra thong tin dang nhap - token phien lam viec that bai. ____Input" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => CS_0024_003C_003E8__locals14.mergeCode)), (object)CS_0024_003C_003E8__locals14.mergeCode) + LogUtil.TraceData(LogUtil.GetMemberName<decimal>(Expression.Lambda<Func<decimal>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals14, typeof(_003C_003Ec__DisplayClass21_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals14.oginalHeight));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Xem van ban gop theo mergeCode that bai____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => CS_0024_003C_003E8__locals14.mergeCode)), (object)CS_0024_003C_003E8__locals14.mergeCode) + LogUtil.TraceData(LogUtil.GetMemberName<decimal>(Expression.Lambda<Func<decimal>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals14, typeof(_003C_003Ec__DisplayClass21_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals14.oginalHeight), ex);
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
			}
			return null;
		}

		public UserControl GetUcDocumentMerge(V_EMR_DOCUMENT document, ref string outPdfFile, bool IsMergeByName = false)
		{
			_003C_003Ec__DisplayClass22_0 CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass22_0();
			CS_0024_003C_003E8__locals23.document = document;
			try
			{
				if (CS_0024_003C_003E8__locals23.document == null)
				{
					throw new ArgumentNullException("document");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals23.document.MERGE_CODE))
				{
					throw new ArgumentNullException("mergeCode");
				}
				if (string.IsNullOrEmpty(CS_0024_003C_003E8__locals23.document.TREATMENT_CODE))
				{
					throw new ArgumentNullException("treatmentCode");
				}
				InitUri();
				if (CheckLogin())
				{
					outPdfFile = GetFileDocumentMerge(CS_0024_003C_003E8__locals23.document.ORIGINAL_HIGH.GetValueOrDefault(), CS_0024_003C_003E8__locals23.document.TREATMENT_CODE, CS_0024_003C_003E8__locals23.document.MERGE_CODE, IsMergeByName ? CS_0024_003C_003E8__locals23.document.DOCUMENT_NAME : "");
					if (!string.IsNullOrEmpty(outPdfFile) && File.Exists(outPdfFile))
					{
						InputADO inputADO = new InputADO();
						inputADO.Treatment = new TreatmentDTO();
						inputADO.Treatment.TREATMENT_CODE = CS_0024_003C_003E8__locals23.document.TREATMENT_CODE;
						inputADO.DocumentTypeCode = "";
						inputADO.HisCode = "";
						inputADO.IsPrint = true;
						inputADO.IsPrintOnlyContent = true;
						inputADO.IsSignConfig = false;
						if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals23.document.PAPER_NAME) && CS_0024_003C_003E8__locals23.document.RAW_KIND.HasValue && CS_0024_003C_003E8__locals23.document.WIDTH.HasValue && CS_0024_003C_003E8__locals23.document.HEIGHT.HasValue)
						{
							inputADO.PaperSizeDefault = new PaperSize(CS_0024_003C_003E8__locals23.document.PAPER_NAME, (int)CS_0024_003C_003E8__locals23.document.WIDTH.Value, (int)CS_0024_003C_003E8__locals23.document.HEIGHT.Value);
							inputADO.PaperSizeDefault.RawKind = CS_0024_003C_003E8__locals23.document.RAW_KIND.Value;
						}
						return new UCViewer(outPdfFile, inputADO, GetSignerData(), GetTreatmentData(), GetTokenCodeData());
					}
				}
				else
				{
					LogSystem.Debug("GetUcDocumentMerge - Xem van ban gop theo mergeCode that bai. Kiem tra thong tin dang nhap - token phien lam viec that bai. ____Input" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => CS_0024_003C_003E8__locals23.document.MERGE_CODE)), (object)CS_0024_003C_003E8__locals23.document.MERGE_CODE) + LogUtil.TraceData(LogUtil.GetMemberName<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass22_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.document.TREATMENT_CODE));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Xem van ban gop theo mergeCode that bai____" + LogUtil.TraceData(LogUtil.GetMemberName<V_EMR_DOCUMENT>((Expression<Func<V_EMR_DOCUMENT>>)(() => CS_0024_003C_003E8__locals23.document)), (object)CS_0024_003C_003E8__locals23.document), ex);
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
			}
			return null;
		}

		private void InitUri()
		{
			if (string.IsNullOrEmpty(ConstanIG.ACS_BASE_URI) && !string.IsNullOrEmpty((string)RegistryProcessor.Read("ACS_BASE_URI")))
			{
				ConstanIG.ACS_BASE_URI = (string)RegistryProcessor.Read("ACS_BASE_URI");
			}
			if (string.IsNullOrEmpty(GlobalStore.EMR_BASE_URI) && !string.IsNullOrEmpty((string)RegistryProcessor.Read("EMR_BASE_URI")))
			{
				GlobalStore.EMR_BASE_URI = (string)RegistryProcessor.Read("EMR_BASE_URI");
			}
			if (string.IsNullOrEmpty(FssConstant.BASE_URI) && !string.IsNullOrEmpty((string)RegistryProcessor.Read("FSS_BASE_URI")))
			{
				FssConstant.BASE_URI = (string)RegistryProcessor.Read("FSS_BASE_URI");
			}
			if (string.IsNullOrEmpty(GlobalStore.HPS_BASE_URI) && !string.IsNullOrEmpty((string)RegistryProcessor.Read("HPS_BASE_URI")))
			{
				GlobalStore.HPS_BASE_URI = (string)RegistryProcessor.Read("HPS_BASE_URI");
			}
			if (!string.IsNullOrEmpty(ConstanIG.ACS_BASE_URI) && !string.IsNullOrEmpty(GlobalStore.EMR_BASE_URI) && !string.IsNullOrEmpty(FssConstant.BASE_URI))
			{
				GlobalStore.IsUseSendDTI = true;
			}
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => ConstanIG.ACS_BASE_URI)), (object)ConstanIG.ACS_BASE_URI) + "__" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => GlobalStore.EMR_BASE_URI)), (object)GlobalStore.EMR_BASE_URI) + "__" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => FssConstant.BASE_URI)), (object)FssConstant.BASE_URI));
		}
	}
}
