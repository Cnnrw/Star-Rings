namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum CharacterJobEnum
    {
        // Not specified
        Unknown = 0,

        // Luke is ...
        Luke = 1,

        // Leia is ...
        Leia = 2,

        // Han is ...
        Han = 3,

        // Chewbacca is ...
        Chewbacca = 4,

        // C3PO is ...
        C3PO = 5,

        // R2D2 is ...
        R2D2 = 6,

        // Fighters hit hard and have fight abilities
        Fighter = 10,

        // Clerics defend well and have buff abilities
        Cleric = 12,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CharacterJobEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterJobEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case CharacterJobEnum.Luke:
                    Message = "Luke Skywalker";
                    break;

                case CharacterJobEnum.Leia:
                    Message = "Leia Organa";
                    break;

                case CharacterJobEnum.Han:
                    Message = "Han Solo";
                    break;

                case CharacterJobEnum.Chewbacca:
                    Message = "Chewbacca";
                    break;

                case CharacterJobEnum.C3PO:
                    Message = "C3PO";
                    break;

                case CharacterJobEnum.R2D2:
                    Message = "R2D2";
                    break;

                case CharacterJobEnum.Fighter:
                    Message = "Fighter";
                    break;

                case CharacterJobEnum.Cleric:
                    Message = "Cleric";
                    break;

                case CharacterJobEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }
}