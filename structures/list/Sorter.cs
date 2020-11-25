using System;
using System.Data;
using DataStructures.structures.queue;

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
        
        public void MergeSort(ArrayList<E> list) {
            MergeSort(list.Elements,0,list.Size-1);
        }

        public void MergeSort(E[] arr) {
            MergeSort(arr,0,arr.Length-1);
        }

        private void MergeSort(E[] arr, int first, int last) {
            if(last-first < 1)
                return; 
            var middle = (first+last-1)/2;
            MergeSort(arr,first,middle);
            MergeSort(arr,middle+1,last);
            Merge(arr,first,middle,last);
        }

        public void Merge(E[] arr, int fi, int mi, int li) {
            var left = new E[mi+1-fi];
            Array.Copy(arr,fi,left,0,mi+1-fi);
            var right = new E[li-mi];
            Array.Copy(arr,mi+1,right,0,li-mi);
            var l = 0;
            var r = 0;
            while(l < left.Length && r < right.Length) {
                if(DoCompare(left[l],right[r])) {
                    arr[fi] = left[l];
                    l++;
                }
                else {
                    arr[fi] = right[r];
                    r++;
                }
                fi++;
            }
            if(l >= left.Length) {
                Array.Copy(right,r,arr,fi,right.Length-l);
            }
            else if(r >= right.Length) {
                Array.Copy(left,l,arr,fi,left.Length-l);
            }
        }

        public void MergeSort(LinkedList<E> list) {
            list.Head = MergeSort(list.Head);
        }
        
        private Node<E> MergeSort(Node<E> h) {
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