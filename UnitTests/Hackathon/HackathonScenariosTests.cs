using Game.Enums;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {

        [SetUp]
        public void Setup()
        {
            // Choose which engine to run
            //EngineViewModel.SetBattleEngineToKoenig();
            EngineViewModel.SetBattleEngineToGame();

            // Put seed data into the system for all tests
            EngineViewModel.Engine.Round.ClearLists();

            // Start the Engine in AutoBattle Mode
            EngineViewModel.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
        }

        [TearDown]
        public void TearDown()
        {
        }

        private readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                                                          new CharacterModel
                                                          {
                                                              Speed = -1, // Will go last...
                                                              Level = 1,
                                                              CurrentHealth = 1,
                                                              ExperienceTotal = 1,
                                                              ExperienceRemaining = 1,
                                                              Name = "Mike"
                                                          });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }

        [Test]
        public async Task HackathonScenario_Scenario_2_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      2
            *      
            * Description: 
            *      Make a Character called Bob, who always misses when he attacks
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes:
            *       BasePlayerModel.cs
            *           Add property LandedAttacksCount to track how many attacks a player has landed
            *       PlayerInfoModel.cs
            *           Add LandedAttacksCount assignment in copy constructors
            *       Game.TurnEngine.cs
            *           CalculateAttackStatus()
            *               Add the attacker as a param to RollToHitTarget()
            *           RollToHitTarget()
            *               Add the attacker as a param
            *               If attacker's name is Bob, force d20 to roll 1, resulting in a miss
            *           TurnAsAttack()
            *               On a successful hit, increment the attacker's LandedHitsCount by 1
            * 
            * Test Algorithm:
            *      Create 2 long-lasting Characters (so they'll attack a few times). One named 'Bob' and the other 'Luke'
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Bob's LandedAttacksCount == 0
            *      Verify Luke's LandedAttacksCount > 0
            */

            // Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 2;

            var CharacterBob = new CharacterModel
                {
                    Speed = 4,
                    Level = 3,
                    MaxHealth = 15,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Bob"
                };

            var CharacterLuke = new CharacterModel
            {
                Speed = 4,
                Level = 3,
                MaxHealth = 15,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Luke"
            };

            // Autobattle uses Characters from the Character index, so add Bob and Luke to the start of the
            // list so they're sure to be included in the party
            CharacterIndexViewModel.Instance.Dataset.Insert(0, CharacterBob);
            CharacterIndexViewModel.Instance.Dataset.Insert(1, CharacterLuke);

            // Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            // Reset

            // Assert
            var FinalBob = EngineViewModel.Engine.EngineSettings.BattleScore.CharacterModelDeathList.Find(c => c.Name.Equals("Bob"));
            var FinalLuke = EngineViewModel.Engine.EngineSettings.BattleScore.CharacterModelDeathList.Find(c => c.Name.Equals("Luke"));

            Assert.AreEqual(0, FinalBob.LandedAttacksCount);
            Assert.AreEqual(true, FinalLuke.LandedAttacksCount > 0);
        }

        [Test]
        public async Task HackathonScenario_Scenario_MostlyDeadIsNotEntirelyDead_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      # 10
            *      
            * Description: 
            *      A monster has dealt a killing blow to one of our characters, but fortunately Miracle
            *      Max steps in to revive them to their full health. This can only happen once per Battle, so
            *      the next hero to die is permadead.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      EngineSettingsModel.cs 
            *           BattleGlobals
            *               MiracleMaxCanRevive - Added a public globals class
            *      TurnEngineBase.cs
            *           TurnAsAttack() 
            *               - Added a check to see if MiracleMaxCanRevive and if its applicable
            *               - If he can, prevent damage and exp and instead restore health and update
            * 
            * Test Algrorithm:
            *      Setup a battle where character will die after the first monster attack
            *      Carry out attack round
            *      Ensure the charcter that should've died is still alive
            *      Ensure that the next character that should've died did die
            * 
            * Test Conditions:
            *       NA, using our autobattle engine with default conditions
            * 
            * Validation:
            *      One character should be prevented from dying, the rest should die when lethal damage is dealt
            *  
            */

            // Arrange
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            var _Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = -1, // Will go last...
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Character"
            });
            // Auto Battle will add the monsters, only need to add our character
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(_Character);
            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;
            // Characters always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Miss;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Character")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }

        [Test]
        public async Task HackathonScenario_Scenario_SleeplessZombiesInSeattle_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      # 17
            *      
            * Description: 
            *      When a monster dies, there is a chance they can return from the dead as a 'Zombie' 
            *      and continue the battle
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      EngineSettingsModel.cs 
            *           ChanceForZombie - added an int that sets the probability out of 100 
            *                               that a monsters turns into a zombie
            *       BattleSettingsModel.cs
            *           ZombiesEnabled - bool to control wether zombies are enabled
            *           
            *       TurnEngine.cs
            *           TurnAsAttack - added a check if zombies are enabled
            * 
            * Test Algrorithm:
            *      Setup a battle where a monster will die after the first character attack
            *      Carry out attack round
            *      Ensure the Monster that should've died is still alive as a zombie
            * 
            * Test Conditions:
            *       ChanceForZombie set to 100, indicating 100% chance of zombies
            * 
            * Validation:
            *      Monsters should never die, and the round count should be hit
            */

            // Arrange 
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ZombiesEnabled = true;
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;
            EngineViewModel.Engine.EngineSettings.MaxRoundCount = 10;
            var _Character = new PlayerInfoModel(new CharacterModel
            {
                Speed = 1,
                Level = 1,
                Attack = 100,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                ExperienceRemaining = 1,
                Name = "Character"
            });
            // Auto Battle will add the monsters, only need to add our character
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(_Character);
            // Monsters always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;
            // Characters always miss
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Miss;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.ZombiesEnabled = false;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreNotEqual(null, EngineViewModel.Engine.EngineSettings.MonsterList);
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
    }
}
