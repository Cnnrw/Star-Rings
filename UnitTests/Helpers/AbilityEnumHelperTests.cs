using System;
using System.Linq;

using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Helpers
{
    [TestFixture]
    class AbilityEnumHelperTests
    {
        [Test]
        public void AbilityEnumHelper_GetFullList_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetFullList;

            // Assert
            Assert.AreEqual(10, result.Count());

            // Assert
        }

        [Test]
        public void AbilityEnumHelper_GetListJedi_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListJedi;

            // Assert
            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void AbilityEnumHelper_GetListWookie_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListWookie;

            // Assert
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void AbilityEnumHelper_GetListOthers_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListOthers;

            // Assert
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void AbilityEnumHelper_ConvertStringToEnum_Should_Pass()
        {
            // Arrange

            var myList = Enum.GetNames(typeof(AbilityEnum)).ToList();

            AbilityEnum myActual;
            AbilityEnum myExpected;

            // Act

            foreach (var item in myList)
            {
                myActual = AbilityEnumHelper.ConvertStringToEnum(item);
                myExpected = (AbilityEnum)Enum.Parse(typeof(AbilityEnum), item);

                // Assert
                Assert.AreEqual(myExpected, myActual, "string: " + item + TestContext.CurrentContext.Test.Name);
            }
        }
    }
}