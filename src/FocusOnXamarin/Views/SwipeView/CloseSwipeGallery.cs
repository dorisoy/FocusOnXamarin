using Xamarin.Forms;

namespace FocusOnXamarin.Views
{
	public class CloseSwipeGallery : ContentPage
	{
		public CloseSwipeGallery()
		{
			Title = "Close SwipeView Gallery";
   
			var swipeLayout = new StackLayout
			{
				Margin = new Thickness(12)
			};
   
			var closeButton = new Button
			{
				Text = "Close SwipeView"
			};

			swipeLayout.Children.Add(closeButton);

			var swipeItem = new SwipeItem
			{
				BackgroundColor = Color.Red,
				IconImageSource = "delete.png",
				Text = "File"
			};

			swipeItem.Invoked += (sender, e) => { DisplayAlert("SwipeView", "File Invoked", "Ok"); };

			var swipeItems = new SwipeItems { swipeItem };

			swipeItems.Mode = SwipeMode.Reveal;

			var swipeContent = new Grid
			{
				BackgroundColor = Color.Gray
			};

			var fileSwipeLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Swipe to Right (File)"
			};

			swipeContent.Children.Add(fileSwipeLabel);

			var swipeView = new SwipeView
			{
				HeightRequest = 60,
				WidthRequest = 300,
				LeftItems = swipeItems,
				Content = swipeContent
			};

			swipeLayout.Children.Add(swipeView);

			Content = swipeLayout;

			closeButton.Clicked += (sender, e) =>
			{
				swipeView.Close();
			};
		}
	}
}