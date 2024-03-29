using System.Collections.Generic;

namespace DStruct.queue
{
    public interface IPriorityQueue<E>
    {
        void Push(E e);

        E Peek();

        E Pop();

        void Remove(int index);

        void Remove(E e);

        E PopPush(E e);

        bool IsEmpty();

        IEnumerable<E> GetEnumerable();

        void Clear();
    }
}