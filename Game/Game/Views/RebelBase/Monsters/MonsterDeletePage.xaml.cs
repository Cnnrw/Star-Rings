using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Delete Monster Page
    /// </summary>
    public partial class MonsterDeletePage : BaseContentPage
    {
        // View Model for Item
        readonly GenericViewModel<MonsterModel> _viewModel;

        // Empty Constructor for UTs
        internal MonsterDeletePage(bool unitTest) { }

        // Constructor for Delete takes a view model of what to delete
        public MonsterDeletePage(GenericViewModel<MonsterModel> monster)
        {
            InitializeComponent();

            BindingContext = _viewModel = monster;

            PageTitle = $"Delete {monster.Title}?";
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
