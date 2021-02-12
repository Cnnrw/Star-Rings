using System;
using System.ComponentModel;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     Character Read Page
    ///     Shows a detailed view of a character
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterDeletePage : ContentPage
    {
        private readonly GenericViewModel<CharacterModel> _viewModel;

        // Empty Constructor for UTs
        public CharacterDeletePage(bool unitTest) { }

        /// <summary>
        ///     CharacterDeletePage Constructor
        ///     Receives ViewModel of the character to delete
        /// </summary>
        /// <param name="character"></param>
        public CharacterDeletePage(GenericViewModel<CharacterModel> character)
        {
            InitializeComponent();

            BindingContext = _viewModel = character;

            _viewModel.Title = "Delete " + character.Title;
        }

        /// <summary>
        ///     Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", _viewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        ///     Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

        /// <summary>
        ///     Trap the Back Button on the Phone
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed() =>
            // TODO: Add your code here...
            true;
    }
}