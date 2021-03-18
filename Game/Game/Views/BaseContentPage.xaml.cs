using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

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
        public static readonly BindableProperty PageTitleProperty =
            BindableProperty.Create(propertyName: nameof(PageTitle),
                                    returnType: typeof(string),
                                    declaringType: typeof(BaseContentPage),
                                    defaultValue: default(string));

        public static readonly BindableProperty PageBackgroundProperty =
            BindableProperty.Create(propertyName: nameof(PageBackground),
                                    returnType: typeof(ImageSource),
                                    declaringType: typeof(BaseContentPage),
                                    defaultValue: (ImageSource)"page_background.png");

        public static readonly BindableProperty BackButtonCommandProperty =
            BindableProperty.Create(propertyName: nameof(Command),
                                    returnType: typeof(ICommand),
                                    declaringType: typeof(BaseContentPage),
                                    propertyChanged: BackButtonCommandPropertyChanged);

        public static readonly BindableProperty BackButtonCommandParameterProperty =
            BindableProperty.Create(propertyName: nameof(BackButtonCommandParameter),
                                    returnType: typeof(ICommand),
                                    declaringType: typeof(BaseContentPage),
                                    defaultValue: null);

        public static readonly BindableProperty IsBackButtonVisibleProperty =
            BindableProperty.Create(propertyName: nameof(IsBackButtonVisible),
                                    returnType: typeof(bool),
                                    declaringType: typeof(BaseContentPage),
                                    defaultValue: true);

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IList<View> ToolbarButtons => Buttons.Children;
        public IList<View> ViewContent => MainContent.Children;

        public event EventHandler BackButtonClicked;

        public BaseContentPage()
        {
            InitializeComponent();
        }

        public string PageTitle
        {
            get => (string)GetValue(PageTitleProperty);
            set => SetValue(PageTitleProperty, value);
        }

        public ImageSource PageBackground
        {
            get => (ImageSource)GetValue(PageBackgroundProperty);
            set => SetValue(PageBackgroundProperty, value);
        }

        public ICommand BackButtonCommand
        {
            get => (ICommand)GetValue(BackButtonCommandProperty);
            set => SetValue(BackButtonCommandProperty, value);
        }

        public object BackButtonCommandParameter
        {
            get => GetValue(BackButtonCommandParameterProperty);
            set => SetValue(BackButtonCommandParameterProperty, value);
        }

        static void BackButtonCommandPropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (BaseContentPage)bo;

            // this gesture recognizer will invoke the command event wherever it is used
            control.BackButton.Command = (ICommand)n;
        }

        public bool IsBackButtonVisible
        {
            get => (bool)GetValue(IsBackButtonVisibleProperty);
            set => SetValue(IsBackButtonVisibleProperty, value);
        }

        void BackButton_OnClicked(object sender, EventArgs e)
        {
            if (BackButtonCommand != null)
            {
                BackButtonCommand.Execute(BackButtonCommandParameter);
                return;
            }

            BackButtonClicked?.Invoke(sender, e);
        }
    }
}
