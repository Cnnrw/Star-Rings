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
    public class ScorePageTests
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

            page = new ScorePage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App       app;
        private ScorePage page;

        [Test]
        public void ScorePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ScorePage_CreateCharacterBox_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new CharacterModel());

            // Act
            ScorePage.CreateCharacterDisplayBox(data);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScorePage_CreateCharacterBox_Null_Should_Pass()
        {
            // Arrange

            // Act
            ScorePage.CreateCharacterDisplayBox(null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScorePage_CreateMonsterBox_Default_Should_Pass()
        {
            // Arrange
            var data = new PlayerInfoModel(new MonsterModel());

            // Act
            ScorePage.CreateMonsterDisplayBox(data);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScorePage_CreateMonsterBox_Null_Should_Pass()
        {
            // Arrange

            // Act
            ScorePage.CreateMonsterDisplayBox(null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScorePage_CreateItemBox_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();

            // Act
            ScorePage.CreateItemDisplayBox(data);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScorePage_CreateItemBox_Null_Should_Pass()
        {
            // Arrange

            // Act
            ScorePage.CreateItemDisplayBox(null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScorePage__Default_Should_Pass()
        {
            // Arrange

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.CharacterModelDeathList
                                 .Add(new PlayerInfoModel(new CharacterModel()));

            // Draw the Monsters
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelDeathList
                                 .Add(new PlayerInfoModel(new CharacterModel()));

            // Draw the Items
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());

            // Act
            page.DrawOutput();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
