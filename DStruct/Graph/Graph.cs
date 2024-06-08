using System.Collections.Generic;

namespace DStruct.Graph
{
    public interface IGraph<T>
    {
        void AddVertex(T vertex);
        
        void AddDirectedEdge(T from, T to);
        
        void AddUndirectedEdge(T from, T to);

        bool ContainsVertex(T vertex);
        
        bool ContainsEdge(T from, T to);

        IEnumerable<T> Neighbors(T from);

        IEnumerable<T> Vertices();
    }
}