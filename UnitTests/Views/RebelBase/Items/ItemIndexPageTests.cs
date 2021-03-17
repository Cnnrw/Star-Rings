using Game;
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
            _app = new App();
            Application.Current = _app;

            _page = new ItemIndexPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App           _app;
        private ItemIndexPage _page;

        public ItemIndexPageTests() : base(true) { }

        [Test]
        public void ItemIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = _page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
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

        [Test]
        public void ItemIndexPage_OnAppearing_Valid_Should_Pass()
        {
            // Arrange

            // Warm it up
            var unused = ItemIndexViewModel.Instance;

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
