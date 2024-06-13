using System.Collections.Generic;

namespace DStruct.Tree.avl;

    public class Node<TKey, TValue>
    {
        public Node(TKey key, TValue val)
        {
            Data = new KeyValuePair<TKey, TValue>(key, val);
        }

        public Node(KeyValuePair<TKey, TValue> data)
        {
            Data = data;
        }

        public Node<TKey, TValue> Left { set; get; }
        public Node<TKey, TValue> Right { set; get; }
        public Node<TKey, TValue> Parent { set; get; }

        public KeyValuePair<TKey, TValue> Data { set; get; }
        
        public TKey Key => Data.Key;
        public TValue Value => Data.Value;

        public int Height { set; get; }

        public Node(Node<TKey, TValue> left, Node<TKey, TValue> right, Node<TKey, TValue> parent, TKey k, TValue v, int height)
        {
            Left = left;
            Right = right;
            Parent = parent;
            Data = new KeyValuePair<TKey, TValue>(k, v);
            Height = height;
        }
    }