using System;
using System.Collections.Generic;

namespace DStruct
{
    public interface ICollection<T>
    {
        int Size { get; }

        void PushFront(T e);

        T PeekFront();

        T PopFront();

        void AddAll(ICollection<T> list);

        IEnumerable<T> GetEnumerable();

        bool IsEmpty();

        void Clear();

        bool Contains(T e);
    }
}