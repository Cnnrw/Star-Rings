using Game;
using Game.ViewModels;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterIndexPageTests : MonsterIndexPage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterIndexPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App              app;
        private MonsterIndexPage page;

        public MonsterIndexPageTests() : base(true) { }

        [Test]
        public void MonsterIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterIndexPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterIndexPage_OnAppearing_Valid_Should_Pass()
        {
            // Arrange
            var ViewModel = MonsterIndexViewModel.Instance;

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
            Assert.IsNotNull(ViewModel);
        }

        [Test]
        public void MonsterIndexPage_OnAppearing_Valid_Empty_Should_Pass()
        {
            // Arrange

            var ViewModel = MonsterIndexViewModel.Instance;
            ViewModel.Dataset.Clear();

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
