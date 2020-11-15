using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.linear
{
    public class Stack<E> : ICollection<E> 
    {
        private Node<E> Top;

        public int Size {
            get;
            private set;
        }
        
        public void Push(E e) {
            var newNode = new Node<E>(e) {
                Next = Top
            };
            Top = newNode;
            Size++;
        }

        public E Peek() {
            return Top.Val;
        }

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
            var curr = Top;
            while(Top != null) {
                var next = curr.Next;
                curr.Next = null;
                curr = next;
            }
            Top = null;
            Size = 0;
        }
        
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