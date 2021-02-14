using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using Game.Controls;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    ///     Main Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        // Collection of Navigation Pages
        public readonly Dictionary<int, CustomNavigationPage> MenuPages = new Dictionary<int, CustomNavigationPage>();

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
                    case (int)MenuItemEnum.Village:
                        MenuPages.Add(id, new CustomNavigationPage(new VillagePage()));
                        break;

                    case (int)MenuItemEnum.Battle:
                        MenuPages.Add(id, new CustomNavigationPage(new PickCharactersPage()));
                        break;

                    case (int)MenuItemEnum.About:
                        MenuPages.Add(id, new CustomNavigationPage(new AboutPage()));
                        break;

                    case (int)MenuItemEnum.Home:
                        MenuPages.Add(id, new CustomNavigationPage(new HomePage()));
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