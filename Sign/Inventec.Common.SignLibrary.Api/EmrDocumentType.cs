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
			EMR_DOCUMENT_TYPE result = null;
			try
			{
				if (!string.IsNullOrEmpty(code))
				{
					EmrDocumentTypeFilter emrDocumentTypeFilter = new EmrDocumentTypeFilter();
					emrDocumentTypeFilter.DOCUMENT_TYPE_CODE__EXACT = code;
					EmrDocumentTypeFilter filter = emrDocumentTypeFilter;
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
				EMR_DOCUMENT_TYPE eMR_DOCUMENT_TYPE = ((!string.IsNullOrEmpty(documentTypeCode)) ? DocumentTypeProperty(documentTypeCode) : null);
				return eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_MULTI_SIGN == 1;
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
				EMR_DOCUMENT_TYPE eMR_DOCUMENT_TYPE = ((!string.IsNullOrEmpty(documentTypeCode)) ? DocumentTypeProperty(documentTypeCode) : null);
				isMultiSign = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_MULTI_SIGN == 1;
				isSignParanel = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_SIGN_PARALLEL == 1;
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
				EMR_DOCUMENT_TYPE eMR_DOCUMENT_TYPE = ((!string.IsNullOrEmpty(documentTypeCode)) ? DocumentTypeProperty(documentTypeCode) : null);
				isMultiSign = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_MULTI_SIGN == 1;
				isSignParanel = eMR_DOCUMENT_TYPE != null && eMR_DOCUMENT_TYPE.IS_SIGN_PARALLEL == 1;
				documentTypeId = ((eMR_DOCUMENT_TYPE != null) ? new long?(eMR_DOCUMENT_TYPE.ID) : ((long?)null));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal EMR_DOCUMENT_TYPE DocumentTypeProperty(string documentTypeCode)
		{
			try
			{
				EmrDocumentTypeFilter emrDocumentTypeFilter = new EmrDocumentTypeFilter();
				emrDocumentTypeFilter.DOCUMENT_TYPE_CODE__EXACT = documentTypeCode;
				EmrDocumentTypeFilter filter = emrDocumentTypeFilter;
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
