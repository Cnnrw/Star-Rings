using System.Linq;

using Game.Enums;

using NUnit.Framework;

namespace UnitTests.Helpers
{
    [TestFixture]
    class CharacterJobEnumHelperTests
    {
        [Test]
        public void CharacterJobEnumHelper_GetJobList_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnumHelper.GetListCharacterJobs;

            // Assert
            Assert.AreEqual(6, result.Count());

            // Assert
        }
    }
}