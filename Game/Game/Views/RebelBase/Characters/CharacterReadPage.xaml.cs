using System.Linq;

using Game.Enums;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Character Read Page code-behind
    /// </summary>
    public partial class CharacterReadPage : BaseContentPage
    {
        // View Model for Item
        internal readonly GenericViewModel<CharacterModel> _viewModel;

        // Empty Constructor for UTs
        internal CharacterReadPage(bool unitTest) { }

        /// <summary>
        /// Constructor is called with the CharacterModel of the
        /// character to be displayed.
        /// </summary>
        /// <param name="model">The character model object to be displayed.</param>
        public CharacterReadPage(GenericViewModel<CharacterModel> model)
        {
            InitializeComponent();

            _viewModel = model;

            BindingContext = _viewModel;

            AddItemsToDisplay();
        }

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        void AddItemsToDisplay()
        {
            var flexList = ItemBox.Children.ToList();
            foreach (var data in flexList)
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
    }
}
