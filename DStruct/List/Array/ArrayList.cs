using System.Collections;
using System.Collections.Generic;

namespace DStruct.List.Array;

public class ArrayList<T> : IList<T>
{
    private T[] _elements;

    public ArrayList() : this(0)
    {
    }

    public ArrayList(int capacity)
    {
        Size = 0;
        _elements = new T[capacity];
    }

    public ArrayList(IReadOnlyList<T> elems)
    {
        Size = elems.Count;
        _elements = new T[Size];
        for (var i = 0; i < Size; i++) _elements[i] = elems[i];
    }

    public int Size { get; private set; }

    public T this[int index]
    {
        get
        {
            RangeCheck(index);
            return _elements[index];
        }
        set
        {
            RangeCheck(index);
            _elements[index] = value;
        }
    }

    // O(n)
    public void PushFront(T e)
    {
        EnsureCapacity(Size + 1);
        System.Array.Copy(_elements, 0, _elements, 1, Size);
        _elements[0] = e;
        Size++;
    }

    // O(1)
    public T PeekFront()
    {
        return this[0];
    }

    // O(n)
    public T PopFront()
    {
        var value = this[0];
        Remove(0);
        return value;
    }

    // O(1) amortized
    public void PushBack(T e)
    {
        EnsureCapacity(Size + 1);
        _elements[Size] = e;
        Size++;
    }

    // O(1)
    public T PeekBack()
    {
        if (Size <= 0)
        {
            throw new EmptyException();
        }
        return _elements[Size - 1];
    }

    // O(1)
    public T PopBack()
    {
        if (Size <= 0)
        {
            throw new EmptyException();
        }
        var value = _elements[Size - 1];
        _elements[Size - 1] = default;
        Size--;
        return value;
    }

    // O(n)
    public void Remove(int index)
    {
        RangeCheck(index);
        Size--;
        
        System.Array.Copy(_elements, index + 1, _elements, index, Size - index);
        _elements[Size] = default;
    }

    // O(n)
    public void Insert(int index, T e)
    {
        RangeCheck(index);
        EnsureCapacity(Size + 1);
        
        System.Array.Copy(_elements, index, _elements, index + 1, Size - index);
        _elements[index] = e;
        Size++;
    }
    
    // O(n)
    public IEnumerable<T> Elements()
    {
        for (var i = 0; i < Size; i++) yield return _elements[i];
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return (IEnumerator<T>) Elements();
    }
    
    // O(n)
    public void AddAll(IList<T> list)
    {
        EnsureCapacity(Size + list.Size);
        var i = 0;
        foreach (var e in list.Elements())
        {
            _elements[i + Size] = e;
            i++;
        }

        Size += list.Size;
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public void Clear()
    {
        System.Array.Clear(_elements, 0, _elements.Length);
    }

    public void RemoveAll()
    {
        Size = 0;
        _elements = System.Array.Empty<T>();
    }

    // O(n)
    public bool Contains(T e)
    {
        for (var i = 0; i < Size; i++)
            if (e.Equals(this[i]))
                return true;
        return false;
    }

    public T[] ToArray()
    {
        var copy = new T[Size];
        System.Array.Copy(_elements, 0, copy, 0, Size);
        return copy;
    }

    private void EnsureCapacity(int size)
    {
        var capacity = _elements.Length;
        if (size < capacity) return;

        var resizedElements = new T[size - capacity + 3 * capacity / 2];
        System.Array.Copy(_elements, 0, resizedElements, 0, Size);
        _elements = resizedElements;
    }

    private void RangeCheck(int index)
    {
        if (index >= Size || index < 0) throw new OutOfRangeException();
    }
}