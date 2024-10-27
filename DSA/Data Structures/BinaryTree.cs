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
    public class BinaryNode<T>(T value)
    {
        public T Value { get; set; } = value;
        public BinaryNode<T>? Left { get; set; }
        public BinaryNode<T>? Right { get; set; }
    }

    public class BinaryTree<T> : ICollection<T>
    {
        public BinaryNode<T>? Root { get; set; }

        public BinaryTree() {}

        public BinaryTree(T root)
        {
            Add(root);
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var newNode = new BinaryNode<T>(item);

            if (Root is null)
                Root = newNode;
            else
            {
                // Perform BFS to assign new node to first non-full node
                foreach (var node in GetBreadthEnumerator(Root))
                {
                    if (node.Left is null)
                    {
                        node.Left = newNode;
                        break;
                    }
                    if (node.Right is null)
                    {
                        node.Right = newNode;
                        break;
                    }
                }
            }

            Count++;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            foreach (var node in GetBreadthEnumerator(Root))
            {
                if (EqualityComparer<T>.Default.Equals(node.Value, item))
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var node in GetInOrderEnumerator(Root))
                array[arrayIndex++] = node.Value;
        }

        public IEnumerable<BinaryNode<T>> GetInOrderEnumerator(BinaryNode<T>? node)
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

        public IEnumerable<BinaryNode<T>> GetPreOrderEnumerator(BinaryNode<T>? node)
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

        public IEnumerable<BinaryNode<T>> GetPostOrderEnumerator(BinaryNode<T>? node)
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

        public IEnumerable<BinaryNode<T>> GetBreadthEnumerator(BinaryNode<T>? node)
        {
            if (node is null)
                yield break;

            Queue<BinaryNode<T>?> nodeQueue = new(Count);
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

        public IEnumerator<T> GetEnumerator()
        {
            if (Root is null)
                yield break;
            else 
                foreach (var n in GetInOrderEnumerator(Root))
                    yield return n.Value;
        }

        /// <summary>
        /// Removes the specified item from the tree. Assumes the tree is being used as a Binary Search Tree (BST) with ordering.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>Whether the item was found and removed or not.</returns>
        public bool Remove(T item)
        {
            // TODO: Remove, assuming BST ordering; maybe make dedicated BST class instead?
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
