using System.Collections.Generic;

namespace DStruct.Graph
{
    public interface IGraph<T>
    {
        bool Add(T vertex);

        void Connect(T from, T to);

        bool Contains(T vertex);

        IEnumerable<T> Vertices();
    }
}