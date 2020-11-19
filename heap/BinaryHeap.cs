using System;
using DataStructures.linear;
using DataStructures.linear.stack;

namespace DataStructures.heap
{
    public class BinaryHeap<E> : IHeap<E>
    {
        private Node<E> Root;
        public int Size { private set; get; }
        private readonly Func<E, E, bool> Compare;

        public BinaryHeap(Func<E, E, bool> compare) {
            Root = null;
            Compare = compare;
        }

        //heapify - O(nlog(n))
        public BinaryHeap(E[] arr, Func<E, E, bool> compare) {
            Compare = compare;
            foreach(var i in arr)
                Push(i);
        }

        public int Depth() {
            return (int)Math.Floor(Math.Log2(Size)) + 1;
        }

        public bool IsEmpty() {
            return Size == 0;  
        }

        public void PrintConsole() {
            PrintConsole(Root);
            Console.WriteLine();
        }
        
        //O(log(n))
        public void Push(E e) {
            var newNode = new Node<E>(e);
            if(Root == null) {
                Root = newNode;
            }
            else {
                Add(Root,Navigate(Size+1),newNode);
            }
            Size++;
        }
        
        //O(1)
        public E Peek() {
            return Size != 0 ? Root.Val : default;
        }

        //O(log(n))
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

        //O(log(n))
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

        private static bool Contains(Node<E> node, E e, Func<E,E,bool> compare) {
            var isEqual = compare(e,node.Val);
            if(node.Left != null) {
                return Contains(node.Left,e,compare) || isEqual;
            }
            if(node.Right != null) {
                return Contains(node.Left,e,compare) || isEqual;
            }
            return isEqual;
        }

        private static ICollection<int> Navigate(int pos) {
            var stack = new Stack<int>();
            var n = pos;
            while(n != 1) {
                stack.Push(n % 2);
                n /= 2;
            }
            return stack;
        }

        private Node<E> Add(Node<E> curr, ICollection<int> stack, Node<E> node) {
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
        
        private void Extract(Node<E> curr, ICollection<int> stack) {
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
        
        private void Replace(Node<E> curr, ICollection<int> stack, E val) {
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

        private void Sink(Node<E> curr) {
            switch(Direction(curr.Left,curr.Right,curr)) {
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

        private int Direction(Node<E> left, Node<E> right, Node<E> parent) {
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

        private static void Move(Node<E> from, Node<E> to) {
            to.Val = from.Val;
        }

        private void SwapIf(Node<E> child, Node<E> parent) {
            if(Compare(child.Val, parent.Val)) {
                Swap(child, parent);
            }
        }
        
        private static void Swap(Node<E> n1, Node<E> n2) {
            var val = n1.Val;
            n1.Val = n2.Val;
            n2.Val = val;
        }

        private static void PrintConsole(Node<E> node) {
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