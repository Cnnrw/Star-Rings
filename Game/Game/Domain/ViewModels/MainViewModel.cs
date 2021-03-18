using System.Windows.Input;

using Game.Services;
using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel() : this(App.NavigationService)
        { }

        internal MainViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public ICommand RebelBaseCommand =>
            new AsyncCommand(() => NavigationService.NavigateAsync(nameof(RebelBasePage)));

        public ICommand BattleCommand =>
            new AsyncCommand(() => NavigationService.NavigateAsync("Battle"));

        public ICommand AutoBattleCommand =>
            new AsyncCommand(() => NavigationService.NavigateAsync(nameof(AutoBattlePage)));

        public ICommand SettingsCommand =>
            new AsyncCommand(() => NavigationService.NavigateModalAsync(nameof(SettingsPage)));

        public ICommand AboutCommand =>
            new AsyncCommand(() => NavigationService.NavigateModalAsync(nameof(AboutPage)));
    }
}
