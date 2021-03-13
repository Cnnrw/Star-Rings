using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Game.Controls.TabView
{
	[Preserve(AllMembers = true)]
	class WindowsTabViewItemTemplate : Grid
	{
		readonly Image _icon;
		readonly Label _text;

		public WindowsTabViewItemTemplate()
		{
			RowSpacing = 0;

			HorizontalOptions = LayoutOptions.FillAndExpand;
			VerticalOptions = LayoutOptions.FillAndExpand;

			RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
			RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			_icon = new Image
			{
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(0, 6, 0, 0)
			};

			_text = new Label
			{
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(0, 6)
			};

			Children.Add(_icon);
			Children.Add(_text);

			SetRow(_icon, 0);
			SetRow(_text, 1);
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();

			BindingContext = Parent;

			_icon.SetBinding(Image.SourceProperty, "CurrentIcon");

			_text.SetBinding(Label.TextProperty, "Text");
			_text.SetBinding(Label.TextColorProperty, "CurrentTextColor");
			_text.SetBinding(Label.FontSizeProperty, "CurrentFontSize");
			_text.SetBinding(Label.FontAttributesProperty, "CurrentFontAttributes");
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);

			UpdateLayout();
		}

		void UpdateLayout()
		{
			if (!(BindingContext is TabViewItem tabViewItem))
				return;

			if (tabViewItem.CurrentIcon == null)
			{
				SetRow(_text, 0);
				SetRowSpan(_text, 2);
			}
			else
			{
				SetRow(_text, 1);
				SetRowSpan(_text, 1);
			}
		}
	}
}
