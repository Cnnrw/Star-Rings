using System;

using Game.Templates.Pages;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	public partial class BattleHomePage : ModalPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public BattleHomePage () =>
			InitializeComponent ();

        /// <summary>
        /// Navigate to the auto-battle page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AutoBattle_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new AutoBattlePage());

        /// <summary>
        /// Navigate to the pick characters page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void PickCharacters_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new PickCharactersPage());

        /// <summary>
        /// Navigate to the new round page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void PickItems_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new PickItemsPage());
    }
}
