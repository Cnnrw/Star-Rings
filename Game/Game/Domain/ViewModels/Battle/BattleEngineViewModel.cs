using System.Collections.Generic;
using System.Windows.Input;

using Game.Engine.EngineInterfaces;
using Game.Models;
using Game.Services;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class BattleEngineViewModel : ObservableObject
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile BattleEngineViewModel _instance;
        private static readonly object                SyncRoot = new object();

        public static BattleEngineViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new BattleEngineViewModel();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion Singleton

        #region BattleEngineSelection

        // The Battle Engine
        public IBattleEngineInterface EngineKoenig = new Engine.EngineKoenig.BattleEngine();

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngineKoenig = new Engine.EngineKoenig.AutoBattleEngine();

        // The Battle Engine
        public IBattleEngineInterface EngineGame = new Engine.EngineGame.BattleEngine();

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngineGame = new Engine.EngineGame.AutoBattleEngine();

        // Set the Battle Engine
        public IBattleEngineInterface Engine;

        // Auto Battle Engine (used for scneario testing)
        public IAutoBattleInterface AutoBattleEngine;

        #endregion BattleEngineSelection

        #region Constructor

        public static INavigationService NavigationService { get; protected set; }

        public ICommand ClosePageCommand { get; } =
            new AsyncCommand(() => NavigationService.GoBack());

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel(INavigationService navigationService = null)
        {
            SetBattleEngineToGame();

            NavigationService = navigationService ?? App.NavigationService;
        }

        /// <summary>
        /// Set the Battle Engine to the Game Version for actual use
        /// </summary>
        /// <returns></returns>
        public bool SetBattleEngineToGame()
        {
            Engine = EngineGame;
            AutoBattleEngine = AutoBattleEngineGame;
            return true;
        }

        /// <summary>
        /// Set the Battle Engine to the Koenig Version for Testing
        /// </summary>
        /// <returns></returns>
        public bool SetBattleEngineToKoenig()
        {
            Engine = EngineKoenig;
            AutoBattleEngine = AutoBattleEngineKoenig;
            return true;
        }

        #endregion Constructor

        // todo move into RoundPage view model
        public IEnumerable<ItemModel> ItemsDroppedList => Instance.Engine.EngineSettings.BattleScore.ItemModelDropList;
    }
}
