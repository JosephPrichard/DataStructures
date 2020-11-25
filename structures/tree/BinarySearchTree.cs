using System;
using System.Collections.Generic;
using DataStructures.structures.list;
using JetBrains.Annotations;

namespace DataStructures.structures.tree
{
    public class BinarySearchTree<V>
    {
        internal Node<V> Root;
        public int Size { private set; get; }

        public void Push(int key, V v) {
            var newNode = new Node<V>(key, v);
            if(Root == null) {
                Root = newNode;
            }
            else {
                Insert(Root,newNode);
            }
            Size++;
        }
        
        public V Retrieve(int key) {
            EmptyCheck();
            var node = Search(Root,key);
            return node != null ? node.Val() : default;
        }

        public void Remove(int key) {
            EmptyCheck();
            var node = Search(Root,key);
            if(node != null) {
                Remove(node);
                Size--;
            }
        }

        private void Insert(Node<V> parent, Node<V> n) {
            if(n.Key() < parent.Key()) {
                if(parent.Left == null) {
                    parent.Left = n;
                    n.Parent = parent;
                }
                else {
                    Insert(parent.Left,n);
                }
            }
            else if (n.Key() > parent.Key()) {
                if(parent.Right == null) {
                    parent.Right = n;
                    n.Parent = parent;
                }
                else {
                    Insert(parent.Right,n);
                }
            }
            else {
                throw new DuplicateKeyException();
            }
        }
        
        private Node<V> Search(Node<V> curr, int key) {
            if(key < curr.Key()) {
                return curr.Left != null ? Search(curr.Left, key) : null;
            } 
            else if(key > curr.Key()) {
                return curr.Right != null ? Search(curr.Right, key) : null;
            }
            else {
                return curr;
            }
        }

        private void Remove(Node<V> r) {
            var hasLeft = r.Left != null;
            var hasRight = r.Right != null;
            if(hasRight && hasLeft) {
                var successor = FindMin(r.Right);
                Move(successor,r);
                Remove(successor);
            }  
            else if(hasRight) {
                ReplaceNodeInParent(r,r.Right);
            } 
            else if(hasLeft) {
                ReplaceNodeInParent(r,r.Left);
            }
            else {
                ReplaceNodeInParent(r,null);
            }
        }

        private void ReplaceNodeInParent(Node<V> node, Node<V> newNode) {
            if(node.Parent == null) {
                Root = newNode;
                return;
            }
            if(node.Parent.Left == node) {
                node.Parent.Left = newNode;
            }
            else {
                node.Parent.Right = newNode;
            }
            if(newNode != null) {
                newNode.Parent = node.Parent;
            }
        }
        
        public V Min() {
            EmptyCheck();
            return FindMin(Root).Val();
        }

        public V Max() {
            EmptyCheck();
            return FindMax(Root).Val();
        }

        private Node<V> FindMin(Node<V> node) {
            while(node.Left != null) {
                node = node.Left;
            }
            return node;
        }
        
        private Node<V> FindMax(Node<V> node) {
            while(node.Right != null) {
                node = node.Right;
            }
            return node;
        }
        
        public int LeftHeight() {
            return LeftHeight(Root);
        }
        
        public int RightHeight() {
            return RightHeight(Root);
        }

        public int LeftHeight(Node<V> node) {
            var len = 0;
            while(node.Left != null) {
                node = node.Left;
                len++;
            }
            return len;
        }
        
        public int RightHeight(Node<V> node) {
            var len = 0;
            while(node.Right != null) {
                node = node.Right;
                len++;
            }
            return len;
        }
        
        private void Move(Node<V> from, Node<V> to) {
            to.Data = from.Data;
        }
        
        public IEnumerable<V> PreOrder() {
            var al = new ArrayList<V>(Size);
            PreOrder(Root,al);
            for(var i = 0; i < al.Size; i++) {
                yield return al[i];
            }
        }

        private void PreOrder(Node<V> node, ArrayList<V> list) {
            if(node == null)
                return;
            list.PushBack(node.Val());
            if(node.Left != null) {
                PreOrder(node.Left,list);
            } 
            if(node.Right != null) {
                PreOrder(node.Right,list);
            }
        }
        
        public IEnumerable<V> PostOrder() {
            var al = new ArrayList<V>(Size);
            PostOrder(Root,al);
            for(var i = 0; i < al.Size; i++) {
                yield return al[i];
            }
        }
        
        private void PostOrder(Node<V> node, ArrayList<V> list) {
            if(node == null)
                return;
            if(node.Left != null) {
                PostOrder(node.Left,list);
            } 
            if(node.Right != null) {
                PostOrder(node.Right,list);
            }
            list.PushBack(node.Val());
        }
        
        public IEnumerable<V> InOrder() {
            var al = new ArrayList<V>(Size);
            InOrder(Root,al);
            for(var i = 0; i < al.Size; i++) {
                yield return al[i];
            }
        }
        
        private void InOrder(Node<V> node, ArrayList<V> list) {
            if(node == null)
                return;
            if(node.Left != null) {
                InOrder(node.Left,list);
            } 
            list.PushBack(node.Val());
            if(node.Right != null) {
                InOrder(node.Right,list);
            }
        }

        [AssertionMethod]
        private void EmptyCheck() {
            if(Size == 0)
                throw new EmptyTreeException();
        }
        
        public void PrintConsole() {
            if(Root == null)
                return;
            PrintConsole(Root);
            Console.WriteLine();
        }
        
        private void PrintConsole(Node<V> node) {
            Console.Write(" | ");
            Console.Write("P: " + node.Key());
            if(node.Left != null) {
                Console.Write(", L: " + node.Left.Key());
            }
            if(node.Right != null) {
                Console.Write(", R: " + node.Right.Key());
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