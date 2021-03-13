namespace Game.Enums
{
    /// <summary>
    /// The Conditions a round can have
    /// </summary>
    public enum BattleStateEnum
    {
        // Invalid State
        Unknown = 0,

        // Just Loaded, nothing has happened
        Starting = 1,

        // Battle Underway
        Battling = 2,

        // Round Over
        RoundOver = 3,

        // NewRound
        NewRound = 4,

        // Game Over
        GameOver = 5,

        // Choosing a target Monster
        ChoosingMonsterTarget = 6,

        StartingMonsterTurn = 7,

        EndingMonsterTurn = 8
    }
}
