﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Game.Enums;
using Game.Helpers;
using Game.Models;
using Game.Services;

using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    ///     Base View Model for Data
    /// </summary>
    public class BaseViewModel<T> : INotifyPropertyChanged where T : new()
    {

        #region PropertyChanges

        /// <summary>
        ///     Tracking what has changed in the dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        #pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
        protected bool SetProperty<T>(ref T backingStore,
                                      #pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
                                      T                         value,
                                      [CallerMemberName] string propertyName = "",
                                      Action                    onChanged    = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion PropertyChanges

        #region Constructor

        /// <summary>
        ///     Initialize the ViewModel
        ///     Sets the collection Dataset
        ///     Sets the Load command
        ///     Sets the default data source
        /// </summary>
        protected async void Initialize()
        {
            Dataset = new ObservableCollection<T>();
            LoadDatasetCommand = new Command(async () => await ExecuteLoadDataCommand());

            await SetDataSource(DataSourceEnum.Mock); // Set to Mock to start with
        }

        #endregion Constructor

        #region Attributes

        // The Mock DataStore
        private static IDataStore<T> DataSource_Mock => MockDataStore<T>.Instance;

        // The SQL DataStore
        private static IDataStore<T> DataSource_SQL => DatabaseService<T>.Instance;

        // Which DataStore to use
        private IDataStore<T> DataStore;

        // The Data set of records
        public ObservableCollection<T> Dataset { get; set; }

        // Tack the current data source, SQL, Mock
        private DataSourceEnum _currentDataSource = DataSourceEnum.Unknown;

        // Track if the system needs refreshing
        private bool _needsRefresh;

        // Command to force a Load of data
        public Command LoadDatasetCommand { get; set; }

        /// <summary>
        ///     Mark if the view model is busy loading or done loading
        /// </summary>
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;

        /// <summary>
        ///     Gets or sets the title of a page.
        /// </summary>
        /// <value>The title of the page</value>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _icon = string.Empty;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The page icon.</value>
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        #endregion Attributes

        #region DataSourceManagement

        // TODO: Check if we need to await results in override
        public async Task<bool> SetDataSource(int isSQL)
        {
            var newDataSource = Enum.IsDefined(typeof(DataSourceEnum), isSQL)
                                    ? (DataSourceEnum)isSQL
                                    : DataSourceEnum.Unknown;

            return await SetDataSource(newDataSource);
        }

        /// <summary>
        ///     Sets the DataSource to use
        ///     (SQL/Mock, Unknown if isSQL falls outside DataSourceEnum range)
        /// </summary>
        /// <param name="isSQL"></param>
        /// <returns></returns>
        public async Task<bool> SetDataSource(DataSourceEnum isSQL)
        {
            if (isSQL == DataSourceEnum.SQL)
            {
                DataStore = DataSource_SQL;
                _currentDataSource = DataSourceEnum.SQL;
            }
            else
            {
                DataStore = DataSource_Mock;
                _currentDataSource = DataSourceEnum.Mock;
            }

            await LoadDefaultDataAsync();

            // Set Flag for Refresh
            SetNeedsRefresh(true);

            return await Task.FromResult(true);
        }

        /// <summary>
        ///     Load the DefaultData
        ///     READ this:
        ///     This will clear the dataset, and then reload the default data.
        ///     This is so the system, is always restored into a known good state
        ///     Defalt Data is part of being in the known good state
        ///     If after wipeing the system, if the data lists are empty, something is wrong
        ///     As populated lists are expected
        /// </summary>
        /// <returns></returns>
        private async Task<bool> LoadDefaultDataAsync()
        {
            if (await DataStore.GetNeedsInitializationAsync())
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
        ///     Get the Default Data
        ///     The ViewModel will Implement
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetDefaultData() => new List<T>();

        #endregion DataSourceManagement

        #region Refresh

        /// <summary>
        ///     Command that Loads the Data
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                await LoadDataFromIndexAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        ///     Load the Data from the Index Call into the Data List
        /// </summary>
        /// <returns></returns>
        private async Task LoadDataFromIndexAsync()
        {
            Dataset.Clear();
            var dataset = await DataStore.IndexAsync();

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
        ///     Default Sort Dataset
        ///     the default just returns the list
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public virtual List<T> SortDataset(IEnumerable<T> dataset) => dataset.ToList();

        /// <summary>
        ///     Return True if a refresh is needed
        ///     It sets the refresh flag to false
        /// </summary>
        /// <returns></returns>
        public bool NeedsRefresh()
        {
            if (!_needsRefresh)
            {
                return false;
            }

            _needsRefresh = false;
            return true;
        }

        /// <summary>
        ///     Returns the needs refresh value
        /// </summary>
        /// <returns></returns>
        public bool GetNeedsRefresh() => _needsRefresh;

        /// <summary>
        ///     Sets the need to refresh
        /// </summary>
        /// <param name="value"></param>
        public void SetNeedsRefresh(bool value) => _needsRefresh = value;

        /// <summary>
        ///     Force data to refresh
        /// </summary>
        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDatasetCommand.CanExecute(null);
            LoadDatasetCommand.Execute(null);
        }

        #endregion Refresh

        #region DataSourceManagement

        /// <summary>
        ///     The Wipe Data comes in from multiple Messages one from each view model
        ///     The user can also click the Wipe button quickly
        ///     So need a way to control the wipe so it does not overlap
        ///     First call up to the shared Helper so wipe wipes all data sets, not just the message that came in
        ///     This will ensure the wipe happens in the correct sequence.
        ///     Then the helper will call to the BaseView to wipe just its data
        /// </summary>
        /// <returns></returns>
        protected async Task<bool> WipeDataListAsync()
        {
            var result = await DataSetsHelper.WipeDataInSequence();

            return result;
        }

        /// <summary>
        ///     Wipes the current Data from the Data Store
        /// </summary>
        public async Task<bool> DataStoreWipeDataListAsync()
        {
            Dataset.Clear();

            await DataStore.WipeDataListAsync();

            // Load the Sample Data
            var result = await LoadDefaultDataAsync();

            return result;
        }

        /// <summary>
        ///     Returns the current data source
        /// </summary>
        public DataSourceEnum GetCurrentDataSource() => _currentDataSource;

        #endregion DataSourceManagement

        #region DataOperations_CRUDi

        /// <summary>
        ///     API to add the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            Dataset.Add(data);
            var result = await DataStore.CreateAsync(data);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        ///     Get the data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> ReadAsync(string id)
        {
            var myData = await DataStore.ReadAsync(id);
            return myData;
        }

        /// <summary>
        ///     Update the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            var BaseDataId = ((BaseModel<T>)(object)data).Id;

            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(BaseDataId);
            if (record == null)
            {
                return false;
            }

            // Save the change to the Data Store
            var result = await DataStore.UpdateAsync(data);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        ///     Delete the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            var BaseDataId = ((BaseModel<T>)(object)data).Id;

            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(BaseDataId);
            if (record == null)
            {
                return false;
            }

            // remove the record from the current data set in the viewmodel
            Dataset.Remove(data);

            // Have the record deleted from the data source
            var result = await DataStore.DeleteAsync(((BaseModel<T>)(object)record).Id);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        ///     Returns the item passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SuppressMessage("Style", "IDE0034:Simplify 'default' expression", Justification = "<Pending>")]
        public virtual T CheckIfExists(T data)
        {
            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item...

            var myList =
                Dataset.FirstOrDefault(a => ((BaseModel<T>)(object)a).Name == ((BaseModel<T>)(object)data).Name);

            if (myList == null)
            {
                // it's not a match, return false;
                return default;
            }

            return myList;
        }

        /// <summary>
        ///     Having this at the ViewModel, because it has the DataStore
        ///     That allows the feature to work for both SQL and the Mock datastores...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateUpdateAsync(T data)
        {
            if (data == null)
            {
                return false;
            }

            // Check to see if the data exist
            var oldData = CheckIfExists(data);
            if (oldData == null)
            {
                return await CreateAsync(data);
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync(data);
            if (UpdateResult)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     This method is for the game engine to call to add an item to the item list
        ///     It is not async, so it can be called from the game engine on it's thread
        ///     It sets the needs refresh flag
        ///     Items added to the list this way, are not saved to the DB, they are temporary during the game.
        ///     Refactor for the future would be to create a separate item list for the game to add to, and work with.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Create_Sync(T data)
        {
            if (data == null)
            {
                return false;
            }

            Dataset.Add(data);
            SetNeedsRefresh(true);
            return true;
        }

        #endregion DataOperations_CRUDi

        #region INotifyPropertyChanged

        /// <summary>
        ///     Notify when changes happen
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}