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

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(propertyName: nameof(Title),
                                                                                        returnType: typeof(string),
                                                                                        declaringType: typeof(FormEntry),
                                                                                        defaultValue: string.Empty,
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

        public static readonly BindableProperty TextProperty = BindableProperty.Create(propertyName: nameof(Text),
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

        #endregion
    }
}
