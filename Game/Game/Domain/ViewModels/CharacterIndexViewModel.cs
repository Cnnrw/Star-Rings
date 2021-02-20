using System.Collections.Generic;
using System.Linq;

using Game.Helpers;
using Game.Models;
using Game.Views;

using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    ///     Character Index View Model
    ///     Manages the list of character data records
    /// </summary>
    public class CharacterIndexViewModel : BaseViewModel<CharacterModel>
    {

        #region Constructor

        /// <summary>
        ///     Constructor
        ///     The constructor subscribes message listeners for crudi operations
        /// </summary>
        public CharacterIndexViewModel()
        {
            Title = "Characters";

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
            MessagingCenter.Subscribe<AboutPage, int>(this, "SetDataSource", async (obj, data) =>
                                                          await SetDataSource(data));

            // Register the wipe data list message
            MessagingCenter.Subscribe<AboutPage, bool>(this, "WipeDataList", async (obj, data) =>
                                                           await WipeDataListAsync());

            #endregion Messages
        }

        #endregion Constructor

        #region DataOperations_CRUDi

        /// <summary>
        ///     Load the Default Data
        /// </summary>
        /// <returns></returns>
        public override List<CharacterModel> GetDefaultData() => DefaultData.LoadData(new CharacterModel());

        #endregion DataOperations_CRUDi

        #region SortDataSet

        /// <summary>
        ///     The Sort Order for the ScoreModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<CharacterModel> SortDataset(IEnumerable<CharacterModel> dataset) =>
            dataset.OrderBy(a => a.Name)
                   .ThenBy(a => a.Description)
                   .ToList();

        #endregion SortDataSet

        /// <summary>
        ///     Returns the character passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override CharacterModel CheckIfExists(CharacterModel data)
        {
            if (data == null)
            {
                // it's not a match, return false;
                return null;
            }

            // This will walk the Scores and find if there is one that is the same.
            // If so, it returns the Score...
            var myList = Dataset.FirstOrDefault(a => a.Name == data.Name &&
                                                     a.Description == data.Description);

            return myList;
        }

        #region Singleton

        // Make this a singleton so it only exist one time because it holds all
        // the character data records in memory
        private static volatile CharacterIndexViewModel instance;
        private static readonly object                  syncRoot = new object();

        public static CharacterIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CharacterIndexViewModel();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton
    }
}