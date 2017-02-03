namespace XamarinErrors.Services.Veemer
{
	public interface IAuthorization
	{
		string UserName { get; }
		string Password { get; }
		PasswordType PasswordType { get; }
	}

}
