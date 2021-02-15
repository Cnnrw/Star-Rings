using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemIndexPage : ContentPage
    {
        // The view model, used for data binding
        private readonly ItemIndexViewModel _viewModel = ItemIndexViewModel.Instance;

        // Empty Constructor for UTs
        public ItemIndexPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        ///
        /// Get the ItemIndexView Model
        /// </summary>
        public ItemIndexPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new ItemReadPage(new GenericViewModel<ItemModel>(data)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddItem_Clicked(object sender, EventArgs e) =>
            await Navigation.PushModalAsync(new NavigationPage(new ItemCreatePage()));

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