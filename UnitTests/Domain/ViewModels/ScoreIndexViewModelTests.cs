using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Game.Models;
using Game.ViewModels;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.ViewModels
{
    public class ScoreIndexViewModelTests
    {
        private ScoreIndexViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            // Add each model here to warm up and load it.
            Game.Helpers.DataSetsHelper.WarmUp();

            _viewModel = ScoreIndexViewModel.Instance;
        }

        [TearDown]
        public void TearDown() => _viewModel.Dataset.Clear();

        [Test]
        public async Task ScoreIndexViewModel_Read_Invalid_ID_Bogus_Should_Fail()
        {
            // Arrange

            // Act
            var result = await _viewModel.ReadAsync("bogus");

            // Reset

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ScoreIndexViewModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = _viewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ScoreIndexViewModel_SortDataSet_Default_Should_Pass()
        {
            // Arrange

            // Add Scores into the list Z ordered
            var dataList = new List<ScoreModel>
            {
                new ScoreModel {Name = "z"},
                new ScoreModel {Name = "m"},
                new ScoreModel {Name = "a"}
            };

            // Act
            var result = _viewModel.SortDataset(dataList);

            // Reset

            // Assert
            Assert.AreEqual("a", result[0].Name);
            Assert.AreEqual("m", result[1].Name);
            Assert.AreEqual("z", result[2].Name);
        }

        [Test]
        public async Task ScoreIndexViewModel_CheckIfScoreExists_Default_Should_Pass()
        {
            // Arrange

            // Add Scores into the list Z ordered
            var dataTest = new ScoreModel {Name = "test"};
            await _viewModel.CreateAsync(dataTest);

            await _viewModel.CreateAsync(new ScoreModel {Name = "z"});
            await _viewModel.CreateAsync(new ScoreModel {Name = "m"});
            await _viewModel.CreateAsync(new ScoreModel {Name = "a"});

            // Act
            var result = _viewModel.CheckIfScoreExists(dataTest);

            // Reset

            // Assert
            Assert.AreEqual(dataTest.Id, result.Id);
        }

        [Test]
        public async Task ScoreIndexViewModel_CheckIfScoreExists_InValid_Missing_Should_Fail()
        {
            // Arrange

            // Add Scores into the list Z ordered
            var dataTest = new ScoreModel {Name = "test"};
            // Don't add it to the list await ViewModel.CreateAsync(dataTest);

            await _viewModel.CreateAsync(new ScoreModel {Name = "z"});
            await _viewModel.CreateAsync(new ScoreModel {Name = "m"});
            await _viewModel.CreateAsync(new ScoreModel {Name = "a"});

            // Act
            var result = _viewModel.CheckIfScoreExists(dataTest);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task ScoreIndexViewModel_Message_Delete_Valid_Should_Pass()
        {
            // Arrange
            await _viewModel.CreateAsync(new ScoreModel());

            // Get the Score to delete
            var first = _viewModel.Dataset.FirstOrDefault();

            // Make a Delete Page
            var myPage = new Game.Views.ScoreDeletePage(true);

            // Act
            MessagingCenter.Send(myPage, "Delete", first);

            var data = await _viewModel.ReadAsync(first.Id);

            // Reset

            // Assert
            Assert.AreEqual(null, data); // Score is removed
        }

        [Test]
        public void ScoreIndexViewModel_Message_Create_Valid_Should_Pass()
        {
            // Arrange

            // Make a new Score
            var data = new ScoreModel();

            // Make a Delete Page
            var myPage = new Game.Views.ScoreCreatePage(true);

            var countBefore = _viewModel.Dataset.Count();

            // Act
            MessagingCenter.Send(myPage, "Create", data);
            var countAfter = _viewModel.Dataset.Count();

            // Reset

            // Assert
            Assert.AreEqual(countBefore + 1, countAfter); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ScoreIndexViewModel_Message_Update_Valid_Should_Pass()
        {
            // Arrange
            await _viewModel.CreateAsync(new ScoreModel());

            // Get the Score to delete
            var first = _viewModel.Dataset.FirstOrDefault();
            first.Name = "test";

            // Make a Delete Page
            var myPage = new Game.Views.ScoreUpdatePage(true);

            // Act
            MessagingCenter.Send(myPage, "Update", first);
            var result = await _viewModel.ReadAsync(first.Id);

            // Reset

            // Assert
            Assert.AreEqual("test", result.Name); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ScoreIndexViewModel_Message_SetDataSource_Valid_Should_Pass()
        {
            // Arrange

            // Get the Score to delete
            var data = 3000; // Non existing value

            // Make the page Page
            var myPage = new Game.Views.SettingsPage(true);

            // Act
            MessagingCenter.Send(myPage, "SetDataSource", data);
            var result = _viewModel.GetCurrentDataSource();

            // Reset
            await _viewModel.SetDataSource(0);

            // Assert
            Assert.AreEqual(0, result); // Count of 0 for the load was skipped
        }

        [Test]
        public async Task ScoreIndexViewModel_Message_WipeDataList_Valid_Should_Pass()
        {
            // Arrange
            await _viewModel.CreateAsync(new ScoreModel());

            // Make the page Page
            var myPage = new Game.Views.SettingsPage(true);

            var data = new ScoreModel();
            await _viewModel.CreateAsync(data);

            // Act
            MessagingCenter.Send(myPage, "WipeDataList", true);
            var countAfter = _viewModel.Dataset.Count();

            // Reset

            // Assert
            Assert.AreEqual(2, countAfter); // Count of 0 for the load was skipped
        }
    }
}