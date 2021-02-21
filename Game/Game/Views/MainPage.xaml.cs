using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using Game.Enums;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     Main Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MainPage : FlyoutPage
    {
        // Collection of Navigation Pages
        public readonly Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        /// <summary>
        ///     Constructor setups the behavior and menu pages
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
        }

        /// <summary>
        ///     Process the Menu Selected item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task NavigateFromMenu(int id)
        {
            // See if the Page is in memory, if not load it
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemEnum.RebelBase:
                        MenuPages.Add(id, new NavigationPage(new RebelBasePage()));
                        break;

                    case (int)MenuItemEnum.Battle:
                        MenuPages.Add(id, new NavigationPage(new PickCharactersPage()));
                        break;

                    case (int)MenuItemEnum.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;

                    case (int)MenuItemEnum.Home:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;

                    case (int)MenuItemEnum.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                }
            }

            // Switch to the Page
            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                // Android needs a deal, iOS and UWP does not
                if (Device.RuntimePlatform == Device.Android)
                {
                    await Task.Delay(100);
                }

                IsPresented = false;
            }
        }
    }
}