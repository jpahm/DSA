namespace DSA
{
    /// <summary>
    /// Represents a basic array-based LIFO stack.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T>(int capacity)
    {
        private readonly T[] InternalArr = new T[capacity];
        private int Position = -1;

        public int Size => Position + 1;
        public bool IsEmpty => Position == -1;
        public bool IsFull => Position == InternalArr.Length - 1;

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
    }
}
