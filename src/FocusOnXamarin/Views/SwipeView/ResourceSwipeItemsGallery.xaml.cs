using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FocusOnXamarin.Views
{
	[Preserve(AllMembers = true)]
	public partial class ResourceSwipeItemsGallery : ContentPage
	{
		public ResourceSwipeItemsGallery()
		{
			InitializeComponent();
			BindingContext = new SwipeViewGalleryViewModel();

			MessagingCenter.Subscribe<SwipeViewGalleryViewModel>(this, "favourite", sender => { DisplayAlert("SwipeView", "Favourite", "Ok"); });
			MessagingCenter.Subscribe<SwipeViewGalleryViewModel>(this, "delete", sender => { DisplayAlert("SwipeView", "Delete", "Ok"); });
		}
	}
}