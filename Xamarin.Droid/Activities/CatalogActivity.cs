using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Xamarin.Services.Veemer;
using Xamarin.Services.Veemer.DTO;
using Xamarin.Droid.Infrastructure;

namespace Xamarin.Droid.Activities
{
	[Activity(Icon = "@mipmap/icon")]
	public class CatalogActivity : BaseAuthorizedActivity
	{
		// RecyclerView instance that displays the catalog
		RecyclerView _recyclerView;
		// Layout manager that lays out each card in the RecyclerView
		RecyclerView.LayoutManager _layoutManager;
		// Adapter that accesses the data set
		CatalogAdapter _catalogAdapter;
		VeemerQueryResult<Product> catalog;

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Catalog);

			var catalogService = new CatalogService(new Services.Veemer.Authorization(UserName, Password, PasswordType.Password));
			catalog = await catalogService.GetAsync(LoginResponse.Shop.Id);

			_recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
			_layoutManager = new LinearLayoutManager(this);

			var onScrollListener = new RecyclerViewOnScrollListener(_layoutManager);
			onScrollListener.LoadMoreEvent += (int page, int totalItemsCount) =>
			{
				//TODO: load new page
				Console.WriteLine($"page: {page}");
			};
			_recyclerView.AddOnScrollListener(onScrollListener);

			_recyclerView.SetLayoutManager(_layoutManager);
			_catalogAdapter = new CatalogAdapter(catalog);
			_recyclerView.SetAdapter(_catalogAdapter);
		}


		//----------------------------------------------------------------------
		// VIEW HOLDER

		// Implement the ViewHolder pattern: each ViewHolder holds references
		// to the UI components (ImageView and TextView) within the CardView 
		// that is displayed in a row of the RecyclerView:
		public class CatalogProductViewHolder : RecyclerView.ViewHolder
		{
			public ImageView ThumbnailImagelUrl { get; private set; }
			public TextView ProductCode { get; private set; }
			public TextView RootProductCode { get; private set; }
			public TextView ProductDescription { get; private set; }

			// Get references to the views defined in the CardView layout.
			public CatalogProductViewHolder(View view)
				: base(view)
			{
				// Locate and cache view references:
				ThumbnailImagelUrl = view.FindViewById<ImageView>(Resource.Id.thumbnail);
				ProductCode = view.FindViewById<TextView>(Resource.Id.productCode);
				RootProductCode = view.FindViewById<TextView>(Resource.Id.rootProductCode);
				ProductDescription = view.FindViewById<TextView>(Resource.Id.productDescription);
			}
		}

		//----------------------------------------------------------------------
		// ADAPTER
		// Adapter to connect the data set (photo album) to the RecyclerView: 
		public class CatalogAdapter : RecyclerView.Adapter
		{
			public VeemerQueryResult<Product> _catalogQueryResult;

			public CatalogAdapter(VeemerQueryResult<Product> catalogQueryResult)
			{
				_catalogQueryResult = catalogQueryResult;
			}

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
			{
				var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CatalogProduct, parent, false);
				var viewHolder = new CatalogProductViewHolder(itemView);
				return viewHolder;
			}

			public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
			{
				var viewHolder = holder as CatalogProductViewHolder;
				var result = _catalogQueryResult.Result.ToList();

				Console.WriteLine($"position: {position} - adapter position: {holder.AdapterPosition}");
				//var imageBitmap = ImageHelper.GetImageBitmapFromUrl(result[position].ThumbnailImagelUrl);
				//viewHolder.ThumbnailImagelUrl.SetImageBitmap(imageBitmap);
				var product = result.ElementAt(holder.AdapterPosition);
				viewHolder.ProductCode.Text = product.ProductCode;
				viewHolder.RootProductCode.Text = product.RootProductCode;
				viewHolder.ProductDescription.Text = product.ProductDescription;
			}

			public override int ItemCount
			{
				get { return _catalogQueryResult.Result.Count(); }
			}
		}
	}
}