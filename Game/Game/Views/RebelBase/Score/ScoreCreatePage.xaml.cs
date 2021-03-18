using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    public partial class ScoreCreatePage : BaseContentPage
    {
        // The item to create
        internal readonly GenericViewModel<ScoreModel> _viewModel;

        // Constructor for Unit Testing
        internal ScoreCreatePage(bool unitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ScoreCreatePage()
        {
            InitializeComponent();

            _viewModel = new GenericViewModel<ScoreModel>
            {
                Data = new ScoreModel()
            };

            BindingContext = _viewModel;

            PageTitle = "Add a score";
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_viewModel.Data.Name))
            {
                if (string.IsNullOrEmpty(_viewModel.Data.ImageURI))
                {
                    _viewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
                }

                MessagingCenter.Send(this, "Create", _viewModel.Data);
                await Navigation.PopModalAsync();
            }

            await DisplayAlert("Hold up!", "Please give your score a name", "OK");
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
