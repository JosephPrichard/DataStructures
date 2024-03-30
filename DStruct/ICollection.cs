using System;
using System.Collections.Generic;

namespace DStruct
{
    public interface ICollection<E>
    {
        int Size { get; }

        void PushFront(E e);

        E PeekFront();

        E PopFront();

        void AddAll(ICollection<E> list);

        IEnumerable<E> GetEnumerable();

        bool IsEmpty();

        void Clear();

        bool Contains(E e, Func<E, E, bool> equals);
    }
}