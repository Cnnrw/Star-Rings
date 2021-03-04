using Game;
using Game.Models;
using Game.ViewModels;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ItemIndexPageTests : ItemIndexPage
    {

        [SetUp]
        public void Setup()
        {
            // Initialize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemIndexPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App           app;
        private ItemIndexPage page;

        public ItemIndexPageTests() : base(true) { }

        [Test]
        public void ItemIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemIndexPage_AddItem_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.AddItem_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemIndexPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        // TODO: commenting out until I find a way to mock SelectionChangedEventArgs
        // [Test]
        // public void ItemIndexPage_OnItemSelected_Clicked_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     var selectedItem = new ItemModel();
        //
        //     var selectedItemChangedEventArgs = new SelectedItemChangedEventArgs(selectedItem, 0);
        //
        //     // Act
        //     page.OnItemSelected(null, selectedItemChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void ItemIndexPage_OnItemSelected_Clicked_Invalid_Null_Should_Fail()
        // {
        //     // Arrange
        //
        //     var selectedItemChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);
        //
        //     // Act
        //     page.OnItemSelected(null, selectedItemChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }

        [Test]
        public void ItemIndexPage_OnAppearing_Valid_Should_Pass()
        {
            // Arrange

            // Warm it up
            var ViewModel = ItemIndexViewModel.Instance;

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemIndexPage_OnAppearing_Valid_Empty_Should_Pass()
        {
            // Arrange

            // Add each model here to warm up and load it.
            var ViewModel = ItemIndexViewModel.Instance;
            ViewModel.Dataset.Clear();

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
