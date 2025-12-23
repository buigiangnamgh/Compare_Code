using System;
using System.IO;
using System.Net.Http;
using Inventec.Common.SignToolViewer.Integrate;
using Newtonsoft.Json;

namespace Inventec.Common.Integrate
{
	internal class FssFileDownload
	{
		internal static MemoryStream GetFile(string fileUrl)
		{
			try
			{
				return GetFile(fileUrl, null);
			}
			catch (FileUploadException ex)
			{
				throw ex;
			}
			catch (Exception inner)
			{
				throw new FileUploadException("Exception when uploading file", inner);
			}
		}

		public static MemoryStream GetFile(string fileUrl, string baseUri)
		{
			MemoryStream result = new MemoryStream();
			try
			{
				using (HttpClient httpClient = new HttpClient())
				{
					if (!string.IsNullOrEmpty(fileUrl) && fileUrl.Replace("/", "\\").StartsWith("\\"))
					{
						HttpResponseMessage result2 = httpClient.GetAsync(((!string.IsNullOrEmpty(baseUri)) ? baseUri : FssConstant.BASE_URI) + fileUrl).Result;
						if (!result2.IsSuccessStatusCode)
						{
							throw new FileDownloadException(result2.StatusCode, result2.ReasonPhrase);
						}
						result = ((result2.Content == null) ? null : new MemoryStream(result2.Content.ReadAsByteArrayAsync().Result));
					}
					else if (!string.IsNullOrEmpty(fileUrl))
					{
						httpClient.BaseAddress = new Uri((!string.IsNullOrEmpty(baseUri)) ? baseUri : FssConstant.BASE_URI);
						httpClient.DefaultRequestHeaders.Accept.Clear();
						httpClient.Timeout = new TimeSpan(0, 0, FssConstant.TIME_OUT);
						string dOWNLOAD_URI = FssConstant.DOWNLOAD_URI;
						ApiParam apiParam = new ApiParam();
						CommonParam commonParam = new CommonParam();
						apiParam.CommonParam = commonParam;
						apiParam.ApiData = fileUrl;
						HttpResponseMessage result3 = HttpClientExtensions.PostAsJsonAsync<ApiParam>(httpClient, dOWNLOAD_URI, apiParam).Result;
						if (!result3.IsSuccessStatusCode)
						{
							throw new Exception("StatusCode:" + result3.StatusCode);
						}
						string result4 = result3.Content.ReadAsStringAsync().Result;
						byte[] buffer = JsonConvert.DeserializeObject<byte[]>(result4);
						try
						{
							result = new MemoryStream(buffer);
						}
						catch (Exception ex)
						{
							throw ex;
						}
					}
				}
			}
			catch (FileUploadException ex2)
			{
				throw ex2;
			}
			catch (Exception inner)
			{
				throw new FileUploadException("Exception when uploading file", inner);
			}
			return result;
		}

		private static string ResolveUrl(string urlFile)
		{
			if (!string.IsNullOrEmpty(urlFile))
			{
				urlFile = urlFile.Replace("\\", "//");
				urlFile = urlFile.Replace("//", "/");
			}
			return urlFile;
		}
	}
}
