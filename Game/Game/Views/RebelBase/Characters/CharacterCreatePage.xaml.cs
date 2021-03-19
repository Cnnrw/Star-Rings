using System;
using System.Collections.Generic;
using System.Linq;

using Game.Enums;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Character creation page
    /// </summary>
    public partial class CharacterCreatePage : BaseContentPage
    {
        // The Character to create
        internal readonly GenericViewModel<CharacterModel> _viewModel;

        // The current Item Location the user is selecting for
        ItemLocationEnum _selectedItemLocation;

        // Empty Constructor for UTs
        internal CharacterCreatePage(bool unitTest) { }

        /// <summary>
        /// Constructor makes a new model
        /// </summary>
        public CharacterCreatePage()
        {
            InitializeComponent();

            _viewModel = new GenericViewModel<CharacterModel>
            {
                Data = new CharacterModel()
            };

            UpdatePageBindingContext();
        }

        /// <summary>
        ///
        /// </summary>
        void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;
            _viewModel.Data = data;
            _viewModel.Title = "Create a Character";

            BindingContext = _viewModel;
            ImagePicker.SelectedItem = CharacterImageEnumExtensions.FromImageURI(data.ImageURI);

            AddItemsToDisplay();
        }

        /// <summary>
        /// Updates the monster image based on the selected value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnImagePickerChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedImage = (CharacterImageEnum)picker.SelectedIndex;

            CharacterImage.Source = selectedImage.ToImageURI();
            CharacterIconImage.Source = selectedImage.ToIconImageURI();
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

            var img = ImagePicker.SelectedItem;
            _viewModel.Data.ImageURI = img.ToImageURI();
            _viewModel.Data.IconImageURI = img.ToIconImageURI();

            MessagingCenter.Send(this, "Create", _viewModel.Data);
            await App.NavigationService.GoBack();
        }

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
        internal StackLayout GetItemToDisplay(ItemLocationEnum itemLocation)
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
        internal void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
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

            PopupItemSelector.IsVisible = false;
        }


        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool ShowPopup(ItemModel data)
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
        internal void ClosePopup_Clicked(object sender, EventArgs e)
        {
            PopupItemSelector.IsVisible = false;
        }

        #endregion EquippedItems
    }
}
