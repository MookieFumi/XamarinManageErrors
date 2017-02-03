using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinErrors.Services.Veemer.DTO;

namespace XamarinErrors.Services.Veemer
{
	public class AccountService : VeemerServiceBase
	{
		public AccountService(IAuthorization authorization) : base(authorization)
		{
		}

		public async Task<LoginResponse> LoginAsync()
		{
			var items = new Dictionary<string, string>();
			items.Add("UserName", _authorization.UserName);
			items.Add("Password", _authorization.Password);
			items.Add("PasswordType", ((int)_authorization.PasswordType).ToString());

			return await ExecuteRequestAsync<LoginResponse>($"/account/login", items);
		}
	}
}