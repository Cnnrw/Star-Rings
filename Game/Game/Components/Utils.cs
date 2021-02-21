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
        /// Task extension to add a timeout.
        /// </summary>
        /// <returns>The task with timeout.</returns>
        /// <param name="task">Task.</param>
        /// <param name="timeoutInMilliseconds">Timeout duration in Milliseconds.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private static async Task<T> WithTimeout<T>(this Task<T> task, int timeoutInMilliseconds)
        {
            var retTask = await Task.WhenAny(task, Task.Delay(timeoutInMilliseconds))
                                    .ConfigureAwait(false);

            #pragma warning disable CS8603 // Possible null reference return.
            return retTask is Task<T> ? task.Result : default;
            #pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Task extension to add a timeout.
        /// </summary>
        /// <returns>The task with timeout.</returns>
        /// <param name="task">Task.</param>
        /// <param name="timeout">Timeout Duration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout) =>
            WithTimeout(task, (int)timeout.TotalMilliseconds);

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