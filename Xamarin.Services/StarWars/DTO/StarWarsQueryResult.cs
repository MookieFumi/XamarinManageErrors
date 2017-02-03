using System.Collections.Generic;

namespace Xamarin.Services.StarWars.DTO
{
	public class StarWarsQueryResult<T>
	{
		public int Count { get; set; }
		public string Next { get; set; }
		public object Previous { get; set; }
		public IList<T> Results { get; set; }
	}
}