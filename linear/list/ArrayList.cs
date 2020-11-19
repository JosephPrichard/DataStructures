using System;
using System.Collections.Generic;

namespace DataStructures.linear.list 
{
    public class ArrayList<E> : IList<E> 
    {
        private E[] Elements;

        public ArrayList() {
            Size = 0;
            Elements = new E[20];
        }

        public ArrayList(int capacity) {
            Size = 0;
            Elements = new E[capacity];
        }

        public int Size { get; private set; }

        public E this[int index] {
            get {
                if(index >= Size)
                    throw new IndexOutOfRangeException();
                return Elements[index];
            }
            set {
                if(index >= Size)
                    throw new IndexOutOfRangeException();
                Elements[index] = value;
            }
        }
        
        //O(n)
        public void Push(E e) {
            EnsureCapacity(Size+1);
            ShiftRight(0, Size, 1);
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
            if(index >= Size)
                throw new IndexOutOfRangeException();
            EnsureCapacity(Size-1);
            ShiftLeft(index, Size, 1);
            Size--;
        }

        //O(n)
        public void Insert(int index, E e) {
            if(index >= Size)
                throw new IndexOutOfRangeException();
            EnsureCapacity(Size+1);
            ShiftRight(index, Size, 1);
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

        //quicksort - O(nlog(n))
        public void Sort(Func<E, E, bool> compare) {
            QuickSort(Elements, 0, Size-1, compare);
        }
        
        private void EnsureCapacity(int size) {
            var capacity = Elements.Length;
            if(size < capacity)
                return;
            var resizedElements = new E[(size-capacity)+(capacity*2)];
            Array.Copy(Elements,0, resizedElements,0,Size);
            Elements = resizedElements;
        }

        private void ShiftRight(int b, int e, int o) {
            for(var i = e; i > b; i--) 
                Elements[i] = Elements[i-o];
        }

        private void ShiftLeft(int b, int e, int o) {
            for(var i = b; i < e; i++) 
                Elements[i] = Elements[i+o];
        }

        private static void Swap(E[] arr, int ele1, int ele2) {
            var temp = arr[ele1];
            arr[ele1] = arr[ele2];
            arr[ele2] = temp;
        }

        private static int Partition(E[] arr, int left, int right, Func<E, E, bool> compare) {
            var pivot = right;
            var i = left-1;
            while(left <= right) {
                if(compare(arr[left], arr[pivot])) {
                    i++;
                    Swap(arr, i, left);
                }
                left++;
            }
            Swap(arr, i+1, pivot);
            return i+1;
        }

        private static void QuickSort(E[] arr, int left, int right, Func<E, E, bool> compare) {
            if(left <= right) {
                var pivot = Partition(arr, left, right, compare);
                QuickSort(arr, left, pivot-1, compare);
                QuickSort(arr, pivot+1, right, compare);
            }
        }
    }
}