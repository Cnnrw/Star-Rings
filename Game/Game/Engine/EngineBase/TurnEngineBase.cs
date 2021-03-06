using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Enums;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;

namespace Game.Engine.EngineBase
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
    ///     Engine controls the turns
    ///     A turn is when a Character takes an action or a Monster takes an action
    /// </summary>
    public class TurnEngineBase : ITurnEngineInterface
    {

        // Hold the BaseEngine
        readonly EngineSettingsModel _engineSettings = EngineSettingsModel.Instance;
        public BattleGlobals _globals = new BattleGlobals();

        // The Turn Engine
        public ITurnEngineInterface Turn = null;

        /// <summary>
        ///     CharacterModel Attacks...
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual bool TakeTurn(PlayerInfoModel attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            // If the action is not set, then try to set it or use Attack
            if (_engineSettings.CurrentAction == ActionEnum.Unknown)
            {
                // Set the action if one is not set
                _engineSettings.CurrentAction = DetermineActionChoice(attacker);

                // When in doubt, attack...
                if (_engineSettings.CurrentAction == ActionEnum.Unknown)
                    _engineSettings.CurrentAction = ActionEnum.Attack;
            }

            var result = _engineSettings.CurrentAction switch
            {
                ActionEnum.Attack  => Attack(attacker),
                ActionEnum.Ability => UseAbility(attacker),
                ActionEnum.Move    => MoveAsTurn(attacker),
                _                  => false,
            };

            _engineSettings.BattleScore.TurnCount++;

            // Save the Previous Action off
            _engineSettings.PreviousAction = _engineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            _engineSettings.CurrentAction = ActionEnum.Unknown;

            return result;
        }

        /// <summary>
        ///     Determine what Actions to do
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual ActionEnum DetermineActionChoice(PlayerInfoModel attacker)
        {
            // If it is the characters turn, and NOT auto battle, use what was sent into the engine
            if (attacker.PlayerType == PlayerTypeEnum.Character)
                if (_engineSettings.BattleScore.AutoBattle == false)
                    return _engineSettings.CurrentAction;

            /*
             * The following is Used for Monsters, and Auto Battle Characters
             *
             * Order of Priority
             * If can attack Then Attack
             * Next use Ability or Move
             */

            // Assume Move if nothing else happens
            _engineSettings.CurrentAction = ActionEnum.Move;

            // Check to see if ability is avaiable
            if (ChooseToUseAbility(attacker))
            {
                _engineSettings.CurrentAction = ActionEnum.Ability;
                return _engineSettings.CurrentAction;
            }

            // See if Desired Target is within Range, and if so attack away
            if (_engineSettings.MapModel.IsTargetInRange(attacker, AttackChoice(attacker))) _engineSettings.CurrentAction = ActionEnum.Attack;

            return _engineSettings.CurrentAction;
        }

        /// <summary>
        ///     Find a Desired Target
        ///     Move close to them
        ///     Get to move the number of Speed
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual bool MoveAsTurn(PlayerInfoModel attacker)
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

            if (attacker.PlayerType != PlayerTypeEnum.Monster)
                return true;


            // For Attack, Choose Who
            _engineSettings.CurrentDefender = AttackChoice(attacker);

            if (_engineSettings.CurrentDefender == null)
                return false;

            // Get X, Y for Defender
            var locationDefender = _engineSettings.MapModel.GetLocationForPlayer(_engineSettings.CurrentDefender);
            if (locationDefender == null)
                return false;

            var locationAttacker = _engineSettings.MapModel.GetLocationForPlayer(attacker);
            if (locationAttacker == null)
                return false;

            // Find Location Nearest to Defender that is Open.

            // Get the Open Locations
            var openSquare = _engineSettings.MapModel.ReturnClosestEmptyLocation(locationDefender);

            Debug.WriteLine(
                            $"{locationAttacker.Player.Name} moves from {locationAttacker.Column},{locationAttacker.Row} to {openSquare.Column},{openSquare.Row}");

            _engineSettings.BattleMessagesModel.TurnMessage =
                attacker.Name + " moves closer to " + _engineSettings.CurrentDefender.Name;

            return _engineSettings.MapModel.MovePlayerOnMap(locationAttacker, openSquare);
        }

        /// <summary>
        ///     Decide to use an Ability or not
        ///     Set the Ability
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual bool ChooseToUseAbility(PlayerInfoModel attacker)
        {
            // See if healing is needed.
            _engineSettings.CurrentActionAbility = attacker.SelectHealingAbility();
            if (_engineSettings.CurrentActionAbility != AbilityEnum.Unknown)
            {
                _engineSettings.CurrentAction = ActionEnum.Ability;
                return true;
            }

            // If not needed, then role dice to see if other ability should be used
            // <30% chance
            if (DiceHelper.RollDice(1, 10) >= 3)
                return false;

            _engineSettings.CurrentActionAbility = attacker.SelectAbilityToUse();

            if (_engineSettings.CurrentActionAbility == AbilityEnum.Unknown)
                return false;

            // Ability can , switch to unknown to exit
            _engineSettings.CurrentAction = ActionEnum.Ability;
            return true;
        }

        /// <summary>
        ///     Use the Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public virtual bool UseAbility(PlayerInfoModel Attacker)
        {
            _engineSettings.BattleMessagesModel.TurnMessage =
                Attacker.Name + " Uses Ability " + _engineSettings.CurrentActionAbility.ToMessage();
            return Attacker.UseAbility(_engineSettings.CurrentActionAbility);
        }

        /// <summary>
        ///     Attack as a Turn
        ///     Pick who to go after
        ///     Determine Attack Score
        ///     Determine DefenseScore
        ///     Do the Attack
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public virtual bool Attack(PlayerInfoModel Attacker)
        {
            // INFO: Teams, AttackChoice will auto pick the target, good for auto battle
            if (_engineSettings.BattleScore.AutoBattle)
            {
                // For Attack, Choose Who
                _engineSettings.CurrentDefender = AttackChoice(Attacker);

                if (_engineSettings.CurrentDefender == null) return false;
            }

            // Do Attack
            TurnAsAttack(Attacker, _engineSettings.CurrentDefender);

            return true;
        }

        /// <summary>
        ///     Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            switch (data.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToAttack();

                default:
                    return SelectMonsterToAttack();
            }
        }

        /// <summary>
        ///     Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public virtual PlayerInfoModel SelectCharacterToAttack()
        {
            if (_engineSettings.PlayerList == null) return null;

            if (_engineSettings.PlayerList.Count < 1) return null;

            // Select first in the list

            // TODO: Teams, You need to implement your own Logic can not use mine.
            var Defender = _engineSettings.PlayerList
                                          .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                                          .OrderBy(m => m.ListOrder)
                                          .FirstOrDefault();

            return Defender;
        }

        /// <summary>
        ///     Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public virtual PlayerInfoModel SelectMonsterToAttack()
        {
            if (_engineSettings.PlayerList == null) return null;

            if (_engineSettings.PlayerList.Count < 1) return null;

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first

            // TODO: Teams, You need to implement your own Logic can not use mine.

            var Defender = _engineSettings.PlayerList
                                          .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                                          .OrderBy(m => m.CurrentHealth)
                                          .FirstOrDefault();

            return Defender;
        }

        /// <summary>
        ///     // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public virtual bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null) return false;

            if (Target == null) return false;

            // Set Messages to empty
            _engineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            _engineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            switch (_engineSettings.BattleMessagesModel.HitStatus)
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

                    //Calculate Damage
                    _engineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue(_engineSettings.RoundLocation);

                    // If critical Hit, double the damage
                    if (_engineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit) _engineSettings.BattleMessagesModel.DamageAmount *= 2;

                    // If MiracleMax can prevent character death, revive the character
                    if (_globals.MiracleMaxCanRevive &&
                        Target.PlayerType == PlayerTypeEnum.Character &&
                        _engineSettings.BattleMessagesModel.DamageAmount >= Target.CurrentHealth)
                    {
                        // Can only revive once per battle
                        _globals.MiracleMaxCanRevive = false;

                        // Restore the Target to full health
                        Target.CurrentHealth = Target.MaxHealth;

                        // Annouce it to the world
                        _engineSettings.BattleMessagesModel.TurnMessageSpecial = $"Miracle Max saved {Target.Name}, restoring them to full health!";
                    }
                    else
                    {
                        // Apply the Damage
                        ApplyDamage(Target);

                        _engineSettings.BattleMessagesModel.TurnMessageSpecial =
                            _engineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                        // Check to see if zombies are enabled, then revive monster
                        if (_engineSettings.BattleSettingsModel.ZombiesEnabled &&
                            Target.PlayerType == PlayerTypeEnum.Monster &&
                            !Target.Alive) 
                        {
                            var result = DiceHelper.RollDice(1, 10);
                            if (result < _globals.ChanceForZombie)
                            {
                                Target.Alive = true;
                                Target.CurrentHealth = (int)(Target.MaxHealth / 2);
                                Target.Name = $"Zombie {Target.Name}";
                            }
                        }

                        // Check if Dead and Remove
                        RemoveIfDead(Target);

                        // If it is a character apply the experience earned
                        CalculateExperience(Attacker, Target);
                    }

                    break;
            }

            _engineSettings.BattleMessagesModel.TurnMessage = Attacker.Name +
                                                              _engineSettings.BattleMessagesModel.AttackStatus +
                                                              Target.Name +
                                                              _engineSettings.BattleMessagesModel.TurnMessageSpecial +
                                                              _engineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(_engineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        ///     See if the Battle Settings will Override the Hit
        ///     Return the Override for the HitStatus
        /// </summary>
        /// <returns></returns>
        public virtual HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            if (Attacker.PlayerType == PlayerTypeEnum.Monster) return BattleSettingsOverrideHitStatusEnum(_engineSettings.BattleSettingsModel.MonsterHitEnum);

            return BattleSettingsOverrideHitStatusEnum(_engineSettings.BattleSettingsModel.CharacterHitEnum);
        }

        /// <summary>
        ///     Return the Override for the HitStatus
        /// </summary>
        /// <param name="myEnum"></param>
        /// <returns></returns>
        public virtual HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            switch (myEnum)
            {
                case HitStatusEnum.Hit:
                    _engineSettings.BattleMessagesModel.AttackStatus = " somehow Hit ";
                    return HitStatusEnum.Hit;

                case HitStatusEnum.CriticalHit:
                    _engineSettings.BattleMessagesModel.AttackStatus = " somehow Critical Hit ";
                    return HitStatusEnum.CriticalHit;

                case HitStatusEnum.Miss:
                    _engineSettings.BattleMessagesModel.AttackStatus = " somehow Missed ";
                    return HitStatusEnum.Miss;

                case HitStatusEnum.CriticalMiss:
                    _engineSettings.BattleMessagesModel.AttackStatus = " somehow Critical Missed ";
                    return HitStatusEnum.CriticalMiss;

                default:
                    // Return what it was
                    return _engineSettings.BattleMessagesModel.HitStatus;
            }
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        /// <param name="Target"></param>
        public virtual int ApplyDamage(PlayerInfoModel Target)
        {
            Target.TakeDamage(_engineSettings.BattleMessagesModel.DamageAmount);
            _engineSettings.BattleMessagesModel.CurrentHealth = Target.GetCurrentHealthTotal;

            return _engineSettings.BattleMessagesModel.DamageAmount;
        }

        /// <summary>
        ///     Calculate the Attack, return if it hit or missed.
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public virtual HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            // Remember Current Player
            _engineSettings.BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            // Choose who to attack
            _engineSettings.BattleMessagesModel.TargetName = Target.Name;
            _engineSettings.BattleMessagesModel.AttackerName = Attacker.Name;

            // Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;

            _engineSettings.BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            return _engineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        ///     Calculate Experience
        ///     Level up if needed
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        public virtual bool CalculateExperience(PlayerInfoModel attacker, PlayerInfoModel target)
        {
            if (attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var points = " points";

                var experienceEarned =
                    target.CalculateExperienceEarned(_engineSettings.BattleMessagesModel.DamageAmount);

                if (experienceEarned == 1) points = " point";

                _engineSettings.BattleMessagesModel.ExperienceEarned = " Earned " + experienceEarned + points;

                var LevelUp = attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    _engineSettings.BattleMessagesModel.LevelUpMessage =
                        attacker.Name + " is now Level " + attacker.Level + " With Health Max of " +
                        attacker.GetMaxHealthTotal;
                    Debug.WriteLine(_engineSettings.BattleMessagesModel.LevelUpMessage);
                }

                // Add Experinece to the Score
                _engineSettings.BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            return true;
        }

        /// <summary>
        ///     If Dead process Target Died
        /// </summary>
        /// <param name="Target"></param>
        public virtual bool RemoveIfDead(PlayerInfoModel Target)
        {
            // Check for alive
            if (Target.Alive == false)
            {
                TargetDied(Target);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Target Died
        ///     Process for death...
        ///     Returns the count of items dropped at death
        /// </summary>
        /// <param name="Target"></param>
        public virtual bool TargetDied(PlayerInfoModel Target)
        {
            // Mark Status in output
            _engineSettings.BattleMessagesModel.TurnMessageSpecial = " and causes death. ";

            // Removing the
            _engineSettings.MapModel.RemovePlayerFromMap(Target);

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the Character to the killed list
                    _engineSettings.BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    _engineSettings.BattleScore.CharacterModelDeathList.Add(Target);

                    DropItems(Target);

                    _engineSettings.CharacterList.Remove(_engineSettings.CharacterList
                                                                        .Find(m => m.Guid.Equals(Target.Guid)));
                    _engineSettings.PlayerList.Remove(_engineSettings.PlayerList
                                                                     .Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                default:
                    // Add one to the monsters killed count...
                    _engineSettings.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    _engineSettings.BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    _engineSettings.BattleScore.MonsterModelDeathList.Add(Target);

                    DropItems(Target);

                    _engineSettings.MonsterList.Remove(_engineSettings.MonsterList
                                                                      .Find(m => m.Guid.Equals(Target.Guid)));
                    _engineSettings.PlayerList.Remove(_engineSettings.PlayerList
                                                                     .Find(m => m.Guid.Equals(Target.Guid)));

                    return true;
            }
        }

        /// <summary>
        ///     Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public virtual int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(_engineSettings.BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                _engineSettings.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            _engineSettings.ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0) DroppedMessage = " Nothing dropped. ";

            _engineSettings.BattleMessagesModel.DroppedMessage = DroppedMessage;

            _engineSettings.BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        ///     Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public virtual HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                _engineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                _engineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to miss ";

                if (_engineSettings.BattleSettingsModel.AllowCriticalMiss)
                {
                    _engineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";
                    _engineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;
                }

                return _engineSettings.BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                _engineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for hit ";
                _engineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

                if (_engineSettings.BattleSettingsModel.AllowCriticalHit)
                {
                    _engineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";
                    _engineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalHit;
                }
                return _engineSettings.BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                _engineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                _engineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                _engineSettings.BattleMessagesModel.DamageAmount = 0;
                return _engineSettings.BattleMessagesModel.HitStatus;
            }

            _engineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            _engineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return _engineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        ///     Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public virtual List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // TODO: Teams, You need to implement your own modification to the Logic cannot use mine as is.

            // You decide how to drop monster items, level, etc.

            // The Number drop can be Up to the Round Count, but may be less.
            // Negative results in nothing dropped
            var NumberToDrop = DiceHelper.RollDice(1, round + 1) - 1;

            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                // Get a random Unique Item
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                result.Add(data);
            }

            return result;
        }

        /// <summary>
        ///     Critical Miss Problem
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public virtual bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return true;
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
