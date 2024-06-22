namespace DStruct.Map.Tree;

public class Node<TKey, TValue>(Node<TKey, TValue> left, Node<TKey, TValue> right, Node<TKey, TValue> parent, KvPair<TKey, TValue> data, int height)
{
    public Node(TKey key, TValue val) : this(null, null, null, new KvPair<TKey, TValue>(key, val), 0)
    {
    }

    public Node(KvPair<TKey, TValue> data) : this(null, null, null, data, 0)
    {
    }

    public Node<TKey, TValue> Left { set; get; } = left;
    public Node<TKey, TValue> Right { set; get; } = right;
    public Node<TKey, TValue> Parent { set; get; } = parent;
    public KvPair<TKey, TValue> Data { set; get; } = data;

    public TKey Key => Data.Key;
    public TValue Value => Data.Value;

    public int Height { set; get; } = height;
}