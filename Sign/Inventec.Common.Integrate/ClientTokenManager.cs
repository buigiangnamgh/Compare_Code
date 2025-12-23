using System;
using System.Net.Http;
using System.Text;

namespace Inventec.Common.Integrate
{
	public class ClientTokenManager
	{
		private TokenData token;

		private string applicationCode;

		private string loginName = "";

		private string baseUriAcs;

		private bool useRegistry = false;

		public ClientTokenManager(string applicationCode)
		{
			this.applicationCode = applicationCode;
			baseUriAcs = ConstanIG.ACS_BASE_URI;
		}

		public ClientTokenManager(string applicationCode, string baseUri)
		{
			this.applicationCode = applicationCode;
			baseUriAcs = baseUri;
		}

		public void UseRegistry(bool useRegistry)
		{
			this.useRegistry = useRegistry;
		}

		public TokenData Init(CommonParam commonParam)
		{
			ReadFromRegistry();
			if (token != null)
			{
				DateTime expireTime = token.ExpireTime;
				if (token.ExpireTime <= DateTime.Now)
				{
					Renew(commonParam);
				}
				else
				{
					token = GetAuthenticated(commonParam, token.TokenCode);
				}
				WriteToRegistry();
			}
			return token;
		}

		public TokenData Login(CommonParam commonParam, string loginName, string password)
		{
			TokenData tokenData = null;
			try
			{
				string s = string.Format("{0}:{1}:{2}", applicationCode, loginName, password);
				s = Convert.ToBase64String(Encoding.Default.GetBytes(s));
				tokenData = CreateRequest<TokenData>(ConstanIG.LOGIN_URI, HttpMethod.Get, commonParam, new string[2]
				{
					"Authorization",
					string.Format("{0} {1}", "Basic", s)
				});
				if (tokenData != null)
				{
					token = tokenData;
					WriteToRegistry();
					this.loginName = loginName;
				}
			}
			catch (Exception)
			{
			}
			return tokenData;
		}

		public TokenData Login(CommonParam commonParam, string loginName, string password, string versionApp)
		{
			TokenData tokenData = null;
			try
			{
				string s = string.Format("{0}:{1}:{2}:{3}:{4}", applicationCode, loginName, password, versionApp, Environment.MachineName);
				s = Convert.ToBase64String(Encoding.Default.GetBytes(s));
				tokenData = CreateRequest<TokenData>(ConstanIG.LOGIN_URI, HttpMethod.Get, commonParam, new string[2]
				{
					"Authorization",
					string.Format("{0} {1}", "Basic", s)
				});
				if (tokenData != null)
				{
					token = tokenData;
					WriteToRegistry();
					this.loginName = loginName;
				}
			}
			catch (Exception)
			{
			}
			return tokenData;
		}

		public TokenData Renew(CommonParam commonParam)
		{
			TokenData tokenData = null;
			try
			{
				tokenData = CreateRequest<TokenData>(ConstanIG.RENEW_URI, HttpMethod.Get, commonParam, new string[2] { "RenewCode", token.RenewCode });
				if (tokenData != null)
				{
					token = tokenData;
					WriteToRegistry();
				}
			}
			catch (Exception)
			{
			}
			return tokenData;
		}

		public TokenData GetTokenData()
		{
			return token;
		}

		public UserData GetUserData()
		{
			TokenData tokenData = GetTokenData();
			return (tokenData != null) ? tokenData.User : null;
		}

		public string GetLoginName()
		{
			UserData userData = GetUserData();
			return (userData != null) ? userData.LoginName : null;
		}

		public string GetUserName()
		{
			UserData userData = GetUserData();
			return (userData != null) ? userData.UserName : null;
		}

		public string GetGroupCode()
		{
			UserData userData = GetUserData();
			return (userData != null) ? userData.GCode : null;
		}

		public string GetLoginAddress()
		{
			TokenData tokenData = GetTokenData();
			return (tokenData != null) ? tokenData.LoginAddress : null;
		}

		public void Logout(CommonParam commonParam)
		{
			try
			{
				ClearRegistry();
			}
			catch (Exception)
			{
			}
			try
			{
				CreateRequest<bool>(ConstanIG.LOGOUT_URI, HttpMethod.Post, commonParam, new string[2] { "TokenCode", token.TokenCode });
			}
			catch (Exception)
			{
			}
		}

