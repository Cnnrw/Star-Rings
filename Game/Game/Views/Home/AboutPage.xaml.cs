using Game.Services;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     About Page
    /// </summary>
    public partial class AboutPage : ContentPage
    {
        readonly INavigationService _navigationService;

        internal AboutPage(bool unitTests) { }

        /// <summary>
        ///     Constructor for About Page
        /// </summary>
        public AboutPage() : this(App.NavigationService)
        { }

        public AboutPage(INavigationService navigationService)
        {
            InitializeComponent();

            _navigationService = navigationService;
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.GoBack();
            return true;
        }
    }
}
