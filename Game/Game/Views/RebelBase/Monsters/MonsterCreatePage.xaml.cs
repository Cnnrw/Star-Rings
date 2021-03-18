using System;

using Game.Enums;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Monster create page
    /// </summary>
    public partial class MonsterCreatePage : BaseContentPage
    {
        /// <summary>
        ///     Internal Monster ViewModel
        /// </summary>
        internal readonly GenericViewModel<MonsterModel> _viewModel;

        /// <summary>
        ///     Empty Constructor for UTs
        /// </summary>
        /// <param name="unitTest"></param>
        internal MonsterCreatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage()
        {
            InitializeComponent();

            _viewModel = new GenericViewModel<MonsterModel>
            {
                Data = RandomPlayerHelper.GetRandomMonster(10)
            };

            UpdatePageBindingContext();
        }

        /// <summary>
        ///
        /// </summary>
        internal void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;

            _viewModel.Data = data;
            PageTitle = "New Monster";

            BindingContext = _viewModel;

            // Set pickers' initially selected items
            ImagePicker.SelectedItem = MonsterImageEnumExtensions.FromImageURI(data.ImageURI);
        }

        /// <summary>
        /// Updates the monster image based on the selected value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnImagePickerChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedImage = (MonsterImageEnum)picker.SelectedIndex;

            MonsterImage.Source = selectedImage.ToImageURI();
            MonsterIconImage.Source = selectedImage.ToIconImageURI();
        }

        /// <summary>
        ///     Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your monster a name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(_viewModel.Data.ImageURI))
            {
                var monsterImg = RandomPlayerHelper.GetMonsterImage();
                _viewModel.Data.ImageURI = monsterImg.ToImageURI();
                _viewModel.Data.IconImageURI = monsterImg.ToIconImageURI();
            }
            else
            {
                var monsterImg = ImagePicker.SelectedItem;
                _viewModel.Data.ImageURI = monsterImg.ToImageURI();
                _viewModel.Data.IconImageURI = monsterImg.ToIconImageURI();
            }

            MessagingCenter.Send(this, "Create", _viewModel.Data);
            await App.NavigationService.GoBack();
        }

        /// <summary>
        ///     Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RandomButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.Data = RandomPlayerHelper.GetRandomMonster(20);
            UpdatePageBindingContext();
        }
    }
}
