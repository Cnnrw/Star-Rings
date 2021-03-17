using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Game.Helpers;
using Game.Models;
using Game.Services;

using Xamarin.CommunityToolkit.ObjectModel;

namespace Game.ViewModels
{
    /// <summary>
    ///     Non-generic BaseViewModel
    /// </summary>
    public class BaseViewModel : ObservableObject
    {
        protected static INavigationService NavigationService { get; set; }

        protected BaseViewModel(INavigationService navigationService = null)
        {
            NavigationService = navigationService;
        }

        /// <summary>
        /// The String to show on the page
        /// </summary>
        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Mark if the view model is busy loading or done loading
        /// </summary>
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }

    /// <summary>
    /// Base View Model for Data
    /// </summary>
    public class BaseViewModel<T> : BaseViewModel
        where T : new()
    {
        #region Constructor

        /// <summary>
        /// Initialize the ViewModel
        /// Sets the collection Dataset
        /// Sets the Load command
        /// Sets the default data source
        /// </summary>
        protected async void Initialize(INavigationService navigationService = null)
        {
            NavigationService = navigationService;
            Dataset = new ObservableCollection<T>();

            LoadDatasetCommand = new AsyncCommand(ExecuteLoadDataCommand);

            await SetDataSource(CurrentDataSource); // Set to Mock to start with
        }

        #endregion Constructor
        #region Static Variables

        // The Mock DataStore
        private static IDataStore<T> DataSourceMock => MockDataStore<T>.Instance;

        // The SQL DataStore
        private static IDataStore<T> DataSourceSQL => DatabaseService<T>.Instance;

        #endregion
        #region Instance Variables


        // Which DataStore to use
        private IDataStore<T> _dataStore;

        // Tack the current data source, SQL, Mock
        public int CurrentDataSource { get; set; }

        // Track if the system needs refreshing
        private bool _needsRefresh;

        #endregion
        #region Properties

        // Command to force a Load of data
        public ICommand LoadDatasetCommand { get; set; }

        // The Data set of records
        public ObservableCollection<T> Dataset { get; set; }

        #endregion Attributes
        #region DataSourceManagement

        /// <summary>
        /// Sets the DataSource to use (SQL or Mock)
        /// </summary>
        /// <param name="isSQL"></param>
        /// <returns></returns>
        public async Task<bool> SetDataSource(int isSQL)
        {
            if (isSQL == 1)
            {
                _dataStore = DataSourceSQL;
                CurrentDataSource = 1;
            }
            else
            {
                _dataStore = DataSourceMock;
                CurrentDataSource = 0;
            }

            await LoadDefaultDataAsync();

            // Set Flag for Refresh
            SetNeedsRefresh(true);

            return await Task.FromResult(true);
        }

        /// <summary>
        ///  Load the DefaultData
        ///
        /// READ this:
        ///
        /// This will clear the dataset, and then reload the default data.
        /// This is so the system, is always restored into a known good state
        /// Default Data is part of being in the known good state
        /// If after wiping the system, if the data lists are empty, something is wrong
        /// As populated lists are expected
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadDefaultDataAsync()
        {
            if (await _dataStore.GetNeedsInitializationAsync())
            {
                Dataset.Clear();

                // Load the Data from the DataStore
                await LoadDataFromIndexAsync();
            }

            // If data exists, do not run
            if (Dataset.Count > 0)
            {
                return false;
            }

            // Take all the items and add them if they don't already exist
            foreach (var data in GetDefaultData())
            {
                await CreateUpdateAsync(data);
            }

            return true;
        }

        /// <summary>
        /// Get the Default Data
        /// The ViewModel will Implement
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetDefaultData() => new List<T>();

        #endregion DataSourceManagement
        #region Refresh

        /// <summary>
        /// Command that Loads the Data
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try { await LoadDataFromIndexAsync(); }
            catch (Exception ex) { Debug.WriteLine(ex); }
            finally { IsBusy = false; }
        }

        /// <summary>
        /// Load the Data from the Index Call into the Data List
        /// </summary>
        /// <returns></returns>
        async Task LoadDataFromIndexAsync()
        {
            Dataset.Clear();
            var dataset = await _dataStore.IndexAsync();

            // Example of how to sort the database output using a linq query.
            // Sort the list
            dataset = SortDataset(dataset);

            foreach (var data in dataset)
            {
                // Make a Copy of the Item Model to add to the List
                Dataset.Add(data);
            }
        }

