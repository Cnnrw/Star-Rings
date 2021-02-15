using Game.Models.Enums;

namespace Game.Models
{
    /// <summary>
    ///     Navigation Menu Item Model
    /// </summary>
    public class MenuItemModel
    {
        // The Current Menu ID
        public MenuItemEnum Id { get; set; }

        // Title for the Page
        public string Title { get; set; }
    }
}