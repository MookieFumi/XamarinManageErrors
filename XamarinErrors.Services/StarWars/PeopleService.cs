using System.Threading.Tasks;
using XamarinErrors.Services.StarWars.DTO;

namespace XamarinErrors.Services.StarWars
{

	public class PeopleService : StarWarsServiceBase
	{
		public async Task<StarWarsQueryResult<Person>> GetAllAsync()
		{
			return await ExecuteRequestAsync<StarWarsQueryResult<Person>>("/people");
		}

		public async Task<Person> GetAsync(int id)
		{
			return await ExecuteRequestAsync<Person>($"/people/{id}");
		}
	}

	
}
