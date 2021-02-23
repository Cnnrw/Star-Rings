using Game;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{

    [TestFixture]
    public class AppTests
    {
        /// <remark>
        /// SetUp runs before / TearDown runs after each test.
        /// </remark>
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();

            // This is your App.xaml and App.xaml.cs, which can have resources, etc.
            Application.Current = new App();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        [Test]
        public void App_Constructor_Default_Should_Pass()
        {
            // Arrange
            // Act
            // Reset
            // Assert
            Assert.IsNotNull(Application.Current);
        }

        [Test]
        public void App_OnResume_Default_Should_Pass()
        {
            // Arrange
            // Act
            Application.Current.SendResume();
            // Reset
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void App_OnSleep_Default_Should_Pass()
        {
            // Arrange
            // Act
            Application.Current.SendSleep();
            // Reset
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void App_OnStart_Default_Should_Pass()
        {
            // Arrange
            // Act
            Application.Current.SendStart();
            // Reset
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void App_InitializeComponent_Default_Should_Pass()
        {
            // Arrange
            // Act
            var result = Application.Current.Resources.TryGetValue("PageBackgroundColor", out var value);
            //Reset
            // Assert
            Assert.That(result, Is.True);
            Assert.That(value, Is.Not.Null);
        }
    }
}
