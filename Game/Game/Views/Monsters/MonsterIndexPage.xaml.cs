using System;
using System.ComponentModel;
using System.Linq;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Monster Index Page
    /// </summary>
    public partial class MonsterIndexPage : ContentPage
    {

        // view model used for data binding
        readonly MonsterIndexViewModel _viewModel = MonsterIndexViewModel.Instance;

        // Empty Constructor for UTs
        internal MonsterIndexPage(bool unitTest) { }

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
        /// <param name="args"></param>
        public async void OnMonsterSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.FirstOrDefault() is MonsterModel data))
                return;

            // Open the Read Page
            await Navigation.PushAsync(new MonsterReadPage(new GenericViewModel<MonsterModel>(data)));

            // Manually deselect item
            MonsterList.SelectedItem = null;
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
