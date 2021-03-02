using System;
using System.Linq;

using Game.Models;
using Game.Templates.Pages;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Character Index Page
    /// </summary>
    public partial class CharacterIndexPage : ModalPage
    {
        private readonly CharacterIndexViewModel _viewModel = CharacterIndexViewModel.Instance;

        // Empty Constructor for UTs
        internal CharacterIndexPage(bool unitTest) { }

        /// <summary>
        ///     Constructor for Index Page
        ///     Get the CharacterIndexView Model
        /// </summary>
        public CharacterIndexPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }

        /// <summary>
        ///     The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnCharacterSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.FirstOrDefault() is CharacterModel data))
                return;

            // Open the Read Page
            await Navigation.PushAsync(new CharacterReadPage(new GenericViewModel<CharacterModel>(data)));

            // Manually deselect item.
            CharacterList.SelectedItem = null;
        }

        /// <summary>
        ///     Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddCharacter_Clicked(object sender, EventArgs e) =>
            await Navigation.PushModalAsync(new NavigationPage(new CharacterCreatePage()));

        /// <summary>
        ///     Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (_viewModel.Dataset.Count == 0)
            {
                _viewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }
    }
}
