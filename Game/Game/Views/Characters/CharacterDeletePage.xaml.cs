using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views.Characters
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CharacterDeletePage : ContentPage
    {
        CharacterReadViewModel viewModel;

        public CharacterDeletePage(CharacterReadViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        /* <summary>
         * Cancel the Delete action for this character
         * </summary
         * <param name="sender"></param>
         * <param name="e"></param>
         */
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /* <summary>
        * Delete this character
        * </summary
        * <param name="sender"></param>
        * <param name="e"></param>
        */
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteItem", viewModel.Character);

            await Navigation.PopModalAsync();
        }
    }
}
