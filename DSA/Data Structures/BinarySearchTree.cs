using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DSA
{
    public class BSTNode<T>(T Value) : IBinaryNode<T>
    {
        public T Value { get; set; } = Value;
        public int Count { get; set; } = 1;
        public BSTNode<T>? Left { get; set; }
        public BSTNode<T>? Right { get; set; }

        IBinaryNode<T>? IBinaryNode<T>.Left
        {
            get { return Left; }
            set { Left = (BSTNode<T>?)value; }
        }
        IBinaryNode<T>? IBinaryNode<T>.Right
        {
            get { return Right; }
            set { Right = (BSTNode<T>?)value; }
        }
    }
    /// <summary>
    /// Basic implementation of a BST. Supports duplicate values via counting.
    /// </summary>
    /// <typeparam name="T">The type of value to store in the BST.</typeparam>
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>
    {
        public BSTNode<T>? BSTRoot => (BSTNode<T>?)Root;

        public override void Add(T item)
        {
            if (BSTRoot is null)
                Root = new BSTNode<T>(item);
            else
                BinarySearchTree<T>.AddRecursive(item, BSTRoot);

            Count++;
        }

        private static void AddRecursive(T item, BSTNode<T> startNode)
        {
            switch (item.CompareTo(startNode.Value))
            {
                case -1:
                    if (startNode.Left is null)
                        startNode.Left = new BSTNode<T>(item);
                    else
                        BinarySearchTree<T>.AddRecursive(item, startNode.Left);
                    break;
                case 0:
                    startNode.Count++;
                    break;
                case 1:
                    if (startNode.Right is null)
                        startNode.Right = new BSTNode<T>(item);
                    else
                        BinarySearchTree<T>.AddRecursive(item, startNode.Right);
                    break;
            }
        }

        public BSTNode<T>? Find(T item)
        {
            if (BSTRoot is null)
                return null;
            return FindRecursive(item, BSTRoot);
        }

        private static BSTNode<T>? FindRecursive(T item, BSTNode<T> node)
        {
            int comparison = item.CompareTo(node.Value);
            if (comparison < 0)
            {
                if (node.Left is null)
                    return null;
                else
                    return FindRecursive(item, node.Left);
            }
            else if (comparison > 0)
            {
                if (node.Right is null)
                    return null;
                else
                    return FindRecursive(item, node.Right);
            }
            else return node;
        }

        public override bool Contains(T item)
        {
            return Find(item) != null;
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var node in GetInOrderEnumerator(Root))
                array[arrayIndex++] = node.Value;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            foreach (var node in GetInOrderEnumerator(Root))
                yield return node.Value;
        }

        public override bool Remove(T item)
        {
            if (BSTRoot is null)
                return false;
            return RemoveRecursive(item, BSTRoot);
        }

        private bool RemoveRecursive(T item, BSTNode<T> node, BSTNode<T>? parentNode = null)
        {
            int comparison = item.CompareTo(node.Value);
            if (comparison < 0)
            {
                if (node.Left is null)
                    return false;
                else
                    return RemoveRecursive(item, node.Left, node);
            }
            else if (comparison > 0)
            {
                if (node.Right is null)
                    return false;
                else
                    return RemoveRecursive(item, node.Right, node);
            }
            else
            {
                // This is where we do the actual removal
                Count--;

                // If this node has duplicates, just decrement the count if > 1
                if (node.Count > 1)
                {
                    node.Count--;
                    return true;
                }

                bool hasLeftChild = node.Left != null;
                bool hasRightChild = node.Right != null;

                if (!hasLeftChild && !hasRightChild)
                {
                    // Node has no children, just remove it from the parent (if there is one)
                    if (parentNode is null)
                        Root = null;
                    else if (parentNode.Left == node)
                        parentNode.Left = null;
                    else
                        parentNode.Right = null;
                } else if (hasRightChild)
                {
                    if (node.Right!.Left is not null) {
                        // Perform right rotation
                        node.Value = node.Right!.Left.Value;
                        node.Count = node.Right!.Left.Count!;
                        node.Right!.Left = null;
                    } else
                    {
                        // Just replace with right node
                        node.Value = node.Right!.Value;
                        node.Count = node.Right!.Count;
                        node.Right = node.Right!.Right;
                    }
                } else if (hasLeftChild)
                {
                    if (node.Left!.Right is not null)
                    {
                        // Perform left rotation
                        node.Value = node.Left!.Right.Value;
                        node.Count = node.Left!.Right.Count!;
                        node.Left!.Right = null;
                    }
                    else
                    {
                        // Just replace with left node
                        node.Value = node.Left!.Value;
                        node.Count = node.Left!.Count;
                        node.Left = node.Left!.Left;
                    }
                }
                return true;
            }
        }
    }
}
