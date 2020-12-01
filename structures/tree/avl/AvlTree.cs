using System;
using System.Collections.Generic;
using DataStructures.structures.list;
using JetBrains.Annotations;

namespace DataStructures.structures.tree.avl
{
    public class AvlTree<K,V> : ISearchTree<K,V> where K : IComparable
    {
        private Node<K,V> root;
        
        public int Size { private set; get; }

        public void Put(K key, V val) {
            var pair = new KeyValuePair<K,V>(key, val);
            if(root == null) {
                root = new Node<K, V>(pair);
                Size++;
            } else {
                var newNode = Put(root, pair);
                if(newNode != null) {
                    Size++;
                    AdjustHeights(newNode);
                    var balances = new Stack<int>();
                    var imbalanced = FindImbalanced(newNode.Parent,balances);
                    if(imbalanced != null) {
                        Rotate(imbalanced, balances.Pop(), balances.Pop());
                    }
                }
            }
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
                Size--;
                var successor = Remove(node);
                AdjustHeights(node);
                var balances = new Stack<int>();
                var imbalanced = FindImbalanced(successor,balances);
                if(imbalanced != null) {
                    var pBalance = balances.Pop();
                    var mBalance = pBalance > 0 ? CalcBalance(imbalanced.Right) : CalcBalance(imbalanced.Left);
                    Rotate(imbalanced, pBalance, mBalance);
                }
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
            var list = new ArrayList<Node<K,V>>(Size);
            InOrder(root, list);
            for(var i = 0; i < list.Size; i++) {
                yield return list[i].Key;
            }
        }
        
        public IEnumerable<V> Elements() {
            var list = new ArrayList<Node<K,V>>(Size);
            InOrder(root, list);
            for(var i = 0; i < list.Size; i++) {
                yield return list[i].Val;
            }
        }

        public IEnumerable<V> RangeSearch(K lower, K upper) {
            var list = new ArrayList<KeyValuePair<K,V>>(Size);
            RangeSearch(root,lower,upper,list);
            for(var i = 0; i < list.Size; i++) {
                yield return list[i].Value;
            }
        }
        
        public int Number(K key) {
            var node = Get(root, key);
            return node != null ? Number(node) : 0;
        }
        
        public int Rank(K key) {
            var number = Rank(root, key);
            return LessThan(root.Key,key) ? ++number : number;
        }

        public K Select(int rank) {
            var selected = Select(root, rank);
            return selected != null ? selected.Key : default;
        }

        public V Min() {
            EmptyCheck();
            return Min(root).Val;
        }

        public V Max() {
            EmptyCheck();
            return Max(root).Val;
        }

        private bool LessThan(K key1, K key2) {
            return key1.CompareTo(key2) == -1;
        }
        
        private bool GreaterThan(K key1, K key2) {
            return key1.CompareTo(key2) == 1;
        }
        
        private bool LessOrEqual(K key1, K key2) {
            return !GreaterThan(key1,key2);
        }
        
        private bool GreaterOrEqual(K key1, K key2) {
            return !LessThan(key1,key2);
        }

        private int Max(int val1, int val2) {
            return val1 > val2 ? val1 : val2;
        }

        private void AdjustHeights(Node<K,V> node) {
            node.Height = Max(Height(node.Left), 
                Height(node.Right)) + 1;
            if(node.Parent != null) {
                AdjustHeights(node.Parent);
            }
        }

        private int Height(Node<K,V> node) {
            return node == null ? 0 : node.Height;
        }  

        private int CalcBalance(Node<K,V> node) {
            return Height(node.Right) - Height(node.Left);
        }

        private bool ImBalanced(int balance) {
            return balance >= 2 || balance <= -2;
        }

        private Node<K,V> FindImbalanced(Node<K,V> node, Stack<int> balances) {
            if(node != null) {
                var balance = CalcBalance(node);
                balances.Push(balance);
                return ImBalanced(balance) ? node : FindImbalanced(node.Parent, balances);
            } else {
                return null;
            }
        }

        private void Rotate(Node<K,V> imbalanced, int pBalance, int mBalance) {
            if(pBalance > 0) {
                if(mBalance > 0) {
                    LeftRotation(imbalanced.Right);
                } else {
                    RightLeftRotation(imbalanced.Right);
                }
            } else {
                if(mBalance < 0) {
                    RightRotation(imbalanced.Left);
                } else {
                    LeftRightRotation(imbalanced.Left);
                }
            }
        }

        private void SetRotationParent(Node<K,V> parentParent, Node<K,V> parent, Node<K,V> middle) {
            middle.Parent = parentParent;
            if(parentParent != null) {
                if(parentParent.Right == parent) {
                    parentParent.Right = middle;
                } else {
                    parentParent.Left = middle;
                }
            } else {
                root = middle;
            }
        }

        private void LeftRotation(Node<K,V> middle) {
            var parent = middle.Parent;
            var parentParent = parent.Parent;
            var left = middle.Left;
            middle.Left = parent;
            parent.Parent = middle;
            parent.Right = left;
            if(left != null) {
                left.Parent = parent;
            }
            SetRotationParent(parentParent, parent, middle);
            AdjustHeights(parent);
        }
        
