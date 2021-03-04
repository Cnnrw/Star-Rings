using System;

using Game.Services;

using Xamarin.Forms;

namespace Game.Templates.Pages
{
    /// <summary>
    ///     BasePage adds a background image and close button that pops the
    ///     top page from the navigation stack.
    /// </summary>
    public partial class BasePage : ContentPage
    {
        #region BindableProperties

        static readonly BindableProperty PageBackgroundProperty = BindableProperty.Create(propertyName: nameof(PageBackground),
                                                                                          typeof(ImageSource),
                                                                                          typeof(BasePage),
                                                                                          (ImageSource)"page_background.png");

        public ImageSource PageBackground
        {
            get => (ImageSource)GetValue(PageBackgroundProperty);
            set => SetValue(PageBackgroundProperty, value);
        }

        #endregion BindableProperties
        #region Constructors
        public BasePage() : this(App.NavigationService) { }

        internal BasePage(INavigationService navigationService)
        {
            _navigationService = navigationService;

            InitializeComponent();
        }

        internal BasePage(bool unitTests) { }

        #endregion Constructors

        readonly INavigationService _navigationService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CloseButton_OnClick(object sender, EventArgs e) =>
            await _navigationService.GoBack();

        protected override bool OnBackButtonPressed()
        {
            _navigationService.GoBack();

            return true;
        }
    }
}
