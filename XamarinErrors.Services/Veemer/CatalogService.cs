using System.Threading.Tasks;
using XamarinErrors.Services.Veemer.DTO;

namespace XamarinErrors.Services.Veemer
{
	public class CatalogService : VeemerServiceBase
	{
		public CatalogService(IAuthorization authorization):base(authorization)
		{
		}

		public async Task<VeemerQueryResult<Product>> GetAsync(int id)
		{
			return await ExecuteRequestAsync<VeemerQueryResult<Product>>($"/shops/{id}/catalog2");
		}
	}
}