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

        public void Insert(int key, V v) {
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
            var node = Retrieve(Root,key);
            return node != null ? node.Val : default;
        }

        public void Remove(int key) {
            EmptyCheck();
            var node = Retrieve(Root,key);
            if(node != null) {
                Remove(node);
                Size--;
            }
        }

        private void Insert(Node<V> tree, Node<V> n) {
            if(n.Key < tree.Key) {
                if(tree.Left == null) {
                    tree.Left = n;
                    n.Parent = tree;
                }
                else {
                    Insert(tree.Left,n);
                }
            }
            else if (n.Key > tree.Key) {
                if(tree.Right == null) {
                    tree.Right = n;
                    n.Parent = tree;
                }
                else {
                    Insert(tree.Right,n);
                }
            }
            else {
                throw new DuplicateKeyException();
            }
        }
        
        private Node<V> Retrieve(Node<V> tree, int key) {
            if(key < tree.Key) {
                return tree.Left != null ? Retrieve(tree.Left, key) : null;
            } 
            else if(key > tree.Key) {
                return tree.Right != null ? Retrieve(tree.Right, key) : null;
            }
            else {
                return tree;
            }
        }

        private void Remove(Node<V> node) {
            var hasLeft = node.Left != null;
            var hasRight = node.Right != null;
            if(hasRight && hasLeft) {
                var successor = FindMin(node.Right);
                Move(successor,node);
                Remove(successor);
            }  
            else if(hasRight) {
                ReplaceNodeInParent(node,node.Right);
            } 
            else if(hasLeft) {
                ReplaceNodeInParent(node,node.Left);
            }
            else {
                ReplaceNodeInParent(node,null);
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
            return FindMin(Root).Val;
        }

        public V Max() {
            EmptyCheck();
            return FindMax(Root).Val;
        }

        private Node<V> FindMin(Node<V> tree) {
            while(tree.Left != null) {
                tree = tree.Left;
            }
            return tree;
        }
        
        private Node<V> FindMax(Node<V> tree) {
            while(tree.Right != null) {
                tree = tree.Right;
            }
            return tree;
        }
        
        public int LeftHeight() {
            return LeftHeight(Root);
        }
        
        public int RightHeight() {
            return RightHeight(Root);
        }

        public int LeftHeight(Node<V> tree) {
            var len = 0;
            while(tree.Left != null) {
                tree = tree.Left;
                len++;
            }
            return len;
        }
        
        public int RightHeight(Node<V> tree) {
            var len = 0;
            while(tree.Right != null) {
                tree = tree.Right;
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
            list.PushBack(node.Val);
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
            list.PushBack(node.Val);
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
            list.PushBack(node.Val);
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
            Console.Write("P: " + node.Key);
            if(node.Left != null) {
                Console.Write(", L: " + node.Left.Key);
            }
            if(node.Right != null) {
                Console.Write(", R: " + node.Right.Key);
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