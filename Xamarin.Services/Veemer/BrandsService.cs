using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Services.Veemer.DTO;

namespace Xamarin.Services.Veemer
{

	public class BrandsService : VeemerServiceBase
	{
		public BrandsService(IAuthorization authorization) : base(authorization)
		{
		}

		public async Task<IEnumerable<ClassificationLevel>> GetClassificationLevelAsync(int id)
		{
			return await ExecuteRequestAsync<IEnumerable<ClassificationLevel>>($"/brands/{id}/classificationlevels");
		}
	}
}
