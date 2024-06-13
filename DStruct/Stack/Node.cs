namespace DStruct.Stack;

public class Node<T>(T value)
{
    public Node<T> Next { set; get; }
    public T Value { get; } = value;
}