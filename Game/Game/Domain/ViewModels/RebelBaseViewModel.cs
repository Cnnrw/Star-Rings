using System.Windows.Input;

using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    public class RebelBaseViewModel : BaseViewModel
    {
        ICommand _characterCommand;
        ICommand _monsterCommand;
        ICommand _itemsCommand;
        ICommand _scoresCommand;

        public RebelBaseViewModel()
        {
            CharacterPageCommand = new AsyncCommand(() => NavigationService.NavigateAsync(nameof(CharacterIndexPage)));
            MonsterPageCommand = new AsyncCommand(() => NavigationService.NavigateAsync(nameof(MonsterIndexPage)));
            ItemsPageCommand = new AsyncCommand(() => NavigationService.NavigateAsync(nameof(ItemIndexPage)));
            ScoresPageCommand = new AsyncCommand(() => NavigationService.NavigateAsync(nameof(ScoreIndexPage)));
        }

        public ICommand CharacterPageCommand
        {
            get => _characterCommand;
            set => SetProperty(ref _characterCommand, value);
        }

        public ICommand MonsterPageCommand
        {
            get => _monsterCommand;
            set => SetProperty(ref _monsterCommand, value);
        }

        public ICommand ItemsPageCommand
        {
            get => _itemsCommand;
            set => SetProperty(ref _itemsCommand, value);
        }

        public ICommand ScoresPageCommand
        {
            get => _scoresCommand;
            set => SetProperty(ref _scoresCommand, value);
        }
    }
}
