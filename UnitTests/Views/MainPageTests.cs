using Game;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class MainPageTests
    {
        [SetUp]
        public void Setup()
        {
            // Initialize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            _app = new App();
            Application.Current = _app;

            _page = new MainPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App      _app;
        private MainPage _page;

        [Test]
        public void MainPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = _page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        // [Test]
        // public void MainPage_DungeonButton_Clicked_Default_Should_Pass()
        // {
        //     // Arrange
        //     // Act
        //     page.DungeonButton_Clicked(null, null);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void MainPage_VillageButton_Clicked_Default_Should_Pass()
        // {
        //     // Arrange
        //     // Act
        //     page.RebelBaseButton_Clicked(null, null);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void MainPage_AutoBattleButton_Clicked_Default_Should_Pass()
        // {
        //     // Arrange
        //     // Act
        //     page.AutobattleButton_Clicked(null, null);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
    }
}
