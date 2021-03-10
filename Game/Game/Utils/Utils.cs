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
        /// <param name="task">Task.</param>
        /// <param name="onException">If an exception is thrown in the Task, <c>onException</c> will execute. If onException is null, the exception will be re-thrown</param>
        /// <param name="continueOnCapturedContext">
        ///     If set to <c>true</c>, continue on captured context; this will ensure that the Synchronization context returns to the calling thread.
        ///     If set to <c>false</c>, continue on a different context; this will allow the Synchronization context to continue on a different thread.
        /// </param>
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
