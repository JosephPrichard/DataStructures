using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DataStructures.structures.list
{
    public class ArrayList<E> : IList<E>
    {
        private E[] elements;

        public ArrayList() {
            Size = 0;
            elements = new E[20];
        }

        public ArrayList(int capacity) {
            Size = 0;
            elements = new E[capacity];
        }

        public int Size { get; private set; }

        public E this[int index] {
            get {
                RangeCheck(index);
                return elements[index];
            }
            set {
                RangeCheck(index);
                elements[index] = value;
            }
        }

        //O(n)
        public void Push(E e) {
            EnsureCapacity(Size+1);
            Array.Copy(elements, 0, elements, 1, Size);
            elements[0] = e;
            Size++;
        }

        //O(1)
        public E Peek() {
            return this[0];
        }

        //O(n)
        public E Pop() {
            var value = this[0];
            Remove(0);
            return value;
        }

        //O(1) amortized
        public void PushBack(E e) {
            EnsureCapacity(Size+1);
            elements[Size] = e;
            Size++;
        }

        //O(1)
        public E PeekBack() {
            return this[Size-1];
        }

        //O(1)
        public E PopBack() {
            var i = Size-1;
            var value = this[i];
            Remove(i);
            return value;
        }

        //O(n)
        public void Remove(int index) {
            RangeCheck(index);
            EnsureCapacity(Size-1);
            Array.Copy(elements, index+1, elements, index, Size-index);
            Size--;
        }

        //O(n)
        public void Insert(int index, E e) {
            RangeCheck(index);
            EnsureCapacity(Size+1);
            Array.Copy(elements, index, elements, index+1, Size-index);
            elements[index] = e;
            Size++;
        }

        //O(n)
        public IEnumerable<E> GetEnumerable() {
            for(var i = 0; i < Size; i++) {
                yield return elements[i];
            }
        }

        //O(n)
        public void AddAll(ICollection<E> list) {
            EnsureCapacity(Size+list.Size);
            var i = 0;
            foreach(var e in list.GetEnumerable()) {
                elements[i+Size] = e;
                i++;
            }
            Size += list.Size;
        }

        public bool IsEmpty() {
            return Size == 0;
        }

        public void Clear() {
            Size = 0;
            elements = Array.Empty<E>();
        }

        //O(n)
        public bool Contains(E e, Func<E, E, bool> equals) {
            for(var i = 0; i < Size; i++) {
                if(equals(e, this[i])) {
                    return true;
                }
            }
            return false;
        }

        public E[] ToArray() {
            var copy = new E[Size];
            Array.Copy(elements, 0, copy, 0, Size);
            return copy;
        }

        private void EnsureCapacity(int size) {
            var capacity = elements.Length;
            if(size < capacity) {
                return;
            }
            var resizedElements = new E[size-capacity+capacity*2];
            Array.Copy(elements, 0, resizedElements, 0, Size);
            elements = resizedElements;
        }

        [AssertionMethod]
        private void RangeCheck(int index) {
            if(index >= Size || index < 0) {
                throw new ListOutOfRange();
            }
        }
    }
}