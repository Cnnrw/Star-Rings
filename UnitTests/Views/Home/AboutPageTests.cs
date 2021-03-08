using System.Threading.Tasks;

using Game;
using Game.Models;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class AboutPageTests : AboutPage
    {

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();
        }

        [TearDown]
        public void TearDown() => Application.Current = null;

        private App app;
        private AboutPage page;

        public AboutPageTests() : base(true) { }

    }
}
