using Game;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views.Templates
{
    [TestFixture]
    public class ListTemplateSelectorTests
    {
        [SetUp]
        public void SetUp()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App                app;
    }
}
