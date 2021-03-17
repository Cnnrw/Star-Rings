using System.Windows.Input;

using Game.Services;
using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    public class RebelBaseViewModel : BaseViewModel
    {
        public ICommand CharacterPageCommand => new AsyncCommand(() => NavigationService.NavigateAsync(nameof(CharacterIndexPage)));
        public ICommand MonsterPageCommand => new AsyncCommand(() => NavigationService.NavigateAsync(nameof(MonsterIndexPage)));
        public ICommand ItemsPageCommand => new AsyncCommand(() => NavigationService.NavigateAsync(nameof(ItemIndexPage)));
        public ICommand ScoresPageCommand => new AsyncCommand(() => NavigationService.NavigateAsync(nameof(ScoreIndexPage)));

        public RebelBaseViewModel() : this(App.NavigationService)
        { }

        internal RebelBaseViewModel(INavigationService navigationService) : base(navigationService)
        { }

    }
}
