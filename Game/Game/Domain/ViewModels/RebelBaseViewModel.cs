using System.Threading.Tasks;
using System.Windows.Input;

using Game.Views;

using Xamarin.Forms;

namespace Game.ViewModels
{
    public class RebelBaseViewModel : BaseViewModel
    {
        INavigation Navigation { get; }

        public RebelBaseViewModel(INavigation navigation)
        {
            Navigation = navigation;

            CharacterPageCommand = new Command(async () => await GoTo_Page(new CharacterIndexPage()));
            MonsterPageCommand = new Command( async () => await GoTo_Page(new MonsterIndexPage()));
            ItemsPageCommand = new Command(async () => await GoTo_Page(new ItemIndexPage()));
            ScoresPageCommand = new Command(async () => await GoTo_Page(new ScoreIndexPage()));
        }

        async Task GoTo_Page(Page page) =>
            await Navigation.PushAsync(page);

        ICommand _characterCommand;
        public ICommand CharacterPageCommand
        {
            get => _characterCommand;
            set => SetProperty(ref _characterCommand, value);
        }

        ICommand _monsterCommand;
        public ICommand MonsterPageCommand
        {
            get => _monsterCommand;
            set => SetProperty(ref _monsterCommand, value);
        }

        ICommand _itemsCommand;
        public ICommand ItemsPageCommand
        {
            get => _itemsCommand;
            set => SetProperty(ref _itemsCommand, value);
        }

        ICommand _scoresCommand;
        public ICommand ScoresPageCommand
        {
            get => _scoresCommand;
            set => SetProperty(ref _scoresCommand, value);
        }
    }
}
