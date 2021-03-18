using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    public partial class ItemReadPage : BaseContentPage
    {
        readonly GenericViewModel<ItemModel> _viewModel;

        // Empty Constructor for UTs
        internal ItemReadPage(bool unitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="itemModel"></param>
        public ItemReadPage(GenericViewModel<ItemModel> itemModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = itemModel;
        }
    }
}
