using Game;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class LandingPageTests
    {

        [SetUp]
        public void Setup()
        {
            // Initialize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new LandingPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App         app;
        private LandingPage page;

        [Test]
        public void LandingPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void LandingPage_GameButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.StartButton_OnReleased(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
