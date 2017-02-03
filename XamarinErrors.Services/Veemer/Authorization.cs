using System;

namespace XamarinErrors.Services.Veemer
{
	public class Authorization : IAuthorization
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public PasswordType PasswordType { get; set; }

		public Authorization(string userName, string password, PasswordType passwordType)
		{
			Password = password;
			UserName = userName;
			PasswordType = passwordType;
		}
	}

}
