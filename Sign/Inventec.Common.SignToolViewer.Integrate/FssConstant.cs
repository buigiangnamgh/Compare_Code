using System.Configuration;

namespace Inventec.Common.SignToolViewer.Integrate
{
	internal class FssConstant
	{
		internal const string HEADER_CLIENT_CODE = "fss-client-code";

		internal const string HEADER_FILE_STORE_LOCATION = "fss-file-store-location";

		internal const string HEADER_STORAGE_MODE = "fss-StorageMode";

		internal static string BASE_URI = "";

		internal static string UPLOAD_URI = "api/File/Upload";

		internal static string DELETE_URI = "api/File/Delete";

		internal static string DOWNLOAD_URI = "api/File/Download";

		internal static string STORAGE_MODE = ConfigurationManager.AppSettings["fss.StorageMode"] ?? "";

		internal static int TIME_OUT = 300;
	}
}
