using Inventec.Common.Integrate;

namespace Inventec.Common.SignLibrary.ADO
{
	public class ApiResult
	{
		public bool data { get; set; }

		public int statusCode { get; set; }

		public bool success { get; set; }

		public int status { get; set; }

		public CommonParam param { get; set; }
	}
}
