using System.Threading.Tasks;

using Game.Models;
using Game.Services;

using NUnit.Framework;

namespace UnitTests.Services
{
    /// <summary>
    ///     This test file is separate from the DatabaseService Tests because it allows it to run in standard mode, rather than
    ///     test mode.
    ///     Only test needed is the if statement on the mode
    ///     Constructor is enough to get to that code
    /// </summary>
    [TestFixture]
    public class DatabaseServiceInitTests
    {

        [SetUp]
        public void Setup() => _dataStore = DatabaseService<ItemModel>.Instance;

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
        public void DatabaseService_GetDataConnection_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = DatabaseService<ItemModel>.GetDataConnection();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
