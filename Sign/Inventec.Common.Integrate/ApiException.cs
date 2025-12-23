using System;
using System.Net;

namespace Inventec.Common.Integrate
{
	public class ApiException : Exception
	{
		public HttpStatusCode StatusCode { get; set; }

		public ApiException(HttpStatusCode statusCode, string message)
			: base(message)
		{
			StatusCode = statusCode;
		}

		public ApiException(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}
	}
}
