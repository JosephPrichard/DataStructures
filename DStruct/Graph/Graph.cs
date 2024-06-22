using System.Collections.Generic;

namespace DStruct.Graph;

public interface IGraph<T>
{
    void AddVertex(T vertex);

    void AddDirectedEdge(Edge<T> edge);

    void AddUndirectedEdge(Edge<T> edge);

    bool ContainsVertex(T vertex);

    bool ContainsEdge(Edge<T> edge);

    IEnumerable<T> Neighbors(T from);

    IEnumerable<T> Vertices();

    IEnumerable<Edge<T>> Edges();
}