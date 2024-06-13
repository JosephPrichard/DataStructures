using System;
using System.Collections.Generic;

namespace DStruct.List;

    public class ArrayList<T> : IList<T>
    {
        private T[] _elements;

        public ArrayList()
        {
            Size = 0;
            _elements = new T[20];
        }

        public ArrayList(int capacity)
        {
            Size = 0;
            _elements = new T[capacity];
        }
        
        public ArrayList(IReadOnlyList<T> elems)
        {
            Size = elems.Count;
            _elements = new T[Size];
            for (var i = 0; i < Size; i++)
            {
                _elements[i] = elems[i];
            }
        }

        public int Size { get; private set; }

        public T this[int index]
        {
            get
            {
                RangeCheck(index);
                return _elements[index];
            }
            set
            {
                RangeCheck(index);
                _elements[index] = value;
            }
        }

        // O(n)
        public void PushFront(T e)
        {
            EnsureCapacity(Size + 1);
            Array.Copy(_elements, 0, _elements, 1, Size);
            _elements[0] = e;
            Size++;
        }

        // O(1)
        public T PeekFront()
        {
            return this[0];
        }

        // O(n)
        public T PopFront()
        {
            var value = this[0];
            Remove(0);
            return value;
        }

        // O(1) amortized
        public void PushBack(T e)
        {
            EnsureCapacity(Size + 1);
            _elements[Size] = e;
            Size++;
        }

        // O(1)
        public T PeekBack()
        {
            return this[Size - 1];
        }

        // O(1)
        public T PopBack()
        {
            var i = Size - 1;
            var value = this[i];
            Remove(i);
            return value;
        }

        // O(n)
        public void Remove(int index)
        {
            RangeCheck(index);
            Size--;
            Array.Copy(_elements, index + 1, _elements, index, Size - index);
        }

        // O(n)
        public void Insert(int index, T e)
        {
            RangeCheck(index);
            EnsureCapacity(Size + 1);
            Array.Copy(_elements, index, _elements, index + 1, Size - index);
            _elements[index] = e;
            Size++;
        }

        // O(n)
        public IEnumerable<T> GetEnumerable()
        {
            for (var i = 0; i < Size; i++)
            {
                yield return _elements[i];
            }
        }

        // O(n)
        public void AddAll(ICollection<T> list)
        {
            EnsureCapacity(Size + list.Size);
            var i = 0;
            foreach (var e in list.GetEnumerable())
            {
                _elements[i + Size] = e;
                i++;
            }

            Size += list.Size;
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        public void Clear()
        {
            Size = 0;
            _elements = Array.Empty<T>();
        }

        // O(n)
        public bool Contains(T e)
        {
            for (var i = 0; i < Size; i++)
            {
                if (e.Equals(this[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public T[] ToArray()
        {
            var copy = new T[Size];
            Array.Copy(_elements, 0, copy, 0, Size);
            return copy;
        }

        private void EnsureCapacity(int size)
        {
            var capacity = _elements.Length;
            if (size < capacity)
            {
                return;
            }

            var resizedElements = new T[size - capacity + capacity * 2];
            Array.Copy(_elements, 0, resizedElements, 0, Size);
            _elements = resizedElements;
        }

        private void RangeCheck(int index)
        {
            if (index >= Size || index < 0)
            {
                throw new OutOfRangeException();
            }
        }
    }