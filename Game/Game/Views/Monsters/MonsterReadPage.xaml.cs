using System;
using System.ComponentModel;
using System.Linq;

using Game.Models;
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

        // Monster ViewModel
        private readonly GenericViewModel<MonsterModel> _viewModel;

        // UnitTest Constructor
        public MonsterReadPage(bool unitTest) { }

        /// <summary>
        ///     Constructor called with a view model
        ///     This is the primary method for opening the page
        ///     The _viewModel is the data that should be displayed
        /// </summary>
        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            _viewModel = data;
            _viewModel.Title = data.Data.Name;

            BindingContext = _viewModel;

            // Show the Monsters Items
            AddItemsToDisplay();
        }

        // /// <summary>
        // ///     Save calls to Update
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // private async void Update_Clicked(object sender, EventArgs e)
        // {
        //     await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(_viewModel)));
        //     await Navigation.PopAsync();
        // }

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

            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));
        }

        /// <summary>
        ///     Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Default Image is for the Plus
            var ImageSource = "icon_cancel.png";
            var ClickableButton = true;

            var data = _viewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                // show the default icon for the location
                data = new ItemModel {Location = location, ImageURI = ImageSource};

                // Turn off click action
                ClickableButton = false;
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"], Source = data.ImageURI
            };

            if (ClickableButton)
            {
                // Add an event so the user can click the item and see more
                ItemButton.Clicked += (sender, args) => ShowPopup(data);
            }

            // Add the Display text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {ItemButton, ItemLabel}
            };

            return ItemStack;
        }

        /// <summary>
        ///     Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void ShowPopup(ItemModel data)
        {
            PopupLoadingView.IsVisible = true;
            PopupItemImage.Source = data.ImageURI;

            PopupItemName.Text = data.Name;
            PopupItemDescription.Text = data.Description;
            PopupItemLocation.Text = data.Location.ToMessage();
            PopupItemAttribute.Text = data.Attribute.ToMessage();
            PopupItemValue.Text = " + " + data.Value;
        }


        /// <summary>
        ///     When the user clicks the close in the Popup
        ///     hide the view
        ///     show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePopup_Clicked(object sender, EventArgs e) => PopupLoadingView.IsVisible = false;
    }
}
