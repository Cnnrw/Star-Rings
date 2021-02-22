using System.Collections.Generic;
using System.Linq;

using Game.Helpers;
using Game.Models;
using Game.Views;

using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class ScoreIndexViewModel : BaseViewModel<ScoreModel>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        ///
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public ScoreIndexViewModel()
        {
            Title = "Scores";

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
        private static volatile ScoreIndexViewModel instance;
        private static readonly object              syncRoot = new object();

        public static ScoreIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ScoreIndexViewModel();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton
        #region DataOperations_CRUDi

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

        /// <summary>
        /// Load the Default Data
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ScoreModel> GetDefaultData() => DefaultData.LoadData(new ScoreModel());

        #endregion DataOperations_CRUDi
    }
}