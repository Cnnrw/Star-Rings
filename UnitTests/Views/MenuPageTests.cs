using System.Linq;

using Game;
using Game.Models;
using Game.Models.Enums;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class MenuPageTests
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MenuPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App      app;
        private MenuPage page;

        [Test]
        public void MenuPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MenuPage_Get_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MenuPage.RootPage;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MenuPage_ListViewMenu_InValid_Null_Should_Fail()
        {
            // Arrange

            var content = (StackLayout)page.Content;
            var listview = (ListView)content.Children.FirstOrDefault();

            // Act
            listview.SelectedItem = null;

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void MenuPage_ListViewMenu_Valid_Game_Should_Pass()
        {
            // Arrange

            var data = new MenuItemModel {Id = MenuItemEnum.Home, Title = "Home"};

            var content = (StackLayout)page.Content;
            var listview = (ListView)content.Children.FirstOrDefault();

            // Act
            listview.SelectedItem = data;

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}