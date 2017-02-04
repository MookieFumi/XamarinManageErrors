using Android.Support.V7.Widget;

namespace Xamarin.Droid.Infrastructure
{

	public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
	{
		public delegate void LoadMoreEventHandler(int page, int totalItemsCount);
		public event LoadMoreEventHandler LoadMoreEvent;

		// The total number of items in the dataset after the last load
		private int previousTotalItemCount = 0;
		private bool loading = true;
		private int visibleThreshold = 5;
		int firstVisibleItem, visibleItemCount, totalItemCount;
		private int startingPageIndex = 0;
		private int currentPage = 0;

		private RecyclerView.LayoutManager _layoutManager;

		public RecyclerViewOnScrollListener(RecyclerView.LayoutManager layoutManager)
		{
			_layoutManager = layoutManager;
		}

		public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
		{
			base.OnScrolled(recyclerView, dx, dy);

			var layoutManager = (LinearLayoutManager)_layoutManager;
			visibleItemCount = recyclerView.ChildCount;
			totalItemCount = layoutManager.ItemCount;
			firstVisibleItem = layoutManager.FindFirstVisibleItemPosition();
			OnScroll(firstVisibleItem, visibleItemCount, totalItemCount);
		}

		public void OnScroll(int firstVisibleItem, int visibleItemCount, int totalItemCount)
		{
			// If the total item count is zero and the previous isn't, assume the
			// list is invalidated and should be reset back to initial state
			if (totalItemCount < previousTotalItemCount)
			{
				this.currentPage = this.startingPageIndex;
				this.previousTotalItemCount = totalItemCount;
				if (totalItemCount == 0)
				{
					this.loading = true;
				}
			}
			// If it’s still loading, we check to see if the dataset count has
			// changed, if so we conclude it has finished loading and update the current page
			// number and total item count.
			if (loading && (totalItemCount > previousTotalItemCount))
			{
				loading = false;
				previousTotalItemCount = totalItemCount;
				currentPage++;
			}

			// If it isn’t currently loading, we check to see if we have breached
			// the visibleThreshold and need to reload more data.
			// If we do need to reload some more data, we execute onLoadMore to fetch the data.
			if (!loading && (totalItemCount - visibleItemCount) <= (firstVisibleItem +
					visibleThreshold))
			{
				LoadMoreEvent(currentPage + 1, totalItemCount);
				loading = true;
			}
		}

		// Defines the process for actually loading more data based on page
		//public abstract void LoadMoreEvent(int page, int totalItemsCount);
	}
}
