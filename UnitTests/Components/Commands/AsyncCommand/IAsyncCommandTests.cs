using System.Threading.Tasks;

using Game.Commands;

using NUnit.Framework;

namespace UnitTests.Components
{
    [TestFixture]
    public class IAsyncCommandTests
    {
        [Test]
        public void IAsyncCommand_CanRaiseCanExecuteChanged()
        {
            IAsyncCommand command = new AsyncCommand(() => Task.CompletedTask);
            command.RaiseCanExecuteChanged();
        }

        [Test]
        public void IAsyncCommandT_CanRaiseCanExecuteChanged()
        {
            IAsyncCommand<string> command = new AsyncCommand<string>(sender => Task.CompletedTask);
            command.RaiseCanExecuteChanged();
        }
    }
}