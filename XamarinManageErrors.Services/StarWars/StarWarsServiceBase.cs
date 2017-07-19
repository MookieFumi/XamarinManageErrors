using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace XamarinManageErrors.Services.StarWars
{
	public class StarWarsServiceBase
	{
		private const string BaseUri = "http://swapi.co/api";

		protected async Task<T> ExecuteRequestAsync<T>(string api)
		{
			using (var client = new HttpClient())
			{
				var uri = $"{BaseUri}{api}";
				var httpRequest = new HttpRequestMessage(new HttpMethod("GET"), uri);
				string content = null;
				try
				{
					client.Timeout = TimeSpan.FromSeconds(30);
					var cancelTokenSource = new CancellationTokenSource();
					var cancelToken = cancelTokenSource.Token;
					var response = await client.SendAsync(httpRequest, cancelToken).ConfigureAwait(false);
					content = await response.Content.ReadAsStringAsync();
					response.EnsureSuccessStatusCode();
				}
				catch (HttpRequestException e)
				{
					throw new HttpWebApiException("HttpWebApiException", e);
				}
				return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});
			}
		}
	}

	
}