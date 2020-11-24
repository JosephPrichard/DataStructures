using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DataStructures.structures.list 
{
    public class ArrayList<E> : IList<E> 
    {
        internal E[] Elements;
        public int Size { get; private set; }

        public ArrayList() {
            Size = 0;
            Elements = new E[20];
        }

        public ArrayList(int capacity) {
            Size = 0;
            Elements = new E[capacity];
        }
        
        public E this[int index] {
            get {
                RangeCheck(index);
                return Elements[index];
            }
            set {
                RangeCheck(index);
                Elements[index] = value;
            }
        }

        public E[] ToArray() {
            var copy = new E[Size];
            Array.Copy(Elements,0,copy,0,Size);
            return copy;
        }

        public void Copy(E[] arr) {
            if(arr.Length > Size) 
                throw new SizeExceedsException();
            Array.Copy(arr, 0, Elements, 0, arr.Length);
        }
        
        //O(n)
        public void Push(E e) {
            EnsureCapacity(Size+1);
            Array.Copy(Elements,0,Elements,1,Size);
            Elements[0] = e;
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
            Elements[Size] = e;
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
            Array.Copy(Elements,index+1,Elements,index,Size-index);
            Size--;
        }

        //O(n)
        public void Insert(int index, E e) {
            RangeCheck(index);
            EnsureCapacity(Size+1);
            Array.Copy(Elements,index,Elements,index+1,Size-index);
            Elements[index] = e;
            Size++;
        }
        
        //O(n)
        public IEnumerable<E> GetEnumerable() {
            for(var i = 0; i < Size; i++) 
                yield return Elements[i];
        }
        
        //O(n)
        public void AddAll(ICollection<E> list) {
            EnsureCapacity(Size+list.Size);
            var i = 0;
            foreach(var e in list.GetEnumerable()) {
                Elements[i+Size] = e;
                i++;
            }
            Size += list.Size;
        }
        
        public bool IsEmpty() {
            return Size == 0;
        }

        public void Clear() {
            Size = 0;
            Elements = Array.Empty<E>();
        }
        
        //O(n)
        public bool Contains(E e, Func<E, E, bool> equals) {
            for(var i = 0; i < Size; i++)
                if(equals(e, this[i]))
                    return true;
            return false;
        }

        private void EnsureCapacity(int size) {
            var capacity = Elements.Length;
            if(size < capacity)
                return;
            var resizedElements = new E[(size-capacity)+(capacity*2)];
            Array.Copy(Elements,0, resizedElements,0,Size);
            Elements = resizedElements;
        }

        [AssertionMethod]
        private void RangeCheck(int index) {
            if(index >= Size || index < 0)
                throw new SizeExceedsException();
        }
    }
}