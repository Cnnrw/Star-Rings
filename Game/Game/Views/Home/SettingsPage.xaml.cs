using System;
using System.Linq;
using System.Threading.Tasks;

using Game.Enums;
using Game.Helpers;
using Game.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        //public DataSourceEnum CurrentDataSource { get; set; } =
        //    ItemIndexViewModel.Instance.GetCurrentDataSource();

        #region Constructors

        public SettingsPage()
        {
            InitializeComponent();

            // Init the Server Item Value to 100 to get everything
            SetServerItemValue("100");
        }

        public SettingsPage(bool UnitTest) { }

        #endregion

        #region Database Settings

        /// <summary>
        /// Data Source Toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataSource_Toggled(object sender, EventArgs e) =>
            MessagingCenter.Send(this, "SetDataSource",
                DataSourceValue.IsToggled ? DataSourceEnum.SQL : DataSourceEnum.Mock);

        /// <summary>
        /// Button to delete the data store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void WipeDataList_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Data", "Are you sure you want to delete all data?", "Yes", "No");
            if (answer)
            {
                RunWipeData();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void RunWipeData() => Task.Run(async () => await DataSetsHelper.WipeDataInSequence());

        #endregion

        #region Debug Settings

        /// <summary>
        /// Example of how to call for Items using HttpGet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void GetItemsGet_Command(object sender, EventArgs e)
        {
            var result = await GetItemsGet();
            await DisplayServerResults(result);
        }

        /// <summary>
        /// Set the value for the Server Item
        /// </summary>
        /// <param name="value"></param>
        public void SetServerItemValue(string value) => ServerItemValue.Text = value;

        /// <summary>
        /// Call the server call for Get Items using HTTP Get
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetItemsGet()
        {
            // Call to the ItemModel Service and have it Get the Items
            // The ServerItemValue Code stands for the batch of items to get
            // as the group to request.  1, 2, 3, 100 (All), or if not specified All

            var result = "No Results";

            var value = Convert.ToInt32(ServerItemValue.Text);
            var dataList = await ItemService.GetItemsFromServerGetAsync(value);

            if (dataList == null)
            {
                return result;
            }

            return dataList.Count == 0
                       ? result
                       : dataList.Aggregate("", (current, itemModel) =>
                           current + itemModel.FormatOutput() + "\n");
        }

        /// <summary>
        /// Example of how to call for Items using Http Post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void GetItemsPost_Command(object sender, EventArgs e)
        {
            var result = await GetItemsPost();
            await DisplayServerResults(result);
        }

        /// <summary>
        /// Show the Results of the server call
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task DisplayServerResults(string result) => await DisplayAlert("Returned List", result, "OK");

        /// <summary>
        /// Get Items using the HTTP Post command
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetItemsPost()
        {
            const string result = "No Results";

            var number = Convert.ToInt32(ServerItemValue.Text);
            const int level = 6;                                        // Max Value of 6
            const AttributeEnum attribute = AttributeEnum.Unknown;      // Any Attribute
            const ItemLocationEnum location = ItemLocationEnum.Unknown; // Any Location
            const bool random = true;                                   // Random between 1 and Level
            const bool updateDataBase = true;                           // Add them to the DB
            const int category = 0;                                     // What category to filter down to, 0 is all

            // will return shoes value 10 of speed.
            // Example  result = await ItemsController.Instance.GetItemsFromGame(1, 10, AttributeEnum.Speed, ItemLocationEnum.Feet, false, true);
            var dataList = await ItemService.GetItemsFromServerPostAsync(number, level, attribute, location, category,
                               random, updateDataBase);

            // Null not possible, returns empty instead
            //if (dataList == null)
            //{
            //    return result;
            //}

            return dataList.Count == 0
                       ? result
                       : dataList.Aggregate("", (current, itemModel) => current + itemModel.FormatOutput() + "\n");

            // Reset the output

            // TODO: Create static Item formatting helper
        }

        #endregion
    }
}
