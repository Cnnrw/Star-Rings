using System;
using System.Diagnostics.CodeAnalysis;

using Game.Components;

using NUnit.Framework;

// ReSharper disable ExplicitCallerInfoArgument

namespace UnitTests.Components
{
    [TestFixture]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class WeakEventManagerTests
    {
        private static int count;

        private static void Handler(object sender, EventArgs eventArgs) => count++;

        internal class TestSource
        {
            public int Count;

            public TestSource()
            {
                EventSource = new TestEventSource();
                EventSource.TestEvent += EventSource_TestEvent;
            }

            private TestEventSource EventSource { get; }

            public  void Clean() => EventSource.TestEvent -= EventSource_TestEvent;
            public  void Fire() => EventSource.FireTestEvent();
            private void EventSource_TestEvent(object sender, EventArgs e) => Count++;
        }

        internal class TestEventSource
        {
            private readonly WeakEventManager _weakEventManager;
            public TestEventSource() => _weakEventManager = new WeakEventManager();

            public void FireTestEvent() => OnTestEvent();

            internal event EventHandler TestEvent
            {
                add => _weakEventManager.AddEventHandler(value);
                remove => _weakEventManager.RemoveEventHandler(value);
            }

            private void OnTestEvent() => _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(TestEvent));
        }

        internal class TestSubscriber
        {
            public void Subscribe(TestEventSource source) => source.TestEvent += SourceOnTestEvent;

            // NOTE: Cannot be static
            private void SourceOnTestEvent(object sender, EventArgs eventArgs) => Assert.Fail();
        }

        [Test]
        public void Add_Handler_With_Empty_Event_Name_Throws_Exception()
        {
            var wem = new WeakEventManager();
            Assert.Throws<ArgumentNullException>(() => wem.AddEventHandler((sender, args) => { }, string.Empty));
        }

        [Test]
        public void Add_Handler_With_Null_Event_Handler_Throws_Exception()
        {
            var wem = new WeakEventManager();
            Assert.Throws<ArgumentNullException>(() => wem.AddEventHandler(null, "test"));
        }

        [Test]
        public void Add_Handler_With_Null_Event_Name_Throws_Exception()
        {
            var wem = new WeakEventManager();
            Assert.Throws<ArgumentNullException>(() => wem.AddEventHandler((sender, args) => { }, null));
        }

        [Test]
        public void Can_Remove_Event_Handler()
        {
            var source = new TestSource();
            _ = source.Count;
            source.Fire();

            Assert.That(source.Count, Is.EqualTo(1));

            source.Clean();
            source.Fire();
            Assert.That(source.Count, Is.EqualTo(1));
        }

        [Test]
        public void Can_Remove_Static_Event_Handler()
        {
            var beforeRun = count;

            var source = new TestEventSource();
            source.TestEvent += Handler;
            source.TestEvent -= Handler;

            source.FireTestEvent();

            Assert.That(count, Is.EqualTo(beforeRun));
        }

        [Test]
        public void Event_Handler_Called()
        {
            var called = false;

            var source = new TestEventSource();
            source.TestEvent += (sender, args) =>
            {
                called = true;
            };

            source.FireTestEvent();

            Assert.That(called, Is.True);
        }

        [Test]
        public void Firing_Event_Without_Handler_Should_Not_Throw()
        {
            var source = new TestEventSource();
            Assert.DoesNotThrow(() => source.FireTestEvent());
        }

        [Test]
        public void Multiple_Handlers_Called()
        {
            var called1 = false;
            var called2 = false;

            var source = new TestEventSource();
            source.TestEvent += (sender, args) =>
            {
                called1 = true;
            };
            source.TestEvent += (sender, args) =>
            {
                called2 = true;
            };

            source.FireTestEvent();

            Assert.That(called1 && called2, Is.True);
        }

        [Test]
        public void Remove_Handler_With_Empty_Event_Name_Throws_Exception()
        {
            var wem = new WeakEventManager();
            Assert.Throws<ArgumentNullException>(() =>
                wem.RemoveEventHandler((sender, args) => { }, string.Empty));
        }

        [Test]
        public void Remove_Handler_With_Null_Event_Handler_Throws_Exception()
        {
            var wem = new WeakEventManager();
            Assert.Throws<ArgumentNullException>(() =>
                wem.RemoveEventHandler(null, "test"));
        }

        [Test]
        public void Remove_Handler_With_Null_Event_Name_Throws_Exception()
        {
            var wem = new WeakEventManager();
            Assert.Throws<ArgumentNullException>(() =>
                wem.RemoveEventHandler((sender, args) => { }, null));
        }

        [Test]
        public void Removing_Non_Existent_Handler_Should_Not_Throw()
        {
            var wem = new WeakEventManager();
            Assert.DoesNotThrow(() =>
                wem.RemoveEventHandler((sender, args) => { }, "fake"));
            Assert.DoesNotThrow(() =>
                wem.RemoveEventHandler(Handler, "alsofake"));
        }

        [Test]
        public void Remove_Handler_With_Multiple_Subscriptions_Removes_One()
        {
            var beforeRun = count;
            var source = new TestEventSource();

            source.TestEvent += Handler;
            source.TestEvent += Handler;
            source.TestEvent -= Handler;

            source.FireTestEvent();

            Assert.That(beforeRun + 1, Is.EqualTo(count));
        }

        [Test]
        public void Static_Handler_Should_Run()
        {
            var beforeRun = count;

            var source = new TestEventSource();

            source.TestEvent += Handler;

            source.FireTestEvent();

            Assert.That(count > beforeRun, Is.True);
        }

        [Test]
        public void Verify_Subscriber_Can_Be_Collected()
        {
            WeakReference wr = null;
            var source = new TestEventSource();
            new Action(() =>
            {
                var ts = new TestSubscriber();
                wr = new WeakReference(ts);
                ts.Subscribe(source);
            })();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.IsNotNull(wr);
            Assert.IsFalse(wr.IsAlive);

            // The handler for this calls Assert.Fail, so if the subscriber has not been collected
            // the handler will be called and the test will fail
            source.FireTestEvent();
        }
    }
}