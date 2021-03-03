using Game;
using Game.Models;
using Game.ViewModels;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class CharacterIndexPageTests : CharacterIndexPage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new CharacterIndexPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App                app;
        private CharacterIndexPage page;

        public CharacterIndexPageTests() : base(true) { }

        [Test]
        public void CharacterIndexPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterIndexPage_AddCharacter_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.AddCharacter_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterIndexPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        // TODO: figure out how SelectionChangedEventArgs can be mocked
        // [Test]
        // public void CharacterIndexPage_OnCharacterSelected_Clicked_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     var selectedCharacter = new CharacterModel();
        //
        //     var selectedCharacterChangedEventArgs = new SelectionChangedEventArgs(0, selectedCharacter);
        //
        //     // Act
        //     page.OnCharacterSelected(null, selectedCharacterChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void CharacterIndexPage_OnCharacterSelected_Clicked_Invalid_Null_Should_Fail()
        // {
        //     // Arrange
        //
        //     var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);
        //
        //     // Act
        //     page.OnItemSelected(null, selectedCharacterChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }

        [Test]
        public void CharacterIndexPage_OnAppearing_Valid_Should_Pass()
        {
            // Arrange
            var viewModel = CharacterIndexViewModel.Instance;

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void CharacterIndexPage_OnAppearing_Valid_Empty_Should_Pass()
        {
            // Arrange

            var ViewModel = CharacterIndexViewModel.Instance;
            ViewModel.Dataset.Clear();

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        // TODO: Comment this out
        // [Test]
        // public void CharacterIndexPage_AddItemClicked_Valid_Should_Pass()
        // {
        //     // Arrange
        //     // Act
        //     page.AddItem_Clicked(null, null);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
    }
}
