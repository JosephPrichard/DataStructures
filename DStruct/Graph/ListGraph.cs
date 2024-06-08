using System.Collections.Generic;
using DStruct.Hash;
using DStruct.List;

namespace DStruct.Graph
{
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

        public void AddDirectedEdge(T from, T to)
        {
            var neighborsFrom = _adjacencyList.Get(from);
            var neighborsTo = _adjacencyList.Get(to);
            if (neighborsFrom == null || neighborsTo == null)
            {
                throw new MissingVertexException();
            }
            neighborsFrom.PushBack(to);
            neighborsTo.PushBack(from);
        }

        public void AddUndirectedEdge(T from, T to)
        {
            var neighborsFrom = _adjacencyList.Get(from);
            if (neighborsFrom == null)
            {
                throw new MissingVertexException();
            }
            neighborsFrom.PushBack(to);
        }

        public bool ContainsVertex(T vertex)
        {
            return _adjacencyList.Contains(vertex);
        }

        public bool ContainsEdge(T from, T to)
        {
            var neighborsFrom = _adjacencyList.Get(from);
            if (neighborsFrom == null)
            {
                throw new MissingVertexException();
            }
            return neighborsFrom.Contains(to);
        }

        public IEnumerable<T> Neighbors(T vertex)
        {
            return _adjacencyList.Get(vertex).GetEnumerable();
        }

        public IEnumerable<T> Vertices()
        {
            return _adjacencyList.Keys();
        }
    }
}