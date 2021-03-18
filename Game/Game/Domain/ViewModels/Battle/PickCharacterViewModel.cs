using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Game.Models;
using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Game.ViewModels
{
    public class PickCharacterViewModel : BattleEngineViewModel
    {
        /// <summary>
        ///     Hold the list of Characters available to be put in a party
        /// </summary>
        public ObservableCollection<CharacterModel> CharacterList { get; set; }

        /// <summary>
        ///     Hold the Proposed List of Characters for the Battle to Use
        /// </summary>
        public ObservableCollection<CharacterModel> SelectedCharacterList { get; set; }

        public AsyncCommand BeginBattleCommand { get; protected set; }
        public Command SelectCharacterCommand { get; protected set; }

        int _partyCount;
        public int PartyCount {
            get => _partyCount;
            set => SetProperty(ref _partyCount, value);
        }

        public PickCharacterViewModel()
        {
            CharacterList = new ObservableCollection<CharacterModel>();
            SelectedCharacterList = new ObservableCollection<CharacterModel>();

            InitializeCharacterList();

            SelectCharacterCommand = new Command<CharacterModel>(AddCharacterToParty);
            BeginBattleCommand = new AsyncCommand(execute: BeginBattle,
                                                  () => SelectedCharacterList.Any());
        }

        async Task BeginBattle()
        {
            CreateEngineCharacterList();
            await App.NavigationService.NavigateModalAsync(nameof(BattlePage));
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="character"></param>
        void AddCharacterToParty(CharacterModel character)
        {
            if (CharacterList.Contains(character))
            {
                // the character is being added to the party
                SelectedCharacterList.Add(character);
                CharacterList.Remove(character);
            }
            else
            {
                // the character is being removed from the party
                SelectedCharacterList.Remove(character);
                CharacterList.Add(character);
            }

            PartyCount = SelectedCharacterList.Count;
            BeginBattleCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///
        /// </summary>
        void CreateEngineCharacterList()
        {
            Engine.EngineSettings.CharacterList.Clear();

            // Load the characters into the engine
            foreach (var character in SelectedCharacterList)
            {
                character.CurrentHealth = character.GetMaxHealthTotal;
                Engine.PopulateCharacterList(character);
            }
        }

        /// <summary>
        ///     Copies characters from the database
        /// </summary>
        void InitializeCharacterList()
        {
            foreach (var character in CharacterIndexViewModel.Instance.Dataset)
                CharacterList.Add(character);
        }
    }
}
