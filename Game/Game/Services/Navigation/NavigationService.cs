using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Game.Services
{
    public class ViewNavigationService : INavigationService
    {
        readonly object                   _sync                = new object();
        readonly Dictionary<string, Type> _pagesByKey          = new Dictionary<string, Type>();
        readonly Stack<NavigationPage>    _navigationPageStack = new Stack<NavigationPage>();

        NavigationPage CurrentNavigationPage => _navigationPageStack.Peek();

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="pageType"></param>
        public void Configure(string pageKey, Type pageType)
        {
            lock (_sync)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                    _pagesByKey[pageKey] = pageType;
                else
                    _pagesByKey.Add(pageKey, pageType);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rootPageKey"></param>
        /// <returns></returns>
        public Page SetRootPage(string rootPageKey)
        {
            var rootPage = GetPage(rootPageKey);
            _navigationPageStack.Clear();

            var mainPage = new NavigationPage(rootPage);
            _navigationPageStack.Push(mainPage);

            return mainPage;
        }

        /// <summary>
        ///
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                lock (_sync)
                {
                    if (CurrentNavigationPage?.CurrentPage == null)
                        return null;

                    var pageType = CurrentNavigationPage.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                               ? _pagesByKey.First(p => p.Value == pageType).Key
                               : null;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task GoBack()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 1)
            {
                await CurrentNavigationPage.PopAsync();
                return;
            }

            if (_navigationPageStack.Count > 1)
            {
                _navigationPageStack.Pop();
                await CurrentNavigationPage.Navigation.PopModalAsync();
                return;
            }

            await CurrentNavigationPage.PopAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task NavigateModalAsync(string pageKey, bool animated = true) => await NavigateModalAsync(pageKey, null, animated);

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            NavigationPage.SetHasNavigationBar(page, false);

            var modalNavigationPage = new NavigationPage(page);
            await CurrentNavigationPage.Navigation.PushModalAsync(modalNavigationPage, animated);

            _navigationPageStack.Push(modalNavigationPage);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task NavigateAsync(string pageKey, bool animated = true) => await NavigateAsync(pageKey, null, animated);

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        public async Task NavigateAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            await CurrentNavigationPage.Navigation.PushAsync(page, animated);
        }

        private Page GetPage(string pageKey, object parameter = null)
        {
            lock (_sync)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                    throw new ArgumentException("No such page -> " +
                                                pageKey +
                                                " <- Did you forget to call NavigationService.Configure?");

                var type = _pagesByKey[pageKey];
                ConstructorInfo constructor;
                object[] parameters;

                if (parameter == null)
                {
                    constructor = type.GetTypeInfo()
                                      .DeclaredConstructors
                                      .FirstOrDefault(c => !c.GetParameters().Any());

                    parameters = new object[] { };
                }
                else
                {
                    constructor = type.GetTypeInfo()
                                      .DeclaredConstructors
                                      .FirstOrDefault(c =>
                                      {
                                          var p = c.GetParameters();
                                          return p.Length == 1 &&
                                                 p[0].ParameterType == parameter.GetType();
                                      });

                    parameters = new[] {parameter};
                }

                if (constructor == null)
                    throw new InvalidOperationException("No suitable constructor found for page -> " +
                                                        pageKey +
                                                        " <- ");

                var page = constructor.Invoke(parameters) as Page;
                return page;
            }
        }
    }
}
