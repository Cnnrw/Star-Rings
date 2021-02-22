using System.Linq;

using Game;
using Game.Enums;
using Game.Models;
using Game.ViewModels;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.TestHelpers
{
    [TestFixture]
    public class ImageButtonExtensionsTests
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App app;

        [Test]
        public void PerformClick_Valid_No_Command_Should_Pass()
        {
            // Arrange
            var imageButtonView = new ImageButton();

            // Act
            imageButtonView.PerformClick();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got here
        }

        [Test]
        public void PerformClick_InValid_Null_Command_Should_Fail()
        {
            // Arrange
            var imageButtonView = new ImageButton();

            imageButtonView = null;

            // Act
            imageButtonView.PerformClick();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got here
        }


        [Test]
        public void PerformClick_Valid_Click_Button_Should_Open_Popup()
        {
            // Arrange

            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            var page = new MonsterUpdatePage(new GenericViewModel<MonsterModel>(new MonsterModel()));

            page._viewModel.Data.UniqueItem = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);

            var stackView = page.GetItemToDisplay();

            var imageButtonView = new ImageButton();

            foreach (var i in stackView.Children.Where(x => x.GetType() == typeof(ImageButton)))
            {
                imageButtonView = (ImageButton)i;
            }

            // Act
            imageButtonView.PerformClick();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}