using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DSA.Tests
{
    [TestClass()]
    public class HashTableTests
    {
        private HashTable<int, int> table = [];

        [TestInitialize]
        public void Init()
        {
            table = [];
        }

        [TestMethod()]
        public void InitTest()
        {
            // Table should be empty and not read-only
            Assert.AreEqual(0, table.Count);
            Assert.IsFalse(table.IsReadOnly);

            foreach (var kvp in table)
                Assert.Fail("Table was not empty on initializtion.");

            int randomKey = Random.Shared.Next();

            // Trying to find or remove a value should fail
            Assert.IsFalse(table.TryGetValue(randomKey, out int _));
            Assert.ThrowsException<KeyNotFoundException>(() => table[randomKey]);
            Assert.IsFalse(table.Remove(randomKey));
        }

        [TestMethod()]
        public void InitCapacityTest()
        {
            // Initialize table with 500 bucket capacity
            table = new HashTable<int, int>(500);
            Assert.AreEqual(0, table.Count);
            Assert.IsFalse(table.IsReadOnly);

            // Ensure capacity before clear is 2^9 (512)
            Assert.AreEqual(512, table.Capacity);
            table.Clear();
            // Ensure capacity after clear is less
            Assert.IsTrue(table.Capacity < 512);
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod()]
        public void AddGetContainsTest()
        {
            // Insert and test 1000 random elements
            for (int i = 0; i < 1000; i++)
            {
                // Get a unique, random key
                int key;
                do {
                    key = Random.Shared.Next();
                } while (table.ContainsKey(key));

                int val = Random.Shared.Next();

                // Alternate between adding via index and with Add()
                if (int.IsEvenInteger(i))
                    table[key] = val;
                else
                    table.Add(key, val);

                // Make sure the table updates appropriately
                Assert.AreEqual(i + 1, table.Count);
                Assert.AreEqual(val, table[key]);
                table.TryGetValue(key, out int outVal);
                Assert.AreEqual(val, outVal);

                // Make sure Contains and ContainsKey work properly
                Assert.IsTrue(table.ContainsKey(key));
                Assert.IsTrue(table.Contains(new(key, val)));
            }
        }

        [TestMethod()]
        public void ResizeTest()
        {
            long maxColls = table.MaxAllowedCollisions;
            double maxClustering = table.MaxAllowedClustering;
            long itemsToResize = (1 + maxColls) * (long)Math.Pow(2, HashTable<int, int>.DEFAULT_SIZE_LOG2);

            int capacityBefore = table.Capacity;

            // Generate enough collisions to cause a resize
            for (long i = 0; i < itemsToResize; ++i)
                table.Add(Random.Shared.Next(), Random.Shared.Next());

            // Ensure we have the correct number of elements
            Assert.AreEqual(itemsToResize, table.Count);
            // Ensure resize happened
            Assert.IsTrue(table.Capacity > capacityBefore);
            // Ensure we resized to within an acceptable clustering level
            Assert.IsTrue(table.GetClustering() <= maxClustering);

            // Ensure clearing shrinks the table
            capacityBefore = table.Capacity;
            table.Clear();
            Assert.IsTrue(table.Capacity < capacityBefore);
        }

        [TestMethod()]
        public void ClearTest()
        {
            Assert.AreEqual(0, table.Count);

            // Clearing an empty table shouldn't modify anything
            int capacityBefore = table.Capacity;
            table.Clear();
            Assert.AreEqual(0, table.Count);
            Assert.AreEqual(capacityBefore, table.Capacity);

            // Make sure table is actually empty via enumerator
            foreach (var _ in table)
                Assert.Fail("Table was not empty after clear.");
        }

        [TestMethod()]
        public void RemoveTest()
        {
            // Add 50 random entries, potentially overlapping
            for (int i = 0; i < 50; ++i)
                table.Add(Random.Shared.Next(), Random.Shared.Next());
            
            int countBefore = table.Count;
            int[] removedKeys = new int[5];

            // Remove 5 random entries
            for (int i = 0; i < 5; ++i)
            {
                Dictionary<int, int> j = [];
                int idx = Random.Shared.Next(table.Count);
                int keyToRemove = table.Keys[idx];
                removedKeys[i] = keyToRemove;
                Assert.IsTrue(table.Remove(keyToRemove));
            }
             
            // Make sure count decreased by 5
            Assert.AreEqual(countBefore - 5, table.Count);

            // Make sure all removed keys were actually removed
            foreach (int key in removedKeys)
            {
                Assert.IsFalse(table.ContainsKey(key));
                Assert.IsFalse(table.TryGetValue(key, out int _));
                Assert.ThrowsException<KeyNotFoundException>(() => table[key]);
            }
        }
    }
}