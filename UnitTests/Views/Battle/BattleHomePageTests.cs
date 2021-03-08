using System.Threading.Tasks;

using Game;
using Game.Enums;
using Game.Models;
using Game.ViewModels;
using Game.Views;

using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class BattleHomePageTests : BattlePage
    {

        private App app;
        private BattlePage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

        }
    }
}
