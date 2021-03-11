using System;

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
        public MonsterCreatePage(GenericViewModel<MonsterModel> viewModel = null)
        {
            InitializeComponent();

            _viewModel = viewModel ?? new GenericViewModel<MonsterModel>
            {
                Title = "Create a Monster",
                Data = RandomPlayerHelper.GetRandomMonster(1)
            };

            BindingContext = _viewModel;

            // Set pickers' initially selected items
            ImagePicker.SelectedItem = RandomPlayerHelper.GetMonsterImage();
            BattleLocationPicker.SelectedItem = _viewModel.Data.BattleLocation.ToString();
        }

        /// <summary>
        /// Update the page's image source according to the Monster's ImageURI
        ///
        /// TODO: this throws an index out of range exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImagePickerChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedIndex = picker.SelectedIndex;

            if (selectedIndex >= 0)
                Image.Source = RandomPlayerHelper.MonsterImageURIs[selectedIndex]; //MonsterModel.ImagesURIs[selectedIndex];
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
                _viewModel.Data.ImageURI = RandomPlayerHelper.GetMonsterImage();

            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your monster a name", "OK");
                return;
            }

            MessagingCenter.Send(this, "Create", _viewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        ///     Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e) =>
            await Navigation.PopModalAsync();

        private void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = "Create a Monster";

            BindingContext = _viewModel;

            ImagePicker.SelectedItem = data.ImageURI;
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
