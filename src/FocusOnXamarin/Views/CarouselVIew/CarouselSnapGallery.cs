using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FocusOnXamarin.Views
{
	[Preserve(AllMembers = true)]
	public class CarouselSnapGallery : ContentPage
	{
		public CarouselSnapGallery()
		{
			On<iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);

			var viewModel = new CarouselItemsGalleryViewModel();

			Title = $"CarouselView Snap Options";

			var layout = new Grid
			{
				RowDefinitions = new RowDefinitionCollection
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Star }
				}
			};

			var snapPointsStack = new StackLayout
			{
				Margin = new Thickness(12)
			};

			var snapPointsLabel = new Label { FontSize = 10, Text = "SnapPointsType:" };
			var snapPointsTypes = Enum.GetNames(typeof(SnapPointsType)).Select(b => b).ToList();

			var snapPointsTypePicker = new Xamarin.Forms.Picker
            {
				ItemsSource = snapPointsTypes,
				SelectedItem = snapPointsTypes[1]
			};

			snapPointsStack.Children.Add(snapPointsLabel);
			snapPointsStack.Children.Add(snapPointsTypePicker);

			layout.Children.Add(snapPointsStack, 0, 0);

			var snapPointsAlignmentsStack = new StackLayout
			{
				Margin = new Thickness(12)
			};

			var snapPointsAlignmentsLabel = new Label { FontSize = 10, Text = "SnapPointsAlignment:" };
			var snapPointsAlignments = Enum.GetNames(typeof(SnapPointsAlignment)).Select(b => b).ToList();

			var snapPointsAlignmentPicker = new Xamarin.Forms.Picker
            {
				ItemsSource = snapPointsAlignments,
				SelectedItem = snapPointsAlignments[0]
			};

			snapPointsAlignmentsStack.Children.Add(snapPointsAlignmentsLabel);
			snapPointsAlignmentsStack.Children.Add(snapPointsAlignmentPicker);

			layout.Children.Add(snapPointsAlignmentsStack, 0, 1);

			var itemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
			{
				SnapPointsType = SnapPointsType.Mandatory,
				SnapPointsAlignment = SnapPointsAlignment.Start
			};

			var itemTemplate = GetCarouselTemplate();

			var carouselView = new CarouselView
			{
				ItemsLayout = itemsLayout,
				ItemTemplate = itemTemplate,
				BackgroundColor = Color.LightGray,
				PeekAreaInsets = new Thickness(0, 0, 100, 0),
				Margin = new Thickness(12),
				AutomationId = "TheCarouselView"
			};

			carouselView.SetBinding(CarouselView.ItemsSourceProperty, "Items");

			layout.Children.Add(carouselView, 0, 2);


			snapPointsTypePicker.SelectedIndexChanged += (sender, e) =>
			{
				if (carouselView.ItemsLayout is LinearItemsLayout linearItemsLayout)
				{
					Enum.TryParse(snapPointsTypePicker.SelectedItem.ToString(), out SnapPointsType snapPointsType);
					linearItemsLayout.SnapPointsType = snapPointsType;
				}
			};

			snapPointsAlignmentPicker.SelectedIndexChanged += (sender, e) =>
			{
				if (carouselView.ItemsLayout is LinearItemsLayout linearItemsLayout)
				{
					Enum.TryParse(snapPointsAlignmentPicker.SelectedItem.ToString(), out SnapPointsAlignment snapPointsAlignment);
					linearItemsLayout.SnapPointsAlignment = snapPointsAlignment;
				}
			};

			Content = layout;
			BindingContext = viewModel;
		}

		internal DataTemplate GetCarouselTemplate()
		{
			return new DataTemplate(() =>
			{
				var grid = new Grid();

				var info = new Label
				{
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Margin = new Thickness(6)
				};

				info.SetBinding(Label.TextProperty, new Binding("Name"));

				grid.Children.Add(info);

				var frame = new Frame
				{
					Content = grid,
					HasShadow = false
				};

				frame.SetBinding(BackgroundColorProperty, new Binding("Color"));

				return frame;
			});
		}
	}
}