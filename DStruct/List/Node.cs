namespace DStruct.List;

public class Node<T>(T value)
{
    public Node<T> Next { set; get; }
    public T Value { set; get; } = value;
}
