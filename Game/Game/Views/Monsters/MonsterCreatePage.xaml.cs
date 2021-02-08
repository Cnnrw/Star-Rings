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
    public partial class MonsterCreatePage : ContentPage
    {
        // The Character to create
        private readonly GenericViewModel<MonsterModel> ViewModel = new GenericViewModel<MonsterModel>();

        // Empty Constructor for UTs
        public MonsterCreatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage()
        {
            InitializeComponent();

            ViewModel.Data = new MonsterModel();

            BindingContext = ViewModel;

            ViewModel.Title = "Create";
        }

        void OnAttackStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValueLabel.Text = String.Format("{0}", e.NewValue);
        }

        void OnDefenseStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValueLabel.Text = String.Format("{0}", e.NewValue);
        }

        void OnSpeedStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValueLabel.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        ///     Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        ///     Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();
    }
}