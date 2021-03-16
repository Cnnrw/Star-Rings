using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Game.Controls
{
    public partial class TextButton : Grid
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(propertyName: nameof(Source),
                                    returnType: typeof(string),
                                    declaringType: typeof(TextButton),
                                    propertyChanged: SourcePropertyChanged);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(propertyName: nameof(Text),
                                    returnType: typeof(string),
                                    declaringType: typeof(TextButton),
                                    defaultValue: default(string),
                                    propertyChanged: TextPropertyChanged);

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(propertyName: nameof(FontSize),
                                    returnType: typeof(double),
                                    declaringType: typeof(TextButton));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(propertyName: nameof(Command),
                                    returnType: typeof(ICommand),
                                    declaringType: typeof(TextButton),
                                    propertyChanged: CommandPropertyChanged);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(propertyName: nameof(CommandParameter),
                                    returnType: typeof(ICommand),
                                    declaringType: typeof(TextButton),
                                    defaultValue: null);

        public event EventHandler Clicked;

        /// <summary>
        /// TextButton c'tor
        /// </summary>
        public TextButton() => InitializeComponent();

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        static void SourcePropertyChanged(BindableObject bo, object o, object n)
        {
            if (!(bo is TextButton control))
                return;
            control.ButtonImage.Source = ImageSource.FromFile(n.ToString());
        }

        static void TextPropertyChanged(BindableObject bo, object o, object n)
        {
            if (!(bo is TextButton control))
                return;
            control.ButtonText.Text = n.ToString();
        }

        static void CommandPropertyChanged(BindableObject bo, object o, object n)
        {
            var control = (TextButton)bo;

            // this gesture recognizer will invoke the command event wherever it is used
            control.ButtonText.Command = (ICommand)n;
        }

        void ButtonText_OnClicked(object sender, EventArgs e)
        {
            if (Command != null)
            {
                Command.Execute(CommandParameter);
                return;
            }

            Clicked?.Invoke(sender, e);
        }
    }
}
