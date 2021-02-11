using System.Collections.Generic;

using Game.GameRules;

namespace Game.Models
{
    /// <summary>
    /// Jedi Characters
    /// 
    /// Derive from CharacterModel
    /// </summary>
    public class JediCharacterModel : CharacterModel
    {
        /// <summary>
        /// Default Jedi character
        /// 
        /// Gets a type, guid, name and description
        /// </summary>
        public JediCharacterModel()
        {
            PlayerType = PlayerTypeEnum.Character;
            Guid = Id;
            Name = "Luke Skywalker";
            Description = "A Jedi Master and leader in the Rebel Alliance";
            Level = 1;
            ImageURI = "item.png";
            ExperienceTotal = 0;
            ExperienceRemaining = LevelTableHelper.LevelDetailsList[Level + 1].Experience - 1;

            // Default to Jedi job
            Job = CharacterJobEnum.Jedi;
        }

        /// <summary>
        /// Create a copy
        /// </summary>
        /// <param name="data"></param>
        public JediCharacterModel(CharacterModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public bool Update(JediCharacterModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            PlayerType = newData.PlayerType;
            Guid = newData.Guid;
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ImageURI = newData.ImageURI;

            // Difficulty = newData.Difficulty;

            Speed = newData.Speed;
            Defense = newData.Defense;
            Attack = newData.Attack;

            ExperienceTotal = newData.ExperienceTotal;
            ExperienceRemaining = newData.ExperienceRemaining;
            CurrentHealth = newData.CurrentHealth;
            MaxHealth = newData.MaxHealth;

            Head = newData.Head;
            Necklass = newData.Necklass;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
            UniqueItem = newData.UniqueItem;

            // Update the Job
            Job = newData.Job;

            return true;
        }
    }
}