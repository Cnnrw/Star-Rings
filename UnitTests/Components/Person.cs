using System;

using Game.Components;

using NUnit.Framework;

namespace UnitTests.Components
{
    [SetUpFixture]
    public class Person : ObservableObject
    {

        private string firstName;

        private string lastName;
        public Action Changed { get; set; }

        public Func<string, string, bool> Validate { get; set; }

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value, onChanged: Changed, validateValue: Validate);
        }

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value, onChanged: Changed, validateValue: Validate);
        }

        public string SortName => FirstName[0].ToString().ToUpperInvariant();
    }
}