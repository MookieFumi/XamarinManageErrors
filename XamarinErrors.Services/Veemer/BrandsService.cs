using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinErrors.Services.Veemer;

namespace XamarinErrors.Services
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
