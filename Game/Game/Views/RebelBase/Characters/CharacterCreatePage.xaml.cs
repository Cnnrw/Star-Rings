using System;

using Game.Enums;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Character creation page
    /// </summary>
    public partial class CharacterCreatePage : BaseContentPage
    {
        // The Character to create
        internal readonly GenericViewModel<CharacterModel> _viewModel;

        // Empty Constructor for UTs
        internal CharacterCreatePage(bool unitTest) { }

        /// <summary>
        /// Constructor makes a new model
        /// </summary>
        public CharacterCreatePage(GenericViewModel<CharacterModel> viewModel = null)
        {
            InitializeComponent();

            _viewModel = viewModel ?? new GenericViewModel<CharacterModel>
            {
                Title = "Create a Character",
                Data = new CharacterModel()
            };

            BindingContext = _viewModel;

            JobPicker.SelectedItem = _viewModel.Data.Job.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = "Create a Character";

            BindingContext = _viewModel;

            // ImagePicker.SelectedItem = data.ImageURI;
            JobPicker.SelectedItem = _viewModel.Data.Job.ToString();
        }

        /// <summary>
        /// Update the Character's ImageURI and the page's image source according to the Character's Job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnJobPickerChanged(object sender, EventArgs e)
        {
            var imageURI = _viewModel.Data.Job.ToImageURI();
            var iconImageURI = _viewModel.Data.Job.ToIconImageURI();

            _viewModel.Data.ImageURI = imageURI;
            _viewModel.Data.IconImageURI = iconImageURI;

            JobImage.Source = imageURI;
        }

        /// <summary>
        /// Update the Level value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnLevelStepperChanged(object sender, ValueChangedEventArgs e) => LevelValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the MaxHealth value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMaxHealthStepperChanged(object sender, ValueChangedEventArgs e) => MaxHealthValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the Attack value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAttackStepperChanged(object sender, ValueChangedEventArgs e) => AttackValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the Defense value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDefenseStepperChanged(object sender, ValueChangedEventArgs e) => DefenseValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the Speed value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSpeedStepperChanged(object sender, ValueChangedEventArgs e) => SpeedValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Save the created Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal async void Save_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Data.Name.Length > 0)
            {
                MessagingCenter.Send(this, "Create", _viewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e) =>
            await Navigation.PopModalAsync();
    }
}
