using System.Windows.Input;

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
    public class TextButtonTests
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
        public void TextButton_LoadFromXaml_ShouldPass()
        {
            // Arrange
            var txtBtn = new TextButton();

            // Act
            txtBtn.LoadFromXaml("<ImgButton Source=\"item.png\" Text=\"Text\"/>");

            // Assert
            Assert.AreEqual("Text", txtBtn.Text);
        }

        [Test]
        public void TextButton_SetCommand_ShouldPass()
        {
            // Arrange
            var txtBtn = new TextButton();

            // Act
            txtBtn.Command = new Mock<ICommand>().Object;

            // Assert
            Assert.That(txtBtn.Command, Is.Not.Null);
        }

        [Test]
        public void TextButton_NoCommand_ShouldFail()
        {
            // Arrange
            var txtBtn = new TextButton();

            // Act

            // Assert
            Assert.IsNull(txtBtn.Command);
        }

        //[Test]
        //public void TextButton_SetCommandParameter_ShouldPass()
        //{
        //    // Arrange
        //    var txtBtn = new TextButton();

        //    // Act
        //    txtBtn.CommandParameter = false;

        //    // Assert
        //    Assert.That(txtBtn.CommandParameter, Is.False);
        //}

        [Test]
        public void TextButton_SetSource_ShouldPass()
        {
            // Arrange
            var txtBtn = new TextButton();

            // Act
            txtBtn.Source = "source";

            // Assert
            Assert.AreEqual("source", txtBtn.Source);
        }

        [Test]
        public void TextButton_SetFontSize_ShouldPass()
        {
            // Arrange
            var txtBtn = new TextButton();

            // Act
            txtBtn.FontSize = 12;

            // Assert
            Assert.AreEqual(12, txtBtn.FontSize);
        }
    }
}
