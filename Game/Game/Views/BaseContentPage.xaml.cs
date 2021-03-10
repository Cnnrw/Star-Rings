using System;
using System.Collections.Generic;

using Game.Services;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     BaseContentPage adds a background image and close button that pops the
    ///     top page from the navigation stack.
    /// </summary>
    public partial class BaseContentPage : ContentPage
    {
        readonly INavigationService _navigationService;

        // ReSharper disable once NotAccessedField.Local
        IList<View> _toolbarButtons;

        public IList<View> ToolbarButtons
        {
            set => _toolbarButtons = value;
        }

        #region Constructors

        public BaseContentPage() : this(App.NavigationService) { }

        BaseContentPage(INavigationService navigationService)
        {
            InitializeComponent();

            _navigationService = navigationService;
        }

        #endregion Constructors
        #region Overrides

        /// <summary>
        /// Retrieves the Children property of the StackLayout for additional
        /// buttons so derived pages can add their own.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Grab 'Buttons' StackLayout
            var buttons = (StackLayout)GetTemplateChild("Buttons");

            // Set 'ToolbarItems' to 'Buttons'
            ToolbarButtons = buttons.Children;
        }

        #endregion Overrides
        #region PageBackground

        static readonly BindableProperty PageBackgroundProperty = BindableProperty.Create(propertyName: nameof(PageBackground),
                                                                                          returnType: typeof(ImageSource),
                                                                                          declaringType: typeof(BaseContentPage),
                                                                                          defaultValue: (ImageSource)"page_background.png");

        public ImageSource PageBackground
        {
            get => (ImageSource)GetValue(PageBackgroundProperty);
            set => SetValue(PageBackgroundProperty, value);
        }

        #endregion PageBackground
        #region BackButton

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CloseButton_OnClick(object sender, EventArgs e) => await _navigationService.GoBack();

        protected override bool OnBackButtonPressed()
        {
            _navigationService.GoBack();
            return true;
        }

        static readonly BindableProperty IsBackButtonVisibleProperty = BindableProperty.Create(propertyName: nameof(IsBackButtonVisible),
                                                                                               returnType: typeof(bool),
                                                                                               declaringType: typeof(BaseContentPage),
                                                                                               defaultValue: true);

        public bool IsBackButtonVisible
        {
            get => (bool)GetValue(IsBackButtonVisibleProperty);
            set => SetValue(IsBackButtonVisibleProperty, value);
        }

        #endregion BackButton
        #region Title

        static readonly BindableProperty PageTitleProperty = BindableProperty.Create(propertyName: nameof(PageTitle),
                                                                                     returnType: typeof(string),
                                                                                     declaringType: typeof(BaseContentPage),
                                                                                     defaultValue: null,
                                                                                     defaultBindingMode: BindingMode.OneWay);

        public string PageTitle
        {
            get => (string)GetValue(PageTitleProperty);
            set => SetValue(PageTitleProperty, value);
        }

        #endregion
    }
}
