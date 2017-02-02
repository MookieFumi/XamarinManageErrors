using System.Threading.Tasks;

namespace XamarinErrors.Services
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
