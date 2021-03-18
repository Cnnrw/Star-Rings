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
    ///     Update a character
    /// </summary>
    public partial class CharacterUpdatePage : BaseContentPage
    {
        // The Character to update
        internal readonly GenericViewModel<CharacterModel> _viewModel;

        // The current Item Location the user is selecting for
        ItemLocationEnum _selectedItemLocation;

        // Empty Constructor for UTs
        internal CharacterUpdatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            _viewModel = data;

            LoadLevelPickerValues();

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
            PageTitle = $"Update {data.Name}";

            BindingContext = _viewModel;

            // this resets the Picker to the Character's level
            LevelPicker.SelectedIndex = _viewModel.Data.Level - 1;

            AddItemsToDisplay();
        }

        /// <summary>
        ///     Save by calling for Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal async void Save_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your character a name", "OK");
                return;
            }

            MessagingCenter.Send(this, "Update", _viewModel.Data);
            await App.NavigationService.GoBack();
        }

        #region Pickers

        /// <summary>
        ///     Propagates the values for the character level picker
        /// </summary>
        void LoadLevelPickerValues()
        {
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
                LevelPicker.Items.Add(i.ToString());

            LevelPicker.SelectedIndex = -1;
        }

        /// <summary>
        ///     Updates character level based on value selected from picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void LevelPicker_Changed(object sender, EventArgs args)
        {
            // If the Picker is not set, then set it
            if (LevelPicker.SelectedIndex == -1)
            {
                LevelPicker.SelectedIndex = _viewModel.Data.Level - 1;
                return;
            }

            var result = LevelPicker.SelectedIndex + 1;

            // If the level hasn't changed, don't roll for health
            if (result == _viewModel.Data.Level)
                return;

            // Change character level
            _viewModel.Data.Level = result;
        }

        #endregion
        #region Steppers

        /// <summary>
        ///     Changes Max Health attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMaxHealthStepperChanged(object sender, ValueChangedEventArgs e) => MaxHealthValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Attack attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAttackStepperChanged(object sender, ValueChangedEventArgs e) => AttackValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Defense attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDefenseStepperChanged(object sender, ValueChangedEventArgs e) => DefenseValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Speed attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSpeedStepperChanged(object sender, ValueChangedEventArgs e) => SpeedValueLabel.Text = $"{e.NewValue}";

        #endregion
        #region EquippedItems

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        void AddItemsToDisplay()
        {
            var flexList = ItemBox.Children.ToList();
            foreach (var data in flexList)
                ItemBox.Children.Remove(data);

            // Add an item display box for each Item Location
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklace));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <returns></returns>
        StackLayout GetItemToDisplay(ItemLocationEnum itemLocation)
        {
            // Get the current Item in this ItemLocation
            var data = itemLocation switch
                       {
                           ItemLocationEnum.Head        => _viewModel.Data.GetItem(_viewModel.Data.Head),
                           ItemLocationEnum.Necklace    => _viewModel.Data.GetItem(_viewModel.Data.Necklace),
                           ItemLocationEnum.PrimaryHand => _viewModel.Data.GetItem(_viewModel.Data.PrimaryHand),
                           ItemLocationEnum.OffHand     => _viewModel.Data.GetItem(_viewModel.Data.OffHand),
                           ItemLocationEnum.LeftFinger  => _viewModel.Data.GetItem(_viewModel.Data.LeftFinger),
                           ItemLocationEnum.RightFinger => _viewModel.Data.GetItem(_viewModel.Data.RightFinger),
                           ItemLocationEnum.Feet        => _viewModel.Data.GetItem(_viewModel.Data.Feet),
                           _                            => null
                       };

            // If there's no Item currently in the slot, show a blank Item
            data ??= new ItemModel
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
            itemButton.Clicked += (sender, args) =>
            {
                _selectedItemLocation = itemLocation;
                ShowPopup(data);
            };

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
                Children = {itemButton, itemLabel}
            };

            return itemStack;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is ItemModel data))
                return;

            // Equip the Item into the selected Item Location
            switch (_selectedItemLocation)
            {
                case ItemLocationEnum.Head:
                    _viewModel.Data.Head = data.Id;
                    break;
                case ItemLocationEnum.Necklace:
                    _viewModel.Data.Necklace = data.Id;
                    break;
                case ItemLocationEnum.PrimaryHand:
                    _viewModel.Data.PrimaryHand = data.Id;
                    break;
                case ItemLocationEnum.OffHand:
                    _viewModel.Data.OffHand = data.Id;
                    break;
                case ItemLocationEnum.LeftFinger:
                    _viewModel.Data.LeftFinger = data.Id;
                    break;
                case ItemLocationEnum.RightFinger:
                    _viewModel.Data.RightFinger = data.Id;
                    break;
                case ItemLocationEnum.Feet:
                    _viewModel.Data.Feet = data.Id;
                    break;
            }

            AddItemsToDisplay();

            ClosePopup();
        }


        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool ShowPopup(ItemModel data)
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
        void ClosePopup_Clicked(object sender, EventArgs e) => ClosePopup();

        /// <summary>
        /// Close the popup
        /// </summary>
        void ClosePopup() => PopupItemSelector.IsVisible = false;

        #endregion EquippedItems
    }
}
