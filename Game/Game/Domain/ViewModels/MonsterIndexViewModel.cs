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
    ///     Monster Index ViewModel
    ///     Manages list of data records
    /// </summary>
    public class MonsterIndexViewModel : BaseViewModel<MonsterModel>
    {
        public ICommand SelectMonsterCommand { get; } =
            new AsyncCommand<MonsterModel>(model => NavigationService.NavigateModalAsync(nameof(MonsterReadPage), new GenericViewModel<MonsterModel>(model)));

        public ICommand AddMonsterCommand { get; } =
            new AsyncCommand(() => NavigationService.NavigateModalAsync(nameof(MonsterCreatePage)));

        #region Constructor

        public MonsterIndexViewModel() : this(App.NavigationService)
        { }

        public MonsterIndexViewModel(INavigationService navigationService = null)
        {
            Title = "Monsters";
            NavigationService = navigationService;

            // Register the Create Message
            MessagingCenter.Subscribe<MonsterCreatePage, MonsterModel>(this, "Create", async (obj, data) =>
                                                                           await CreateAsync(data));

            // Register the Update Message
            MessagingCenter.Subscribe<MonsterUpdatePage, MonsterModel>(this, "Update", async (obj, data) =>
            {
                data.Update(data);

                await UpdateAsync(data);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<MonsterDeletePage, MonsterModel>(this, "Delete", async (obj, data) =>
                                                                           await DeleteAsync(data));

            // Register the set data source message
            MessagingCenter.Subscribe<SettingsPage, int>(this, "SetDataSource", async (obj, data) =>
                                                             await SetDataSource(data));
            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<SettingsPage, bool>(this, "WipeDataList", async (obj, data) =>
                                                              await WipeDataListAsync());
        }

        #endregion
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
        #region DataOperations_CRUDi

        /// <summary>
        ///     Loads the default data
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<MonsterModel> GetDefaultData() => DefaultData.Monsters;

        /// <summary>
        /// The Sort Order for the MonsterModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<MonsterModel> SortDataset(IEnumerable<MonsterModel> dataset) =>
            dataset
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Description)
                .ToList();

        /// <summary>
        ///     Returns the monster passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override MonsterModel CheckIfExists(MonsterModel data)
        {
            if (data == null)
            {
                return null;
            }

            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item
            var myList = Dataset.FirstOrDefault(a => a.Name == data.Name &&
                                                     a.Description == data.Description);

            return myList;
        }

        #endregion
    }
}
