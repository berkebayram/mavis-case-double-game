using NUnit.Framework;
using System;

namespace Main.Scripts.EventSystem.Tests
{
    public class FunctionalityTest
    {
        private struct MessageTestEvent
        {
            public string Message;
        }

        private struct FloatTestEvent
        {
            public float Value;
        }

        [Test]
        public void DispatchTest()
        {
            var subject = "hello";
            Dispatcher.Subscribe<MessageTestEvent>((val) =>
            {
                subject = val.Message;
            });
            Dispatcher.Dispatch(new MessageTestEvent() { Message = "World" });
            Assert.AreEqual(subject, "World");
        }

        [Test]
        public void DispatchMultipleTest()
        {
            var firstSubject = 0f;
            var secondSubject = 0f;

            Dispatcher.Subscribe<FloatTestEvent>((val) =>
            {
                firstSubject = val.Value;
            });
            Dispatcher.Subscribe<FloatTestEvent>((val) =>
            {
                secondSubject = val.Value;
            });
            Dispatcher.Dispatch(new FloatTestEvent() { Value = 5f });

            Assert.AreEqual(firstSubject, 5f);
            Assert.AreEqual(secondSubject, 5f);
        }

        [Test]
        public void DispatchClearTest()
        {
            var subject = "hello";
            Dispatcher.Subscribe<MessageTestEvent>((val) =>
            {
                subject = val.Message;
            });

            Dispatcher.Dispatch(new MessageTestEvent() { Message = "World" });
            Dispatcher.Clear();

            Dispatcher.Dispatch(new MessageTestEvent() { Message = "Is it clear now ? " });
            Assert.AreEqual(subject, "World");
        }

        [Test]
        public void DispatchUnsubscribeTest()
        {
            var subject = "hello";

            Action<MessageTestEvent> actPointer = (val) =>
            {
                subject = val.Message;
            };
            Dispatcher.Subscribe<MessageTestEvent>(actPointer);

            Dispatcher.Dispatch(new MessageTestEvent() { Message = "World" });
            Dispatcher.Unsubscribe<MessageTestEvent>(actPointer);

            Dispatcher.Dispatch(new MessageTestEvent() { Message = "Is it clear now ? " });
            Assert.AreEqual(subject, "World");

        }

        [TearDown]
        public void CleanUp()
        {
            Dispatcher.Clear();
        }
    }
}
