using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Droid.Infrastructure;
using Xamarin.Services;
using Xamarin.Services.StarWars;
using Xamarin.Services.Veemer;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Xamarin.Droid.Activities
{
	[Activity(Icon = "@mipmap/icon")]
	public class MainActivity : BaseAuthorizedActivity
	{
		Button _btnUnhandledException;
		Button _btnHandledException;
		Button _btnSwNoError;
		Button _btnSwError;
		Button _btnCatalog;
		Button _btnClassificationLevels;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			// setup the action bar
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
			SetSupportActionBar(toolbar);

			// ensure that the system bar color gets drawn
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			// set the title of both the activity and the action bar
			Title = SupportActionBar.Title = Resources.GetString(Resource.String.app_name);

			_btnUnhandledException = FindViewById<Button>(Resource.Id.btnUnhandledException);
			_btnUnhandledException.Click += btnUnhandledException_OnClick;

			_btnHandledException = FindViewById<Button>(Resource.Id.btnHandledException);
			_btnHandledException.Click += btnHandledException_OnClick;

			_btnSwNoError = FindViewById<Button>(Resource.Id.btnSWNoError);
			_btnSwNoError.Click += btnSWNoError_OnClick;

			_btnSwError = FindViewById<Button>(Resource.Id.btnSWError);
			_btnSwError.Click += btnSWError_OnClick;

			_btnCatalog = FindViewById<Button>(Resource.Id.btnCatalog);
			_btnCatalog.Click += btnCatalog_OnClick;

			_btnClassificationLevels = FindViewById<Button>(Resource.Id.btnClassificationLevels);
			_btnClassificationLevels.Click += btnClassificationLevels_OnClick;
		}

		public override void OnBackPressed()
		{
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.main_menu, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item != null)
			{
				switch (item.ItemId)
				{
					case Resource.Id.logout:
						var builder = new AlertDialog.Builder(this);
						builder.SetTitle("Logout")
							   .SetMessage("Are your sure to log out?")
							   .SetPositiveButton("Yes", (sender, e) => Logout())
							   .SetNegativeButton("No", (sender, e) => { });
						builder.Create().Show();
						break;
					case Resource.Id.catalog:
						Toast.MakeText(this, "Catalog pressed", ToastLength.Long).Show();
						//StartActivity(new Intent(this, typeof(SettingsActivity)));
						break;
					case Resource.Id.user:
						Toast.MakeText(this, "User pressed", ToastLength.Long).Show();
						//StartActivity(new Intent(this, typeof(SettingsActivity)));
						break;
					case Resource.Id.about:
						Toast.MakeText(this, "About pressed", ToastLength.Long).Show();
						//StartActivity(new Intent(this, typeof(SettingsActivity)));
						break;
				}
			}

			return base.OnOptionsItemSelected(item);
		}

		void Logout()
		{
			SharedPreferencesManager.ClearAll();
			StartActivity(new Intent(this, typeof(LoginActivity)));
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
				Toast.MakeText(this, $"{exception.InnerException.Message}", ToastLength.Long).Show();
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
				Toast.MakeText(this, $"{exception.InnerException.Message}", ToastLength.Long).Show();
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
				Toast.MakeText(this, $"{exception.InnerException.Message}", ToastLength.Long).Show();
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