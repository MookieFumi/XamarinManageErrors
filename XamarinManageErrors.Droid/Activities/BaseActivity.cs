using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace XamarinManageErrors.Droid.Activities
{
	public class BaseActivity : AppCompatActivity
	{

		protected ProgressDialog _progressDialog;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetTitle("Retrieving your data");
			_progressDialog.SetMessage("Please wait, be patient.");
			_progressDialog.SetCancelable(false);
		}
	}

}