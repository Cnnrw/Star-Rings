using System.Collections.Generic;
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

                case ActionEnum.Block:
                    // TODO: implement Block(ActivePlayer)
                    result = Attack(ActivePlayer);
                    break;

                case ActionEnum.Ability:
                    result = UseAbility(ActivePlayer);
                    break;

                case ActionEnum.Move:
                    result = MoveAsTurn(ActivePlayer);
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
             * Order of Priority
             * If player ... then Attack,
             * otherwise ... Block
             */

            int RollResult = DiceHelper.RollDice(1, 2);

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
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        public override bool MoveAsTurn(PlayerInfoModel Attacker)
        {
            /*
             * TODO: TEAMS Work out your own move logic if you are implementing move
             *
             * Mike's Logic
             * The monster or charcter will move to a different square if one is open
             * Find the Desired Target
             * Jump to the closest space near the target that is open
             *
             * If no open spaces, return false
             *
             */

            // If the Monster the calculate the options
            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                // For Attack, Choose Who

                // Get X, Y for Defender

                // Get X, Y for the Attacker

                // Find Location Nearest to Defender that is Open.

                // Get the Open Locations

                // Format a message to show

                throw new System.NotImplementedException();
            }

            return true;
        }

        /// <summary>
        /// Use the Ability
        /// </summary>
        /// <param name="ActivePlayer"></param>
        /// <returns></returns>
        public override bool UseAbility(PlayerInfoModel ActivePlayer)
        {
            return base.UseAbility(ActivePlayer);
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
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

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
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

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
            return base.TurnAsAttack(Attacker, Target);
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
            return base.CalculateAttackStatus(Attacker, Target);
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
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        public override HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            return base.RollToHitTarget(AttackScore, DefenseScore);
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
