using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class CircularQueueTests
    {
        private CircularQueue<int> queue = new(0);

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
            // Try adding 3 items
            queue.Enqeue(1);
            Assert.IsFalse(queue.IsEmpty);
            Assert.IsFalse(queue.IsFull);
            Assert.AreEqual(1, queue.Size);
            queue.Enqeue(2);
            Assert.IsFalse(queue.IsEmpty);
            Assert.IsFalse(queue.IsFull);
            Assert.AreEqual(2, queue.Size);
            queue.Enqeue(3);
            Assert.IsFalse(queue.IsEmpty);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            // Ensure adding a 4th throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => queue.Enqeue(4));
        }

        [TestMethod()]
        public void DequeueTest()
        {
            queue.Enqeue(1);
            queue.Enqeue(2);
            queue.Enqeue(3);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            int val = queue.Dequeue();
            Assert.IsFalse(queue.IsFull);
            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(2, queue.Size);
            Assert.AreEqual(1, val);

            val = queue.Dequeue();
            Assert.IsFalse(queue.IsFull);
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
            queue.Enqeue(1);
            queue.Enqeue(2);
            queue.Enqeue(3);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            int val = queue.Peek();
            Assert.AreEqual(1, val);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);
        }

        [TestMethod()]
        public void CircleTest()
        {
            // Queue 3 items
            queue.Enqeue(1);
            queue.Enqeue(2);
            queue.Enqeue(3);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            // Dequeue first item
            int val = queue.Dequeue();
            Assert.IsFalse(queue.IsFull);
            Assert.AreEqual(1, val);

            // Queue another 3
            queue.Enqeue(3);
            Assert.IsTrue(queue.IsFull);
            Assert.AreEqual(3, queue.Size);

            // Ensure queueing another item throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => queue.Enqeue(4));

            // Dequeue remaining items
            val = queue.Dequeue();
            Assert.AreEqual(2, val);
            val = queue.Dequeue();
            Assert.AreEqual(3, val);
            val = queue.Dequeue();
            Assert.AreEqual(3, val);

            // Ensure emptiness
            Assert.IsFalse(queue.IsFull);
            Assert.IsTrue(queue.IsEmpty);

            // Queue a 4
            queue.Enqeue(4);
            Assert.AreEqual(1, queue.Size);
            Assert.IsFalse(queue.IsFull);
            Assert.IsFalse(queue.IsEmpty);

            // Dequeue the 4
            val = queue.Dequeue();
            Assert.AreEqual(4, val);

            // Ensure emptiness
            Assert.IsFalse(queue.IsFull);
            Assert.IsTrue(queue.IsEmpty);
            Assert.AreEqual(0, queue.Size);
        }
    }
}