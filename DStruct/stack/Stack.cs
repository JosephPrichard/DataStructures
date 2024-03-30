using System;
using System.Collections.Generic;
using System.Linq;

namespace DStruct.stack
{
    public class Stack<E> : ICollection<E>
    {
        private Node<E> top;
        public int Size { private set; get; }

        public void Push(E e)
        {
            PushFront(e);
        }
        
        public E Peek()
        {
            return PeekFront();
        }
        
        public E Pop()
        {
            return PopFront();
        }

        //o(1)
        public void PushFront(E e)
        {
            var newNode = new Node<E>(e)
            {
                Next = top
            };
            top = newNode;
            Size++;
        }

        //o(1)
        public E PeekFront()
        {
            if (Size == 0)
            {
                throw new EmptyStackException();
            }

            return top.Val;
        }

        //o(1)
        public E PopFront()
        {
            var value = PeekFront();
            top = top.Next;
            Size--;
            return value;
        }

        public IEnumerable<E> GetEnumerable()
        {
            var curr = top;
            while (curr != null)
            {
                yield return curr.Val;
                curr = curr.Next;
            }
        }

        //o(n)
        public void AddAll(ICollection<E> list)
        {
            var tail = top;
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
            top = null;
        }

        //o(n)
        public bool Contains(E e, Func<E, E, bool> equals)
        {
            var curr = top;
            while (curr != null)
            {
                if (equals(e, curr.Val))
                {
                    return true;
                }

                curr = curr.Next;
            }

            return false;
        }
    }
}