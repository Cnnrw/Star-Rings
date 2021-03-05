using System;
using System.Diagnostics;

using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Game.UWP
{
    public sealed partial class MainPage
    {
        ThreadPoolTimer? _mTimer;

        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;

            LoadApplication(new Game.App());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Window.Current.SizeChanged += OnWindowSizeChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (_mTimer != null)
            {
                _mTimer.Cancel();
                _mTimer = null;
            }

            var period = TimeSpan.FromSeconds(1.0);
            _mTimer = ThreadPoolTimer.CreateTimer(async (source) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ApplicationView.GetForCurrentView().TryResizeView(new Size(320, 800));
                });
            }, period);
        }

        static void OnLoaded(object sender, RoutedEventArgs e)
        {
            var result = ApplicationView.GetForCurrentView().TryResizeView(new Size(500, 800));
            Debug.WriteLine("OnLoaded TryResizeView: " + result);
        }
    }
}
