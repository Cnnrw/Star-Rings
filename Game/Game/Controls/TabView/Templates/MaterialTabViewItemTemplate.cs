using Xamarin.CommunityToolkit.Converters;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Game.Controls.TabView
{
	[Preserve(AllMembers = true)]
	class MaterialTabViewItemTemplate : Grid
	{

        readonly Image _icon;
		readonly Label _text;

		public MaterialTabViewItemTemplate()
		{
            var visualFeedback = new VisualFeedbackEffect();
			Effects.Add(visualFeedback);

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
				Margin = new Thickness(0, 6)
			};

			_text = new Label
			{
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
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

			_text.SetBinding(Label.TextProperty, "Text", BindingMode.OneWay, new TextCaseConverter { Type = TextCaseType.Upper });
			_text.SetBinding(Label.TextColorProperty, "CurrentTextColor");
			_text.SetBinding(Label.FontSizeProperty, "CurrentFontSize");
			_text.SetBinding(Label.FontAttributesProperty, "CurrentFontAttributes");
			_text.SetBinding(Label.FontFamilyProperty, "CurrentFontFamily");

			VisualFeedbackEffect.SetFeedbackColor(this, Color.White);
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
