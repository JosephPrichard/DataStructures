using System;

namespace DataStructures.heap
{
    public interface IHeap<E>
    {
        int Depth();

        bool IsEmpty();

        void Push(E e);

        E Peek();

        E Pop();

        E PopPush(E e);

    }
}