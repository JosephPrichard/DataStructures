using System.Collections.Generic;

namespace DStruct.Heap;

public interface IPriorityQueue<T>
{
    void Push(T e);

    T Peek();

    T Pop();

    void Remove(int index);

    void Remove(T e);

    T PopPush(T e);

    bool IsEmpty();

    IEnumerable<T> GetEnumerable();

    void Clear();
}