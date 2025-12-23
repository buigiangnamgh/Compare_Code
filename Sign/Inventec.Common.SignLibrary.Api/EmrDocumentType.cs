using System;
using System.Collections.Generic;
using System.Linq;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrDocumentType : BussinessBase
	{
		internal EmrDocumentType()
		{
		}

		internal EmrDocumentType(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_DOCUMENT_TYPE> Get(EmrDocumentTypeFilter filter)
		{
			List<EMR_DOCUMENT_TYPE> list = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				return GlobalStore.EmrConsumer.Get<List<EMR_DOCUMENT_TYPE>>("api/EmrDocumentType/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal List<EMR_DOCUMENT_TYPE> Get()
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			List<EMR_DOCUMENT_TYPE> list = null;
			try
			{
				EmrDocumentTypeFilter filter = new EmrDocumentTypeFilter();
				return Get(filter);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return null;
			}
		}

		internal EMR_DOCUMENT_TYPE GetByCode(string code)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			EMR_DOCUMENT_TYPE result = null;
			try
			{
				if (!string.IsNullOrEmpty(code))
				{
					EmrDocumentTypeFilter filter = new EmrDocumentTypeFilter
					{
						DOCUMENT_TYPE_CODE__EXACT = code
					};
					result = Get(filter).FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				result = null;
			}
			return result;
		}

		internal bool IsMultiSign(string documentTypeCode)
		{
			bool flag = false;
			try
			{
				EMR_DOCUMENT_TYPE val = ((!string.IsNullOrEmpty(documentTypeCode)) ? DocumentTypeProperty(documentTypeCode) : null);
				return val != null && val.IS_MULTI_SIGN == 1;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				return false;
			}
		}

		internal void DocumentTypeProperty(string documentTypeCode, ref bool isMultiSign, ref bool isSignParanel)
		{
			try
			{
				EMR_DOCUMENT_TYPE val = ((!string.IsNullOrEmpty(documentTypeCode)) ? DocumentTypeProperty(documentTypeCode) : null);
				isMultiSign = val != null && val.IS_MULTI_SIGN == 1;
				isSignParanel = val != null && val.IS_SIGN_PARALLEL == 1;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal void DocumentTypeProperty(string documentTypeCode, ref bool isMultiSign, ref bool isSignParanel, ref long? documentTypeId)
		{
			try
			{
				EMR_DOCUMENT_TYPE val = ((!string.IsNullOrEmpty(documentTypeCode)) ? DocumentTypeProperty(documentTypeCode) : null);
				isMultiSign = val != null && val.IS_MULTI_SIGN == 1;
				isSignParanel = val != null && val.IS_SIGN_PARALLEL == 1;
				documentTypeId = ((val != null) ? new long?(val.ID) : ((long?)null));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal EMR_DOCUMENT_TYPE DocumentTypeProperty(string documentTypeCode)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			try
			{
				EmrDocumentTypeFilter filter = new EmrDocumentTypeFilter
				{
					DOCUMENT_TYPE_CODE__EXACT = documentTypeCode
				};
				List<EMR_DOCUMENT_TYPE> list = Get(filter);
				return (list != null && list.Count > 0) ? list.FirstOrDefault() : null;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return null;
		}
	}
}
