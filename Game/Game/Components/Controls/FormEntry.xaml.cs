using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormEntry : Grid
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(FormEntry), default(string));

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(FormEntry), default(string),
                                    BindingMode.TwoWay);

        public FormEntry()
        {
            InitializeComponent();

            ControlTitle.Text = Title;
            ControlEntry.Text = Text;
            ControlEntry.TextChanged += OnTextChanged;
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e) => Text = e.NewTextValue;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                ControlTitle.Text = Title;
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                ControlEntry.Text = Text;
            }
        }
    }
}