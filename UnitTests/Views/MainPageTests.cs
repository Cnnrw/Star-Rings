using System.Threading.Tasks;

using Game;
using Game.Models.Enums;
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
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MainPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        App      app;
        MainPage page;

        [Test]
        public void MainPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task MainPage_Navigate_About_Should_Pass()
        {
            // Arrange

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.About);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Game_Should_Pass()
        {
            // Arrange

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Home);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Village_Should_Pass()
        {
            // Arrange

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Village);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Battle_Should_Pass()
        {
            // Arrange

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Battle);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Game_Twice_Should_Skip()
        {
            // Arrange

            await page.NavigateFromMenu((int)MenuItemEnum.Home);

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Home);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Invalid_ID_100_Should_Skip()
        {
            // Arrange

            page.MenuPages.Add(100, null);

            // Act
            await page.NavigateFromMenu(100);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task MainPage_Navigate_Device_Android_Game_Should_Pass()
        {
            // Arrange
            MockForms.Init(Device.Android);

            await page.NavigateFromMenu((int)MenuItemEnum.Home);

            // Act
            await page.NavigateFromMenu((int)MenuItemEnum.Home);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}