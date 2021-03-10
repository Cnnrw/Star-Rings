using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    public partial class ItemReadPage : BaseContentPage
    {
        // View Model for Item
        readonly GenericViewModel<ItemModel> _viewModel;

        // Empty Constructor for UTs
        internal ItemReadPage(bool unitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="data"></param>
        public ItemReadPage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = _viewModel = data;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemUpdatePage(_viewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemDeletePage(_viewModel)));
            await Navigation.PopAsync();
        }
    }
}
