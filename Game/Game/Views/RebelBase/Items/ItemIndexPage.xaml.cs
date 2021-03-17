using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    public partial class ItemIndexPage : BaseContentPage
    {
        // The view model, used for data binding
        readonly ItemIndexViewModel _viewModel = ItemIndexViewModel.Instance;

        // Empty Constructor for UTs
        internal ItemIndexPage(bool unitTest) { }

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
