using System;
using System.Collections.Generic;

namespace DStruct.List
{
    public class LinkedList<T> : IList<T>
    {
        internal Node<T> Head;
        public int Size { get; private set; }

        public T this[int index]
        {
            set
            {
                RangeCheck(index);
                var curr = Head;
                for (var i = 0; i < index; i++)
                {
                    curr = curr.Next;
                }

                curr.Val = value;
            }
            get
            {
                RangeCheck(index);
                var curr = Head;
                for (var i = 0; i < index; i++)
                {
                    curr = curr.Next;
                }

                return curr.Val;
            }
        }

        // o(1)
        public void PushFront(T e)
        {
            var newNode = new Node<T>(e)
            {
                Next = Head
            };
            Head = newNode;
            Size++;
        }

        // o(1)
        public T PeekFront()
        {
            return this[0];
        }

        // o(1)
        public T PopFront()
        {
            var value = this[0];
            Remove(0);
            return value;
        }

        // o(n)
        public void PushBack(T e)
        {
            var newNode = new Node<T>(e);
            var curr = Head;
            Size++;
            if (curr == null)
            {
                Head = newNode;
                return;
            }

            while (curr.Next != null)
            {
                curr = curr.Next;
            }

            curr.Next = newNode;
        }

        // o(n)
        public T PeekBack()
        {
            return this[Size - 1];
        }

        // o(n)
        public T PopBack()
        {
            var i = Size - 1;
            var value = this[i];
            Remove(i);
            return value;
        }

        // o(n)
        public void Remove(int index)
        {
            RangeCheck(index);
            
            if (index == 0)
            {
                Head = Head.Next;
                Size--;
                return;
            }
            
            var prev = Head;
            for (var i = 0; i < index - 1; i++)
            {
                prev = prev.Next;
            }

            prev.Next = prev.Next.Next;
            Size--;
        }

        // o(n)
        public void Insert(int index, T e)
        {
            RangeCheck(index);
            var curr = Head;
            for (var i = 0; i < index - 1; i++)
            {
                curr = curr.Next;
            }

            var newNode = new Node<T>(e)
            {
                Next = curr.Next
            };
            curr.Next = newNode;
            Size++;
        }

        // o(n)
        public IEnumerable<T> GetEnumerable()
        {
            var curr = Head;
            for (var i = 0; i < Size && curr != null; i++)
            {
                var v = curr.Val;
                curr = curr.Next;
                yield return v;
            }
        }

        // o(n)
        public void AddAll(ICollection<T> list)
        {
            var tail = Head;
            while (tail.Next != null)
            {
                tail = tail.Next;
            }
            
            foreach (var e in list.GetEnumerable())
            {
                var newNode = new Node<T>(e);
                tail.Next = newNode;
                tail = tail.Next;
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
            Head = null;
        }

        public bool Contains(T e)
        {
            var curr = Head;
            while (curr != null)
            {
                if (e.Equals(curr.Val))
                {
                    return true;
                }

                curr = curr.Next;
            }

            return false;
        }

        private void RangeCheck(int index)
        {
            if (index >= Size || index < 0)
            {
                throw new OutOfRangeException();
            }
        }
    }
}