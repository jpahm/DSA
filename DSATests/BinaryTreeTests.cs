using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        private BinaryTree<int> tree = [];

        [TestInitialize]
        public void Init()
        {
            tree = [];
        }

        [TestMethod()]
        public void InitTest()
        {
            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);

            // Make sure all enumerations halt immediately on empty tree
            foreach (var _ in tree)
                Assert.Fail();
            foreach (var _ in tree.GetBreadthEnumerator(tree.Root))
                Assert.Fail();
            foreach (var _ in tree.GetInOrderEnumerator(tree.Root))
                Assert.Fail();
            foreach (var _ in tree.GetPreOrderEnumerator(tree.Root))
                Assert.Fail();
            foreach (var _ in tree.GetPostOrderEnumerator(tree.Root))
                Assert.Fail();

            // Clearing an empty tree should not alter anything
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);
        }

        [TestMethod()]
        public void AddTest()
        {
            tree = [2, 1, 3];
            Assert.AreEqual(3, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(2, tree.Root.Value);

            tree.Add(0);
            Assert.AreEqual(4, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(2, tree.Root.Value);
        }

        [TestMethod()]
        public void ClearTest()
        {
            for (int i = 0; i < 10; i++)
                tree.Add(i);

            Assert.AreEqual(10, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(0, tree.Root.Value);

            tree.Clear();

            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);
        }

        [TestMethod()]
        public void ContainsTest()
        {
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            
            for (int i = 0; i < 10; i++)
                Assert.IsTrue(tree.Contains(i));

            for (int i = 10; i < 20; i++)
                Assert.IsFalse(tree.Contains(i));
        }

        [TestMethod()]
        public void CopyToTest()
        {
            int[] ints = new int[10];
            for (int i = 0; i < 10; i++)
                tree.Add(i);

            // Copyto copies via InOrder traversal, so the sequence we're looking for is as such
            int[] desiredInts = [ 7, 3, 8, 1, 9, 4, 0, 5, 2, 6 ];

            tree.CopyTo(ints, 0);

            for (int i = 0; i < 10; i++)
                Assert.AreEqual(desiredInts[i], ints[i]);
        }

        [TestMethod()]
        public void InOrderTest()
        {
            int[] ints = new int[10];
            for (int i = 0; i < 10; i++)
                tree.Add(i);

            int[] desiredInts = [7, 3, 8, 1, 9, 4, 0, 5, 2, 6];

            int idx = 0;
            foreach (var node in tree.GetInOrderEnumerator(tree.Root))
            {
                ints[idx++] = node.Value;
            }

            for (int i = 0; i < 10; i++)
                Assert.AreEqual(desiredInts[i], ints[i]);
        }

        [TestMethod()]
        public void PreOrderTest()
        {
            int[] ints = new int[10];
            for (int i = 0; i < 10; i++)
                tree.Add(i);

            int[] desiredInts = [0, 1, 3, 7, 8, 4, 9, 2, 5, 6];

            int idx = 0;
            foreach (var node in tree.GetPreOrderEnumerator(tree.Root))
            {
                ints[idx++] = node.Value;
            }

            for (int i = 0; i < 10; i++)
                Assert.AreEqual(desiredInts[i], ints[i]);
        }

        [TestMethod()]
        public void PostOrderTest()
        {
            int[] ints = new int[10];
            for (int i = 0; i < 10; i++)
                tree.Add(i);

            int[] desiredInts = [7, 8, 3, 9, 4, 1, 5, 6, 2, 0];

            int idx = 0;
            foreach (var node in tree.GetPostOrderEnumerator(tree.Root))
            {
                ints[idx++] = node.Value;
            }

            for (int i = 0; i < 10; i++)
                Assert.AreEqual(desiredInts[i], ints[i]);
        }

        [TestMethod()]
        public void BreadthTest()
        {
            int[] ints = new int[10];

            for (int i = 0; i < 10; i++)
                tree.Add(i);

            int idx = 0;
            foreach (var node in tree.GetBreadthEnumerator(tree.Root))
            {
                ints[idx++] = node.Value;
            }

            for (int i = 0; i < 10; i++)
                Assert.AreEqual(i, ints[i]);
        }

        [TestMethod()]
        public void EnumeratorTest()
        {
            int[] ints = new int[10];
            for (int i = 0; i < 10; i++)
                tree.Add(i);

            // Should be same as InOrder
            int[] desiredInts = [7, 3, 8, 1, 9, 4, 0, 5, 2, 6];

            int idx = 0;
            foreach (int i in tree)
            {
                ints[idx++] = i;
            }

            for (int i = 0; i < 10; i++)
                Assert.AreEqual(desiredInts[i], ints[i]);
        }
    }
}