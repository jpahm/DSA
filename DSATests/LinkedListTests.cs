using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class LinkedListTests
    {
        private LinkedList<int> list = [];

        [TestInitialize]
        public void Init()
        {
            list = [];
        }

        [TestMethod()]
        public void InitTest()
        {
            Assert.IsTrue(list.IsEmpty);
            Assert.AreEqual(0, list.Count);
            Assert.IsFalse(list.IsReadOnly);

            // Removing from an empty list shouldn't throw or modify the list
            list.RemoveStart();
            list.RemoveEnd();

            Assert.IsTrue(list.IsEmpty);
            Assert.AreEqual(0, list.Count);

            // Trying to remove from an invalid index should throw
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));

            // Trying to set/get an invalid index should throw
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.SetAt(0, 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.GetAt(0));
        }

        [TestMethod()]
        public void AddTest()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            for (int i = 0; i < list.Count; ++i)
                Assert.AreEqual(i + 1, list[i]);
        }

        [TestMethod()]
        public void AddStartTest()
        {
            list.AddStart(3);
            list.AddStart(2);
            list.AddStart(1);
            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            for (int i = 0; i < list.Count; ++i)
                Assert.AreEqual(i + 1, list[i]);
        }

        [TestMethod()]
        public void RemoveEndTest()
        {
            list = [1, 2, 3];

            list.RemoveEnd();

            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.IsEmpty);

            list.RemoveEnd();
            list.RemoveEnd();

            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod()]
        public void RemoveStartTest()
        {
            list = [1, 2, 3];

            list.RemoveStart();

            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.IsEmpty);

            list.RemoveStart();
            list.RemoveStart();

            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            list = [1, 2, 3];

            list.Remove(2);

            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.IsEmpty);

            list.Remove(1);
            list.Remove(3);

            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod()]
        public void IndexerTest()
        {
            list = [1, 2, 3];

            Assert.AreEqual(list[0], 1);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[2], 3);

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            list[0] = 4;
            list[1] = 5;
            list[-1] = 6;

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            Assert.AreEqual(list[0], 4);
            Assert.AreEqual(list[1], 5);
            Assert.AreEqual(list[-1], 6);

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);
        }

        [TestMethod()]
        public void InsertTest()
        {
            list.Insert(0, 1);
            list.Insert(1, 3);
            list.Insert(2, 5);

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            list.Insert(1, 2);

            Assert.AreEqual(4, list.Count);

            list.Insert(3, 4);

            Assert.AreEqual(5, list.Count);

            list.Insert(5, 6);

            Assert.AreEqual(6, list.Count);

            Assert.AreEqual(list[0], 1);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[2], 3);
            Assert.AreEqual(list[3], 4);
            Assert.AreEqual(list[4], 5);
            Assert.AreEqual(list[5], 6);
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            list = [1, 2, 3];

            Assert.AreEqual(3, list.Count);

            list.RemoveAt(1);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(3, list[1]);

            list.RemoveAt(0);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(3, list[0]);

            list.RemoveAt(-1);

            Assert.IsTrue(list.IsEmpty);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            list = [1, 2, 3];

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            Assert.AreEqual(0, list.IndexOf(1));
            Assert.AreEqual(1, list.IndexOf(2));
            Assert.AreEqual(2, list.IndexOf(3));
            Assert.AreEqual(-1, list.IndexOf(4));

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            list.Remove(2);
            list.Add(3);

            Assert.AreEqual(0, list.IndexOf(1));
            Assert.AreEqual(-1, list.IndexOf(2));
            Assert.AreEqual(1, list.IndexOf(3));
            Assert.AreEqual(-1, list.IndexOf(4));

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);
        }

        [TestMethod()]
        public void ClearTest()
        {
            list = [1, 2, 3];

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);

            list.Clear();

            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(list.IsEmpty);

            // Trying to remove from an invalid index should throw
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));

            // Trying to set/get an invalid index should throw
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.SetAt(0, 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.GetAt(0));
        }

        [TestMethod()]
        public void ContainsTest()
        {
            list = [1, 2, 3];

            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
            Assert.IsTrue(list.Contains(3));

            Assert.IsFalse(list.Contains(4));

            list.Clear();

            Assert.IsFalse(list.Contains(1));
            Assert.IsFalse(list.Contains(2));
            Assert.IsFalse(list.Contains(3));
            Assert.IsFalse(list.Contains(4));
        }

        [TestMethod()]
        public void CopyToTest()
        {
            list = [1, 2, 3];

            int[]? ints = null;

            // Copying to null array throws ArgumentNullException
            Assert.ThrowsException<ArgumentNullException>(() => list.CopyTo(ints!, 0));

            ints = new int[list.Count];
            list.CopyTo(ints, 0);

            Assert.AreEqual(1, ints[0]);
            Assert.AreEqual(2, ints[1]);
            Assert.AreEqual(3, ints[2]);

            // Copying to negative index throws ArgumentOutOfRangeException
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.CopyTo(ints, -1));
            // Copying beyond end of array throws ArgumentOutOfRangeException
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.CopyTo(ints, 3));
            // Copying without enough space throws ArgumentException
            Assert.ThrowsException<ArgumentException>(() => list.CopyTo(ints, 2));
        }

        [TestMethod()]
        public void EnumeratorTest()
        {
            list = [1, 2, 3];

            int i = 0;
            foreach (int val in list)
                Assert.AreEqual(++i, val);

            var enumerator = list.GetEnumerator();
            for (i = 0; enumerator.MoveNext(); )
                Assert.AreEqual(++i, enumerator.Current);

            Assert.AreEqual(3, list.Count);
            Assert.IsFalse(list.IsEmpty);
        }
    }
}