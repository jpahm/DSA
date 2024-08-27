namespace DSA
{
    /// <summary>
    /// Represents a double-ended circular array-based FIFO queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Deque<T>(int capacity)
    {
        private readonly T[] InternalArr = new T[capacity];
        private int Front = -1;
        private int Rear = 0;

        public int Size => Front == -1 ? 0 : Rear < Front ?
            InternalArr.Length - (Front - Rear) + 1
            : Rear - Front + 1;
        public bool IsFull => Front == (Rear + 1) % InternalArr.Length;
        public bool IsEmpty => Front == -1;

        public int EnqeueRear(T item)
        {
            // Check whether queue is full
            if (IsFull)
                throw new IndexOutOfRangeException("Queue is full!");

            if (Front == -1)
            {
                Front = 0;
                Rear = 0;
            }
            else
            {
                // Increment and wrap-around rear
                Rear++;
                Rear %= InternalArr.Length;
            }

            InternalArr[Rear] = item;
            return Rear;
        }

        public int EnqeueFront(T item)
        {
            // Check whether queue is full
            if (IsFull)
                throw new IndexOutOfRangeException("Queue is full!");

            if (Front == -1)
            {
                Front = 0;
                Rear = 0;
            }
            else
            {
                Front--;

                // Wrap-around front if necessary
                if (Front < 0)
                    Front = InternalArr.Length - 1;
            }

            InternalArr[Front] = item;
            return Front;
        }

        public T DequeueFront()
        {
            // Check whether queue is empty
            if (IsEmpty)
                throw new IndexOutOfRangeException("Queue is empty!");

            T val = InternalArr[Front];

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

        public T DequeueRear()
        {
            // Check whether queue is empty
            if (IsEmpty)
                throw new IndexOutOfRangeException("Queue is empty!");

            T val = InternalArr[Rear];

            if (Front == Rear)
            {
                Front = -1;
                Rear = -1;
            }
            else
            {
                Rear--;
                if (Rear < 0)
                    Rear = InternalArr.Length - 1;
            }

            return val;
        }

        public T PeekFront()
        {
            // Check whether queue is empty
            return IsEmpty ? throw new IndexOutOfRangeException("Queue is empty!") : InternalArr[Front];
        }

        public T PeekRear()
        {
            // Check whether queue is empty
            return IsEmpty ? throw new IndexOutOfRangeException("Queue is empty!") : InternalArr[Rear];
        }
    }
}
