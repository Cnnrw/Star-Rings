using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RebelBasePage : ContentPage
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public RebelBasePage()
        {
            InitializeComponent();
            // Remove the nav bar on the home page
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        ///     Jump to the Monsters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void MonstersButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new MonsterIndexPage());

        /// <summary>
        ///     Jump to the Characters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CharactersButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new CharacterIndexPage());

        /// <summary>
        ///     Jump to the Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ItemsButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new ItemIndexPage());

        /// <summary>
        ///     Jump to the Scores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ScoresButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new ScoreIndexPage());
    }
}