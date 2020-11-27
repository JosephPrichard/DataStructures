using System;
using System.Collections.Generic;
using DataStructures.structures.list;
using JetBrains.Annotations;

namespace DataStructures.structures.tree.bst
{
    public class BinarySearchTree<K,V> : IMap<K,V> where K : IComparable
    {
        private Node<K,V> root;
        
        public int Size { private set; get; }

        public void Put(K key, V val) {
            var pair = new KeyValuePair<K,V>(key, val);
            if(root == null) {
                root = new Node<K, V>(pair);
            } else {
                Put(root, pair);
            }
            Size++;
        }

        public V Get(K key) {
            EmptyCheck();
            var node = Get(root, key);
            return node != null ? node.Val : default;
        }

        public bool Remove(K key) {
            EmptyCheck();
            var node = Get(root, key);
            if(node != null) {
                Remove(node);
                Size--;
            }
            return node != null;
        }

        public bool Contains(K key) {
            return Get(root, key) != null;
        }

        public void Clear() {
            root = null;
            Size = 0;
        }
        
        public bool IsEmpty() {
            return Size == 0;
        }
        
        public IEnumerable<K> Keys() {
            var list = new ArrayList<KeyValuePair<K,V>>(Size);
            InOrder(root, list);
            for(var i = 0; i < list.Size; i++) {
                yield return list[i].Key;
            }
        }
        
        public IEnumerable<V> Elements() {
            var list = new ArrayList<KeyValuePair<K,V>>(Size);
            InOrder(root, list);
            for(var i = 0; i < list.Size; i++) {
                yield return list[i].Value;
            }
        }
        
        public V Min() {
            EmptyCheck();
            return Max(root).Val;
        }

        public V Max() {
            EmptyCheck();
            return Min(root).Val;
        }
        
        public int LeftHeight() {
            return LeftHeight(root);
        }

        public int RightHeight() {
            return RightHeight(root);
        }
        
        private bool LessThan(K key1, K key2) {
            return key1.CompareTo(key2) == -1;
        }
        
        private bool GreaterThan(K key1, K key2) {
            return key1.CompareTo(key2) == 1;
        }
        
        private bool EqualTo(K key1, K key2) {
            return key1.CompareTo(key2) == 0;
        }

        private void Put(Node<K,V> tree, KeyValuePair<K,V> pair) {
            if(LessThan(pair.Key,tree.Key)) {
                if(tree.Left == null) {
                    var newNode = new Node<K,V>(pair);
                    tree.Left = newNode;
                    newNode.Parent = tree;
                } else {
                    Put(tree.Left, pair);
                }
            } else if(GreaterThan(pair.Key, tree.Key)) {
                if(tree.Right == null) {
                    var newNode = new Node<K,V>(pair);
                    tree.Right = newNode;
                    newNode.Parent = tree;
                } else {
                    Put(tree.Right, pair);
                }
            } else {
                tree.Data = pair;
            }
        }

        private Node<K,V> Get(Node<K,V> tree, K key) {
            if(LessThan(key,tree.Key)) {
                return tree.Left != null ? Get(tree.Left, key) : null;
            } else if(GreaterThan(key,tree.Key)) {
                return tree.Right != null ? Get(tree.Right, key) : null;
            } else {
                return tree;
            }
        }

        private void Remove(Node<K,V> node) {
            var hasLeft = node.Left != null;
            var hasRight = node.Right != null;
            if(hasRight && hasLeft) {
                var successor = Max(node.Right);
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

        private void Replace(Node<K,V> node, Node<K,V> newNode) {
            if(node.Parent == null) {
                root = newNode;
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

        private Node<K,V> Max(Node<K,V> tree) {
            while(tree.Left != null) {
                tree = tree.Left;
            }
            return tree;
        }

        private Node<K,V> Min(Node<K,V> tree) {
            while(tree.Right != null) {
                tree = tree.Right;
            }
            return tree;
        }

        private int LeftHeight(Node<K,V> tree) {
            var len = 0;
            while(tree.Left != null) {
                tree = tree.Left;
                len++;
            }
            return len;
        }

        private int RightHeight(Node<K,V> tree) {
            var len = 0;
            while(tree.Right != null) {
                tree = tree.Right;
                len++;
            }
            return len;
        }

        private void Move(Node<K,V> from, Node<K,V> to) {
            to.Data = from.Data;
        }

        private void InOrder(Node<K,V> node, ArrayList<KeyValuePair<K,V>> list) {
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