using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	internal class Verify
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_3
		{
			public EMR_VERSION lastVersionSigned;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass4_1
		{
			public EMR_VERSION sign;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass8
		{
			public long documentSignId;

			public bool isAllowDuplicateHisCode;

			public InputADO inputADO;

			public bool isShowSignedFile;

			public bool isValidExistsDoc;

			public bool _003CVerifyHisCode_003Eb__2(V_EMR_DOCUMENT o)
			{
				return o.HIS_CODE == inputADO.HisCode;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClasse
		{
			public _003C_003Ec__DisplayClass8 CS_0024_003C_003E8__locals9;

			public _003C_003Ec__DisplayClass3_3 _003C_003Ec__DisplayClass3_;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass14
		{
			public long documentSignId;

			public InputADO inputADO;

			public bool isShowSignedFile;

			public bool _003CVerifyHisCode_003Eb__12(V_EMR_DOCUMENT o)
			{
				return o.HIS_CODE == inputADO.HisCode;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass16
		{
			public _003C_003Ec__DisplayClass14 CS_0024_003C_003E8__locals15;

			public _003C_003Ec__DisplayClass4_1 _003C_003Ec__DisplayClass4_;
		}

		private const string SplitTagPatientCode = "#@!@#";

		private const int IS_HAS_ONE = 1;

		internal static bool VerifyHisCode(InputADO inputADO, bool isShowSignedFile, ref string base64FileSigned, ref V_EMR_DOCUMENT documentData)
		{
			return VerifyHisCode(inputADO, isShowSignedFile, false, ref base64FileSigned, ref documentData);
		}

		internal static bool VerifyHisCode(InputADO inputADO, bool isShowSignedFile, bool isValidExistsDoc, ref string base64FileSigned, ref V_EMR_DOCUMENT documentData)
		{
			_003C_003Ec__DisplayClass8 CS_0024_003C_003E8__locals47 = new _003C_003Ec__DisplayClass8();
			CS_0024_003C_003E8__locals47.inputADO = inputADO;
			CS_0024_003C_003E8__locals47.isShowSignedFile = isShowSignedFile;
			CS_0024_003C_003E8__locals47.isValidExistsDoc = isValidExistsDoc;
			bool flag = false;
			CS_0024_003C_003E8__locals47.documentSignId = 0L;
			string text = "";
			CS_0024_003C_003E8__locals47.isAllowDuplicateHisCode = false;
			if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals47.inputADO.DocumentTypeCode))
			{
				EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(CS_0024_003C_003E8__locals47.inputADO.DocumentTypeCode);
				CS_0024_003C_003E8__locals47.isAllowDuplicateHisCode = byCode != null && byCode.IS_ALLOW_DUPLICATE_HIS_CODE == 1;
				if (byCode != null)
				{
					short? iS_HAS_ONE = byCode.IS_HAS_ONE;
					if (iS_HAS_ONE == 1 && iS_HAS_ONE.HasValue && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals47.inputADO.Treatment.TREATMENT_CODE))
					{
						List<V_EMR_DOCUMENT> view = new EmrDocument().GetView(new EmrDocumentViewFilter
						{
							TREATMENT_CODE__EXACT = CS_0024_003C_003E8__locals47.inputADO.Treatment.TREATMENT_CODE,
							DOCUMENT_TYPE_CODE__EXACT = CS_0024_003C_003E8__locals47.inputADO.DocumentTypeCode,
							IS_DELETE = false
						}, new CommonParam());
						flag = view != null && view.Count > 0;
						if (flag)
						{
							CS_0024_003C_003E8__locals47.documentSignId = view[0].ID;
							text = view[0].MERGE_CODE;
							documentData = view[0];
						}
					}
				}
			}
			if (CS_0024_003C_003E8__locals47.documentSignId == 0 && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals47.inputADO.HisCode))
			{
				_003C_003Ec__DisplayClass8 _003C_003Ec__DisplayClass = CS_0024_003C_003E8__locals47;
				List<V_EMR_DOCUMENT> docWithHisCodes = new EmrDocument().GetView(new EmrDocumentViewFilter
				{
					TREATMENT_CODE__EXACT = CS_0024_003C_003E8__locals47.inputADO.Treatment.TREATMENT_CODE,
					DOCUMENT_TYPE_CODE__EXACT = CS_0024_003C_003E8__locals47.inputADO.DocumentTypeCode,
					ORDER_FIELD = "ID",
					ORDER_DIRECTION = "DESC",
					IS_DELETE = false
				}, new CommonParam());
				docWithHisCodes = ((docWithHisCodes != null) ? docWithHisCodes.Where((V_EMR_DOCUMENT o) => o.HIS_CODE == CS_0024_003C_003E8__locals47.inputADO.HisCode).ToList() : null);
				flag = docWithHisCodes != null && docWithHisCodes.Count > 0;
				if (flag)
				{
					CS_0024_003C_003E8__locals47.documentSignId = docWithHisCodes[0].ID;
					text = docWithHisCodes[0].MERGE_CODE;
					documentData = docWithHisCodes[0];
				}
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => docWithHisCodes), docWithHisCodes));
			}
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals47.documentSignId), CS_0024_003C_003E8__locals47.documentSignId));
			if (flag)
			{
				if (CS_0024_003C_003E8__locals47.isAllowDuplicateHisCode)
				{
					CS_0024_003C_003E8__locals47.isShowSignedFile = true;
				}
				else
				{
					List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
					if (emrConfigs != null && emrConfigs.Count > 0)
					{
						IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_DOCUMENT.DULICATE_HIS_CODE.WARNING_OPTION");
						EMR_CONFIG eMR_CONFIG = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
						if (eMR_CONFIG != null)
						{
							string text2 = ((!string.IsNullOrEmpty(eMR_CONFIG.VALUE)) ? eMR_CONFIG.VALUE : eMR_CONFIG.DEFAULT_VALUE);
							if (text2 == "1" && CS_0024_003C_003E8__locals47.documentSignId > 0)
							{
								CS_0024_003C_003E8__locals47.isShowSignedFile = false;
								if (CS_0024_003C_003E8__locals47.isValidExistsDoc)
								{
									LogSystem.Info(MessageUitl.GetMessage("VanBanDaTonTaiKhongTheKyTiepDoThietLapDangChan") + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals47.isValidExistsDoc), CS_0024_003C_003E8__locals47.isValidExistsDoc));
									XtraMessageBox.Show(MessageUitl.GetMessage("VanBanDaTonTaiKhongTheKyTiepDoThietLapDangChan"), MessageUitl.GetMessage("ThongBao"));
									documentData = null;
									return false;
								}
								LogSystem.Info(MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu") + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals47.isShowSignedFile), CS_0024_003C_003E8__locals47.isShowSignedFile));
								XtraMessageBox.Show(MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu"), MessageUitl.GetMessage("ThongBao"));
							}
							else if (text2 == "2" && CS_0024_003C_003E8__locals47.documentSignId > 0)
							{
								_003C_003Ec__DisplayClass8 _003C_003Ec__DisplayClass2 = CS_0024_003C_003E8__locals47;
								EmrSign emrSign = new EmrSign();
								EmrSignFilter emrSignFilter = new EmrSignFilter();
								emrSignFilter.DOCUMENT_ID = CS_0024_003C_003E8__locals47.documentSignId;
								List<EMR_SIGN> signExists = emrSign.Get(emrSignFilter);
								if (signExists != null && signExists.Count > 0 && signExists.Exists((EMR_SIGN o) => !o.REJECT_TIME.HasValue))
								{
									CS_0024_003C_003E8__locals47.isShowSignedFile = false;
									LogSystem.Info("TH có key cau hinh EMR.EMR_DOCUMENT.DULICATE_HIS_CODE.WARNING_OPTION gia tri = " + text2 + " " + MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu") + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals47.isShowSignedFile), CS_0024_003C_003E8__locals47.isShowSignedFile) + LogUtil.TraceData(LogUtil.GetMemberName(() => signExists), signExists) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals47.isAllowDuplicateHisCode), CS_0024_003C_003E8__locals47.isAllowDuplicateHisCode));
									XtraMessageBox.Show(MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu"), MessageUitl.GetMessage("ThongBao"));
								}
								else
								{
									LogSystem.Info("TH có key cau hinh EMR.EMR_DOCUMENT.DULICATE_HIS_CODE.WARNING_OPTION gia tri = " + text2 + " , nhung khong tim thay emr_sign nao thoa man: \"Văn bản đã ký hoặc đã thiết lập ký\"____" + LogUtil.TraceData(LogUtil.GetMemberName(() => signExists), signExists) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals47.isAllowDuplicateHisCode), CS_0024_003C_003E8__locals47.isAllowDuplicateHisCode));
								}
							}
						}
					}
				}
				if (CS_0024_003C_003E8__locals47.isShowSignedFile)
				{
					if (XtraMessageBox.Show(MessageUitl.GetMessage("VanBanCoTheDaTonTaiTrenHeThongEMRBanCoThucHienKhong"), MessageUitl.GetMessage("ThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						LogSystem.Info("Ton tai van ban da co tren he thong cua ho so & loai van ban ==> show thong bao co muon thuc hien ==> chon khong muon thuc hien ==> he thong khong tim duoc file da ky truoc do de view ");
						return false;
					}
					LogSystem.Info("Ton tai van ban da co tren he thong cua ho so & loai van ban ==> show thong bao co muon thuc hien ==> chon co muon thuc hien tiep ==> mo xem ky nhu binh thuong");
					documentData = null;
					return true;
				}
				string fileUrl = "";
				if (documentData != null && !string.IsNullOrEmpty(documentData.LAST_VERSION_URL))
				{
					fileUrl = documentData.LAST_VERSION_URL;
				}
				else
				{
					_003C_003Ec__DisplayClasse _003C_003Ec__DisplayClasse = new _003C_003Ec__DisplayClasse();
					_003C_003Ec__DisplayClasse.CS_0024_003C_003E8__locals9 = CS_0024_003C_003E8__locals47;
					_003C_003Ec__DisplayClasse._003C_003Ec__DisplayClass3_ = new _003C_003Ec__DisplayClass3_3();
					_003C_003Ec__DisplayClasse._003C_003Ec__DisplayClass3_.lastVersionSigned = ((CS_0024_003C_003E8__locals47.documentSignId > 0) ? new EmrVersion().GetSignedDocumentLast(CS_0024_003C_003E8__locals47.documentSignId) : null);
					if (_003C_003Ec__DisplayClasse._003C_003Ec__DisplayClass3_.lastVersionSigned != null)
					{
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals47.documentSignId), CS_0024_003C_003E8__locals47.documentSignId) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<_003C_003Ec__DisplayClass3_3>>(Expression.Field(Expression.Constant(_003C_003Ec__DisplayClasse), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), _003C_003Ec__DisplayClasse._003C_003Ec__DisplayClass3_.lastVersionSigned.URL));
						fileUrl = _003C_003Ec__DisplayClasse._003C_003Ec__DisplayClass3_.lastVersionSigned.URL;
					}
				}
				MemoryStream file = FssFileDownload.GetFile(fileUrl);
				if (file != null && file.Length > 0)
				{
					file.Position = 0L;
					string text3 = "";
					if (!string.IsNullOrEmpty(text))
					{
						string fileDocumentMerge = new DocumentManager().GetFileDocumentMerge(0m, CS_0024_003C_003E8__locals47.inputADO.Treatment.TREATMENT_CODE, text);
						if (!string.IsNullOrEmpty(fileDocumentMerge))
						{
							text3 = Convert.ToBase64String(Utils.FileToByte(fileDocumentMerge));
						}
					}
					if (string.IsNullOrEmpty(text3))
					{
						text3 = Convert.ToBase64String(Utils.StreamToByte(file));
					}
					if (!string.IsNullOrEmpty(text3))
					{
						base64FileSigned = text3;
					}
					else
					{
						documentData = null;
					}
					LogSystem.Info("Ton tai van ban da co tren he thong cua ho so & loai van ban ==> show thong bao co muon thuc hien ==> chon khong muon thuc hien ==> he thong tim duoc file da ky truoc do cua ho so de show len voi che do chi xem");
					return true;
				}
			}
			else
			{
				LogSystem.Debug("Khong Ton tai van ban da co tren he thong cua ho so & loai van ban");
			}
			return true;
		}

		internal static bool VerifyHisCode(InputADO inputADO, bool isShowSignedFile, ref byte[] inputByte)
		{
			_003C_003Ec__DisplayClass14 CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass14();
			CS_0024_003C_003E8__locals23.inputADO = inputADO;
			CS_0024_003C_003E8__locals23.isShowSignedFile = isShowSignedFile;
			bool flag = false;
			CS_0024_003C_003E8__locals23.documentSignId = 0L;
			if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals23.inputADO.DocumentTypeCode))
			{
				EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(CS_0024_003C_003E8__locals23.inputADO.DocumentTypeCode);
				if (byCode != null && byCode.IS_HAS_ONE == 1)
				{
					List<V_EMR_DOCUMENT> view = new EmrDocument().GetView(new EmrDocumentViewFilter
					{
						TREATMENT_CODE__EXACT = CS_0024_003C_003E8__locals23.inputADO.Treatment.TREATMENT_CODE,
						DOCUMENT_TYPE_CODE__EXACT = CS_0024_003C_003E8__locals23.inputADO.DocumentTypeCode
					}, new CommonParam());
					flag = view != null && view.Count > 0;
					if (flag)
					{
						CS_0024_003C_003E8__locals23.documentSignId = view[0].ID;
					}
				}
			}
			if (CS_0024_003C_003E8__locals23.documentSignId == 0 && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals23.inputADO.HisCode))
			{
				List<V_EMR_DOCUMENT> view2 = new EmrDocument().GetView(new EmrDocumentViewFilter
				{
					TREATMENT_CODE__EXACT = CS_0024_003C_003E8__locals23.inputADO.Treatment.TREATMENT_CODE,
					DOCUMENT_TYPE_CODE__EXACT = ((!string.IsNullOrEmpty(CS_0024_003C_003E8__locals23.inputADO.DocumentTypeCode) && CS_0024_003C_003E8__locals23.inputADO.DocumentTypeCode.Length == 2) ? CS_0024_003C_003E8__locals23.inputADO.DocumentTypeCode : ""),
					ORDER_FIELD = "ID",
					ORDER_DIRECTION = "DESC"
				}, new CommonParam());
				view2 = ((view2 != null) ? view2.Where((V_EMR_DOCUMENT o) => o.HIS_CODE == CS_0024_003C_003E8__locals23.inputADO.HisCode).ToList() : null);
				flag = view2 != null && view2.Count > 0;
				if (flag)
				{
					CS_0024_003C_003E8__locals23.documentSignId = view2[0].ID;
				}
			}
			if (flag)
			{
				if (XtraMessageBox.Show(MessageUitl.GetMessage("VanBanCoTheDaTonTaiTrenHeThongEMRBanCoThucHienKhong"), MessageUitl.GetMessage("ThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals23.isShowSignedFile), CS_0024_003C_003E8__locals23.isShowSignedFile));
					if (CS_0024_003C_003E8__locals23.isShowSignedFile)
					{
						_003C_003Ec__DisplayClass16 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass16();
						_003C_003Ec__DisplayClass.CS_0024_003C_003E8__locals15 = CS_0024_003C_003E8__locals23;
						_003C_003Ec__DisplayClass._003C_003Ec__DisplayClass4_ = new _003C_003Ec__DisplayClass4_1();
						_003C_003Ec__DisplayClass._003C_003Ec__DisplayClass4_.sign = new EmrVersion().GetSignedDocumentLast(CS_0024_003C_003E8__locals23.documentSignId);
						if (_003C_003Ec__DisplayClass._003C_003Ec__DisplayClass4_.sign != null)
						{
							LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals23.documentSignId), CS_0024_003C_003E8__locals23.documentSignId) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<_003C_003Ec__DisplayClass4_1>>(Expression.Field(Expression.Constant(_003C_003Ec__DisplayClass), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), _003C_003Ec__DisplayClass._003C_003Ec__DisplayClass4_.sign.URL));
							MemoryStream file = FssFileDownload.GetFile(_003C_003Ec__DisplayClass._003C_003Ec__DisplayClass4_.sign.URL);
							if (file != null && file.Length > 0)
							{
								file.Position = 0L;
								byte[] array = Utils.StreamToByte(file);
								if (array != null && array.Length != 0)
								{
									inputByte = array;
									LogSystem.Info("Ton tai van ban da co tren he thong cua ho so & loai van ban ==> show thong bao co muon thuc hien ==> chon khong muon thuc hien ==> he thong tim duoc file da ky truoc do cua ho so de show len voi che do chi xem");
									return true;
								}
							}
						}
					}
					LogSystem.Info("Ton tai van ban da co tren he thong cua ho so & loai van ban ==> show thong bao co muon thuc hien ==> chon khong muon thuc hien ==> he thong khong tim duoc file da ky truoc do de view");
					return false;
				}
				LogSystem.Info("Ton tai van ban da co tren he thong cua ho so & loai van ban ==> show thong bao co muon thuc hien ==> chon co muon thuc hien tiep ==> mo xem ky nhu binh thuong");
			}
			return true;
		}

		internal static bool VerifyTreatmentCode(InputADO inputADO, ref SignToken signToken)
		{
			bool flag = false;
			try
			{
				if (signToken == null)
				{
					signToken = new SignToken();
				}
				if (inputADO.IsOutsideTreatment == 1)
				{
					signToken.Treatment = new EMR_TREATMENT
					{
						TREATMENT_CODE = inputADO.Treatment.TREATMENT_CODE
					};
					return true;
				}
				if (!string.IsNullOrEmpty(inputADO.Treatment.TREATMENT_CODE))
				{
					signToken.Treatment = new EmrTreatment().GetByCode(inputADO.Treatment.TREATMENT_CODE);
					flag = signToken.Treatment != null && signToken.Treatment.ID > 0;
				}
				if (!flag)
				{
					MessageManager.Show(string.Format(MessageUitl.GetMessage("MaHoSoDieuTriKhongHopLe"), inputADO.Treatment.TREATMENT_CODE));
				}
			}
			catch (Exception ex)
			{
				flag = false;
				LogSystem.Warn(ex);
			}
			return flag;
		}

		internal static bool VerifySigner(List<SignTDO> signs, DocumentTDO document, EMR_TREATMENT treatment, ref string err, bool isPatientOrHomeRelativeSign = false, bool IsUsingSignPad = false)
		{
			bool result = true;
			try
			{
				if (GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION == "3" && isPatientOrHomeRelativeSign)
				{
					if (GlobalStore.SIGN_CERTIFICATE_OPTION != "1")
					{
						return result;
					}
					if (GlobalStore.SIGN_CERTIFICATE_OPTION == "1" && IsUsingSignPad)
					{
						return result;
					}
				}
				if (document != null && !string.IsNullOrEmpty(document.DocumentCode))
				{
					V_EMR_DOCUMENT viewByCode = new EmrDocument().GetViewByCode(document.DocumentCode);
					if (viewByCode != null && !string.IsNullOrEmpty(viewByCode.NEXT_SIGNER) && viewByCode.NEXT_SIGNER.Replace("#@!@#", "") == treatment.PATIENT_CODE)
					{
						result = true;
					}
					else
					{
						result = false;
						err += string.Format(MessageUitl.GetMessage("BenhNhanKhongPhaiNguoiKyTiepTheoVuiLongKiemTraLai"), treatment.VIR_PATIENT_NAME);
						err = err + " | PATIENT_CODE = " + treatment.PATIENT_CODE;
					}
				}
				else
				{
					result = signs != null && signs.Count > 0;
					if (result)
					{
						SignTDO signTDO = signs.Where((SignTDO o) => !o.SignerId.HasValue && o.PatientCode == treatment.PATIENT_CODE).FirstOrDefault();
						if (signTDO != null)
						{
							long minNumOrder = signs.Min((SignTDO o) => o.NumOrder);
							result = result && signs.Any((SignTDO o) => !o.SignerId.HasValue && o.NumOrder == minNumOrder);
							if (!result)
							{
								err += string.Format(MessageUitl.GetMessage("BenhNhanKhongPhaiNguoiKyTiepTheoVuiLongKiemTraLai"), treatment.VIR_PATIENT_NAME);
							}
						}
						else
						{
							result = false;
							err += string.Format(MessageUitl.GetMessage("BenhNhanKhongCoTrongLuongKy"), treatment.VIR_PATIENT_NAME);
						}
					}
					else
					{
						err += string.Format(MessageUitl.GetMessage("BenhNhanKhongCoTrongLuongKy"), treatment.VIR_PATIENT_NAME);
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		internal static bool VerifySignPrintNow(List<FileADO> filesigns, ref string err)
		{
			bool flag = true;
			try
			{
				flag = filesigns != null && filesigns.Count > 0;
				if (flag)
				{
					if (filesigns.Exists((FileADO o) => string.IsNullOrEmpty(o.Base64FileContent)))
					{
						flag = false;
						err += string.Format(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					}
				}
				else
				{
					err += string.Format(MessageUitl.GetMessage("DuLieuKhongHopLe"));
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}
	}
}
