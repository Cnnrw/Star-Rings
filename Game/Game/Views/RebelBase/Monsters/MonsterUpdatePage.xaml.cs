using System;
using System.Collections.Generic;
using System.Linq;

using Game.Enums;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Update a Monster
    /// </summary>
    public partial class MonsterUpdatePage : BaseContentPage
    {
        // The Monster to update
        internal readonly GenericViewModel<MonsterModel> _viewModel;

        // Empty Constructor for UTs
        internal MonsterUpdatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor that takes an existing monster
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> monster)
        {
            InitializeComponent();

            _viewModel = monster;

            UpdatePageBindingContext();
        }

        /// <summary>
        ///     Updates the BindingContext to trigger a refresh
        /// </summary>
        private void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = $"Update {data.Name}";

            BindingContext = _viewModel;

            BattleLocationPicker.SelectedItem = _viewModel.Data.BattleLocation.ToString();

            AddItemsToDisplay();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSpeedStepperValueChanged(object sender, ValueChangedEventArgs e) => SpeedValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDefenseStepperValueChanged(object sender, ValueChangedEventArgs e) => DefenseValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAttackStepperValueChanged(object sender, ValueChangedEventArgs e) => AttackValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Save by calling for Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal async void Save_Clicked(object sender, EventArgs e)
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
        internal async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

        /// <summary>
        ///     Randomize Monster Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void RandomButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.Data.Update(RandomPlayerHelper.GetRandomMonster(1));
            UpdatePageBindingContext();
        }

        #region Items Popup

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        private void AddItemsToDisplay()
        {
            var flexList = ItemBox.Children.ToList();
            foreach (var data in flexList)
                ItemBox.Children.Remove(data);

            ItemBox.Children.Add(GetItemToDisplay());
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <returns></returns>
        public StackLayout GetItemToDisplay()
        {
            var data = _viewModel.Data.GetItem(_viewModel.Data.UniqueItem) ??
                       new ItemModel
                       {
                           Location = ItemLocationEnum.Unknown,
                           ImageURI = "icon_cancel.png",
                           Name = "No item"
                       };

            // Hookup the Image Button to show the Item picture
            var itemButton = new ImageButton
            {
                Source = data.ImageURI,
                Style = Application.Current.Resources.TryGetValue("ImageMediumStyle", out var buttonStyle)
                            ? (Style)buttonStyle
                            : null
            };

            // Add a event so the user can click the item and see more
            itemButton.Clicked += (sender, args) => ShowPopup(data);

            // Add the Display Text for the item
            var itemLabel = new Label
            {
                Text = data.Name,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out var labelStyle)
                            ? (Style)labelStyle
                            : null
            };

            // Put the Image Button and Text inside a layout
            var itemStack = new StackLayout
            {
                Padding = 3,
                Style = Application.Current.Resources.TryGetValue("ItemImageBox", out var stackStyle)
                            ? (Style)stackStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    itemButton,
                    itemLabel
                }
            };

            return itemStack;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is ItemModel data))
                return;

            _viewModel.Data.UniqueItem = data.Id;

            AddItemsToDisplay();

            ClosePopup();
        }


        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemModel data)
        {
            PopupItemSelector.IsVisible = true;

            // Make a fake item for None
            var noneItem = new ItemModel
            {
                Id = null,     // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            var itemList = new List<ItemModel> {noneItem};

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
        public void ClosePopup_Clicked(object sender, EventArgs e) => ClosePopup();

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup() => PopupItemSelector.IsVisible = false;

        #endregion UniqueItems
    }
}
