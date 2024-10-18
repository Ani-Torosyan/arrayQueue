namespace arrayQueue
{
    internal class Program
    {
        internal class ArrayQueue<T>
        {
            private T[] _elements;
            private int _size;
            private int _front;
            private int _back;
            private const int DefaultCapacity = 4;

            public ArrayQueue()
            {
                _elements = new T[DefaultCapacity];
                _front = 0;
                _back = -1;
            }

            public void Enqueue(T item)
            {
                if (_size == _elements.Length)
                {
                    Resize(_elements.Length * 2);
                }

                _back = (_back + 1) % _elements.Length;
                _elements[_back] = item;
                _size++;
            }

            public T Dequeue()
            {
                if (_size == 0)
                    throw new InvalidOperationException("Queue is empty.");

                T item = _elements[_front];
                _elements[_front] = default(T)!;
                _front = (_front + 1) % _elements.Length;
                _size--;

                if (_size > 0 && _size == _elements.Length / 4)
                {
                    Resize(_elements.Length / 2);
                }

                return item;
            }

            public T Peek()
            {
                if (_size == 0)
                    throw new InvalidOperationException("Queue is empty.");

                return _elements[_front];
            }

            public int Count => _size;

            public bool IsEmpty => _size == 0;

            private void Resize(int newCapacity)
            {
                T[] newArray = new T[newCapacity];
                for (int i = 0; i < _size; i++)
                {
                    newArray[i] = _elements[(_front + i) % _elements.Length];
                }
                _elements = newArray;
                _front = 0;
                _back = _size - 1;
            }
        }
        static void Main(string[] args)
        {
            ArrayQueue<string> queue = new ArrayQueue<string>();

            queue.Enqueue("Hello");
            queue.Enqueue("World!");

            Console.WriteLine(queue.Peek());
            queue.Dequeue();
            Console.WriteLine(queue.Peek());
        }
    }
}
