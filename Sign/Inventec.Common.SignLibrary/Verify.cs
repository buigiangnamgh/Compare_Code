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

		private const string SplitTagPatientCode = "#@!@#";

		private const int IS_HAS_ONE = 1;

		internal static bool VerifyHisCode(InputADO inputADO, bool isShowSignedFile, ref string base64FileSigned, ref V_EMR_DOCUMENT documentData)
		{
			return VerifyHisCode(inputADO, isShowSignedFile, false, ref base64FileSigned, ref documentData);
		}

		internal static bool VerifyHisCode(InputADO inputADO, bool isShowSignedFile, bool isValidExistsDoc, ref string base64FileSigned, ref V_EMR_DOCUMENT documentData)
		{
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0234: Expected O, but got Unknown
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			//IL_0603: Unknown result type (might be due to invalid IL or missing references)
			//IL_060a: Expected O, but got Unknown
			bool flag = false;
			long documentSignId = 0L;
			string text = "";
			bool isAllowDuplicateHisCode = false;
			if (!string.IsNullOrEmpty(inputADO.DocumentTypeCode))
			{
				EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(inputADO.DocumentTypeCode);
				isAllowDuplicateHisCode = byCode != null && byCode.IS_ALLOW_DUPLICATE_HIS_CODE == 1;
				if (byCode != null && byCode.IS_HAS_ONE == 1 && !string.IsNullOrEmpty(inputADO.Treatment.TREATMENT_CODE))
				{
					EmrDocumentViewFilter val = new EmrDocumentViewFilter();
					val.TREATMENT_CODE__EXACT = inputADO.Treatment.TREATMENT_CODE;
					if ((inputADO.Treatment.TREATMENT_CODE ?? "").ToUpper().StartsWith("MPS"))
					{
						val.HIS_CODE__EXACT = inputADO.HisCode;
					}
					val.DOCUMENT_TYPE_CODE__EXACT = inputADO.DocumentTypeCode;
					((FilterBase)val).ORDER_FIELD = "ID";
					((FilterBase)val).ORDER_DIRECTION = "DESC";
					val.IS_DELETE = false;
					List<V_EMR_DOCUMENT> view = new EmrDocument().GetView(val, new CommonParam());
					flag = view != null && view.Count > 0;
					if (flag)
					{
						documentSignId = view[0].ID;
						text = view[0].MERGE_CODE;
						documentData = view[0];
					}
				}
			}
			if (documentSignId == 0L && !string.IsNullOrEmpty(inputADO.HisCode))
			{
				EmrDocumentViewFilter val2 = new EmrDocumentViewFilter();
				val2.TREATMENT_CODE__EXACT = inputADO.Treatment.TREATMENT_CODE;
				if ((inputADO.Treatment.TREATMENT_CODE ?? "").ToUpper().StartsWith("MPS"))
				{
					val2.HIS_CODE__EXACT = inputADO.HisCode;
				}
				val2.DOCUMENT_TYPE_CODE__EXACT = inputADO.DocumentTypeCode;
				((FilterBase)val2).ORDER_FIELD = "ID";
				((FilterBase)val2).ORDER_DIRECTION = "DESC";
				val2.IS_DELETE = false;
				List<V_EMR_DOCUMENT> docWithHisCodes = new EmrDocument().GetView(val2, new CommonParam());
				docWithHisCodes = ((docWithHisCodes != null) ? docWithHisCodes.Where((V_EMR_DOCUMENT o) => o.HIS_CODE == inputADO.HisCode).ToList() : null);
				flag = docWithHisCodes != null && docWithHisCodes.Count > 0;
				if (flag)
				{
					documentSignId = docWithHisCodes[0].ID;
					text = docWithHisCodes[0].MERGE_CODE;
					documentData = docWithHisCodes[0];
				}
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<List<V_EMR_DOCUMENT>>((Expression<Func<List<V_EMR_DOCUMENT>>>)(() => docWithHisCodes)), (object)docWithHisCodes));
			}
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<long>((Expression<Func<long>>)(() => documentSignId)), (object)documentSignId));
			if (flag)
			{
				if (isAllowDuplicateHisCode)
				{
					isShowSignedFile = true;
				}
				else
				{
					List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
					if (emrConfigs != null && emrConfigs.Count > 0)
					{
						IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_DOCUMENT.DULICATE_HIS_CODE.WARNING_OPTION");
						EMR_CONFIG val3 = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
						if (val3 != null)
						{
							string text2 = ((!string.IsNullOrEmpty(val3.VALUE)) ? val3.VALUE : val3.DEFAULT_VALUE);
							if (text2 == "1" && documentSignId > 0)
							{
								isShowSignedFile = false;
								if (isValidExistsDoc)
								{
									LogSystem.Info(MessageUitl.GetMessage("VanBanDaTonTaiKhongTheKyTiepDoThietLapDangChan") + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isValidExistsDoc)), (object)isValidExistsDoc));
									XtraMessageBox.Show(MessageUitl.GetMessage("VanBanDaTonTaiKhongTheKyTiepDoThietLapDangChan"), MessageUitl.GetMessage("ThongBao"));
									documentData = null;
									return false;
								}
								LogSystem.Info(MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu") + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isShowSignedFile)), (object)isShowSignedFile));
								XtraMessageBox.Show(MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu"), MessageUitl.GetMessage("ThongBao"));
							}
							else if (text2 == "2" && documentSignId > 0)
							{
								EmrSign emrSign = new EmrSign();
								EmrSignFilter val4 = new EmrSignFilter();
								val4.DOCUMENT_ID = documentSignId;
								List<EMR_SIGN> signExists = emrSign.Get(val4);
								if (signExists != null && signExists.Count > 0 && signExists.Exists((EMR_SIGN o) => !o.REJECT_TIME.HasValue))
								{
									isShowSignedFile = false;
									LogSystem.Info("TH có key cau hinh EMR.EMR_DOCUMENT.DULICATE_HIS_CODE.WARNING_OPTION gia tri = " + text2 + " " + MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu") + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isShowSignedFile)), (object)isShowSignedFile) + LogUtil.TraceData(LogUtil.GetMemberName<List<EMR_SIGN>>((Expression<Func<List<EMR_SIGN>>>)(() => signExists)), (object)signExists) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isAllowDuplicateHisCode)), (object)isAllowDuplicateHisCode));
									XtraMessageBox.Show(MessageUitl.GetMessage("VanBanDaTonTaiPhanMemSeHienThiVanBanCu"), MessageUitl.GetMessage("ThongBao"));
								}
								else
								{
									LogSystem.Info("TH có key cau hinh EMR.EMR_DOCUMENT.DULICATE_HIS_CODE.WARNING_OPTION gia tri = " + text2 + " , nhung khong tim thay emr_sign nao thoa man: \"Văn bản đã ký hoặc đã thiết lập ký\"____" + LogUtil.TraceData(LogUtil.GetMemberName<List<EMR_SIGN>>((Expression<Func<List<EMR_SIGN>>>)(() => signExists)), (object)signExists) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isAllowDuplicateHisCode)), (object)isAllowDuplicateHisCode));
								}
							}
						}
					}
				}
				if (isShowSignedFile)
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
					_003C_003Ec__DisplayClass3_3 _003C_003Ec__DisplayClass3_ = new _003C_003Ec__DisplayClass3_3();
					_003C_003Ec__DisplayClass3_.lastVersionSigned = ((documentSignId > 0) ? new EmrVersion().GetSignedDocumentLast(documentSignId) : null);
					if (_003C_003Ec__DisplayClass3_.lastVersionSigned != null)
					{
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<long>((Expression<Func<long>>)(() => documentSignId)), (object)documentSignId) + LogUtil.TraceData(LogUtil.GetMemberName<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Field(Expression.Constant(_003C_003Ec__DisplayClass3_, typeof(_003C_003Ec__DisplayClass3_3)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)_003C_003Ec__DisplayClass3_.lastVersionSigned.URL));
						fileUrl = _003C_003Ec__DisplayClass3_.lastVersionSigned.URL;
					}
				}
				MemoryStream file = FssFileDownload.GetFile(fileUrl);
				if (file != null && file.Length > 0)
				{
					file.Position = 0L;
					string text3 = "";
					if (!string.IsNullOrEmpty(text))
					{
						string fileDocumentMerge = new DocumentManager().GetFileDocumentMerge(0m, inputADO.Treatment.TREATMENT_CODE, text);
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
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_017a: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Expected O, but got Unknown
			bool flag = false;
			long documentSignId = 0L;
			if (!string.IsNullOrEmpty(inputADO.DocumentTypeCode))
			{
				EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(inputADO.DocumentTypeCode);
				if (byCode != null && byCode.IS_HAS_ONE == 1)
				{
					EmrDocumentViewFilter val = new EmrDocumentViewFilter();
					val.TREATMENT_CODE__EXACT = inputADO.Treatment.TREATMENT_CODE;
					if ((inputADO.Treatment.TREATMENT_CODE ?? "").ToUpper().StartsWith("MPS"))
					{
						val.HIS_CODE__EXACT = inputADO.HisCode;
					}
					val.DOCUMENT_TYPE_CODE__EXACT = inputADO.DocumentTypeCode;
					List<V_EMR_DOCUMENT> view = new EmrDocument().GetView(val, new CommonParam());
					flag = view != null && view.Count > 0;
					if (flag)
					{
						documentSignId = view[0].ID;
					}
				}
			}
			if (documentSignId == 0L && !string.IsNullOrEmpty(inputADO.HisCode))
			{
				EmrDocumentViewFilter val2 = new EmrDocumentViewFilter();
				val2.TREATMENT_CODE__EXACT = inputADO.Treatment.TREATMENT_CODE;
				if ((inputADO.Treatment.TREATMENT_CODE ?? "").ToUpper().StartsWith("MPS"))
				{
					val2.HIS_CODE__EXACT = inputADO.HisCode;
				}
				val2.DOCUMENT_TYPE_CODE__EXACT = ((!string.IsNullOrEmpty(inputADO.DocumentTypeCode) && inputADO.DocumentTypeCode.Length == 2) ? inputADO.DocumentTypeCode : "");
				((FilterBase)val2).ORDER_FIELD = "ID";
				((FilterBase)val2).ORDER_DIRECTION = "DESC";
				val2.IS_DELETE = false;
				List<V_EMR_DOCUMENT> view2 = new EmrDocument().GetView(val2, new CommonParam());
				view2 = ((view2 != null) ? view2.Where((V_EMR_DOCUMENT o) => o.HIS_CODE == inputADO.HisCode).ToList() : null);
				flag = view2 != null && view2.Count > 0;
				if (flag)
				{
					documentSignId = view2[0].ID;
				}
			}
			if (flag)
			{
				if (XtraMessageBox.Show(MessageUitl.GetMessage("VanBanCoTheDaTonTaiTrenHeThongEMRBanCoThucHienKhong"), MessageUitl.GetMessage("ThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isShowSignedFile)), (object)isShowSignedFile));
					if (isShowSignedFile)
					{
						_003C_003Ec__DisplayClass4_1 _003C_003Ec__DisplayClass4_ = new _003C_003Ec__DisplayClass4_1();
						_003C_003Ec__DisplayClass4_.sign = new EmrVersion().GetSignedDocumentLast(documentSignId);
						if (_003C_003Ec__DisplayClass4_.sign != null)
						{
							LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<long>((Expression<Func<long>>)(() => documentSignId)), (object)documentSignId) + LogUtil.TraceData(LogUtil.GetMemberName<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Field(Expression.Constant(_003C_003Ec__DisplayClass4_, typeof(_003C_003Ec__DisplayClass4_1)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)_003C_003Ec__DisplayClass4_.sign.URL));
							MemoryStream file = FssFileDownload.GetFile(_003C_003Ec__DisplayClass4_.sign.URL);
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
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
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
						SignTDO val = signs.Where((SignTDO o) => !o.SignerId.HasValue && o.PatientCode == treatment.PATIENT_CODE).FirstOrDefault();
						if (val != null)
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
