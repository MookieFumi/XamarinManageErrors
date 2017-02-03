using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin.Droid.Infrastructure;
using Xamarin.Services;
using Xamarin.Services.Veemer;

namespace Xamarin.Droid.Activities
{
	[Activity(MainLauncher = true, Icon = "@mipmap/icon")]
	public class LoginActivity : BaseActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Login);

#if DEBUG
			GetUserName().Text = "miguel.martin@analyticalways.com";
			GetPassword().Text = "piloto@20";
#endif

			var login = FindViewById<Button>(Resource.Id.login);
			login.Click += Login_OnClick;
		}

		async void Login_OnClick(object sender, EventArgs e)
		{
			_progressDialog.Show();
			try
			{
				var accountService = new AccountService(new Authorization(GetUserName().Text, GetPassword().Text, PasswordType.Password));
				var loginResponse = await accountService.LoginAsync();
				SharedPreferencesManager.SaveUserName(loginResponse.UserName);
				SharedPreferencesManager.SavePassword(GetPassword().Text);
				SharedPreferencesManager.SaveLoginResponse(JsonConvert.SerializeObject(loginResponse));

				var intent = new Intent(this, typeof(MainActivity));
				StartActivity(intent);
			}
			catch (HttpWebApiException exception)
			{
				Toast.MakeText(this, $"{exception.InnerException.Message}", ToastLength.Long).Show();
			}
			finally
			{
				_progressDialog.Dismiss();
			}
		}

		EditText GetUserName()
		{
			return FindViewById<EditText>(Resource.Id.userName);
		}

		EditText GetPassword()
		{
			return FindViewById<EditText>(Resource.Id.password);
		}
	}

}