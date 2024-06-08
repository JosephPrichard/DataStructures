using System.Collections.Generic;

namespace DStruct.Graph;

public class Edge<T>(T from, T to)
{
    public T From { get; } = from;
    public T To { get; } = to;

    public bool Equals(Edge<T> other)
    {
        return EqualityComparer<T>.Default.Equals(From, other.From) && EqualityComparer<T>.Default.Equals(To, other.To);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Edge<T>) obj);
    }

    public override int GetHashCode()
    {
        return From.GetHashCode() * 17 + To.GetHashCode();
    }
}