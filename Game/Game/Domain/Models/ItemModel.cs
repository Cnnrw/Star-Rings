using Game.Enums;
using Game.Helpers;
using Game.Services;

namespace Game.Models
{
    /// <summary>
    /// Item for the Game
    ///
    /// The Items that a character can use, a Monster may drop, or may be randomly available.
    /// The items are stored in the DB, and during game time a random item is selected.
    /// The system supports CRUDi operations on the items
    /// When in test mode, a test set of items is loaded
    /// When in run mode the items from from the database
    /// When in online mode, the items come from an api call to a webservice
    ///
    /// When characters or monsters die, they drop items into the Items Pool for the Battle
    ///
    /// </summary>
    public class ItemModel : BaseModel<ItemModel>
    {
        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public ItemModel() => ImageURI = ItemService.DefaultImageURI;

        /// <summary>
        /// Constructor to create an item based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public ItemModel(ItemModel data) => Update(data);

        public int Range { get; set; }         // Range of the item, swords are 1, hats/rings are 0, bows are >1
        public int Damage { get; set; }        // Damage the Item can do if it is in the primary hand
        public int Value { get; set; }         // The Value item modifies. i.e. ring of Health +3 -> Value = 3
        public int Count { get; set; } = 1;    // Count of how many
        public bool IsConsumable { get; set; } // Tracks if the item is a consumable or not

        // Enum of the different attributes that the item modifies, Items can only modify one item
        public AttributeEnum Attribute { get; set; } = AttributeEnum.Attack;

        public ItemCategories Category { get; set; } // The Category of the item

        // Where the Item goes on the character.  Head, Foot etc.
        public ItemLocationEnum Location { get; set; } = ItemLocationEnum.PrimaryHand;

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public sealed override bool Update(ItemModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Description = newData.Description;
            Value = newData.Value;
            Attribute = newData.Attribute;
            Location = newData.Location;
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            Range = newData.Range;
            Damage = newData.Damage;

            IsConsumable = newData.IsConsumable;
            Category = newData.Category;

            return true;
        }

        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                           Description + " for " +
                           Location + " with " +
                           Attribute + "+" +
                           Value + " , " +
                           "Damage : " + Damage + " , " +
                           "Range : " + Range;

            return myReturn.Trim();
        }

        /// <summary>
        /// Updates the Item to be closer to what the Level would want
        /// </summary>
        /// <param name="level"></param>
        public int ScaleLevel(int level)
        {
            if (DiceHelper.ForceRollsToNotRandom)
            {
                // Use the level as the value
                Value = level;
            }

            // Roll a dice of up to the Level
            Value = DiceHelper.RollDice(1, level);

            return Value;
        }
    }
}