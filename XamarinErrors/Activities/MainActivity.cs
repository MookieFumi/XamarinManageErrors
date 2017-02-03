using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using XamarinErrors.Droid;
using XamarinErrors.Services;
using XamarinErrors.Services.StarWars;
using XamarinErrors.Services.Veemer;

namespace XamarinErrors
{
	[Activity(Label = "XamarinErrors", Icon = "@mipmap/icon")]
	public class MainActivity : BaseAuthorizedActivity
	{
		Button _btnUnhandledException;
		Button _btnHandledException;
		Button _btnSWNoError;
		Button _btnSWError;
		Button _btnCatalog;
		Button _btnClassificationLevels;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			_btnUnhandledException = FindViewById<Button>(Resource.Id.btnUnhandledException);
			_btnUnhandledException.Click += btnUnhandledException_OnClick;

			_btnHandledException = FindViewById<Button>(Resource.Id.btnHandledException);
			_btnHandledException.Click += btnHandledException_OnClick;

			_btnSWNoError = FindViewById<Button>(Resource.Id.btnSWNoError);
			_btnSWNoError.Click += btnSWNoError_OnClick;

			_btnSWError = FindViewById<Button>(Resource.Id.btnSWError);
			_btnSWError.Click += btnSWError_OnClick;

			_btnCatalog = FindViewById<Button>(Resource.Id.btnCatalog);
			_btnCatalog.Click += btnCatalog_OnClick;

			_btnClassificationLevels = FindViewById<Button>(Resource.Id.btnClassificationLevels);
			_btnClassificationLevels.Click += btnClassificationLevels_OnClick;
		}

		public override void OnBackPressed()
		{
		}

		async void btnClassificationLevels_OnClick(object sender, EventArgs e)
		{
			_progressDialog.Show();
			try
			{
				var value = FindViewById<EditText>(Resource.Id.txtBrandId).Text;
				var brandsService = new BrandsService(new Authorization(UserName, Password, PasswordType.Password));
				var response = await brandsService.GetClassificationLevelAsync(Convert.ToInt32(value));
				Toast.MakeText(this, $"Catalog: {response.Count()} items. First product: {response.First().Level}",
					ToastLength.Long).Show();
			}
			catch (HttpWebApiException exception)
			{
				Toast.MakeText(this, $"Exception: {exception.InnerException.Message}", ToastLength.Long).Show();
			}
			finally
			{
				_progressDialog.Dismiss();
			}
		}

		async void btnCatalog_OnClick(object sender, EventArgs e)
		{
			_progressDialog.Show();
			try
			{
				var value = FindViewById<EditText>(Resource.Id.txtShopId).Text;
				var catalogService = new CatalogService(new Authorization(UserName, Password, PasswordType.Password));
				var response = await catalogService.GetAsync(Convert.ToInt32(value));
				Toast.MakeText(this, $"Catalog: {response.TotalCount} items.", ToastLength.Long).Show();
			}
			catch (HttpWebApiException exception)
			{
				Toast.MakeText(this, $"Exception: {exception.InnerException.Message}", ToastLength.Long).Show();
			}
			finally
			{
				_progressDialog.Dismiss();
			}
		}

		async void btnSWError_OnClick(object sender, EventArgs e)
		{
			_progressDialog.Show();
			try
			{
				var peopleService = new PeopleService();
				var response = await peopleService.GetAsync(999);
				Toast.MakeText(this, $"Person: {response.Name} - {response.Url}", ToastLength.Long).Show();
			}
			catch (HttpWebApiException exception)
			{
				Toast.MakeText(this, $"Exception: {exception.InnerException.Message}", ToastLength.Long).Show();
			}
			finally
			{
				_progressDialog.Dismiss();
			}
		}

		async void btnSWNoError_OnClick(object sender, EventArgs e)
		{
			_progressDialog.Show();

			var peopleService = new PeopleService();
			var response = await peopleService.GetAllAsync();

			_progressDialog.Dismiss();

			Toast.MakeText(this, $"There are {response.Count} people. First one is {response.Results.First().Name}",
				ToastLength.Long).Show();
		}

		void btnHandledException_OnClick(object sender, EventArgs e)
		{
			try
			{
				throw new OverflowException();
			}
			catch (Exception ex)
			{
				Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
			}
		}

		void btnUnhandledException_OnClick(object sender, EventArgs e)
		{
			throw new OutOfMemoryException("AnalyticAlways Exception");
		}
	}
}