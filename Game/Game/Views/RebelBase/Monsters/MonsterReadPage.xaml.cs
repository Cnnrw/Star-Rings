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
    public partial class MonsterReadPage : BaseContentPage
    {
        internal readonly GenericViewModel<MonsterModel> _viewModel;

        // UnitTest Constructor
        internal MonsterReadPage(bool unitTest) { }

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

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var flexList = ItemBox.Children.ToList();
            foreach (var data in flexList)
                ItemBox.Children.Remove(data);

            // Add an item display box for each Item Location
            ItemBox.Children.Add(GetItemToDisplay());
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <returns></returns>
        public StackLayout GetItemToDisplay()
        {
            // If there's no Item currently in the slot, show a blank Item
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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
