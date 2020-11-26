using System.Collections.Generic;

namespace DataStructures.structures.tree
{
    public class Node<V>
    {
        public Node(int key, V val) {
            Data = new KeyValuePair<int, V>(key, val);
        }

        public Node<V> Left { set; get; }
        public Node<V> Right { set; get; }
        public Node<V> Parent { set; get; }

        public KeyValuePair<int, V> Data { set; get; }
        public int Key => Data.Key;
        public V Val => Data.Value;
    }
}