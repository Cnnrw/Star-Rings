using Game.Services;
using Game.Templates.Pages;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    ///     The Main Game Page
    /// </summary>
    public partial class RebelBasePage : BasePage
    {
        internal RebelBasePage(bool unitTests) { }

        /// <summary>
        ///     Constructor
        /// </summary>
        public RebelBasePage() : this(App.NavigationService)
        { }

        public RebelBasePage(INavigationService navigationService)
        {
            InitializeComponent();

            BindingContext = new RebelBaseViewModel(Navigation);
        }
    }
}
