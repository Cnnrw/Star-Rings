using System;
using System.Linq;

using Game.Models;
using Game.Templates.Pages;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    public partial class ScoreIndexPage : ModalPage
    {
        // The view model, used for data binding
        readonly ScoreIndexViewModel _viewModel = ScoreIndexViewModel.Instance;

        // Empty Constructor for UTs
        internal ScoreIndexPage(bool unitTest) { }

        /// <summary>
        /// Constructor for Index Page
        ///
        /// Get the ScoreIndexView Model
        /// </summary>
        public ScoreIndexPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnScoreSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.FirstOrDefault() is ScoreModel data))
                return;

            // Open the Read Page
            await Navigation.PushAsync(new ScoreReadPage(new GenericViewModel<ScoreModel>(data)));

            // Manually deselect item.
            ScoreList.SelectedItem = null;
        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddScore_Clicked(object sender, EventArgs e) =>
            await Navigation
                .PushModalAsync(new NavigationPage(new ScoreCreatePage(new GenericViewModel<ScoreModel>())));

        /// <summary>
        /// Refresh the list on page appearing
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
