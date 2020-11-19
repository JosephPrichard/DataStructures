using System;
using System.Runtime.InteropServices;

namespace DataStructures.heap
{
    public class ArrayHeap<E> : IHeap<E> where E : IComparable
    {
        private E[] Elements;
        public int Size { private set; get; }
        private readonly Func<E, E, bool> IsBetter;
        
        public ArrayHeap(HeapType type) {
            if(type == HeapType.Min) 
                IsBetter = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
            else 
                IsBetter = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
            Elements = new E[20];
        }

        public ArrayHeap(HeapType type, int capacity) {
            if(type == HeapType.Min) 
                IsBetter = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
            else 
                IsBetter = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
            Elements = new E[capacity];
        }
        
        //heapify - O(n)
        public ArrayHeap(HeapType type, E[] array) {
            if(type == HeapType.Min) 
                IsBetter = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
            else 
                IsBetter = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
            Elements = array;
            Size = array.Length;
            for(var i = Elements.Length-1; i >= 0; i--)
                SiftDown(i);
        }

        public int Depth() {
            return InternalDepth() + 1;
        }

        private int InternalDepth() {
            return (int) Math.Floor(Math.Log2(Size));
        }

        public bool IsEmpty() {
            return Size == 0;  
        }

        public void PrintConsole() {
            for(var i = 0; i < Size; i++)
                Console.Write(Elements[i] + ",");
            Console.WriteLine();
        }
        
        //O(1) due to probability
        public void Push(E e) {
            EnsureCapacity(Size+1);
            Elements[Size] = e;
            SiftUp(Size);
            Size++;
        }
        
        //O(1)
        public E Peek() {
            return Size != 0 ? Elements[0] : default;
        }

        //O(log(n))
        public E Pop() {
            if(Size == 0)
                return default;
            var val = Elements[0];
            Move(Size-1,0);
            SiftDown(0);
            Size--;
            return val;
        }

        //O(log(n))
        public E PopPush(E e) {
            if(Size != 0) {
                var val = Elements[0];
                Elements[0] = e;
                SiftDown(0);
                return val;
            }
            else {
                Size++;
                Elements[0] = e;
                return default;
            }
        }

        private void SiftUp(int pos) {
            var parent = (pos-1)/2;
            while(parent >= 0 && pos >= 0) {
                if(IsBetter(Elements[pos],Elements[parent])) {
                    Swap(pos,parent);
                    pos = parent;
                    parent = (pos-1)/2;
                }
                else {
                    pos = -1;
                }
            }
        }

        private void SiftDown(int pos) {
            var left = (2*pos)+1;
            var right= (2*pos)+2;
            if(left >= Size)
                return;
            var child = right >= Size || IsBetter(Elements[left],Elements[right]) ? left : right;
            if(IsBetter(Elements[child],Elements[pos])) {
                Swap(child,pos);
                SiftDown(child);
            }
        }

        private void EnsureCapacity(int size) {
            var capacity = Elements.Length;
            if(size < capacity)
                return;
            var resizedElements = new E[(size-capacity)+(capacity*2)];
            Array.Copy(Elements,0, resizedElements,0,Size);
            Elements = resizedElements;
        }
        
        private void Swap(int index1, int index2) {
            var val = Elements[index1];
            Elements[index1] = Elements[index2];
            Elements[index2] = val;
        }
        
        private void Move(int from, int to) {
            Elements[to] = Elements[from];
        }
        
    }
}