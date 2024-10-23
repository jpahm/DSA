using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace DSA
{
    /// <summary>
    /// Represents a basic hash table implementation utilizing multiplicative hashing and chaining with linked lists.
    /// </summary>
    /// <typeparam name="KeyType">The type to key on.</typeparam>
    /// /// <typeparam name="ValueType">The type to store.</typeparam>
    public class HashTable<KeyType, ValueType> : IDictionary<KeyType, ValueType> where KeyType : notnull
    {
        public ValueType this[KeyType key] { get => GetValueAt(key); set => Add(key, value); }

        public ImmutableArray<KeyType> Keys => _buckets.SelectMany(bucket => bucket?.Select(x => x.Key) ?? []).ToImmutableArray();
        public ImmutableArray<ValueType> Values => _buckets.SelectMany(bucket => bucket?.Select(x => x.Value) ?? []).ToImmutableArray();

        ICollection<KeyType> IDictionary<KeyType, ValueType>.Keys => Keys;
        ICollection<ValueType> IDictionary<KeyType, ValueType>.Values => Values;

        public int Count { get; private set; } = 0;

        public int Capacity => (int)Math.Pow(2, _sizeLog2);

        public bool IsReadOnly => false;

        private LinkedList<KeyValuePair<KeyType, ValueType>>?[] _buckets;

        /// <summary>
        /// The maximum amount of collisions allowed before the table attempts to resize.
        /// </summary>
        public int MaxAllowedCollisions { get; set; } = 10;

        /// <summary>
        /// The minimum clustering factor required before a resize will be allowed.
        /// </summary>
        public double MaxAllowedClustering { get; set; } = 2.0;

        /// <summary>
        /// The default capacity of the map implementation in log2.
        /// </summary>
        public const int DEFAULT_SIZE_LOG2 = 5;

        private int _sizeLog2 = DEFAULT_SIZE_LOG2;

        private uint _collisions;

        /// <summary>
        /// Creates an empty hashtable.
        /// </summary>
        public HashTable()
        {
            _buckets = new LinkedList<KeyValuePair<KeyType, ValueType>>[1 << _sizeLog2];
        }

        /// <summary>
        /// Creates a hashtable with the specified initial bucket capacity. Note this is NOT the element capacity; it is only the initial number of buckets.
        /// </summary>
        /// <param name="capacity">The initial bucket capacity.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public HashTable(uint capacity)
        {
            _sizeLog2 = Math.Clamp((int)Math.Ceiling(Math.Log2(capacity)), 2, 31);
            _buckets = new LinkedList<KeyValuePair<KeyType, ValueType>>[1 << _sizeLog2];
        }

        /// <summary>
        /// Computes the clustering level of the table; used to determine whether to resize the table or not.
        /// See https://www.cs.cornell.edu/courses/cs3110/2008fa/lectures/lec21.html for some details.
        /// </summary>
        /// <returns>The clustering factor. < 0 indicates better than even distribution, 0 indicates even distribution, > 0 indicates worse than even distribution</returns>
        public double GetClustering()
        {
            if (Count == 0)
                return 0;
            double sumOfSizesSquared = _buckets.Sum(x => x is null ? 0 : (int)Math.Pow(x.Count, 2));
            //int numUsedBuckets = _buckets.Count(x => x is not null);
            return (((double)Count / Capacity) - 1) * ((sumOfSizesSquared / Capacity) - 1);
        }

        /// <summary>
        /// Resizes the hashtable until the clustering level is no worse than MaxAllowedClustering.
        /// </summary>
        /// <returns>Whether the resize attempt was successful.</returns>
        private bool TryResize()
        {
            // Reset collision counter
            _collisions = 0;

            // Only resize if we're not at max capacity and clustering is bad enough
            if (_sizeLog2 >= 31 || GetClustering() <= MaxAllowedClustering)
                return false;

            _sizeLog2++;
            var newBuckets = new LinkedList<KeyValuePair<KeyType, ValueType>>[1 << _sizeLog2];

            // Re-hash old buckets to new buckets, keeping track of collisions and resizing more if needed
            foreach (var bucket in _buckets)
            {
                if (bucket == null)
                    continue;
                foreach (var kvp in bucket)
                {
                    uint idx = GetBucketIndex(kvp.Key);
                    ref var newBucket = ref newBuckets[idx];
                    if (newBucket == null)
                        newBucket = [kvp];
                    else
                    {
                        newBucket.Add(kvp);
                        _collisions++;
                        // Recursively resize further if necessary
                        if (_collisions > MaxAllowedCollisions && TryResize())
                            return true;
                    }
                }
            }
            _buckets = newBuckets;
            return true;
        }

        /// <summary>
        /// Knuth's suggested multiplier; 2^32 * Phi (Golden Ratio) -- chosen for good distribution
        /// </summary>
        private const uint _hashMultiplier = 0x9E3779B1;

        /// <summary>
        /// Uses a multiplicative hash function to compute the bucket index of the specified key. 
        /// See https://www.cs.cornell.edu/courses/cs3110/2008fa/lectures/lec21.html for some details.
        /// </summary>
        /// <param name="key">The key to be hashed.</param>
        /// <returns>The key's associated bucket index.</returns>
        private uint GetBucketIndex(KeyType key)
        {
            return (uint)key.GetHashCode() * _hashMultiplier >> (32 - _sizeLog2); // Get top log2(_buckets.Length) bits of key * _hashMultiplier
        }

        private ValueType GetValueAt(KeyType key)
        {
            var bucket = _buckets[GetBucketIndex(key)] ?? throw new KeyNotFoundException();
            try
            {
                return GetValueInBucket(key, bucket);
            } catch
            {
                throw new KeyNotFoundException();
            }
        }

        private ValueType GetValueInBucket(KeyType key, LinkedList<KeyValuePair<KeyType, ValueType>> bucket)
        {
            return bucket.First(x => EqualityComparer<KeyType>.Default.Equals(x.Key, key)).Value;
        }

        public void Add(KeyType key, ValueType value)
        {
            uint idx = GetBucketIndex(key);
            ref var bucket = ref _buckets[idx];
            if (bucket == null)
            {
                bucket = [new(key, value)];
                Count++;
            }
            else
            {
                // Check to see whether we're just replacing an existing value or if there's a collision
                bool replaced = false;
                for (Node<KeyValuePair<KeyType, ValueType>>? node = bucket.Head; node != null; node = node.Next)
                {
                    if (EqualityComparer<KeyType>.Default.Equals(key, node.Value.Key))
                    {
                        node.Value = new(key, value);
                        replaced = true;
                        break;
                    }
                }
                if (!replaced)
                {
                    // Add a new item to the bucket if it's a collision
                    bucket.Add(new(key, value));
                    _collisions++;
                    Count++;

                    // Try to resize if necessary
                    if (_collisions > MaxAllowedCollisions)
                        TryResize();
                }
            }
        }

        public void Add(KeyValuePair<KeyType, ValueType> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _sizeLog2 = DEFAULT_SIZE_LOG2;
            _buckets = new LinkedList<KeyValuePair<KeyType, ValueType>>[1 << _sizeLog2];
            Count = 0;
        }

        public bool Contains(KeyValuePair<KeyType, ValueType> item)
        {
            var bucket = _buckets[GetBucketIndex(item.Key)];
            return bucket != null && bucket.Contains(item);
        }

        public bool ContainsKey(KeyType key)
        {
            var bucket = _buckets[GetBucketIndex(key)];
            return bucket != null && bucket.Any(x => EqualityComparer<KeyType>.Default.Equals(x.Key, key));
        }

        public void CopyTo(KeyValuePair<KeyType, ValueType>[] array, int arrayIndex)
        {
            foreach (var bucket in _buckets)
            {
                if (bucket == null)
                    continue;
                bucket.CopyTo(array, arrayIndex);
                arrayIndex += bucket.Count;
            }
        }

        public IEnumerator<KeyValuePair<KeyType, ValueType>> GetEnumerator()
        {
            foreach (var bucket in _buckets.Where(bucket => bucket != null))
                foreach (KeyValuePair<KeyType, ValueType> kvp in bucket!)
                    yield return kvp;
        }

        public bool Remove(KeyType key)
        {
            ref var bucket = ref _buckets[GetBucketIndex(key)];
            if (bucket == null)
                return false;

            if (bucket.Remove(bucket.First(x => EqualityComparer<KeyType>.Default.Equals(x.Key, key))))
            {
                if (bucket.Count == 0)
                    bucket = null;
                Count--;
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<KeyType, ValueType> item)
        {
            ref var bucket = ref _buckets[GetBucketIndex(item.Key)];
            if (bucket == null)
                return false;

            if (bucket.Remove(item))
            {
                if (bucket.Count == 0)
                    bucket = null;
                Count--;
                return true;
            }
            return false;
        }

        public bool TryGetValue(KeyType key, [MaybeNullWhen(false)] out ValueType value)
        {
            try
            {
                value = GetValueAt(key);
                return true;
            } catch
            {
                value = default;
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
