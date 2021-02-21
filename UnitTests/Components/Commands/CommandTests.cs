using System;
using System.Diagnostics.CodeAnalysis;

using Game.Commands;
using Game.Exceptions;

using NExpect;

using NUnit.Framework;

using static NExpect.Expectations;

namespace UnitTests.Components.Commands
{
    [TestFixture]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class CommandTests
    {
        [Test]
        public void Constructor()
        {
            var cmd = new Command(() => { });

            Expect(cmd.CanExecute(null))
                .To.Be.True();
        }

        [Test]
        public void Throws_With_Null_Constructor() =>
            Expect(() => new Command((Action)null))
                .To.Throw<ArgumentNullException>();

        [Test]
        public void Throws_With_Null_Parameterized_Constructor() =>
            Expect(() => new Command((Action<object>)null))
                .To.Throw<ArgumentNullException>();

        [Test]
        public void Throws_With_Null_Execute_Valid_CanExecute() =>
            Expect(() => new Command(null, () => true))
                .To.Throw<ArgumentNullException>();

        [Test]
        public void Execute()
        {
            // Arrange
            var executed = false;
            var cmd = new Command(() => executed = true);

            // Act
            cmd.Execute(null);

            // Assert
            Expect(executed)
                .To.Be.True();
        }

        [Test]
        public void Execute_Parameterized()
        {
            // Arrange
            object executed = null;
            var cmd = new Command(o => executed = o);
            var expected = new object();

            // Act
            cmd.Execute(expected);

            // Assert
            Expect(expected)
                .To.Equal(executed);
        }

        [Test]
        public void Execute_With_CanExecute()
        {
            // Arrange
            var executed = false;
            var cmd = new Command(() => executed = true, () => true);

            // Act
            cmd.Execute(null);

            // Assert
            Expect(executed)
                .To.Be.True();
        }

        [Test]
        public void CanExecute()
        {
            // Arrange
            var canExecuteRan = false;
            var cmd = new Command(() => { }, () =>
            {
                canExecuteRan = true;
                return true;
            });

            // Act

            // Assert
            Expect(cmd.CanExecute(null))
                .To.Equal(true);
            Expect(canExecuteRan)
                .To.Be.True();
        }

        [Test]
        public void Change_CanExecute()
        {
            // Arrange
            var signaled = false;
            var cmd = new Command(() => { });

            // Act
            cmd.CanExecuteChanged += (sender, args) => signaled = true;
            cmd.RaiseCanExecuteChanged();

            // Assert
            Expect(signaled)
                .To.Be.True();
        }

        [Test]
        public void Generic_Throws_With_Null_Execute() =>
            Expect(() => new Command<string>(null))
                .To.Throw<ArgumentNullException>();

        [Test]
        public void Generic_Throws_With_Null_Execute_And_CanExecute_Valid() =>
            Expect(() => new Command<string>(null, s => true))
                .To.Throw<ArgumentNullException>();

        [Test]
        public void Generic_Throws_With_Valid_Execute_And_CanExecute_Null() =>
            Expect(() => new Command<string>(s => { }, null))
                .To.Throw<ArgumentNullException>();

        [Test]
        public void Generic_Execute()
        {
            // Arrange
            string result = null;
            var cmd = new Command<string>(s => result = s);

            // Act
            cmd.Execute("Foo");

            // Assert
            Expect(result)
                .To.Equal("Foo");
        }

        [Test]
        public void Generic_Execute_With_CanExecute()
        {
            // Arrange
            string result = null;
            var cmd = new Command<string>(s => result = s);

            // Act
            cmd.Execute("Foo");

            // Assert
            Expect(result)
                .To.Equal("Foo");
        }

        [Test]
        public void Generic_CanExecute()
        {
            // Arrange
            string result = null;
            var cmd = new Command<string>(s => { }, s =>
            {
                result = s;
                return true;
            });

            // Act

            // Assert
            Expect(cmd.CanExecute("Foo"))
                .To.Equal(true);
            Expect(result)
                .To.Equal("Foo");
        }

        private class FakeParentContext
        {
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class FakeChildContext
        {
        }

        [Test]
        public void CanExecute_Returns_False_If_Parameter_Is_Wrong_Reference_Type()
        {
            var command = new Command<FakeChildContext>(context => { }, context => true);

            Expect(() => command.CanExecute(new FakeParentContext()))
                .To.Throw<InvalidCommandParameterException>();
        }

        [Test]
        public void CanExecute_Returns_False_If_Parameter_Is_Wrong_Value_Type()
        {
            var command = new Command<int>(context => { }, context => true);

            Expect(() => command.CanExecute(10.5))
                .To.Throw<InvalidCommandParameterException>();
        }

        [Test]
        public void CanExecute_Uses_Parameter_If_Reference_Type_And_Set_To_Null()
        {
            var command = new Command<FakeChildContext>(context => { }, context => true);

            Expect(command.CanExecute(null))
                .To.Be.True("null is a valid value for a reference type");
        }

        [Test]
        public void CanExecute_Uses_Parameter_If_Nullable_And_Set_To_Null()
        {
            var command = new Command<int?>(context => { }, context => true);

            Expect(command.CanExecute(null))
                .To.Be.True("null is a valid value for a Nullable<int> type");
        }

        [Test]
        public void CanExecute_Ignores_Parameter_If_Value_Type_And_Set_To_Null()
        {
            var command = new Command<int>(context => { }, context => true);

            Expect(() => command.CanExecute(null))
                .To.Throw<InvalidCommandParameterException>();
        }

        [Test]
        public void Execute_Does_Not_Run_If_Parameter_Is_Wrong_Reference_Type()
        {
            var executions = 0;
            var command = new Command<FakeChildContext>(context => executions += 1);

            Expect(executions)
                .To.Equal(0, "the command should not have executed");
        }

        [Test]
        public void Execute_Does_Not_Run_If_Parameter_Is_Wrong_Value_Type()
        {
            var executions = 0;
            var command = new Command<int>(context => executions += 1);

            Expect(executions)
                .To.Equal(0, "the command should not have executed");
        }

        [Test]
        public void Execute_Runs_If_Reference_Type_And_Set_To_Null()
        {
            var executions = 0;
            var command = new Command<FakeChildContext>(context => executions += 1);

            command.Execute(null);
            Expect(executions)
                .To.Equal(1, "the command should have executed");
        }

        [Test]
        public void Execute_Runs_If_Nullable_And_Set_To_Null()
        {
            var executions = 0;
            var command = new Command<int?>(context => executions += 1);

            command.Execute(null);

            Expect(executions)
                .To.Equal(1, "the command should have executed");
        }

        [Test]
        public void Execute_Does_Not_Run_If_Value_Type_And_Set_To_Null()
        {
            var executions = 0;
            var command = new Command<int>(context => executions += 1);

            Expect(executions)
                .To.Equal(0, "the command should not have executed");
        }
    }
}