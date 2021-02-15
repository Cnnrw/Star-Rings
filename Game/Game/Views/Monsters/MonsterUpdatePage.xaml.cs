using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Game.GameRules;
using Game.Models;
using Game.Models.Enums;
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
        private readonly GenericViewModel<MonsterModel> _viewModel;

        private void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = "Update " + data.Name;

            BindingContext = _viewModel;

            BattleLocationPicker.SelectedItem = _viewModel.Data.BattleLocation.ToString();

            AddItemsToDisplay();
        }

        #region Ctors

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor that takes and existing data item
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            _viewModel = data;

            UpdatePageBindingContext();
        }

        #endregion

        #region Steppers

        private void OnSpeedStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            SpeedValueLabel.Text = $"{e.NewValue}";

        private void OnDefenseStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            DefenseValueLabel.Text = $"{e.NewValue}";

        private void OnAttackStepperValueChanged(object sender, ValueChangedEventArgs e) =>
            AttackValueLabel.Text = $"{e.NewValue}";

        #endregion

        #region DataCRUDi

        /// <summary>
        ///     Save by calling for Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your monster a name", "OK");
                return;
            }

            MessagingCenter.Send(this, "Update", _viewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        ///     Cancel the Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

        /// <summary>
        ///     Randomize Monster Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.Data.Update(RandomPlayerHelper.GetRandomMonster(1));
            UpdatePageBindingContext();
        }

        #endregion

        #region Items Popup

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        private void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemBox.Children.Remove(data);
            }

            ItemBox.Children.Add(GetItemToDisplay());
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <returns></returns>
        private StackLayout GetItemToDisplay()
        {
            var data = _viewModel.Data.GetItem(_viewModel.Data.UniqueItem) ??
                       new ItemModel
                       {
                           Location = ItemLocationEnum.Unknown,
                           ImageURI = "icon_cancel.png",
                           Name = "Click to Add"
                       };

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Source = data.ImageURI,
                Style = Application.Current.Resources.TryGetValue("ImageMediumStyle", out object buttonStyle)
                            ? (Style)buttonStyle
                            : null
            };

            // Add a event so the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(data);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = data.Name,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out object labelStyle)
                            ? (Style)labelStyle
                            : null
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = Application.Current.Resources.TryGetValue("ItemImageBox", out object stackStyle)
                            ? (Style)stackStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Children = {ItemButton, ItemLabel}
            };

            return ItemStack;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is ItemModel data))
            {
                return;
            }

            _viewModel.Data.UniqueItem = data.Id;

            AddItemsToDisplay();

            ClosePopup();
        }


        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool ShowPopup(ItemModel data)
        {
            PopupItemSelector.IsVisible = true;

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null,     // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel> {NoneItem, data};

            // Add the rest of the items to the list
            itemList.AddRange(ItemIndexViewModel.Instance.Dataset);

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;
            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePopup_Clicked(object sender, EventArgs e) => ClosePopup();

        /// <summary>
        /// Close the popup
        /// </summary>
        private void ClosePopup() => PopupItemSelector.IsVisible = false;

        #endregion UniqueItems
    }
}