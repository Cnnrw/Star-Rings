using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Enums;
using Game.Helpers;
using Game.Models;

namespace Game.Engine.EngineGame
{
    /*
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     *
     */

    /// <summary>
    /// Engine controls the turns
    ///
    /// A turn is when a Character takes an action or a Monster takes an action
    ///
    /// </summary>
    public class TurnEngine : TurnEngineBase, ITurnEngineInterface
    {

        // Hold the BaseEngine
        public EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool TakeTurn(PlayerInfoModel ActivePlayer)
        {
            bool result = false;

            if (EngineSettings.ForcedPlayerAction != ActionEnum.Unknown)
            {
                EngineSettings.CurrentAction = EngineSettings.ForcedPlayerAction;
            }

            // If the action is not set, then try to set it or just attack
            if (EngineSettings.CurrentAction == ActionEnum.Unknown)
            {
                // Set the action if one is not set
                EngineSettings.CurrentAction = DetermineActionChoice(ActivePlayer);
            }

            // Act based on the Current Action
            switch (EngineSettings.CurrentAction)
            {
                case ActionEnum.Attack:
                    result = Attack(ActivePlayer);
                    break;

                default:
                    result = Block(ActivePlayer);
                    break;
            }

            // Increment Turn Count so you know what turn number
            EngineSettings.BattleScore.TurnCount++;

            // Save the Previous Action off
            EngineSettings.PreviousAction = EngineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            EngineSettings.CurrentAction = ActionEnum.Unknown;

            return result;
        }

        /// <summary>
        /// Determine what Actions to do
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override ActionEnum DetermineActionChoice(PlayerInfoModel Attacker)
        {
            // If it is the characters turn, and NOT auto battle, use what was sent into the engine
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                if (EngineSettings.BattleScore.AutoBattle == false)
                {
                    return EngineSettings.CurrentAction;
                }
            }

            /*
             * The following is Used for Monsters, and Auto Battle Characters
             * 
             * Randomly choose between attacking and blocking
             */

            int RollResult = DiceHelper.RollDice(1, 5);

            if (RollResult == 1)
            {
                EngineSettings.CurrentAction = ActionEnum.Attack;
            } else
            {
                EngineSettings.CurrentAction = ActionEnum.Block;
            }

            return EngineSettings.CurrentAction;
        }

        /// <summary>
        /// Attack as a Turn
        ///
        /// Pick who to go after
        ///
        /// Determine Attack Score
        /// Determine DefenseScore
        ///
        /// Do the Attack
        ///
        /// </summary>
        public override bool Attack(PlayerInfoModel ActivePlayer)
        {
            return base.Attack(ActivePlayer);
        }

