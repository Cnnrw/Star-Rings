using Game;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    public class MonsterCellTests
    {
        [SetUp]
        public void Setup()
        {
            // Initialize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            _app = new App();
            Application.Current = _app;
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App _app;

        [Test]
        public void MonsterCell_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new MonsterCell();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
