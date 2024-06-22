using System;
using System.Collections.Generic;
using DStruct.List;

namespace DStruct.Heap;

public class Heap<TElem> : IPriorityQueue<TElem> where TElem : IComparable
{
    private Func<TElem, TElem, bool> _doCompare;
    private TElem[] _elements;

    public Heap(PriorityType type)
    {
        SetHeapType(type);
        _elements = new TElem[20];
    }

    public Heap(PriorityType type, int capacity)
    {
        SetHeapType(type);
        _elements = new TElem[capacity];
    }

    public Heap(PriorityType type, TElem[] array)
    {
        SetHeapType(type);

        _elements = new TElem[array.Length];
        Array.Copy(array, 0, _elements, 0, array.Length);
        Size = array.Length;
        ReOrder();
    }

    public int Size { private set; get; }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    //O(1) due to probability
    public void Push(TElem e)
    {
        EnsureCapacity(Size + 1);
        _elements[Size] = e;
        SiftUp(Size);
        Size++;
    }

    // O(1)
    public TElem Peek()
    {
        EmptyCheck();
        return _elements[0];
    }

    // O(log(n))
    public TElem Pop()
    {
        EmptyCheck();
        var val = _elements[0];
        Move(Size - 1, 0);
        Size--;
        SiftDown(0);
        return val;
    }

    // O(log(n))
    public void Remove(int index)
    {
        EmptyCheck();
        RangeCheck(index);
        Swap(Size - 1, index);
        Size--;
        SiftDown(index);
    }

    public void Remove(TElem e)
    {
        for (var i = 0; i < Size; i++)
            if (_elements[i].Equals(e))
            {
                Remove(i);
                return;
            }
    }

    // O(log(n))
    public TElem PopPush(TElem e)
    {
        EmptyCheck();
        var val = _elements[0];
        _elements[0] = e;
        SiftDown(0);
        return val;
    }

    // o(n)
    public IEnumerable<TElem> GetEnumerable()
    {
        for (var i = 0; i < Size; i++) yield return _elements[i];
    }

    public void Clear()
    {
        Size = 0;
        _elements = Array.Empty<TElem>();
    }

    private void SetHeapType(PriorityType type)
    {
        if (type == PriorityType.Min)
            _doCompare = (ele1, ele2) => ele1.CompareTo(ele2) == -1;
        else
            _doCompare = (ele1, ele2) => ele1.CompareTo(ele2) == 1;
    }

    public int Depth()
    {
        return (int)Math.Floor(Math.Log(Size, 2)) + 1;
    }

    public TElem[] Copy()
    {
        var copy = new TElem[Size];
        Array.Copy(_elements, 0, copy, 0, Size);
        return copy;
    }

    //O(n)
    private void ReOrder()
    {
        for (var i = Size - 1; i >= 0; i--) SiftDown(i);
    }

    private void EmptyCheck()
    {
        if (Size == 0) throw new EmptyHeapException();
    }

    private void RangeCheck(int index)
    {
        if (index >= Size || index < 0) throw new OutOfRangeException();
    }

    private void SiftUp(int pos)
    {
        var parent = (pos - 1) / 2;
        while (parent >= 0)
            if (_doCompare(_elements[pos], _elements[parent]))
            {
                Swap(pos, parent);
                pos = parent;
                parent = (pos - 1) / 2;
            }
            else
            {
                parent = -1;
            }
    }

    private void SiftDown(int pos)
    {
        while (true)
        {
            var left = 2 * pos + 1;
            var right = 2 * pos + 2;
            if (left >= Size) return;

            var child = right >= Size || _doCompare(_elements[left], _elements[right]) ? left : right;
            if (_doCompare(_elements[child], _elements[pos]))
            {
                Swap(child, pos);
                pos = child;
                continue;
            }

            break;
        }
    }

    private void EnsureCapacity(int size)
    {
        var capacity = _elements.Length;
        if (size < capacity) return;

        var resizedElements = new TElem[size - capacity + capacity * 2];
        Array.Copy(_elements, 0, resizedElements, 0, Size);
        _elements = resizedElements;
    }

    private void Swap(int index1, int index2)
    {
        var val = _elements[index1];
        _elements[index1] = _elements[index2];
        _elements[index2] = val;
    }

    private void Move(int from, int to)
    {
        _elements[to] = _elements[from];
    }
}