        /// <summary>
        /// Decide which Player to attack
        /// </summary>
        /// <param name="AttackingPlayer">The Player who is currently attacking</param>
        /// <returns></returns>
        public override PlayerInfoModel AttackChoice(PlayerInfoModel AttackingPlayer)
        {
            return base.AttackChoice(AttackingPlayer);
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        public override PlayerInfoModel SelectCharacterToAttack()
        {
            // Select the Character with the lowest Current Health (that is still alive)
            var Defender = EngineSettings.PlayerList
                .Where(c => c.Alive && c.PlayerType == PlayerTypeEnum.Character)
                .OrderBy(c => c.CurrentHealth)
                .FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        public override PlayerInfoModel SelectMonsterToAttack()
        {
            // Select the Monster with the lowest Level (that is still alive)
            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderBy(m => m.Level)
                .FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        public override bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null) return false;

            if (Target == null) return false;

            // Set Messages to empty
            EngineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            EngineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            switch (EngineSettings.BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.CriticalMiss:
                    // It's a Critical Miss, so Bad things may happen
                    DetermineCriticalMissProblem(Attacker);

                    break;

                case HitStatusEnum.CriticalHit:
                case HitStatusEnum.Hit:
                    // It's a Hit

                    Attacker.LandedAttacksCount++;

                    // Calculate Damage
                    EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue(EngineSettings.RoundLocation);

                    // If critical Hit, double the damage
                    if (EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit) EngineSettings.BattleMessagesModel.DamageAmount *= 2;

                    // Apply the Damage
                    ApplyDamage(Target);

                    EngineSettings.BattleMessagesModel.TurnMessageSpecial =
                        EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    RemoveIfDead(Target);

                    // If it is a character apply the experience earned
                    CalculateExperience(Attacker, Target);

                    break;
            }

            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name +
                                                              EngineSettings.BattleMessagesModel.AttackStatus +
                                                              Target.Name +
                                                              EngineSettings.BattleMessagesModel.TurnMessageSpecial +
                                                              EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// Block incoming attacks by raising defense
        /// </summary>
        /// <returns></returns>
        public bool Block(PlayerInfoModel Blocker)
        {
            if (EngineSettings.BattleScore.AutoBattle)
            {
                // Choose who to block
                EngineSettings.CurrentDefender = BlockChoice(Blocker);
            }

            // Perform block
            TurnAsBlock(Blocker, EngineSettings.CurrentDefender);

            return true;
        }

        /// <summary>
        /// Decide who to block
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual PlayerInfoModel BlockChoice(PlayerInfoModel Blocker)
        {
            switch (Blocker.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToBlock();

                default:
                    return SelectMonsterToBlock();
            }
        }

        /// <summary>
        /// Pick the Character to Block
        /// </summary>
        /// <returns></returns>
        public virtual PlayerInfoModel SelectCharacterToBlock()
        {
            // Monsters are dumb, so they only Block the first Character in the list
            var BlockedCharacter = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                .OrderBy(m => m.ListOrder)
                .FirstOrDefault();

            return BlockedCharacter;
        }

        /// <summary>
        /// Pick the Monster to Block
        /// </summary>
        /// <returns></returns>
        public virtual PlayerInfoModel SelectMonsterToBlock()
        {
            // Block the Monster with the highest Attack
            var BlockedMonster = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderBy(m => m.Attack)
                .FirstOrDefault();

            return BlockedMonster;
        }

        /// <summary>
        /// Player Blocks Player
        /// </summary>
        /// <param name="Blocker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public virtual bool TurnAsBlock(PlayerInfoModel Blocker, PlayerInfoModel Target)
        {
            // Set Messages to empty
            EngineSettings.BattleMessagesModel.ClearMessages();

            
            int RollResult = DiceHelper.RollDice(1, 10);

            EngineSettings.BattleMessagesModel.TurnMessage =
                Blocker.Name +
                " tries to block " +
                Target.Name +
                " but misses";

            // Apply the Block on the target's Attack
            if (RollResult <= 3)
            {
                EngineSettings.BattleMessagesModel.TurnMessage =
                    Blocker.Name +
                    " considers blocking " +
                    Target.Name +
                    " but sees they're already weak";

                if (Target.Attack > 1)
                {
                    Target.Attack -= 1;

                    EngineSettings.BattleMessagesModel.TurnMessage =
                        Blocker.Name +
                        " blocks " +
                        Target.Name +
                        " and drops their attack to " +
                        Target.Attack;
                }
            }

            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            return base.BattleSettingsOverride(Attacker);
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            return base.BattleSettingsOverrideHitStatusEnum(myEnum);
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        public override int ApplyDamage(PlayerInfoModel Target)
        {
            return base.ApplyDamage(Target);
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        public override HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            // Remember Current Player
            EngineSettings.BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            // Choose who to attack
            EngineSettings.BattleMessagesModel.TargetName = Target.Name;
            EngineSettings.BattleMessagesModel.AttackerName = Attacker.Name;

            // Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;

            EngineSettings.BattleMessagesModel.HitStatus = RollToHitTarget(Attacker, Target, AttackScore, DefenseScore);

            return EngineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        public override bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateExperience(Attacker, Target);
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        public override bool RemoveIfDead(PlayerInfoModel Target)
        {
            return base.RemoveIfDead(Target);
        }

        /// <summary>
        /// Target Died
        ///
        /// Process for death...
        ///
        /// Returns the count of items dropped at death
        /// </summary>
        public override bool TargetDied(PlayerInfoModel Target)
        {
            bool found;

            // Mark Status in output
            EngineSettings.BattleMessagesModel.TurnMessageSpecial = " and deals a deadly blow! ";

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the Character to the killed list
                    EngineSettings.BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.CharacterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = EngineSettings.CharacterList.Remove(EngineSettings.CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    EngineSettings.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    EngineSettings.BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.MonsterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = EngineSettings.MonsterList.Remove(EngineSettings.MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        public override int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped: \n";

            // Drop Items into an Item Pool
            var ItemPool = Target.DropAllItems();

            // Add to ScoreModel
            foreach (var ItemModel in ItemPool)
            {
                EngineSettings.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            EngineSettings.ItemPool.AddRange(ItemPool);

            if (ItemPool.Count == 0)
            {
                DroppedMessage = " Nothing. ";
            }

            EngineSettings.BattleMessagesModel.DroppedMessage = DroppedMessage;

            EngineSettings.BattleScore.ItemModelDropList.AddRange(ItemPool);

            return ItemPool.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="Attacker">The player that is trying to hit</param>
        /// <param name="Target">The attacker's target</param>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        public HitStatusEnum RollToHitTarget(PlayerInfoModel Attacker, PlayerInfoModel Target, int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (Attacker.Name.Equals("Bob"))
            {
                d20 = 1;
            }

            if (d20 == 1)
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to miss ";

                if (EngineSettings.BattleSettingsModel.AllowCriticalMiss)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;
                }

                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for hit ";
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

                if (EngineSettings.BattleSettingsModel.AllowCriticalHit)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalHit;
                }
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.DamageAmount = 0;
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

            return EngineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Critical Miss Problem
        /// </summary>
        public override bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return base.DetermineCriticalMissProblem(attacker);
        }

        #region Algrorithm

        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over

        #endregion Algrorithm
    }
}
