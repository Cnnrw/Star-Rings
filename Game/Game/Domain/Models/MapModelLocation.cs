namespace Game.Models
{
    /// <summary>
    /// Represent the Map
    /// 
    /// The Cordinates
    /// What is at that location
    /// 
    /// </summary>
    public class MapModelLocation
    {

        // They Y, which is Column in Grid
        public int Column;

        // If IsSelected, used for targeting
        public bool IsSelectedTarget = false;

        // The Player, Character or Unknown for blank
        public PlayerInfoModel Player = new PlayerInfoModel();
        // The X, which is R in Grid
        public int Row;
    }
}