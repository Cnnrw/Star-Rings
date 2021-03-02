using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickItemsPage : ContentPage
    {
        // Empty Constructor for UTs
        public PickItemsPage(bool UnitTest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage()
        {
            InitializeComponent();

            BindingContext = BattleEngineViewModel.Instance;
            //BindingContext = BattleEngineViewModel.Instance;

            // UpdateNextButtonState();
        }

        /// <summary>
        /// Automatically distribute items amongst the character party
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DistributeButton_Clicked(object sender, EventArgs e)
        {
            // TODO: implement distribution
        }

        /// <summary>
        /// Handles the select event for party members
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPartyMemberSelected(object sender, EventArgs e)
        {
            // TODO: implement
        }

        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
