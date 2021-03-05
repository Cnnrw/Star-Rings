using Game.Services;

using Xamarin.Forms;

namespace Game
{
    /// <summary>
    ///     Main Application entry point
    /// </summary>
    public partial class App : Application
    {
        internal static INavigationService NavigationService { get; } = new ViewNavigationService();

        /// <summary>
        ///     Default App Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();

            NavigationService.Configure("MainPage", typeof(Views.MainPage));

            // Call the Landing Page to open
            MainPage = ((ViewNavigationService)NavigationService).SetRootPage("MainPage");
        }

        /// <summary>
        ///     On Startup code if needed
        /// </summary>
        protected override void OnStart()
        {
            // Add each model here to warm up and load it.
            Helpers.DataSetsHelper.WarmUp();
        }

        /// <summary>
        ///     On Sleep code if needed
        /// </summary>
        protected override void OnSleep()
        { }

        /// <summary>
        ///     On App Resume code if needed
        /// </summary>
        protected override void OnResume()
        { }
    }
}
