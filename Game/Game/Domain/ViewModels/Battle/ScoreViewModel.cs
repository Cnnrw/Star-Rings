using System.Collections.ObjectModel;
using System.Windows.Input;

using Game.Models;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    public class ScoreViewModel : BattleEngineViewModel
    {
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Number of monsters killed
        /// </summary>
        int _monstersKilled;
        public int MonstersKilled
        {
            get => _monstersKilled;
            set => SetProperty(ref _monstersKilled, value);
        }

        /// <summary>
        /// Ending score
        /// </summary>
        int _totalScore;
        public int TotalScore
        {
            get => _totalScore;
            set => SetProperty(ref _totalScore, value);
        }

        /// <summary>
        /// Total experience
        /// </summary>
        int _totalExperience;
        public int TotalExperience
        {
            get => _totalExperience;
            set => SetProperty(ref _totalExperience, value);
        }

        public int TotalItemsCollected => Items.Count;

        public int CharactersKilled => DeadCharacters.Count;

        /// <summary>
        /// List of all items dropped
        /// </summary>
        public ObservableCollection<ItemModel> Items { get; }

        /// <summary>
        /// Dead characters
        /// </summary>
        public ObservableCollection<PlayerInfoModel> DeadCharacters { get; }

        /// <summary>
        /// Dead Monsters
        /// </summary>
        public ObservableCollection<PlayerInfoModel> DeadMonsters { get; }

        readonly ScoreModel _battleScore = Instance.Engine.EngineSettings.BattleScore;

        public ScoreViewModel()
        {
            CloseCommand = new AsyncCommand(() => App.NavigationService.GoBack());

            // battle scores
            MonstersKilled = _battleScore.MonsterSlainNumber;
            TotalScore = _battleScore.ScoreTotal;
            TotalExperience = _battleScore.ExperienceGainedTotal;

            Items = new ObservableCollection<ItemModel>(_battleScore.ItemModelDropList);
            DeadCharacters = new ObservableCollection<PlayerInfoModel>(_battleScore.CharacterModelDeathList);
            DeadMonsters = new ObservableCollection<PlayerInfoModel>(_battleScore.MonsterModelDeathList);
        }
    }
}
