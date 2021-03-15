using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     BaseContentPage adds a background image and close button that pops the
    ///     top page from the navigation stack.
    /// </summary>
    [ExcludeFromCodeCoverage] // This class crashes R# code coverage
    [ContentProperty(nameof(ViewContent))]
    public partial class BaseContentPage : ContentPage
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IList<View> ToolbarButtons => Buttons.Children;
        public IList<View> ViewContent => MainContent.Children;

        #region Constructors

        public BaseContentPage() =>
            InitializeComponent();

        #endregion Constructors
        #region PageBackground

        static readonly BindableProperty PageBackgroundProperty =
            BindableProperty.Create(propertyName: nameof(PageBackground),
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

        async void CloseButton_OnClick(object sender, EventArgs e) => await App.NavigationService.GoBack();

        static readonly BindableProperty IsBackButtonVisibleProperty =
            BindableProperty.Create(propertyName: nameof(IsBackButtonVisible),
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

        public static readonly BindableProperty PageTitleProperty =
            BindableProperty.Create(propertyName: nameof(PageTitle),
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
