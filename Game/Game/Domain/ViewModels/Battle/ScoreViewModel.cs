using System.Collections.ObjectModel;

using Game.Models;

namespace Game.ViewModels
{
    public class ScoreViewModel : BattleEngineViewModel
    {
        static readonly ScoreModel BattleScore = Instance.Engine.EngineSettings.BattleScore;

        /// <summary>
        /// Ending score
        /// </summary>
        public int TotalScore { get; } = BattleScore.ScoreTotal;

        /// <summary>
        /// Total experience
        /// </summary>
        public int TotalExperience { get; } = BattleScore.ExperienceGainedTotal;

        /// <summary>
        /// List of all items dropped
        /// </summary>
        public ObservableCollection<ItemModel> Items { get; } =
            new ObservableCollection<ItemModel>(BattleScore.ItemModelDropList);

        /// <summary>
        /// Dead characters
        /// </summary>
        public ObservableCollection<PlayerInfoModel> DeadCharacters { get; } =
            new ObservableCollection<PlayerInfoModel>(BattleScore.CharacterModelDeathList);

        /// <summary>
        /// Dead Monsters
        /// </summary>
        public ObservableCollection<PlayerInfoModel> DeadMonsters { get; } =
            new ObservableCollection<PlayerInfoModel>(BattleScore.MonsterModelDeathList);
    }
}
