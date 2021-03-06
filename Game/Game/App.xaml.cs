using Game.Services;
using Game.Views;

using Xamarin.Forms;

namespace Game
{
    /// <summary>
    ///     Main Application entry point
    /// </summary>
    public partial class App : Application
    {
        internal static INavigationService NavigationService { get; private set; }

        /// <summary>
        ///     Default App Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Create NavigationService Instance
            var navigationService = new ViewNavigationService();

            AddPagesToNavigation(navigationService);

            MainPage = navigationService.SetRootPage(nameof(LandingPage));

            NavigationService = navigationService;
        }

        static void AddPagesToNavigation(INavigationService navigationService)
        {
            // Landing
            navigationService.Configure(nameof(LandingPage), typeof(LandingPage));

            // MainPage
            navigationService.Configure(nameof(MainPage), typeof(MainPage));
            navigationService.Configure(nameof(SettingsPage), typeof(SettingsPage));
            navigationService.Configure(nameof(AboutPage), typeof(AboutPage));

            // Rebel Base Pages
            navigationService.Configure(nameof(RebelBasePage), typeof(RebelBasePage));

            navigationService.Configure(nameof(CharacterIndexPage), typeof(CharacterIndexPage));
            navigationService.Configure(nameof(CharacterReadPage), typeof(CharacterReadPage));
            navigationService.Configure(nameof(CharacterUpdatePage), typeof(CharacterUpdatePage));
            navigationService.Configure(nameof(CharacterDeletePage), typeof(CharacterDeletePage));
            navigationService.Configure(nameof(CharacterCreatePage), typeof(CharacterCreatePage));

            navigationService.Configure(nameof(MonsterIndexPage), typeof(MonsterIndexPage));
            navigationService.Configure(nameof(MonsterReadPage),   typeof(MonsterReadPage));
            navigationService.Configure(nameof(MonsterUpdatePage), typeof(MonsterUpdatePage));
            navigationService.Configure(nameof(MonsterDeletePage), typeof(MonsterDeletePage));
            navigationService.Configure(nameof(MonsterCreatePage), typeof(MonsterCreatePage));

            navigationService.Configure(nameof(ItemIndexPage), typeof(ItemIndexPage));
            navigationService.Configure(nameof(ItemReadPage),   typeof(ItemReadPage));
            navigationService.Configure(nameof(ItemUpdatePage), typeof(ItemUpdatePage));
            navigationService.Configure(nameof(ItemDeletePage), typeof(ItemDeletePage));
            navigationService.Configure(nameof(ItemCreatePage), typeof(ItemCreatePage));

            navigationService.Configure(nameof(ScoreIndexPage), typeof(ScoreIndexPage));
            navigationService.Configure(nameof(ScoreReadPage),   typeof(ScoreReadPage));
            navigationService.Configure(nameof(ScoreUpdatePage), typeof(ScoreUpdatePage));
            navigationService.Configure(nameof(ScoreDeletePage), typeof(ScoreDeletePage));
            navigationService.Configure(nameof(ScoreCreatePage), typeof(ScoreCreatePage));

            // Dungeon Pages
            navigationService.Configure("Battle", typeof(PickCharactersPage));
            navigationService.Configure(nameof(ScorePage), typeof(ScorePage));
            navigationService.Configure(nameof(RoundOverPage), typeof(RoundOverPage));
            navigationService.Configure(nameof(BattleSettingsPage), typeof(BattleSettingsPage));
            navigationService.Configure(nameof(NewRoundPage), typeof(NewRoundPage));
            navigationService.Configure(nameof(BattlePage), typeof(BattlePage));

            // AutoBattle
            navigationService.Configure(nameof(AutoBattlePage), typeof(AutoBattlePage));
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
