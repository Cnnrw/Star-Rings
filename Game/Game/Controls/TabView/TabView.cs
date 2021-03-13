using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Game.Controls.TabView
{
    [Preserve(AllMembers = true)]
    [ContentProperty(nameof(TabItems))]
    public class TabView : ContentView, IDisposable
    {

        public delegate void TabSelectionChangedEventHandler(object sender, TabSelectionChangedEventArgs e);

        public delegate void TabViewScrolledEventHandler(object sender, ItemsViewScrolledEventArgs e);

        const uint TabIndicatorAnimationDuration = 100;

        public static readonly BindableProperty TabItemsSourceProperty =
            BindableProperty.Create(nameof(TabItemsSource), typeof(IList), typeof(TabView), null,
                                    propertyChanged: OnTabItemsSourceChanged);

        public static readonly BindableProperty TabViewItemDataTemplateProperty =
            BindableProperty.Create(nameof(TabViewItemDataTemplate), typeof(DataTemplate), typeof(TabView));

        public static readonly BindableProperty TabContentDataTemplateProperty =
            BindableProperty.Create(nameof(TabContentDataTemplate), typeof(DataTemplate), typeof(TabView));

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(TabView), -1, BindingMode.TwoWay,
                                    propertyChanged: OnSelectedIndexChanged);

        public static readonly BindableProperty TabStripPlacementProperty =
            BindableProperty.Create(nameof(TabStripPlacement), typeof(TabStripPlacement), typeof(TabView), TabStripPlacement.Top,
                                    propertyChanged: OnTabStripPlacementChanged);

        public static readonly BindableProperty TabStripBackgroundColorProperty =
            BindableProperty.Create(nameof(TabStripBackgroundColor), typeof(Color), typeof(TabView), Color.Default,
                                    propertyChanged: OnTabStripBackgroundColorChanged);

        public static readonly BindableProperty TabStripBackgroundViewProperty =
            BindableProperty.Create(nameof(TabStripBackgroundColor), typeof(View), typeof(TabView), null,
                                    propertyChanged: OnTabStripBackgroundViewChanged);

        public static readonly BindableProperty TabStripBorderColorProperty =
            BindableProperty.Create(nameof(TabStripBorderColor), typeof(Color), typeof(TabView), Color.Default,
                                    propertyChanged: OnTabStripBorderColorChanged);

        public static readonly BindableProperty TabContentBackgroundColorProperty =
            BindableProperty.Create(nameof(TabContentBackgroundColor), typeof(Color), typeof(TabView), Color.Default,
                                    propertyChanged: OnTabContentBackgroundColorChanged);

        public static readonly BindableProperty TabStripHeightProperty =
            BindableProperty.Create(nameof(TabStripHeight), typeof(double), typeof(TabView), 48d,
                                    propertyChanged: OnTabStripHeightChanged);

        public static readonly BindableProperty IsTabStripVisibleProperty =
            BindableProperty.Create(nameof(IsTabStripVisible), typeof(bool), typeof(TabView), true,
                                    propertyChanged: OnIsTabStripVisibleChanged);

        public static readonly BindableProperty TabContentHeightProperty =
            BindableProperty.Create(nameof(TabContentHeight), typeof(double), typeof(TabView), -1d,
                                    propertyChanged: OnTabContentHeightChanged);

        public static readonly BindableProperty TabIndicatorColorProperty =
            BindableProperty.Create(nameof(TabIndicatorColor), typeof(Color), typeof(TabView), Color.Default,
                                    propertyChanged: OnTabIndicatorColorChanged);

        public static readonly BindableProperty TabIndicatorHeightProperty =
            BindableProperty.Create(nameof(TabIndicatorHeight), typeof(double), typeof(TabView), 3d,
                                    propertyChanged: OnTabIndicatorHeightChanged);

        public static readonly BindableProperty TabIndicatorWidthProperty =
            BindableProperty.Create(nameof(TabIndicatorWidth), typeof(double), typeof(TabView), default(double),
                                    propertyChanged: OnTabIndicatorWidthChanged);

        public static readonly BindableProperty TabIndicatorViewProperty =
            BindableProperty.Create(nameof(TabIndicatorView), typeof(View), typeof(TabView), null,
                                    propertyChanged: OnTabIndicatorViewChanged);

        public static readonly BindableProperty TabIndicatorPlacementProperty =
            BindableProperty.Create(nameof(TabIndicatorPlacement), typeof(TabIndicatorPlacement), typeof(TabView), TabIndicatorPlacement.Bottom,
                                    propertyChanged: OnTabIndicatorPlacementChanged);

        public static readonly BindableProperty IsTabTransitionEnabledProperty =
            BindableProperty.Create(nameof(IsTabTransitionEnabled), typeof(bool), typeof(TabView), true,
                                    propertyChanged: OnIsTabTransitionEnabledChanged);

        public static readonly BindableProperty IsSwipeEnabledProperty =
            BindableProperty.Create(nameof(IsSwipeEnabled), typeof(bool), typeof(TabView), true,
                                    propertyChanged: OnIsSwipeEnabledChanged);

        readonly CarouselView _contentContainer;

        readonly List<double> _contentWidthCollection;

        readonly Grid                     _mainContainer;
        readonly Grid                     _tabStripBackground;
        readonly BoxView                  _tabStripBorder;
        readonly Grid                     _tabStripContainer;
        readonly ScrollView               _tabStripContainerScroll;
        readonly Grid                     _tabStripContent;
        readonly Grid                     _tabStripIndicator;
        ObservableCollection<TabViewItem> _contentTabItems;
        IList                             _tabItemsSource;

        public TabView()
        {
            TabItems = new ObservableCollection<TabViewItem>();

            _contentWidthCollection = new List<double>();

            BatchBegin();

            _tabStripBackground = new Grid
            {
                BackgroundColor = TabStripBackgroundColor,
                HeightRequest = TabStripHeight,
                VerticalOptions = LayoutOptions.Start
            };

            _tabStripBorder = new BoxView
            {
                Color = TabStripBorderColor,
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Start
            };

            _tabStripBackground.Children.Add(_tabStripBorder);

            _tabStripIndicator = new Grid
            {
                BackgroundColor = TabIndicatorColor,
                HeightRequest = TabIndicatorHeight,
                HorizontalOptions = LayoutOptions.Start
            };

            UpdateTabIndicatorPlacement(TabIndicatorPlacement);

            _tabStripContent = new Grid
            {
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                ColumnSpacing = 0
            };

            var tabStripContentContainer = new Grid
            {
                BackgroundColor = Color.Transparent,
                Children = {_tabStripIndicator, _tabStripContent},
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start
            };

            _tabStripContainerScroll = new ScrollView
            {
                BackgroundColor = Color.Transparent,
                Orientation = ScrollOrientation.Horizontal,
                Content = tabStripContentContainer,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start
            };

            if (Device.RuntimePlatform == Device.macOS || Device.RuntimePlatform == Device.UWP)
                _tabStripContainerScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Default;

            _tabStripContainer = new Grid
            {
                BackgroundColor = Color.Transparent,
                Children = {_tabStripBackground, _tabStripContainerScroll}
            };

            _contentContainer = new CarouselView
            {
                BackgroundColor = Color.Transparent,
                ItemsSource = TabItems.Where(t => t.Content != null),
                ItemTemplate = new DataTemplate(() =>
                {
                    var tabViewItemContent = new ContentView();
                    tabViewItemContent.SetBinding(ContentProperty, "CurrentContent");
                    return tabViewItemContent;
                }),
                IsSwipeEnabled = IsSwipeEnabled,
                IsScrollAnimated = IsTabTransitionEnabled,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Workaround to fix a Xamarin.Forms CarouselView issue that create a wrong 1px margin.
            if (Device.RuntimePlatform == Device.iOS)
                _contentContainer.Margin = new Thickness(-1, -1, 0, 0);

            _mainContainer = new Grid
            {
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {_contentContainer, _tabStripContainer},
                RowSpacing = 0
            };

            _mainContainer.RowDefinitions.Add(new RowDefinition {Height = TabStripHeight > 0 ? TabStripHeight : GridLength.Auto});
            _mainContainer.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            _mainContainer.RowDefinitions.Add(new RowDefinition {Height = GridLength.Star});

            Grid.SetRow(_tabStripContainer, 0);
            Grid.SetRowSpan(_tabStripContainer, 2);

            Grid.SetRow(_contentContainer, 1);
            Grid.SetRowSpan(_contentContainer, 2);

            Content = _mainContainer;

            BatchCommit();

            DisableLoop();
            UpdateIsEnabled();
        }

        public ObservableCollection<TabViewItem> TabItems { get; set; }

        public IList TabItemsSource
        {
            get => (IList)GetValue(TabItemsSourceProperty);
            set => SetValue(TabItemsSourceProperty, value);
        }

        public DataTemplate TabViewItemDataTemplate
        {
            get => (DataTemplate)GetValue(TabViewItemDataTemplateProperty);
            set => SetValue(TabViewItemDataTemplateProperty, value);
        }

        public DataTemplate TabContentDataTemplate
        {
            get => (DataTemplate)GetValue(TabContentDataTemplateProperty);
            set => SetValue(TabContentDataTemplateProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public TabStripPlacement TabStripPlacement
        {
            get => (TabStripPlacement)GetValue(TabStripPlacementProperty);
            set => SetValue(TabStripPlacementProperty, value);
        }

        public Color TabStripBackgroundColor
        {
            get => (Color)GetValue(TabStripBackgroundColorProperty);
            set => SetValue(TabStripBackgroundColorProperty, value);
        }

        public View TabStripBackgroundView
        {
            get => (View)GetValue(TabStripBackgroundViewProperty);
            set => SetValue(TabStripBackgroundViewProperty, value);
        }

        public Color TabStripBorderColor
        {
            get => (Color)GetValue(TabStripBorderColorProperty);
            set => SetValue(TabStripBorderColorProperty, value);
        }

        public Color TabContentBackgroundColor
        {
            get => (Color)GetValue(TabContentBackgroundColorProperty);
            set => SetValue(TabContentBackgroundColorProperty, value);
        }

        public double TabStripHeight
        {
            get => (double)GetValue(TabStripHeightProperty);
            set => SetValue(TabStripHeightProperty, value);
        }

        public bool IsTabStripVisible
        {
            get => (bool)GetValue(IsTabStripVisibleProperty);
            set => SetValue(IsTabStripVisibleProperty, value);
        }

        public double TabContentHeight
        {
            get => (double)GetValue(TabContentHeightProperty);
            set => SetValue(TabContentHeightProperty, value);
        }

        public Color TabIndicatorColor
        {
            get => (Color)GetValue(TabIndicatorColorProperty);
            set => SetValue(TabIndicatorColorProperty, value);
        }

        public double TabIndicatorHeight
        {
            get => (double)GetValue(TabIndicatorHeightProperty);
            set => SetValue(TabIndicatorHeightProperty, value);
        }

        public double TabIndicatorWidth
        {
            get => (double)GetValue(TabIndicatorWidthProperty);
            set => SetValue(TabIndicatorWidthProperty, value);
        }

        public View TabIndicatorView
        {
            get => (View)GetValue(TabIndicatorViewProperty);
            set => SetValue(TabIndicatorViewProperty, value);
        }

        public TabIndicatorPlacement TabIndicatorPlacement
        {
            get => (TabIndicatorPlacement)GetValue(TabIndicatorPlacementProperty);
            set => SetValue(TabIndicatorPlacementProperty, value);
        }

        public bool IsTabTransitionEnabled
        {
            get => (bool)GetValue(IsTabTransitionEnabledProperty);
            set => SetValue(IsTabTransitionEnabledProperty, value);
        }

        public bool IsSwipeEnabled
        {
            get => (bool)GetValue(IsSwipeEnabledProperty);
            set => SetValue(IsSwipeEnabledProperty, value);
        }

        public void Dispose()
        {
            if (_contentContainer != null)
            {
                _contentContainer.PropertyChanged -= OnContentContainerPropertyChanged;
                _contentContainer.Scrolled -= OnContentContainerScrolled;
            }

            if (_tabItemsSource is INotifyCollectionChanged notifyTabItemsSource)
                notifyTabItemsSource.CollectionChanged -= OnTabItemsSourceCollectionChanged;

            if (TabItems != null)
                TabItems.CollectionChanged -= OnTabItemsCollectionChanged;
        }

        void DisableLoop()
        {
            // If TabView is used with Xamarin.Forms >= 5.0, the default value of the CarouselView Loop property is true,
            // whereas in TabView we are not yet ready to support it. Access the property and disable the loop.
            var loopProperty = _contentContainer.GetType().GetProperty("Loop");

            if (loopProperty != null && loopProperty.CanWrite)
                loopProperty.SetValue(_contentContainer, false, null);
        }

        static void OnTabItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabItemsSource();
        }

        static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TabView tabView && tabView.TabItems != null)
            {
                var selectedIndex = (int)newValue;

                if (selectedIndex < 0) return;
                if ((int)oldValue != selectedIndex)
                    tabView.UpdateSelectedIndex(selectedIndex);
            }
        }

        static void OnTabStripPlacementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabStripPlacement((TabStripPlacement)newValue);
        }

        static void OnTabStripBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabStripBackgroundColor((Color)newValue);
        }

        static void OnTabStripBackgroundViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabStripBackgroundView((View)newValue);
        }

        static void OnTabStripBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabStripBorderColor((Color)newValue);
        }

        static void OnTabContentBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabContentBackgroundColor((Color)newValue);
        }

        static void OnTabStripHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabStripHeight((double)newValue);
        }

        static void OnIsTabStripVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateIsTabStripVisible((bool)newValue);
        }

        static void OnTabContentHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabContentHeight((double)newValue);
        }

        static void OnTabIndicatorColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabIndicatorColor((Color)newValue);
        }

        static void OnTabIndicatorHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabIndicatorHeight((double)newValue);
        }

        static void OnTabIndicatorWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabIndicatorWidth((double)newValue);
        }

        static void OnTabIndicatorViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabIndicatorView((View)newValue);
        }

        static void OnTabIndicatorPlacementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateTabIndicatorPlacement((TabIndicatorPlacement)newValue);
        }

        static void OnIsTabTransitionEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateIsTabTransitionEnabled((bool)newValue);
        }

        static void OnIsSwipeEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabView)?.UpdateIsSwipeEnabled((bool)newValue);
        }

        public event TabSelectionChangedEventHandler SelectionChanged;

        public event TabViewScrolledEventHandler Scrolled;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsEnabledProperty.PropertyName)
                UpdateIsEnabled();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (TabItems == null || TabItems.Count == 0)
                return;

            foreach (var tabViewItem in TabItems)
                UpdateTabViewItemBindingContext(tabViewItem);
        }

        void OnTabViewItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var tabViewItem = (TabViewItem)sender;

            if (e.PropertyName == TabViewItem.TabWidthProperty.PropertyName)
                UpdateTabViewItemTabWidth(tabViewItem);
        }

        void OnTabItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (TabViewItem oldItem in e.OldItems)
                    ClearTabViewItem(oldItem);

            if (e.NewItems != null)
                foreach (TabViewItem newTabViewItem in e.NewItems)
                    AddTabViewItem(newTabViewItem, TabItems.IndexOf(newTabViewItem));
        }

        void OnContentContainerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CarouselView.ItemsSource) ||
                e.PropertyName == nameof(CarouselView.VisibleViews))
            {
                var items = _contentContainer.ItemsSource;

                UpdateItemsSource(items);
            }
            else if (e.PropertyName == nameof(CarouselView.Position))
            {
                var selectedIndex = _contentContainer.Position;
                if (SelectedIndex != selectedIndex)
                    UpdateSelectedIndex(selectedIndex, true);
            }
        }

        void OnContentContainerScrolled(object sender, ItemsViewScrolledEventArgs args)
        {
            for (var i = 0; i < TabItems.Count; i++)
                TabItems[i].UpdateCurrentContent();

            UpdateTabIndicatorPosition(args);

            OnTabViewScrolled(args);
        }

        void ClearTabStrip()
        {
            foreach (var tabViewItem in TabItems)
                ClearTabViewItem(tabViewItem);

            if (_tabStripContent.Children.Count > 0)
                _tabStripContent.Children.Clear();

            _tabStripContent.ColumnDefinitions.Clear();

            var hasItems = TabItems.Count > 0 || TabItemsSource.Count > 0;
            _tabStripContainer.IsVisible = hasItems;
        }

        void ClearTabViewItem(View tabViewItem)
        {
            tabViewItem.PropertyChanged -= OnTabViewItemPropertyChanged;
            _tabStripContent.Children.Remove(tabViewItem);
        }

        void AddTabViewItem(TemplatedView tabViewItem, int index = -1)
        {
            tabViewItem.PropertyChanged -= OnTabViewItemPropertyChanged;
            tabViewItem.PropertyChanged += OnTabViewItemPropertyChanged;

            tabViewItem.ControlTemplate ??= Device.RuntimePlatform switch
                                            {
                                                Device.Android => new ControlTemplate(typeof(MaterialTabViewItemTemplate)),
                                                Device.iOS     => new ControlTemplate(typeof(CupertinoTabViewItemTemplate)),
                                                Device.UWP     => new ControlTemplate(typeof(WindowsTabViewItemTemplate)),
                                                _              => new ControlTemplate(typeof(MaterialTabViewItemTemplate))
                                            };

            AddSelectionTapRecognizer(tabViewItem);

            AddTabViewItemToTabStrip(tabViewItem, index);

            UpdateTabContentSize();
            UpdateTabStripSize();

            if (SelectedIndex != 0)
                UpdateSelectedIndex(0);
        }

        void UpdateTabStripSize()
        {
            var tabStripSize = _tabStripContent.Measure(double.PositiveInfinity, double.PositiveInfinity, MeasureFlags.IncludeMargins);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_tabStripContainer.HeightRequest != tabStripSize.Request.Height)
                _tabStripContainer.HeightRequest = tabStripSize.Request.Height;
        }

        void UpdateTabContentSize()
        {
            var items = _contentContainer.ItemsSource;

            var count = 0;

            var enumerator = items.GetEnumerator();

            while (enumerator.MoveNext())
                count++;

            VerticalOptions = count != 0 ? LayoutOptions.FillAndExpand : LayoutOptions.Start;
            _mainContainer.HeightRequest = count != 0 ? TabContentHeight + TabStripHeight : TabStripHeight;
            UpdateTabContentHeight(count != 0 ? TabContentHeight : 0);
        }

        void AddTabViewItemFromTemplate(object item, int index = -1)
        {
            AddTabViewItemFromTemplateToTabStrip(item, index);
        }

        void UpdateTabViewItemBindingContext(TabViewItem tabViewItem)
        {
            if (tabViewItem == null || tabViewItem.Content == null)
                return;

            tabViewItem.Content.BindingContext = BindingContext;
        }

        void AddSelectionTapRecognizer(View view)
        {
            var tapRecognizer = new TapGestureRecognizer();

            tapRecognizer.Tapped += (sender, args) =>
            {
                var capturedIndex = _tabStripContent.Children.IndexOf((View)sender);

                if (view is TabViewItem tabViewItem)
                {
                    var tabTappedEventArgs = new TabTappedEventArgs(capturedIndex);
                    tabViewItem.OnTabTapped(tabTappedEventArgs);
                }

                if (CanUpdateSelectedIndex(capturedIndex))
                    if (SelectedIndex != capturedIndex)
                        UpdateSelectedIndex(capturedIndex);
            };

            view.GestureRecognizers.Add(tapRecognizer);
        }

        void AddTabViewItemToTabStrip(View item, int index = -1)
        {
            var tabViewItemSizeRequest = item.Measure(double.PositiveInfinity, double.PositiveInfinity, MeasureFlags.IncludeMargins);

            if (tabViewItemSizeRequest.Request.Height < TabStripHeight)
            {
                item.HeightRequest = TabStripHeight;
                item.VerticalOptions = TabStripPlacement == TabStripPlacement.Top ? LayoutOptions.Start : LayoutOptions.End;
            }

            _tabStripContent.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = item is TabViewItem tabViewItem && tabViewItem.TabWidth > 0 ? tabViewItem.TabWidth : GridLength.Star
            });

            if (index >= 0)
            {
                _tabStripContent.Children.Insert(index, item);

                for (var i = index; i < _tabStripContent.Children.Count; i++)
                    Grid.SetColumn(_tabStripContent.Children[i], i);
            }
            else
            {
                _tabStripContent.Children.Add(item);
                var count = _tabStripContent.Children.Count - 1;
                item.SetValue(Grid.ColumnProperty, count);
            }

            UpdateTabViewItemTabWidth(item as TabViewItem);
        }

        void AddTabViewItemFromTemplateToTabStrip(object item, int index = -1)
        {
            var view = !(TabViewItemDataTemplate is DataTemplateSelector tabItemDataTemplate)
                           ? (View)TabViewItemDataTemplate.CreateContent()
                           : (View)tabItemDataTemplate.SelectTemplate(item, this).CreateContent();

            view.BindingContext = item;

            view.Effects.Add(new VisualFeedbackEffect());

            AddSelectionTapRecognizer(view);
            AddTabViewItemToTabStrip(view, index);
        }

        void UpdateIsEnabled()
        {
            if (IsEnabled)
            {
                _contentContainer.PropertyChanged += OnContentContainerPropertyChanged;
                _contentContainer.Scrolled += OnContentContainerScrolled;

                TabItems.CollectionChanged += OnTabItemsCollectionChanged;
            }
            else
            {
                _contentContainer.PropertyChanged -= OnContentContainerPropertyChanged;
                _contentContainer.Scrolled -= OnContentContainerScrolled;

                TabItems.CollectionChanged -= OnTabItemsCollectionChanged;
            }

            _tabStripContent.IsEnabled = IsEnabled;
            _contentContainer.IsEnabled = IsEnabled;
        }

        void UpdateTabViewItemTabWidth(TabViewItem tabViewItem)
        {
            if (tabViewItem == null)
                return;

            var index = _tabStripContent.Children.IndexOf(tabViewItem);
            var colummns = _tabStripContent.ColumnDefinitions;

            ColumnDefinition column = null;

            if (index < colummns.Count)
                column = colummns[index];

            if (column == null)
                return;

            column.Width = tabViewItem.TabWidth > 0 ? tabViewItem.TabWidth : GridLength.Star;
            UpdateTabIndicatorPosition(SelectedIndex);
        }

        void UpdateTabItemsSource()
        {
            if (TabItemsSource == null || TabViewItemDataTemplate == null)
                return;

            if (_tabItemsSource is INotifyCollectionChanged oldnNotifyTabItemsSource)
                oldnNotifyTabItemsSource.CollectionChanged -= OnTabItemsSourceCollectionChanged;

            _tabItemsSource = TabItemsSource;

            if (_tabItemsSource is INotifyCollectionChanged newNotifyTabItemsSource)
                newNotifyTabItemsSource.CollectionChanged += OnTabItemsSourceCollectionChanged;

            ClearTabStrip();

            _contentContainer.ItemTemplate = TabContentDataTemplate;
            _contentContainer.ItemsSource = TabItemsSource;

            foreach (var item in TabItemsSource)
                AddTabViewItemFromTemplate(item);

            UpdateTabContentSize();
            UpdateTabStripSize();
            if (SelectedIndex != 0)
                UpdateSelectedIndex(0);
        }

        void OnTabItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateTabItemsSource();
        }

        void UpdateItemsSource(IEnumerable items)
        {
            _contentWidthCollection.Clear();

            if (_contentContainer.VisibleViews.Count == 0)
                return;

            var first = _contentContainer.VisibleViews.FirstOrDefault();
            if (first != null)
            {
                var contentWidth = first.Width;
                var tabItemsCount = items.Cast<object>().Count();

                for (var i = 0; i < tabItemsCount; i++)
                    _contentWidthCollection.Add(contentWidth * i);
            }
        }

        bool CanUpdateSelectedIndex(int selectedIndex)
        {
            if (TabItems == null || TabItems.Count == 0)
                return true;

            var tabItem = TabItems[selectedIndex];

            if (tabItem != null && tabItem.Content == null)
            {
                var itemsCount = TabItems.Count;
                var contentItemsCount = TabItems.Count(t => t.Content == null);

                return itemsCount == contentItemsCount;
            }

            return true;
        }

        void UpdateSelectedIndex(int position, bool hasCurrentItem = false)
        {
            if (position < 0)
                return;
            var oldposition = SelectedIndex;

            var newPosition = position;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_contentTabItems == null || _contentTabItems.Count != TabItems.Count)
                    _contentTabItems = new ObservableCollection<TabViewItem>(TabItems.Where(t => t.Content != null));

                var contentIndex = position;
                var tabStripIndex = position;

                if (TabItems.Count > 0)
                {
                    TabViewItem currentItem = null;

                    if (hasCurrentItem)
                        currentItem = (TabViewItem)_contentContainer.CurrentItem;

                    var tabViewItem = TabItems[position];

                    contentIndex = _contentTabItems.IndexOf(currentItem ?? tabViewItem);
                    tabStripIndex = TabItems.IndexOf(currentItem ?? tabViewItem);

                    position = tabStripIndex;

                    for (var index = 0; index < TabItems.Count; index++)
                        if (index == position)
                            TabItems[position].IsSelected = true;
                        else
                            TabItems[index].IsSelected = false;

                    var currentTabItem = TabItems[position];
                    currentTabItem.SizeChanged += OnCurrentTabItemSizeChanged;
                    UpdateTabIndicatorPosition(currentTabItem);
                }
                else
                {
                    UpdateTabIndicatorPosition(position);
                }

                if (contentIndex >= 0)
                    _contentContainer.Position = contentIndex;

                if (_tabStripContent.Children.Count > 0)
                    await _tabStripContainerScroll.ScrollToAsync(_tabStripContent.Children[tabStripIndex], ScrollToPosition.MakeVisible, false);

                SelectedIndex = position;
                if (oldposition != SelectedIndex)
                {
                    var selectionChangedArgs = new TabSelectionChangedEventArgs
                    {
                        NewPosition = newPosition,
                        OldPosition = SelectedIndex
                    };

                    OnTabSelectionChanged(selectionChangedArgs);
                }
            });
        }

        void OnCurrentTabItemSizeChanged(object sender, EventArgs e)
        {
            var currentTabItem = (View)sender;
            UpdateTabIndicatorWidth(TabIndicatorWidth > 0 ? TabIndicatorWidth : currentTabItem.Width);
            UpdateTabIndicatorPosition(currentTabItem);
            currentTabItem.SizeChanged -= OnCurrentTabItemSizeChanged;
        }

        void UpdateTabStripPlacement(TabStripPlacement tabStripPlacement)
        {
            if (tabStripPlacement == TabStripPlacement.Top)
            {
                _tabStripBackground.VerticalOptions = LayoutOptions.Start;

                Grid.SetRow(_tabStripContainer, 0);
                Grid.SetRowSpan(_tabStripContainer, 2);

                _mainContainer.RowDefinitions[0].Height = TabStripHeight > 0 ? TabStripHeight : GridLength.Auto;
                _mainContainer.RowDefinitions[1].Height = GridLength.Auto;
                _mainContainer.RowDefinitions[2].Height = GridLength.Star;

                _tabStripBorder.VerticalOptions = LayoutOptions.End;
            }

            if (tabStripPlacement == TabStripPlacement.Bottom)
            {
                _tabStripBackground.VerticalOptions = LayoutOptions.End;

                Grid.SetRow(_tabStripContainer, 1);
                Grid.SetRowSpan(_tabStripContainer, 2);

                _mainContainer.RowDefinitions[0].Height = GridLength.Star;
                _mainContainer.RowDefinitions[1].Height = GridLength.Auto;
                _mainContainer.RowDefinitions[2].Height = TabStripHeight > 0 ? TabStripHeight : GridLength.Auto;

                _tabStripBorder.VerticalOptions = LayoutOptions.Start;
            }

            UpdateTabContentLayout();
            UpdateTabIndicatorMargin();
        }

        void UpdateTabContentLayout()
        {
            if (_tabStripContainer.IsVisible)
            {
                if (TabStripPlacement == TabStripPlacement.Top)
                {
                    Grid.SetRow(_contentContainer, 1);
                    Grid.SetRowSpan(_contentContainer, 2);
                }
                else
                {
                    Grid.SetRow(_contentContainer, 0);
                    Grid.SetRowSpan(_contentContainer, 2);
                }
            }
            else
            {
                Grid.SetRow(_contentContainer, 0);
                Grid.SetRowSpan(_contentContainer, 3);
            }

            if (TabStripBackgroundView != null)
            {
                var tabStripBackgroundViewHasCornerRadius =
                    TabStripBackgroundView is IBorderElement borderElement && borderElement.CornerRadius != default ||
                    TabStripBackgroundView is BoxView boxView && boxView.CornerRadius != default;

                if (tabStripBackgroundViewHasCornerRadius)
                {
                    Grid.SetRow(_contentContainer, 0);
                    Grid.SetRowSpan(_contentContainer, 3);
                }
            }
        }

        void UpdateTabStripBackgroundColor(Color tabStripBackgroundColor)
        {
            _tabStripBackground.BackgroundColor = tabStripBackgroundColor;

            if (Device.RuntimePlatform == Device.macOS)
                _tabStripContainerScroll.BackgroundColor = tabStripBackgroundColor;
        }

        void UpdateTabStripBackgroundView(View tabStripBackgroundView)
        {
            if (tabStripBackgroundView != null)
                _tabStripBackground.Children.Add(tabStripBackgroundView);
            else
                _tabStripBackground.Children.Clear();

            UpdateTabContentLayout();
        }

        void UpdateTabStripBorderColor(Color tabStripBorderColor)
        {
            _tabStripBorder.Color = tabStripBorderColor;

            UpdateTabIndicatorMargin();
        }

        void UpdateTabIndicatorMargin()
        {
            if (TabStripBorderColor == Color.Default)
                return;

            if (TabStripPlacement == TabStripPlacement.Top && TabIndicatorPlacement == TabIndicatorPlacement.Bottom)
                _tabStripIndicator.Margin = new Thickness(0, 0, 0, 1);

            if (TabStripPlacement == TabStripPlacement.Bottom && TabIndicatorPlacement == TabIndicatorPlacement.Top)
                _tabStripIndicator.Margin = new Thickness(0, 1, 0, 0);
        }

        void UpdateTabContentBackgroundColor(Color tabContentBackgroundColor)
        {
            _contentContainer.BackgroundColor = tabContentBackgroundColor;
        }

        void UpdateTabStripHeight(double tabStripHeight)
        {
            _tabStripBackground.HeightRequest = tabStripHeight;
        }

        void UpdateIsTabStripVisible(bool isTabStripVisible)
        {
            _tabStripContainer.IsVisible = isTabStripVisible;

            UpdateTabContentLayout();
        }

        void UpdateTabContentHeight(double tabContentHeight)
        {
            _contentContainer.HeightRequest = tabContentHeight;
        }

        void UpdateTabIndicatorColor(Color tabIndicatorColor)
        {
            _tabStripIndicator.BackgroundColor = tabIndicatorColor;
        }

        void UpdateTabIndicatorHeight(double tabIndicatorHeight)
        {
            _tabStripIndicator.HeightRequest = tabIndicatorHeight;
        }

        void UpdateTabIndicatorWidth(double tabIndicatorWidth)
        {
            _tabStripIndicator.WidthRequest = tabIndicatorWidth;
        }

        void UpdateTabIndicatorView(View tabIndicatorView)
        {
            if (tabIndicatorView != null)
                _tabStripIndicator.Children.Add(tabIndicatorView);
            else
                _tabStripIndicator.Children.Clear();
        }

        void UpdateTabIndicatorPlacement(TabIndicatorPlacement tabIndicatorPlacement)
        {
            _tabStripIndicator.VerticalOptions = tabIndicatorPlacement switch
                                                 {
                                                     TabIndicatorPlacement.Top    => LayoutOptions.Start,
                                                     TabIndicatorPlacement.Center => LayoutOptions.Center,
                                                     _                            => LayoutOptions.End
                                                 };

            UpdateTabIndicatorMargin();
        }

        void UpdateIsSwipeEnabled(bool isSwipeEnabled)
        {
            _contentContainer.IsSwipeEnabled = isSwipeEnabled;
        }

        void UpdateIsTabTransitionEnabled(bool isTabTransitionEnabled)
        {
            _contentContainer.IsScrollAnimated = isTabTransitionEnabled;
        }

        void UpdateTabIndicatorPosition(int tabViewItemIndex)
        {
            if (_tabStripContent == null || _tabStripContent.Children.Count == 0 || tabViewItemIndex == -1)
                return;

            var currentTabViewItem = _tabStripContent.Children[tabViewItemIndex];

            if (currentTabViewItem.Width <= 0)
                currentTabViewItem.SizeChanged += OnCurrentTabItemSizeChanged;
            else
                UpdateTabIndicatorWidth(currentTabViewItem.Width);

            UpdateTabIndicatorPosition(currentTabViewItem);
        }

        void UpdateTabIndicatorPosition(ItemsViewScrolledEventArgs args)
        {
            if (args.HorizontalOffset == 0)
            {
                UpdateTabIndicatorPosition(SelectedIndex);
                return;
            }

            if (_tabStripContent == null || TabItems.Count == 0)
                return;

            if (_contentWidthCollection.Count == 0)
                UpdateItemsSource(_contentContainer.ItemsSource);

            var offset = args.HorizontalOffset;
            var toRight = args.HorizontalDelta > 0;

            var nextIndex = toRight ? _contentWidthCollection.FindIndex(c => c > offset) : _contentWidthCollection.FindLastIndex(c => c < offset);
            var previousIndex = toRight ? nextIndex - 1 : nextIndex + 1;

            if (previousIndex < 0 || nextIndex < 0)
                return;

            var itemsCount = TabItems.Count;

            if (previousIndex >= 0 && previousIndex < itemsCount)
            {
                var currentTabViewItem = TabItems[previousIndex];
                var currentTabViewItemWidth = currentTabViewItem.Width;

                UpdateTabIndicatorWidth(currentTabViewItemWidth);

                var contentItemsCount = _contentWidthCollection.Count;

                if (previousIndex >= 0 && previousIndex < contentItemsCount)
                {
                    var progress = (offset - _contentWidthCollection[previousIndex]) / (_contentWidthCollection[nextIndex] - _contentWidthCollection[previousIndex]);
                    var position = toRight ? currentTabViewItem.X + currentTabViewItemWidth * progress : currentTabViewItem.X - currentTabViewItemWidth * progress;

                    _tabStripIndicator.TranslateTo(position, 0, TabIndicatorAnimationDuration, Easing.Linear);
                }
            }
        }

        void UpdateTabIndicatorPosition(View currentTabViewItem)
        {
            var width = TabIndicatorWidth > 0 ? currentTabViewItem.Width - _tabStripIndicator.Width : 0;
            var position = currentTabViewItem.X + width / 2;
            _tabStripIndicator.TranslateTo(position, 0, TabIndicatorAnimationDuration, Easing.Linear);
        }

        internal virtual void OnTabSelectionChanged(TabSelectionChangedEventArgs e)
        {
            var handler = SelectionChanged;
            handler?.Invoke(this, e);
        }

        internal virtual void OnTabViewScrolled(ItemsViewScrolledEventArgs e)
        {
            var handler = Scrolled;
            handler?.Invoke(this, e);
        }
    }
}
