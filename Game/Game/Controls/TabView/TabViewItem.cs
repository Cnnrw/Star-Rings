using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Game.Controls.TabView
{
    [Preserve(AllMembers = true)]
    [ContentProperty(nameof(Content))]
    public sealed class TabViewItem : TemplatedView
    {
        const string SelectedVisualState   = "Selected";
        const string UnselectedVisualState = "Unselected";

        bool _isOnScreen;

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(TabViewItem), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TabViewItem), Color.Default,
                                    propertyChanged: OnTabViewItemPropertyChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty TextColorSelectedProperty =
            BindableProperty.Create(nameof(TextColorSelected), typeof(Color), typeof(TabViewItem), Color.Default);

        public Color TextColorSelected
        {
            get => (Color)GetValue(TextColorSelectedProperty);
            set => SetValue(TextColorSelectedProperty, value);
        }

        public static readonly BindableProperty ContentProperty =
            BindableProperty.Create(nameof(Content), typeof(View), typeof(TabViewItem));

        public View Content
        {
            get => (View)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(TabViewItem), null,
                                    propertyChanged: OnTabViewItemPropertyChanged);

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly BindableProperty IconSelectedProperty =
            BindableProperty.Create(nameof(IconSelected), typeof(ImageSource), typeof(TabViewItem), null,
                                    propertyChanged: OnTabViewItemPropertyChanged);

        public ImageSource IconSelected
        {
            get => (ImageSource)GetValue(IconSelectedProperty);
            set => SetValue(IconSelectedProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(TabViewItem), false,
                                    propertyChanged: OnIsSelectedChanged);

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        static async void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TabViewItem tabViewItem)
            {
                tabViewItem.UpdateCurrent();
                await tabViewItem.UpdateTabAnimationAsync();
            }
        }

        public double TabWidth
        {
            get => (double)GetValue(TabWidthProperty);
            set => SetValue(TabWidthProperty, value);
        }

        public static readonly BindableProperty TabWidthProperty =
            BindableProperty.Create(nameof(TabWidth), typeof(double), typeof(TabViewItem), -1d);

        public static readonly BindableProperty TabAnimationProperty =
            BindableProperty.Create(nameof(TabAnimation), typeof(ITabViewItemAnimation), typeof(TabViewItem));

        public ITabViewItemAnimation TabAnimation
        {
            get => (ITabViewItemAnimation)GetValue(TabAnimationProperty);
            set => SetValue(TabAnimationProperty, value);
        }

        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(TabViewItem));

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        static void OnTabViewItemPropertyChanged(BindableObject bindable, object oldValue, object newValue) =>
            (bindable as TabViewItem)?.UpdateCurrent();

        static readonly BindablePropertyKey CurrentTextColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(CurrentTextColor), typeof(Color), typeof(TabViewItem), Color.Default);

        public static readonly BindableProperty CurrentTextColorProperty = CurrentTextColorPropertyKey.BindableProperty;

        public Color CurrentTextColor
        {
            get => (Color)GetValue(CurrentTextColorProperty);
            private set => SetValue(CurrentTextColorPropertyKey, value);
        }

        internal static readonly BindablePropertyKey CurrentContentPropertyKey = BindableProperty.CreateReadOnly(nameof(CurrentContent), typeof(View), typeof(TabViewItem), null);

        public static readonly BindableProperty CurrentContentProperty = CurrentContentPropertyKey.BindableProperty;

        public View CurrentContent
        {
            get => (View)GetValue(CurrentContentProperty);
            private set => SetValue(CurrentContentPropertyKey, value);
        }

        internal static readonly BindablePropertyKey CurrentIconPropertyKey = BindableProperty.CreateReadOnly(nameof(CurrentIcon), typeof(ImageSource), typeof(TabViewItem), null);

        public static readonly BindableProperty CurrentIconProperty = CurrentIconPropertyKey.BindableProperty;

        public ImageSource CurrentIcon
        {
            get => (ImageSource)GetValue(CurrentIconProperty);
            private set => SetValue(CurrentIconPropertyKey, value);
        }

        public delegate void TabTappedEventHandler(object sender, TabTappedEventArgs e);

        public event TabTappedEventHandler TabTapped;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Renderer")
                UpdateCurrent();
        }

        internal void OnTabTapped(TabTappedEventArgs e)
        {
            if (IsEnabled)
            {
                var handler = TabTapped;
                handler?.Invoke(this, e);

                TapCommand?.Execute(null);
            }
        }

        internal void UpdateCurrentContent(bool isOnScreen = true)
        {
            this._isOnScreen = isOnScreen;
            var newCurrentContent = this._isOnScreen ? Content : null;

            if (newCurrentContent != CurrentContent)
                CurrentContent = newCurrentContent;
        }

        void UpdateCurrent()
        {
            CurrentTextColor = !IsSelected || TextColorSelected == Color.Default ? TextColor : TextColorSelected;
            CurrentIcon = !IsSelected || IconSelected == null ? Icon : IconSelected;

            UpdateCurrentContent();
            ApplyIsSelectedState();
        }

        async Task UpdateTabAnimationAsync()
        {
            if (TabAnimation == null)
                return;

            var tabViewItemChildren = this.GetChildren();

            if (tabViewItemChildren.Count == 0 || !(tabViewItemChildren[0] is View view))
                return;

            if (IsSelected)
                await TabAnimation.OnSelected(view);
            else
                await TabAnimation.OnDeSelected(view);
        }

        void ApplyIsSelectedState() =>
            VisualStateManager.GoToState(this, IsSelected ? SelectedVisualState : UnselectedVisualState);
    }
}
