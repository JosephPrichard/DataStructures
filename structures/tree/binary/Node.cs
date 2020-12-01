namespace DataStructures.structures.tree.binary
{
    public class Node<V>
    {
        public Node(V val)
        {
            Val = val;
        }

        public Node<V> Left { set; get; }
        public Node<V> Right { set; get; }
        public V Val { set; get; }
    }
}