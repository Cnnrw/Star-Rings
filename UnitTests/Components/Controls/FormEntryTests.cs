using Game;
using Game.Controls;

using Moq;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xamarin.Forms.Xaml;

namespace UnitTests.Components.Controls
{
    [TestFixture]
    public class FormEntryTests
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();

            Application.Current = new App();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        [Test]
        public void FormEntry_LoadFromXaml_ShouldPass()
        {
            // Arrange
            var formLbl = new FormEntry();

            // Act
            formLbl.LoadFromXaml("<FormEntry Text=\"Text\" />");

            // Assert
            Assert.AreEqual("Text", formLbl.Text);
        }

        [Test]
        public void FormEntry_TextChanged_ShouldPass()
        {
            var mockFormLabel = new Mock<FormEntry>();
        }
    }
}
