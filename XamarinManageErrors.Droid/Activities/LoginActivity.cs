using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using XamarinManageErrors.Droid.Infrastructure;
using XamarinManageErrors.Services;
using XamarinManageErrors.Services.Veemer;

namespace XamarinManageErrors.Droid.Activities
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
            login.Click += Login_OnClick2;
        }

        private async void Login_OnClick2(object sender, EventArgs e)
        {
            Task.Run(() => DoSomething());
            //try
            //{
            //    throw new DivideByZeroException();
            //}
            //catch (Exception ex)
            //{
            //    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            //}
        }

        private Task DoSomething()
        {
            Task.Delay(new TimeSpan(0, 0, 3));
            throw new DivideByZeroException();
        }

        private async void Login_OnClick(object sender, EventArgs e)
        {
            ProgressDialog.Show();
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
                ProgressDialog.Dismiss();
            }
        }

        private EditText GetUserName()
        {
            return FindViewById<EditText>(Resource.Id.userName);
        }

        private EditText GetPassword()
        {
            return FindViewById<EditText>(Resource.Id.password);
        }
    }

}