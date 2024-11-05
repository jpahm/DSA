using System;
using System.Collections;

namespace DSA
{
    // Represents a node in a linked list.
    public class DoublyLinkedListNode<T>(T value)
    {
        public T Value { get; set; } = value;
        public DoublyLinkedListNode<T>? Next { get; set; }
        public DoublyLinkedListNode<T>? Previous { get; set; }
    }

    // Represents a basic singly-linked list.
    public class DoublyLinkedList<T> : IList<T>
    {
        public DoublyLinkedListNode<T>? Head { get; private set; }
        public DoublyLinkedListNode<T>? Tail { get; private set; }

        public DoublyLinkedList() { }

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
                var oldTail = Tail;
                Tail = new(value)
                {
                    Previous = oldTail
                };
                oldTail.Next = Tail;
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
                DoublyLinkedListNode<T> newStart = new(value)
                {
                    Next = Head
                };
                Head = newStart;
            }
            Count++;
        }

        public void RemoveEnd()
        {
            if (Tail is null)
                return;

            if (Tail.Previous is null)
                Head = Tail = null;
            else
            {
                Tail.Previous.Next = null;
                Tail = Tail.Previous;
            }

            Count--;
        }

        public void RemoveStart()
        {
            if (Head is null)
                return;

            // Move head forward
            Head = Head.Next;

            // Set previous to null if new head isn't null
            if (Head is not null)
                Head.Previous = null;

            // Assign tail to new head if necessary
            if (Head?.Next is null)
                Tail = Head;

            Count--;
        }

        public bool Remove(T item)
        {
            for (DoublyLinkedListNode<T>? temp = Head; temp != null; temp = temp.Next)
            {
                if ((temp.Value is null && item is null) || (temp.Value is not null && temp.Value.Equals(item)))
                {
                    // Update forward ref or set new head
                    if (temp.Previous is null)
                        Head = temp.Next;
                    else
                        temp.Previous.Next = temp.Next;
                    
                    // Update backwards ref or set new tail
                    if (temp.Next is not null)
                        temp.Next.Previous = temp.Previous;
                    else
                        Tail = temp.Previous;

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

            DoublyLinkedListNode<T>? temp;
            // Start search from tail end if we're looking for an index more than halfway through
            if (index > Count / 2)
            {
                temp = Tail;
                for (int i = Count - 1; temp != null && i > index; --i)
                    temp = temp.Previous;
            }
            // Otherwise, start from head
            else
            {
                temp = Head;
                for (int i = 0; temp != null && i < index; ++i)
                    temp = temp.Next;
            }

            if (temp == null)
                throw new ArgumentOutOfRangeException(nameof(index));
            else
                return temp.Value;
        }

        public void SetAt(int index, T value)
        {
            if (index < 0)
                index = Count + index;

            DoublyLinkedListNode<T>? temp;
            // Start search from tail end if we're looking for an index more than halfway through
            if (index > Count / 2)
            {
                temp = Tail;
                for (int i = Count - 1; temp != null && i > index; --i)
                    temp = temp.Previous;
            }
            // Otherwise, start from head
            else
            {
                temp = Head;
                for (int i = 0; temp != null && i < index; ++i)
                    temp = temp.Next;
            }

            if (temp == null)
                throw new ArgumentOutOfRangeException(nameof(index));
            else
                temp.Value = value;
        }

        public void Insert(int index, T value)
        {
            if (index < 0)
                index = Count + index;

            DoublyLinkedListNode<T>? prev;
            DoublyLinkedListNode<T>? temp;

            int i;
            // Start search from tail end if we're looking for an index more than halfway through
            if (index > Count / 2)
            {
                temp = null;
                prev = Tail;
                for (i = Count; i > index; --i)
                {
                    temp = prev;
                    prev = temp?.Previous;
                }
            }
            // Otherwise, start from head
            else
            {
                temp = Head;
                prev = null;
                for (i = 0; temp != null && i < index; ++i)
                {
                    prev = temp;
                    temp = temp.Next;
                }
            }

            if (temp == null && i < index)
                throw new ArgumentOutOfRangeException(nameof(index));

            DoublyLinkedListNode<T> newNode = new(value);
            if (prev is not null)
                prev.Next = newNode;
            else
                Head = newNode;

            newNode.Next = temp;
            newNode.Previous = prev;

            if (newNode.Next is null)
                Tail = newNode;
            else
                newNode.Next.Previous = temp;

            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0)
                index = Count + index;

            DoublyLinkedListNode<T>? prev;
            DoublyLinkedListNode<T>? temp;

            // Start search from tail end if we're looking for an index more than halfway through
            if (index > Count / 2)
            {
                temp = null;
                prev = Tail;
                for (int i = Count; i > index; --i)
                {
                    temp = prev;
                    prev = temp?.Previous;
                }
            }
            // Otherwise, start from head
            else
            {
                temp = Head;
                prev = null;
                for (int i = 0; temp != null && i < index; ++i)
                {
                    prev = temp;
                    temp = temp.Next;
                }
            }

            if (temp == null)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (prev is not null)
                prev.Next = temp.Next;
            else
                Head = temp.Next;

            if (temp.Next is null)
                Tail = prev;
            else
                temp.Next.Previous = prev;

            Count--;
        }

        public int IndexOf(T item)
        {
            int index = 0;
            for (DoublyLinkedListNode<T>? temp = Head; temp != null; temp = temp.Next, ++index)
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

            for (DoublyLinkedListNode<T>? temp = Head; temp != null; temp = temp.Next, ++arrayIndex)
                array[arrayIndex] = temp.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (DoublyLinkedListNode<T>? node = Head; node != null; node = node.Next)
                yield return node.Value;
        }

        public IEnumerable<T> GetBackwardsEnumerable()
        {
            for (DoublyLinkedListNode<T>? node = Tail; node != null; node = node.Previous)
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
