using Android.Support.V7.Widget;

namespace XamarinManageErrors.Droid.Infrastructure
{

	public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
	{
		public delegate void LoadMoreEventHandler(int page, int totalItemsCount);
		public event LoadMoreEventHandler LoadMoreEvent;

		// The total number of items in the dataset after the last load
		private int _previousTotalItemCount = 0;
		private bool _loading = true;
		private const int VisibleThreshold = 5;
		private int _firstVisibleItem, _visibleItemCount, _totalItemCount;
		private const int StartingPageIndex = 0;
		private int _currentPage = 0;
		private readonly RecyclerView.LayoutManager _layoutManager;

		public RecyclerViewOnScrollListener(RecyclerView.LayoutManager layoutManager)
		{
			_layoutManager = layoutManager;
		}

		public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
		{
			base.OnScrolled(recyclerView, dx, dy);

			var layoutManager = (LinearLayoutManager)_layoutManager;
			_visibleItemCount = recyclerView.ChildCount;
			_totalItemCount = layoutManager.ItemCount;
			_firstVisibleItem = layoutManager.FindFirstVisibleItemPosition();
			OnScroll(_firstVisibleItem, _visibleItemCount, _totalItemCount);
		}

		public void OnScroll(int firstVisibleItem, int visibleItemCount, int totalItemCount)
		{
			// If the total item count is zero and the previous isn't, assume the
			// list is invalidated and should be reset back to initial state
			if (totalItemCount < _previousTotalItemCount)
			{
				_currentPage = StartingPageIndex;
				_previousTotalItemCount = totalItemCount;
				if (totalItemCount == 0)
				{
					_loading = true;
				}
			}
			// If it’s still loading, we check to see if the dataset count has
			// changed, if so we conclude it has finished loading and update the current page
			// number and total item count.
			if (_loading && (totalItemCount > _previousTotalItemCount))
			{
				_loading = false;
				_previousTotalItemCount = totalItemCount;
				_currentPage++;
			}

			// If it isn’t currently loading, we check to see if we have breached
			// the visibleThreshold and need to reload more data.
			// If we do need to reload some more data, we execute onLoadMore to fetch the data.
			if (!_loading && (totalItemCount - visibleItemCount) <= (firstVisibleItem +
					VisibleThreshold))
			{
				LoadMoreEvent(_currentPage + 1, totalItemCount);
				_loading = true;
			}
		}

		// Defines the process for actually loading more data based on page
		//public abstract void LoadMoreEvent(int page, int totalItemsCount);
	}
}
