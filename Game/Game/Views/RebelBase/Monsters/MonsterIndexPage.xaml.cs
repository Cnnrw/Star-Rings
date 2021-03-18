using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    ///     Monster Index Page
    /// </summary>
    public partial class MonsterIndexPage : BaseContentPage
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
