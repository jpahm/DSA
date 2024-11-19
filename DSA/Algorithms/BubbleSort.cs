using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA
{
    public static partial class Sorting
    {
        public static IEnumerable<T> BubbleSort<T>(IEnumerable<T> collection) where T : IComparable<T>
        {
            T[] array = collection.ToArray();
            // Outer loop keeps track of how many times we've "bubbled", aka how many elements are sorted
            for (int i = 0; i < array.Length; ++i)
            {
                bool didSwap = false;
                // "Bubble" largest element to the top via repeated swapping
                for (int j = 1; j < array.Length - i; ++j)
                {
                    if (array[j - 1].CompareTo(array[j]) > 0)
                    {
                        (array[j - 1], array[j]) = (array[j], array[j - 1]);
                        didSwap = true;
                    }
                }
                // Terminate early if we did no swapping/bubbling
                if (!didSwap)
                    break;
            }
            return array;
        }
    }
}
