using System;
using System.Threading.Tasks;

namespace Game.Services.Navigation
{
    public interface INavigationService
    {
        /// <summary>
        ///
        /// </summary>
        string CurrentPageKey { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="pageType"></param>
        void Configure(string pageKey, Type pageType);

        /// <summary>
        ///
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
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        Task NavigateModalAsync(string pageKey, object parameter, bool animated = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        Task NavigateAsync(string pageKey, bool animated = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        /// <param name="animated"></param>
        /// <returns></returns>
        Task NavigateAsync(string pageKey, object parameter, bool animated = true);
    }
}
