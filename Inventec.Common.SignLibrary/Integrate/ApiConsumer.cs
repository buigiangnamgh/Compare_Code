using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.DTO;

namespace Inventec.Common.Integrate
{
	// Token: 0x02000008 RID: 8
	public class ApiConsumer
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00005598 File Offset: 0x00003798
		public ApiConsumer(string baseUri, string applicationCode)
		{
			this.Init(baseUri, null, applicationCode);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000055F0 File Offset: 0x000037F0
		public ApiConsumer(string baseUri, string tokenCode, string applicationCode)
		{
			this.Init(baseUri, tokenCode, applicationCode);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005648 File Offset: 0x00003848
		public string GetBaseUri()
		{
			return this.baseUri;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005660 File Offset: 0x00003860
		private void Init(string baseUri, string tokenCode, string applicationCode)
		{
			this.baseUri = baseUri;
			this.token = tokenCode;
			this.applicationCode = applicationCode;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005678 File Offset: 0x00003878
		public void SetTokenCode(string tokenCode)
		{
			this.token = tokenCode;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005682 File Offset: 0x00003882
		public void SetBaseUri(string baseUri)
		{
			this.baseUri = baseUri;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000568C File Offset: 0x0000388C
		public T Get<T>(string uri, object commonParam, object filter, params object[] listParam)
		{
			return this.Get<T>(uri, commonParam, filter, 0, listParam);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000056AC File Offset: 0x000038AC
		public T Get<T>(string uri, object commonParam, object filter, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string text = "";
				this.HttpRequestBuilder(httpClient, uri, ref text, userTimeout, listParam);
				bool flag = filter != null || commonParam != null;
				if (flag)
				{
					ApiParam apiParam = new ApiParam();
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = filter;
					string arg = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvertUtil.SerializeObject(apiParam)));
					text += string.Format("{0}={1}", "param", arg);
				}
				HttpResponseMessage result2 = httpClient.GetAsync(text).Result;
				bool flag2 = !result2.IsSuccessStatusCode;
				if (flag2)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}{4}.", new object[]
					{
						httpClient.BaseAddress.AbsoluteUri,
						uri,
						result2.StatusCode.GetHashCode(),
						JsonConvertUtil.SerializeObject(filter),
						JsonConvertUtil.SerializeObject(commonParam)
					}));
				}
				string result3 = result2.Content.ReadAsStringAsync().Result;
				ApiResultObject<T> apiResultObject = JsonConvertUtil.DeserializeObject<ApiResultObject<T>>(result3);
				bool flag3 = apiResultObject != null;
				if (flag3)
				{
					result = apiResultObject.Data;
					commonParam = apiResultObject.Param;
				}
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005818 File Offset: 0x00003A18
		public T GetRO<T>(string uri, object commonParam, object filter, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string text = "";
				this.HttpRequestBuilder(httpClient, uri, ref text, userTimeout, listParam);
				bool flag = filter != null || commonParam != null;
				if (flag)
				{
					ApiParam apiParam = new ApiParam();
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = filter;
					string arg = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvertUtil.SerializeObject(apiParam)));
					text += string.Format("{0}={1}", "param", arg);
				}
				HttpResponseMessage result2 = httpClient.GetAsync(text).Result;
				bool flag2 = !result2.IsSuccessStatusCode;
				if (flag2)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}{4}.", new object[]
					{
						httpClient.BaseAddress.AbsoluteUri,
						uri,
						result2.StatusCode.GetHashCode(),
						JsonConvertUtil.SerializeObject(filter),
						JsonConvertUtil.SerializeObject(commonParam)
					}));
				}
				string result3 = result2.Content.ReadAsStringAsync().Result;
				result = JsonConvertUtil.DeserializeObject<T>(result3);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005968 File Offset: 0x00003B68
		[DebuggerStepThrough]
		public Task<T> GetAsync<T>(string uri, object commonParam, object filter, params object[] listParam)
		{
			ApiConsumer.<GetAsync>d__14<T> <GetAsync>d__ = new ApiConsumer.<GetAsync>d__14<T>();
			<GetAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<GetAsync>d__.<>4__this = this;
			<GetAsync>d__.uri = uri;
			<GetAsync>d__.commonParam = commonParam;
			<GetAsync>d__.filter = filter;
			<GetAsync>d__.listParam = listParam;
			<GetAsync>d__.<>1__state = -1;
			<GetAsync>d__.<>t__builder.Start<ApiConsumer.<GetAsync>d__14<T>>(ref <GetAsync>d__);
			return <GetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000059CC File Offset: 0x00003BCC
		[DebuggerStepThrough]
		public Task<T> GetAsync<T>(string uri, object commonParam, object filter, int userTimeout, params object[] listParam)
		{
			ApiConsumer.<GetAsync>d__15<T> <GetAsync>d__ = new ApiConsumer.<GetAsync>d__15<T>();
			<GetAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<GetAsync>d__.<>4__this = this;
			<GetAsync>d__.uri = uri;
			<GetAsync>d__.commonParam = commonParam;
			<GetAsync>d__.filter = filter;
			<GetAsync>d__.userTimeout = userTimeout;
			<GetAsync>d__.listParam = listParam;
			<GetAsync>d__.<>1__state = -1;
			<GetAsync>d__.<>t__builder.Start<ApiConsumer.<GetAsync>d__15<T>>(ref <GetAsync>d__);
			return <GetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005A38 File Offset: 0x00003C38
		public T Post<T>(string uri, CommonParam commonParam, object data, params object[] listParam)
		{
			return this.Post<T>(uri, commonParam, data, 0, listParam);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005A58 File Offset: 0x00003C58
		public T Post<T>(string uri, CommonParam commonParam, object data, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				LogSystem.Debug("Post.1");
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string>(() => uri), uri) + LogUtil.TraceData(LogUtil.GetMemberName<object>(() => data), data));
				string requestUri = "";
				this.HttpRequestBuilder(httpClient, uri, ref requestUri, userTimeout, listParam);
				ApiParam apiParam = new ApiParam();
				bool flag = data != null || commonParam != null;
				if (flag)
				{
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = data;
				}
				LogSystem.Debug("Post.2");
				HttpResponseMessage result2 = httpClient.PostAsJsonAsync(requestUri, apiParam).Result;
				bool flag2 = !result2.IsSuccessStatusCode;
				if (flag2)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}.", new object[]
					{
						httpClient.BaseAddress.AbsoluteUri,
						uri,
						result2.StatusCode.GetHashCode(),
						JsonConvertUtil.SerializeObject(data)
					}));
				}
				LogSystem.Debug("Post.3");
				string result3 = result2.Content.ReadAsStringAsync().Result;
				try
				{
					ApiResultObject<T> apiResultObject = JsonConvertUtil.DeserializeObject<ApiResultObject<T>>(result3);
					bool flag3 = apiResultObject != null;
					if (flag3)
					{
						result = apiResultObject.Data;
						commonParam = apiResultObject.Param;
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				LogSystem.Debug("Post.4");
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005C98 File Offset: 0x00003E98
		public T PostRO<T>(string uri, CommonParam commonParam, object data, params object[] listParam)
		{
			return this.PostRO<T>(uri, commonParam, data, 0, new object[0]);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005CBC File Offset: 0x00003EBC
		public T PostRO<T>(string uri, CommonParam commonParam, object data, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string requestUri = "";
				this.HttpRequestBuilder(httpClient, uri, ref requestUri, userTimeout, listParam);
				ApiParam apiParam = new ApiParam();
				bool flag = data != null || commonParam != null;
				if (flag)
				{
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = data;
				}
				HttpResponseMessage result2 = httpClient.PostAsJsonAsync(requestUri, apiParam).Result;
				bool flag2 = !result2.IsSuccessStatusCode;
				if (flag2)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}.", new object[]
					{
						httpClient.BaseAddress.AbsoluteUri,
						uri,
						result2.StatusCode.GetHashCode(),
						JsonConvertUtil.SerializeObject(data)
					}));
				}
				string result3 = result2.Content.ReadAsStringAsync().Result;
				try
				{
					result = JsonConvertUtil.DeserializeObject<T>(result3);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005DDC File Offset: 0x00003FDC
		public T PostWithouApiParam<T>(string uri, object data, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string requestUri = "";
				this.HttpRequestBuilder(httpClient, uri, ref requestUri, userTimeout, listParam);
				HttpResponseMessage result2 = httpClient.PostAsJsonAsync(requestUri, data).Result;
				bool flag = !result2.IsSuccessStatusCode;
				if (flag)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", this.baseUri, uri, result2.StatusCode.GetHashCode()));
				}
				string result3 = result2.Content.ReadAsStringAsync().Result;
				Console.WriteLine(string.Format("responseData: {0}", result3));
				result = JsonConvertUtil.DeserializeObject<T>(result3);
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005EB0 File Offset: 0x000040B0
		[DebuggerStepThrough]
		public Task<T> PostWithouApiParamAsync<T>(string uri, object data, int userTimeout, params object[] listParam)
		{
			ApiConsumer.<PostWithouApiParamAsync>d__21<T> <PostWithouApiParamAsync>d__ = new ApiConsumer.<PostWithouApiParamAsync>d__21<T>();
			<PostWithouApiParamAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<PostWithouApiParamAsync>d__.<>4__this = this;
			<PostWithouApiParamAsync>d__.uri = uri;
			<PostWithouApiParamAsync>d__.data = data;
			<PostWithouApiParamAsync>d__.userTimeout = userTimeout;
			<PostWithouApiParamAsync>d__.listParam = listParam;
			<PostWithouApiParamAsync>d__.<>1__state = -1;
			<PostWithouApiParamAsync>d__.<>t__builder.Start<ApiConsumer.<PostWithouApiParamAsync>d__21<T>>(ref <PostWithouApiParamAsync>d__);
			return <PostWithouApiParamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005F14 File Offset: 0x00004114
		[DebuggerStepThrough]
		public Task<T> PostAsync<T>(string uri, object commonParam, object data, params object[] listParam)
		{
			ApiConsumer.<PostAsync>d__22<T> <PostAsync>d__ = new ApiConsumer.<PostAsync>d__22<T>();
			<PostAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<PostAsync>d__.<>4__this = this;
			<PostAsync>d__.uri = uri;
			<PostAsync>d__.commonParam = commonParam;
			<PostAsync>d__.data = data;
			<PostAsync>d__.listParam = listParam;
			<PostAsync>d__.<>1__state = -1;
			<PostAsync>d__.<>t__builder.Start<ApiConsumer.<PostAsync>d__22<T>>(ref <PostAsync>d__);
			return <PostAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005F78 File Offset: 0x00004178
		[DebuggerStepThrough]
		public Task<T> PostAsync<T>(string uri, object commonParam, object data, int userTimeout, params object[] listParam)
		{
			ApiConsumer.<PostAsync>d__23<T> <PostAsync>d__ = new ApiConsumer.<PostAsync>d__23<T>();
			<PostAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<PostAsync>d__.<>4__this = this;
			<PostAsync>d__.uri = uri;
			<PostAsync>d__.commonParam = commonParam;
			<PostAsync>d__.data = data;
			<PostAsync>d__.userTimeout = userTimeout;
			<PostAsync>d__.listParam = listParam;
			<PostAsync>d__.<>1__state = -1;
			<PostAsync>d__.<>t__builder.Start<ApiConsumer.<PostAsync>d__23<T>>(ref <PostAsync>d__);
			return <PostAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005FE4 File Offset: 0x000041E4
		public T PostWithFile<T>(string uri, object commonParam, object data, List<FileHolder> files, params object[] listParam)
		{
			return this.PostWithFile<T>(uri, commonParam, data, files, 0, listParam);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006004 File Offset: 0x00004204
		public T PostWithFile<T>(string uri, object commonParam, object data, List<FileHolder> files, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string text = "";
				this.HttpRequestBuilder(httpClient, uri, ref text, userTimeout, listParam);
				ApiParam apiParam = new ApiParam();
				bool flag = data != null || commonParam != null;
				if (flag)
				{
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = data;
				}
				using (MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent())
				{
					foreach (FileHolder fileHolder in files)
					{
						multipartFormDataContent.Add(new StreamContent(fileHolder.Content), "File", fileHolder.FileName);
					}
					HttpContent httpContent = new StringContent(JsonConvertUtil.SerializeObject(apiParam));
					multipartFormDataContent.Add(httpContent, "Data", "Data");
					using (HttpResponseMessage result2 = httpClient.PostAsync(text, multipartFormDataContent).Result)
					{
						bool flag2 = !result2.IsSuccessStatusCode;
						if (flag2)
						{
							LogSystem.Error(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", this.baseUri, uri, result2.StatusCode.GetHashCode()));
						}
						string result3 = result2.Content.ReadAsStringAsync().Result;
						result = JsonConvertUtil.DeserializeObject<T>(result3);
					}
				}
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000061E4 File Offset: 0x000043E4
		private void HttpRequestBuilder(HttpClient client, string uri, ref string requestedUrl, int userTimeout, params object[] listParam)
		{
			client.DefaultRequestHeaders.Add("TokenCode", this.token);
			client.DefaultRequestHeaders.Add("ApplicationCode", this.applicationCode);
			client.BaseAddress = new Uri(this.baseUri);
			client.DefaultRequestHeaders.Accept.Clear();
			client.Timeout = new TimeSpan(0, 0, (userTimeout > 0) ? userTimeout : this.TIME_OUT);
			requestedUrl = string.Format("{0}?", uri);
			bool flag = listParam != null && listParam.Length != 0;
			if (flag)
			{
				bool flag2 = listParam.Length % 2 != 0;
				if (flag2)
				{
					throw new ArgumentException("Danh sach param khong hop le. So luong param phai la so chan");
				}
				for (int i = 0; i < listParam.Length; i += 2)
				{
					string str = requestedUrl;
					string format = "{0}={1}&";
					object obj = listParam[i];
					object arg = HttpUtility.UrlEncode(((obj != null) ? obj.ToString() : null) ?? "");
					object obj2 = listParam[i + 1];
					requestedUrl = str + string.Format(format, arg, HttpUtility.UrlEncode(((obj2 != null) ? obj2.ToString() : null) ?? ""));
				}
			}
		}

		// Token: 0x04000024 RID: 36
		private const string API_PARAM = "param";

		// Token: 0x04000025 RID: 37
		private int TIME_OUT = int.Parse(ConfigurationManager.AppSettings["Inventec.Common.WebApiClient.Timeout"] ?? "60");

		// Token: 0x04000026 RID: 38
		private string baseUri = null;

		// Token: 0x04000027 RID: 39
		private string token = null;

		// Token: 0x04000028 RID: 40
		private string applicationCode = null;
	}
}
