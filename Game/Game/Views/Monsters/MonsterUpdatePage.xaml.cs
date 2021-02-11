using System;
using System.ComponentModel;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Update a Monster
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterUpdatePage : ContentPage
    {
        // The Monster to update
        private readonly GenericViewModel<MonsterModel> ViewModel = new GenericViewModel<MonsterModel>();

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor that takes and existing data item
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = ViewModel = data;

            // DifficultyPicker.SelectedItem = ViewModel.Data.Difficulty.ToMessage();
        }

        private void OnSpeedStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            SpeedValueLabel.Text = string.Format("{0}", e.NewValue);

        private void OnDefenseStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            DefenseValueLabel.Text = string.Format("{0}", e.NewValue);

        private void OnAttackStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            AttackValueLabel.Text = string.Format("{0}", e.NewValue);

        /// <summary>
        ///     Save by calling for Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your monster a name", "OK");
            }
            else
            {
                MessagingCenter.Send(this, "Update", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        ///     Cancel the Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();
    }
}