namespace Game.Models.Enums
{
    // What data store will be used.
    public enum DataSourceEnum
    {
        // Unknown is not a valid store
        Unknown = 0,

        // Use SQLite and save to disk
        SQL = 1,

        // In Memory Store
        Mock = 2
    }
}