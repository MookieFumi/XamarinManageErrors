using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Bumptech.Glide;
using Xamarin.Droid.Infrastructure;
using Xamarin.Services.Veemer.DTO;

namespace Xamarin.Droid.Activities.Catalog
{

	//----------------------------------------------------------------------
	// ADAPTER
	// Adapter to connect the data set (photo album) to the RecyclerView: 
	public class CatalogAdapter : RecyclerView.Adapter
	{
		public IEnumerable<Product> _products;
		private Context _context;

		public CatalogAdapter(Context context, IEnumerable<Product> catalogQueryResult)
		{
			_context = context;
			_products = catalogQueryResult;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CatalogProduct, parent, false);
			var viewHolder = new CatalogProductViewHolder(itemView);
			return viewHolder;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var catalogProductViewHolder = holder as CatalogProductViewHolder;
			var product = _products.ElementAt(holder.AdapterPosition);

			Glide.With(_context).Load(product.ThumbnailImagelUrl)
				 .Placeholder(Resource.Drawable.default_product_image)
				 .Into(catalogProductViewHolder.ThumbnailImagelUrl);

			//var imageBitmap = ImageHelper.GetImageBitmapFromUrl(product.ThumbnailImagelUrl);
			//catalogProductViewHolder.ThumbnailImagelUrl.SetImageBitmap(imageBitmap);
			catalogProductViewHolder.ItemPosition.Text = $"#{position}";
			catalogProductViewHolder.ProductCode.Text = product.ProductCode;
			catalogProductViewHolder.RootProductCode.Text = product.RootProductCode;
			catalogProductViewHolder.ProductDescription.Text = product.ProductDescription;
		}

		public override int ItemCount
		{
			get { return _products.Count(); }
		}
	}
}