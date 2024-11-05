using System;
using System.Collections;

namespace DSA
{
    // Represents a node in a linked list.
    public class LinkedListNode<T>(T value)
    {
        public T Value { get; set; } = value;
        public LinkedListNode<T>? Next { get; set; }
    }

    // Represents a basic singly-linked list.
    public class LinkedList<T> : IList<T>
    {
        public LinkedListNode<T>? Head { get; private set; }
        public LinkedListNode<T>? Tail { get; private set; }

        public LinkedList() { }

        public bool IsEmpty => Head == null;
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public void Add(T value)
        {
            if (Tail is null)
            {
                Head = Tail = new(value);
            }
            else
            {
                Tail.Next = new(value);
                Tail = Tail.Next;
            }
            Count++;
        }

        public void AddStart(T value)
        {
            if (Head is null)
            {
                Head = Tail = new(value);
            }
            else
            {
                LinkedListNode<T> newStart = new(value)
                {
                    Next = Head
                };
                Head = newStart;
            }
            Count++;
        }

        public void RemoveEnd()
        {
            if (Head is null)
                return;

            LinkedListNode<T>? temp = Head;
            while (temp.Next?.Next != null)
                temp = temp.Next;

            if (temp.Next is null)
                Head = Tail = null;
            else
            {
                temp.Next = null;
                Tail = temp;
            }

            Count--;
        }

        public void RemoveStart()
        {
            if (Head is null)
                return;

            Head = Head.Next;
            if (Head?.Next is null)
                Tail = Head;

            Count--;
        }

        public bool Remove(T item)
        {
            LinkedListNode<T>? prev = null;
            for (LinkedListNode<T>? temp = Head; temp != null; prev = temp, temp = temp.Next)
            {
                if ((temp.Value is null && item is null) || (temp.Value is not null && temp.Value.Equals(item)))
                {
                    if (prev is null)
                        Head = temp.Next;
                    else
                        prev.Next = temp.Next;

                    if (temp == Tail)
                        Tail = prev;

                    Count--;
                    return true;
                }
            }
            return false;
        }

        public T GetAt(int index)
        {
            if (index < 0)
                index = Count + index;

            LinkedListNode<T>? temp = Head;
            for (int i = 0; temp != null && i < index; ++i)
                temp = temp.Next;

            if (temp == null)
                throw new ArgumentOutOfRangeException(nameof(index));
            else
                return temp.Value;
        }

        public void SetAt(int index, T value)
        {
            if (index < 0)
                index = Count + index;

            LinkedListNode<T>? temp = Head;
            for (int i = 0; temp != null && i < index; ++i)
                temp = temp.Next;

            if (temp == null)
                throw new ArgumentOutOfRangeException(nameof(index));
            else
                temp.Value = value;
        }

        public void Insert(int index, T value)
        {
            if (index < 0)
                index = Count + index;

            LinkedListNode<T>? prev = null;
            LinkedListNode<T>? temp = Head;

            int i = 0;
            for (; temp != null && i < index; ++i)
            {
                prev = temp;
                temp = temp.Next;
            }

            if (temp == null && i < index)
                throw new ArgumentOutOfRangeException(nameof(index));

            LinkedListNode<T> newNode = new(value);
            if (prev is not null)
                prev.Next = newNode;
            else
                Head = newNode;

            newNode.Next = temp;

            if (newNode.Next is null)
                Tail = newNode;

            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0)
                index = Count + index;

            LinkedListNode<T>? prev = null;
            LinkedListNode<T>? temp = Head;
            for (int i = 0; temp != null && i < index; ++i)
            {
                prev = temp;
                temp = temp.Next;
            }

            if (temp == null)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (prev is not null)
                prev.Next = temp.Next;
            else
                Head = temp.Next;

            if (temp.Next is null)
                Tail = prev;

            Count--;
        }

        public int IndexOf(T item)
        {
            int index = 0;
            for (LinkedListNode<T>? temp = Head; temp != null; temp = temp.Next, ++index)
            {
                if (temp.Value is null && item is null)
                    return index;
                else if (temp.Value is not null && temp.Value.Equals(item))
                    return index;
            }
            return -1;
        }

        public void Clear() {
            Head = Tail = null;
            Count = 0;
        }

        public bool Contains(T item) => IndexOf(item) != -1;

        public void CopyTo(T[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array, nameof(array));
            ArgumentOutOfRangeException.ThrowIfNegative(arrayIndex, nameof(arrayIndex));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arrayIndex, array.Length, nameof(arrayIndex));
            
            if (array.Length < arrayIndex + Count)
                throw new ArgumentException("Array is not large enough!", nameof(array));

            for (LinkedListNode<T>? temp = Head; temp != null; temp = temp.Next, ++arrayIndex)
                array[arrayIndex] = temp.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (LinkedListNode<T>? node = Head; node != null; node = node.Next)
                yield return node.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T this[int idx]
        {
            get => GetAt(idx);
            set => SetAt(idx, value);
        }
    }
}
