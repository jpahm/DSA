using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA
{
    public static partial class Sorting
    {
        public static IEnumerable<T> InsertionSort<T>(IEnumerable<T> collection) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            // Start from the second element and insert backwards
            for (int i = 1; i < array.Length; ++i)
            {
                T key = array[i];
                int j = i - 1;
                // Shift preceding elements forward until we find a place to insert
                for (; j >= 0 && array[j].CompareTo(key) > 0; --j)
                    array[j + 1] = array[j];
                array[j + 1] = key;
            }
            return array;
        }
    }
}
