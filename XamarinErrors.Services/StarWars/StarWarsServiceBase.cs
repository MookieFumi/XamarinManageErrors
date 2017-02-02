using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace XamarinErrors.Services.StarWars
{
	public class StarWarsServiceBase
	{
		string baseUri = "http://swapi.co/api";

		protected async Task<T> ExecuteRequestAsync<T>(string api)
		{
			using (var client = new HttpClient())
			{
				var uri = string.Format("{0}{1}", baseUri, api);
				var httpRequest = new HttpRequestMessage(new HttpMethod("GET"), uri);
				string content = null;
				try
				{
					client.Timeout = TimeSpan.FromSeconds(30);
					var _cancelTokenSource = new CancellationTokenSource();
					var _cancelToken = _cancelTokenSource.Token;
					var response = await client.SendAsync(httpRequest, _cancelToken).ConfigureAwait(false);
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