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
        public ScoreDeletePage(GenericViewModel<ScoreModel> score)
        {
            InitializeComponent();

            BindingContext = _viewModel = score;

            PageTitle = $"Delete {score.Title}?";
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", _viewModel.Data);
            await App.NavigationService.GoBack();
        }

        /// <summary>
        /// Trap the Back Button on the Phone
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
