using System;
using System.ComponentModel;
using System.Linq;

using Game.Controls;
using Game.Models;
using Game.Models.Enums;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Monster read page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterReadPage : ContentPage
    {
        private readonly GenericViewModel<MonsterModel> _viewModel;

        #region Constructors

        // UnitTest Constructor
        // public MonsterReadPage(bool unitTest) { }

        /// <summary>
        ///     Constructor called with a view model
        ///     This is the primary method for opening the page
        ///     The _viewModel is the data that should be displayed
        /// </summary>
        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = _viewModel = data;
            _viewModel.Title = data.Data.Name;

            // Show the Monsters Items
            AddItemsToDisplay();
        }

        #endregion
        #region Event Handlers

        /// <summary>
        ///     Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(_viewModel)));
            await Navigation.PopAsync();
        }

        /// <summary>
        ///     Calls MonsterDeletePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterDeletePage(_viewModel)));
            await Navigation.PopAsync();
        }

        // /// <summary>
        // ///     Show the Popup for the Item
        // /// </summary>
        // /// <param name="data"></param>
        // /// <returns></returns>
        // private void ShowPopup(ItemModel data)
        // {
        //     PopupLoadingView.IsVisible = true;
        //     PopupItemImage.Source = data.ImageURI;
        //
        //     PopupItemName.Text = data.Name;
        //     PopupItemDescription.Text = data.Description;
        //     PopupItemLocation.Text = data.Location.ToMessage();
        //     PopupItemAttribute.Text = data.Attribute.ToMessage();
        //     PopupItemValue.Text = " + " + data.Value;
        // }

        /// <summary>
        ///     When the user clicks the close in the Popup
        ///     hide the view
        ///     show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePopup_Clicked(object sender, EventArgs e) => PopupLoadingView.IsVisible = false;

        #endregion
        #region Item Popup

        /// <summary>
        ///     Show the Items the Monster has
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
        ///     Look up the Item to Display
        /// </summary>
        /// <returns></returns>
        private StackLayout GetItemToDisplay()
        {
            // Default Image is for the Plus
            // const string ImageSource = "icon_cancel.png";
            // var ClickableButton = true;

            var data = _viewModel.Data.GetItem(_viewModel.Data.UniqueItem);
            if (data == null)
            {
                // show the default icon for the location
                data = new ItemModel {Location = ItemLocationEnum.Unknown, ImageURI = "icon_cancel.png"};

                // Turn off click action
                // ClickableButton = false;
            }

            var ItemView = new BasicItemView
            {
                ItemName = data.Name,
                ItemDescription = "Hello",
                IconImageSource = ImageSource.FromFile(data.ImageURI)
            };

            // Hookup the Image Button to show the Item picture

            // var ItemButton = new ImageButton
            // {
            //     Source = data.ImageURI,
            //     Style = Application.Current.Resources.TryGetValue("ImageMediumStyle", out var iMS) ?
            //                 (Style)iMS :
            //                 null
            // };

            // if (ClickableButton)
            // {
            //     // Add an event so the user can click the item and see more
            //     ItemButton.Clicked += (sender, args) => ShowPopup(data);
            // }
            //
            // Add the Display text for the item
            // var ItemLabel = new Label
            // {
            //     Text = "Unique Drop",
            //     Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out var vSM) ?
            //                 (Style)vSM :
            //                 null,
            //     HorizontalOptions = LayoutOptions.Center,
            //     HorizontalTextAlignment = TextAlignment.Center
            // };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = Application.Current.Resources.TryGetValue("ItemImageBox", out var iIB) ? (Style)iIB : null,
                HorizontalOptions = LayoutOptions.Center,
                // Children = {ItemButton, ItemLabel}
                Children = {ItemView}
            };

            return ItemStack;
        }

        #endregion
    }
}