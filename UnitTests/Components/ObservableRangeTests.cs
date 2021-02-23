using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

using Game.Components;

using NUnit.Framework;

namespace UnitTests.Components
{
    [TestFixture]
    public class ObservableRangeTests
    {
        [Test]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Add_Range()
        {
            var collection = new ObservableRangeCollection<int>();
            var toAdd = new[] {3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3};

            collection.CollectionChanged += (s, e) =>
            {
                Assert.AreEqual(e.Action, NotifyCollectionChangedAction.Add, "AddRange didn't use Add like requested.");
                Assert.IsNull(e.OldItems, "OldItems should be null");
                Assert.AreEqual(toAdd.Length, e.NewItems.Count, "Expected and actual OldItems don't match.");

                for (var i = 0; i < toAdd.Length; i++)
                {
                    Assert.AreEqual(toAdd[i], (int)e.NewItems[i], "Expected and actual NewItems don't match.");
                }
            };

            collection.AddRange(toAdd);
        }

        [Test]
        public void Add_Range_Empty()
        {
            var collection = new ObservableRangeCollection<int>();
            var toAdd = new int[0];

            collection.CollectionChanged += (s, e) =>
            {
                Assert.Fail("The event is raised.");
            };
            collection.AddRange(toAdd);
        }

        [Test]
        public void ReplaceRange()
        {
            var collection = new ObservableRangeCollection<int>();

            var toAdd = new[] {3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3};
            var toRemove = new[] {1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 0, 0};

            collection.AddRange(toRemove);
            collection.CollectionChanged += (s, e) =>
            {
                Assert.AreEqual(e.Action,
                    NotifyCollectionChangedAction.Reset,
                    "ReplaceRange didn't use Remove like requested.");

                Assert.IsNull(e.OldItems, "OldItems should be null.");
                Assert.IsNull(e.NewItems, "NewItems should be null.");

                Assert.AreEqual(collection.Count, toAdd.Length, "Lengths are not the same");

                for (var i = 0; i < toAdd.Length; i++)
                {
                    if (collection[i] != toAdd[i])
                    {
                        Assert.Fail("Expected and actual items don't match.");
                    }
                }
            };
            collection.ReplaceRange(toAdd);
        }

        [Test]
        public void Replace_Range_On_NonEmpty_Collection_Should_Always_Raise_Collection_Changes()
        {
            var collection = new ObservableRangeCollection<int>(new[] {1});
            var toAdd = new int[0];
            var eventRaised = false;

            collection.CollectionChanged += (s, e) =>
            {
                eventRaised = true;
            };

            collection.ReplaceRange(toAdd);
            Assert.IsTrue(eventRaised, "Collection Reset should be raised");
        }

        [Test]
        public void Replace_Range_On_Empty_Collection_Should_NOT_Raise_Collection_Changes_When_Empty()
        {
            var collection = new ObservableRangeCollection<int>();
            var toAdd = new int[0];

            collection.CollectionChanged += (s, e) =>
            {
                Assert.Fail("Collection changes should NOT be raised.");
            };

            collection.ReplaceRange(toAdd);
        }

        [Test]
        public void Replace_Range_Should_NOT_Mutate_Source()
        {
            var sourceData = new List<int>(new[] {1, 2, 3});
            var collection = new ObservableRangeCollection<int>(new[] {1, 2, 3, 4, 5, 6});

            collection.ReplaceRange(sourceData);

            Assert.IsTrue(sourceData.Count == 3, "source data was mutated");
        }

        [Test]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void Remove_Range_Remove_Test_Method()
        {
            var collection = new ObservableRangeCollection<int>();

            var toAdd = new[] {3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3};
            var toRemove = new[] {1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 0, 0};

            collection.AddRange(toAdd);

            collection.CollectionChanged += (s, e) =>
            {
                if (e.Action != NotifyCollectionChangedAction.Remove)
                {
                    Assert.Fail("RemoveRange didn't use Remove like requested.");
                }

                if (e.OldItems == null)
                {
                    Assert.Fail("OldItems should not be null.");
                }

                var expected = new[] {1, 1, 2, 2, 3, 3, 4, 5, 5, 6, 7, 8, 9, 9};

                if (expected.Length != e.OldItems.Count)
                {
                    Assert.Fail("Expected and actual OldItems don't match.");
                }

                for (var i = 0; i < expected.Length; i++)
                {
                    if (expected[i] != (int)e.OldItems[i])
                    {
                        Assert.Fail("Expected and actual OldItems don't match.");
                    }
                }
            };
            collection.RemoveRange(toRemove, NotifyCollectionChangedAction.Remove);
        }

        [Test]
        public void Remove_Range_Empty()
        {
            var collection = new ObservableRangeCollection<int>();

            var toAdd = new[] {3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3};

            var toRemove = new int[0];

            collection.AddRange(toAdd);
            collection.CollectionChanged += (s, e) =>
            {
                Assert.Fail("The event is raised.");
            };

            collection.RemoveRange(toRemove, NotifyCollectionChangedAction.Remove);
        }

        [Test]
        public void Remove_Range_Should_NOT_Mutate_Source_When_Source_Data_Is_Not_Present()
        {
            var sourceData = new List<int>(new[] {1, 2, 3});
            var collection = new ObservableRangeCollection<int>(new[] {4, 5, 6});

            collection.RemoveRange(sourceData, NotifyCollectionChangedAction.Remove);

            Assert.IsTrue(sourceData.Count == 3, "Source data was mutated");
        }

        [Test]
        public void Remove_Range_Should_NOT_Mutate_Source_When_Source_Data_Is_Present()
        {
            var sourceData = new List<int>(new[] {1, 2, 3});
            var collection = new ObservableRangeCollection<int>(new[] {1, 2, 3, 4, 5, 6});

            collection.RemoveRange(sourceData, NotifyCollectionChangedAction.Remove);

            Assert.That(sourceData.Count == 3, Is.True, "source data was mutated");
        }

        [Test]
        public void Remove_Range_Should_NOT_Mutate_Collection_When_Source_Data_Is_Not_Preseent()
        {
            var sourceData = new List<int>(new[] {1, 2, 3});
            var collection = new ObservableRangeCollection<int>(new[] {4, 5, 6, 7, 8, 9});

            collection.RemoveRange(sourceData, NotifyCollectionChangedAction.Remove);

            // The collection should not be modified if the source items are not found
            Assert.That(collection.Count == 6, Is.True, "collection was mutated");
        }
    }
}