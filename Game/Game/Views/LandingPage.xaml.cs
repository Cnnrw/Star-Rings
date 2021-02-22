using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     The first page a users sees
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public LandingPage()
        {
            InitializeComponent();

            // Remove the nav bar on the home page
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        ///     Example of a Button Click (this one is Sync, if calling Async then needs to be Async)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new HomePage());

        private void StartButton_OnPressed(object sender, EventArgs e)
        {
            StartButton.Source = "start_button_pressed.png";
            StartButton.Scale = .95;
        }

        public async void StartButton_OnReleased(object sender, EventArgs e)
        {
            StartButton.Source = "start_button_normal.png";
            StartButton.Scale = 1;

            await Navigation.PushAsync(new HomePage());
        }
    }
}