using System.Collections.Generic;
using System.Linq;

using Game.Enums;
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
    public class ItemIndexViewModel : BaseViewModel<ItemModel>
    {

        #region Constructor

        /// <summary>
        /// Constructor
        ///
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public ItemIndexViewModel()
        {
            Title = "Items";

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<ItemCreatePage, ItemModel>(this, "Create", async (obj, data) =>
                                                                     await CreateAsync(data));

            // Register the Update Message
            MessagingCenter.Subscribe<ItemUpdatePage, ItemModel>(this, "Update", async (obj, data) =>
            {
                data.Update(data); // Have the item update itself
                await UpdateAsync(data);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<ItemDeletePage, ItemModel>(this, "Delete", async (obj, data) =>
                                                                     await DeleteAsync(data));

            // Register the Set Data Source Message
            MessagingCenter.Subscribe<SettingsPage, int>(this, "SetDataSource", async (obj, data) =>
                                                             await SetDataSource(data));

            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<SettingsPage, bool>(this, "WipeDataList", async (obj, data) =>
                                                              await WipeDataListAsync());

            #endregion Messages
        }

        #endregion Constructor

        #region SortDataSet

        /// <summary>
        /// The Sort Order for the ItemModel
        /// </summary>
        /// <returns></returns>
        public override List<ItemModel> SortDataset(IEnumerable<ItemModel> dataset) =>
            dataset.OrderBy(a => a.Name)
                   .ThenBy(a => a.Description)
                   .ToList();

        #endregion SortDataSet

        /// <summary>
        /// Takes an item string ID and looks it up and returns the item
        /// This is because the Items on a character are stores as strings of the GUID.  That way it can be saved to the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ItemModel GetItem(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            // Item myData = DataStore.GetAsync_Item(ItemID).GetAwaiter().GetResult();
            ItemModel myData = Dataset.FirstOrDefault(a => a.Id.Equals(id));
            return myData;
        }

        /// <summary>
        /// Get the ID of the Default Item for the Location
        /// The Default item is the first Item in the List
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public string GetDefaultItemId(ItemLocationEnum location)
        {
            var data = GetDefaultItem(location);

            return data?.Id;
        }

        /// <summary>
        /// Get the First item of the location from the list
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public ItemModel GetDefaultItem(ItemLocationEnum location)
        {
            var dataList = GetLocationItems(location);
            if (!dataList.Any())
            {
                return null;
            }

            var data = dataList.FirstOrDefault();

            return data;
        }

        /// <summary>
        /// Get all the items for a set location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public List<ItemModel> GetLocationItems(ItemLocationEnum location)
        {
            // Convert Right and Left Finger to Finger
            if (location == ItemLocationEnum.RightFinger)
            {
                location = ItemLocationEnum.Finger;
            }

            if (location == ItemLocationEnum.LeftFinger)
            {
                location = ItemLocationEnum.Finger;
            }

            // Find the Items that meet the criteria
            var data = Dataset.Where(m => m.Location == location)
                              .ToList();

            return data;
        }

        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile ItemIndexViewModel instance;
        private static readonly object             syncRoot = new object();

        public static ItemIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ItemIndexViewModel();
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
        /// Returns the item passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override ItemModel CheckIfExists(ItemModel data)
        {
            if (data == null)
            {
                return null;
            }

            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item...

            var myList = Dataset.FirstOrDefault(a => a.Name == data.Name &&
                                                     a.Description == data.Description &&
                                                     a.Damage == data.Damage &&
                                                     a.Attribute == data.Attribute &&
                                                     a.Location == data.Location &&
                                                     a.Range == data.Range &&
                                                     a.Value == data.Value);

            // if (myList == null)
            // {
            //     // it's not a match, return false;
            //     return null;
            // }

            return myList; // use null propagation
        }

        /// <summary>
        /// Load the Default Data
        /// </summary>
        /// <returns></returns>
        public override List<ItemModel> GetDefaultData() => DefaultData.LoadData(new ItemModel());

        #endregion DataOperations_CRUDi
    }
}