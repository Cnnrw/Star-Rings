using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    public partial class ScoreDeletePage : BaseContentPage
    {
        // View Model for Item
        readonly GenericViewModel<ScoreModel> _viewModel;

        // Constructor for Unit Testing
        internal ScoreDeletePage(bool unitTest) { }


        // Constructor for Delete takes a view model of what to delete
        public ScoreDeletePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            BindingContext = _viewModel = data;

            _viewModel.Title = "Delete " + data.Title;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", _viewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Trap the Back Button on the Phone
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // Add your code here...
            return true;
        }
    }
}
