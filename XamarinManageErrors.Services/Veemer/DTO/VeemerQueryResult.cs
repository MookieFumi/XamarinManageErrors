using System.Collections.Generic;

namespace XamarinManageErrors.Services.Veemer.DTO
{

	public class VeemerQueryResult<T>
	{
		public IEnumerable<T> Result { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPageCount { get; set; }
		public bool HasPreviousPage { get; set; }
		public bool HasNextPage { get; set; }
	}
}
