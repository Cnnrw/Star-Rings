using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;

using Game.Commands;
using Game.Components;

using NExpect;

using NUnit.Framework;

using static NExpect.Expectations;

namespace UnitTests.Components
{
    [TestFixture]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class AsyncCommandTests
    {
        private ICommand refreshCommand;

        private ICommand RefreshCommand => refreshCommand ??= new AsyncCommand<bool>(t => ExecuteLoadCommand(true));
        private static async Task ExecuteLoadCommand(bool forceRefresh) => await Task.Delay(1000);

        protected event EventHandler TestEvent
        {
            add => TestWeakEventManager.AddEventHandler(value);
            remove => TestWeakEventManager.RemoveEventHandler(value);
        }

        private const int Delay = 500;
        private WeakEventManager TestWeakEventManager { get; } = new WeakEventManager();

        private static Task NoParameterTask() => Task.Delay(Delay);
        private static Task IntParameterTask(int delay) => Task.Delay(delay);
        private static Task StringParameterTask(string text) => Task.Delay(Delay);
        protected Task NoParameterImmediateNullReferenceExceptionTask() => throw new NullReferenceException();
        protected Task ParameterImmediateNullReferenceExceptionTask(int delay) => throw new NullReferenceException();

        protected async Task NoParameterDelayedNullReferenceExceptionTask()
        {
            await Task.Delay(Delay);
            throw new NullReferenceException();
        }

        protected async Task IntParameterDelayedNullReferenceExceptionTask(int delay)
        {
            await Task.Delay(delay);
            throw new NullReferenceException();
        }

        private static bool CanExecuteTrue(object    parameter)        => true;
        private static bool CanExecuteFalse(object   parameter)        => false;
        protected      bool CanExecuteDynamic(object booleanParameter) => (bool)booleanParameter;

        [Test]
        public void AsyncCommand_UsingICommand() =>
            // Arrange
            RefreshCommand.Execute(true);

        [Test]
        public void AsyncCommand_NullExecuteParameter() =>
            // Assert
            Expect(() => new AsyncCommand(null))
                .To.Throw<ArgumentNullException>();

        [Test]
        public void AsyncCommandT_NullExecuteParameter() =>
            // Assert
            Expect(() => new AsyncCommand<object>(null))
                .To.Throw<ArgumentNullException>();

        [Test]
        public async Task AsyncCommand_Execute_Async_Int_Parameter_Test()
        {
            // Arrange
            var command = new AsyncCommand<int>(IntParameterTask);

            // Act
            await command.ExecuteAsync(500);
            await command.ExecuteAsync(default);
        }

        [Test]
        public async Task AsyncCommand_Execute_Async_String_Parameter_Test()
        {
            // Arrange
            var command = new AsyncCommand<string>(StringParameterTask);

            // Act
            await command.ExecuteAsync("Hello");
            await command.ExecuteAsync(default);

            // Assert
        }

        [Test]
        public void AsyncCommand_Parameter_CanExecute_True_Test()
        {
            // Arrange
            var command = new AsyncCommand<int>(IntParameterTask, CanExecuteTrue);

            // Act

            // Assert
            Expect(command.CanExecute(null))
                .To.Be.True();
        }

        [Test]
        public void AsyncCommand_Parameter_CanExecute_False_Test()
        {
            // Arrange
            var command = new AsyncCommand<int>(IntParameterTask, CanExecuteFalse);

            // Act

            // Assert
            Expect(command.CanExecute(null))
                .To.Be.False();
        }

        [Test]
        public void AsyncCommand_No_Parameter_CanExecute_True_Test()
        {
            // Arrange
            var command = new AsyncCommand(NoParameterTask, CanExecuteTrue);

            // Act

            // Assert
            Expect(command.CanExecute(null)).To.Be.True();
        }

        [Test]
        public void AsyncCommand_No_Parameter_CanExecute_False_Test()
        {
            // Arrange
            var command = new AsyncCommand(NoParameterTask, CanExecuteFalse);

            // Act

            // Assert
            Expect(command.CanExecute(null)).To.Be.False();
        }

        [Test]
        public void AsyncCommand_CanExecute_Changed_Test()
        {
            // Arrange
            var canCommandExecute = false;
            var didCanExecuteChangeFire = false;

            var command = new AsyncCommand(NoParameterTask, commandCanExecute);
            command.CanExecuteChanged += handleCanExecuteChanged;

            bool commandCanExecute(object parameter)
            {
                return canCommandExecute;
            }

            void handleCanExecuteChanged(object sender, EventArgs e)
            {
                didCanExecuteChangeFire = true;
            }

            Expect(command.CanExecute(null))
                .To.Be.False();

            // Act
            canCommandExecute = true;

            // Assert
            Expect(command.CanExecute(null))
                .To.Be.True();
            Expect(didCanExecuteChangeFire)
                .To.Be.False();

            // Act
            command.RaiseCanExecuteChanged();

            // Assert
            Expect(didCanExecuteChangeFire)
                .To.Be.True();
            Expect(command.CanExecute(null))
                .To.Be.True();
        }
    }
}