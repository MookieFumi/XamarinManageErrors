using HockeyApp.Android;

namespace XamarinErrors
{
    class MyCrashListener : CrashManagerListener
    {
        public override string Contact
        {
            get
            {
                return "Miguel Angel Martin Hernandez";
            }
        }

        public override string UserID
        {
            get
            {
                return "mookiefumi@outlook.com";
            }
        }

        public override string Description
        {
            get
            {
                return "My Description";
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