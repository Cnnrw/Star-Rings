using System;
using System.Linq;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Selecting Characters for the Game
    ///
    /// TODO: Team
    /// Mike's game allows duplicate characters in a party (6 Mikes can all fight)
    /// If you do not allow duplicates, change the code below
    /// Instead of using the database list directly make a copy of it in the viewmodel
    /// Then have on select of the database remove the character from that list and add to the part list
    /// Have select from the party list remove it from the party list and add it to the database list
    ///
    /// </summary>
    public partial class PickCharactersPage : BaseContentPage
    {
        readonly BattleEngineViewModel _viewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Empty Constructor for UTs
        /// </summary>
        /// <param name="unitTest"></param>
        internal PickCharactersPage(bool unitTest) { }

        /// <summary>
        /// Constructor for Index Page
        ///
        /// Get the CharacterIndexView Model
        /// </summary>
        public PickCharactersPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;

            PopulateCharacterPool();

            _viewModel.PartyCharacterList.Clear();

            UpdateNextButtonState();
        }

        /// <summary>
        /// Copies Characters from the database into a modifiable Character pool list
        /// </summary>
        private void PopulateCharacterPool()
        {
            _viewModel.PoolCharacterList.Clear();

            foreach (var character in _viewModel.DatabaseCharacterList)
                _viewModel.PoolCharacterList.Add(character);
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnCharacterSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.FirstOrDefault() is CharacterModel data))
                return;

            // Manually deselect Character.
            CharacterList.SelectedItem = null;

            // Don't add more than the party max
            if (_viewModel.PartyCharacterList.Count() < _viewModel.Engine.EngineSettings.MaxNumberPartyCharacters)
            {
                // Remove the character from the pool list and add it to the party list
                _viewModel.PoolCharacterList.Remove(data);
                _viewModel.PartyCharacterList.Add(data);
            }

            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnPartyCharacterSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.FirstOrDefault() is CharacterModel data))
                return;

            // Manually deselect Character.
            PartyList.SelectedItem = null;

            // Remove the character from the party list and add it back to the pool list
            _viewModel.PartyCharacterList.Remove(data);
            _viewModel.PoolCharacterList.Add(data);

            UpdateNextButtonState();
        }

        /// <summary>
        /// Next Button is based on the count
        ///
        /// If no selected characters, disable
        ///
        /// Show the Count of the party
        /// </summary>
        public void UpdateNextButtonState()
        {
            BeginBattleButton.IsEnabled = _viewModel.PartyCharacterList != null &&
                                          _viewModel.PartyCharacterList.Any();

            PartyCountLabel.Text = (_viewModel.PartyCharacterList ??
                                    Enumerable.Empty<CharacterModel>()).Count()
                                                                       .ToString();
        }

        /// <summary>
        /// Jump to the Battle
        ///
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleButton_Clicked(object sender, EventArgs e)
        {
            CreateEngineCharacterList();

            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Clear out the old list and make the new list
        /// </summary>
        internal void CreateEngineCharacterList()
        {
            // Clear the current list
            _viewModel.Engine.EngineSettings.CharacterList.Clear();

            // Load the Characters into the Engine
            foreach (var data in _viewModel.PartyCharacterList)
            {
                data.CurrentHealth = data.GetMaxHealthTotal;
                _viewModel.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            }
        }
    }
}
