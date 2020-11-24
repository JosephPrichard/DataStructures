using System;
using DataStructures.structures.heap;

namespace DataStructures.structures.list
{
    public enum SortType
    {
        Asc,
        Dsc
    }
    
    public class Sorter<E> where E : IComparable
    {
        private readonly Func<E, E, bool> DoCompare;
        private readonly SortType Type;

        public Sorter(SortType typeIn) {
            Type = typeIn;
            if(typeIn == SortType.Asc) 
                DoCompare = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
            else 
                DoCompare = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
        }
        
        public static void Swap(E[] arr, int ele1, int ele2) {
            var temp = arr[ele1];
            arr[ele1] = arr[ele2];
            arr[ele2] = temp;
        }
        
        //---HeapSort---O(nlog(n))---//
        
        public void HeapSort(ArrayList<E> list) {
            var type = Type == SortType.Asc ? HeapType.Max : HeapType.Min;
            var heap = new Heap<E>(type, list.ToArray());
            while(heap.Size > 0) {
                heap.Remove(0);
            }
            list.Copy(heap.Copy());
        }
        
        public void HeapSort(E[] arr) {
            var type = Type == SortType.Asc ? HeapType.Max : HeapType.Min;
            var heap = new Heap<E>(type, arr);
            while(heap.Size > 0) {
                heap.Remove(0);
            }
            var copy = heap.Copy();
            Array.Copy(copy,0,arr,0,copy.Length);
        }
        
        //---QuickSort---O(nlog(n))---//

        public void QuickSort(ArrayList<E> list) {
            QuickSort(list.Elements, 0, list.Size-1);
        }
        
        public void QuickSort(E[] arr) {
            QuickSort(arr, 0, arr.Length-1);
        }
        
        private void QuickSort(E[] arr, int left, int right) {
            if(left <= right) {
                var pivot = Partition(arr, left, right);
                QuickSort(arr, left, pivot-1);
                QuickSort(arr, pivot+1, right);
            }
        }

        private int Partition(E[] arr, int left, int right) {
            var pivot = right;
            var i = left-1;
            while(left <= right) {
                if(DoCompare(arr[left], arr[pivot])) {
                    i++;
                    Swap(arr, i, left);
                }
                left++;
            }
            Swap(arr, i+1, pivot);
            return i+1;
        }

        //---MergeSort---O(nlog(n))---//

        public void MergeSort(LinkedList<E> list) {
            list.Head = MergeSort(list.Head);
        }
        
        public Node<E> MergeSort(Node<E> h) {
            if(h?.Next == null)
                return h;
            var middle = FindMiddle(h);
            var middleNext = middle.Next;
            middle.Next = null;
            var left = MergeSort(h);
            var right = MergeSort(middleNext);
            var sorted = Merge(left, right);
            return sorted;
        }

        private static Node<E> FindMiddle(Node<E> h) {
            var fast = h;
            while(fast.Next?.Next != null) {
                fast = fast.Next.Next;
                h = h.Next;
            }
            return h;
        }

        private Node<E> Merge(Node<E> left, Node<E> right) {
            if(left == null)
                return right;
            if(right == null)
                return left;
            if(DoCompare(left.Val, right.Val)) {
                left.Next = Merge(left.Next, right);
                return left;
            } else {
                right.Next = Merge(left, right.Next);
                return right;
            }
        }
        
    }
}