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
        }

        /// <summary>
        ///     Example of a Button Click (this one is Sync, if calling Async then needs to be Async)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new HomePage());
    }
}