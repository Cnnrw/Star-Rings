using System;
using System.ComponentModel;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     Create a character
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
        ///     Constructor for Create makes a new model
        /// </summary>
        public CharacterCreatePage()
        {
            InitializeComponent();

            _viewModel.Data = new CharacterModel();

            BindingContext = _viewModel;

            JobPicker.SelectedItem = _viewModel.Data.Job.ToString();

            _viewModel.Title = "Create";
        }

        private void OnJobPickerChanged(object sender, EventArgs e)
        {
            string iconImageURI = CharacterJobEnumExtensions.ToIconImageURI(_viewModel.Data.Job);

            _viewModel.Data.ImageURI = iconImageURI;
            JobImage.Source = iconImageURI;
        }

        /// <summary>
        ///     Changes Level attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLevelStepperChanged(object sender, ValueChangedEventArgs e) =>
            LevelValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Max Health attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMaxHealthStepperChanged(object sender, ValueChangedEventArgs e) =>
            MaxHealthValueLabel.Text = $"{e.NewValue}";

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

        /// <summary>
        ///     Save by calling for Create
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
        ///     Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();
    }
}