#nullable enable

using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.Commands
{
    /// <summary>
    /// Implementation of an Async Command
    /// </summary>
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Xamarin.Forms.WeakEventManager _weakEventManager = new Xamarin.Forms.WeakEventManager();
        private readonly Func<object, bool>?            canExecute;
        private readonly bool                           continueOnCapturedContext;
        private readonly Func<Task>                     execute;
        private readonly Action<Exception>?             onException;

        /// <summary>
        /// Create a new AsyncCommand
        /// </summary>
        /// <param name="execute">Function to execute</param>
        /// <param name="canExecute">Function to call to determine if it can be executed</param>
        /// <param name="onException">Action callback when an exception occurs</param>
        /// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
        public AsyncCommand(Func<Task>          execute,
                            Func<object, bool>? canExecute                = null,
                            Action<Exception>?  onException               = null,
                            bool                continueOnCapturedContext = false)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
            this.onException = onException;
            this.continueOnCapturedContext = continueOnCapturedContext;
        }

        /// <summary>
        /// Event triggered when Can Execute changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => _weakEventManager.AddEventHandler(value);
            remove => _weakEventManager.RemoveEventHandler(value);
        }

        /// <summary>
        /// Invoke the CanExecute method and return if it can be executed.
        /// </summary>
        /// <param name="parameter">Parameter to pass to CanExecute.</param>
        /// <returns>If it can be executed.</returns>
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Execute the command async.
        /// </summary>
        /// <returns>Task of action being executed that can be awaited.</returns>
        public Task ExecuteAsync() => execute();

        /// <summary>
        /// Raise a CanExecute change event.
        /// </summary>
        public void RaiseCanExecuteChanged() =>
            _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

        #region Explicit implementations

        void ICommand.Execute(object parameter) =>
            ExecuteAsync().SafeFireAndForget(onException, continueOnCapturedContext);

        #endregion
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        private readonly Xamarin.Forms.WeakEventManager _weakEventManager = new Xamarin.Forms.WeakEventManager();
        private readonly Func<object, bool>?            canExecute;
        private readonly bool                           continueOnCapturedContext;
        private readonly Func<T, Task>                  execute;
        private readonly Action<Exception>?             onException;

        /// <summary>
        /// Create a new AsyncCommand
        /// </summary>
        /// <param name="execute">Function to execute</param>
        /// <param name="canExecute">Function to call to determine if it can be executed</param>
        /// <param name="onException">Action callback when an exception occurs</param>
        /// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
        public AsyncCommand(Func<T, Task>       execute,
                            Func<object, bool>? canExecute                = null,
                            Action<Exception>?  onException               = null,
                            bool                continueOnCapturedContext = false)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
            this.onException = onException;
            this.continueOnCapturedContext = continueOnCapturedContext;
        }

        /// <summary>
        /// Event triggered when Can Execute changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => _weakEventManager.AddEventHandler(value);
            remove => _weakEventManager.RemoveEventHandler(value);
        }

        /// <summary>
        /// Invoke the CanExecute method and return if it can be executed.
        /// </summary>
        /// <param name="parameter">Parameter to pass to CanExecute.</param>
        /// <returns>If it can be executed</returns>
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Execute the command async.
        /// </summary>
        /// <param name="parameter">Parameter to pass</param>
        /// <returns>Task that is executing and can be awaited.</returns>
        public Task ExecuteAsync(T parameter) => execute(parameter);

        /// <summary>
        /// Raise a CanExecute change event.
        /// </summary>
        public void RaiseCanExecuteChanged() =>
            _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

        #region Explicit implementations

        void ICommand.Execute(object parameter)
        {
            if (CommandUtils.IsValidCommandParameter<T>(parameter))
            {
                ExecuteAsync((T)parameter).SafeFireAndForget(onException, continueOnCapturedContext);
            }
        }

        #endregion
    }
}