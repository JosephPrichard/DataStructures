namespace DataStructures.linear
{
    public class Node<E>
    {
        public Node(E val) {
            Val = val;
        }

        public Node<E> Next { set; get; }

        public E Val { set; get; }
    }
}