using System.Windows.Input;

using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        ICommand _rebelBaseCommand;
        ICommand _dungeonCommand;
        ICommand _autoBattleCommand;
        ICommand _settingsCommand;
        ICommand _aboutCommand;

        public MainViewModel()
        {
            RebelBaseCommand = new AsyncCommand(() => NavigationService.NavigateAsync(nameof(RebelBasePage)));
            DungeonCommand = new AsyncCommand(() => NavigationService.NavigateAsync("DungeonPage"));
            AutoBattleCommand = new AsyncCommand(() => NavigationService.NavigateAsync(nameof(AutoBattlePage)));
            SettingsCommand = new AsyncCommand(() => NavigationService.NavigateAsync(nameof(SettingsPage)));
            AboutCommand = new AsyncCommand(() => NavigationService.NavigateModalAsync(nameof(AboutPage)));
        }

        public ICommand RebelBaseCommand
        {
            get => _rebelBaseCommand;
            set => SetProperty(ref _rebelBaseCommand, value);
        }

        public ICommand DungeonCommand
        {
            get => _dungeonCommand;
            set => SetProperty(ref _dungeonCommand, value);
        }

        public ICommand AutoBattleCommand
        {
            get => _autoBattleCommand;
            set => SetProperty(ref _autoBattleCommand, value);
        }

        public ICommand SettingsCommand
        {
            get => _settingsCommand;
            set => SetProperty(ref _settingsCommand, value);
        }

        public ICommand AboutCommand
        {
            get => _aboutCommand;
            set => SetProperty(ref _aboutCommand, value);
        }
    }
}
