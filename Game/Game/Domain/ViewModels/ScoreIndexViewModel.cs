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
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class ScoreIndexViewModel : BaseViewModel<ScoreModel>
    {
        public ICommand SelectScoreCommand { get; } =
            new AsyncCommand<ScoreModel>(model => NavigationService.NavigateModalAsync(nameof(ScoreReadPage), new GenericViewModel<ScoreModel>(model)));

        public ICommand AddScoreCommand { get; } =
            new AsyncCommand(() => NavigationService.NavigateModalAsync(nameof(ScoreCreatePage)));

        #region Constructor

        public ScoreIndexViewModel() : this(App.NavigationService)
        { }

        /// <summary>
        /// Constructor
        ///
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        ScoreIndexViewModel(INavigationService navigationService = null)
        {
            Title = "Scores";
            NavigationService = navigationService;

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<ScoreCreatePage, ScoreModel>(this, "Create", async (obj, data) =>
            {
                await CreateAsync(data);
            });

            // Register the Update Message
            MessagingCenter.Subscribe<ScoreUpdatePage, ScoreModel>(this, "Update", async (obj, data) =>
            {
                // Have the Score update itself
                data.Update(data);

                await UpdateAsync(data);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<ScoreDeletePage, ScoreModel>(this, "Delete", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            // Register the Set Data Source Message
            MessagingCenter.Subscribe<SettingsPage, int>(this, "SetDataSource", async (obj, data) =>
            {
                await SetDataSource(data);
            });

            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<SettingsPage, bool>(this, "WipeDataList", async (obj, data) =>
            {
                await WipeDataListAsync();
            });

            #endregion Messages
        }

        #endregion Constructor
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile ScoreIndexViewModel _instance;
        private static readonly object              SyncRoot = new object();

        public static ScoreIndexViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new ScoreIndexViewModel();
                            _instance.Initialize();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion Singleton
        #region DataOperations_CRUDi

        /// <summary>
        ///     Loads the default data
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ScoreModel> GetDefaultData() => DefaultData.Scores;

        /// <summary>
        /// The Sort Order for the ScoreModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<ScoreModel> SortDataset(IEnumerable<ScoreModel> dataset) =>
            dataset
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Description)
                .ToList();

        /// <summary>
        /// Returns the Score passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ScoreModel CheckIfScoreExists(ScoreModel data)
        {
            // This will walk the Scores and find if there is one that is the same.
            // If so, it returns the Score...

            var myList = Dataset.FirstOrDefault(a => a.Name == data.Name);

            return myList;
        }

        #endregion DataOperations_CRUDi
    }
}
