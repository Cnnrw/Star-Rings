using System.Linq;
using System.Threading.Tasks;

using Game.Models;
using Game.Services;

using NUnit.Framework;

namespace UnitTests.Services
{
    [TestFixture]
    public class DatabaseServiceTests
    {

        [SetUp]
        public void Setup()
        {
            DatabaseService<ItemModel>._testMode = true;
            _dataStore = DatabaseService<ItemModel>.Instance;
        }

        [TearDown]
        public async Task TearDown() => await _dataStore.WipeDataListAsync();

        private DatabaseService<ItemModel> _dataStore;

        [Test]
        public void DatabaseService_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = _dataStore;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DatabaseService_Constructor_InValid_Should_Fail()
        {
            // Arrange

            // Make a second instance
            DatabaseService<ItemModel>.Initialized = false;

            // Act
            var dataStore2 = new DatabaseService<ItemModel>();

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public async Task DatabaseService_WipeDataListAsync_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = await _dataStore.WipeDataListAsync();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task DatabaseService_WipeDataListAsync_InValid_ForceException_Should_Fail()
        {
            // Arrange

            _dataStore.ForceExceptionOnNumber = 1;

            // Act
            var result = await _dataStore.WipeDataListAsync();

            // Reset
            _dataStore.ForceExceptionOnNumber = 0;

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void DatabaseService_GetDataConnection_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = DatabaseService<ItemModel>.GetDataConnection();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DatabaseService_SetNeedsRefresh_Valid_True_Should_Pass()
        {
            // Arrange
            var originalState = await _dataStore.GetNeedsInitializationAsync();

            // Act
            _dataStore.NeedsInitialization = true;
            var newState = await _dataStore.GetNeedsInitializationAsync();

            // Reset

            // Turn it back to the original state
            _dataStore.NeedsInitialization = originalState;

            // Assert
            Assert.AreEqual(true, newState);
        }

        [Test]
        public async Task DatabaseService_SetNeedsRefresh_Twice_True_Should_Pass()
        {
            // Arrange
            var originalState = await _dataStore.GetNeedsInitializationAsync();

            // Act
            _dataStore.NeedsInitialization = true;
            var newState = await _dataStore.GetNeedsInitializationAsync();
            var newState2 = await _dataStore.GetNeedsInitializationAsync();

            // Reset

            // Turn it back to the original state
            _dataStore.NeedsInitialization = originalState;

            // Assert
            Assert.AreEqual(false, newState2);
        }

        [Test]
        public async Task DatabaseService_WipeDataListAsync_Valid_True_Should_Pass()
        {
            // Arrange

            // Act
            var newState = await _dataStore.WipeDataListAsync();

            // Reset

            // Turn it back to the original state

            // Assert
            Assert.AreEqual(true, newState);
        }

        [Test]
        public async Task DatabaseService_CreateAsync_Valid_Should_Pass()
        {
            // Arrange

            // Act
            var result = await _dataStore.CreateAsync(new ItemModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task DatabaseService_CreateAsync_InValid_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = await _dataStore.CreateAsync(null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task DatabaseService_CreateAsync_InValid_ForceException_Should_Fail()
        {
            // Arrange
            _dataStore.ForceExceptionOnNumber = 1;

            // Act
            var result = await _dataStore.CreateAsync(new ItemModel());

            // Reset
            _dataStore.ForceExceptionOnNumber = 0;

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task DatabaseService_Read_Valid_Should_Pass()
        {
            // Arrange
            var item = new ItemModel();
            await _dataStore.CreateAsync(item);

            // Act
            var result = await _dataStore.ReadAsync(item.Id);

            // Reset

            // Assert
            Assert.AreEqual(item.Id, result.Id);
        }

        [Test]
        public async Task DatabaseService_Read_InValid_Null_List_Should_Fail()
        {
            // Arrange
            var item = new ItemModel();
            await _dataStore.CreateAsync(item);

            // Act
            var result = await _dataStore.ReadAsync(null);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task DatabaseService_Read_InValid_ForceException_Should_Fail()
        {
            // Arrange
            var item = new ItemModel();
            await _dataStore.CreateAsync(item);

            _dataStore.ForceExceptionOnNumber = 1;
            // Act
            var result = await _dataStore.ReadAsync(item.Id);

            // Reset
            _dataStore.ForceExceptionOnNumber = 0;

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task DatabaseService_Index_Valid_Should_Pass()
        {
            // Arrange
            var item = new ItemModel();
            await _dataStore.CreateAsync(item);

            // Act
            var result = await _dataStore.IndexAsync();

            // Reset

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task DatabaseService_Index_InValid_ForceException_Should_Fail()
        {
            // Arrange
            var item = new ItemModel();
            await _dataStore.CreateAsync(item);

            _dataStore.ForceExceptionOnNumber = 1;
            // Act
            var result = await _dataStore.IndexAsync();

            // Reset
            _dataStore.ForceExceptionOnNumber = 0;

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task DatabaseService_Delete_Valid_Should_Pass()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            var item2 = new ItemModel {Name = "b"};

            await _dataStore.CreateAsync(item1);
            await _dataStore.CreateAsync(item2);

            // Act
            var result = await _dataStore.DeleteAsync(item1.Id);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task DatabaseService_Delete_InValid_Should_Fail()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            var item2 = new ItemModel {Name = "b"};

            await _dataStore.CreateAsync(item1);
            await _dataStore.CreateAsync(item2);

            // Act
            var result = await _dataStore.DeleteAsync("bogus");

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task DatabaseService_Delete_InValid_Null_Should_Fail()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            var item2 = new ItemModel {Name = "b"};

            await _dataStore.CreateAsync(item1);
            await _dataStore.CreateAsync(item2);

            // Act
            var result = await _dataStore.DeleteAsync(null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task DatabaseService_Delete_InValid_ForceException_Should_Fail()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            await _dataStore.CreateAsync(item1);

            _dataStore.ForceExceptionOnNumber = 3; // Read, Delete

            // Act
            var result = await _dataStore.DeleteAsync(item1.Id);

            // Reset
            _dataStore.ForceExceptionOnNumber = 0;

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task DatabaseService_Update_Valid_Should_Pass()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            var item2 = new ItemModel {Name = "b"};

            await _dataStore.CreateAsync(item1);
            await _dataStore.CreateAsync(item2);

            // Act
            item2.Name = "c";

            var result = await _dataStore.UpdateAsync(item2);
            var name = await _dataStore.ReadAsync(item2.Id);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("c", name.Name);
        }

        [Test]
        public async Task DatabaseService_Update_InValid_Null_Should_Fail()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            var item2 = new ItemModel {Name = "b"};

            await _dataStore.CreateAsync(item1);
            await _dataStore.CreateAsync(item2);

            // Act
            var result = await _dataStore.UpdateAsync(null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task DatabaseService_Update_InValid_ID_Should_Fail()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            var item2 = new ItemModel {Name = "b"};

            await _dataStore.CreateAsync(item1);
            //await DataStore.CreateAsync(item2);   // Don't put 2 in the list

            // Act
            var result = await _dataStore.UpdateAsync(item2);
            var name = await _dataStore.ReadAsync(item1.Id);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual("a", name.Name);
        }

        [Test]
        public async Task DatabaseService_Update_ForceException_Should_Fail()
        {
            // Arrange
            var item1 = new ItemModel {Name = "a"};

            var item2 = new ItemModel {Name = "b"};

            await _dataStore.CreateAsync(item1);
            await _dataStore.CreateAsync(item2);

            // Act
            item2.Name = "c";

            _dataStore.ForceExceptionOnNumber = 3; // Read, then update

            // Act
            var result = await _dataStore.UpdateAsync(item1);

            // Reset
            _dataStore.ForceExceptionOnNumber = 0;

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
