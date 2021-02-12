using System;
using System.ComponentModel;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     Character Read Page code-behind
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterReadPage : ContentPage
    {
        // View Model for Item
        private readonly GenericViewModel<CharacterModel> _viewModel;

        // Empty Constructor for UTs
        public CharacterReadPage(bool UnitTest) { }

        /// <summary>
        ///     Constructor called with a view model
        ///     This is the primary way to open the page
        ///     The ViewModel is the data that should be displayed
        /// </summary>
        /// <param name="data"></param>
        public CharacterReadPage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            _viewModel = data;
            _viewModel.Title = data.Data.Name;

            BindingContext = _viewModel;
        }

        /// <summary>
        ///     Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterUpdatePage(_viewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        ///     Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterDeletePage(_viewModel)));
            await Navigation.PopAsync();
        }
    }
}