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
    public class BattlePageTests : BattlePage
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

            page = new BattlePage();

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

        private App        app;
        private BattlePage page;

        public BattlePageTests() : base(true) { }

        [Test]
        public void BattlePage_OnAppearing_Should_Pass()
        {
            // Get the current valute

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattlePage_AttackButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.AttackButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_ShowScoreButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ShowScoreButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_ExitButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ExitButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_StartButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            // page.StartButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_BlockButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.BlockButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_StartRoundButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.StartRoundButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_EndRoundButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.EndRoundButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_FigureButton_Clicked_Unknown_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;

            // Act
            page.FigureButton_Clicked(null, null);

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void BattlePage_FigureButton_Clicked_ChoosingMonsterTarget_Should_Pass()
        //{
        //    // Arrange
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.ChoosingMonsterTarget;

        //    // Act
        //    page.FigureButton_Clicked(null, null);

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        //[Test]
        //public void BattlePage_NextButton_Clicked_StartingMonsterTurn_Should_Pass()
        //{
        //    // Arrange
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.StartingMonsterTurn;

        //    // Act
        //    page.NextButton_Clicked(null, null);

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        [Test]
        public void BattlePage_NextButton_Clicked_EndingMonsterTurn_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.EndingMonsterTurn;

            // Act
            page.NextButton_Clicked(null, null);

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_ShowModalRoundOverPage_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ShowModalRoundOverPage();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void BattlePage_ClearMessages_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClearMessages();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_GameMessage_Default_Should_Pass()
        {
            // Arrange

            // Act
            GameMessage();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_GameMessage_LevelUp_Default_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage = "me";

            // Act
            GameMessage();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void BattlePage_NextAttackExample_GameOver_Should_Pass()
        //{
        //    // Arrange

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Clear();

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList
        //                         .Add(new PlayerInfoModel(new MonsterModel()));

        //    BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();

        //    // Has no Character, so should show end game

        //    // Act
        //    page.NextAttackExample();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        [Test]
        public void BattlePage_EndTurn_NewRound_Should_Pass()
        {
            // Arrange
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel
            {
                Speed = 10,
                Level = 1,
                Attack = 5,
                Defense = 5,
                CurrentHealth = 5,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Mike"
            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();

            // Act
            page.EndTurn();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_EndTurn_GameOver_Should_Pass()
        {
            // Arrange

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();

            // Act
            page.EndTurn();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DoCharacterTurn_Should_Pass()
        {
            // Arrange
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel
            {
                Speed = 10,
                Level = 1,
                Attack = 5,
                Defense = 5,
                CurrentHealth = 5,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Mike"
            });

            var MonsterPlayer = new PlayerInfoModel(new MonsterModel
            {
                Speed = 10,
                Level = 1,
                Attack = 5,
                Defense = 5,
                CurrentHealth = 5,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Grog"
            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker = CharacterPlayer;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender = MonsterPlayer;

            // Act
            page.DoCharacterTurn();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_StartMonsterTurn_Should_Pass()
        {
            // Arrange
            var MonsterPlayer = new PlayerInfoModel(new MonsterModel
            {
                Speed = 10,
                Level = 1,
                Attack = 5,
                Defense = 5,
                CurrentHealth = 5,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Grog"
            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker = MonsterPlayer;

            // Act
            page.StartMonsterTurn();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void BattlePage_DoMonsterTurn_Should_Pass()
        //{
        //    // Arrange
        //    var CharacterPlayer = new PlayerInfoModel(new CharacterModel
        //    {
        //        Speed = 10,
        //        Level = 1,
        //        Attack = 5,
        //        Defense = 5,
        //        CurrentHealth = 5,
        //        ExperienceTotal = 1,
        //        ExperienceRemaining = 1,
        //        Name = "Mike"
        //    });

        //    var MonsterPlayer = new PlayerInfoModel(new MonsterModel
        //    {
        //        Speed = 10,
        //        Level = 1,
        //        Attack = 5,
        //        Defense = 5,
        //        CurrentHealth = 5,
        //        ExperienceTotal = 1,
        //        ExperienceRemaining = 1,
        //        Name = "Grog"
        //    });

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker = MonsterPlayer;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender = CharacterPlayer;

        //    // Act
        //    page.DoMonsterTurn();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        [Test]
        public void BattlePage_GameOver_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GameOver();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void BattlePage_SetSelectedCharacter_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    var result = page.SetSelectedCharacter(new MapModelLocation());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(true, result); // Got to here, so it happened...
        //}

        //[Test]
        //public void BattlePage_SetSelectedMonster_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    var result = page.SetSelectedMonster(new MapModelLocation());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(true, result); // Got to here, so it happened...
        //}

        //[Test]
        //public void BattlePage_SetSelectedEmpty_Default_Should_Pass()
        //{
        //    // Arrange

        //    // Act
        //    var result = page.SetSelectedEmpty(new MapModelLocation());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(true, result); // Got to here, so it happened...
        //}

        //[Test]
        //public void BattlePage_UpdateMapGrid_InValid_Bogus_Image_Should_Fail()
        //{
        //    // Make the Row Bogus
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Row = -1;

        //    // Act
        //    var result = page.UpdateMapGrid();

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Row = 0;

        //    // Assert
        //    Assert.AreEqual(false, result); // Got to here, so it happened...
        //}

        //[Test]
        //public void BattlePage_UpdateMapGrid_InValid_Bogus_ImageButton_Should_Fail()
        //{
        //    // Get the current valute
        //    var name = "MapR0C0ImageButton";
        //    page.MapLocationObject.TryGetValue(name, out var data);
        //    page.MapLocationObject.Remove(name);

        //    // Act
        //    var result = page.UpdateMapGrid();

        //    // Reset
        //    page.MapLocationObject.Add(name, data);

        //    // Assert
        //    Assert.AreEqual(false, result); // Got to here, so it happened...
        //}

        //[Test]
        //public void BattlePage_UpdateMapGrid_InValid_Bogus_Stack_Should_Fail()
        //{
        //    // Get the current valute
        //    var nameStack = "MapR0C0Stack";
        //    page.MapLocationObject.TryGetValue(nameStack, out var dataStack);
        //    page.MapLocationObject.Remove(nameStack);

        //    var nameImage = "MapR0C0ImageButton";
        //    page.MapLocationObject.TryGetValue(nameImage, out var dataImage);

        //    page.MapLocationObject.Remove(nameImage);

        //    var dataImageBogus = new ImageButton {AutomationId = "bogus"};
        //    page.MapLocationObject.Add(nameImage, dataImageBogus);

        //    // Act
        //    var result = page.UpdateMapGrid();

        //    // Reset
        //    page.MapLocationObject.Remove(nameImage);
        //    page.MapLocationObject.Add(nameImage, dataImage);
        //    page.MapLocationObject.Add(nameStack, dataStack);

        //    // Assert
        //    Assert.AreEqual(false, result); // Got to here, so it happened...
        //}

        //[Test]
        //public void BattlePage_UpdateMapGrid_Valid_Stack_Should_Pass()
        //{
        //    // Need to build out a valid MapGrid with Engine MapGridLocation

        //    // Make Map in Engine

        //    var MonsterPlayer = new PlayerInfoModel(new MonsterModel());

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel
        //        .Instance.Engine.EngineSettings.PlayerList);

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.AutoBattle = true;

        //    // Make UI Map
        //    //page.CreateMapGridObjects();
        //    page.UpdateMapGrid();

        //    // Move Character in Engine
        //    var result = BattleEngineViewModel.Instance.Engine.Round.Turn.MoveAsTurn(MonsterPlayer);

        //    // Act

        //    // Call for UpateMap
        //    page.UpdateMapGrid();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(true, result); // Got to here, so it happened...
        //}

        //[Test]
        //public async Task BattlePage_ShowBattleSettingsPage_Default_Should_Pass()
        //{
        //    // Get the current valute

        //    // Act
        //    await page.ShowBattleSettingsPage();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got to here, so it happened...
        //}

        [Test]
        public void BattlePage_Settings_Clicked_Default_Should_Pass()
        {
            // Get the current valute

            // Act
            page.Settings_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        //[Test]
        //public void BattleSettingsPage_MakeMapGridBox_InValid_Should_Fail()
        //{
        //    // Arrange
        //    var data = new MapModelLocation
        //    {
        //        Player = null,
        //        Column = 0,
        //        Row = 0
        //    };

        //    // Act
        //    var result = page.MakeMapGridBox(data);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(HitStatusEnum.Default,
        //                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum);
        //}

        [Test]
        public void BattleSettingsPage_ShowBattleMode_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ShowBattleMode();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_Starting_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            // Act
            page.ShowBattleModeUiElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_NewRound_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

            // Act
            page.ShowBattleModeUiElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_GameOver_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

            // Act
            page.ShowBattleModeUiElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_RoundOver_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.RoundOver;

            // Act
            page.ShowBattleModeUiElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_Battling_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Act
            page.ShowBattleModeUiElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_Unknown_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;

            // Act
            page.ShowBattleModeUiElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        //[Test]
        //public void BattleSettingsPage_ShowBattleModeDisplay_MapAbility_Should_Pass()
        //{
        //    // Arrange
        //    var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum =
        //        BattleModeEnum.MapAbility;

        //    // Act
        //    ShowBattleModeDisplay();

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

        //    // Assert
        //    Assert.IsTrue(true); // Got Here
        //}

        //[Test]
        //public void BattleSettingsPage_ShowBattleModeDisplay_MapFull_Should_Pass()
        //{
        //    // Arrange
        //    var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum =
        //        BattleModeEnum.MapFull;

        //    // Act
        //    ShowBattleModeDisplay();

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

        //    // Assert
        //    Assert.IsTrue(true); // Got Here
        //}

        //[Test]
        //public void BattleSettingsPage_ShowBattleModeDisplay_MapNext_Should_Pass()
        //{
        //    // Arrange
        //    var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum =
        //        BattleModeEnum.MapNext;

        //    // Act
        //    ShowBattleModeDisplay();

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

        //    // Assert
        //    Assert.IsTrue(true); // Got Here
        //}

        //[Test]
        //public void BattleSettingsPage_ShowBattleModeDisplay_SimpleAbility_Should_Pass()
        //{
        //    // Arrange
        //    var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum =
        //        BattleModeEnum.SimpleAbility;

        //    // Act
        //    ShowBattleModeDisplay();

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

        //    // Assert
        //    Assert.IsTrue(true); // Got Here
        //}

        //[Test]
        //public void BattleSettingsPage_ShowBattleModeDisplay_SimpleUnknown_Should_Pass()
        //{
        //    // Arrange
        //    var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum =
        //        BattleModeEnum.Unknown;

        //    // Act
        //    ShowBattleModeDisplay();

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

        //    // Assert
        //    Assert.IsTrue(true); // Got Here
        //}

        //[Test]
        //public void BattleSettingsPage_ShowBattleModeDisplay_SimpleNext_Should_Pass()
        //{
        //    // Arrange
        //    var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum =
        //        BattleModeEnum.SimpleNext;

        //    // Act
        //    ShowBattleModeDisplay();

        //    // Reset
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

        //    // Assert
        //    Assert.IsTrue(true); // Got Here
        //}

        // [Test]
        // public void BattleSettingsPage_MapIcon_Clicked_Character_Should_Pass()
        // {
        //     // Arrange
        //     var CharacterPlayer = new PlayerInfoModel(new CharacterModel());
        //     BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);
        //
        //     var MonsterPlayer = new PlayerInfoModel(new MonsterModel());
        //     BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);
        //
        //     BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel
        //         .Instance.Engine.EngineSettings.PlayerList);
        //
        //     // Make UI Map
        //     //page.CreateMapGridObjects();
        //
        //     var nameImage = "MapR0C0ImageButton";
        //     page.MapLocationObject.TryGetValue(nameImage, out var dataImage);
        //
        //     // Act
        //
        //     // Force the click event to fire
        //     ((ImageButton)dataImage)?.PropagateUpClicked();
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(true); // Got Here
        // }

        //[Test]
        //public void BattleSettingsPage_MapIcon_Clicked_Monster_Should_Pass()
        //{
        //    // Arrange
        //    var CharacterPlayer = new PlayerInfoModel(new CharacterModel());
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

        //    var MonsterPlayer = new PlayerInfoModel(new MonsterModel());
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel
        //        .Instance.Engine.EngineSettings.PlayerList);

        //    // Make UI Map
        //    //page.CreateMapGridObjects();

        //    var nameImage = "MapR5C0ImageButton";
        //    page.MapLocationObject.TryGetValue(nameImage, out var dataImage);

        //    // Act

        //    // Force the click event to fire
        //    ((ImageButton)dataImage).PropagateUpClicked();

        //    // Reset

        //    // Assert
        //    Assert.IsTrue(true); // Got Here
        //}

        //    [Test]
        //    public void BattleSettingsPage_MapIcon_Clicked_Empty_Should_Pass()
        //    {
        //        // Arrange
        //        var CharacterPlayer = new PlayerInfoModel(new CharacterModel());
        //        BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

        //        var MonsterPlayer = new PlayerInfoModel(new MonsterModel());
        //        BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

        //        BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel
        //            .Instance.Engine.EngineSettings.PlayerList);

        //        // Make UI Map
        //        //page.DrawMapGridInitialState();

        //        var nameImage = "MapR3C3ImageButton";
        //        page.MapLocationObject.TryGetValue(nameImage, out var dataImage);

        //        // Act

        //        // Force the click event to fire
        //        ((ImageButton)dataImage)?.PropagateUpClicked();

        //        // Reset

        //        // Assert
        //        Assert.IsTrue(true); // Got Here
        //    }

        //[Test]
        //public void BattleSettingsPage_DetermineMapBackgroundColor_Should_Pass()
        //{
        //    // Arrange
        //    MapModelLocation character = new MapModelLocation();
        //    character.Player.PlayerType = PlayerTypeEnum.Character;
        //    MapModelLocation monster = new MapModelLocation();
        //    monster.Player.PlayerType = PlayerTypeEnum.Monster;
        //    MapModelLocation unknown = new MapModelLocation();
        //    unknown.Player.PlayerType = PlayerTypeEnum.Unknown;
        //    MapModelLocation defaultCase = new MapModelLocation();

        //    // Act
        //    var characterColor = BattlePage.DetermineMapBackgroundColor(character);
        //    var monsterColor = BattlePage.DetermineMapBackgroundColor(monster);
        //    var unknownColor = BattlePage.DetermineMapBackgroundColor(unknown);
        //    var defaultColor = BattlePage.DetermineMapBackgroundColor(defaultCase);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(characterColor, Color.Purple);
        //    Assert.AreEqual(monsterColor, Color.Lavender);
        //    Assert.AreEqual(unknownColor, Color.Transparent);
        //    Assert.AreEqual(defaultColor, Color.Transparent);
        //}
    }
}
