using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

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

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScoreUpdatePage(_viewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScoreDeletePage(_viewModel)));
            await Navigation.PopAsync();
        }
    }
}
