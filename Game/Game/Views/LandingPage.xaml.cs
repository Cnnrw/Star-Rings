using System;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     The first page a users sees
    /// </summary>
    public partial class LandingPage : ContentPage
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public LandingPage()
        {
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_OnPressed(object sender, EventArgs e)
        {
            StartButton.Source = "start_button_pressed.png";
            StartButton.Scale = .95;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_OnReleased(object sender, EventArgs e)
        {
            StartButton.Source = "start_button_normal.png";
            StartButton.Scale = 1;

            await Navigation.PushAsync(new Page());
        }
    }
}
