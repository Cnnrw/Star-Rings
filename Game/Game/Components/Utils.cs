#nullable enable
using System;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Extension Utils
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Attempts to await on the task and catches exception.
        ///
        /// Async void is intentional here. This provides a way to call an
        /// async method from the constructor while communicating intent to
        /// fire and forget, and allow handling of exceptions.
        /// </summary>
        /// <param name="task">Task to execute</param>
        /// <param name="onException">What to do when method has an exception</param>
        /// <param name="continueOnCapturedContext">If the context should be captured.</param>
        public static async void SafeFireAndForget(this Task          task,
                                                   Action<Exception>? onException               = null,
                                                   bool               continueOnCapturedContext = false)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException.Invoke(ex);
            }
        }
    }
}
