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
    public class ImgButtonTests
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
        public void ImgButton_LoadFromXaml_ShouldPass()
        {
            // Arrange
            var imgButton = new ImgButton();

            // Act
            imgButton.LoadFromXaml("<ImgButton Source=\"item.png\" Text=\"Text\"/>");

            // Assert
            Assert.AreEqual("Text", imgButton.Text);
            Assert.AreEqual("item.png", imgButton.Source);
        }

        [Test]
        public void ImgButton_SetCommand_ShouldPass()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act
            imgBttn.Command = new Mock<ICommand>().Object;

            // Assert
            Assert.That(imgBttn.Command, Is.Not.Null);
        }

        [Test]
        public void ImgButton_NoCommand_ShouldFail()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act

            // Assert
            Assert.IsNull(imgBttn.Command);
        }

        [Test]
        public void ImgButton_SetCommandParameter_ShouldPass()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act
            imgBttn.CommandParameter = false;

            // Assert
            Assert.That(imgBttn.CommandParameter, Is.False);
        }

        [Test]
        public void ImgButton_ImageWidthRequest_Get_Set_Should_Pass()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act
            imgBttn.ImageWidthRequest = 20;

            // Assert
            Assert.AreEqual(20, imgBttn.ImageWidthRequest);
        }

        [Test]
        public void ImgButton_ImageHeightRequest_Get_Set_Should_Pass()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act
            imgBttn.ImageHeightRequest = 20;

            // Assert
            Assert.AreEqual(20, imgBttn.ImageHeightRequest);
        }

        [Test]
        public void ImgButton_TextStyle_Get_Set_Should_Pass()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act
            imgBttn.TextStyle = Device.Styles.BodyStyle;

            // Assert
            Assert.AreEqual(Device.Styles.BodyStyle, imgBttn.TextStyle);
        }

        [Test]
        public void ImgButton_ImageStyle_Get_Set_Should_Pass()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act
            imgBttn.ImageStyle = Device.Styles.BodyStyle;

            // Assert
            Assert.AreEqual(Device.Styles.BodyStyle, imgBttn.ImageStyle);
        }

        [Test]
        public void ImgButton_StackStyle_Get_Set_Should_Pass()
        {
            // Arrange
            var imgBttn = new ImgButton();

            // Act
            imgBttn.StackStyle = Device.Styles.BodyStyle;

            // Assert
            Assert.AreEqual(Device.Styles.BodyStyle, imgBttn.StackStyle);
        }
    }
}
