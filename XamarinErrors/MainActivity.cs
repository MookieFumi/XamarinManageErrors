using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using XamarinErrors.Services;
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

            SetContentView(Resource.Layout.Main);

            var button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button1_Click;

            var button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += Button2_Click;

            var button3 = FindViewById<Button>(Resource.Id.button3);
            button3.Click += Button3_Click;

            var button4 = FindViewById<Button>(Resource.Id.button4);
            button4.Click += Button4_Click;

            var button5 = FindViewById<Button>(Resource.Id.button5);
            button5.Click += Button5_Click;

            var button6 = FindViewById<Button>(Resource.Id.button6);
            button6.Click += Button6_Click;
        }

        private async void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                var value = FindViewById<EditText>(Resource.Id.editText2).Text;
                var brandsService = new BrandsService(new Authorization(GetEmail(), GetPassword()));
                var response = await brandsService.GetClassificationLevelAsync(Convert.ToInt32(value));
                Toast.MakeText(this, $"Catalog: {response.Count()} items. First product: {response.First().Level}",
                    ToastLength.Long).Show();
            }
            catch (HttpWebApiException exception)
            {
                Toast.MakeText(this, $"Exception: {exception.InnerException.Message}", ToastLength.Long).Show();
            }
        }

        private async void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                var value = FindViewById<EditText>(Resource.Id.editText1).Text;
                var catalogService = new CatalogService(new Authorization(GetEmail(), GetPassword()));
                var response = await catalogService.GetAsync(Convert.ToInt32(value));
                Toast.MakeText(this, $"Catalog: {response.TotalCount} items.", ToastLength.Long).Show();
            }
            catch (HttpWebApiException exception)
            {
                Toast.MakeText(this, $"Exception: {exception.InnerException.Message}", ToastLength.Long).Show();
            }
        }

        private async void Button4_Click(object sender, EventArgs e)
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
        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            var peopleService = new PeopleService();
            var response = await peopleService.GetAllAsync();
            Toast.MakeText(this, $"There are {response.Count} people. First one is {response.Results.First().Name}",
                ToastLength.Long).Show();
        }

        private void Button2_Click(object sender, EventArgs e)
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

        private void Button1_Click(object sender, EventArgs e)
        {
            throw new OutOfMemoryException("AnalyticAlways Exception");
        }

        private string GetEmail()
        {
            return FindViewById<EditText>(Resource.Id.editText3).Text;
        }

        private string GetPassword()
        {
            return FindViewById<EditText>(Resource.Id.editText4).Text;
        }
    }
}