using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Services.Veemer.DTO;

namespace Xamarin.Services.Veemer
{
	public class CatalogService : VeemerServiceBase
	{
		public CatalogService(IAuthorization authorization) : base(authorization)
		{
		}

		public async Task<VeemerQueryResult<Product>> GetAsync(int shopId, int page, int pageSize = 20)
		{
			var parameters = new Dictionary<string, object>();
			parameters.Add("pageIndex", page);
			parameters.Add("pageSize", pageSize);
			return await ExecuteRequestAsync<VeemerQueryResult<Product>>($"/shops/{shopId}/catalog2", parameters);
		}
	}
}