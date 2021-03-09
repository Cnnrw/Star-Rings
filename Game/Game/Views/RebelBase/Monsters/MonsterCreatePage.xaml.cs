using System;
using System.ComponentModel;

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
    [DesignTimeVisible(false)]
    public partial class MonsterCreatePage : ContentPage
    {
        /// <summary>
        ///     Internal Monster ViewModel
        /// </summary>
        public readonly GenericViewModel<MonsterModel> _viewModel = new GenericViewModel<MonsterModel>();

        /// <summary>
        ///     PopupLocationEnum holds the current location selected
        /// </summary>
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        /// <summary>
        ///     Empty Constructor for UTs
        /// </summary>
        /// <param name="unitTest"></param>
        public MonsterCreatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage()
        {
            InitializeComponent();

            _viewModel.Data = RandomPlayerHelper.GetRandomMonster(1);
            _viewModel.Title = _viewModel.Data.Name;

            BindingContext = _viewModel;

            // Set pickers' initially selected items
            ImagePicker.SelectedItem = "orc.png";
            BattleLocationPicker.SelectedItem = _viewModel.Data.BattleLocation.ToString();
        }

        /// <summary>
        /// Update the page's image source according to the Monster's ImageURI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImagePickerChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex >= 0)
            {
                Image.Source = MonsterModel.ImagesURIs[selectedIndex];
            }
        }

        /// <summary>
        ///     Changes Attack attribute of a Monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAttackStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            AttackValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes defense attribute of a Monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDefenseStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            DefenseValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Speed attribute of a Monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSpeedStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            SpeedValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_viewModel.Data.ImageURI))
            {
                _viewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your monster a name", "OK");
            }
            else
            {
                MessagingCenter.Send(this, "Create", _viewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        ///     Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

        private void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = "Create " + data.Name;

            BindingContext = _viewModel;

            BattleLocationPicker.SelectedItem = _viewModel.Data.BattleLocation.ToString();
        }

        /// <summary>
        ///     Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RandomButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.Data = RandomPlayerHelper.GetRandomMonster(1);
            UpdatePageBindingContext();
        }
    }
}
