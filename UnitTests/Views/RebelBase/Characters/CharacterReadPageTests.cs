using System.Linq;
using System.Threading.Tasks;

using Game;
using Game.Enums;
using Game.Models;
using Game.ViewModels;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class CharacterReadPageTests : CharacterReadPage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CharacterReadPage(new GenericViewModel<CharacterModel>(new CharacterModel()));
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App               app;
        private CharacterReadPage page;

        public CharacterReadPageTests() : base(true) { }

        [Test]
        public void CharacterReadPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterReadPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterReadPage_GetItemToDisplay_Valid_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemToDisplay(ItemLocationEnum.Feet);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterReadPage_GetItemToDisplay_Finger_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemToDisplay(ItemLocationEnum.Finger);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        // TODO: Comment this out
        // [Test]
        // public void CharacterReadPage_ShowPopup_Valid_Should_Pass()
        // {
        //     // Arrange
        //
        //     // Act
        //     page.ShowPopup(new ItemModel());
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // TODO: Comment this out
        // [Test]
        // public void CharacterReadPage_ClosePopup_Clicked_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     // Act
        //     page.ClosePopup_Clicked(null, null);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }

        // [Test]
        // public void CharacterReadPage_AddItemsToDisplay_With_Data_Should_Remove_And_Pass()
        // {
        //     // Arrange
        //
        //     // Put some data into the box so it can be removed
        //     var itemBox = (FlexLayout)page.Content.FindByName("ItemBox");
        //
        //     itemBox.Children.Add(new Label());
        //     itemBox.Children.Add(new Label());
        //
        //     // Act
        //     page.AddItemsToDisplay();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.AreEqual(7, itemBox.Children.Count()); // Got to here, so it happened...
        // }

        [Test]
        public async Task CharacterReadPage_GetItemToDisplay_With_Item_Should_Pass()
        {
            // Arrange
            ItemIndexViewModel.Instance.Dataset.Clear();
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel {Location = ItemLocationEnum.PrimaryHand});

            var character = new CharacterModel();
            character.Head = ItemIndexViewModel.Instance.GetLocationItems(ItemLocationEnum.PrimaryHand).First().Id;
            page._viewModel.Data = character;

            // Act
            var result = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

            // Reset
            ItemIndexViewModel.Instance.Dataset.Clear();
            await ItemIndexViewModel.Instance.LoadDefaultDataAsync();

            // Assert
            Assert.AreEqual(2, result.Children.Count()); // Got to here, so it happened...
        }

        [Test]
        public async Task CharacterReadPage_GetItemToDisplay_With_NoItem_Should_Pass()
        {
            // Arrange
            ItemIndexViewModel.Instance.Dataset.Clear();
            var item = new ItemModel {Location = ItemLocationEnum.PrimaryHand};
            await ItemIndexViewModel.Instance.CreateAsync(item);

            // Act
            var result = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);

            // Reset
            ItemIndexViewModel.Instance.Dataset.Clear();
            await ItemIndexViewModel.Instance.LoadDefaultDataAsync();

            // Assert
            Assert.AreEqual(2, result.Children.Count()); // Got to here, so it happened...
        }

        [Test]
        public void CharacterReadPage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            page._viewModel.Data.PrimaryHand = item.Id;
            var StackItem = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
