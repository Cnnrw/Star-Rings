using System;
using System.Threading.Tasks;

namespace Game.Services
{
    /// <summary>
    /// Source: https://mallibone.com/post/a-simple-navigation-service-for-xamarinforms"
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///     <c>key</c> of the topmost page in the <c>NavigationStack.</c>
        /// </summary>
        string CurrentPageKey { get; }

        /// <summary>
        ///     Initializes the navigation service by registering all the views
        ///     with a corresponding key
        /// </summary>
        /// <param name="pageKey">Key used to look up the page in the page registry.</param>
        /// <param name="pageType">Type of page being added to the registry</param>
        void Configure(string pageKey, Type pageType);

        /// <summary>
        ///     <c>GoBack</c> checks what back navigation is intended for the current
        ///     page. A root page in a navigation stack is either the root page of the
        ///     application or a root page in a modal navigation stack. In either case
        ///     it will choose the appropriate back navigation method.
        ///
        ///     If the page is not the root page it simply navigates back to the
        ///     previous page that was pushed onto the navigation stack.
        /// </summary>
        /// <returns></returns>
        Task GoBack();

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        Task NavigateModalAsync(string pageKey, bool animated = true);

        /// <summary>
        ///     Modal navigation leaves the current navigation stack and displays
        ///     a page on its own. If you want to navigate from a modal root page
        ///     via push to a child page the modal root page has to be in a
        ///     <c>NavigationPage</c> itself.
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        Task NavigateModalAsync(string pageKey, object parameter, bool animated = true);

        /// <summary>
        ///     Pushes a page onto the navigation stack.
        /// </summary>
        /// <param name="pageKey"><c>key</c> of the page you want to navigate to.</param>
        /// <param name="animated"><c>bool</c>: whether the page transition should be animated or not.</param>
        /// <returns></returns>
        Task NavigateAsync(string pageKey, bool animated = true);

        /// <summary>
        ///     Pushes a page onto the navigation stack with parameter.
        /// </summary>
        /// <param name="pageKey"><c>key</c> of the page you want to navigate to.</param>
        /// <param name="parameter">Parameter passed to the page getting added to the current navigation stack.</param>
        /// <param name="animated"><c>bool</c>: whether the page transition should be animated or not.</param>
        /// <returns></returns>
        Task NavigateAsync(string pageKey, object parameter, bool animated = true);
    }
}
