using Android.App;
using Android.Content;
using Newtonsoft.Json;
using XamarinErrors.Services.Veemer.DTO;

namespace XamarinErrors
{

	public static class SharedPreferencesManager
	{
		const string APPNAME = "XamarinErrors";
		const string USERNAME = "UserName";
		const string PASSWORD = "Password";
		const string LOGINRESPONSE = "LoginResponse";

		public static string GetUserName()
		{
			return Get(USERNAME);
		}

		public static string GetPassword()
		{
			return Get(PASSWORD);
		}

		public static LoginResponse GetLoginResponse()
		{
			return JsonConvert.DeserializeObject<LoginResponse>(Get(LOGINRESPONSE));
		}

		public static void SaveUserName(string userName)
		{
			Save(USERNAME, userName);
		}

		public static void SavePassword(string password)
		{
			Save(PASSWORD, password);
		}

		public static void SaveLoginResponse(string loginResponse)
		{
			Save(LOGINRESPONSE, loginResponse);
		}

		static void Save(string key, string value)
		{
			var sharedPreferences = Application.Context.GetSharedPreferences(APPNAME, FileCreationMode.Private);
			var edit = sharedPreferences.Edit();
			edit.PutString(key, value);
			edit.Commit();
		}

		static string Get(string key)
		{
			var sharedPreferences = Application.Context.GetSharedPreferences(APPNAME, FileCreationMode.Private);
			return sharedPreferences.GetString(key, null);
		}
	}
}
