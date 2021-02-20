using System;
using System.ComponentModel;
using System.Linq;

using Game.Enums;
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
            ItemBox.Children.Add(GetItemToDisplay());
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <returns></returns>
        private StackLayout GetItemToDisplay()
        {
            ItemModel data = null;

            // If there's no Item currently in the slot, show a blank Item
            data = _viewModel.Data.GetItem(_viewModel.Data.UniqueItem) ??
                   new ItemModel
                   {
                       Location = ItemLocationEnum.Unknown,
                       ImageURI = "icon_cancel.png",
                       Name = "No item"
                   };

            // Hookup the Image Button to show the Item picture
            var ItemButton = new Image
            {
                Source = data.ImageURI,
                Style = Application.Current.Resources.TryGetValue("ImageMediumStyle", out object buttonStyle)
                            ? (Style)buttonStyle
                            : null
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
                Children = {ItemButton, ItemLabel}
            };

            return ItemStack;
        }

        #endregion EquippedItems
    }
}