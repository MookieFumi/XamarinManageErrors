namespace XamarinErrors.Services
{

	public class Authorization : IAuthorization
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		public Authorization(string userName, string password)
		{
			Password = password;
			UserName = userName;
		}
	}

}
