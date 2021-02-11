using System;
using System.ComponentModel;

using Game.GameRules;
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
        private readonly GenericViewModel<MonsterModel> _viewModel = new GenericViewModel<MonsterModel>();

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
        private async void Save_Clicked(object sender, EventArgs e)
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
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

        /// <summary>
        ///     Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomButton_Clicked(object sender, EventArgs e)
        {
            BindingContext = null;

            _viewModel.Data = RandomPlayerHelper.GetRandomMonster(1);
            _viewModel.Title = _viewModel.Data.Name;

            BindingContext = _viewModel;
        }
    }
}