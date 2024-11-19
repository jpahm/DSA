using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Tests
{
    internal static class AssertionExtensions
    {
        /// <summary>
        /// Asserts that two sequences are equal; that is, they have the same length and have the same values in the same positions.
        /// </summary>
        /// <typeparam name="T">The type stored in the two sequences.</typeparam>
        /// <param name="expected">The expected sequence to test against.</param>
        /// <param name="actual">The actual sequence being tested.</param>
        public static void SequencesEqual<T>(this Assert _, IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.AreEqual(expected.Count(), actual.Count());
            for (int i = 0; i < expected.Count(); ++i)
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
        }
    }
}
