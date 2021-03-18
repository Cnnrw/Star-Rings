using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    public partial class ScoreIndexPage : BaseContentPage
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
