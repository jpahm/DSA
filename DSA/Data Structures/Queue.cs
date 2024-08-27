namespace DSA
{
    /// <summary>
    /// Represents a basic array-based FIFO queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T>(int capacity)
    {
        private readonly T[] InternalArr = new T[capacity];
        private int Front = -1;
        private int Rear = -1;

        public int Size => IsEmpty ? 0 : Rear - Front + 1;
        public bool IsFull => Rear == InternalArr.Length - 1;
        public bool IsEmpty => Front == -1;

        public int Enqeue(T item)
        {
            // Check whether queue is full
            if (IsFull)
                throw new IndexOutOfRangeException("Queue is full!");

            if (Rear == -1)
                Front = 0;

            InternalArr[++Rear] = item;
            return Rear;
        }
        public T Dequeue()
        {
            // Check whether queue is empty
            if (IsEmpty)
                throw new IndexOutOfRangeException("Queue is empty!");

            T val = InternalArr[Front];

            // We need to reset the queue if we reach the end of the array
            if (Front == InternalArr.Length - 1)
            {
                Front = -1;
                Rear = -1;
            }
            else
            {
                Front++;
            }

            return val;
        }
        public T Peek()
        {
            // Check whether queue is empty
            return IsEmpty ? throw new IndexOutOfRangeException("Queue is empty!") : InternalArr[Front];
        }
    }
}
