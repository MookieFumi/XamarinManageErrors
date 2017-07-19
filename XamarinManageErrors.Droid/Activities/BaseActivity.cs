using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace XamarinManageErrors.Droid.Activities
{
	public class BaseActivity : AppCompatActivity
	{

		protected ProgressDialog ProgressDialog;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			ProgressDialog = new ProgressDialog(this);
			ProgressDialog.SetTitle("Retrieving your data");
			ProgressDialog.SetMessage("Please wait, be patient.");
			ProgressDialog.SetCancelable(false);
		}
	}

}