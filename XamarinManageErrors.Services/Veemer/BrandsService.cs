using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinManageErrors.Services.Veemer.DTO;

namespace XamarinManageErrors.Services.Veemer
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
