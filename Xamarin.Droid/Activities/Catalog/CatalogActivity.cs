using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Xamarin.Droid.Infrastructure;
using Xamarin.Services.Veemer;
using Xamarin.Services.Veemer.DTO;

namespace Xamarin.Droid.Activities.Catalog
{
	[Activity(Icon = "@mipmap/icon")]
	public class CatalogActivity : BaseAuthorizedActivity
	{
		RecyclerView _recyclerView;
		GridLayoutManager _layoutManager;
		CatalogAdapter _catalogAdapter;

		CatalogService _catalogService;
		VeemerQueryResult<Product> _queryResult;
		List<Product> _products = new List<Product>();

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Catalog);

			_queryResult = await GetCatalogQueryResult(LoginResponse.Shop.Id);
			_recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
			//_layoutManager = new LinearLayoutManager(this);
			_layoutManager = new GridLayoutManager(this, 2);

			var onScrollListener = new RecyclerViewOnScrollListener(_layoutManager);
			onScrollListener.LoadMoreEvent += async (int page, int totalItemsCount) =>
			{
				if (_queryResult.HasNextPage)
				{
					_queryResult = await GetCatalogQueryResult(LoginResponse.Shop.Id, page);
					Console.WriteLine($"page: {page} - totalItemsCount: {totalItemsCount}");
				}
			};

			_recyclerView.AddOnScrollListener(onScrollListener);
			_recyclerView.SetLayoutManager(_layoutManager);
			_catalogAdapter = new CatalogAdapter(this, _products);
			_recyclerView.SetAdapter(_catalogAdapter);
		}

		async Task<VeemerQueryResult<Product>> GetCatalogQueryResult(int shopId, int page = 1)
		{
			_catalogService = new CatalogService(new Authorization(UserName, Password, PasswordType.Password));
			var result = await _catalogService.GetAsync(shopId, page);

			_products.AddRange(result.Result);

			if (_catalogAdapter != null)
			{
				_catalogAdapter.NotifyDataSetChanged();
			}

			return result;
		}
	}
}