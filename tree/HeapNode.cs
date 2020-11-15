namespace DataStructures.tree
{
    public class BNode<T>
    {
        public BNode(T val) {
            Val = val;
        }

        public BNode<T> Left { set; get; }

        public BNode<T> Right { set; get; }

        public T Val { set; get; }

    }
}