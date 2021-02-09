using System;
using System.ComponentModel;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        /// Constructor that takes and existing data item
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            // DifficultyPicker.SelectedItem = ViewModel.Data.Difficulty.ToMessage();
        }

        void OnSpeedStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValueLabel.Text = String.Format("{0}", e.NewValue);
        }

        void OnDefenseStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValueLabel.Text = String.Format("{0}", e.NewValue);
        }

        void OnAttackStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValueLabel.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Save by calling for Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();
    }
}
