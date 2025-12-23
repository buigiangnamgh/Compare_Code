using System;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Inventec.Common.Logging;
using Newtonsoft.Json;

namespace Inventec.Common.Integrate
{
	public class BackendAdapter : EntityBaseAdapter
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass15_0<T>
		{
			public object filter;

			public CommonParam commonParam;

			public object[] listParam;

			public int userTimeout;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass21_0<T>
		{
			public object filter;

			public CommonParam commonParam;

			public object[] listParam;

			public int userTimeout;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass22_0<T>
		{
			public object filter;

			public CommonParam commonParam;

			public object[] listParam;

			public int userTimeout;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass28_0<T>
		{
			public object filter;

			public CommonParam commonParam;

			public object[] listParam;

			public int userTimeout;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass34_0<T>
		{
			public object data;

			public CommonParam commonParam;

			public object[] listParam;

			public int userTimeout;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass40_0<T>
		{
			public object data;

			public CommonParam commonParam;

			public object[] listParam;

			public int userTimeout;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass46_0<T>
		{
			public object data;

			public CommonParam commonParam;

			public object[] listParam;

			public int userTimeout;
		}

		private string errorFormat = "Call API \"{0}/{1}\":";

		private static string STR_CANNOT_CONNECT_TO_SERVER = "Không kết nối được máy chủ";

		private static string STR_SESSION_TIMEOUT = "Phiên làm việc đã hết hiệu lực";

		private static string LanguageCode = "VI";

		protected CommonParam param { get; set; }

		public BackendAdapter()
		{
			param = new CommonParam();
		}

		public BackendAdapter(CommonParam paramBusiness)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
		}

		public T Get<T>(string requestUri, ApiConsumer consumer, object filter, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Get<T>(requestUri, consumer, commonParam, filter, 0, null, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Get<T>(string requestUri, ApiConsumer consumer, object filter, int userTimeout, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Get<T>(requestUri, consumer, commonParam, filter, userTimeout, null, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Get<T>(string requestUri, ApiConsumer consumer, object filter, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Get<T>(requestUri, consumer, commonParam, filter, 0, action, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Get<T>(string requestUri, ApiConsumer consumer, object filter, int userTimeout, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Get<T>(requestUri, consumer, commonParam, filter, userTimeout, action, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Get<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object filter, Action action, params object[] listParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Get<T>(requestUri, consumer, commonParam, filter, 0, action, listParam);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Get<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object filter, int userTimeout, Action action, params object[] listParam)
		{
			_003C_003Ec__DisplayClass15_0<T> CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass15_0<T>();
			CS_0024_003C_003E8__locals23.filter = filter;
			CS_0024_003C_003E8__locals23.commonParam = commonParam;
			CS_0024_003C_003E8__locals23.listParam = listParam;
			CS_0024_003C_003E8__locals23.userTimeout = userTimeout;
			T val = default(T);
			try
			{
				ApiResultObject<T> apiResultObject = null;
				if (CS_0024_003C_003E8__locals23.commonParam != null)
				{
					CS_0024_003C_003E8__locals23.commonParam.LanguageCode = LanguageCode;
				}
				apiResultObject = ((CS_0024_003C_003E8__locals23.listParam == null || CS_0024_003C_003E8__locals23.listParam.Length == 0) ? consumer.Get<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, new object[0]) : consumer.Get<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, CS_0024_003C_003E8__locals23.listParam));
				if (apiResultObject != null)
				{
					if (apiResultObject.Param != null)
					{
						param.Messages.AddRange(apiResultObject.Param.Messages);
						param.BugCodes.AddRange(apiResultObject.Param.BugCodes);
					}
					val = apiResultObject.Data;
				}
				if (apiResultObject == null || !apiResultObject.Success || val == null)
				{
					base.Input = LogUtil.TraceData(LogUtil.GetMemberName<object>((Expression<Func<object>>)(() => CS_0024_003C_003E8__locals23.filter)), CS_0024_003C_003E8__locals23.filter) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass15_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass15_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.commonParam) + LogUtil.TraceData(LogUtil.GetMemberName<object[]>(Expression.Lambda<Func<object[]>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass15_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass15_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.listParam) + LogUtil.TraceData(LogUtil.GetMemberName<int>(Expression.Lambda<Func<int>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass15_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass15_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.userTimeout);
					base.ErrorFormat = string.Format(errorFormat, consumer.GetBaseUri(), requestUri);
					LogInOut(JsonConvert.SerializeObject((object)apiResultObject), LogType.Error);
				}
			}
			catch (ApiException ex)
			{
				ApiException ex2 = ex;
				ApiException ex3 = ex2;
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<HttpStatusCode>((Expression<Func<HttpStatusCode>>)(() => ex3.StatusCode)), (object)ex3.StatusCode));
				if (ex3.StatusCode == HttpStatusCode.NotFound)
				{
					param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
				}
				else if (ex3.StatusCode == HttpStatusCode.Unauthorized)
				{
					param.Messages.Add(STR_SESSION_TIMEOUT);
					param.HasException = true;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (AggregateException ex4)
			{
				LogSystem.Error((Exception)ex4);
				param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
			}
			catch (Exception ex5)
			{
				LogSystem.Error(ex5);
			}
			return val;
		}

		public async Task<T> GetAsync<T>(string requestUri, ApiConsumer consumer, object filter, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await GetAsync<T>(requestUri, consumer, commonParam, filter, 0, null, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> GetAsync<T>(string requestUri, ApiConsumer consumer, object filter, int userTimeout, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await GetAsync<T>(requestUri, consumer, commonParam, filter, userTimeout, null, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> GetAsync<T>(string requestUri, ApiConsumer consumer, object filter, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await GetAsync<T>(requestUri, consumer, commonParam, filter, 0, action, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> GetAsync<T>(string requestUri, ApiConsumer consumer, object filter, int userTimeout, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await GetAsync<T>(requestUri, consumer, commonParam, filter, userTimeout, action, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> GetAsync<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object filter, Action action, params object[] listParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await GetAsync<T>(requestUri, consumer, commonParam, filter, 0, action, listParam);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> GetAsync<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object filter, int userTimeout, Action action, params object[] listParam)
		{
			_003C_003Ec__DisplayClass21_0<T> CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass21_0<T>();
			CS_0024_003C_003E8__locals23.filter = filter;
			CS_0024_003C_003E8__locals23.commonParam = commonParam;
			CS_0024_003C_003E8__locals23.listParam = listParam;
			CS_0024_003C_003E8__locals23.userTimeout = userTimeout;
			T result = default(T);
			try
			{
				if (CS_0024_003C_003E8__locals23.commonParam != null)
				{
					CS_0024_003C_003E8__locals23.commonParam.LanguageCode = LanguageCode;
				}
				ApiResultObject<T> rs = ((CS_0024_003C_003E8__locals23.listParam == null || CS_0024_003C_003E8__locals23.listParam.Length == 0) ? (await consumer.GetAsync<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, new object[0]).ConfigureAwait(false)) : (await consumer.GetAsync<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, CS_0024_003C_003E8__locals23.listParam).ConfigureAwait(false)));
				if (rs != null)
				{
					if (rs.Param != null)
					{
						param.Messages.AddRange(rs.Param.Messages);
						param.BugCodes.AddRange(rs.Param.BugCodes);
					}
					result = rs.Data;
				}
				if (rs == null || !rs.Success || result == null)
				{
					base.Input = LogUtil.TraceData(LogUtil.GetMemberName<object>((Expression<Func<object>>)(() => CS_0024_003C_003E8__locals23.filter)), CS_0024_003C_003E8__locals23.filter) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass21_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass21_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.commonParam) + LogUtil.TraceData(LogUtil.GetMemberName<object[]>(Expression.Lambda<Func<object[]>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass21_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass21_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.listParam) + LogUtil.TraceData(LogUtil.GetMemberName<int>(Expression.Lambda<Func<int>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass21_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass21_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.userTimeout);
					base.ErrorFormat = string.Format(errorFormat, consumer.GetBaseUri(), requestUri);
					LogInOut(JsonConvert.SerializeObject((object)rs), LogType.Error);
				}
			}
			catch (ApiException ex)
			{
				ApiException ex2 = ex;
				ApiException ex3 = ex2;
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<HttpStatusCode>((Expression<Func<HttpStatusCode>>)(() => ex3.StatusCode)), (object)ex3.StatusCode));
				if (ex3.StatusCode == HttpStatusCode.NotFound)
				{
					param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
				}
				else if (ex3.StatusCode == HttpStatusCode.Unauthorized)
				{
					param.Messages.Add(STR_SESSION_TIMEOUT);
					param.HasException = true;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (AggregateException ex4)
			{
				AggregateException ex5 = ex4;
				LogSystem.Error((Exception)ex5);
				param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
			}
			catch (Exception ex6)
			{
				Exception ex7 = ex6;
				LogSystem.Error(ex7);
			}
			return result;
		}

		public async Task<ApiResultObject<T>> GetAsyncRO<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object filter, int userTimeout, Action action, params object[] listParam)
		{
			_003C_003Ec__DisplayClass22_0<T> CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass22_0<T>();
			CS_0024_003C_003E8__locals23.filter = filter;
			CS_0024_003C_003E8__locals23.commonParam = commonParam;
			CS_0024_003C_003E8__locals23.listParam = listParam;
			CS_0024_003C_003E8__locals23.userTimeout = userTimeout;
			ApiResultObject<T> rs = null;
			try
			{
				if (CS_0024_003C_003E8__locals23.commonParam != null)
				{
					CS_0024_003C_003E8__locals23.commonParam.LanguageCode = LanguageCode;
				}
				rs = ((CS_0024_003C_003E8__locals23.listParam == null || CS_0024_003C_003E8__locals23.listParam.Length == 0) ? (await consumer.GetAsync<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, new object[0]).ConfigureAwait(false)) : (await consumer.GetAsync<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, CS_0024_003C_003E8__locals23.listParam).ConfigureAwait(false)));
				if (rs != null && rs.Param != null)
				{
					param.Messages.AddRange(rs.Param.Messages);
					param.BugCodes.AddRange(rs.Param.BugCodes);
				}
				if (rs == null || !rs.Success)
				{
					base.Input = LogUtil.TraceData(LogUtil.GetMemberName<object>((Expression<Func<object>>)(() => CS_0024_003C_003E8__locals23.filter)), CS_0024_003C_003E8__locals23.filter) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass22_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass22_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.commonParam) + LogUtil.TraceData(LogUtil.GetMemberName<object[]>(Expression.Lambda<Func<object[]>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass22_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass22_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.listParam) + LogUtil.TraceData(LogUtil.GetMemberName<int>(Expression.Lambda<Func<int>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass22_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass22_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.userTimeout);
					base.ErrorFormat = string.Format(errorFormat, consumer.GetBaseUri(), requestUri);
					LogInOut(JsonConvert.SerializeObject((object)rs), LogType.Error);
				}
			}
			catch (ApiException ex)
			{
				ApiException ex2 = ex;
				ApiException ex3 = ex2;
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<HttpStatusCode>((Expression<Func<HttpStatusCode>>)(() => ex3.StatusCode)), (object)ex3.StatusCode));
				if (ex3.StatusCode == HttpStatusCode.NotFound)
				{
					param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
				}
				else if (ex3.StatusCode == HttpStatusCode.Unauthorized)
				{
					param.Messages.Add(STR_SESSION_TIMEOUT);
					param.HasException = true;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (AggregateException ex4)
			{
				AggregateException ex5 = ex4;
				LogSystem.Error((Exception)ex5);
				param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
			}
			catch (Exception ex6)
			{
				Exception ex7 = ex6;
				LogSystem.Error(ex7);
			}
			return rs;
		}

		public ApiResultObject<T> GetRO<T>(string requestUri, ApiConsumer consumer, object filter, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = GetRO<T>(requestUri, consumer, commonParam, filter, 0, null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> GetRO<T>(string requestUri, ApiConsumer consumer, object filter, int userTimeout, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = GetRO<T>(requestUri, consumer, commonParam, filter, userTimeout, null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> GetRO<T>(string requestUri, ApiConsumer consumer, object filter, Action action, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = GetRO<T>(requestUri, consumer, commonParam, filter, 0, action, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> GetRO<T>(string requestUri, ApiConsumer consumer, object filter, int userTimeout, Action action, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = GetRO<T>(requestUri, consumer, commonParam, filter, userTimeout, action, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> GetRO<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object filter, Action action, params object[] listParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = GetRO<T>(requestUri, consumer, commonParam, filter, 0, action, listParam);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> GetRO<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object filter, int userTimeout, Action action, params object[] listParam)
		{
			_003C_003Ec__DisplayClass28_0<T> CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass28_0<T>();
			CS_0024_003C_003E8__locals23.filter = filter;
			CS_0024_003C_003E8__locals23.commonParam = commonParam;
			CS_0024_003C_003E8__locals23.listParam = listParam;
			CS_0024_003C_003E8__locals23.userTimeout = userTimeout;
			ApiResultObject<T> apiResultObject = null;
			try
			{
				if (CS_0024_003C_003E8__locals23.commonParam != null)
				{
					CS_0024_003C_003E8__locals23.commonParam.LanguageCode = LanguageCode;
				}
				apiResultObject = ((CS_0024_003C_003E8__locals23.listParam == null || CS_0024_003C_003E8__locals23.listParam.Length == 0) ? consumer.Get<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, new object[0]) : consumer.Get<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.filter, CS_0024_003C_003E8__locals23.userTimeout, CS_0024_003C_003E8__locals23.listParam));
				if (apiResultObject != null && apiResultObject.Param != null)
				{
					param.Messages.AddRange(apiResultObject.Param.Messages);
					param.BugCodes.AddRange(apiResultObject.Param.BugCodes);
				}
				if (apiResultObject == null || !apiResultObject.Success || apiResultObject.Data == null)
				{
					base.Input = LogUtil.TraceData(LogUtil.GetMemberName<object>((Expression<Func<object>>)(() => CS_0024_003C_003E8__locals23.filter)), CS_0024_003C_003E8__locals23.filter) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass28_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass28_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.commonParam) + LogUtil.TraceData(LogUtil.GetMemberName<object[]>(Expression.Lambda<Func<object[]>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass28_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass28_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.listParam) + LogUtil.TraceData(LogUtil.GetMemberName<int>(Expression.Lambda<Func<int>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass28_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass28_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.userTimeout);
					base.ErrorFormat = string.Format(errorFormat, consumer.GetBaseUri(), requestUri);
					LogInOut(JsonConvert.SerializeObject((object)apiResultObject), LogType.Error);
				}
			}
			catch (ApiException ex)
			{
				ApiException ex2 = ex;
				ApiException ex3 = ex2;
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<HttpStatusCode>((Expression<Func<HttpStatusCode>>)(() => ex3.StatusCode)), (object)ex3.StatusCode));
				if (ex3.StatusCode == HttpStatusCode.NotFound)
				{
					param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
				}
				else if (ex3.StatusCode == HttpStatusCode.Unauthorized)
				{
					param.Messages.Add(STR_SESSION_TIMEOUT);
					param.HasException = true;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (AggregateException ex4)
			{
				LogSystem.Error((Exception)ex4);
				param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
			}
			catch (Exception ex5)
			{
				LogSystem.Error(ex5);
			}
			return apiResultObject;
		}

		public T Post<T>(string requestUri, ApiConsumer consumer, object data, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Post<T>(requestUri, consumer, commonParam, data, 0, null, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Post<T>(string requestUri, ApiConsumer consumer, object data, int userTimeout, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Post<T>(requestUri, consumer, commonParam, data, userTimeout, null, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Post<T>(string requestUri, ApiConsumer consumer, object data, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Post<T>(requestUri, consumer, commonParam, data, 0, action, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Post<T>(string requestUri, ApiConsumer consumer, object data, int userTimeout, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Post<T>(requestUri, consumer, commonParam, data, userTimeout, action, null);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Post<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object data, Action action, params object[] listParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = Post<T>(requestUri, consumer, commonParam, data, 0, action, listParam);
				return result;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public T Post<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object data, int userTimeout, Action action, params object[] listParam)
		{
			_003C_003Ec__DisplayClass34_0<T> CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass34_0<T>();
			CS_0024_003C_003E8__locals23.data = data;
			CS_0024_003C_003E8__locals23.commonParam = commonParam;
			CS_0024_003C_003E8__locals23.listParam = listParam;
			CS_0024_003C_003E8__locals23.userTimeout = userTimeout;
			T val = default(T);
			try
			{
				ApiResultObject<T> apiResultObject = null;
				if (CS_0024_003C_003E8__locals23.commonParam != null)
				{
					CS_0024_003C_003E8__locals23.commonParam.LanguageCode = LanguageCode;
				}
				apiResultObject = ((CS_0024_003C_003E8__locals23.listParam == null || CS_0024_003C_003E8__locals23.listParam.Length == 0) ? consumer.Post<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.data, CS_0024_003C_003E8__locals23.userTimeout, new object[0]) : consumer.Post<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.data, CS_0024_003C_003E8__locals23.userTimeout, CS_0024_003C_003E8__locals23.listParam));
				if (apiResultObject != null)
				{
					if (apiResultObject.Param != null)
					{
						param.Messages.AddRange(apiResultObject.Param.Messages);
						param.BugCodes.AddRange(apiResultObject.Param.BugCodes);
					}
					val = apiResultObject.Data;
				}
				if (apiResultObject == null || !apiResultObject.Success || val == null)
				{
					base.Input = LogUtil.TraceData(LogUtil.GetMemberName<object>((Expression<Func<object>>)(() => CS_0024_003C_003E8__locals23.data)), CS_0024_003C_003E8__locals23.data) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass34_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass34_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.commonParam) + LogUtil.TraceData(LogUtil.GetMemberName<object[]>(Expression.Lambda<Func<object[]>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass34_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass34_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.listParam) + LogUtil.TraceData(LogUtil.GetMemberName<int>(Expression.Lambda<Func<int>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass34_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass34_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.userTimeout);
					base.ErrorFormat = string.Format(errorFormat, consumer.GetBaseUri(), requestUri);
					LogInOut(JsonConvert.SerializeObject((object)apiResultObject), LogType.Error);
				}
			}
			catch (ApiException ex)
			{
				ApiException ex2 = ex;
				ApiException ex3 = ex2;
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<HttpStatusCode>((Expression<Func<HttpStatusCode>>)(() => ex3.StatusCode)), (object)ex3.StatusCode));
				if (ex3.StatusCode == HttpStatusCode.NotFound)
				{
					param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
				}
				else if (ex3.StatusCode == HttpStatusCode.Unauthorized)
				{
					param.Messages.Add(STR_SESSION_TIMEOUT);
					param.HasException = true;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (AggregateException ex4)
			{
				LogSystem.Error((Exception)ex4);
				param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
			}
			catch (Exception ex5)
			{
				LogSystem.Error(ex5);
			}
			return val;
		}

		public ApiResultObject<T> PostRO<T>(string requestUri, ApiConsumer consumer, object data, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = PostRO<T>(requestUri, consumer, commonParam, data, 0, null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> PostRO<T>(string requestUri, ApiConsumer consumer, object data, int userTimeout, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = PostRO<T>(requestUri, consumer, commonParam, data, userTimeout, null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> PostRO<T>(string requestUri, ApiConsumer consumer, object data, Action action, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = PostRO<T>(requestUri, consumer, commonParam, data, 0, action, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> PostRO<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object data, Action action, params object[] listParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = PostRO<T>(requestUri, consumer, commonParam, data, 0, action, listParam);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> PostRO<T>(string requestUri, ApiConsumer consumer, object data, int userTimeout, Action action, CommonParam commonParam)
		{
			ApiResultObject<T> result = null;
			try
			{
				base.FrameIndex = 1;
				result = PostRO<T>(requestUri, consumer, commonParam, data, userTimeout, action, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		public ApiResultObject<T> PostRO<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object data, int userTimeout, Action action, params object[] listParam)
		{
			_003C_003Ec__DisplayClass40_0<T> CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass40_0<T>();
			CS_0024_003C_003E8__locals23.data = data;
			CS_0024_003C_003E8__locals23.commonParam = commonParam;
			CS_0024_003C_003E8__locals23.listParam = listParam;
			CS_0024_003C_003E8__locals23.userTimeout = userTimeout;
			ApiResultObject<T> apiResultObject = null;
			try
			{
				if (CS_0024_003C_003E8__locals23.commonParam != null)
				{
					CS_0024_003C_003E8__locals23.commonParam.LanguageCode = LanguageCode;
				}
				apiResultObject = ((CS_0024_003C_003E8__locals23.listParam == null || CS_0024_003C_003E8__locals23.listParam.Length == 0) ? consumer.Post<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.data, CS_0024_003C_003E8__locals23.userTimeout, new object[0]) : consumer.Post<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.data, CS_0024_003C_003E8__locals23.userTimeout, CS_0024_003C_003E8__locals23.listParam));
				if (apiResultObject != null && apiResultObject.Param != null)
				{
					param.Messages.AddRange(apiResultObject.Param.Messages);
					param.BugCodes.AddRange(apiResultObject.Param.BugCodes);
				}
				if (apiResultObject == null || !apiResultObject.Success || apiResultObject.Data == null)
				{
					base.Input = LogUtil.TraceData(LogUtil.GetMemberName<object>((Expression<Func<object>>)(() => CS_0024_003C_003E8__locals23.data)), CS_0024_003C_003E8__locals23.data) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass40_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass40_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.commonParam) + LogUtil.TraceData(LogUtil.GetMemberName<object[]>(Expression.Lambda<Func<object[]>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass40_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass40_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.listParam) + LogUtil.TraceData(LogUtil.GetMemberName<int>(Expression.Lambda<Func<int>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass40_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass40_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.userTimeout);
					base.ErrorFormat = string.Format(errorFormat, consumer.GetBaseUri(), requestUri);
					LogInOut(JsonConvert.SerializeObject((object)apiResultObject), LogType.Error);
				}
			}
			catch (ApiException ex)
			{
				ApiException ex2 = ex;
				ApiException ex3 = ex2;
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<HttpStatusCode>((Expression<Func<HttpStatusCode>>)(() => ex3.StatusCode)), (object)ex3.StatusCode));
				if (ex3.StatusCode == HttpStatusCode.NotFound)
				{
					param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
				}
				else if (ex3.StatusCode == HttpStatusCode.Unauthorized)
				{
					param.Messages.Add(STR_SESSION_TIMEOUT);
					param.HasException = true;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (AggregateException ex4)
			{
				LogSystem.Error((Exception)ex4);
				param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
			}
			catch (Exception ex5)
			{
				LogSystem.Error(ex5);
			}
			return apiResultObject;
		}

		public async Task<T> PostAsync<T>(string requestUri, ApiConsumer consumer, object data, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await PostAsync<T>(requestUri, consumer, commonParam, data, 0, null, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> PostAsync<T>(string requestUri, ApiConsumer consumer, object data, int userTimeout, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await PostAsync<T>(requestUri, consumer, commonParam, data, userTimeout, null, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> PostAsync<T>(string requestUri, ApiConsumer consumer, object data, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await PostAsync<T>(requestUri, consumer, commonParam, data, 0, action, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> PostAsync<T>(string requestUri, ApiConsumer consumer, object data, int userTimeout, Action action, CommonParam commonParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await PostAsync<T>(requestUri, consumer, commonParam, data, userTimeout, action, null);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> PostAsync<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object data, Action action, params object[] listParam)
		{
			T result = default(T);
			try
			{
				base.FrameIndex = 1;
				result = await PostAsync<T>(requestUri, consumer, commonParam, data, 0, action, listParam);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return result;
		}

		public async Task<T> PostAsync<T>(string requestUri, ApiConsumer consumer, CommonParam commonParam, object data, int userTimeout, Action action, params object[] listParam)
		{
			_003C_003Ec__DisplayClass46_0<T> CS_0024_003C_003E8__locals23 = new _003C_003Ec__DisplayClass46_0<T>();
			CS_0024_003C_003E8__locals23.data = data;
			CS_0024_003C_003E8__locals23.commonParam = commonParam;
			CS_0024_003C_003E8__locals23.listParam = listParam;
			CS_0024_003C_003E8__locals23.userTimeout = userTimeout;
			T result = default(T);
			try
			{
				if (CS_0024_003C_003E8__locals23.commonParam != null)
				{
					CS_0024_003C_003E8__locals23.commonParam.LanguageCode = LanguageCode;
				}
				ApiResultObject<T> rs = ((CS_0024_003C_003E8__locals23.listParam == null || CS_0024_003C_003E8__locals23.listParam.Length == 0) ? (await consumer.PostAsync<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.data, CS_0024_003C_003E8__locals23.userTimeout, new object[0])) : (await consumer.PostAsync<ApiResultObject<T>>(requestUri, CS_0024_003C_003E8__locals23.commonParam, CS_0024_003C_003E8__locals23.data, CS_0024_003C_003E8__locals23.userTimeout, CS_0024_003C_003E8__locals23.listParam)));
				if (rs != null)
				{
					if (rs.Param != null)
					{
						param.Messages.AddRange(rs.Param.Messages);
						param.BugCodes.AddRange(rs.Param.BugCodes);
					}
					result = rs.Data;
				}
				if (rs == null || !rs.Success || result == null)
				{
					base.Input = LogUtil.TraceData(LogUtil.GetMemberName<object>((Expression<Func<object>>)(() => CS_0024_003C_003E8__locals23.data)), CS_0024_003C_003E8__locals23.data) + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>(Expression.Lambda<Func<CommonParam>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass46_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass46_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.commonParam) + LogUtil.TraceData(LogUtil.GetMemberName<object[]>(Expression.Lambda<Func<object[]>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass46_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass46_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.listParam) + LogUtil.TraceData(LogUtil.GetMemberName<int>(Expression.Lambda<Func<int>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals23, typeof(_003C_003Ec__DisplayClass46_0<T>)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass46_0<T>).TypeHandle)), new ParameterExpression[0])), (object)CS_0024_003C_003E8__locals23.userTimeout);
					base.ErrorFormat = string.Format(errorFormat, consumer.GetBaseUri(), requestUri);
					LogInOut(JsonConvert.SerializeObject((object)result), LogType.Error);
				}
			}
			catch (ApiException ex)
			{
				ApiException ex2 = ex;
				ApiException ex3 = ex2;
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<HttpStatusCode>((Expression<Func<HttpStatusCode>>)(() => ex3.StatusCode)), (object)ex3.StatusCode));
				if (ex3.StatusCode == HttpStatusCode.NotFound)
				{
					param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
				}
				else if (ex3.StatusCode == HttpStatusCode.Unauthorized)
				{
					param.Messages.Add(STR_SESSION_TIMEOUT);
					param.HasException = true;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (AggregateException ex4)
			{
				AggregateException ex5 = ex4;
				LogSystem.Error((Exception)ex5);
				param.Messages.Add(STR_CANNOT_CONNECT_TO_SERVER);
			}
			catch (Exception ex6)
			{
				Exception ex7 = ex6;
				LogSystem.Error(ex7);
			}
			return result;
		}
	}
}
