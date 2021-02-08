using System.Collections.Generic;
using System.Linq;

using Game.GameRules;
using Game.Models;
using Game.Views;

using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    ///     Monster Index ViewModel
    ///     Manages list of data records
    /// </summary>
    public class MonsterIndexViewModel : BaseViewModel<CharacterModel>
    {
        #region Singleton

        // Must be singleton, holds all data records in memory
        private static volatile MonsterIndexViewModel instance;
        private static readonly object                syncRoot = new object();

        public static MonsterIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MonsterIndexViewModel();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        public MonsterIndexViewModel()
        {
            Title = "Monsters";

            // // Register the Create Message
            // MessagingCenter.Subscribe<MonsterCreatePage, MonsterModel>(this, "Create", async (obj, data) =>
            // {
            //     await CreateAsync(data as MonsterModel);
            // });
            //
            // // Register the Delete Message
            // MessagingCenter.Subscribe<MonsterDeletePage, MonsterModel>(this, "Delete", async (obj, data) =>
            // {
            //     await DeleteAsync(data as MonsterModel);
            // });

            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<AboutPage, bool>(this, "WipeDataList", async (obj, data) =>
            {
                await WipeDataListAsync();
            });
        }

        /// <summary>
        ///     Loads the default data
        /// </summary>
        /// <returns></returns>
        public override List<CharacterModel> GetDefaultData() =>
            DefaultData.LoadData(new CharacterModel());

        /// <summary>
        ///     The Sort Order for the CharacterModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<CharacterModel> SortDataset(List<CharacterModel> dataset) =>
            dataset.OrderBy(a => a.Name)
                   .ThenBy(a => a.Description)
                   .ToList();

        public override CharacterModel CheckIfExists(CharacterModel data)
        {
            if (data == null)
            {
                return null;
            }

            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item
            var myList = Dataset.FirstOrDefault(a =>
                                                    a.Name == data.Name &&
                                                    a.Description == data.Description);

            return myList;
        }


    }
}