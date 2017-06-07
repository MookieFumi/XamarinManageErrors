using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace XamarinManageErrors.Droid.Activities.Catalog
{

	//----------------------------------------------------------------------
	// VIEW HOLDER
	// Implement the ViewHolder pattern: each ViewHolder holds references
	// to the UI components (ImageView and TextView) within the CardView 
	// that is displayed in a row of the RecyclerView:
	public class CatalogProductViewHolder : RecyclerView.ViewHolder
	{
		public ImageView ThumbnailImagelUrl { get; private set; }
		public TextView ItemPosition { get; private set; }
		public TextView ProductCode { get; private set; }
		public TextView RootProductCode { get; private set; }
		public TextView ProductDescription { get; private set; }

		// Get references to the views defined in the CardView layout.
		public CatalogProductViewHolder(View view)
			: base(view)
		{
			// Locate and cache view references:
			ThumbnailImagelUrl = view.FindViewById<ImageView>(Resource.Id.thumbnail);
			ItemPosition = view.FindViewById<TextView>(Resource.Id.itemPosition);
			ProductCode = view.FindViewById<TextView>(Resource.Id.productCode);
			RootProductCode = view.FindViewById<TextView>(Resource.Id.rootProductCode);
			ProductDescription = view.FindViewById<TextView>(Resource.Id.productDescription);
		}
	}
}