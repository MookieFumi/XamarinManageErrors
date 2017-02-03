using HockeyApp.Android;
using Newtonsoft.Json;

namespace XamarinErrors
{
	class XamarinErrorsCrashListener : CrashManagerListener
	{
		public override string Contact
		{
			get
			{
				return SharedPreferencesManager.GetLoginResponse().FullName;
			}
		}

		public override string UserID
		{
			get
			{
				return SharedPreferencesManager.GetUserName();
			}
		}

		public override string Description
		{
			get
			{
				return JsonConvert.SerializeObject(SharedPreferencesManager.GetLoginResponse());
			}
		}

		public override bool IncludeDeviceData()
		{
			return true;
		}

		public override bool IncludeDeviceIdentifier()
		{
			return true;
		}

		public override bool ShouldAutoUploadCrashes()
		{
			return true;
		}
	}
}