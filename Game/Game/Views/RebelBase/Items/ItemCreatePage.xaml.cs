using System;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Create Item
    /// </summary>
    public partial class ItemCreatePage : BaseContentPage
    {
        // The item to create
        public readonly GenericViewModel<ItemModel> _viewModel;

        // Empty Constructor for UTs
        internal ItemCreatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor for ItemCreatePage
        ///     Lets a user create a new Item.
        /// </summary>
        public ItemCreatePage()
        {
            InitializeComponent();

            _viewModel = new GenericViewModel<ItemModel>
            {
                Title = "Create",
                Data = new ItemModel()
            };

            BindingContext = _viewModel;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = _viewModel.Data.Location.ToString();
            AttributePicker.SelectedItem = _viewModel.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(_viewModel.Data.ImageURI))
                _viewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;

            if (_viewModel.Data.Name.Length == 0)
                await DisplayAlert("Hold up!", "Please give your item a name", "OK");
            else
            {
                MessagingCenter.Send(this, "Create", _viewModel.Data);
                await App.NavigationService.GoBack();
            }
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e) => await App.NavigationService.GoBack();

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
