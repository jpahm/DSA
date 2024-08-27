using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class DequeTests
    {
        private Deque<int> deque = new(0);

        [TestInitialize]
        public void Init()
        {
            deque = new(3);
        }

        [TestMethod()]
        public void InitTest()
        {
            Assert.IsTrue(deque.IsEmpty);
            Assert.IsFalse(deque.IsFull);
            Assert.AreEqual(0, deque.Size);

            // Make sure dequeueing or peeking from empty queue throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.DequeueRear());
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.PeekRear());
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.DequeueFront());
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.PeekFront());
        }

        [TestMethod()]
        public void EnqueueRearTest()
        {
            // Try adding 3 items
            deque.EnqeueRear(1);
            Assert.IsFalse(deque.IsEmpty);
            Assert.IsFalse(deque.IsFull);
            Assert.AreEqual(1, deque.Size);
            deque.EnqeueRear(2);
            Assert.IsFalse(deque.IsEmpty);
            Assert.IsFalse(deque.IsFull);
            Assert.AreEqual(2, deque.Size);
            deque.EnqeueRear(3);
            Assert.IsFalse(deque.IsEmpty);
            Assert.IsTrue(deque.IsFull);
            Assert.AreEqual(3, deque.Size);

            // Ensure adding a 4th throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.EnqeueRear(4));
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.EnqeueFront(4));
        }

        [TestMethod()]
        public void EnqueueFrontTest()
        {
            // Try adding 3 items
            deque.EnqeueFront(1);
            Assert.IsFalse(deque.IsEmpty);
            Assert.IsFalse(deque.IsFull);
            Assert.AreEqual(1, deque.Size);
            deque.EnqeueFront(2);
            Assert.IsFalse(deque.IsEmpty);
            Assert.IsFalse(deque.IsFull);
            Assert.AreEqual(2, deque.Size);
            deque.EnqeueFront(3);
            Assert.IsFalse(deque.IsEmpty);
            Assert.IsTrue(deque.IsFull);
            Assert.AreEqual(3, deque.Size);

            // Ensure adding a 4th throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.EnqeueRear(4));
            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.EnqeueFront(4));
        }

        [TestMethod()]
        public void EnqueueRearDequeueFrontTest()
        {
            deque.EnqeueRear(1);
            deque.EnqeueRear(2);
            deque.EnqeueRear(3);
            Assert.IsTrue(deque.IsFull);
            Assert.AreEqual(3, deque.Size);

            int val = deque.DequeueFront();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(1, val);

            val = deque.DequeueFront();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(2, val);

            val = deque.DequeueFront();
            Assert.IsFalse(deque.IsFull);
            Assert.IsTrue(deque.IsEmpty);
            Assert.AreEqual(0, deque.Size);
            Assert.AreEqual(3, val);
        }

        [TestMethod()]
        public void EnqueueRearDequeueRearTest()
        {
            deque.EnqeueRear(1);
            deque.EnqeueRear(2);
            deque.EnqeueRear(3);
            Assert.IsTrue(deque.IsFull);
            Assert.AreEqual(3, deque.Size);

            int val = deque.DequeueRear();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(3, val);

            val = deque.DequeueRear();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(2, val);

            val = deque.DequeueRear();
            Assert.IsFalse(deque.IsFull);
            Assert.IsTrue(deque.IsEmpty);
            Assert.AreEqual(0, deque.Size);
            Assert.AreEqual(1, val);
        }

        [TestMethod()]
        public void EnqueueFrontDequeueRearTest()
        {
            deque.EnqeueFront(1);
            deque.EnqeueFront(2);
            deque.EnqeueFront(3);
            Assert.IsTrue(deque.IsFull);
            Assert.AreEqual(3, deque.Size);

            int val = deque.DequeueRear();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(1, val);

            val = deque.DequeueRear();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(2, val);

            val = deque.DequeueRear();
            Assert.IsFalse(deque.IsFull);
            Assert.IsTrue(deque.IsEmpty);
            Assert.AreEqual(0, deque.Size);
            Assert.AreEqual(3, val);
        }

        [TestMethod()]
        public void EnqueueFrontDequeueFrontTest()
        {
            deque.EnqeueFront(1);
            deque.EnqeueFront(2);
            deque.EnqeueFront(3);
            Assert.IsTrue(deque.IsFull);
            Assert.AreEqual(3, deque.Size);

            int val = deque.DequeueFront();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(3, val);

            val = deque.DequeueFront();
            Assert.IsFalse(deque.IsFull);
            Assert.IsFalse(deque.IsEmpty);
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(2, val);

            val = deque.DequeueFront();
            Assert.IsFalse(deque.IsFull);
            Assert.IsTrue(deque.IsEmpty);
            Assert.AreEqual(0, deque.Size);
            Assert.AreEqual(1, val);
        }

        [TestMethod()]
        public void EnqueueFrontPeekFrontTest()
        {
            deque.EnqeueFront(1);
            deque.EnqeueFront(2);
            deque.EnqeueFront(3);

            int val = deque.PeekFront();
            Assert.AreEqual(3, deque.Size);
            Assert.AreEqual(3, val);
            deque.DequeueFront();

            val = deque.PeekFront();
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(2, val);
            deque.DequeueFront();

            val = deque.PeekFront();
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(1, val);
            deque.DequeueFront();

            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.PeekFront());
        }

        [TestMethod()]
        public void EqueueFrontPeekRearTest()
        {
            deque.EnqeueFront(1);
            deque.EnqeueFront(2);
            deque.EnqeueFront(3);

            int val = deque.PeekRear();
            Assert.AreEqual(3, deque.Size);
            Assert.AreEqual(1, val);
            deque.DequeueRear();

            val = deque.PeekRear();
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(2, val);
            deque.DequeueRear();

            val = deque.PeekRear();
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(3, val);
            deque.DequeueRear();

            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.PeekRear());
        }

        [TestMethod()]
        public void EnqueueRearPeekFrontTest()
        {
            deque.EnqeueRear(1);
            deque.EnqeueRear(2);
            deque.EnqeueRear(3);

            int val = deque.PeekFront();
            Assert.AreEqual(3, deque.Size);
            Assert.AreEqual(1, val);
            deque.DequeueFront();

            val = deque.PeekFront();
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(2, val);
            deque.DequeueFront();

            val = deque.PeekFront();
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(3, val);
            deque.DequeueFront();

            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.PeekFront());
        }

        [TestMethod()]
        public void EqueueRearPeekRearTest()
        {
            deque.EnqeueRear(1);
            deque.EnqeueRear(2);
            deque.EnqeueRear(3);

            int val = deque.PeekRear();
            Assert.AreEqual(3, deque.Size);
            Assert.AreEqual(3, val);
            deque.DequeueRear();

            val = deque.PeekRear();
            Assert.AreEqual(2, deque.Size);
            Assert.AreEqual(2, val);
            deque.DequeueRear();

            val = deque.PeekRear();
            Assert.AreEqual(1, deque.Size);
            Assert.AreEqual(1, val);
            deque.DequeueRear();

            Assert.ThrowsException<IndexOutOfRangeException>(() => deque.PeekRear());
        }
    }
}