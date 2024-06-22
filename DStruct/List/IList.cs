using System.Collections;
using System.Collections.Generic;

namespace DStruct.List;

public interface IList<T> : IEnumerable<T>
{
    int Size { get; }

    bool IsEmpty();

    void RemoveAll();

    bool Contains(T e);
    
    T this[int index] { set; get; }

    void PushBack(T e);

    T PopBack();

    T PeekBack();
    
    void PushFront(T e);

    T PeekFront();

    T PopFront();

    void Remove(int index);

    void Insert(int index, T e);
    
    void AddAll(IList<T> list);

    IEnumerable<T> Elements();
}