		public bool ChangePassword(CommonParam commonParam, string oldPassword, string newPassword)
		{
			bool result = false;
			try
			{
				if (token != null && token.ExpireTime >= DateTime.Now)
				{
					string s = string.Format("{0}:{1}", oldPassword, newPassword);
					s = Convert.ToBase64String(Encoding.Default.GetBytes(s));
					result = CreateRequest<bool>(ConstanIG.CHANGE_PASS_URI, HttpMethod.Post, commonParam, new string[4] { "TokenCode", token.TokenCode, "Password", s });
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		private TokenData GetAuthenticated(CommonParam commonParam, string tokenCode)
		{
			TokenData tokenData = null;
			try
			{
				tokenData = CreateRequest<TokenData>(ConstanIG.GET_AUTHENTICATED_URI, HttpMethod.Get, commonParam, new string[4] { "TokenCode", tokenCode, "ApplicationCode", applicationCode });
				if (tokenData != null)
				{
					token = tokenData;
				}
			}
			catch (Exception)
			{
			}
			return tokenData;
		}

		private T CreateRequest<T>(string requestUri, HttpMethod method, CommonParam commonParam, params string[] headerParams)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(baseUriAcs);
				httpClient.DefaultRequestHeaders.Accept.Clear();
				httpClient.Timeout = new TimeSpan(0, 0, ConstanIG.TIME_OUT);
				if (headerParams != null && headerParams.Length % 2 == 0)
				{
					for (int i = 0; i < headerParams.Length; i += 2)
					{
						httpClient.DefaultRequestHeaders.Add(headerParams[i], headerParams[i + 1]);
					}
				}
				commonParam = ((commonParam == null) ? new CommonParam() : commonParam);
				HttpResponseMessage httpResponseMessage = null;
				try
				{
					if (method == HttpMethod.Get)
					{
						httpResponseMessage = httpClient.GetAsync(requestUri).Result;
					}
					else if (method == HttpMethod.Post)
					{
						httpResponseMessage = httpClient.PostAsJsonAsync(requestUri, "").Result;
					}
				}
				catch (Exception ex)
				{
					commonParam.Messages.Add(string.Format("Không thể truy cập tới máy chủ. (Địa chỉ: {0}{1})", baseUriAcs, requestUri));
					throw ex;
				}
				if (httpResponseMessage == null || !httpResponseMessage.IsSuccessStatusCode)
				{
					int hashCode = httpResponseMessage.StatusCode.GetHashCode();
					commonParam.Messages.Add(string.Format("Không thể truy cập tới máy chủ. (Địa chỉ: {0}{1}. Mã: {2})", baseUriAcs, requestUri, hashCode));
					throw new Exception(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUriAcs, requestUri, hashCode));
				}
				string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
				ApiResultObject<T> apiResultObject = JsonConvertUtil.DeserializeObject<ApiResultObject<T>>(result);
				if (apiResultObject != null && apiResultObject.Param != null)
				{
					if (apiResultObject.Param.BugCodes != null)
					{
						commonParam.BugCodes.AddRange(apiResultObject.Param.BugCodes);
					}
					if (apiResultObject.Param.Messages != null)
					{
						commonParam.Messages.AddRange(apiResultObject.Param.Messages);
					}
				}
				if (apiResultObject == null || !apiResultObject.Success)
				{
					throw new Exception(string.Format("Loi khi goi API. Response {0}:", result));
				}
				return apiResultObject.Data;
			}
		}

		private void WriteToRegistry()
		{
			try
			{
				if (useRegistry && token != null)
				{
					long? num = DateTimeConvert.SystemDateTimeToTimeNumber(token.ExpireTime);
					RegistryProcessor.Write(ConstanIG.REGISTRY_TOKEN_CODE, token.TokenCode, ConstanIG.REGISTRY_SUBFOLDER);
					RegistryProcessor.Write(ConstanIG.REGISTRY_RENEW_CODE, token.RenewCode, ConstanIG.REGISTRY_SUBFOLDER);
					RegistryProcessor.Write(ConstanIG.REGISTRY_EXPIRE_TIME, num.Value, ConstanIG.REGISTRY_SUBFOLDER);
				}
			}
			catch (Exception)
			{
			}
		}

		private void ClearRegistry()
		{
			try
			{
				if (useRegistry)
				{
					RegistryProcessor.DeleteValue(ConstanIG.REGISTRY_TOKEN_CODE, ConstanIG.REGISTRY_SUBFOLDER);
					RegistryProcessor.DeleteValue(ConstanIG.REGISTRY_RENEW_CODE, ConstanIG.REGISTRY_SUBFOLDER);
					RegistryProcessor.DeleteValue(ConstanIG.REGISTRY_EXPIRE_TIME, ConstanIG.REGISTRY_SUBFOLDER);
				}
			}
			catch (Exception)
			{
			}
		}

		private void ReadFromRegistry()
		{
			try
			{
				if (useRegistry)
				{
					string text = (string)RegistryProcessor.Read(ConstanIG.REGISTRY_TOKEN_CODE, ConstanIG.REGISTRY_SUBFOLDER);
					string renewCode = (string)RegistryProcessor.Read(ConstanIG.REGISTRY_RENEW_CODE, ConstanIG.REGISTRY_SUBFOLDER);
					string text2 = (string)RegistryProcessor.Read(ConstanIG.REGISTRY_EXPIRE_TIME, ConstanIG.REGISTRY_SUBFOLDER);
					long num = ((!string.IsNullOrWhiteSpace(text2)) ? long.Parse(text2) : 0);
					if (!string.IsNullOrWhiteSpace(text) && num > 0)
					{
						token = new TokenData();
						token.TokenCode = text;
						token.RenewCode = renewCode;
						token.ExpireTime = DateTimeConvert.TimeNumberToSystemDateTime(num).Value;
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
