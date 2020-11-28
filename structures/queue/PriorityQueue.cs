using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DataStructures.structures.queue
{
    public enum PriorityType
    {
        Min,
        Max
    }
    
    public class PriorityQueue<E> : IPriorityQueue<E> where E : IComparable
    {
        private Func<E,E,bool> doCompare;
        private E[] elements;

        public PriorityQueue(PriorityType type) {
            SetHeapType(type);
            elements = new E[20];
        }

        public PriorityQueue(PriorityType type, int capacity) {
            SetHeapType(type);
            elements = new E[capacity];
        }

        public PriorityQueue(PriorityType type, E[] array) {
            SetHeapType(type);
            elements = new E[array.Length];
            Array.Copy(array, 0, elements, 0, array.Length);
            Size = array.Length;
            ReOrder();
        }

        public int Size { private set; get; }

        public bool IsEmpty() {
            return Size == 0;
        }

        //O(1) due to probability
        public void Push(E e) {
            EnsureCapacity(Size+1);
            elements[Size] = e;
            SiftUp(Size);
            Size++;
        }

        //O(1)
        public E Peek() {
            EmptyCheck();
            return elements[0];
        }

        //O(log(n))
        public E Pop() {
            EmptyCheck();
            var val = elements[0];
            Move(Size-1, 0);
            Size--;
            SiftDown(0);
            return val;
        }

        //O(log(n))
        public void Remove(int index) {
            EmptyCheck();
            RangeCheck(index);
            Swap(Size-1, index);
            Size--;
            SiftDown(index);
        }

        public void Remove(E e) {
            for(var i = 0; i < Size; i++) {
                if(elements[i].Equals(e)) {
                    Remove(i);
                    return;
                }
            }
        }

        //O(log(n))
        public E PopPush(E e) {
            EmptyCheck();
            var val = elements[0];
            elements[0] = e;
            SiftDown(0);
            return val;
        }

        //o(n)
        public IEnumerable<E> GetEnumerable() {
            for(var i = 0; i < Size; i++) {
                yield return elements[i];
            }
        }

        public void Clear() {
            Size = 0;
            elements = Array.Empty<E>();
        }

        private void SetHeapType(PriorityType type) {
            if(type == PriorityType.Min) {
                doCompare = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
            } else {
                doCompare = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
            }
        }

        public int Depth() {
            return (int) Math.Floor(Math.Log2(Size))+1;
        }

        public E[] Copy() {
            var copy = new E[Size];
            Array.Copy(elements, 0, copy, 0, Size);
            return copy;
        }

        //O(n)
        public void ReOrder() {
            for(var i = Size-1; i >= 0; i--) {
                SiftDown(i);
            }
        }

        [AssertionMethod]
        private void EmptyCheck() {
            if(Size == 0) {
                throw new EmptyQueueException();
            }
        }

        [AssertionMethod]
        private void RangeCheck(int index) {
            if(index >= Size || index < 0) {
                throw new OutOfRangeException();
            }
        }

        private void SiftUp(int pos) {
            var parent = (pos-1)/2;
            while(parent >= 0) {
                if(doCompare(elements[pos], elements[parent])) {
                    Swap(pos, parent);
                    pos = parent;
                    parent = (pos-1)/2;
                } else {
                    parent = -1;
                }
            }
        }

        private void SiftDown(int pos) {
            var left = 2*pos+1;
            var right = 2*pos+2;
            if(left >= Size) {
                return;
            }
            var child = right >= Size || doCompare(elements[left], elements[right]) ? left : right;
            if(doCompare(elements[child], elements[pos])) {
                Swap(child, pos);
                SiftDown(child);
            }
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

        private void Swap(int index1, int index2) {
            var val = elements[index1];
            elements[index1] = elements[index2];
            elements[index2] = val;
        }

        private void Move(int from, int to) {
            elements[to] = elements[from];
        }
    }
}