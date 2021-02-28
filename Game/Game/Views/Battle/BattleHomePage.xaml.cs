using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Engine.EngineKoenig;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleHomePage : ContentPage
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public BattleHomePage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// Navigate to the auto-battle page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AutoBattle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutoBattlePage());
        }

        /// <summary>
        /// Navigate to the pick characters page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void PickCharacters_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickCharactersPage());
        }
    }
}