        /// <summary>
        /// Default Sort Dataset
        /// the default just returns the list
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public virtual List<T> SortDataset(IEnumerable<T> dataset) =>
            dataset.ToList();

        /// <summary>
        /// Return True if a refresh is needed
        /// It sets the refresh flag to false
        /// </summary>
        /// <returns></returns>
        public bool NeedsRefresh()
        {
            if (!_needsRefresh)
                return false;

            _needsRefresh = false;
            return true;
        }

        /// <summary>
        /// Returns the needs refresh value
        /// </summary>
        /// <returns></returns>
        public bool GetNeedsRefresh() => _needsRefresh;

        /// <summary>
        /// Sets the need to refresh
        /// </summary>
        /// <param name="value"></param>
        public void SetNeedsRefresh(bool value) => _needsRefresh = value;

        /// <summary>
        /// Force data to refresh
        /// </summary>
        public void ForceDataRefresh()
        {
            // Reset
            // ReSharper disable once UnusedVariable
            var canExecute = LoadDatasetCommand.CanExecute(null);
            LoadDatasetCommand.Execute(null);
        }

        #endregion Refresh
        #region DataSourceManagement

        /// <summary>
        /// The Wipe Data comes in from multiple Messages one from each view model
        /// The user can also click the Wipe button quickly
        ///
        /// So need a way to control the wipe so it does not overlap
        ///
        /// First call up to the shared Helper so wipe wipes all data sets, not just the message that came in
        /// This will ensure the wipe happens in the correct sequence.
        ///
        /// Then the helper will call to the BaseView to wipe just its data
        /// </summary>
        /// <returns></returns>
        protected async Task WipeDataListAsync() => await DataSetsHelper.WipeDataInSequence();

        /// <summary>
        /// Wipes the current Data from the Data Store
        /// </summary>
        public async Task DataStoreWipeDataListAsync()
        {
            Dataset.Clear();

            await _dataStore.WipeDataListAsync();
            await LoadDefaultDataAsync(); // Load the Sample Data
        }

        /// <summary>
        /// Returns the current data source
        /// </summary>
        public int GetCurrentDataSource() => CurrentDataSource;

        #endregion DataSourceManagement
        #region DataOperations_CRUDi

        /// <summary>
        /// API to add the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(T data)
        {
            if (data == null)
                return false;

            Dataset.Add(data);
            var result = await _dataStore.CreateAsync(data);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Get the data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> ReadAsync(string id)
        {
            var myData = await _dataStore.ReadAsync(id);
            return myData;
        }

        /// <summary>
        /// Update the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            var baseDataId = ((BaseModel<T>)(object)data).Id;

            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(baseDataId);
            if (record == null)
            {
                return false;
            }

            // Save the change to the Data Store
            var result = await _dataStore.UpdateAsync(data);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Delete the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            var baseDataId = ((BaseModel<T>)(object)data).Id;

            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(baseDataId);
            if (record == null)
            {
                return false;
            }

            // remove the record from the current data set in the viewmodel
            Dataset.Remove(data);

            // Have the record deleted from the data source
            var result = await _dataStore.DeleteAsync(((BaseModel<T>)(object)record).Id);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Returns the item passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual T CheckIfExists(T data)
        {
            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item...

            var myList =
                Dataset.FirstOrDefault(a => ((BaseModel<T>)(object)a).Name == ((BaseModel<T>)(object)data).Name);

            return myList ?? default(T);
        }

        /// <summary>
        /// Having this at the ViewModel, because it has the DataStore
        /// That allows the feature to work for both SQL and the Mock datastores...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateUpdateAsync(T data)
        {
            if (data == null)
                return false;

            // Check to see if the data exist
            var oldData = CheckIfExists(data);
            if (oldData == null)
                return await CreateAsync(data);

            // Compare it, if different update in the DB
            // ReSharper disable once UnusedVariable
            var UpdateResult = await UpdateAsync(data);

            // Return True, not adding
            return true;
        }

        /// <summary>
        /// This method is for the game engine to call to add an item to the item list
        /// It is not async, so it can be called from the game engine on it's thread
        /// It sets the needs refresh flag
        /// Items added to the list this way, are not saved to the DB, they are temporary during the game.
        /// Refactor for the future would be to create a separate item list for the game to add to, and work with.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Create_Sync(T data)
        {
            if (data == null)
                return false;

            Dataset.Add(data);
            SetNeedsRefresh(true);
            return true;
        }

        #endregion DataOperations_CRUDi
    }
}
