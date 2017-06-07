using Android.OS;
using XamarinManageErrors.Droid.Infrastructure;
using XamarinManageErrors.Services.Veemer.DTO;

namespace XamarinManageErrors.Droid.Activities
{
	public class BaseAuthorizedActivity : BaseActivity
	{
		public string UserName
		{
			get;
			private set;
		}
		public string Password
		{
			get;
			private set;
		}
		public LoginResponse LoginResponse
		{
			get;
			private set;
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			UserName = SharedPreferencesManager.GetUserName();
			Password = SharedPreferencesManager.GetPassword();
			LoginResponse = SharedPreferencesManager.GetLoginResponse();
		}
	}

}