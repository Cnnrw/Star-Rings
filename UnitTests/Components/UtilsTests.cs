using System;
using System.Threading.Tasks;

using Game;

using NUnit.Framework;

#nullable enable

namespace UnitTests.Components
{
    [TestFixture]
    public class UtilsTests
    {
        const  int  Delay = 500;
        static Task NoParameterTask() => Task.Delay(Delay);

        static async Task NoParameterDelayedNullReferenceExceptionTask()
        {
            await Task.Delay(Delay);
            throw new NullReferenceException();
        }

        [Test]
        public async Task Utils_SafeFireAndForget_HandledException_Should_Pass()
        {
            // Arrange
            Exception? exception = null;

            // Act
            NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget(ex => exception = ex);
            await NoParameterTask();
            await NoParameterTask();

            // Assert
            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task Utils_SafeFireAndForget_SetDefaultExceptionHandling_NoParams()
        {
            // Arrange
            Exception? exception = null;
            Action<Exception> myAct = ex => exception = ex;

            // Call with Error = true

            // Act
            NoParameterDelayedNullReferenceExceptionTask().SafeFireAndForget(myAct);
            await NoParameterTask();
            await NoParameterTask();

            Assert.IsNotNull(exception);
        }
    }
}
