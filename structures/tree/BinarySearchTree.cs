using System;
using System.Collections.Generic;
using DataStructures.structures.list;
using JetBrains.Annotations;

namespace DataStructures.structures.tree
{
    public class BinarySearchTree<V> : IVault<V>
    {
        internal Node<V> Root;
        
        public int Size { private set; get; }

        public void Put(int key, V v) {
            var newNode = new Node<V>(key, v);
            if(Root == null) {
                Root = newNode;
            } else {
                Put(Root, newNode);
            }
            Size++;
        }

        public V Get(int key) {
            EmptyCheck();
            var node = Get(Root, key);
            return node != null ? node.Val : default;
        }

        public void Remove(int key) {
            EmptyCheck();
            var node = Get(Root, key);
            if(node != null) {
                Remove(node);
                Size--;
            }
        }

        private void Put(Node<V> tree, Node<V> n) {
            if(n.Key < tree.Key) {
                if(tree.Left == null) {
                    tree.Left = n;
                    n.Parent = tree;
                } else {
                    Put(tree.Left, n);
                }
            } else if(n.Key > tree.Key) {
                if(tree.Right == null) {
                    tree.Right = n;
                    n.Parent = tree;
                } else {
                    Put(tree.Right, n);
                }
            } else {
                throw new DuplicateKeyException();
            }
        }

        private Node<V> Get(Node<V> tree, int key) {
            if(key < tree.Key) {
                return tree.Left != null ? Get(tree.Left, key) : null;
            } else if(key > tree.Key) {
                return tree.Right != null ? Get(tree.Right, key) : null;
            } else {
                return tree;
            }
        }

        private void Remove(Node<V> node) {
            var hasLeft = node.Left != null;
            var hasRight = node.Right != null;
            if(hasRight && hasLeft) {
                var successor = FindMin(node.Right);
                Move(successor, node);
                Remove(successor);
            } else if(hasRight) {
                Replace(node, node.Right);
            } else if(hasLeft) {
                Replace(node, node.Left);
            } else {
                Replace(node, null);
            }
        }

        private void Replace(Node<V> node, Node<V> newNode) {
            if(node.Parent == null) {
                Root = newNode;
                return;
            }
            if(node.Parent.Left == node) {
                node.Parent.Left = newNode;
            } else {
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

        private int LeftHeight(Node<V> tree) {
            var len = 0;
            while(tree.Left != null) {
                tree = tree.Left;
                len++;
            }
            return len;
        }

        private int RightHeight(Node<V> tree) {
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

        public bool IsEmpty() {
            return Size == 0;
        }
        
        public IEnumerable<V> Elements() {
            var list = new ArrayList<KeyValuePair<int, V>>(Size);
            InOrder(Root, list);
            for(var i = 0; i < list.Size; i++) {
                yield return list[i].Value;
            }
        }
        
        public IEnumerable<int> Keys() {
            var list = new ArrayList<KeyValuePair<int, V>>(Size);
            InOrder(Root, list);
            for(var i = 0; i < list.Size; i++) {
                yield return list[i].Key;
            }
        }

        private void InOrder(Node<V> node, ArrayList<KeyValuePair<int, V>> list) {
            if(node == null) {
                return;
            }
            if(node.Left != null) {
                InOrder(node.Left, list);
            }
            list.PushBack(node.Data);
            if(node.Right != null) {
                InOrder(node.Right, list);
            }
        }

        [AssertionMethod]
        private void EmptyCheck() {
            if(Size == 0) {
                throw new EmptyTreeException();
            }
        }
    }
}