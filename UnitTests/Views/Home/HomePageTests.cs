using Game;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class HomePageTests
    {

        [SetUp]
        public void Setup()
        {
            // Initialize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new HomePage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        App      app;
        HomePage page;

        [Test]
        public void HomePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GamePage_DungeonButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.DungeonButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void GamePage_VillageButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.VillageButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void GamePage_AutobattleButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AutobattleButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}