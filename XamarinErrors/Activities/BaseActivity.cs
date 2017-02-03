using Android.App;
using Android.OS;

namespace XamarinErrors
{
	public class BaseActivity : Activity
	{

		protected ProgressDialog _progressDialog;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetTitle("Calling Api (HttpClient)");
			_progressDialog.SetMessage("Please wait, be patient.");
			_progressDialog.SetCancelable(false);
		}
	}

}