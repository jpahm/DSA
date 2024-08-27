using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class LinkedListTests
    {
        private LinkedList<int> list = new();

        [TestInitialize]
        public void Init()
        {
            list = new();
        }

        [TestMethod()]
        public void InitTest()
        {
            Assert.IsTrue(list.IsEmpty);
            Assert.AreEqual(0, list.Size);

            // Make sure deleting from empty list throws
            Assert.ThrowsException<IndexOutOfRangeException>(() => list.DeleteStart());
            Assert.ThrowsException<IndexOutOfRangeException>(() => list.DeleteEnd());
            Assert.ThrowsException<IndexOutOfRangeException>(() => list.DeleteAt(0));
        }

        [TestMethod()]
        public void AddStartTest()
        {
            list.AddStart(1);
            list.AddStart(2);
            list.AddStart(3);
            Assert.AreEqual(3, list.Size);
            Assert.IsFalse(list.IsEmpty);
        }

        [TestMethod()]
        public void AddEndTest()
        {
            list.AddEnd(1);
            list.AddEnd(2);
            list.AddEnd(3);
            Assert.AreEqual(3, list.Size);
            Assert.IsFalse(list.IsEmpty);
        }

        [TestMethod()]
        public void InsertAtTest()
        {
            list.InsertAt(0, 1);
            list.InsertAt(1, 2);
            list.InsertAt(2, 3);
            Assert.AreEqual(3, list.Size);
            Assert.IsFalse(list.IsEmpty);
        }
    }
}