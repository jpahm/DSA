using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class StackTests
    {
        private Stack<int> stack = new(0);

        [TestInitialize]
        public void Init()
        {
            stack = new(3);
        }

        [TestMethod()]
        public void InitTest()
        {
            Assert.IsTrue(stack.IsEmpty);
            Assert.IsFalse(stack.IsFull);
            Assert.AreEqual(0, stack.Count);

            // Make sure popping or peeking from empty stack throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => stack.Pop());
            Assert.ThrowsException<IndexOutOfRangeException>(() => stack.Peek());
        }

        [TestMethod()]
        public void PushTest()
        {
            stack.Push(1);
            Assert.IsFalse(stack.IsEmpty);
            Assert.IsFalse(stack.IsFull);
            Assert.AreEqual(1, stack.Count);
            stack.Push(2);
            Assert.IsFalse(stack.IsEmpty);
            Assert.IsFalse(stack.IsFull);
            Assert.AreEqual(2, stack.Count);
            stack.Push(3);
            Assert.IsFalse(stack.IsEmpty);
            Assert.IsTrue(stack.IsFull);
            Assert.AreEqual(3, stack.Count);

            // Ensure adding a 4th throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => stack.Push(4));
        }

        [TestMethod()]
        public void PopTest()
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.IsTrue(stack.IsFull);
            Assert.AreEqual(3, stack.Count);

            int val = stack.Pop();
            Assert.IsFalse(stack.IsFull);
            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(2, stack.Count);
            Assert.AreEqual(3, val);

            val = stack.Pop();
            Assert.IsFalse(stack.IsFull);
            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(1, stack.Count);
            Assert.AreEqual(2, val);

            val = stack.Pop();
            Assert.IsFalse(stack.IsFull);
            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(0, stack.Count);
            Assert.AreEqual(1, val);
        }

        [TestMethod()]
        public void PeekTest()
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.IsTrue(stack.IsFull);
            Assert.AreEqual(3, stack.Count);

            int val = stack.Peek();
            Assert.AreEqual(3, val);
            Assert.IsTrue(stack.IsFull);
            Assert.AreEqual(3, stack.Count);
        }
    }
}