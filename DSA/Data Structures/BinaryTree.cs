using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DSA
{
    public interface IBinaryNode<T>
    {
        public T Value { get; set; }
        public IBinaryNode<T>? Left { get; set; }
        public IBinaryNode<T>? Right { get; set; }
    }

    /// <summary>
    /// Abstract class describing the basis for all binary trees.
    /// </summary>
    /// <typeparam name="T">The type of item stored in the tree.</typeparam>
    public abstract class BinaryTree<T> : ICollection<T>
    {
        public BinaryTree() {}

        public BinaryTree(T root)
        {
            Add(root);
        }

        public int Count { get; protected set; }

        public bool IsReadOnly => false;

        public abstract void Add(T item);

        public abstract void Clear();

        public abstract bool Contains(T item);

        public abstract void CopyTo(T[] array, int arrayIndex);

        public IEnumerable<IBinaryNode<T>> GetInOrderEnumerator(IBinaryNode<T>? node)
        {
            if (node is null)
                yield break;

            if (node.Left is not null)
                foreach (var n in GetInOrderEnumerator(node.Left))
                    yield return n;

            yield return node;

            if (node.Right is not null)
                foreach (var n in GetInOrderEnumerator(node.Right))
                    yield return n;
        }

        public IEnumerable<IBinaryNode<T>> GetPreOrderEnumerator(IBinaryNode<T>? node)
        {
            if (node is null)
                yield break;

            yield return node;

            if (node.Left is not null)
                foreach (var n in GetPreOrderEnumerator(node.Left))
                    yield return n;

            if (node.Right is not null)
                foreach (var n in GetPreOrderEnumerator(node.Right))
                    yield return n;
        }

        public IEnumerable<IBinaryNode<T>> GetPostOrderEnumerator(IBinaryNode<T>? node)
        {
            if (node is null)
                yield break;

            if (node.Left is not null)
                foreach (var n in GetPostOrderEnumerator(node.Left))
                    yield return n;

            if (node.Right is not null)
                foreach (var n in GetPostOrderEnumerator(node.Right))
                    yield return n;

            yield return node;
        }

        public IEnumerable<IBinaryNode<T>> GetBreadthEnumerator(IBinaryNode<T>? node)
        {
            if (node is null)
                yield break;

            Queue<IBinaryNode<T>?> nodeQueue = new(Count);
            nodeQueue.Enqueue(node);

            while (!nodeQueue.IsEmpty)
            {
                var nextNode = nodeQueue.Dequeue();
                if (nextNode is null)
                    yield break;
                else
                    yield return nextNode;

                if (nextNode.Left is not null)
                    nodeQueue.Enqueue(nextNode.Left);
                if (nextNode.Right is not null)
                    nodeQueue.Enqueue(nextNode.Right);
            }
        }

        public abstract IEnumerator<T> GetEnumerator();

        public abstract bool Remove(T item);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
