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
    ///     Monster create page
    /// </summary>
    public partial class MonsterCreatePage : BaseContentPage
    {
        /// <summary>
        ///     Internal Monster ViewModel
        /// </summary>
        internal readonly GenericViewModel<MonsterModel> _viewModel;

        /// <summary>
        ///     Empty Constructor for UTs
        /// </summary>
        /// <param name="unitTest"></param>
        internal MonsterCreatePage(bool unitTest) { }

        /// <summary>
        ///     Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage()
        {
            InitializeComponent();

            _viewModel = new GenericViewModel<MonsterModel>
            {
                Data = RandomPlayerHelper.GetRandomMonster(10)
            };

            UpdatePageBindingContext();
        }

        /// <summary>
        ///
        /// </summary>
        internal void UpdatePageBindingContext()
        {
            var data = _viewModel.Data;

            // Clear the Binding and reset
            BindingContext = null;

            _viewModel.Data = data;
            PageTitle = "New Monster";

            BindingContext = _viewModel;

            // Set pickers' initially selected items
            ImagePicker.SelectedItem = MonsterImageEnumExtensions.FromImageURI(data.ImageURI);
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
            var selectedImage = (MonsterImageEnum)picker.SelectedIndex;

            MonsterImage.Source = selectedImage.ToImageURI();
            MonsterIconImage.Source = selectedImage.ToIconImageURI();
        }

        /// <summary>
        ///     Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.Data.Name.Length == 0)
            {
                await DisplayAlert("Hold up!", "Please give your monster a name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(_viewModel.Data.ImageURI))
            {
                var monsterImg = RandomPlayerHelper.GetMonsterImage();
                _viewModel.Data.ImageURI = monsterImg.ToImageURI();
                _viewModel.Data.IconImageURI = monsterImg.ToIconImageURI();
            }
            else
            {
                var monsterImg = ImagePicker.SelectedItem;
                _viewModel.Data.ImageURI = monsterImg.ToImageURI();
                _viewModel.Data.IconImageURI = monsterImg.ToIconImageURI();
            }

            MessagingCenter.Send(this, "Create", _viewModel.Data);
            await App.NavigationService.GoBack();
        }

        /// <summary>
        ///     Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RandomButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.Data = RandomPlayerHelper.GetRandomMonster(20);
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

        #endregion ItemPopup
    }
}
