using System.Linq;
using System.Threading.Tasks;

using Game;
using Game.Enums;
using Game.Models;
using Game.ViewModels;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterReadPageTests : MonsterReadPage
    {

        [SetUp]
        public void Setup()
        {
            // Initialize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            _app = new App();
            Application.Current = _app;

            _page = new MonsterReadPage(new GenericViewModel<MonsterModel>(new MonsterModel()));
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App             _app;
        private MonsterReadPage _page;

        public MonsterReadPageTests() : base(true) { }

        [Test]
        public void MonsterReadPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = _page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterReadPage_Update_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            _page.Update_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterReadPage_Delete_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            _page.Delete_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterReadPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterReadPage_GetItemToDisplay_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _page.GetItemToDisplay();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterReadPage_GetItemToDisplay_Valid_Popup_Should_Have_Clickable_Button()
        {
            // Arrange
            _page._viewModel.Data.UniqueItem = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);

            // Act
            _page.GetItemToDisplay();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterReadPage_GetItemToDisplay_Valid_Click_Button_Should_Open_Popup()
        {
            // Arrange
            _page._viewModel.Data.UniqueItem = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);

            var stackView = _page.GetItemToDisplay();

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


        // TODO: Comment These

        // [Test]
        // public void MonsterReadPage_ShowPopup_Valid_Should_Pass()
        // {
        //     // Arrange
        //
        //     // Act
        //     page.ShowPopup(new ItemModel());
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void MonsterReadPage_ClosePopup_Clicked_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     // Act
        //     page.ClosePopup_Clicked(null, null);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }

        // [Test]
        // public void MonsterReadPage_AddItemsToDisplay_With_Data_Should_Remove_And_Pass()
        // {
        //     // Arrange
        //
        //     // Put some data into the box so it can be removed
        //     var itemBox = (FlexLayout)_page.Content.FindByName("ItemBox");
        //
        //     itemBox.Children.Add(new Label());
        //     itemBox.Children.Add(new Label());
        //
        //     // Act
        //     _page.AddItemsToDisplay();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.AreEqual(1, itemBox.Children.Count()); // Got to here, so it happened...
        // }

        [Test]
        public async Task MonsterReadPage_GetItemToDisplay_With_Item_Should_Pass()
        {
            // Arrange
            ItemIndexViewModel.Instance.Dataset.Clear();

            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel {Location = ItemLocationEnum.Head});

            var Monster = new MonsterModel();
            Monster.Head = ItemIndexViewModel.Instance.GetLocationItems(ItemLocationEnum.Head).First().Id;
            _page._viewModel.Data = Monster;

            // Act
            var result = _page.GetItemToDisplay();

            // Reset
            ItemIndexViewModel.Instance.ForceDataRefresh();

            // Assert
            Assert.AreEqual(2, result.Children.Count()); // Got to here, so it happened...
        }

        [Test]
        public async Task MonsterReadPage_GetItemToDisplay_With_NoItem_Should_Pass()
        {
            // Arrange
            ItemIndexViewModel.Instance.Dataset.Clear();
            await ItemIndexViewModel.Instance.CreateAsync(new ItemModel {Location = ItemLocationEnum.Head});

            // Act
            var result = _page.GetItemToDisplay();

            // Reset
            ItemIndexViewModel.Instance.ForceDataRefresh();

            // Assert
            Assert.AreEqual(2, result.Children.Count()); // Got to here, so it happened...
        }

        [Test]
        public void MonsterReadPage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            var StackItem = _page.GetItemToDisplay();
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
