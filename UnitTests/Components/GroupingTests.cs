using System.Linq;

using Game.Components;

using NExpect;

using NUnit.Framework;

using static NExpect.Expectations;

namespace UnitTests.Components
{
    [TestFixture]
    public class GroupingTests
    {
        [Test]
        public void Grouping()
        {
            var grouped = new ObservableRangeCollection<Grouping<string, Person>>();
            var people = new[]
            {
                new Person {FirstName = "Connor", LastName = "Wilding"},
                new Person {FirstName = "Tate", LastName = "Brasel"},
                new Person {FirstName = "Jack", LastName = "Witt"}
            };

            var sorted = from person in people
                         orderby person.FirstName
                         group person by person.SortName
                         into personGroup
                         select new Grouping<string, Person>(personGroup.Key, personGroup);

            grouped.AddRange(sorted);

            Expect(grouped.Count)
                .To.Equal(3, "There should be 3 groups");
            Expect(grouped[2].Key)
                .To.Equal("T", "Key for group 3 should be T");
            Expect(grouped[0].Count)
                .To.Equal(1, "There should be 1 items in group 0)");
            Expect(grouped[0].Items.Count)
                .To.Equal(1, "There should be 1 items in group 0");
        }

        [Test]
        public void GroupingSubKey()
        {
            var grouped = new ObservableRangeCollection<Grouping<string, string, Person>>();
            var people = new[]
            {
                new Person {FirstName = "Connor", LastName = "Wilding"},
                new Person {FirstName = "James", LastName = "Boggan"},
                new Person {FirstName = "Jack", LastName = "Witt"}
            };

            var sorted = from person in people
                         orderby person.FirstName
                         group person by person.SortName
                         into personGroup
                         select new Grouping<string, string, Person>(personGroup.Key, personGroup.Key, personGroup);

            grouped.AddRange(sorted);

            Expect(grouped.Count)
                .To.Equal(2, "there should be 2 groups");

            Expect(grouped[1].SubKey)
                .To.Equal("J", "Key for group 1 should be J");
            Expect(grouped[1].Key)
                .To.Equal("J", "Key for group 1 should be J");
            Expect(grouped[1].Count)
                .To.Equal(2, "There should be 2 items in group 1");

            Expect(grouped[0].Count)
                .To.Equal(1, "There should be 1 items in group 0");
        }
    }

}