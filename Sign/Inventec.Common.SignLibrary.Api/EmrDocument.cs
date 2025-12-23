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
using Inventec.Common.SignLibrary.Popup;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrDocument : BussinessBase
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass14_0
		{
			public string dependentCode;

			public string treatmentCode;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public EmrDocumentFilter filter;

			public CommonParam paramCommon;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public EmrDocumentViewFilter filter;

			public CommonParam paramCommon;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass0
		{
			public _003C_003Ec__DisplayClass2_0 CS_0024_003C_003E8__locals8;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2
		{
			public _003C_003Ec__DisplayClass3_0 CS_0024_003C_003E8__locals12;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass28
		{
			public _003C_003Ec__DisplayClass14_0 CS_0024_003C_003E8__locals9;
		}

		internal EmrDocument()
		{
		}

		internal EmrDocument(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_DOCUMENT> Get(EmrDocumentFilter filter, CommonParam paramCommon)
		{
			_003C_003Ec__DisplayClass0 CS_0024_003C_003E8__locals9 = new _003C_003Ec__DisplayClass0();
			CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8 = new _003C_003Ec__DisplayClass2_0();
			CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8.filter = filter;
			CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8.paramCommon = paramCommon;
			List<EMR_DOCUMENT> list = null;
			try
			{
				return GlobalStore.EmrConsumer.Get<List<EMR_DOCUMENT>>("api/EmrDocument/Get", CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8.paramCommon, CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8.filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8.filter), CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8.filter) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals9), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals9.CS_0024_003C_003E8__locals8.paramCommon), ex);
				return null;
			}
		}

		internal List<V_EMR_DOCUMENT> GetView(EmrDocumentViewFilter filter, CommonParam paramCommon)
		{
			_003C_003Ec__DisplayClass2 CS_0024_003C_003E8__locals13 = new _003C_003Ec__DisplayClass2();
			CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12 = new _003C_003Ec__DisplayClass3_0();
			CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.filter = filter;
			CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.paramCommon = paramCommon;
			List<V_EMR_DOCUMENT> list = null;
			try
			{
				list = GlobalStore.EmrConsumer.Get<List<V_EMR_DOCUMENT>>("api/EmrDocument/GetView", CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.paramCommon, CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.filter, new object[0]);
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.filter), CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.filter) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals13), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.paramCommon) + "____" + GlobalStore.EmrConsumer.GetBaseUri());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.filter), CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.filter) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals13), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals12.paramCommon), ex);
				list = null;
			}
			return list;
		}

		internal EMR_DOCUMENT GetByCode(string code)
		{
			EMR_DOCUMENT result = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(code))
				{
					CommonParam commonParam = new CommonParam();
					EmrDocumentFilter emrDocumentFilter = new EmrDocumentFilter();
					emrDocumentFilter.DOCUMENT_CODE__EXACT = code;
					List<EMR_DOCUMENT> list = GlobalStore.EmrConsumer.Get<List<EMR_DOCUMENT>>("api/EmrDocument/Get", commonParam, emrDocumentFilter, new object[0]);
					result = ((list != null) ? list.FirstOrDefault((EMR_DOCUMENT o) => o.DOCUMENT_CODE == code) : null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => code), code), ex);
				result = null;
			}
			return result;
		}

		internal List<EMR_DOCUMENT> GetByMergeCode(string mergeCode)
		{
			List<EMR_DOCUMENT> result = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(mergeCode))
				{
					CommonParam commonParam = new CommonParam();
					EmrDocumentFilter emrDocumentFilter = new EmrDocumentFilter();
					emrDocumentFilter.MERGE_CODE__EXACT = mergeCode;
					result = GlobalStore.EmrConsumer.Get<List<EMR_DOCUMENT>>("api/EmrDocument/Get", commonParam, emrDocumentFilter, new object[0]);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => mergeCode), mergeCode), ex);
				result = null;
			}
			return result;
		}

		internal byte[] GetDocumentByMergeCode(string mergeCode, string documentName, List<long> documentIds = null)
		{
			byte[] result = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(mergeCode))
				{
					CommonParam commonParam = new CommonParam();
					DocumentMergeSDO documentMergeSDO = new DocumentMergeSDO();
					documentMergeSDO.MergeCode = mergeCode;
					if (documentIds != null && documentIds.Count > 0)
					{
						documentMergeSDO.DocumentIds = documentIds;
					}
					if (!string.IsNullOrEmpty(documentName))
					{
						documentMergeSDO.DocumentName = documentName;
					}
					string text = GlobalStore.EmrConsumer.Post<string>("api/EmrDocument/MakeDocumentMergeBySdo", commonParam, documentMergeSDO, new object[0]);
					if (!string.IsNullOrEmpty(text))
					{
						result = Convert.FromBase64String(text);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => mergeCode), mergeCode), ex);
				result = null;
			}
			return result;
		}

		internal V_EMR_DOCUMENT GetViewByCode(string code)
		{
			V_EMR_DOCUMENT result = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(code))
				{
					CommonParam commonParam = new CommonParam();
					EmrDocumentViewFilter emrDocumentViewFilter = new EmrDocumentViewFilter();
					emrDocumentViewFilter.DOCUMENT_CODE__EXACT = code;
					List<V_EMR_DOCUMENT> list = GlobalStore.EmrConsumer.Get<List<V_EMR_DOCUMENT>>("api/EmrDocument/GetView", commonParam, emrDocumentViewFilter, new object[0]);
					result = ((list != null) ? list.FirstOrDefault((V_EMR_DOCUMENT o) => o.DOCUMENT_CODE == code) : null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => code), code), ex);
				result = null;
			}
			return result;
		}

		internal DocumentTDO CreateByTdo(string TokenCode, DocumentTDO dataDoc)
		{
			CommonParam paramCommon = new CommonParam();
			DocumentTDO result = null;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				LogSystem.Info("dataDoc " + LogUtil.TraceData("EMR.URI.EmrDocument.CREATE_BY_TDO ", dataDoc));
				ApiResultObject<DocumentTDO> rs = setDicConsumer.PostRO<ApiResultObject<DocumentTDO>>("api/EmrDocument/CreateByTdo", paramCommon, dataDoc, new object[0]);
				LogSystem.Info("rs " + LogUtil.TraceData("EmrConsumer.c ", rs));
				if (rs != null)
				{
					result = rs.Data;
					if (rs.Param != null)
					{
						base.param.Messages.AddRange(rs.Param.Messages);
						base.param.BugCodes.AddRange(rs.Param.BugCodes);
					}
				}
				else
				{
					LogSystem.Error("Call api api/EmrDocument/CreateByTdo return fail ____" + LogUtil.TraceData(LogUtil.GetMemberName(() => TokenCode), TokenCode) + LogUtil.TraceData("GlobalStore.GetDicEmrConsumer", GlobalStore.GetDicEmrConsumer()) + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + LogUtil.TraceData(LogUtil.GetMemberName(() => paramCommon), paramCommon));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => dataDoc), dataDoc), ex);
				result = null;
			}
			return result;
		}

		internal HsmSignCreateTDO CreateAndSignHsm(string TokenCode, HsmSignCreateTDO dataSign)
		{
			CommonParam paramCommon = new CommonParam();
			HsmSignCreateTDO result = null;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				ApiResultObject<HsmSignCreateTDO> rs = setDicConsumer.PostRO<ApiResultObject<HsmSignCreateTDO>>("api/EmrDocument/CreateAndSignHsm", paramCommon, dataSign, new object[0]);
				if (rs != null)
				{
					result = rs.Data;
					if (rs.Param != null && rs.Param.BugCodes != null && rs.Param.BugCodes.Count > 0 && rs.Param.BugCodes.Contains("EMR053"))
					{
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(GlobalStore.LoginName);
						frmUpdateSigner frmUpdateSigner = new frmUpdateSigner(byLoginName, 1);
						frmUpdateSigner.ShowDialog();
					}
					else if (rs.Param != null)
					{
						base.param.Messages.AddRange(rs.Param.Messages);
						base.param.BugCodes.AddRange(rs.Param.BugCodes);
					}
				}
				else
				{
					LogSystem.Error("Call api api/EmrDocument/CreateAndSignHsm return fail ____" + LogUtil.TraceData(LogUtil.GetMemberName(() => TokenCode), TokenCode) + LogUtil.TraceData("GlobalStore.GetDicEmrConsumer", GlobalStore.GetDicEmrConsumer()) + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + LogUtil.TraceData(LogUtil.GetMemberName(() => paramCommon), paramCommon));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = null;
			}
			return result;
		}

		internal EmrSignResultSDO SignHsm(string TokenCode, EmrSignHsmSDO dataSign)
		{
			CommonParam paramCommon = new CommonParam();
			EmrSignResultSDO result = null;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				ApiResultObject<EmrSignResultSDO> rs = setDicConsumer.PostRO<ApiResultObject<EmrSignResultSDO>>("api/EmrSign/SignPdfHsm", paramCommon, dataSign, new object[0]);
				if (rs != null)
				{
					result = rs.Data;
					if (rs.Param != null && rs.Param.BugCodes != null && rs.Param.BugCodes.Count > 0 && rs.Param.BugCodes.Contains("EMR053"))
					{
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(GlobalStore.LoginName);
						frmUpdateSigner frmUpdateSigner = new frmUpdateSigner(byLoginName, 1);
						frmUpdateSigner.ShowDialog();
					}
					else if (rs.Param != null)
					{
						base.param.Messages.AddRange(rs.Param.Messages);
						base.param.BugCodes.AddRange(rs.Param.BugCodes);
					}
				}
				else
				{
					LogSystem.Error("Call api api/EmrSign/SignPdfHsm return fail ____" + LogUtil.TraceData(LogUtil.GetMemberName(() => TokenCode), TokenCode) + LogUtil.TraceData("GlobalStore.GetDicEmrConsumer", GlobalStore.GetDicEmrConsumer()) + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + LogUtil.TraceData(LogUtil.GetMemberName(() => paramCommon), paramCommon));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = null;
			}
			return result;
		}

		internal EmrSignResultSDO PatientOrHomeRelativeSignHsm(string TokenCode, EmrSignHsmSDO dataSign)
		{
			CommonParam paramCommon = new CommonParam();
			EmrSignResultSDO result = null;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				ApiResultObject<EmrSignResultSDO> rs = setDicConsumer.PostRO<ApiResultObject<EmrSignResultSDO>>("api/EmrSign/PatientAddAndSign", paramCommon, dataSign, new object[0]);
				if (rs != null)
				{
					result = rs.Data;
					if (rs.Param != null && rs.Param.BugCodes != null && rs.Param.BugCodes.Count > 0 && rs.Param.BugCodes.Contains("EMR053"))
					{
						EMR_SIGNER byLoginName = GlobalStore.GetByLoginName(GlobalStore.LoginName);
						frmUpdateSigner frmUpdateSigner = new frmUpdateSigner(byLoginName, 1);
						frmUpdateSigner.ShowDialog();
					}
					else if (rs.Param != null)
					{
						base.param.Messages.AddRange(rs.Param.Messages);
						base.param.BugCodes.AddRange(rs.Param.BugCodes);
					}
				}
				else
				{
					LogSystem.Error("Call api api/EmrSign/SignPdfHsm return fail ____" + LogUtil.TraceData(LogUtil.GetMemberName(() => TokenCode), TokenCode) + LogUtil.TraceData("GlobalStore.GetDicEmrConsumer", GlobalStore.GetDicEmrConsumer()) + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + LogUtil.TraceData(LogUtil.GetMemberName(() => paramCommon), paramCommon));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = null;
			}
			return result;
		}

		internal UsbSignCreateTDO CreateAndSignUsb(string TokenCode, UsbSignCreateTDO dataSign)
		{
			CommonParam paramCommon = new CommonParam();
			UsbSignCreateTDO result = null;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				ApiResultObject<UsbSignCreateTDO> rs = setDicConsumer.PostRO<ApiResultObject<UsbSignCreateTDO>>("api/EmrDocument/CreateAndSignUsb", paramCommon, dataSign, new object[0]);
				if (rs != null)
				{
					result = rs.Data;
					if (rs.Param != null)
					{
						base.param.Messages.AddRange(rs.Param.Messages);
						base.param.BugCodes.AddRange(rs.Param.BugCodes);
					}
				}
				else
				{
					LogSystem.Error("Call api api/EmrDocument/CreateAndSignUsb return fail ____" + LogUtil.TraceData(LogUtil.GetMemberName(() => TokenCode), TokenCode) + LogUtil.TraceData("GlobalStore.GetDicEmrConsumer", GlobalStore.GetDicEmrConsumer()) + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + LogUtil.TraceData(LogUtil.GetMemberName(() => paramCommon), paramCommon));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = null;
			}
			return result;
		}

		internal EmrSignResultSDO SignUsb(string TokenCode, EmrSignUsbSDO dataSign)
		{
			CommonParam paramCommon = new CommonParam();
			EmrSignResultSDO result = null;
			try
			{
				ApiConsumer setDicConsumer = GlobalStore.GetSetDicConsumer(TokenCode);
				ApiResultObject<EmrSignResultSDO> rs = setDicConsumer.PostRO<ApiResultObject<EmrSignResultSDO>>("api/EmrSign/SignPdfUsb", paramCommon, dataSign, new object[0]);
				if (rs != null)
				{
					result = rs.Data;
					if (rs.Param != null)
					{
						base.param.Messages.AddRange(rs.Param.Messages);
						base.param.BugCodes.AddRange(rs.Param.BugCodes);
					}
				}
				else
				{
					LogSystem.Error("Call api api/EmrSign/SignPdfUsb return fail ____" + LogUtil.TraceData(LogUtil.GetMemberName(() => TokenCode), TokenCode) + LogUtil.TraceData("GlobalStore.GetDicEmrConsumer", GlobalStore.GetDicEmrConsumer()) + LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs) + LogUtil.TraceData(LogUtil.GetMemberName(() => paramCommon), paramCommon));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = null;
			}
			return result;
		}

		internal List<V_EMR_DOCUMENT> GetDocumentDependent(string dependentCode, string treatmentCode, string signer)
		{
			_003C_003Ec__DisplayClass28 CS_0024_003C_003E8__locals10 = new _003C_003Ec__DisplayClass28();
			CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9 = new _003C_003Ec__DisplayClass14_0();
			CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.dependentCode = dependentCode;
			CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.treatmentCode = treatmentCode;
			List<V_EMR_DOCUMENT> result = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.dependentCode))
				{
					CommonParam commonParam = new CommonParam();
					EmrDocumentViewFilter emrDocumentViewFilter = new EmrDocumentViewFilter();
					emrDocumentViewFilter.PARENT_DEPENDENT_CODE__EXACT = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.dependentCode;
					emrDocumentViewFilter.IS_DELETE = false;
					emrDocumentViewFilter.HAS_REJECTER = false;
					emrDocumentViewFilter.TREATMENT_CODE__EXACT = CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.treatmentCode;
					emrDocumentViewFilter.HAS_RESIGN_FAILED = false;
					emrDocumentViewFilter.HAS_NEXT_SIGNER = true;
					emrDocumentViewFilter.NEXT_SIGNER__EXACT = signer;
					result = GlobalStore.EmrConsumer.Get<List<V_EMR_DOCUMENT>>("api/EmrDocument/GetView", commonParam, emrDocumentViewFilter, new object[0]);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.dependentCode), CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.dependentCode) + "____" + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<string>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals10), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals10.CS_0024_003C_003E8__locals9.treatmentCode), ex);
				result = null;
			}
			return result;
		}
	}
}
