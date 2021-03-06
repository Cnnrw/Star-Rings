using System.Windows.Input;

using Xamarin.Forms;

// ReSharper disable ArgumentsStyleLiteral
namespace Game.Controls
{
    /// <summary>
    ///     Custom Image Button Control that places text beneath the
    ///     image and attaches a TapGestureRecognizer to the entire
    ///     button, including the text.
    /// </summary>
    public partial class ImgButton : StackLayout
    {
        public ImgButton() => InitializeComponent();

        #region StackLayout Style

        static readonly BindableProperty StackStyleProperty = BindableProperty.Create(propertyName: "StackStyle",
                                                                                      returnType: typeof(Style),
                                                                                      declaringType: typeof(ImgButton),
                                                                                      defaultBindingMode: BindingMode.OneWay,
                                                                                      propertyChanged: StackStylePropertyChanged);

        public Style StackStyle
        {
            get => (Style)GetValue(StackStyleProperty);
            set => SetValue(StackStyleProperty, value);
        }

        static void StackStylePropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;
            control.ButtonStack.Style = (Style)n;
        }

        #endregion StackLayout Style
        #region ImageSource

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(propertyName: "Source",
                                                                                              returnType: typeof(string),
                                                                                              declaringType: typeof(ImgButton),
                                                                                              defaultValue: string.Empty,
                                                                                              defaultBindingMode: BindingMode.TwoWay,
                                                                                              propertyChanged: ImageSourcePropertyChanged);

        public string Source
        {
            get => GetValue(ImageSourceProperty).ToString();
            set => SetValue(ImageSourceProperty, value);
        }

        static void ImageSourcePropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;
            control.ButtonImage.Source = ImageSource.FromFile(n.ToString());
        }

        #endregion ImageSource
        #region ImageStyle

        public static readonly BindableProperty ImageStyleProperty = BindableProperty.Create(propertyName: "ImageStyle",
                                                                                             returnType: typeof(Style),
                                                                                             declaringType: typeof(ImgButton),
                                                                                             defaultValue: null,
                                                                                             defaultBindingMode: BindingMode.TwoWay,
                                                                                             propertyChanged: ImageStylePropertyChanged);

        public Style ImageStyle
        {
            get => (Style)GetValue(ImageStyleProperty);
            set => SetValue(ImageStyleProperty, value);
        }

        static void ImageStylePropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;
            control.ButtonImage.Style = (Style)n;
        }

        #endregion
        #region LabelText

        public static readonly BindableProperty LabelTextProperty =
            BindableProperty.Create(propertyName: nameof(Text),
                                    returnType: typeof(string),
                                    declaringType: typeof(ImgButton),
                                    defaultValue: default(string),
                                    propertyChanged: LabelTextPropertyChanged);

        public string Text
        {
            get => GetValue(LabelTextProperty).ToString();
            set => SetValue(LabelTextProperty, value);
        }

        static void LabelTextPropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;
            control.ButtonText.Text = n.ToString();
        }

        #endregion LabelText
        #region LabelText Style

        static readonly BindableProperty LabelTextStyleProperty =
            BindableProperty.Create(propertyName: nameof(TextStyle),
                                    returnType: typeof(Style),
                                    declaringType: typeof(ImgButton),
                                    defaultValue: null,
                                    propertyChanged: LabelTextStylePropertyChanged);

        public Style TextStyle
        {
            get => (Style)GetValue(LabelTextStyleProperty);
            set => SetValue(LabelTextStyleProperty, value);
        }

        static void LabelTextStylePropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;
            control.ButtonText.Style = (Style)n;
        }

        #endregion
        #region ImageHeightWidth

        #region ImageHeightRequest

        public static readonly BindableProperty ImageHeightRequestProperty = BindableProperty.Create(propertyName: "ImageHeightRequest",
                                                                                                     returnType: typeof(double),
                                                                                                     declaringType: typeof(ImgButton),
                                                                                                     propertyChanged: ImageHeightRequestPropertyChanged);

        public double ImageHeightRequest
        {
            get => (double)GetValue(ImageHeightRequestProperty);
            set => SetValue(ImageHeightRequestProperty, value);
        }

        static void ImageHeightRequestPropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;
            control.ButtonImage.HeightRequest = (double)n;
        }

        #endregion ImageHeightRequest
        #region ImageWidthRequest

        public static readonly BindableProperty ImageWidthRequestProperty = BindableProperty.Create(propertyName: "ImageWidthRequest",
                                                                                                    returnType: typeof(double),
                                                                                                    declaringType: typeof(ImgButton),
                                                                                                    propertyChanged: ImageWidthRequestPropertyChanged);

        public double ImageWidthRequest
        {
            get => (double)GetValue(ImageWidthRequestProperty);
            set => SetValue(ImageWidthRequestProperty, value);
        }

        static void ImageWidthRequestPropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;
            control.ButtonImage.WidthRequest = (double)n;
        }

        #endregion ImageWidthRequest

        #endregion ImageHeightWidthProperty
        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(propertyName: "Command",
                                                                                          returnType: typeof(ICommand),
                                                                                          declaringType: typeof(ImgButton),
                                                                                          propertyChanged: CommandPropertyChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        static void CommandPropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (ImgButton)bo;

            // this gesture recognizer will invoke the command event wherever it is used
            control.ButtonStack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = (ICommand)n,
                CommandParameter = control.CommandParameter
            });
        }

        #endregion Command
        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(propertyName: "CommandParameter",
                                                                                                   returnType: typeof(object),
                                                                                                   declaringType: typeof(ImgButton));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion CommandParameter
    }
}
