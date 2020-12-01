namespace DataStructures.structures.list
{
    public class Node<T>
    {
        public Node(T val)
        {
            Val = val;
        }

        public Node<T> Next { set; get; }
        public T Val { set; get; }
    }
}