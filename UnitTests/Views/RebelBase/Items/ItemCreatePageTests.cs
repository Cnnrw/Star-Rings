using Game;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ItemCreatePageTests : ItemCreatePage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemCreatePage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App            app;
        private ItemCreatePage page;

        public ItemCreatePageTests() : base(true) { }

        [Test]
        public void ItemCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page._viewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        // [Test]
        // public void ItemCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        // {
        //     // Arrange
        //
        //     // Act
        //     OnBackButtonPressed();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }

        [Test]
        public void ItemCreatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldRange = 0.0;
            var newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Range_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var oldDamage = 0.0;
            var newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Damage_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
