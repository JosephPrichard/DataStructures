using System;
using System.Collections.Generic;

namespace DataStructures.linear
{
    public class Vector<E> : IList<E> 
    {
        private E[] Elements;

        public Vector() {
            Size = 0;
            Capacity = 10;
            Elements = new E[Capacity];
        }

        public Vector(int capacity) {
            Size = 0;
            Capacity = capacity;
            Elements = new E[Capacity];
        }

        public int Capacity { get; private set; }

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
        
        public void Push(E e) {
            EnsureCapacity(Size+1);
            ShiftRight(0, Size, 1);
            Elements[0] = e;
            Size++;
        }

        public E Peek() {
            return this[0];
        }

        public E Pop() {
            var value = this[0];
            Remove(0);
            return value;
        }
        
        public void PushBack(E e) {
            EnsureCapacity(Size+1);
            Elements[Size] = e;
            Size++;
        }
        
        public E PeekBack() {
            return this[Size-1];
        }

        public E PopBack() {
            var i = Size-1;
            var value = this[i];
            Remove(i);
            return value;
        }
        
        public void Remove(int index) {
            if(index >= Size)
                throw new IndexOutOfRangeException();
            EnsureCapacity(Size-1);
            ShiftLeft(index, Size, 1);
            Size--;
        }

        public void Insert(int index, E e) {
            if(index >= Size)
                throw new IndexOutOfRangeException();
            EnsureCapacity(Size+1);
            ShiftRight(index, Size, 1);
            Elements[index] = e;
            Size++;
        }
        
        public IEnumerable<E> GetEnumerable() {
            for(var i = 0; i < Size; i++) 
                yield return Elements[i];
        }
        
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
            Capacity = 0;
            Elements = new E[Capacity];
        }
        
        public bool Contains(E e, Func<E, E, bool> equals) {
            for(var i = 0; i < Size; i++)
                if(equals(e, this[i]))
                    return true;
            return false;
        }

        public void Sort(Func<E, E, bool> compare) {
            QuickSort(Elements, 0, Size-1, compare);
        }
        
        private void EnsureCapacity(int size) {
            if(size < Capacity)
                return;
            Capacity = (size-Capacity)+(Capacity*2);
            var resizedElements = new E[Capacity];
            for(var i = 0; i < Elements.Length; i++) 
                resizedElements[i] = Elements[i];
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