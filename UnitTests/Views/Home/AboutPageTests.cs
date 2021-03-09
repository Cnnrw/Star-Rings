using System.Threading.Tasks;

using Game;
using Game.Models;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class AboutPageTests : AboutPage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new AboutPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App app;
        private AboutPage page;

        public AboutPageTests() : base(true) { }

        [Test]
        public void AboutPage_Default_Constructor_Should_Pass()
        {
            // Arrange

            // Act
            var result = new AboutPage();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
