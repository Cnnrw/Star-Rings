using Game;
using Game.Controls;

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
        public void FormEntry_NoPlaceholder_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry {Text = "text"};

            // Act

            // Assert
            Assert.IsNull(formEntry.Placeholder);
        }

        [Test]
        public void FormEntry_SetPlaceholder_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry
            {
                Placeholder = "hold my place"
            };

            // Act

            // Assert
            Assert.AreEqual("hold my place", formEntry.Placeholder);
        }

        [Test]
        public void FormEntry_NoTitle_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry();

            // Act

            // Assert
            Assert.IsNull(formEntry.Title);
        }

        [Test]
        public void FormEntry_SetTitle_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry
            {
                Title = "title"
            };

            // Act

            // Assert
            Assert.AreEqual("title", formEntry.Title);
        }

        [Test]
        public void FormEntry_SetText_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry();

            // Act
            formEntry.Text = "test text";

            // Assert
            Assert.AreEqual("test text", formEntry.Text);
        }

        [Test]
        public void FormEntry_NullText_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry();

            // Act

            // Assert
            Assert.IsNull(formEntry.Text);
        }

        [Test]
        public void FormEntry_SetPlaceHolderTextColor_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry
            {
                Title = "title"
            };

            // Act
            formEntry.PlaceholderTextColor = Color.Black;

            // Assert
            Assert.AreEqual(Color.Black, formEntry.PlaceholderTextColor);
        }

        [Test]
        public void FormEntry_SetIsSpellCheckEnabled_ShouldPass()
        {
            // Arrange
            var formEntry = new FormEntry
            {
                Title = "title"
            };

            // Act
            formEntry.IsSpellCheckEnabled = true;

            // Assert
            Assert.AreEqual(true, formEntry.IsSpellCheckEnabled);
        }
    }
}
