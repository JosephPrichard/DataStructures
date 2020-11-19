using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.linear.stack
{
    public class Stack<E> : ICollection<E> 
    {
        private Node<E> Top;
        public int Size { private set; get; }
        
        //o(1)
        public void Push(E e) {
            var newNode = new Node<E>(e) {
                Next = Top
            };
            Top = newNode;
            Size++;
        }

        //o(1)
        public E Peek() {
            return Top.Val;
        }

        //o(1)
        public E Pop() {
            var value = Top.Val;
            Top = Top.Next;
            Size--;
            return value;
        }
        
        public IEnumerable<E> GetEnumerable() {
            var curr = Top;
            while(curr != null) {
                yield return curr.Val;
                curr = curr.Next;
            }
        }
        
        //o(n)
        public void AddAll(ICollection<E> list) {
            var tail = Top;
            while(tail.Next != null)
                tail = tail.Next;
            foreach(var e in list.GetEnumerable().Reverse()) 
                Push(e);
            Size += list.Size;
        }

        public bool IsEmpty() {
            return Size == 0;
        }

        public void Clear() {
            Size = 0;
            Top = null;
        }
        
        //o(n)
        public bool Contains(E e, Func<E, E, bool> equals) {
            var curr = Top;
            while(curr != null) {
                if(equals(e, curr.Val))
                    return true;
                curr = curr.Next;
            }
            return false;
        }
    }
}