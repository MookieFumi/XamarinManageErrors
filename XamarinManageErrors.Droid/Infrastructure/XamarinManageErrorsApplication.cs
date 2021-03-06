using System;
using Android.App;
using Android.Runtime;
using HockeyApp.Android;
using System.Diagnostics;
using System.Threading.Tasks;

namespace XamarinManageErrors.Droid.Infrastructure
{
    [Application]
    public class XamarinManageErrorsApplication : Application
    {
        public XamarinManageErrorsApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();

            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                Console.WriteLine(args.Exception.Message);
            };
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_OnUnhandledException();

            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_OnUnhandledExceptionRaiser();
            CrashManager.Register(this, "2c08366e1b8a431ab13e2b22d5c7745a", new XamarinErrorsCrashListener());
        }

        private static UnhandledExceptionEventHandler CurrentDomain_OnUnhandledException()
        {
            return (sender, args) =>
            {
                Debug.WriteLine(nameof(CurrentDomain_OnUnhandledException));
                var a = "";
                /*
                 * When a background thread crashes this is the code that will be executed. You can
                 * recover from this.
                 * You might for example:
                 *  _CurrentActivity.RunOnUiThread(() => Toast.MakeText(_CurrentActivity, "Unhadled Exception was thrown", ToastLength.Short).Show());
                 *  
                 * or
                 * 
                 * _CurrentActivity.StartActivity(typeof(SomeClass));
                 * _CurrentActivity.Finish();
                 *
                 * It is up to the developer as to what he/she wants to do here.
                 * 
                 * If you are requiring a minimum version less than API 14, you would have to set _CurrentActivity in each time
                 * the a different activity is brought to the foreground.
                 */
            };
        }

        private static EventHandler<RaiseThrowableEventArgs> AndroidEnvironment_OnUnhandledExceptionRaiser()
        {
            return (sender, args) =>
            {
                Debug.WriteLine(nameof(AndroidEnvironment_OnUnhandledExceptionRaiser));
                var a = "";
                /*
                 * When the UI Thread crashes this is the code that will be executed. There is no context at this point
                 * and no way to recover from the exception. This is where you would capture the error and log it to a 
                 * file for example. You might be able to post to a web handler, I have not tried that.
                 * 
                 * You can access the information about the exception in the args.Exception object.
                 */
            };
        }

    }
}
