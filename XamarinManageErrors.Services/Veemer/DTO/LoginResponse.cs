namespace XamarinManageErrors.Services.Veemer.DTO
{

	public class LoginResponse
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public Company Company { get; set; }
		public Shop Shop { get; set; }
		public string Photo { get; set; }
		public int RoleType { get; set; }
		public bool HasPin { get; set; }
	}
}
