using System;
using DataStructures.linear;

namespace DataStructures.tree
{
    public class BHeap<E> : IHeap<E>
    {
        private BNode<E> Root;

        public int Size { set; get; }

        private readonly Func<E, E, bool> Compare;

        public BHeap(Func<E, E, bool> compare) {
            Root = null;
            Compare = compare;
        }
        
        public static IHeap<E> Union(BHeap<E> heap1, BHeap<E> heap2, Func<E, E, bool> compare) {
            var heap = new BHeap<E>(compare);
            if(!heap1.IsEmpty()) {
                Merge(heap,heap1.Root);
            }
            if(!heap2.IsEmpty()) {
                Merge(heap,heap2.Root);
            }
            return heap;
        }

        private static void Merge(BHeap<E> heap, BNode<E> node) {
            heap.Push(node.Val);
            if(node.Left != null) {
                Merge(heap, node.Left);
            }
            if(node.Right != null) {
                Merge(heap, node.Right);
            }
        }

        public int GetDepth() {
            return (int)Math.Floor(Math.Log2(Size)) + 1;
        }

        public bool IsEmpty() {
            return Size == 0;  
        }

        public void Print() {
            PrintConsole(Root);
            Console.WriteLine();
        }
        
        public void Push(E e) {
            var newNode = new BNode<E>(e);
            if(Root == null) {
                Root = newNode;
            }
            else {
                Add(Root,Navigate(Size+1),newNode);
            }
            Size++;
        }
        
        public E Peek() {
            return Size == 0 ? default : Root.Val;
        }

        public E Pop() {
            switch(Size) {
                case 0:
                    return default;
                case 1:
                    var val1 = Root.Val;
                    Root = null;
                    Size--;
                    return val1;
                default:
                    var val2 = Root.Val;
                    Extract(Root,Navigate(Size));
                    Sink(Root);
                    Size--;
                    return val2;
            }
        }

        public E PopPush(E e) {
            switch(Size) {
                case 0:
                    Push(e);
                    return default;
                case 1:
                    var val1 = Root.Val;
                    Root.Val = e;
                    return val1;
                default:
                    var val2 = Root.Val;
                    Replace(Root,Navigate(Size),e);
                    Sink(Root);
                    return val2;
            }
        }
        
        public bool Contains(E e, Func<E,E,bool> compare) {
            return Contains(Root,e,compare);
        }

        private static bool Contains(BNode<E> node, E e, Func<E,E,bool> compare) {
            var isEqual = compare(e,node.Val);
            if(node.Left != null) {
                return Contains(node.Left,e,compare) || isEqual;
            }
            if(node.Right != null) {
                return Contains(node.Left,e,compare) || isEqual;
            }
            return isEqual;
        }

        private static Stack<int> Navigate(int pos) {
            var stack = new Stack<int>();
            var n = pos;
            while(n != 1) {
                stack.Push(n % 2);
                n /= 2;
            }
            return stack;
        }

        private BNode<E> Add(BNode<E> curr, Stack<int> stack, BNode<E> node) {
            if(stack.Pop() == 0) {
                if(curr.Left != null) {
                    SwapIf(Add(curr.Left,stack,node), curr);
                }
                else {
                    curr.Left = node;
                    SwapIf(curr.Left, curr);
                }
                return curr;
            }
            else {
                if(curr.Right != null) {
                    SwapIf(Add(curr.Right,stack,node), curr);
                }
                else {
                    curr.Right = node;
                    SwapIf(curr.Right, curr);
                }
                return curr;
            }
        }
        
        private void Extract(BNode<E> curr, Stack<int> stack) {
            if(stack.Pop() == 0) {
                if(stack.IsEmpty()) {
                    Move(curr.Left, Root);
                    curr.Left = null;
                }
                else {
                    Extract(curr.Left, stack);
                }
            }
            else {
                if(stack.IsEmpty()) {
                    Move(curr.Right, Root);
                    curr.Right = null;
                }
                else {
                    Extract(curr.Right, stack);
                }
            }
        }
        
        private void Replace(BNode<E> curr, Stack<int> stack, E val) {
            if(stack.Pop() == 0) {
                if(stack.IsEmpty()) {
                    Move(curr.Left, Root);
                    curr.Left.Val = val;
                }
                else {
                   Replace(curr.Left, stack, val);
                }
            }
            else {
                if(stack.IsEmpty()) {
                    Move(curr.Right, Root);
                    curr.Right.Val = val;
                }
                else {
                    Replace(curr.Right, stack, val);
                }
            }
        }

        private void Sink(BNode<E> curr) {
            var d = Direction(curr.Left,curr.Right,curr);
            switch(d) {
                case -1:
                    Swap(curr.Left,curr);
                    Sink(curr.Left);
                    break;
                case 1:
                    Swap(curr.Right,curr);
                    Sink(curr.Right);
                    break;
            }
        }

        private int Direction(BNode<E> left, BNode<E> right, BNode<E> parent) {
            if(right == null) {
                if (left  == null) {
                    return 0;
                }
                else {
                    return Compare(left.Val, parent.Val) ? -1 : 0;
                }
            }
            if(Compare(left.Val, right.Val)) {
                return Compare(left.Val, parent.Val) ? -1 : 0;
            }
            else {
                return Compare(right.Val, parent.Val) ? 1 : 0;
            }
        }

        private static void Move(BNode<E> from, BNode<E> to) {
            to.Val = from.Val;
        }

        private void SwapIf(BNode<E> child, BNode<E> parent) {
            if(Compare(child.Val, parent.Val)) {
                Swap(child, parent);
            }
        }
        
        private static void Swap(BNode<E> n1, BNode<E> n2) {
            var val = n1.Val;
            n1.Val = n2.Val;
            n2.Val = val;
        }

        private static void PrintConsole(BNode<E> node) {
            Console.Write(" | ");
            Console.Write("P: " + node.Val);
            if(node.Left != null) {
                Console.Write(", L: " + node.Left.Val);
            }
            if(node.Right != null) {
                Console.Write(", R: " + node.Right.Val);
            }
            if(node.Left != null) {
                PrintConsole(node.Left);
            }
            if(node.Right != null) {
                PrintConsole(node.Right);
            }
        }

    }
}