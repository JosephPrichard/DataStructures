using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DStruct.List;
using DStruct.List.Array;

namespace DStruct.Map.Tree;

public class TreeMap<TKey, TValue> : IOrderedMap<TKey, TValue> where TKey : IComparable<TKey>
{
    private Node<TKey, TValue> _root;

    public int Size { private set; get; }

    public TValue this[TKey key]
    {
        get => Get(key);
        set => Put(key, value);
    }
    
    public void Clear()
    {
        _root = null;
        Size = 0;
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public void Put(TKey key, TValue val)
    {
        var pair = new KvPair<TKey, TValue>(key, val);
        if (_root == null)
        {
            _root = new Node<TKey, TValue>(pair);
            Size++;
        }
        else
        {
            var newNode = Put(_root, pair);
            if (newNode != null)
            {
                Size++;
                AdjustHeights(newNode);
                
                var ibTree = FindImbalanced(newNode.Parent, out var pBalance, out var mBalance);
                if (ibTree != null) Rotate(ibTree, pBalance, mBalance);
            }
        }
    }
    
    private static Node<TKey, TValue> Put(Node<TKey, TValue> tree, KvPair<TKey, TValue> data)
    {
        while (true)
        {
            if (LessThan(data.Key, tree.Key))
            {
                if (tree.Left == null)
                {
                    var newNode = new Node<TKey, TValue>(data);
                    tree.Left = newNode;
                    newNode.Parent = tree;
                    return newNode;
                }

                tree = tree.Left;
            }
            else if (GreaterThan(data.Key, tree.Key))
            {
                if (tree.Right == null)
                {
                    var newNode = new Node<TKey, TValue>(data);
                    tree.Right = newNode;
                    newNode.Parent = tree;
                    return newNode;
                }

                tree = tree.Right;
            }
            else
            {
                tree.Data = data;
                return null;
            }
        }
    }

    public TValue Get(TKey key)
    {
        EmptyCheck();
        var node = Get(_root, key);
        return node != null ? node.Value : default;
    }
    
    private static Node<TKey, TValue> Get(Node<TKey, TValue> tree, TKey key)
    {
        while (true)
        {
            if (tree == null) return null;

            if (LessThan(key, tree.Key))
            {
                if (tree.Left != null)
                {
                    tree = tree.Left;
                    continue;
                }

                return null;
            }

            if (GreaterThan(key, tree.Key))
            {
                if (tree.Right != null)
                {
                    tree = tree.Right;
                    continue;
                }

                return null;
            }

            return tree;
        }
    }
    
    public bool Contains(TKey key)
    {
        return Get(_root, key) != null;
    }

    public bool Remove(TKey key)
    {
        EmptyCheck();
        var tree = Get(_root, key);
        if (tree != null)
        {
            Size--;
            var successor = Remove(tree);
            AdjustHeights(tree);
            
            var imbalancedTree = FindImbalanced(successor, out var pBalance, out _);
            if (imbalancedTree != null)
            {
                var middle = pBalance > 0 ? imbalancedTree.Right : imbalancedTree.Left;
                Rotate(imbalancedTree, pBalance, CalcBalance(middle));
            }
        }

        return tree != null;
    }
    
    private Node<TKey, TValue> Remove(Node<TKey, TValue> tree)
    {
        while (true)
        {
            if (tree.Left != null && tree.Right != null)
            {
                var succTree = Min(tree.Right);
                succTree.Data = tree.Data;
                tree = succTree;
                continue;
            }

            if (tree.Right != null)
            {
                Replace(tree, tree.Right);
                return tree.Right;
            }

            if (tree.Left != null)
            {
                Replace(tree, tree.Left);
                return tree.Left;
            }

            Replace(tree, null);
            return tree.Parent;
        }
    }
    
    private void Replace(Node<TKey, TValue> node, Node<TKey, TValue> newNode)
    {
        if (node.Parent == null)
        {
            _root = newNode;
            return;
        }

        if (node.Parent.Left == node)
            node.Parent.Left = newNode;
        else
            node.Parent.Right = newNode;

        if (newNode != null) newNode.Parent = node.Parent;
    }
   
    public TValue Min()
    {
        EmptyCheck();
        return Min(_root).Value;
    }

    public TValue Max()
    {
        EmptyCheck();
        return Max(_root).Value;
    }
    
    private static Node<TKey, TValue> Min(Node<TKey, TValue> tree)
    {
        while (tree.Left != null) tree = tree.Left;

        return tree;
    }

    private static Node<TKey, TValue> Max(Node<TKey, TValue> tree)
    {
        while (tree.Right != null) tree = tree.Right;

        return tree;
    }

    public IEnumerable<TKey> Keys()
    {
        return InOrder().Select(node => node.Key);
    }

    public IEnumerable<TValue> Values()
    {
        return InOrder().Select(node => node.Value);
    }

    public IEnumerable<KvPair<TKey, TValue>> Pairs()
    {
        return InOrder().Select(node => node.Data);
    }
    
    private IEnumerable<Node<TKey, TValue>> InOrder()
    {
        var stack = new ArrayList<Node<TKey, TValue>>();
        
        var curr = _root;
        while (curr != null || !stack.IsEmpty())
        {
            while (curr != null)
            {
                stack.PushBack(curr);
                curr = curr.Left;
            }
            
            curr = stack.PopBack();
            yield return curr;

            curr = curr.Right;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerator<KvPair<TKey, TValue>> GetEnumerator()
    {
        return (IEnumerator<KvPair<TKey, TValue>>) Pairs();
    }

    public IEnumerable<TValue> RangeSearch(TKey lower, TKey upper)
    {
        var stack = new ArrayList<Node<TKey, TValue>>();
        
        var curr = _root;
        while (curr != null || !stack.IsEmpty())
        {
            if (curr != null)
            {
                stack.PushBack(curr);
                curr = curr.Left;
            }
            while (curr != null && GreaterOrEqual(curr.Key, lower))
            {
                stack.PushBack(curr);
                curr = curr.Left;
            }

            curr = stack.PopBack();
            if (LessOrEqual(curr.Key, upper) && GreaterOrEqual(curr.Key, lower)) yield return curr.Value;

            curr = LessOrEqual(curr.Key, upper) ? curr.Right : null;
        }
    }

    public int NodeCount(TKey key)
    {
        var node = Get(_root, key);
        return node != null ? NodeCount(node) : 0;
    }

    public int Rank(TKey key)
    {
        var rank = Rank(_root, key);
        return LessThan(_root.Key, key) ? rank + 1 : rank;
    }
    
    public TKey Select(int rank)
    {
        var selected = Select(_root, rank);
        return selected != null ? selected.Key : default;
    }

    private static bool LessThan(TKey key1, TKey key2)
    {
        return key1.CompareTo(key2) < 0;
    }

    private static bool GreaterThan(TKey key1, TKey key2)
    {
        return key1.CompareTo(key2) > 0;
    }

    private static bool LessOrEqual(TKey key1, TKey key2)
    {
        return key1.CompareTo(key2) <= 0;
    }

    private static bool GreaterOrEqual(TKey key1, TKey key2)
    {
        return key1.CompareTo(key2) >= 0;
    }

    private static int Height(Node<TKey, TValue> tree)
    {
        return tree?.Height ?? 0;
    }

    private static int CalcBalance(Node<TKey, TValue> tree)
    {
        return Height(tree.Right) - Height(tree.Left);
    }

    private static bool ImBalanced(int balance)
    {
        return balance is >= 2 or <= -2;
    }

    private static void AdjustHeights(Node<TKey, TValue> tree)
    {
        while (true)
        {
            AdjustHeight(tree);
            if (tree.Parent != null)
            {
                tree = tree.Parent;
                continue;
            }

            break;
        }
    }

    private static void AdjustHeight(Node<TKey, TValue> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private static Node<TKey, TValue> FindImbalanced(Node<TKey, TValue> tree, out int pBalance, out int mBalance)
    {
        var prevBalance = int.MinValue;
        while (true)
        {
            if (tree != null)
            {
                var balance = CalcBalance(tree);
                if (ImBalanced(balance))
                {
                    pBalance = balance;
                    mBalance = prevBalance;
                    return tree;
                }

                prevBalance = balance;
                tree = tree.Parent;
            }
            else
            {
                pBalance = 0;
                mBalance = 0;
                return null;
            }
        }
    }

    private void Rotate(Node<TKey, TValue> imbalanced, int pBalance, int mBalance)
    {
        if (pBalance > 0)
        {
            if (mBalance > 0)
                LeftRotation(imbalanced.Right);
            else
                RightLeftRotation(imbalanced.Right);
        }
        else
        {
            if (mBalance < 0)
                RightRotation(imbalanced.Left);
            else
                LeftRightRotation(imbalanced.Left);
        }

        AdjustHeights(imbalanced);
    }

    private void SetRotationParent(Node<TKey, TValue> parentParent, Node<TKey, TValue> parent, Node<TKey, TValue> middle)
    {
        middle.Parent = parentParent;
        if (parentParent != null)
        {
            if (parentParent.Right == parent)
                parentParent.Right = middle;
            else
                parentParent.Left = middle;
        }
        else
        {
            _root = middle;
        }
    }

    private void LeftRotation(Node<TKey, TValue> middle)
    {
        var parent = middle.Parent;
        var parentParent = parent.Parent;
        var left = middle.Left;
        middle.Left = parent;
        parent.Parent = middle;
        parent.Right = left;
        if (left != null) left.Parent = parent;

        SetRotationParent(parentParent, parent, middle);
    }

    private void RightRotation(Node<TKey, TValue> middle)
    {
        var parent = middle.Parent;
        var parentParent = parent.Parent;
        var right = middle.Right;
        middle.Right = parent;
        parent.Parent = middle;
        parent.Left = right;
        if (right != null) right.Parent = parent;

        SetRotationParent(parentParent, parent, middle);
    }

    private void LeftRightRotation(Node<TKey, TValue> middle)
    {
        var child = middle.Right;
        var parent = middle.Parent;
        var childLeft = child.Left;
        child.Left = middle;
        child.Parent = parent;
        parent.Left = child;
        middle.Parent = child;
        middle.Right = childLeft;
        if (childLeft != null) childLeft.Parent = middle;

        RightRotation(child);
        AdjustHeight(middle);
    }

    private void RightLeftRotation(Node<TKey, TValue> middle)
    {
        var child = middle.Left;
        var parent = middle.Parent;
        var childRight = child.Right;
        child.Right = middle;
        child.Parent = parent;
        parent.Right = child;
        middle.Parent = child;
        middle.Left = childRight;
        if (childRight != null) childRight.Parent = middle;

        LeftRotation(child);
        AdjustHeight(middle);
    }
    
    private static int NodeCount(Node<TKey, TValue> tree)
    {
        var left = tree.Right != null ? NodeCount(tree.Right) + 1 : 0;
        var right = tree.Left != null ? NodeCount(tree.Left) + 1 : 0;
        return left + right;
    }

    private static int Rank(Node<TKey, TValue> tree, TKey key)
    {
        var right = tree.Right != null && LessThan(tree.Key, key)
            ? Rank(tree.Right, key) + (LessThan(tree.Right.Key, key) ? 1 : 0)
            : 0;
        var left = tree.Left != null ? 
            Rank(tree.Left, key) + (LessThan(tree.Left.Key, key) ? 1 : 0) 
            : 0;
        return right + left;
    }

    private Node<TKey, TValue> Select(Node<TKey, TValue> tree, int rank)
    {
        if (Rank(tree.Key) == rank) return tree;

        var left = tree.Left != null ? Select(tree.Left, rank) : null;
        var right = tree.Right != null ? Select(tree.Right, rank) : null;
        return left ?? right;
    }

    private void EmptyCheck()
    {
        if (Size == 0) throw new EmptyException();
    }
}