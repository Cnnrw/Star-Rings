using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Game.GameRules;
using Game.Models;
using Game.Models.Enums;
using Game.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     Update a character
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        // The Character to update
        private readonly GenericViewModel<CharacterModel> _viewModel;

        // The current Item Location the user is selecting for
        private ItemLocationEnum _selectedItemLocation; 

        // Empty Constructor for UTs
        public CharacterUpdatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = _viewModel = data;

            LoadLevelPickerValues();

            UpdatePageBindingContext();
        }

        /// <summary>
        ///     Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        private void UpdatePageBindingContext()
        {
            // Temp store off the level
            var data = _viewModel.Data;

            // Clear the Binding and reset it
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = data.Name;

            BindingContext = _viewModel;

            // this resets the Picker to the Character's level
            LevelPicker.SelectedIndex = _viewModel.Data.Level - 1;

            JobPicker.SelectedItem = _viewModel.Data.Job.ToString();

            AddItemsToDisplay();
        }

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
                await DisplayAlert("Hold up!", "Please give your character a name", "OK");
            }
            else
            {
                MessagingCenter.Send(this, "Update", _viewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        ///     Cancel the Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Cancel_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

        // /// <summary>
        // ///     Randomizes the Character values and items
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // private void RandomButton_Clicked(object sender, EventArgs e)
        // {
        //     _viewModel.Data.Update(RandomPlayerHelper.GetRandomCharacter(20));
        //     UpdatePageBindingContext();
        // }

        #endregion

        #region Pickers

        /// <summary>
        /// Updates the Character's ImageURI and the page's image source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnJobPickerChanged(object sender, EventArgs e)
        {
            var iconImageURI = _viewModel.Data.Job.ToIconImageURI();

            _viewModel.Data.ImageURI = iconImageURI;
            JobImage.Source = iconImageURI;
        }

        /// <summary>
        ///     Propagates the values for the character level picker
        /// </summary>
        private void LoadLevelPickerValues()
        {
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                LevelPicker.Items.Add(i.ToString());
            }

            LevelPicker.SelectedIndex = -1;
        }

        /// <summary>
        ///     Updates character level based on value selected from picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LevelPicker_Changed(object sender, EventArgs args)
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
            {
                return;
            }

            // Change character level
            _viewModel.Data.Level = result;
        }

        #endregion

        #region Steppers

        // /// <summary>
        // ///     Changes Level attribute of a Character
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // private void OnLevelStepperChanged(object sender, ValueChangedEventArgs e) =>
        //     LevelValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Max Health attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMaxHealthStepperChanged(object sender, ValueChangedEventArgs e) =>
            MaxHealthValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Attack attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAttackStepperChanged(object sender, ValueChangedEventArgs e) =>
            AttackValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Defense attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDefenseStepperChanged(object sender, ValueChangedEventArgs e) =>
            DefenseValueLabel.Text = $"{e.NewValue}";

        /// <summary>
        ///     Changes Speed attribute of a Character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSpeedStepperChanged(object sender, ValueChangedEventArgs e) =>
            SpeedValueLabel.Text = $"{e.NewValue}";

        #endregion

        #region EquippedItems

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
        private StackLayout GetItemToDisplay(ItemLocationEnum itemLocation)
        {
            ItemModel data = null;

            // Get the current Item in this ItemLocation
            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    data = _viewModel.Data.GetItem(_viewModel.Data.Head);
                    break;
                case ItemLocationEnum.Necklace:
                    data = _viewModel.Data.GetItem(_viewModel.Data.Necklace);
                    break;
                case ItemLocationEnum.PrimaryHand:
                    data = _viewModel.Data.GetItem(_viewModel.Data.PrimaryHand);
                    break;
                case ItemLocationEnum.OffHand:
                    data = _viewModel.Data.GetItem(_viewModel.Data.OffHand);
                    break;
                case ItemLocationEnum.LeftFinger:
                    data = _viewModel.Data.GetItem(_viewModel.Data.LeftFinger);
                    break;
                case ItemLocationEnum.RightFinger:
                    data = _viewModel.Data.GetItem(_viewModel.Data.RightFinger);
                    break;
                case ItemLocationEnum.Feet:
                    data = _viewModel.Data.GetItem(_viewModel.Data.Feet);
                    break;
                default:
                    break;
            }

            // If there's no Item currently in the slot, show a blank Item
            data = data ??
                new ItemModel
                {
                    Location = ItemLocationEnum.Unknown,
                    ImageURI = "icon_cancel.png",
                    Name = "No item"
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
            ItemButton.Clicked += (sender, args) =>
            {
                _selectedItemLocation = itemLocation;
                ShowPopup(data);
            };

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
                Children = { ItemButton, ItemLabel }
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
                default:
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

            List<ItemModel> itemList = new List<ItemModel> { NoneItem, data };

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

        #endregion EquippedItems
    }
}