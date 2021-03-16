using System.Collections.Generic;

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
    public class PickCharactersPageTests : PickCharactersPage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            // For now, set the engine to the Koenig Engine, change when ready
            BattleEngineViewModel.Instance.SetBattleEngineToKoenig();

            page = new PickCharactersPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App                app;
        private PickCharactersPage page;

        [Test]
        public void PickCharactersPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PickCharactersPage_Constructor_UT_Default_Should_Pass()
        {
            // Arrange

            // Act

            // Reset

            // Assert
        }

        [Test]
        public void PickCharactersPage_BattleButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickCharactersPage_CreateEngineCharacterList_Default_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList = new List<PlayerInfoModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList
                                 .Add(new PlayerInfoModel(new CharacterModel()));

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList = new List<PlayerInfoModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList
                                 .Add(new PlayerInfoModel(new MonsterModel()));


            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void PickCharactersPage_OnApperaing_Default_Should_Pass()
        //{
        //    // Arrange
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList = new List<PlayerInfoModel>();
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList = new List<PlayerInfoModel>();
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

        //    BattleEngineViewModel.Instance.PartyCharacterList = new ObservableCollection<CharacterModel>();
        //    BattleEngineViewModel.Instance.PartyCharacterList.Add(new CharacterModel());

        //    var temp = page.EngineViewModel.Engine;

        //    page.UpdateNextButtonState();

        //    // Act
        //    OnAppearing();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        [Test]
        public void PickCharactersPage_UpdateButtonState_Default_Should_Pass()
        {
            // Arrange

            // Act

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        // [Test]
        // public void PickCharactersPage_OnPartyCharacterItemSelected_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     var selectedCharacter = new CharacterModel();
        //
        //     var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(selectedCharacter, 0);
        //
        //     // Act
        //     page.OnPartyCharacterItemSelected(null, selectedCharacterChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void PickCharactersPage_OnPartyCharacterItemSelected_InValid_Should_Pass()
        // {
        //     // Arrange
        //
        //     var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);
        //
        //     // Act
        //     page.OnPartyCharacterItemSelected(null, selectedCharacterChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void PickCharactersPage_OnDatabaseCharacterItemSelected_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     var selectedCharacter = new CharacterModel();
        //
        //     var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(selectedCharacter, 0);
        //
        //     // Act
        //     page.OnDatabaseCharacterItemSelected(null, selectedCharacterChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void PickCharactersPage_OnDatabaseCharacterItemSelected_InValid_Should_Pass()
        // {
        //     // Arrange
        //
        //     var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);
        //
        //     // Act
        //     page.OnDatabaseCharacterItemSelected(null, selectedCharacterChangedEventArgs);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got to here, so it happened...
        // }
    }
}
