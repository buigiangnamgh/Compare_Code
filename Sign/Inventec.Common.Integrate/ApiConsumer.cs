using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.DTO;

namespace Inventec.Common.Integrate
{
	public class ApiConsumer
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass17_0<T>
		{
			public string uri;

			public object data;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClassb<T>
		{
			public _003C_003Ec__DisplayClass17_0<T> CS_0024_003C_003E8__locals11;
		}

		private const string API_PARAM = "param";

		private int TIME_OUT = int.Parse(ConfigurationManager.AppSettings["Inventec.Common.WebApiClient.Timeout"] ?? "60");

		private string baseUri = null;

		private string token = null;

		private string applicationCode = null;

		public ApiConsumer(string baseUri, string applicationCode)
		{
			Init(baseUri, null, applicationCode);
		}

		public ApiConsumer(string baseUri, string tokenCode, string applicationCode)
		{
			Init(baseUri, tokenCode, applicationCode);
		}

		public string GetBaseUri()
		{
			return baseUri;
		}

		private void Init(string baseUri, string tokenCode, string applicationCode)
		{
			this.baseUri = baseUri;
			token = tokenCode;
			this.applicationCode = applicationCode;
		}

		public void SetTokenCode(string tokenCode)
		{
			token = tokenCode;
		}

		public void SetBaseUri(string baseUri)
		{
			this.baseUri = baseUri;
		}

		public T Get<T>(string uri, object commonParam, object filter, params object[] listParam)
		{
			return Get<T>(uri, commonParam, filter, 0, listParam);
		}

		public T Get<T>(string uri, object commonParam, object filter, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(httpClient, uri, ref requestedUrl, userTimeout, listParam);
				if (filter != null || commonParam != null)
				{
					ApiParam apiParam = new ApiParam();
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = filter;
					string arg = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvertUtil.SerializeObject(apiParam)));
					requestedUrl += string.Format("{0}={1}", "param", arg);
				}
				HttpResponseMessage result2 = httpClient.GetAsync(requestedUrl).Result;
				if (!result2.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}{4}.", httpClient.BaseAddress.AbsoluteUri, uri, result2.StatusCode.GetHashCode(), JsonConvertUtil.SerializeObject(filter), JsonConvertUtil.SerializeObject(commonParam)));
				}
				string result3 = result2.Content.ReadAsStringAsync().Result;
				ApiResultObject<T> apiResultObject = JsonConvertUtil.DeserializeObject<ApiResultObject<T>>(result3);
				if (apiResultObject != null)
				{
					result = apiResultObject.Data;
					commonParam = apiResultObject.Param;
				}
			}
			return result;
		}

		public T GetRO<T>(string uri, object commonParam, object filter, int userTimeout, params object[] listParam)
		{
			T val = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(httpClient, uri, ref requestedUrl, userTimeout, listParam);
				if (filter != null || commonParam != null)
				{
					ApiParam apiParam = new ApiParam();
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = filter;
					string arg = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvertUtil.SerializeObject(apiParam)));
					requestedUrl += string.Format("{0}={1}", "param", arg);
				}
				HttpResponseMessage result = httpClient.GetAsync(requestedUrl).Result;
				if (!result.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}{4}.", httpClient.BaseAddress.AbsoluteUri, uri, result.StatusCode.GetHashCode(), JsonConvertUtil.SerializeObject(filter), JsonConvertUtil.SerializeObject(commonParam)));
				}
				string result2 = result.Content.ReadAsStringAsync().Result;
				return JsonConvertUtil.DeserializeObject<T>(result2);
			}
		}

		public async Task<T> GetAsync<T>(string uri, object commonParam, object filter, params object[] listParam)
		{
			return await GetAsync<T>(uri, commonParam, filter, 0, new object[0]);
		}

		public async Task<T> GetAsync<T>(string uri, object commonParam, object filter, int userTimeout, params object[] listParam)
		{
			using (HttpClient client = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(client, uri, ref requestedUrl, userTimeout, listParam);
				if (filter != null || commonParam != null)
				{
					ApiParam apiParam = new ApiParam();
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = filter;
					ApiParam data = apiParam;
					requestedUrl = string.Concat(str1: string.Format("{0}={1}", "param", Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvertUtil.SerializeObject(data)))), str0: requestedUrl);
				}
				HttpResponseMessage resp = await client.GetAsync(requestedUrl).ConfigureAwait(false);
				if (!resp.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}{4}.", client.BaseAddress.AbsoluteUri, uri, resp.StatusCode.GetHashCode(), JsonConvertUtil.SerializeObject(filter), JsonConvertUtil.SerializeObject(commonParam)));
				}
				return JsonConvertUtil.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
			}
		}

		public T Post<T>(string uri, CommonParam commonParam, object data, params object[] listParam)
		{
			return Post<T>(uri, commonParam, data, 0, listParam);
		}

		public T Post<T>(string uri, CommonParam commonParam, object data, int userTimeout, params object[] listParam)
		{
			_003C_003Ec__DisplayClassb<T> CS_0024_003C_003E8__locals12 = new _003C_003Ec__DisplayClassb<T>();
			CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11 = new _003C_003Ec__DisplayClass17_0<T>();
			CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.uri = uri;
			CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.data = data;
			T result = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				LogSystem.Debug("Post.1");
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.uri), CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.uri) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<object>>(Expression.Field(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals12), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClassb<T>).TypeHandle)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/, typeof(_003C_003Ec__DisplayClass17_0<T>).TypeHandle)), new ParameterExpression[0])), CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.data));
				string requestedUrl = "";
				HttpRequestBuilder(httpClient, CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.uri, ref requestedUrl, userTimeout, listParam);
				ApiParam apiParam = new ApiParam();
				if (CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.data != null || commonParam != null)
				{
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.data;
				}
				LogSystem.Debug("Post.2");
				HttpResponseMessage result2 = httpClient.PostAsJsonAsync(requestedUrl, apiParam).Result;
				if (!result2.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}.", httpClient.BaseAddress.AbsoluteUri, CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.uri, result2.StatusCode.GetHashCode(), JsonConvertUtil.SerializeObject(CS_0024_003C_003E8__locals12.CS_0024_003C_003E8__locals11.data)));
				}
				LogSystem.Debug("Post.3");
				string result3 = result2.Content.ReadAsStringAsync().Result;
				try
				{
					ApiResultObject<T> apiResultObject = JsonConvertUtil.DeserializeObject<ApiResultObject<T>>(result3);
					if (apiResultObject != null)
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

		public T PostRO<T>(string uri, CommonParam commonParam, object data, params object[] listParam)
		{
			return PostRO<T>(uri, commonParam, data, 0, new object[0]);
		}

		public T PostRO<T>(string uri, CommonParam commonParam, object data, int userTimeout, params object[] listParam)
		{
			T val = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(httpClient, uri, ref requestedUrl, userTimeout, listParam);
				ApiParam apiParam = new ApiParam();
				if (data != null || commonParam != null)
				{
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = data;
				}
				HttpResponseMessage result = httpClient.PostAsJsonAsync(requestedUrl, apiParam).Result;
				if (!result.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}.", httpClient.BaseAddress.AbsoluteUri, uri, result.StatusCode.GetHashCode(), JsonConvertUtil.SerializeObject(data)));
				}
				string result2 = result.Content.ReadAsStringAsync().Result;
				try
				{
					return JsonConvertUtil.DeserializeObject<T>(result2);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		public T PostWithouApiParam<T>(string uri, object data, int userTimeout, params object[] listParam)
		{
			T val = default(T);
			using (HttpClient client = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(client, uri, ref requestedUrl, userTimeout, listParam);
				HttpResponseMessage result = client.PostAsJsonAsync(requestedUrl, data).Result;
				if (!result.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUri, uri, result.StatusCode.GetHashCode()));
				}
				string result2 = result.Content.ReadAsStringAsync().Result;
				Console.WriteLine(string.Format("responseData: {0}", result2));
				return JsonConvertUtil.DeserializeObject<T>(result2);
			}
		}

		public async Task<T> PostWithouApiParamAsync<T>(string uri, object data, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient client = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(client, uri, ref requestedUrl, userTimeout, listParam);
				HttpResponseMessage resp = await client.PostAsJsonAsync(requestedUrl, data).ConfigureAwait(false);
				if (!resp.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUri, uri, resp.StatusCode.GetHashCode()));
				}
				T val = JsonConvertUtil.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
				result = val;
			}
			return result;
		}

		public async Task<T> PostAsync<T>(string uri, object commonParam, object data, params object[] listParam)
		{
			return await PostAsync<T>(uri, commonParam, data, 0, new object[0]);
		}

		public async Task<T> PostAsync<T>(string uri, object commonParam, object data, int userTimeout, params object[] listParam)
		{
			T result = default(T);
			using (HttpClient client = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(client, uri, ref requestedUrl, userTimeout, listParam);
				ApiParam apiParam = new ApiParam();
				if (data != null || commonParam != null)
				{
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = data;
				}
				HttpResponseMessage resp = await client.PostAsJsonAsync(requestedUrl, apiParam).ConfigureAwait(false);
				if (!resp.IsSuccessStatusCode)
				{
					LogSystem.Warn(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}.", client.BaseAddress.AbsoluteUri, uri, resp.StatusCode.GetHashCode(), JsonConvertUtil.SerializeObject(data)));
				}
				T val = JsonConvertUtil.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
				result = val;
			}
			return result;
		}

		public T PostWithFile<T>(string uri, object commonParam, object data, List<FileHolder> files, params object[] listParam)
		{
			return PostWithFile<T>(uri, commonParam, data, files, 0, listParam);
		}

		public T PostWithFile<T>(string uri, object commonParam, object data, List<FileHolder> files, int userTimeout, params object[] listParam)
		{
			T val = default(T);
			using (HttpClient httpClient = new HttpClient())
			{
				string requestedUrl = "";
				HttpRequestBuilder(httpClient, uri, ref requestedUrl, userTimeout, listParam);
				ApiParam apiParam = new ApiParam();
				if (data != null || commonParam != null)
				{
					apiParam.CommonParam = commonParam;
					apiParam.ApiData = data;
				}
				using (MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent())
				{
					foreach (FileHolder file in files)
					{
						multipartFormDataContent.Add(new StreamContent(file.Content), "File", file.FileName);
					}
					HttpContent content = new StringContent(JsonConvertUtil.SerializeObject(apiParam));
					multipartFormDataContent.Add(content, "Data", "Data");
					using (HttpResponseMessage httpResponseMessage = httpClient.PostAsync(requestedUrl, multipartFormDataContent).Result)
					{
						if (!httpResponseMessage.IsSuccessStatusCode)
						{
							LogSystem.Error(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUri, uri, httpResponseMessage.StatusCode.GetHashCode()));
						}
						string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
						return JsonConvertUtil.DeserializeObject<T>(result);
					}
				}
			}
		}

		private void HttpRequestBuilder(HttpClient client, string uri, ref string requestedUrl, int userTimeout, params object[] listParam)
		{
			client.DefaultRequestHeaders.Add("TokenCode", token);
			client.DefaultRequestHeaders.Add("ApplicationCode", applicationCode);
			client.BaseAddress = new Uri(baseUri);
			client.DefaultRequestHeaders.Accept.Clear();
			client.Timeout = new TimeSpan(0, 0, (userTimeout > 0) ? userTimeout : TIME_OUT);
			requestedUrl = string.Format("{0}?", uri);
			if (listParam != null && listParam.Length != 0)
			{
				if (listParam.Length % 2 != 0)
				{
					throw new ArgumentException("Danh sach param khong hop le. So luong param phai la so chan");
				}
				for (int i = 0; i < listParam.Length; i += 2)
				{
					string text = requestedUrl;
					object obj = listParam[i];
					string arg = HttpUtility.UrlEncode(((obj != null) ? obj.ToString() : null) ?? "");
					object obj2 = listParam[i + 1];
					requestedUrl = text + string.Format("{0}={1}&", arg, HttpUtility.UrlEncode(((obj2 != null) ? obj2.ToString() : null) ?? ""));
				}
			}
		}
	}
}
