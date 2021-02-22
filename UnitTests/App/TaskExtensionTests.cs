// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// using Game.Helpers;
// using Game.Models;
// using Game.Services;
//
// using NUnit.Framework;
//
// using Xamarin.Forms.Mocks;
//
// namespace UnitTests.Views
// {
//     [TestFixture]
//     public class TaskExtensionsTests
//     {
//         [Test]
//         public void TaskExtensions_DatabasePath_Default_Should_Pass()
//         {
//             // Arrange
//
//             // Initilize Xamarin Forms
//             MockForms.Init();
//
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//
//             // Act
//             // Add each model here to warm up and load it.
//             DataSetsHelper.WarmUp();
//
//             // Reset
//
//             // Assert
//             Assert.AreEqual(true, true);
//             Assert.IsNotNull(DataSource_SQL);
//         }
//
//         [Test]
//         public void TaskExtensions_DatabasePath_Error_Should_Fail()
//         {
//             // Arrange
//
//             // Initilize Xamarin Forms
//             MockForms.Init();
//
//             var myException = new NotImplementedException();
//
//             //Action<Exception> myact = (ex => { throw ex; });
//             #pragma warning disable CS0219 // Variable is assigned but its value is never used
//             Action<Exception> myact = ex =>
//             {
//                 var a = 1;
//             };
//             #pragma warning restore CS0219 // Variable is assigned but its value is never used
//
//             // Call with Error=true
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(true, myact);
//
//             // Act
//             // Add each model here to warm up and load it.
//             DataSetsHelper.WarmUp();
//
//             // Reset
//
//             // Assert
//             Assert.AreEqual(true, true);
//         }
//
//         [Test]
//         public void TestDatabaseService_WipeDataListAsync_Should_Throw_Excpetion()
//         {
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//             Assert.ThrowsAsync<NotImplementedException>(() => DataSource_SQL.WipeDataListAsync());
//         }
//
//         [Test]
//         public void TestDatabaseService_CreateAsync_Should_Throw_Excpetion()
//         {
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//             Assert.ThrowsAsync<NotImplementedException>(async () => await DataSource_SQL.CreateAsync(null));
//         }
//
//         [Test]
//         public void TestDatabaseService_ReadAsync_Should_Throw_Excpetion()
//         {
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//             Assert.ThrowsAsync<NotImplementedException>(async () => await DataSource_SQL.ReadAsync(null));
//         }
//
//         [Test]
//         public void TestDatabaseService_UpdateAsync_Should_Throw_Excpetion()
//         {
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//             Assert.ThrowsAsync<NotImplementedException>(async () => await DataSource_SQL.UpdateAsync(null));
//         }
//
//         [Test]
//         public void TestDatabaseService_DeleteAsync_Should_Throw_Excpetion()
//         {
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//             Assert.ThrowsAsync<NotImplementedException>(async () => await DataSource_SQL.DeleteAsync(null));
//         }
//
//         [Test]
//         public void TestDatabaseService_IndexAsync_Should_Throw_Excpetion()
//         {
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//             Assert.ThrowsAsync<NotImplementedException>(async () => await DataSource_SQL.IndexAsync());
//         }
//
//         [Test]
//         public void TestDatabaseService_GetNeedsInitializationAsync_Should_Throw_Excpetion()
//         {
//             IDataStore<ItemModel> DataSource_SQL = new TestDatabaseService<ItemModel>(false);
//             Assert.ThrowsAsync<NotImplementedException>(async () => await DataSource_SQL.GetNeedsInitializationAsync());
//         }
//     }
//
//     #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
//     public class TestDatabaseService<T> : IDataStore<T> where T : new()
//     {
//         private static bool _error;
//
//         public TestDatabaseService(bool condition, Action<Exception> onException = null)
//         {
//             _error = condition;
//             InitializeAsync().SafeFireAndForget(false, onException);
//         }
//
//         public async Task<bool> WipeDataListAsync() => throw new NotImplementedException();
//
//         public async Task<bool> CreateAsync(T data) => throw new NotImplementedException();
//
//         public async Task<T> ReadAsync(string id) => throw new NotImplementedException();
//
//         public async Task<bool> UpdateAsync(T data) => throw new NotImplementedException();
//
//         public async Task<bool> DeleteAsync(string id) => throw new NotImplementedException();
//
//         public async Task<List<T>> IndexAsync() => throw new NotImplementedException();
//
//         public async Task<bool> GetNeedsInitializationAsync() => throw new NotImplementedException();
//
//         private async Task InitializeAsync()
//         {
//             if (_error)
//             {
//                 throw new NotImplementedException();
//             }
//         }
//     }
//     #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
// }

