namespace DSA
{
    /// <summary>
    /// Represents a circular array-based FIFO queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularQueue<T>(int capacity)
    {
        private readonly T[] InternalArr = new T[capacity];
        private int Front = -1;
        private int Rear = -1;

        public int Size => Front == -1 ? 0 : Rear < Front ?
            InternalArr.Length - (Front - Rear) + 1
            : Rear - Front + 1;
        public bool IsFull => Front == (Rear + 1) % InternalArr.Length;
        public bool IsEmpty => Front == -1;

        public int Enqeue(T item)
        {
            // Check whether queue is full
            if (IsFull)
                throw new IndexOutOfRangeException("Queue is full!");

            // Increment and wrap-around rear
            Rear++;
            Rear %= InternalArr.Length;

            // Update front to 0 on first enqueue
            if (Front == -1)
                Front = 0;

            InternalArr[Rear] = item;
            return Rear;
        }
        public T Dequeue()
        {
            // Check whether queue is empty
            if (IsEmpty)
                throw new IndexOutOfRangeException("Queue is empty!");

            T val = InternalArr[Front];

            // We can reset the queue if we dequeue the last element
            if (Front == Rear)
            {
                Front = -1;
                Rear = -1;
            }
            else
            {
                Front++;
                Front %= InternalArr.Length;
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
