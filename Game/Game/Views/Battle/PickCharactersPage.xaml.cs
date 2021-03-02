using System;
using System.Linq;

using Xamarin.Forms;

using Game.Models;
using Game.ViewModels;
using Game.Templates.Pages;

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
    public partial class PickCharactersPage : ModalPage
    {
        internal readonly BattleEngineViewModel ViewModel = BattleEngineViewModel.Instance;

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

            BindingContext = ViewModel;

            // Clear the Database List and the Party List to start
            ViewModel.PartyCharacterList.Clear();

            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void OnCharacterSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.FirstOrDefault() is CharacterModel data))
                return;

            // Manually deselect Character.
            CharacterList.SelectedItem = null;

            // Don't add more than the party max
            if (ViewModel.PartyCharacterList.Count() < ViewModel.Engine.EngineSettings.MaxNumberPartyCharacters)
                ViewModel.PartyCharacterList.Add(data);

            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void OnPartyCharacterSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.FirstOrDefault() is CharacterModel data))
                return;

            // Manually deselect Character.
            PartyList.SelectedItem = null;

            // Remove the character from the list
            ViewModel.PartyCharacterList.Remove(data);

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
            BeginBattleButton.IsEnabled = (ViewModel.PartyCharacterList != null) && (ViewModel.PartyCharacterList.Any());
            PartyCountLabel.Text = (ViewModel.PartyCharacterList ?? Enumerable.Empty<CharacterModel>()).Count().ToString();
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
            ViewModel.Engine.EngineSettings.CharacterList.Clear();

            // Load the Characters into the Engine
            foreach (var data in ViewModel.PartyCharacterList)
            {
                data.CurrentHealth = data.GetMaxHealthTotal;
                ViewModel.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            }
        }
    }
}
