using System;

namespace DataStructures.tree
{
    public interface IHeap<E>
    {
        int GetDepth();

        bool IsEmpty();

        void Push(E e);

        E Peek();

        E Pop();

        E PopPush(E e);

        bool Contains(E e, Func<E, E, bool> compare);

    }
}