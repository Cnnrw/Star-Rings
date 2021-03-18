using Game.ViewModels;

using NUnit.Framework;

namespace UnitTests.ViewModels
{
    [TestFixture]
    public class ScoreViewModelTests : ScoreViewModel
    {
        [Test]
        public void ScoreViewModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ScoreViewModel();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
