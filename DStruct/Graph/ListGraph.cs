using System.Collections.Generic;
using DStruct.Hash;
using DStruct.List;

namespace DStruct.Graph;

public class ListGraph<T> : IGraph<T>
{
    private readonly HashTable<T, List.IList<T>> _adjacencyList = new();

    public void AddVertex(T vertex)
    {
        if (_adjacencyList.Contains(vertex))
        {
            throw new DuplicateVertexException();
        }
        _adjacencyList.Put(vertex, new ArrayList<T>());
    }

    public void AddDirectedEdge(Edge<T> edge)
    {
        var neighborsFrom = _adjacencyList.Get(edge.From);
        var neighborsTo = _adjacencyList.Get(edge.To);
        if (neighborsFrom == null || neighborsTo == null)
        {
            throw new MissingVertexException();
        }
        neighborsFrom.PushBack(edge.To);
        neighborsTo.PushBack(edge.From);
    }

    public void AddUndirectedEdge(Edge<T> edge)
    {
        var neighborsFrom = _adjacencyList.Get(edge.From);
        if (neighborsFrom == null)
        {
            throw new MissingVertexException();
        }
        neighborsFrom.PushBack(edge.To);
    }

    public bool ContainsVertex(T vertex)
    {
        return _adjacencyList.Contains(vertex);
    }

    public bool ContainsEdge(Edge<T> edge)
    {
        var neighborsFrom = _adjacencyList.Get(edge.From);
        if (neighborsFrom == null)
        {
            throw new MissingVertexException();
        }
        return neighborsFrom.Contains(edge.To);
    }

    public IEnumerable<T> Neighbors(T vertex)
    {
        return _adjacencyList.Get(vertex).GetEnumerable();
    }

    public IEnumerable<T> Vertices()
    {
        return _adjacencyList.Keys();
    }
    
    public IEnumerable<Edge<T>> Edges()
    {
        var keys = _adjacencyList.Keys();
        foreach (var key in keys)
        {
            foreach (var value in _adjacencyList.Get(key).GetEnumerable())
            {
                yield return new Edge<T>(key, value);
            }
        }
    }
}