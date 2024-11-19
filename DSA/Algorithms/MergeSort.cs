using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA
{
    public static partial class Sorting
    {
        public static IEnumerable<T> MergeSort<T>(IEnumerable<T> collection) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            MergeSortRecursive(array, 0, array.Length - 1);
            return array;
        }

        private static void MergeSortRecursive<T>(T[] array, int startIdx, int endIdx) where T : IComparable<T>
        {
            if (startIdx >= endIdx)
                return;
            int midIdx = (startIdx + endIdx) / 2;
            // Sort left half
            MergeSortRecursive(array, startIdx, midIdx);
            // Sort right half
            MergeSortRecursive(array, midIdx + 1, endIdx);
            // Merge halves
            Merge(array, startIdx, midIdx, endIdx);
        }

        private static void Merge<T>(T[] array, int startIdx, int midIdx, int endIdx) where T : IComparable<T>
        {
            int sizeLeft = midIdx - startIdx + 1;
            int sizeRight = endIdx - midIdx;

            // Make copies of the left and right regions
            T[] left = new T[sizeLeft];
            T[] right = new T[sizeRight];
            Array.Copy(array, startIdx, left, 0, sizeLeft);
            Array.Copy(array, midIdx + 1, right, 0, sizeRight);

            // Merge the smallest item from left/right until one of them runs out
            int leftIdx = 0, rightIdx = 0, sourceIdx = startIdx;
            while (leftIdx < sizeLeft && rightIdx < sizeRight)
            {
                if (left[leftIdx].CompareTo(right[rightIdx]) <= 0)
                    array[sourceIdx++] = left[leftIdx++];
                else
                    array[sourceIdx++] = right[rightIdx++];
            }

            // Once one of left or right is empty, merge all remaining from other
            while (leftIdx < sizeLeft)
                array[sourceIdx++] = left[leftIdx++];
            while (rightIdx < sizeRight)
                array[sourceIdx++] = right[rightIdx++];
        }
    }
}
