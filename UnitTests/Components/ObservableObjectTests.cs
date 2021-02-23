using System;
using System.ComponentModel;

using NUnit.Framework;

namespace UnitTests.Components
{
    [TestFixture]
    public class ObservableObjectTests
    {

        [SetUp]
        public void Setup() => person = new Person {FirstName = "James", LastName = "Montemagno"};

        private Person person;

        [Test]
        public void OnPropertyChanged()
        {
            PropertyChangedEventArgs updated = null;
            person.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            person.FirstName = "Motz";

            Assert.IsNotNull(updated, "Property changed didn't raise");
            Assert.AreEqual(updated.PropertyName, nameof(person.FirstName), "Correct Property name didn't get raised");
        }

        [Test]
        public void OnDidntChange()
        {
            PropertyChangedEventArgs updated = null;
            person.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            person.FirstName = "James";
            Assert.IsNull(updated, "Property changed was raised, but shouldn't have been");
        }

        [Test]
        public void OnChangedEvent()
        {
            var triggered = false;
            person.Changed = () =>
            {
                triggered = true;
            };

            person.FirstName = "Motz";

            Assert.IsTrue(triggered, "OnChanged didn't raise");
        }

        [Test]
        public void ValidateEvent()
        {
            const string control = "Motz";
            var triggered = false;
            person.Validate = (oldValue, newValue) =>
            {
                triggered = true;
                return oldValue != newValue;
            };

            person.FirstName = control;

            Assert.IsTrue(triggered, "ValidateValue didn't raise");
            Assert.AreEqual(person.FirstName, control, "Value was not set correctly.");
        }

        [Test]
        public void NotValidateEvent()
        {
            var control = person.FirstName;
            var triggered = false;
            person.Validate = (oldValue, newValue) =>
            {
                triggered = true;
                return false;
            };

            person.FirstName = "Motz";

            Assert.IsTrue(triggered, "ValidateValue didn't raise");
            Assert.AreEqual(person.FirstName, control, "Value should not have been set.");
        }

        [Test]
        public void ValidateEventException()
        {
            person.Validate = (oldValue, newValue) => throw new ArgumentOutOfRangeException();

            Assert.That(() => person.FirstName = "Motz", Throws.TypeOf<ArgumentOutOfRangeException>(),
                "Should throw ArgumentOutOfRangeException");
        }
    }
}