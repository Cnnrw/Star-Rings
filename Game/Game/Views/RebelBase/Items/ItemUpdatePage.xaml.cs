using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    public partial class ItemUpdatePage : BaseContentPage
    {
        // View Model for Item
        internal readonly GenericViewModel<ItemModel> _viewModel;

        // Empty Constructor for Tests
        internal ItemUpdatePage(bool unitTest){ }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            _viewModel = data;

            UpdatePageBindingContext();
        }

        /// <summary>
        ///     Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        void UpdatePageBindingContext()
        {
            // Temp store off the level
            var data = _viewModel.Data;

            // Clear the Binding and reset it
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = $"Update {data.Name}";

            BindingContext = _viewModel;

            // Update the pickers
            LocationPicker.SelectedItem = data.Location.ToString();
            AttributePicker.SelectedItem = data.Attribute.ToString();
        }


        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(_viewModel.Data.ImageURI))
                _viewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;

            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your item a name", "OK");
                return;
            }

            MessagingCenter.Send(this, "Update", _viewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e) =>
            await Navigation.PopModalAsync();

        /// <summary>
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            RangeValue.Text = $"{e.NewValue}";

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            ValueValue.Text = $"{e.NewValue}";

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            DamageValue.Text = $"{e.NewValue}";
    }
}
