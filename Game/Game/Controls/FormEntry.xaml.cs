using Xamarin.Forms;

namespace Game.Controls
{
    public partial class FormEntry : Grid
    {
        public FormEntry()
        {
            InitializeComponent();

            ControlTitle.Text = Title;
            ControlEntry.Text = Text;
            ControlEntry.TextChanged += OnTextChanged;
        }

        #region Title

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(propertyName: nameof(Title),
                                    returnType: typeof(string),
                                    declaringType: typeof(FormEntry),
                                    defaultValue: default(string),
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: TitlePropertyChanged);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        static void TitlePropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (FormEntry)bo;
            control.ControlTitle.Text = $"{n}: ";
        }

        #endregion Title
        #region Text

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(propertyName: nameof(Text),
                                    returnType: typeof(string),
                                    declaringType: typeof(FormEntry),
                                    defaultValue: default(string),
                                    defaultBindingMode: BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        void OnTextChanged(object sender, TextChangedEventArgs e) =>
            Text = e.NewTextValue;

        public static readonly BindableProperty IsSpellCheckEnabledProperty =
            BindableProperty.Create(propertyName: nameof(IsSpellCheckEnabled),
                                    returnType: typeof(bool),
                                    declaringType: typeof(FormEntry),
                                    defaultValue: true,
                                    propertyChanged: (bo, o, n) =>
                                    {
                                        if (!(bo is FormEntry control))
                                            return;

                                        control.ControlEntry.IsTextPredictionEnabled = (bool)n;
                                    });

        public bool IsSpellCheckEnabled
        {
            get => (bool)GetValue(IsSpellCheckEnabledProperty);
            set => SetValue(IsSpellCheckEnabledProperty, value);
        }

        #endregion Text
        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(propertyName: nameof(Placeholder),
                                    returnType: typeof(string),
                                    declaringType: typeof(FormEntry),
                                    defaultValue: default(string),
                                    defaultBindingMode: BindingMode.TwoWay,
                                    propertyChanged: (bo, o, n) =>
                                    {
                                        if (!(bo is FormEntry control))
                                            return;

                                        if (n != null)
                                            control.ControlEntry.Placeholder = n.ToString();
                                    });
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create(propertyName: nameof(PlaceholderTextColor),
                                    returnType: typeof(Color),
                                    declaringType: typeof(FormEntry),
                                    defaultValue: default(Color),
                                    propertyChanged: (bo, o, n) =>
                                    {
                                        if (!(bo is FormEntry control))
                                            return;

                                        control.ControlEntry.PlaceholderColor = (Color)n;
                                    });

        public Color PlaceholderTextColor
        {
            get => (Color)GetValue(PlaceholderTextColorProperty);
            set => SetValue(PlaceholderTextColorProperty, value);
        }

        #endregion
    }
}
