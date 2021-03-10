using Game.Enums;

namespace Game.Models
{
    /// <summary>
    /// Example of Battle Settings Control
    /// </summary>
    public class BattleSettingsModel
    {

        // Are Critical Hits Allowed?
        public bool AllowCriticalHit = false;

        // Are Critical Misses Allowed?
        public bool AllowCriticalMiss = false;

        // Can monsters have Items and weapons?
        public bool AllowMonsterItems = false;
        // The Battle Model (Simple, Map, etc.)
        public BattleModeEnum BattleModeEnum = BattleModeEnum.SimpleNext;

        // Characters always Hit or Miss or Default
        public HitStatusEnum CharacterHitEnum = HitStatusEnum.Default;

        // Monster always Hit or Miss or Default
        public HitStatusEnum MonsterHitEnum = HitStatusEnum.Default;
    }
}
