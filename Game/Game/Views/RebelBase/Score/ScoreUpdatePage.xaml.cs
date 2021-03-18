using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Score Update Page
    /// </summary>
    public partial class ScoreUpdatePage : BaseContentPage
    {
        // View Model for Score
        internal readonly GenericViewModel<ScoreModel> _viewModel;

        // Constructor for Unit Testing
        internal ScoreUpdatePage(bool unitTest) { }

        /// <summary>
        /// Constructor that takes and existing data Score
        /// </summary>
        public ScoreUpdatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            BindingContext = _viewModel = data;

            UpdatePageBindingContext();
        }

        /// <summary>
        ///     Updates the BindingContext to trigger a refresh
        /// </summary>
        private void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;
            _viewModel.Data = data;
            PageTitle= $"Update {data.Name}";

            BindingContext = _viewModel;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // Don't allow users to input an empty name
            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your score a name", "OK");
                return;
            }

            MessagingCenter.Send(this, "Update", _viewModel.Data);
            await App.NavigationService.GoBack();
        }
    }
}
