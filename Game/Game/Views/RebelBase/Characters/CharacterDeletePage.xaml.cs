using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Character Read Page
    ///     Shows a detailed view of a character
    /// </summary>
    public partial class CharacterDeletePage : BaseContentPage
    {
        readonly GenericViewModel<CharacterModel> _viewModel;

        // Empty Constructor for UTs
        internal CharacterDeletePage(bool unitTest) { }

        /// <summary>
        ///     CharacterDeletePage Constructor
        ///     Receives ViewModel of the character to delete
        /// </summary>
        /// <param name="character"></param>
        public CharacterDeletePage(GenericViewModel<CharacterModel> character)
        {
            InitializeComponent();

            BindingContext = _viewModel = character;

            PageTitle = $"Delete {character.Title}?";
        }

        /// <summary>
        ///     Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", _viewModel.Data);
            await App.NavigationService.GoBack();
        }

        /// <summary>
        ///     Override back button behavior to capture user on screen
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
