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
    public class AVLNode<T>(T Value) : IBinaryNode<T>
    {
        public T Value { get; set; } = Value;
        public int Count { get; set; } = 1;
        public int Height { get; set; }
        public AVLNode<T>? Left { get; set; }
        public AVLNode<T>? Right { get; set; }

        IBinaryNode<T>? IBinaryNode<T>.Left
        {
            get { return Left; }
            set { Left = (AVLNode<T>?)value; }
        }
        IBinaryNode<T>? IBinaryNode<T>.Right
        {
            get { return Right; }
            set { Right = (AVLNode<T>?)value; }
        }
    }
    /// <summary>
    /// Implementation of an AVL self-balancing BST. Supports duplicate values via counting.
    /// </summary>
    /// <typeparam name="T">The type of value to store in the BST.</typeparam>
    public class AVLTree<T> : BinaryTree<T> where T : IComparable<T>
    {
        public AVLNode<T>? Root { get; protected set; }

        private void BalanceSubtree(AVLNode<T> root)
        {
            int balanceFactor = (root.Left?.Height ?? 0) - (root.Right?.Height ?? 0);
            if (balanceFactor > 1)
            {
                // Right subtree is too small

                // Calculate balance factor of left subtree
                balanceFactor = (root.Left!.Left?.Height ?? 0) - (root.Left!.Right?.Height ?? 0);
                // If left subtree is right-heavy, rotate it left first
                if (balanceFactor < 0)
                    RotateLeft(root.Left!.Right!, root.Left);
                RotateRight(root.Left, root);
            }
            else if (balanceFactor < -1)
            {
                // Left subtree is too small

                // Calculate balance factor of right subtree
                balanceFactor = (root.Right!.Left?.Height ?? 0) - (root.Right!.Right?.Height ?? 0);
                // If right subtree is left-heavy, rotate it right first
                if (balanceFactor > 0)
                    RotateRight(root.Right!.Left!, root.Right);
                RotateLeft(root.Right, root);
            }
        }

        public override void Add(T item)
        {
            if (Root is null)
                Root = new AVLNode<T>(item);
            else
                AddRecursive(item, Root);

            Count++;
        }

        private void AddRecursive(T item, AVLNode<T> node)
        {
            switch (item.CompareTo(node.Value))
            {
                case -1:
                    if (node.Left is null)
                        node.Left = new AVLNode<T>(item);
                    else
                        AddRecursive(item, node.Left);
                    break;
                case 0:
                    node.Count++;
                    return;
                case 1:
                    if (node.Right is null)
                        node.Right = new AVLNode<T>(item);
                    else
                        AddRecursive(item, node.Right);
                    break;
            }
            
            // Update height and balance
            node.Height = 1 + Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0);
            BalanceSubtree(node);
        }

        public override void Clear()
        {
            Root = null;
            Count = 0;
        }

        public AVLNode<T>? Find(T item)
        {
            if (Root is null)
                return null;
            return FindRecursive(item, Root);
        }

        private static AVLNode<T>? FindRecursive(T item, AVLNode<T> node)
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
            if (Root is null)
                return false;
            return RemoveRecursive(item, Root);
        }

        private bool RemoveRecursive(T item, AVLNode<T> node, AVLNode<T>? parentNode = null)
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
                } else if (hasLeftChild && hasRightChild)
                {
                    // Node has two children, replace the node with its inorder successor (next greater value)
                    AVLNode<T> successor = node.Right!;
                    AVLNode<T> successorParent = node;
                    while (successor.Left != null)
                    {
                        successorParent = successor;
                        successor = successor.Left;
                    }
                    successorParent.Left = null;
                    node.Value = successor.Value;
                    node.Count = successor.Count;
                } else
                {
                    // Node only has one child, replace accordingly
                    if (hasLeftChild)
                    {
                        node.Value = node.Left!.Value;
                        node.Count = node.Left!.Count;
                        node.Left = null;
                    }
                    else
                    {
                        node.Value = node.Right!.Value;
                        node.Count = node.Right!.Count;
                        node.Right = null;
                    }
                }

                // Update height and balance
                node.Height = 1 + Math.Max(node.Left?.Height ?? 0, node.Right?.Height ?? 0);
                BalanceSubtree(node);

                return true;
            }
        }

        private void RotateLeft(AVLNode<T> node, AVLNode<T>? parentNode = null)
        {
            if (node.Right is null)
                return;

            if (node.Right.Left is not null)
                node.Right = node.Right.Left;

            if (parentNode is null)
                Root = node.Right;
            else if (parentNode.Left == node)
                parentNode.Left = node.Right;
            else
                parentNode.Right = node.Right;

            node.Right.Left = node;
        }

        private void RotateRight(AVLNode<T> node, AVLNode<T>? parentNode = null)
        {
            if (node.Left is null)
                return;

            if (node.Left.Right is not null)
                node.Left = node.Left.Right;

            if (parentNode is null)
                Root = node.Left;
            else if (parentNode.Right == node)
                parentNode.Right = node.Left;
            else
                parentNode.Left = node.Left;

            node.Left.Right = node;
        }
    }
}
