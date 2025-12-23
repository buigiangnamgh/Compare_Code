using System;
using System.Net;

namespace Inventec.Common.Integrate
{
	internal class FileDownloadException : Exception
	{
		internal HttpStatusCode StatusCode { get; set; }

		internal FileDownloadException()
		{
		}

		internal FileDownloadException(string message, Exception inner)
			: base(message, inner)
		{
		}

		internal FileDownloadException(HttpStatusCode statusCode, string message)
			: base(message)
		{
			StatusCode = statusCode;
		}
	}
}
