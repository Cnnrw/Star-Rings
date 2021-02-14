using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Controls
{
    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicItemView : ContentView
    {

        public BasicItemView() => InitializeComponent();
        #region Bindable Properties

        public static readonly BindableProperty ItemNameProperty =
            BindableProperty.Create(nameof(ItemName), typeof(string), typeof(BasicItemView), string.Empty);
        public static readonly BindableProperty ItemDescriptionProperty =
            BindableProperty.Create(nameof(ItemDescription), typeof(string), typeof(BasicItemView), string.Empty);
        public static readonly BindableProperty IconImageSourceProperty =
            BindableProperty.Create(nameof(IconImageSource), typeof(ImageSource), typeof(BasicItemView),
                                    default(ImageSource));

        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }

        public string ItemDescription
        {
            get => (string)GetValue(ItemDescriptionProperty);
            set => SetValue(ItemDescriptionProperty, value);
        }

        public ImageSource IconImageSource
        {
            get => (ImageSource)GetValue(IconImageSourceProperty);
            set => SetValue(IconImageSourceProperty, value);
        }

        #endregion
    }
}