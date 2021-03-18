using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using Game.Models;
using Game.Services;
using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    ///     Character Index View Model
    ///     Manages the list of character data records
    /// </summary>
    public class CharacterIndexViewModel : BaseViewModel<CharacterModel>
    {
        public ICommand SelectCharacterCommand { get; } =
            new AsyncCommand<CharacterModel>(model => NavigationService.NavigateAsync(nameof(CharacterReadPage), new GenericViewModel<CharacterModel>(model)));

        public ICommand AddCharacterCommand { get; } =
            new AsyncCommand(() => NavigationService.NavigateModalAsync(nameof(CharacterCreatePage)));

        #region Singleton

        // Make this a singleton so it only exist one time because it holds all
        // the character data records in memory
        private static volatile CharacterIndexViewModel _instance;
        private static readonly object                  SyncRoot = new object();

        public static CharacterIndexViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new CharacterIndexViewModel();
                            _instance.Initialize();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion Singleton
        #region Constructor

        public CharacterIndexViewModel() : this(App.NavigationService)
        { }

        /// <summary>
        ///     Constructor
        ///     The constructor subscribes message listeners for crudi operations
        /// </summary>
        internal CharacterIndexViewModel(INavigationService navigationService = null)
        {
            Title = "Characters";
            NavigationService = navigationService;

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<CharacterCreatePage, CharacterModel>(this, "Create", async (obj, data) =>
                                                                               await CreateAsync(data));

            // Register the Update Message
            MessagingCenter.Subscribe<CharacterUpdatePage, CharacterModel>(this, "Update", async (obj, data) =>
            {
                // Have the character update itself
                data.Update(data);
                await UpdateAsync(data);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<CharacterDeletePage, CharacterModel>(this, "Delete", async (obj, data) =>
                                                                               await DeleteAsync(data));

            // Register the set data source message
            MessagingCenter.Subscribe<SettingsPage, int>(this, "SetDataSource", async (obj, data) =>
                                                             await SetDataSource(data));

            // Register the wipe data list message
            MessagingCenter.Subscribe<SettingsPage, bool>(this, "WipeDataList", async (obj, data) =>
                                                              await WipeDataListAsync());

            #endregion Messages
        }

        #endregion Constructor
        #region DataOperations_CRUDi

        /// <summary>
        ///     Load the Default Data
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<CharacterModel> GetDefaultData() => DefaultData.Characters;

        /// <summary>
        ///     The Sort Order for the CharacterModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<CharacterModel> SortDataset(IEnumerable<CharacterModel> dataset) =>
            dataset.OrderBy(a => a.Name)
                   .ThenBy(a => a.Description)
                   .ToList();

        /// <summary>
        ///     Returns the character passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override CharacterModel CheckIfExists(CharacterModel data)
        {
            if (data == null)
                return null;

            // This will walk the Scores and find if there is one that is the same.
            // If so, it returns the Score...
            var myList = Dataset.FirstOrDefault(a => a.Name == data.Name &&
                                                     a.Description == data.Description);

            return myList;
        }

        #endregion DataOperations_CRUDi
    }
}
