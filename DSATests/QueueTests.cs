using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class QueueTests
    {
        private Queue<int> queue = new(0);

        [TestInitialize]
        public void Init()
        {
            queue = new(3);
        }

        [TestMethod()]
        public void InitTest()
        {
            Assert.IsTrue(queue.IsEmpty);
            Assert.IsFalse(queue.IsFull);
            Assert.AreEqual(0, queue.Size);

            // Make sure dequeueing or peeking from empty queue throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => queue.Dequeue());
            Assert.ThrowsException<IndexOutOfRangeException>(() => queue.Peek());
        }

        [TestMethod()]
        public void EnqueueTest()
        {
            queue.Enqueue(1);
            Assert.IsFalse(queue.IsEmpty);
            Assert.IsFalse(queue.IsFull);
            Assert.AreEqual(1, queue.Size);
            queue.Enqueue(2);
            Assert.IsFalse(queue.IsEmpty);
            Assert.IsFalse(queue.IsFull);
            Assert.AreEqual(2, queue.Size);
            queue.Enqueue(3);
            Assert.IsFalse(queue.IsEmpty);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            // Ensure adding a 4th throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => queue.Enqueue(4));
        }

        [TestMethod()]
        public void DequeueTest()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            int val = queue.Dequeue();
            Assert.IsTrue(queue.IsFull);
            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(2, queue.Size);
            Assert.AreEqual(1, val);

            val = queue.Dequeue();
            Assert.IsTrue(queue.IsFull);
            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(1, queue.Size);
            Assert.AreEqual(2, val);

            val = queue.Dequeue();
            Assert.IsFalse(queue.IsFull);
            Assert.IsTrue(queue.IsEmpty);
            Assert.AreEqual(0, queue.Size);
            Assert.AreEqual(3, val);
        }

        [TestMethod()]
        public void PeekTest()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            int val = queue.Peek();
            Assert.AreEqual(1, val);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);
        }
    }
}