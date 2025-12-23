using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMR.EFMODEL.DataModels;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignFile;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.CacheClient;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	internal class VerifySign
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public EMR_SIGNER signer;

			public bool? vOption;

			public bool? isShowMessageAlert;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_1
		{
			public string vlOptionCfg;

			public _003C_003Ec__DisplayClass3_0 CS_0024_003C_003E8__locals1;
		}

		private static bool? optionChoice;

		internal static long GetNumOderByCommentText(string commentText)
		{
			long result = 0L;
			try
			{
				string[] array = commentText.Split(new string[1] { "__" }, StringSplitOptions.RemoveEmptyEntries);
				if (array != null && array.Count() > 0)
				{
					string inputValue = array[0].Replace("$", "").Replace("\n", "");
					result = TypeConvertParse.ToInt64(inputValue);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal static long GetNumOrderBySignOrDefault(EMR_SIGN signSelected, EMR_SIGNER singer, EMR_TREATMENT treatment, List<SignTDO> listSign = null, bool isPatientSign = false, bool isHomeRelativeSign = false, int nextNum = 0)
		{
			long num = -1L;
			try
			{
				if (signSelected != null)
				{
					num = signSelected.NUM_ORDER;
				}
				else if (listSign != null && listSign.Count > 0)
				{
					List<SignTDO> list = null;
					list = ((!(isPatientSign || isHomeRelativeSign)) ? listSign.Where((SignTDO o) => o.Loginname == singer.LOGINNAME).ToList() : listSign.Where((SignTDO o) => o.PatientCode == treatment.PATIENT_CODE).ToList());
					num = ((list != null && list.Count > 0) ? list.FirstOrDefault().NumOrder : (-1));
				}
				else
				{
					num = (long)nextNum + 1L;
				}
			}
			catch (Exception ex)
			{
				num = -1L;
				LogSystem.Warn(ex);
			}
			LogSystem.Debug("GetNumOrderBySignOrDefault: " + num);
			return num;
		}

		internal static bool? VerifySignImageWithOption(InputADO inputADO, EMR_SIGNER signer, bool hasNextSignPosition, SignPositionADO nextSignPosition, bool isMultiSign)
		{
			_003C_003Ec__DisplayClass3_0 CS_0024_003C_003E8__locals32 = new _003C_003Ec__DisplayClass3_0();
			CS_0024_003C_003E8__locals32.signer = signer;
			CS_0024_003C_003E8__locals32.vOption = null;
			CS_0024_003C_003E8__locals32.isShowMessageAlert = null;
			optionChoice = null;
			try
			{
				if (CS_0024_003C_003E8__locals32.signer != null)
				{
					if (GlobalStore.EMR_EMR_SIGNER_AUTO_UPDATE_SIGN_IMAGE == "2")
					{
						return CS_0024_003C_003E8__locals32.vOption;
					}
					if (CS_0024_003C_003E8__locals32.signer.SIGN_IMAGE == null || CS_0024_003C_003E8__locals32.signer.SIGN_IMAGE.Length == 0)
					{
						_003C_003Ec__DisplayClass3_1 CS_0024_003C_003E8__locals31 = new _003C_003Ec__DisplayClass3_1();
						CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1 = CS_0024_003C_003E8__locals32;
						EMR_CONFIG val = GlobalStore.EmrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGN_DISPLAY_OPTION").FirstOrDefault();
						CS_0024_003C_003E8__locals31.vlOptionCfg = ((val == null) ? "" : ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : val.DEFAULT_VALUE));
						if (hasNextSignPosition && nextSignPosition != null)
						{
							if (isMultiSign && nextSignPosition.SignPositionAutos != null && nextSignPosition.SignPositionAutos.Count > 0)
							{
								List<SignPositionADO> list = nextSignPosition.SignPositionAutos.OrderBy((SignPositionADO o) => GetNumOderByCommentText(o.Text)).ToList();
								foreach (SignPositionADO item in list)
								{
									if (item.TypeDisplay > 0)
									{
										CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert = item.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP || item.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
										break;
									}
								}
							}
							else if (nextSignPosition.TypeDisplay > 0)
							{
								CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert = nextSignPosition.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP || nextSignPosition.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT;
							}
						}
						if (!CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert.HasValue && (CS_0024_003C_003E8__locals31.vlOptionCfg == "2" || (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals31.vlOptionCfg) && CS_0024_003C_003E8__locals31.vlOptionCfg != "1" && CS_0024_003C_003E8__locals31.vlOptionCfg != "3" && CS_0024_003C_003E8__locals31.vlOptionCfg != "2")))
						{
							CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert = true;
						}
						if (CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert.HasValue && CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert.Value)
						{
							_003C_003Ec__DisplayClass3_1 _003C_003Ec__DisplayClass3_ = CS_0024_003C_003E8__locals31;
							string vlState = CacheClientWorker.GetValue();
							if (!string.IsNullOrEmpty(vlState))
							{
								_003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.vOption = vlState == Constans.DISPLAY_IMAGE_STAMP.ToString();
								if (_003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.vOption == false)
								{
									MessageBox.Show(MessageUitl.GetMessage("TaiKhoanThieuThongTinAnh"));
								}
							}
							else
							{
								bool IsReturn = false;
								if (!string.IsNullOrEmpty(_003C_003Ec__DisplayClass3_.vlOptionCfg) && _003C_003Ec__DisplayClass3_.vlOptionCfg != "1" && _003C_003Ec__DisplayClass3_.vlOptionCfg != "3" && GlobalStore.EMR_EMR_SIGNER_AUTO_UPDATE_SIGN_IMAGE == "1" && GlobalStore.EMR_HSM_INTEGRATE_OPTION == "4")
								{
									frmConfirmAutoUpdateSign frmConfirmAutoUpdateSign2 = new frmConfirmAutoUpdateSign(delegate(bool confirm)
									{
										if (confirm)
										{
											CommonParam commonParam = new CommonParam();
											EMR_SIGNER val2 = GlobalStore.EmrConsumer.Post<EMR_SIGNER>("api/EmrSigner/AutoUpdateSignImage", commonParam, _003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.signer.ID, new object[0]);
											if (val2 != null && val2.SIGN_IMAGE != null && val2.SIGN_IMAGE.Length != 0)
											{
												_003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.signer = val2;
												IsReturn = true;
											}
										}
									});
									frmConfirmAutoUpdateSign2.ShowDialog();
								}
								if (!IsReturn)
								{
									frmConfirmSign frmConfirmSign2 = new frmConfirmSign(ActNo, ActYes);
									frmConfirmSign2.ShowDialog();
									_003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.vOption = optionChoice.HasValue && optionChoice.Value;
								}
							}
							LogSystem.Info(MessageUitl.GetMessage("TaiKhoanThieuThongTinAnhBanCoMuonBoHienThiAnh") + "____nguoi dung chon " + ((_003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.vOption.HasValue && _003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.vOption.Value) ? "bo hien thi anh chi hien thi thong tin dang text" : "tu choi & ket thuc khong ky____") + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => _003C_003Ec__DisplayClass3_.vlOptionCfg)), (object)_003C_003Ec__DisplayClass3_.vlOptionCfg) + LogUtil.TraceData(LogUtil.GetMemberName<bool?>((Expression<Func<bool?>>)(() => optionChoice)), (object)optionChoice) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => vlState)), (object)vlState) + LogUtil.TraceData(LogUtil.GetMemberName<bool?>((Expression<Func<bool?>>)(() => _003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.vOption)), (object)_003C_003Ec__DisplayClass3_.CS_0024_003C_003E8__locals1.vOption));
						}
						else
						{
							LogSystem.Warn("Khong hien thi thong bao do du lieu hop le____" + LogUtil.TraceData(LogUtil.GetMemberName<bool?>((Expression<Func<bool?>>)(() => CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert)), (object)CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.isShowMessageAlert) + LogUtil.TraceData(LogUtil.GetMemberName<bool?>(Expression.Lambda<Func<bool?>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals31, typeof(_003C_003Ec__DisplayClass3_1)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals31.CS_0024_003C_003E8__locals1.vOption));
						}
					}
					else
					{
						LogSystem.Debug("GlobalStore.Singer.SIGN_IMAGE has value");
					}
				}
				else
				{
					LogSystem.Warn("Khong tim thay thong tin nguoi ky tren emr tuong ung voi tai khoan dang ky" + LogUtil.TraceData(LogUtil.GetMemberName<EMR_SIGNER>((Expression<Func<EMR_SIGNER>>)(() => CS_0024_003C_003E8__locals32.signer)), (object)CS_0024_003C_003E8__locals32.signer) + LogUtil.TraceData(LogUtil.GetMemberName<bool?>(Expression.Lambda<Func<bool?>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals32, typeof(_003C_003Ec__DisplayClass3_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals32.vOption));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return CS_0024_003C_003E8__locals32.vOption;
		}

		private static void ActYes()
		{
			optionChoice = true;
		}

		private static void ActNo()
		{
			optionChoice = false;
		}
	}
}
