using SQLite;

namespace Game.Models
{
    /// <summary>
    /// Default Model
    /// This model is empty
    /// It servers as the base for all models
    /// The ViewModels that do not go to the DB use this
    /// </summary>
    public class DefaultModel
    {
        // The ID for the item
        [PrimaryKey]
        public string Id { get; set; } = System.Guid
                                               .NewGuid()
                                               .ToString();

        // Guid, passed from the server
        public string Guid { get; set; } = "";

        // The Name of the Item
        public string Name { get; set; }

        // The Description of the Item
        public string Description { get; set; }
    }
}