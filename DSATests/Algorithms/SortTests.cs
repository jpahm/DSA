using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Tests
{
    [TestClass()]
    public class SortTests
    {
        private int[] ints = new int[1024];
        private List<int> sortedInts = [];

        [TestInitialize]
        public void Init()
        {
            Utilities.RandomizeArray(ints);
            sortedInts = [.. ints];
            sortedInts.Sort();
        }

        [TestMethod()]
        public void InsertionSortTest()
        {
            var result = Sorting.InsertionSort(ints);
            Assert.That.SequencesEqual(sortedInts, result);
        }

        [TestMethod()]
        public void BubbleSortTest()
        {
            var result = Sorting.BubbleSort(ints);
            Assert.That.SequencesEqual(sortedInts, result);
        }

        [TestMethod()]
        public void MergeSortTest()
        {
            var result = Sorting.MergeSort(ints);
            Assert.That.SequencesEqual(sortedInts, result);
        }
    }
}