        private void RightRotation(Node<K,V> middle) {
            var parent = middle.Parent;
            var parentParent = parent.Parent;
            var right = middle.Right;
            middle.Right = parent;
            parent.Parent = middle;
            parent.Left = right;
            if(right != null) {
                right.Parent = parent;
            }
            SetRotationParent(parentParent, parent, middle);
            AdjustHeights(parent);
        }

        private void LeftRightRotation(Node<K,V> middle) {
            var child = middle.Right;
            var parent = middle.Parent;
            var childLeft = child.Left;
            child.Left = middle;
            child.Parent = parent;
            parent.Left = child;
            middle.Parent = child;
            middle.Right = childLeft;
            if(childLeft != null) {
                childLeft.Parent = middle;
            }
            RightRotation(child);
            AdjustHeights(middle);
        }
        
        private void RightLeftRotation(Node<K,V> middle) {
            var child = middle.Left;
            var parent = middle.Parent;
            var childRight = child.Right;
            child.Right = middle;
            child.Parent = parent;
            parent.Right = child;
            middle.Parent = child;
            middle.Left = childRight;
            if(childRight != null) {
                childRight.Parent = middle;
            }
            LeftRotation(child);
            AdjustHeights(middle);
        }
        
        private Node<K,V> Put(Node<K,V> tree, KeyValuePair<K,V> pair) {
            if(LessThan(pair.Key,tree.Key)) {
                if(tree.Left == null) {
                    var newNode = new Node<K,V>(pair);
                    tree.Left = newNode;
                    newNode.Parent = tree;
                    return newNode;
                } else {
                    return Put(tree.Left, pair);
                }
            } else if(GreaterThan(pair.Key, tree.Key)) {
                if(tree.Right == null) {
                    var newNode = new Node<K,V>(pair);
                    tree.Right = newNode;
                    newNode.Parent = tree;
                    return newNode;
                } else {
                    return Put(tree.Right, pair);
                }
            } else {
                tree.Data = pair;
                return null;
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

        private Node<K,V> Remove(Node<K,V> node) {
            var hasLeft = node.Left != null;
            var hasRight = node.Right != null;
            if(hasRight && hasLeft) {
                var successor = Min(node.Right);
                Move(successor, node);
                return Remove(successor);
            } else {
                if(hasRight) {
                    Replace(node, node.Right);
                    return node.Right;
                } else if(hasLeft) {
                    Replace(node, node.Left);
                    return node.Left;
                } else {
                    Replace(node, null);
                    return node.Parent;
                }
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

        private Node<K,V> Min(Node<K,V> tree) {
            while(tree.Left != null) {
                tree = tree.Left;
            }
            return tree;
        }

        private Node<K,V> Max(Node<K,V> tree) {
            while(tree.Right != null) {
                tree = tree.Right;
            }
            return tree;
        }

        private void Move(Node<K,V> from, Node<K,V> to) {
            to.Data = from.Data;
        }

        private void InOrder(Node<K,V> node, ArrayList<Node<K,V>> list) {
            if(node == null) {
                return;
            }
            if(node.Left != null) {
                InOrder(node.Left, list);
            }
            list.PushBack(node);
            if(node.Right != null) {
                InOrder(node.Right, list);
            }
        }
        
        private void RangeSearch(Node<K,V> node, K lower, K upper, ArrayList<KeyValuePair<K,V>> list) {
            if(node == null) {
                return;
            }
            if(node.Left != null && GreaterOrEqual(node.Key,lower)) {
                RangeSearch(node.Left,lower,upper,list);
            }
            if(LessOrEqual(node.Key,upper) && GreaterOrEqual(node.Key,lower)) {
                list.PushBack(node.Data);
            }
            if(node.Right != null && LessOrEqual(node.Key,upper)) {
                RangeSearch(node.Right,lower,upper,list);
            }
        }

        private int Number(Node<K,V> node) {
            return (node.Right != null ? Number(node.Right) + 1 : 0) + 
                   (node.Left != null ? Number(node.Left) + 1 : 0);
        }

        private int Rank(Node<K,V> node, K key) {
            return (node.Right != null && LessThan(node.Key,key) 
                       ? Rank(node.Right,key) + (LessThan(node.Right.Key,key) ? 1 : 0) : 0) + 
                   (node.Left != null ? Rank(node.Left,key) + (LessThan(node.Left.Key,key) ? 1 : 0) : 0);
        }

        private Node<K,V> Select(Node<K,V> node, int rank) {
            if(Rank(node.Key) == rank) {
                return node;
            }
            var left = node.Left != null ? Select(node.Left, rank) : null;
            var right = node.Right != null ? Select(node.Right, rank) : null;
            return left != null ? left : right;
        }
        
        [AssertionMethod]
        private void EmptyCheck() {
            if(Size == 0) {
                throw new EmptyTreeException();
            }
        }
        
        public void PrintConsole() {
            if(root == null) {
                Console.WriteLine(" | Empty");
                return;
            }
            PrintConsole(root);
            Console.WriteLine();
        }
        
        private static void PrintConsole(Node<K,V> node) {
            Console.Write(" | ");
            Console.Write("P: "+node.Key);
            if(node.Left != null) {
                Console.Write(", L: "+node.Left.Key);
            }
            if(node.Right != null) {
                Console.Write(", R: "+node.Right.Key);
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