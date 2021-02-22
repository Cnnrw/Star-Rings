using System.Threading.Tasks;

using Game;
using Game.Models;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class SettingsPageTests : SettingsPage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new SettingsPage();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App          app;
        private SettingsPage page;

        // Base Constructor
        public SettingsPageTests() : base(true) { }

        [Test]
        public void SettingsPage_Elements_Get_Set_Should_Pass()
        {
            // Arrange

            // Act
            ((StackLayout)page.FindByName("DatabaseSettingsFrame")).IsVisible = true;
            ((StackLayout)page.FindByName("DebugSettingsFrame")).IsVisible = true;

            ((Switch)page.FindByName("DataSourceValue")).IsVisible = true;
            ((Switch)page.FindByName("DataSourceValue")).IsToggled = true;
            ((Switch)page.FindByName("DataSourceValue")).IsToggled = false;

            ((Entry)page.FindByName("ServerItemValue")).IsVisible = true;

            // Reset

            // Assert
            Assert.IsNotNull((StackLayout)page.FindByName("DebugSettingsFrame"));
            Assert.IsNotNull((StackLayout)page.FindByName("DatabaseSettingsFrame"));

            Assert.IsNotNull((Switch)page.FindByName("DataSourceValue"));
            Assert.IsNotNull((Entry)page.FindByName("ServerItemValue"));
        }

        // TODO: Comment

        // [Test]
        // public void SettingsPage_DatabaseSettingsSwitch_OnToggled_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     var frame = (StackLayout)page.FindByName("DatabaseSettingsFrame");
        //     var current = frame.IsVisible;
        //
        //     var args = new ToggledEventArgs(current);
        //
        //
        //     // Act
        //     page.DatabaseSettingsSwitch_OnToggled(null, args);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(!current); // Got to here, so it happened...
        // }
        //
        // [Test]
        // public void SettingsPage_DebugSettingsSwitch_OnToggled_Default_Should_Pass()
        // {
        //     // Arrange
        //
        //     var frame = (StackLayout)page.FindByName("DebugSettingsFrame");
        //     var current = frame.IsVisible;
        //
        //     var args = new ToggledEventArgs(current);
        //
        //
        //     // Act
        //     page.DebugSettingsSwitch_OnToggled(null, args);
        //
        //     // Reset
        //
        //     // Assert
        //     Assert.IsTrue(!current); // Got to here, so it happened...
        // }

        [Test]
        public void SettingsPage_DataSource_Toggled_Default_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled;

            var args = new ToggledEventArgs(current);

            // Act
            page.DataSource_Toggled(null, args);

            // Reset

            // Assert
            Assert.IsTrue(!current); // Got to here, so it happened...
        }

        [Test]
        public void SettingsPage_DataSource_Toggled_False_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled = false;

            // Act
            control.IsToggled = true;

            var result = control.IsToggled;

            // Reset

            // Assert
            Assert.AreEqual(!current, result);
        }

        [Test]
        public void SettingsPage_DataSource_Toggled_True_Should_Pass()
        {
            // Arrange

            var control = (Switch)page.FindByName("DataSourceValue");
            var current = control.IsToggled = true;

            // Act
            control.IsToggled = false;

            var result = control.IsToggled;

            // Reset

            // Assert
            Assert.AreEqual(!current, result);
        }

        [Test]
        public void SettingsPage_WipeDataList_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.WipeDataList_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public async Task SettingsPage_GetItemsGet_Default_Should_Pass()
        {
            // Arrange
            // Act
            var result = await page.GetItemsGet();

            // Reset

            // Assert
            Assert.AreNotEqual("No Results", result); // Got to here, so it happened...
        }

        //[Test]
        //public async Task AboutPage_GetItemsPost_Default_Should_Pass()
        //{
        //    // Arrange
        //    // Act
        //    var result = await page.GetItemsPost();

        //    // Reset

        //    // Assert
        //    Assert.AreNotEqual("No Results", result); // Got to here, so it happened...
        //}

        [Test]
        public void SettingsPage_GetItemsGet_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemsGet_Command(null, null);

            // Reset

            // Assert
            Assert.AreEqual(true, true); // Got to here, so it happened...
        }

        [Test]
        public void SettingsPage_GetItemsPost_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GetItemsPost_Command(null, null);

            // Reset

            // Assert
            Assert.AreEqual(true, true); // Got to here, so it happened...
        }

        [Test]
        public void SettingsPage_RunWipeData_Should_Pass()
        {
            // Arrange

            // Act
            page.RunWipeData();

            // Reset

            // Assert
            Assert.AreEqual(true, true); // Got to here, so it happened...
        }

        [Test]
        public async Task SettingsPage_GetItemsGet_BadURL_Should_Fail()
        {
            // Arrange
            var hold = WebGlobalsModel.WebSiteAPIURL;
            WebGlobalsModel.WebSiteAPIURL = "https://bogusurl";

            // Act
            var result = await page.GetItemsGet();

            // Reset
            WebGlobalsModel.WebSiteAPIURL = hold;

            // Assert
            Assert.AreEqual("No Results", result); // Got to here, so it happened...
        }

        [Test]
        public async Task SettingsPage_GetItemsGet_Neg_Should_Fail()
        {
            // Arrange

            page.SetServerItemValue("-100");

            // Act
            var result = await page.GetItemsGet();

            // Reset

            // Assert
            Assert.AreEqual("No Results", result); // Got to here, so it happened...
        }

        [Test]
        public async Task SettingsPage_GetItemsPost_BadURL_Should_Fail()
        {
            // Arrange
            var hold = WebGlobalsModel.WebSiteAPIURL;
            WebGlobalsModel.WebSiteAPIURL = "https://bogusurl";

            // Act
            var result = await page.GetItemsPost();

            // Reset
            WebGlobalsModel.WebSiteAPIURL = hold;

            // Assert
            Assert.AreEqual("No Results", result); // Got to here, so it happened...
        }

        [Test]
        public async Task SettingsPage_GetItemsPost_Neg_Should_Fail()
        {
            // Arrange
            var hold = WebGlobalsModel.WebSiteAPIURL;
            WebGlobalsModel.WebSiteAPIURL = "https://bogusurl";

            page.SetServerItemValue("-100");

            // Act
            var result = await page.GetItemsPost();

            // Reset
            WebGlobalsModel.WebSiteAPIURL = hold;

            // Assert
            Assert.AreEqual("No Results", result); // Got to here, so it happened...
        }
    }
}