using System.Collections.Generic;

namespace DStruct.tree.avl
{
    public class Node<K, V>
    {
        public Node(K key, V val)
        {
            Data = new KeyValuePair<K, V>(key, val);
        }

        public Node(KeyValuePair<K, V> data)
        {
            Data = data;
        }

        public Node<K, V> Left { set; get; }
        public Node<K, V> Right { set; get; }
        public Node<K, V> Parent { set; get; }

        public KeyValuePair<K, V> Data { set; get; }
        
        public K Key => Data.Key;
        public V Val => Data.Value;

        public int Height { set; get; }

        public Node(Node<K, V> left, Node<K, V> right, Node<K, V> parent, K k, V v, int height)
        {
            Left = left;
            Right = right;
            Parent = parent;
            Data = new KeyValuePair<K, V>(k, v);
            Height = height;
        }
    }
}