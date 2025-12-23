using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Api
{
	internal class EmrTreatment : BussinessBase
	{
		internal EmrTreatment()
		{
		}

		internal EmrTreatment(CommonParam param)
			: base(param)
		{
		}

		internal List<EMR_TREATMENT> Get(EmrTreatmentFilter filter)
		{
			List<EMR_TREATMENT> result = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				result = GlobalStore.EmrConsumer.Get<List<EMR_TREATMENT>>("api/EmrTreatment/Get", commonParam, filter, new object[0]);
			}
			catch (Exception ex)
			{
				string baseUri = GlobalStore.EmrConsumer.GetBaseUri();
				LogSystem.Warn("EmrTreatment.Get:" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => GlobalStore.EMR_BASE_URI)), (object)GlobalStore.EMR_BASE_URI) + LogUtil.TraceData(LogUtil.GetMemberName<EmrTreatmentFilter>((Expression<Func<EmrTreatmentFilter>>)(() => filter)), (object)filter) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => baseUri)), (object)baseUri) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => "api/EmrTreatment/Get")), (object)"api/EmrTreatment/Get"));
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal EMR_TREATMENT GetByCode(string code)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			EMR_TREATMENT data = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(code))
				{
					CommonParam commonParam = new CommonParam();
					EmrTreatmentFilter filter = new EmrTreatmentFilter();
					filter.TREATMENT_CODE__EXACT = code;
					List<EMR_TREATMENT> rs = GlobalStore.EmrConsumer.Get<List<EMR_TREATMENT>>("api/EmrTreatment/Get", commonParam, filter, new object[0]);
					data = ((rs != null && rs.Count > 0) ? rs.FirstOrDefault() : null);
					if (data == null)
					{
						LogSystem.Warn("Tìm hồ sơ điều trị theo mã kết quả không tìm thấy hồ sơ nào____TreatmentCode:" + code + LogUtil.TraceData(LogUtil.GetMemberName<EMR_TREATMENT>((Expression<Func<EMR_TREATMENT>>)(() => data)), (object)data) + LogUtil.TraceData(LogUtil.GetMemberName<EmrTreatmentFilter>((Expression<Func<EmrTreatmentFilter>>)(() => filter)), (object)filter) + LogUtil.TraceData(LogUtil.GetMemberName<List<EMR_TREATMENT>>((Expression<Func<List<EMR_TREATMENT>>>)(() => rs)), (object)rs));
					}
				}
			}
			catch (Exception ex)
			{
				string baseUri = GlobalStore.EmrConsumer.GetBaseUri();
				LogSystem.Warn("EMR_TREATMENT GetByCode:" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => code)), (object)code) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => GlobalStore.EMR_BASE_URI)), (object)GlobalStore.EMR_BASE_URI) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => baseUri)), (object)baseUri) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => "api/EmrTreatment/Get")), (object)"api/EmrTreatment/Get"));
				LogSystem.Warn(ex);
			}
			return data;
		}
	}
}
