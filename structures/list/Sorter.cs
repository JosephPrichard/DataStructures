using System;

namespace DataStructures.structures.list
{
    public enum SortType
    {
        Asc,
        Dsc
    }

    public class Sorter<E> where E : IComparable
    {
        private Func<E, E, bool> doCompare;

        public Sorter(SortType typeIn)
        {
            SetType(typeIn);
        }

        public void SetType(SortType typeIn)
        {
            if(typeIn == SortType.Asc) {
                doCompare = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
            } else {
                doCompare = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
            }
        }

        public static void Swap(E[] arr, int ele1, int ele2)
        {
            var temp = arr[ele1];
            arr[ele1] = arr[ele2];
            arr[ele2] = temp;
        }

        public static void Swap(IList<E> arr, int ele1, int ele2)
        {
            var temp = arr[ele1];
            arr[ele1] = arr[ele2];
            arr[ele2] = temp;
        }

        //---QuickSort---O(nlog(n))---//

        public void QuickSort(IList<E> list)
        {
            QuickSort(list, 0, list.Size - 1);
        }

        private void QuickSort(IList<E> arr, int left, int right)
        {
            if(left <= right) {
                var pivot = Partition(arr, left, right);
                QuickSort(arr, left, pivot - 1);
                QuickSort(arr, pivot + 1, right);
            }
        }

        private int Partition(IList<E> arr, int left, int right)
        {
            var pivot = right;
            var i = left - 1;
            while(left <= right) {
                if(doCompare(arr[left], arr[pivot])) {
                    i++;
                    Swap(arr, i, left);
                }
                left++;
            }
            Swap(arr, i + 1, pivot);
            return i + 1;
        }

        //---MergeSort---O(nlog(n))---//

        public void MergeSort(E[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }

        private void MergeSort(E[] arr, int first, int last)
        {
            if(last - first < 1) {
                return;
            }
            var middle = (first + last - 1) / 2;
            MergeSort(arr, first, middle);
            MergeSort(arr, middle + 1, last);
            Merge(arr, first, middle, last);
        }

        public void Merge(E[] arr, int fi, int mi, int li)
        {
            var left = new E[mi + 1 - fi];
            Array.Copy(arr, fi, left, 0, mi + 1 - fi);
            var right = new E[li - mi];
            Array.Copy(arr, mi + 1, right, 0, li - mi);
            var l = 0;
            var r = 0;
            while(l < left.Length && r < right.Length) {
                if(doCompare(left[l], right[r])) {
                    arr[fi] = left[l];
                    l++;
                } else {
                    arr[fi] = right[r];
                    r++;
                }
                fi++;
            }
            if(l >= left.Length) {
                Array.Copy(right, r, arr, fi, right.Length - l);
            } else if(r >= right.Length) {
                Array.Copy(left, l, arr, fi, left.Length - l);
            }
        }

        public void MergeSort(LinkedList<E> list)
        {
            list.Head = MergeSort(list.Head);
        }

        private Node<E> MergeSort(Node<E> h)
        {
            if(h?.Next == null) {
                return h;
            }
            var middle = FindMiddle(h);
            var middleNext = middle.Next;
            middle.Next = null;
            var left = MergeSort(h);
            var right = MergeSort(middleNext);
            var sorted = Merge(left, right);
            return sorted;
        }

        private static Node<E> FindMiddle(Node<E> h)
        {
            var fast = h;
            while(fast.Next?.Next != null) {
                fast = fast.Next.Next;
                h = h.Next;
            }
            return h;
        }

        private Node<E> Merge(Node<E> left, Node<E> right)
        {
            if(left == null) {
                return right;
            }
            if(right == null) {
                return left;
            }
            if(doCompare(left.Val, right.Val)) {
                left.Next = Merge(left.Next, right);
                return left;
            } else {
                right.Next = Merge(left, right.Next);
                return right;
            }
        }
    }
}