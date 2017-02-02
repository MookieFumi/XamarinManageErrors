using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace XamarinErrors.Services.Veemer
{

	public static class VeemerHttpClientExtensions
	{
		public static HttpClient AddHeaders(this HttpClient client, IAuthorization authorization, string version = "1", PasswordType type = PasswordType.Password)
		{
			// We will implement Basic Authorization
			var byteArray = Encoding.UTF8.GetBytes(String.Format("{0}:{1}", authorization.UserName, authorization.Password));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			client.DefaultRequestHeaders.Add("X-Api-Version", version);
			client.DefaultRequestHeaders.Add("X-PasswordType", Convert.ToString((int)type));
			return client;
		}
	}

}