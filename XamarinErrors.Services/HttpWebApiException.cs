using System;

namespace XamarinErrors.Services
{

	public class HttpWebApiException : Exception
	{
		public HttpWebApiException()
		{
		}

		public HttpWebApiException(string message) : base(message)
		{
		}

		public HttpWebApiException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}

}
