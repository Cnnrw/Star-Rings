using System;
using System.ComponentModel;

using Game.GameRules;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     Update a character
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        // The Character to update
        private readonly GenericViewModel<CharacterModel> _viewModel;

        // Empty Constructor for UTs
        public CharacterUpdatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = _viewModel = data;

            LoadLevelPickerValues();

            UpdatePageBindingContext();
        }

        /// <summary>
        ///     Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        private void UpdatePageBindingContext()
        {
            // Temp store off the level
            var data = _viewModel.Data;

            // Clear the Binding and reset it
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = data.Name;

            BindingContext = _viewModel;

            // this resets the Picker to the Character's level
            LevelPicker.SelectedIndex = _viewModel.Data.Level - 1;

            JobPicker.SelectedItem = _viewModel.Data.Job.ToString();
        }

        #region DataCRUDi

        /// <summary>
        ///     Save by calling for Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your character a name", "OK");
            }
            else
            {
                MessagingCenter.Send(this, "Update", _viewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        ///     Cancel the Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

        // /// <summary>
        // ///     Randomizes the Character values and items
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // private void RandomButton_Clicked(object sender, EventArgs e)
        // {
        //     _viewModel.Data.Update(RandomPlayerHelper.GetRandomCharacter(20));
        //     UpdatePageBindingContext();
        // }

        #endregion

        #region Pickers

        /// <summary>
        /// Updates the Character's ImageURI and the page's image source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnJobPickerChanged(object sender, EventArgs e)
        {
            string iconImageURI = CharacterJobEnumExtensions.ToIconImageURI(_viewModel.Data.Job);

            _viewModel.Data.ImageURI = iconImageURI;
            JobImage.Source = iconImageURI;
        }

        /// <summary>
        ///     Propagates the values for the character level picker
        /// </summary>
        private void LoadLevelPickerValues()
        {
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                LevelPicker.Items.Add(i.ToString());
            }

            LevelPicker.SelectedIndex = -1;
        }

        /// <summary>
        ///     Updates character level based on value selected from picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LevelPicker_Changed(object sender, EventArgs args)
        {
            // If the Picker is not set, then set it
            if (LevelPicker.SelectedIndex == -1)
            {
                LevelPicker.SelectedIndex = _viewModel.Data.Level - 1;
                return;
            }

            var result = LevelPicker.SelectedIndex + 1;

            // If the level hasn't changed, don't roll for health
            if (result == _viewModel.Data.Level)
            {
                return;
            }

            // Change character level
            _viewModel.Data.Level = result;

            // Roll for new HP
            _viewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(_viewModel.Data.Level);

            UpdateHealthValue();
        }

        /// <summary>
        ///     Updates character max health based on new level
        /// </summary>
        private void UpdateHealthValue() => MaxHealthValue.Text = _viewModel.Data.MaxHealth.ToString();

        #endregion

        #region Steppers

        // /// <summary>
        // ///     Changes Level attribute of a Character
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // private void OnLevelStepperChanged(object sender, ValueChangedEventArgs e) =>
        //     LevelValueLabel.Text = $"{e.NewValue}";

        // /// <summary>
        // ///     Changes Max Health attribute of a Character
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // private void OnMaxHealthStepperChanged(object sender, ValueChangedEventArgs e) =>
        //     MaxHealthValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Attack attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAttackStepperChanged(object sender, ValueChangedEventArgs e) =>
            AttackValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Defense attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDefenseStepperChanged(object sender, ValueChangedEventArgs e) =>
            DefenseValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Speed attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSpeedStepperChanged(object sender, ValueChangedEventArgs e) =>
            SpeedValueLabel.Text = $"{e.NewValue}";

        #endregion
    }
}