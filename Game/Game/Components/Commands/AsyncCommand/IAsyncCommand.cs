using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.Commands
{
    /// <summary>
    /// Interface for async command
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Execute the command async.
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();

        /// <summary>
        /// Raise a CanExecute change event
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    public interface IAsyncCommand<T> : ICommand
    {
        /// <summary>
        /// Execute the command async.
        /// </summary>
        /// <param name="parameter">Parameter to pass to command</param>
        /// <returns>Task to be awaited on</returns>
        Task ExecuteAsync(T parameter);

        /// <summary>
        /// Raise a CanExecute change event.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}