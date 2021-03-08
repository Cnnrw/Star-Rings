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
    public class BattleHomePageTests : BattlePage
    {

        private App app;
        private BattleHomePage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            // This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            // For now, set the engine to the Koenig Engine, change when ready
            BattleEngineViewModel.Instance.SetBattleEngineToKoenig();

            page = new BattleHomePage();

            // Put seed data into the system for all tests
            BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            BattleEngineViewModel.Instance.Engine.StartBattle(false);

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList
                                 .Add(new PlayerInfoModel(new CharacterModel()));
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList
                                 .Add(new PlayerInfoModel(new MonsterModel()));
            BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        public BattleHomePageTests() : base(true) { }

        [Test]
        public void BattleHomePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattleHomePage_AutoBattle_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.AutoBattle_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattleHomePage_PickCharacters_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.PickCharacters_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattleHomePage_PickItems_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.PickItems_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattleHomePage_ScorePage_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ScorePage_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
