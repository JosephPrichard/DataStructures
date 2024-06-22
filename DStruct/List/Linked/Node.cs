namespace DStruct.List.Linked;

public class Node<T>(T data)
{
    public Node<T> Next { set; get; }
    public T Data { set; get; } = data;
}