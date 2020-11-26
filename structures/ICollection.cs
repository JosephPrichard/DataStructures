using System;
using System.Collections.Generic;

namespace DataStructures.structures
{
    public interface ICollection<E>
    {
        int Size { get; }

        void Push(E e);

        E Peek();

        E Pop();

        void AddAll(ICollection<E> list);

        IEnumerable<E> GetEnumerable();

        bool IsEmpty();

        void Clear();

        bool Contains(E e, Func<E, E, bool> equals);
    }
}