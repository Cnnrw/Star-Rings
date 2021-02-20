using System;
using System.Collections.ObjectModel;

using Game.Engine.EngineInterfaces;
using Game.Models;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class BattleEngineViewModel
    {

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngine = new Engine.EngineKoenig.AutoBattleEngine();

        // The Battle Engine
        public IBattleEngineInterface Engine = new Engine.EngineKoenig.BattleEngine();

        //// Hold the View Model to the CharacterIndexViewModel
        //public CharacterIndexViewModel DatabaseCharacterViewModel = CharacterIndexViewModel.Instance;

        //// Have the Database Character List point to the Character View Model List
        //public ObservableCollection<CharacterModel> DatabaseCharacterList { get; set; } = CharacterIndexViewModel.Instance.Dataset;


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel()
        {
        }

        #endregion Constructor

        // Hold the Proposed List of Characters for the Battle to Use
        public ObservableCollection<CharacterModel> PartyCharacterList { get; set; } =
            new ObservableCollection<CharacterModel>();
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile BattleEngineViewModel instance;
        private static readonly object                syncRoot = new Object();

        public static BattleEngineViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new BattleEngineViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton
    }
}