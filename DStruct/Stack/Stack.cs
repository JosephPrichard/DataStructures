using System;
using System.Collections.Generic;
using System.Linq;

namespace DStruct.Stack
{
    public class Stack<T> : ICollection<T>
    {
        private Node<T> _top;
        public int Size { private set; get; }

        public void Push(T e)
        {
            PushFront(e);
        }
        
        public T Peek()
        {
            return PeekFront();
        }
        
        public T Pop()
        {
            return PopFront();
        }

        // o(1)
        public void PushFront(T e)
        {
            var newNode = new Node<T>(e)
            {
                Next = _top
            };
            _top = newNode;
            Size++;
        }

        // o(1)
        public T PeekFront()
        {
            if (Size == 0)
            {
                throw new EmptyStackException();
            }

            return _top.Val;
        }

        // o(1)
        public T PopFront()
        {
            var value = PeekFront();
            _top = _top.Next;
            Size--;
            return value;
        }

        public IEnumerable<T> GetEnumerable()
        {
            var curr = _top;
            while (curr != null)
            {
                yield return curr.Val;
                curr = curr.Next;
            }
        }

        // o(n)
        public void AddAll(ICollection<T> list)
        {
            var tail = _top;
            while (tail.Next != null)
            {
                tail = tail.Next;
            }

            foreach (var e in list.GetEnumerable().Reverse())
            {
                PushFront(e);
            }
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        public void Clear()
        {
            Size = 0;
            _top = null;
        }

        // o(n)
        public bool Contains(T e)
        {
            var curr = _top;
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
    }
}