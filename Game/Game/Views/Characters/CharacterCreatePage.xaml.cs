using System;
using System.ComponentModel;

using Game.Models;
using Game.Models.Enums;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// Character creation page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterCreatePage : ContentPage
    {
        // The Character to create
        private readonly GenericViewModel<CharacterModel> _viewModel = new GenericViewModel<CharacterModel>();

        // Empty Constructor for UTs
        public CharacterCreatePage(bool unitTest) { }

        /// <summary>
        /// Constructor makes a new model
        /// </summary>
        public CharacterCreatePage()
        {
            InitializeComponent();

            _viewModel.Data = new CharacterModel();

            BindingContext = _viewModel;

            JobPicker.SelectedItem = _viewModel.Data.Job.ToString();

            _viewModel.Title = "Create";
        }

        /// <summary>
        /// Update the Character's ImageURI and the page's image source according to the Character's Job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnJobPickerChanged(object sender, EventArgs e)
        {
            var iconImageURI = _viewModel.Data.Job.ToIconImageURI();

            _viewModel.Data.ImageURI = iconImageURI;
            JobImage.Source = iconImageURI;
        }

        /// <summary>
        /// Update the Level value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLevelStepperChanged(object sender, ValueChangedEventArgs e) =>
            LevelValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the MaxHealth value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMaxHealthStepperChanged(object sender, ValueChangedEventArgs e) =>
            MaxHealthValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the Attack value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAttackStepperChanged(object sender, ValueChangedEventArgs e) =>
            AttackValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the Defense value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDefenseStepperChanged(object sender, ValueChangedEventArgs e) =>
            DefenseValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Update the Speed value Label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSpeedStepperChanged(object sender, ValueChangedEventArgs e) =>
            SpeedValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        /// Save the created Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            //TODO: Create entry validator to attach to xaml control
            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your character a name", "OK");
            }
            else
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
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();
    }
}