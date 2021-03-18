using System;
using System.Linq;

using Game.Enums;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    public partial class RoundOverPage : BaseContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoundOverPage()
        {
            InitializeComponent();

            // Update the Round Count
            TotalRound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString();

            // Update the Found Number
            TotalFound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count()
                                                   .ToString();

            DrawCharacterList();

            DrawItemLists();
        }

        /// <summary>
        /// Clear and Add the Characters that survived
        /// </summary>
        public void DrawCharacterList()
        {
            // Clear and Populate the Characters Remaining
            var flexList = CharacterListFrame.Children.ToList();
            foreach (var data in flexList)
                CharacterListFrame.Children.Remove(data);

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
                CharacterListFrame.Children.Add(CreatePlayerDisplayBox(data));
        }

        /// <summary>
        /// Draw the List of Items
        ///
        /// The Ones Dropped
        ///
        /// The Ones Selected
        ///
        /// </summary>
        public void DrawItemLists()
        {
            DrawDroppedItems();
            DrawSelectedItems();

            // Only need to update the selected, the Dropped is set in the constructor
            TotalSelected.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList
                                                      .Count()
                                                      .ToString();
        }

        /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
        public void DrawDroppedItems()
        {
            // Clear and Populate the Dropped Items
            var flexList = ItemListFoundFrame.Children.ToList();
            foreach (var data in flexList)
                ItemListFoundFrame.Children.Remove(data);

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList
                                                      .Distinct())
                ItemListFoundFrame.Children.Add(GetItemToDisplay(data));
        }

        /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
        public void DrawSelectedItems()
        {
            // Clear and Populate the Dropped Items
            var flexList = ItemListSelectedFrame.Children.ToList();
            foreach (var data in flexList)
                ItemListSelectedFrame.Children.Remove(data);

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList)
                ItemListSelectedFrame.Children.Add(GetItemToDisplay(data));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemModel item)
        {
            if (item == null)
                return new StackLayout();

            if (string.IsNullOrEmpty(item.Id))
                return new StackLayout();

            // Default Image is the Plus

            var data = ItemIndexViewModel.Instance.GetItem(item.Id)
                       ?? new ItemModel {Name = "Unknown", ImageURI = "icon_cancel.png"};

            // Hookup the Image Button to show the Item picture
            var itemImageButton = new ImageButton
            {
                Style = Application.Current.Resources.TryGetValue("ImageMediumStyle", out var imageStyle)
                            ? (Style)imageStyle
                            : null,
                Source = data.ImageURI
            };

            itemImageButton.Clicked += (sender, args) => ShowItemDetailsPopup(data);

            // Put the Image Button and Text inside a layout
            var itemStack = new StackLayout
            {
                Padding = 3,
                Style = Application.Current.Resources.TryGetValue("ItemImageBox", out var boxStyle)
                            ? (Style)boxStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Children = {itemImageButton}
            };

            return itemStack;
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            data ??= new PlayerInfoModel();

            // Hookup the image
            var playerImage = new Image
            {
                Style = Application.Current.Resources.TryGetValue("ImageBattleLargeStyle", out var imageStyle)
                            ? (Style)imageStyle
                            : null,
                Source = data.ImageURI
            };

            var valueStyleMicro = Application.Current.Resources.TryGetValue("ValueStyleMicro", out var valueStyle)
                                      ? (Style)valueStyle
                                      : null;

            // Add the Level
            var playerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = valueStyleMicro,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Add the HP
            var playerHpLabel = new Label
            {
                Text = "HP : " + data.GetCurrentHealthTotal,
                Style = valueStyleMicro,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            var playerNameLabel = new Label()
            {
                Text = data.Name,
                Style = valueStyleMicro,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var playerStack = new StackLayout
            {
                Style = Application.Current.Resources.TryGetValue("PlayerInfoBox", out var boxStyle)
                            ? (Style)boxStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    playerImage,
                    playerNameLabel,
                    playerLevelLabel,
                    playerHpLabel,
                },
            };

            return playerStack;
        }

        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowItemDetailsPopup(ItemModel data)
        {
            PopupLoadingView.IsVisible = true;
            PopupItemImage.Source = data.ImageURI;

            PopupItemName.Text = data.Name;
            PopupItemDescription.Text = data.Description;
            PopupItemLocation.Text = data.Location.ToMessage();
            PopupItemAttribute.Text = data.Attribute.ToMessage();
            PopupItemValue.Text = " + " + data.Value;
            return true;
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e) => PopupLoadingView.IsVisible = false;

        /// <summary>
        /// Closes the Round Over Popup
        ///
        /// Launches the Next Round Popup
        ///
        /// Resets the Game Round
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await App.NavigationService.GoBack();
        }

        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AutoAssignButton_Clicked(object sender, EventArgs e)
        {
            // Distribute the Items
            BattleEngineViewModel.Instance.Engine.Round.PickupItemsForAllCharacters();

            // Show what was picked up
            DrawItemLists();
        }
    }
}
