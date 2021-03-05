using Game.Services;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Game.Views
{
    /// <summary>
    ///     Main Page
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        ///     Unit test c'tor
        /// </summary>
        /// <param name="unitTests"></param>
        internal MainPage(bool unitTests) { }

        public MainPage() : this(App.NavigationService)
        { }

        public MainPage(INavigationService navigationService)
        {
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);

            BindingContext = new MainViewModel(navigationService);
        }
    }
}
