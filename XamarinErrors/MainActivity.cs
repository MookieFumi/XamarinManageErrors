using Android.App;
using Android.Widget;
using Android.OS;
using System;
using XamarinErrors.Services;
using System.Linq;
using System.Threading;
using Android.Content;
using XamarinErrors.Services.StarWars;
using XamarinErrors.Services.Veemer;

namespace XamarinErrors
{
	[Activity(Label = "XamarinErrors", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			var button1 = FindViewById<Button>(Resource.Id.button1);
			button1.Click += Button1_Click;

			var button2 = FindViewById<Button>(Resource.Id.button2);
			button2.Click += Button2_Click;

			var button3 = FindViewById<Button>(Resource.Id.button3);
			button3.Click += async (sender, e) =>
			{
				var peopleService = new PeopleService();
				var response = await peopleService.GetAllAsync();
				Toast.MakeText(this, $"There are {response.Count} people. First one is {response.Results.First().Name}", ToastLength.Long).Show();
			};

			var button4 = FindViewById<Button>(Resource.Id.button4);
			button4.Click += async (sender, e) =>
			{
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
			};

			var button5 = FindViewById<Button>(Resource.Id.button5);
			button5.Click += async (sender, e) =>
			{
				try
				{
					string value = FindViewById<EditText>(Resource.Id.editText1).Text;
					var catalogService = new CatalogService(new Authorization(GetEmail(), GetPassword()));
					var response = await catalogService.GetAsync(Convert.ToInt32(value));
					Toast.MakeText(this, $"Catalog: {response.TotalCount} items.", ToastLength.Long).Show();
				}
				catch (HttpWebApiException exception)
				{
					Toast.MakeText(this, $"Exception: {exception.InnerException.Message}", ToastLength.Long).Show();
				}
			};

			var button6 = FindViewById<Button>(Resource.Id.button6);
			button6.Click += async (sender, e) =>
			{
				try
				{
					string value = FindViewById<EditText>(Resource.Id.editText2).Text;
					var brandsService = new BrandsService(new Authorization(GetEmail(), GetPassword()));
					var response = await brandsService.GetClassificationLevelAsync(Convert.ToInt32(value));
					Toast.MakeText(this, $"Catalog: {response.Count()} items. First product: {response.First().Level}", ToastLength.Long).Show();
				}
				catch (HttpWebApiException exception)
				{
					Toast.MakeText(this, $"Exception: {exception.InnerException.Message}", ToastLength.Long).Show();
				}
			};
		}

		string GetEmail() { 
			return FindViewById<EditText>(Resource.Id.editText3).Text;
		}

		string GetPassword()
		{
			return FindViewById<EditText>(Resource.Id.editText4).Text;
		}

		void Button1_Click(object sender, EventArgs e)
		{
			throw new OutOfMemoryException("AnalyticAlways Exception");
		}

		void Button2_Click(object sender, EventArgs e)
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
	}
}