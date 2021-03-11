using System.Collections.Generic;
using System.Threading.Tasks;

using Game.Engine.EngineGame;
using Game.Enums;
using Game.Models;

using NUnit.Framework;

namespace Scenario
{
    [TestFixture]
    public class GameAutoBattleEngineScenarioTests
    {

        [SetUp]
        public void Setup()
        {
            AutoBattle = new AutoBattleEngine();

            AutoBattle.Battle.EngineSettings.CharacterList.Clear();
            AutoBattle.Battle.EngineSettings.MonsterList.Clear();
            AutoBattle.Battle.EngineSettings.CurrentDefender = null;
            AutoBattle.Battle.EngineSettings.CurrentAttacker = null;

            AutoBattle.Battle.StartBattle(true); // Clear the Engine
        }

        [TearDown]
        public void TearDown()
        {
        }

        private AutoBattleEngine AutoBattle;

        [Test]
        public void AutoBattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AutoBattle;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Monsters_1_Should_Pass()
        {
            // Arrange

            // Set the round location

            AutoBattle.Battle.EngineSettings.RoundLocation = BattleLocationEnum.Mordor;

            // Add Characters

            AutoBattle.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = -1,
                    Level = 10,
                    CurrentHealth = 100,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Luke",
                    Job = CharacterJobEnum.Jedi,
                    ListOrder = 1
                }
            );

            AutoBattle.Battle.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Add Monsters

            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            AutoBattle.Battle.EngineSettings.MaxNumberPartyMonsters = 1;

            AutoBattle.Battle.EngineSettings.MonsterList.AddRange(GetListOfMonstersForEachLocation());

            //Act
            var result = await AutoBattle.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(false, result);
        }

        public List<PlayerInfoModel> GetListOfMonstersForEachLocation()
        {
            List<PlayerInfoModel> myList = new List<PlayerInfoModel> {
                new PlayerInfoModel(
                    new MonsterModel
                    {
                        Speed = -1,
                        Level = 1,
                        CurrentHealth = 1,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "Shire Monster",
                        BattleLocation = BattleLocationEnum.Shire,
                        ListOrder = 2
                    }
                ),
                new PlayerInfoModel(
                    new MonsterModel
                    {
                        Speed = -1,
                        Level = 1,
                        CurrentHealth = 1,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "ElvenCity Monster",
                        BattleLocation = BattleLocationEnum.ElvenCity,
                        ListOrder = 3
                    }
                ),
                new PlayerInfoModel(
                    new MonsterModel
                    {
                        Speed = -1,
                        Level = 1,
                        CurrentHealth = 1,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "Forest Monster",
                        BattleLocation = BattleLocationEnum.Forest,
                        ListOrder = 4
                    }
                ),
                new PlayerInfoModel(
                    new MonsterModel
                    {
                        Speed = -1,
                        Level = 1,
                        CurrentHealth = 1,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "Dungeon Monster",
                        BattleLocation = BattleLocationEnum.Dungeons,
                        ListOrder = 5
                    }
                ),
                new PlayerInfoModel(
                    new MonsterModel
                    {
                        Speed = -1,
                        Level = 1,
                        CurrentHealth = 1,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "Mordor Monster",
                        BattleLocation = BattleLocationEnum.Mordor,
                        ListOrder = 6
                    }
                )
            };

            return myList;
        }
    }
}
