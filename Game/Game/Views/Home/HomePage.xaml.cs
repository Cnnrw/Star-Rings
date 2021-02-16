using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     The Main Home Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public HomePage()
        {
            InitializeComponent();

            // Remove the nav bar on the home page
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        ///     Jump to the Dungeon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void DungeonButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new PickCharactersPage());

        /// <summary>
        ///     Jump to the Rebel Base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void RebelBaseButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new RebelBasePage());

        /// <summary>
        ///     Jump to the Dungeon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AutobattleButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new AutoBattlePage());

        /// <summary>
        /// Jump to Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SettingsButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new SettingsPage());

        /// <summary>
        /// Jump to About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AboutButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new AboutPage());
    }
}