using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    public partial class ScoreReadPage : BaseContentPage
    {
        // View Model for Score
        readonly GenericViewModel<ScoreModel> _viewModel;

        internal ScoreReadPage(bool unitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="score"></param>
        public ScoreReadPage(GenericViewModel<ScoreModel> score)
        {
            InitializeComponent();

            BindingContext = _viewModel = score;
        }
    }
}
