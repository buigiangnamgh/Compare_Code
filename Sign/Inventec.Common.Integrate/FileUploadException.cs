using System;
using System.Net;

namespace Inventec.Common.Integrate
{
	internal class FileUploadException : Exception
	{
		internal HttpStatusCode StatusCode { get; set; }

		internal FileUploadException()
		{
		}

		internal FileUploadException(string message, Exception inner)
			: base(message, inner)
		{
		}

		internal FileUploadException(HttpStatusCode statusCode, string message)
			: base(message)
		{
			StatusCode = statusCode;
		}
	}
}
