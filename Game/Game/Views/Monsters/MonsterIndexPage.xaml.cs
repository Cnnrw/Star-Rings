using System;
using System.ComponentModel;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Monster Index Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterIndexPage : ContentPage
    {

        // view model used for data binding
        private readonly MonsterIndexViewModel _viewModel = MonsterIndexViewModel.Instance;

        // Empty Constructor for UTs
        public MonsterIndexPage(bool unitTest) { }

        /// <summary>
        ///     Default constructor for Monster Index Page.
        ///     Get the ItemIndexView Model
        /// </summary>
        public MonsterIndexPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }

        /// <summary>
        ///     Call to add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddItem_Clicked(object sender, EventArgs e) =>
            await Navigation.PushModalAsync(new NavigationPage(new MonsterCreatePage()));

        /// <summary>
        ///     The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is MonsterModel data))
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new MonsterReadPage(new GenericViewModel<MonsterModel>(data)));

            // Manually deselect item
            MonstersListView.SelectedItem = null;
        }

        /// <summary>
        ///     Refresh the list on page render
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

            // If the needsRefresh flag is set, update it
            if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }
    }
}