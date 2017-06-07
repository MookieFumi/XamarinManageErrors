using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace XamarinManageErrors.Services.Veemer
{
	public class VeemerServiceBase
	{
		protected readonly IAuthorization _authorization;

		public VeemerServiceBase(IAuthorization authorization)
		{
			_authorization = authorization;
		}

		string baseUri = "https://api-veemer-pre.azurewebsites.net";

		protected async Task<T> ExecuteRequestAsync<T>(string api, Dictionary<string, object> parameters = null)
		{
			using (var httpClient = new HttpClient())
			{
				httpClient.AddHeaders(_authorization);

				var uri = string.Format("{0}{1}", baseUri, api);

				if (parameters != null)
				{
					var queryString = string.Join("&", parameters.Select(p => string.Format($"{p.Key}={p.Value}")));
					uri += "?" + queryString;
				}

				var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);

				string content = null;
				try
				{
					httpClient.Timeout = TimeSpan.FromSeconds(10);
					var _cancelTokenSource = new CancellationTokenSource();
					var _cancelToken = _cancelTokenSource.Token;
					var response = await httpClient.SendAsync(httpRequest, _cancelToken).ConfigureAwait(false);
					content = await response.Content.ReadAsStringAsync();
					response.EnsureSuccessStatusCode();
				}
				catch (Exception e)
				{
					throw new HttpWebApiException(e.Message, e);
				}
				return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});
			}
		}

		protected async Task<T> ExecuteRequestAsync<T>(string api, Dictionary<string, string> values)
		{
			using (var httpClient = new HttpClient())
			{
				httpClient.AddHeaders(_authorization).AddHeaderContentType();

				var uri = string.Format("{0}{1}", baseUri, api);
				var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

				var content = JsonConvert.SerializeObject(values, new KeyValuePairConverter());
				httpRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");

				try
				{
					httpClient.Timeout = TimeSpan.FromSeconds(10);
					var _cancelTokenSource = new CancellationTokenSource();
					var _cancelToken = _cancelTokenSource.Token;
					var response = await httpClient.SendAsync(httpRequest, _cancelToken).ConfigureAwait(false);
					content = await response.Content.ReadAsStringAsync();
					response.EnsureSuccessStatusCode();
				}
				catch (Exception e)
				{
					throw new HttpWebApiException(e.Message, e);
				}
				return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});
			}
		}
	}


}