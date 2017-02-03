using System.Collections.Generic;

namespace Xamarin.Services.Veemer.DTO
{

	public class Product
	{
		public bool HasChildren { get; set; }
		public int ProductId { get; set; }
		public string ProductCode { get; set; }
		public string RootProductCode { get; set; }
		public string ProductDescription { get; set; }
		public Brand Brand { get; set; }
		public IEnumerable<string> ClassificationLevels { get; set; }
		public string ThumbnailImagelUrl { get; set; }
		public string ImageUrl { get; set; }
		public object Size { get; set; }
		public bool Novelty { get; set; }
		public bool Discontinued { get; set; }
	}

}
