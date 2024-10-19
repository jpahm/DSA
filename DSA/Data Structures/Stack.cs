using System.Collections;

namespace DSA
{
    /// <summary>
    /// Represents a basic array-based LIFO stack.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T>(int capacity): ICloneable, ICollection
    {
        private readonly T[] InternalArr = new T[capacity];
        private int Position = -1;

        public int Count => Position + 1;
        public bool IsEmpty => Position == -1;
        public bool IsFull => Position == InternalArr.Length - 1;
        public bool IsSynchronized => InternalArr.IsSynchronized;
        public object SyncRoot => InternalArr.SyncRoot;

        public int Push(T item)
        {
            if (Position + 1 >= InternalArr.Length)
                throw new IndexOutOfRangeException("Stack is full!");
            InternalArr[++Position] = item;
            return Position;
        }
        public T Pop()
        {
            return Position < 0 ? throw new IndexOutOfRangeException("Stack is empty!") : InternalArr[Position--];
        }
        public T Peek()
        {
            return Position < 0 ? throw new IndexOutOfRangeException("Stack is empty!") : InternalArr[Position];
        }

        public object Clone()
        {
            return InternalArr.Clone();
        }

        public void CopyTo(Array array, int index)
        {
            InternalArr.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = Position; i >= 0; --i)
                yield return InternalArr[i];
        }
    }
}
