using Microsoft.VisualStudio.TestTools.UnitTesting;

// NOTE: These tests are essentially just the standard BST tests with some balance checks. 

namespace DSA.Tests
{
    [TestClass()]
    public class AVLTreeTests
    {
        private AVLTree<int> tree = [];

        private void AssertIsBalanced()
        {
            foreach (AVLNode<int> node in tree.GetInOrderEnumerable(tree.Root))
            {
                int balanceFactor = (node.Left?.Height ?? 0) - (node.Right?.Height ?? 0);
                Assert.IsTrue(balanceFactor <= 1 && balanceFactor >= -1);
            }
        }

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
            foreach (var _ in tree.GetBreadthEnumerable(tree.Root))
                Assert.Fail();
            foreach (var _ in tree.GetInOrderEnumerable(tree.Root))
                Assert.Fail();
            foreach (var _ in tree.GetPreOrderEnumerable(tree.Root))
                Assert.Fail();
            foreach (var _ in tree.GetPostOrderEnumerable(tree.Root))
                Assert.Fail();

            // Clearing an empty tree should not alter anything
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);
        }

        [TestMethod()]
        public void AddTest()
        {
            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7];
            Assert.AreEqual(10, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(10, tree.Root.Value);

            AssertIsBalanced();

            tree.Add(9);
            Assert.AreEqual(11, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(10, tree.Root.Value);

            AssertIsBalanced();
        }

        [TestMethod()]
        public void RemoveTest()
        {
            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];
            Assert.AreEqual(11, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(10, tree.Root.Value);

            AssertIsBalanced();

            // Remove root

            tree.Remove(10);
            Assert.AreEqual(10, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(11, tree.Root.Value);
            Assert.IsFalse(tree.Contains(10));

            AssertIsBalanced();

            int[] ints = new int[tree.Count];
            int[] correctInts = [3, 4, 5, 6, 7, 8, 9, 11, 12, 13];

            int idx = 0;
            foreach (int i in tree)
                ints[idx++] = i;

            for (int i = 0; i < tree.Count; i++)
                Assert.AreEqual(correctInts[i], ints[i]);

            // Remove left leaf

            tree.Remove(3);
            Assert.AreEqual(9, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(11, tree.Root.Value);
            Assert.IsFalse(tree.Contains(3));

            AssertIsBalanced();

            ints = new int[tree.Count];
            correctInts = [4, 5, 6, 7, 8, 9, 11, 12, 13];

            idx = 0;
            foreach (int i in tree)
                ints[idx++] = i;

            for (int i = 0; i < tree.Count; i++)
                Assert.AreEqual(correctInts[i], ints[i]);

            // Remove right leaf

            tree.Remove(13);
            Assert.AreEqual(8, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(11, tree.Root.Value);
            Assert.IsFalse(tree.Contains(13));

            AssertIsBalanced();

            ints = new int[tree.Count];
            correctInts = [4, 5, 6, 7, 8, 9, 11, 12];

            idx = 0;
            foreach (int i in tree)
                ints[idx++] = i;

            for (int i = 0; i < tree.Count; i++)
                Assert.AreEqual(correctInts[i], ints[i]);

            // Remove branch nodes

            tree.Remove(6);
            tree.Remove(8);
            tree.Remove(4);
            Assert.AreEqual(5, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(11, tree.Root.Value);
            Assert.IsFalse(tree.Contains(6));
            Assert.IsFalse(tree.Contains(8));
            Assert.IsFalse(tree.Contains(4));

            AssertIsBalanced();

            ints = new int[tree.Count];
            correctInts = [5, 7, 9, 11, 12, 13];

            idx = 0;
            foreach (int i in tree)
                ints[idx++] = i;

            for (int i = 0; i < tree.Count; i++)
                Assert.AreEqual(correctInts[i], ints[i]);
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Create usual test tree but with some duplicates (10x3, 8x2, 3x3)
            tree = [10, 10, 10, 6, 12, 4, 8, 8, 11, 13, 3, 5, 3, 7, 3, 9];
            Assert.AreEqual(16, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(10, tree.Root.Value);
            Assert.AreEqual(3, tree.Root.Count);

            AssertIsBalanced();

            // Remove first copy of root
            tree.Remove(10);
            Assert.AreEqual(15, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(10, tree.Root.Value);
            Assert.AreEqual(2, tree.Root.Count);
            Assert.IsTrue(tree.Contains(10));

            AssertIsBalanced();

            // Remove root entirely
            tree.Remove(10);
            tree.Remove(10);
            Assert.AreEqual(13, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(11, tree.Root.Value);
            Assert.AreEqual(1, tree.Root.Count);
            Assert.IsFalse(tree.Contains(10));

            AssertIsBalanced();

            // Make sure other duplicates are valid
            Assert.IsTrue(tree.Contains(8));
            Assert.IsTrue(tree.Contains(3));

            var node = tree.Find(8);
            Assert.IsNotNull(node);
            Assert.AreEqual(2, node.Count);

            node = tree.Find(3);
            Assert.IsNotNull(node);
            Assert.AreEqual(3, node.Count);
        }

        [TestMethod()]
        public void ClearTest()
        {
            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            Assert.AreEqual(11, tree.Count);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(10, tree.Root.Value);

            tree.Clear();

            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);
        }

        [TestMethod()]
        public void ContainsTest()
        {
            int[] ints = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];
            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            foreach (int i in ints)
                Assert.IsTrue(tree.Contains(i));

            for (int i = -10; i <= 2; i++)
                Assert.IsFalse(tree.Contains(i));
            for (int i = 14; i <= 20; i++)
                Assert.IsFalse(tree.Contains(i));
        }

        [TestMethod()]
        public void CopyToTest()
        {
            int[] ints = new int[11];
            int[] correctInts = [3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13];

            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            tree.CopyTo(ints, 0);

            // CopyTo should put the inserted ints in order
            for (int i = 0; i < 11; i++)
                Assert.AreEqual(correctInts[i], ints[i]);
        }

        [TestMethod()]
        public void InOrderTest()
        {
            int[] ints = new int[11];
            int[] correctInts = [3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13];

            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            int idx = 0;
            foreach (var node in tree.GetInOrderEnumerable(tree.Root))
                ints[idx++] = node.Value;

            for (int i = 0; i < 11; i++)
                Assert.AreEqual(correctInts[i], ints[i]);
        }

        [TestMethod()]
        public void PreOrderTest()
        {
            int[] ints = new int[11];
            int[] correctInts = [10, 6, 4, 3, 5, 8, 7, 9, 12, 11, 13];

            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            int idx = 0;
            foreach (var node in tree.GetPreOrderEnumerable(tree.Root))
            {
                ints[idx++] = node.Value;
            }

            for (int i = 0; i < 11; i++)
                Assert.AreEqual(correctInts[i], ints[i]);
        }

        [TestMethod()]
        public void PostOrderTest()
        {
            int[] ints = new int[11];
            int[] correctInts = [3, 5, 4, 7, 9, 8, 6, 11, 13, 12, 10];

            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            int idx = 0;
            foreach (var node in tree.GetPostOrderEnumerable(tree.Root))
            {
                ints[idx++] = node.Value;
            }

            for (int i = 0; i < 11; i++)
                Assert.AreEqual(correctInts[i], ints[i]);
        }

        [TestMethod()]
        public void BreadthTest()
        {
            int[] ints = new int[11];
            int[] correctInts = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            int idx = 0;
            foreach (var node in tree.GetBreadthEnumerable(tree.Root))
            {
                ints[idx++] = node.Value;
            }

            for (int i = 0; i < 11; i++)
                Assert.AreEqual(correctInts[i], ints[i]);
        }

        [TestMethod()]
        public void EnumeratorTest()
        {
            int[] ints = new int[11];
            int[] correctInts = [3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13];

            tree = [10, 6, 12, 4, 8, 11, 13, 3, 5, 7, 9];

            // Should be same as InOrder

            int idx = 0;
            foreach (int i in tree)
            {
                ints[idx++] = i;
            }

            for (int i = 0; i < 11; i++)
                Assert.AreEqual(correctInts[i], ints[i]);
        }
    }
}