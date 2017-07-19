using Android.App;
using Android.Content;
using Newtonsoft.Json;
using XamarinManageErrors.Services.Veemer.DTO;

namespace XamarinManageErrors.Droid.Infrastructure
{

	public static class SharedPreferencesManager
	{
		private const string Appname = "XamarinErrors";
		private const string Username = "UserName";
		private const string Password = "Password";
		private const string Loginresponse = "LoginResponse";

		public static string GetUserName()
		{
			return Get(Username);
		}

		public static string GetPassword()
		{
			return Get(Password);
		}

		public static LoginResponse GetLoginResponse()
		{
			return JsonConvert.DeserializeObject<LoginResponse>(Get(Loginresponse));
		}

		public static void SaveUserName(string userName)
		{
			Save(Username, userName);
		}

		public static void SavePassword(string password)
		{
			Save(Password, password);
		}

		public static void SaveLoginResponse(string loginResponse)
		{
			Save(Loginresponse, loginResponse);
		}

		public static void ClearAll()
		{
			var sharedPreferences = Application.Context.GetSharedPreferences(Appname, FileCreationMode.Private);
			var edit = sharedPreferences.Edit();
			edit.Clear();
			edit.Commit();
		}

		private static void Save(string key, string value)
		{
			var sharedPreferences = Application.Context.GetSharedPreferences(Appname, FileCreationMode.Private);
			var edit = sharedPreferences.Edit();
			edit.PutString(key, value);
			edit.Commit();
		}

		private static string Get(string key)
		{
			var sharedPreferences = Application.Context.GetSharedPreferences(Appname, FileCreationMode.Private);
			return sharedPreferences.GetString(key, null);
		}
	}
}
