using System;
using DataStructures.structures.stack;
using JetBrains.Annotations;

namespace DataStructures.structures.binaryTree
{
    public enum SearchType
    {
        Min,
        Max
    }
    
    public class BinarySearchTree<E> where E : IComparable
    {
        private Node<E> Root;
        public int Size { private set; get; }
        private Func<E, E, bool> DoCompare;

        public BinarySearchTree(SearchType type) {
            SetHeapType(type);
            Root = null;
        }

        private void SetHeapType(SearchType type) {
            if(type == SearchType.Min) 
                DoCompare = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
            else 
                DoCompare = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
        }

        public int Depth() {
            return (int)Math.Floor(Math.Log2(Size)) + 1;
        }

        public bool IsEmpty() {
            return Size == 0;  
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
        
        //o(1)
        public E Peek() {
            CheckEmpty();
            return Root.Val;
        }

        //O(log(n))
        public E Pop() {
            CheckEmpty();
            var val = Root.Val;
            if(Size > 1) {
                Extract(Root,Navigate(Size));
                SiftDown(Root);
            }
            else {
                Root = null;
            }
            Size--;
            return val;
        }

        //O(log(n))
        public E PopPush(E e) {
            CheckEmpty();
            var val = Root.Val;
            if(Size > 1) {
                Replace(Root,Navigate(Size),e);
                SiftDown(Root);
            }
            else {
                Root.Val = e;
            }
            return val;
        }
        
        [AssertionMethod]
        private void CheckEmpty() {
            
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
            }
            else {
                if(curr.Right != null) {
                    SwapIf(Add(curr.Right,stack,node), curr);
                }
                else {
                    curr.Right = node;
                    SwapIf(curr.Right, curr);
                }
            }
            return curr;
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

        private void SiftDown(Node<E> curr) {
            if(curr.Left == null)
                return;
            var child = curr.Right == null || DoCompare(curr.Left.Val, curr.Right.Val) ? curr.Left : curr.Right;
            if(DoCompare(child.Val,curr.Val)) {
                Swap(child,curr);
                SiftDown(child);
            }
        }

        private static void Move(Node<E> from, Node<E> to) {
            to.Val = from.Val;
        }

        private void SwapIf(Node<E> child, Node<E> parent) {
            if(DoCompare(child.Val, parent.Val)) {
                Swap(child, parent);
            }
        }
        
        private static void Swap(Node<E> n1, Node<E> n2) {
            var val = n1.Val;
            n1.Val = n2.Val;
            n2.Val = val;
        }

    }
}