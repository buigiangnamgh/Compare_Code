using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.SDO;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.UC.Login.Base;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrSign : BussinessBase
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public EMR_SIGNER signer;

			public EMR_TREATMENT treatment;

			public EMR_SIGN data;

			public V_EMR_DOCUMENT document;

			internal bool _003CGetSignDocumentFirst_003Eb__6(EMR_SIGN o)
			{
				return o.LOGINNAME == signer.LOGINNAME;
			}

			internal bool _003CGetSignDocumentFirst_003Eb__7(EMR_SIGN o)
			{
				return o.PATIENT_CODE == treatment.PATIENT_CODE;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1f
		{
			public _003C_003Ec__DisplayClass10_0 CS_0024_003C_003E8__locals26;

			public bool _003CGetSignDocumentFirst_003Eb__8(EMR_SIGN o)
			{
				return o.LOGINNAME == CS_0024_003C_003E8__locals26.signer.LOGINNAME;
			}

			public bool _003CGetSignDocumentFirst_003Eb__9(EMR_SIGN o)
			{
				return o.PATIENT_CODE == CS_0024_003C_003E8__locals26.treatment.PATIENT_CODE;
			}
		}

		private V_EMR_DOCUMENT document { get; set; }

		internal EmrSign()
		{
		}

		internal EmrSign(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_SIGN> Get(EmrSignFilter filter)
		{
			List<EMR_SIGN> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_SIGN>>("api/EmrSign/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal SignTDO GetSignDocumentFirstByLoginName(string documentCode, EMR_SIGNER signer)
		{
			SignTDO result = null;
			try
			{
				if (!string.IsNullOrEmpty(documentCode) && signer != null)
				{
					EMR_DOCUMENT byCode = new EmrDocument().GetByCode(documentCode);
					CommonParam commonParam = new CommonParam();
					EmrSignFilter emrSignFilter = new EmrSignFilter();
					emrSignFilter.LOGINNAME__EXACT = signer.LOGINNAME;
					emrSignFilter.DOCUMENT_ID = byCode.ID;
					emrSignFilter.HAS_SIGN_TIME = false;
					EMR_SIGN eMR_SIGN = (from o in GlobalStore.EmrConsumer.Get<List<EMR_SIGN>>("api/EmrSign/Get", commonParam, emrSignFilter, new object[0])
						orderby o.NUM_ORDER
						select o).FirstOrDefault();
					if (eMR_SIGN != null)
					{
						SignTDO signTDO = new SignTDO();
						signTDO.DepartmentCode = eMR_SIGN.DEPARTMENT_CODE;
						signTDO.DepartmentName = eMR_SIGN.DEPARTMENT_NAME;
						signTDO.FirstName = eMR_SIGN.FIRST_NAME;
						signTDO.FullName = eMR_SIGN.VIR_PATIENT_NAME;
						signTDO.LastName = eMR_SIGN.LAST_NAME;
						signTDO.Loginname = eMR_SIGN.LOGINNAME;
						signTDO.NumOrder = eMR_SIGN.NUM_ORDER;
						signTDO.PatientCode = eMR_SIGN.PATIENT_CODE;
						signTDO.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
						signTDO.SignerId = signer.ID;
						signTDO.DocumentCode = documentCode;
						signTDO.Title = eMR_SIGN.TITLE;
						signTDO.Username = eMR_SIGN.USERNAME;
						result = signTDO;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				result = null;
			}
			return result;
		}

		internal EMR_SIGN GetSignDocumentFirst(string documentCode, EMR_SIGNER signer, EMR_TREATMENT treatment, bool isMultiSign, bool isGetOtherSignTimeNull)
		{
			EMR_SIGN eMR_SIGN = null;
			try
			{
				document = GetDocumentView(documentCode);
				return (document != null) ? GetSignDocumentFirst(document, signer, treatment, isGetOtherSignTimeNull, isMultiSign) : null;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal V_EMR_DOCUMENT GetDocumentView(string documentCode)
		{
			return document = new EmrDocument().GetViewByCode(documentCode);
		}

		internal EMR_SIGN GetSignDocumentFirst(V_EMR_DOCUMENT document, EMR_SIGNER signer, EMR_TREATMENT treatment, bool isGetOtherSignTimeNull = false, bool? isMultiSign = null)
		{
			_003C_003Ec__DisplayClass1f CS_0024_003C_003E8__locals28 = new _003C_003Ec__DisplayClass1f();
			CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26 = new _003C_003Ec__DisplayClass10_0();
			CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.signer = signer;
			CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.treatment = treatment;
			CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.document = document;
			CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data = null;
			try
			{
				_003C_003Ec__DisplayClass1f _003C_003Ec__DisplayClass1f = CS_0024_003C_003E8__locals28;
				bool flag = (isMultiSign.HasValue ? isMultiSign.Value : (CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.document.IS_MULTI_SIGN == 1));
				string name = ClientTokenManagerStore.ClientTokenManager.GetLoginName();
				if (string.IsNullOrEmpty(name))
				{
					name = GlobalStore.LoginName;
				}
				CommonParam commonParam = new CommonParam();
				EmrSignFilter emrSignFilter = new EmrSignFilter();
				emrSignFilter.DOCUMENT_ID = CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.document.ID;
				List<EMR_SIGN> datas = GlobalStore.EmrConsumer.Get<List<EMR_SIGN>>("api/EmrSign/Get", commonParam, emrSignFilter, new object[0]);
				if (datas != null && datas.Count > 0)
				{
					bool flag2 = datas.Any((EMR_SIGN o) => o.FLOW_ID.HasValue && o.FLOW_ID > 0 && (o.IS_SIGNING == 1 || (o.IS_SIGNING != 1 && o.SIGN_TIME.GetValueOrDefault() <= 0)));
					LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => datas), datas));
					if (flag2)
					{
						if (datas.Any((EMR_SIGN o) => o.FLOW_ID.HasValue && o.FLOW_ID > 0 && (o.IS_SIGNING == 1 || (o.IS_SIGNING != 1 && o.SIGN_TIME.GetValueOrDefault() <= 0))))
						{
							CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data = datas.FirstOrDefault((EMR_SIGN o) => (o.IS_SIGNING == 1 || (o.IS_SIGNING != 1 && o.SIGN_TIME.GetValueOrDefault() <= 0)) && o.FLOW_ID.HasValue && o.FLOW_ID > 0 && (("," + o.UN_SIGNERS + ",").Contains("," + name + ",") || name == o.LOGINNAME));
						}
						else
						{
							CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data = (from o in datas.Where(delegate(EMR_SIGN o)
								{
									int result;
									if (o.FLOW_ID.HasValue)
									{
										long? fLOW_ID = o.FLOW_ID;
										if (fLOW_ID.GetValueOrDefault() > 0 && fLOW_ID.HasValue && (o.IS_SIGNING == 1 || (o.SIGN_TIME.GetValueOrDefault() <= 0 && o.IS_SIGNING != 1)))
										{
											result = (("," + o.UN_SIGNERS + ",").Contains("," + name + ",") ? 1 : 0);
											goto IL_00b8;
										}
									}
									result = 0;
									goto IL_00b8;
									IL_00b8:
									return (byte)result != 0;
								})
								orderby o.NUM_ORDER
								select o).FirstOrDefault();
						}
					}
					else
					{
						if (CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.signer != null)
						{
							datas = datas.Where((EMR_SIGN o) => o.LOGINNAME == CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.signer.LOGINNAME).ToList();
						}
						else
						{
							datas = datas.Where((EMR_SIGN o) => o.PATIENT_CODE == CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.treatment.PATIENT_CODE).ToList();
						}
						CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data = (flag ? (datas.Any((EMR_SIGN o) => o.IS_SIGNING == 1) ? datas.FirstOrDefault((EMR_SIGN o) => o.IS_SIGNING == 1) : (isGetOtherSignTimeNull ? (from o in datas
							where !o.SIGN_TIME.HasValue
							orderby o.NUM_ORDER
							select o).FirstOrDefault() : null)) : (isGetOtherSignTimeNull ? (from o in datas
							where !o.SIGN_TIME.HasValue
							orderby o.NUM_ORDER
							select o).FirstOrDefault() : null));
						CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data = ((CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.signer == null && CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data == null) ? (from o in datas
							where !o.SIGN_TIME.HasValue
							orderby o.NUM_ORDER
							select o).FirstOrDefault() : CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data);
					}
					LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data), CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data));
				}
				else
				{
					LogSystem.Warn("Ky voi van ban da ton tai, khong tim thay sign cua nguoi ky de ky, khong the tiep tuc luong ky____" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.document), CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.document));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Khong tim thay ban ghi EMR_SIGN nao thoa man____Input data:" + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.document), CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.document) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<EMR_SIGNER>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals28), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.signer), ex);
				CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data = null;
			}
			return CS_0024_003C_003E8__locals28.CS_0024_003C_003E8__locals26.data;
		}

		internal List<EMR_SIGN> GetSignDocumentFirstForSignElectronic(DocumentTDO document)
		{
			List<EMR_SIGN> result = null;
			try
			{
				V_EMR_DOCUMENT viewByCode = new EmrDocument().GetViewByCode(document.DocumentCode);
				CommonParam commonParam = new CommonParam();
				EmrSignFilter emrSignFilter = new EmrSignFilter();
				emrSignFilter.DOCUMENT_ID = viewByCode.ID;
				List<EMR_SIGN> list = GlobalStore.EmrConsumer.Get<List<EMR_SIGN>>("api/EmrSign/Get", commonParam, emrSignFilter, new object[0]);
				if (list != null && list.Count > 0)
				{
					result = list.Where(delegate(EMR_SIGN o)
					{
						short? iS_SIGN_ELECTRONIC = o.IS_SIGN_ELECTRONIC;
						return iS_SIGN_ELECTRONIC == 1 && iS_SIGN_ELECTRONIC.HasValue && o.IS_SIGN_BOARD != 1 && o.REJECT_TIME.GetValueOrDefault() == 0;
					}).ToList();
				}
				else
				{
					LogSystem.Warn("Ky voi van ban da ton tai, khong tim thay sign cua nguoi ky de ky, khong the tiep tuc luong ky____" + LogUtil.TraceData(LogUtil.GetMemberName(() => document), document));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				result = null;
			}
			return result;
		}

		internal List<EMR_SIGN> GetSignDocumentForDocument(DocumentTDO document)
		{
			List<EMR_SIGN> list = null;
			try
			{
				V_EMR_DOCUMENT viewByCode = new EmrDocument().GetViewByCode(document.DocumentCode);
				CommonParam commonParam = new CommonParam();
				EmrSignFilter emrSignFilter = new EmrSignFilter();
				emrSignFilter.DOCUMENT_ID = viewByCode.ID;
				return GlobalStore.EmrConsumer.Get<List<EMR_SIGN>>("api/EmrSign/Get", commonParam, emrSignFilter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal bool Reject(string TokenCode, EmrSignRejectSDO signRejectSDO)
		{
			CommonParam commonParam = new CommonParam();
			bool result = false;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				ApiResultObject<EMR_SIGN> apiResultObject = setDicConsumer.PostRO<ApiResultObject<EMR_SIGN>>("api/EmrSign/Reject", commonParam, signRejectSDO, new object[0]);
				if (apiResultObject != null)
				{
					result = apiResultObject.Success;
					if (apiResultObject.Param != null)
					{
						base.param.Messages.AddRange(apiResultObject.Param.Messages);
						base.param.BugCodes.AddRange(apiResultObject.Param.BugCodes);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				result = false;
			}
			return result;
		}

		internal EMR_SIGN SignEnd(string TokenCode, long signId)
		{
			CommonParam commonParam = new CommonParam();
			EMR_SIGN result = null;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				ApiResultObject<EMR_SIGN> apiResultObject = setDicConsumer.PostRO<ApiResultObject<EMR_SIGN>>("api/EmrSign/Finish", commonParam, signId, new object[0]);
				if (apiResultObject != null)
				{
					result = apiResultObject.Data;
					if (apiResultObject.Param != null)
					{
						base.param.Messages.AddRange(apiResultObject.Param.Messages);
						base.param.BugCodes.AddRange(apiResultObject.Param.BugCodes);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				result = null;
			}
			return result;
		}
	}
}
