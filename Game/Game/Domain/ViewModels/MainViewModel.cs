using System.Threading.Tasks;
using System.Windows.Input;

using Game.Services;

using Xamarin.Forms;

namespace Game.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly INavigationService _navigation;

        public MainViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            _navigation.Configure("RebelBasePage", typeof(Views.RebelBasePage));
            _navigation.Configure("DungeonPage", typeof(Views.BattleHomePage));
            _navigation.Configure("AutoBattlePage", typeof(Views.AutoBattlePage));
            _navigation.Configure("SettingsPage", typeof(Views.SettingsPage));
            _navigation.Configure("AboutPage", typeof(Views.AboutPage));

            RebelBaseCommand = new Command( async () => await GoTo_Page("RebelBasePage"));
            DungeonCommand = new Command(async () => await GoTo_Page("DungeonPage"));
            AutoBattleCommand = new Command(async () => await GoTo_Page("AutoBattlePage"));
            SettingsCommand = new Command(async () => await GoTo_Page("SettingsPage"));
            AboutCommand = new Command(async () => await _navigation.NavigateModalAsync("AboutPage", false));
        }

        async Task GoTo_Page(string page) =>
            await _navigation.NavigateAsync(page);

        ICommand _rebelBaseCommand;
        public ICommand RebelBaseCommand
        {
            get => _rebelBaseCommand;
            set => SetProperty(ref _rebelBaseCommand, value);
        }

        ICommand _dungeonCommand;
        public ICommand DungeonCommand
        {
            get => _dungeonCommand;
            set => SetProperty(ref _dungeonCommand, value);
        }

        ICommand _autoBattleCommand;
        public ICommand AutoBattleCommand
        {
            get => _autoBattleCommand;
            set => SetProperty(ref _autoBattleCommand, value);
        }

        ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get => _settingsCommand;
            set => SetProperty(ref _settingsCommand, value);
        }

        ICommand _aboutCommand;
        public ICommand AboutCommand
        {
            get => _aboutCommand;
            set => SetProperty(ref _aboutCommand, value);
        }
    }
}
