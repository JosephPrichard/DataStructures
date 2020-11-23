namespace DataStructures.structures.binaryTree
{
    public class Node<T>
    {
        public Node<T> Left { set; get; }
        public Node<T> Right { set; get; }
        public T Val { set; get; }
        
        public Node(T val) {
            Val = val;
        }

    }
}