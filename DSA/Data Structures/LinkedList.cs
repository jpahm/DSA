namespace DSA
{
    // Represents a basic singly-linked list.
    public class LinkedList<T>
    {
        private class Node(T value) {
            public T Value = value;
            public Node? Next;
        }

        private Node? Head;
        private Node? Tail;

        public LinkedList() { }
        public bool IsEmpty => Head == null;
        public int Size { get; private set; }

        public void AddEnd(T value) { 
            if (Tail is null)
            {
                Head = new Node(value);
                Tail = Head;
            } else
            {
                Tail.Next = new Node(value);
                Tail = Tail.Next;
            }
            Size++;
        }

        public void AddStart(T value)
        {
            if (Head is null)
            {
                Head = new Node(value);
                Tail = Head;
            }
            else
            {
                Node newStart = new Node(value);
                newStart.Next = Head;
                Head = newStart;
            }
            Size++;
        }

        public void DeleteEnd()
        {
            if (Head is null || Tail is null)
            {
                throw new IndexOutOfRangeException("Linked list is empty!");
            }
            else
            {
                Node temp = Head;
                while (temp.Next?.Next != null)
                    temp = temp.Next;

                if (temp == Head)
                    Head = Tail = null;
                else
                    temp.Next = Tail = null;
            }
            Size--;
        }

        public void DeleteStart()
        {
            if (Head is null)
            {
                throw new IndexOutOfRangeException("Linked list is empty!");
            }
            else
            {
                Head = Head.Next;
            }
            Size--;
        }

        public T GetValueAt(int index)
        {
            Node? temp = Head;
            for (int i = 0; temp != null && i < index; ++i)
            {
                temp = temp.Next;
            }

            if (temp == null)
                throw new IndexOutOfRangeException("Cannot read past the end of the linked list!");
            else
                return temp.Value;
        }

        public void SetValueAt(int index, T value)
        {
            Node? temp = Head;
            for (int i = 0; temp != null && i < index; ++i)
            {
                temp = temp.Next;
            }

            if (temp == null)
                throw new IndexOutOfRangeException("Cannot write past the end of the linked list!");
            else
                temp.Value = value;
        }

        public void InsertAt(int index, T value)
        {
            Node? prev = null;
            Node? temp = Head;

            int i = 0;
            for (; temp != null && i < index; ++i)
            {
                prev = temp;
                temp = temp.Next;
            }

            if (temp == null && i < index)
                throw new IndexOutOfRangeException("Cannot write past the end of the linked list!");

            Node newNode = new(value);
            if (prev is not null)
                prev.Next = newNode;
            else
                Head = newNode;

            newNode.Next = temp;

            if (newNode.Next is null)
                Tail = newNode;

            Size++;
        }

        public void DeleteAt(int index)
        {
            Node? prev = null;
            Node? temp = Head;
            for (int i = 0; temp != null && i < index; ++i)
            {
                prev = temp;
                temp = temp.Next;
            }

            if (temp == null)
                throw new IndexOutOfRangeException("Cannot delete past the end of the linked list!");

            if (prev is not null)
                prev.Next = temp.Next;
            else
                Head = null;

            if (temp.Next is null)
                Tail = prev;

            Size--;
        }

        public T this[int idx] {
            get => GetValueAt(idx);
            set => SetValueAt(idx, value);
        }
    }
}
