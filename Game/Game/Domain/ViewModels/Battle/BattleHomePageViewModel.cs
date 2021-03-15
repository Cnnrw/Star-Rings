using System.Windows.Input;

using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    public class BattleHomePageViewModel : BaseViewModel
    {
        public ICommand PickCharactersCommand =>
            new AsyncCommand(() => NavigationService.NavigateAsync(nameof(PickCharactersPage)));

        public ICommand ScoresCommand =>
            new AsyncCommand(() => NavigationService.NavigateAsync(nameof(ScorePage)));

        public BattleHomePageViewModel() =>
            ConfigureBattlePages();

        void ConfigureBattlePages()
        {
            NavigationService.Configure(nameof(PickCharactersPage), typeof(PickCharactersPage));
            NavigationService.Configure(nameof(BattlePage), typeof(BattlePage));
            NavigationService.Configure(nameof(BattleSettingsPage), typeof(BattleSettingsPage));

            NavigationService.Configure(nameof(NewRoundPage), typeof(NewRoundPage));
            NavigationService.Configure(nameof(RoundOverPage), typeof(RoundOverPage));
            NavigationService.Configure(nameof(ScorePage), typeof(ScorePage));
        }
    }
}
