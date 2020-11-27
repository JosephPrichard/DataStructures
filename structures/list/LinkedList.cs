using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DataStructures.structures.list
{
    public class LinkedList<E> : IList<E>
    {
        internal Node<E> Head;
        public int Size { get; private set; }

        public E this[int index] {
            set {
                RangeCheck(index);
                var curr = Head;
                for(var i = 0; i < index; i++) {
                    curr = curr.Next;
                }
                curr.Val = value;
            }
            get {
                RangeCheck(index);
                var curr = Head;
                for(var i = 0; i < index; i++) {
                    curr = curr.Next;
                }
                return curr.Val;
            }
        }

        //o(1)
        public void Push(E e) {
            var newNode = new Node<E>(e) {
                Next = Head
            };
            Head = newNode;
            Size++;
        }

        //o(1)
        public E Peek() {
            return this[0];
        }

        //o(1)
        public E Pop() {
            var value = this[0];
            Remove(0);
            return value;
        }

        //o(n)
        public void PushBack(E e) {
            var newNode = new Node<E>(e);
            var curr = Head;
            Size++;
            if(curr == null) {
                Head = newNode;
                return;
            }
            while(curr.Next != null) {
                curr = curr.Next;
            }
            curr.Next = newNode;
        }

        //o(n)
        public E PeekBack() {
            return this[Size-1];
        }

        //o(n)
        public E PopBack() {
            var i = Size-1;
            var value = this[i];
            Remove(i);
            return value;
        }

        //o(n)
        public void Remove(int index) {
            RangeCheck(index);
            var prev = Head;
            for(var i = 0; i < index-1; i++) {
                prev = prev.Next;
            }
            prev.Next = prev.Next.Next;
            Size--;
        }

        //o(n)
        public void Insert(int index, E e) {
            RangeCheck(index);
            var curr = Head;
            for(var i = 0; i < index-1; i++) {
                curr = curr.Next;
            }
            var newNode = new Node<E>(e) {
                Next = curr.Next
            };
            curr.Next = newNode;
            Size++;
        }

        //o(n)
        public IEnumerable<E> GetEnumerable() {
            var curr = Head;
            while(curr != null) {
                yield return curr.Val;
                curr = curr.Next;
            }
        }

        //o(n)
        public void AddAll(ICollection<E> list) {
            var tail = Head;
            while(tail.Next != null) {
                tail = tail.Next;
            }
            foreach(var e in list.GetEnumerable()) {
                var newNode = new Node<E>(e);
                tail.Next = newNode;
                tail = tail.Next;
            }
            Size += list.Size;
        }

        public bool IsEmpty() {
            return Size == 0;
        }

        public void Clear() {
            Size = 0;
            Head = null;
        }

        public bool Contains(E e, Func<E, E, bool> equals) {
            var curr = Head;
            while(curr != null) {
                if(equals(e, curr.Val)) {
                    return true;
                }
                curr = curr.Next;
            }
            return false;
        }

        [AssertionMethod]
        private void RangeCheck(int index) {
            if(index >= Size || index < 0) {
                throw new ListOutOfRange();
            }
        }
    }
}