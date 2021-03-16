using Game;
using Game.Models;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views.Templates
{
    [TestFixture]
    public class ListTemplateSelectorTests : ListTemplateSelector
    {
        [SetUp]
        public void SetUp()
        {
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            _app = new App();
            Application.Current = _app;
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        App _app;

        [Test]
        public void ListTemplateSelector_ReturnsCorrectType()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsInstanceOf<DataTemplate>(SelectTemplate(new ItemModel(), null));
            Assert.IsInstanceOf<DataTemplate>(SelectTemplate(new CharacterModel(), null));
            Assert.IsInstanceOf<DataTemplate>(SelectTemplate(new MonsterModel(), null));
            Assert.IsInstanceOf<DataTemplate>(SelectTemplate(new ScoreModel(), null));
            Assert.That(SelectTemplate(new object(), null), Is.Null);
        }
    }
}
