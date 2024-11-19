using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Tests
{
    internal static class Utilities
    {
        /// <summary>
        /// Randomizes the given integer array.
        /// </summary>
        /// <param name="array">The array to randomize.</param>
        public static void RandomizeArray(int[] array)
        {
            for (int i = 0; i < array.Length; ++i)
                array[i] = Random.Shared.Next(int.MinValue, int.MaxValue);
        }
    }
}
