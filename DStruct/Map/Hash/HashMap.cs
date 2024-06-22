using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DStruct.List.Linked;

namespace DStruct.Map.Hash;

public class HashMap<TKey, TValue>(int predictedCount) : IMap<TKey, TValue>
{
    private Node<KvPair<TKey, TValue>>[] _table = new Node<KvPair<TKey, TValue>>[predictedCount * 2 + 1];

    public HashMap() : this(15)
    {
    }

    public int Size { private set; get; }

    public TValue this[TKey key]
    {
        get => Get(key);
        set => Put(key, value);
    }

    public void Put(TKey key, TValue val)
    {
        var hash = Hash(key);
        var head = _table[hash];
        
        var pair = new KvPair<TKey, TValue>(key, val);
        if (head == null)
        {
            _table[hash] = new Node<KvPair<TKey, TValue>>(pair);
        }
        else
        {
            var prev = head;
            while (head != null)
            {
                if (head.Data.Key.Equals(key)) head.Data = pair;

                prev = head;
                head = head.Next;
            }

            prev.Next = new Node<KvPair<TKey, TValue>>(pair);
        }

        Size++;

        if (Size >= 8 * _table.Length / 10)
        {
            Rehash();
        }
    }
    
    private void Rehash()
    {
        var prevTable = _table;
        _table = new Node<KvPair<TKey, TValue>>[prevTable.Length * 2 + 1];

        foreach (var node in prevTable)
        {
            var curr = node;
            while (curr != null)
            {
                var next = curr.Next;
                var hash = Hash(curr.Data.Key);
                var head = _table[hash];
                
                if (head == null)
                {
                    curr.Next = null;
                    _table[hash] = curr;
                }
                else
                {
                    curr.Next = curr;
                    _table[hash] = curr;
                }

                curr = next;
            }
        }
    }

    private int Hash(TKey key)
    {
        return Math.Abs(key.GetHashCode() % _table.Length);
    }

    public TValue Get(TKey key)
    {
        var pair = GetPair(key);
        return pair != null ? pair.Data.Value : default;
    }
    
    private Node<KvPair<TKey, TValue>> GetPair(TKey key)
    {
        var head = _table[Hash(key)];
        while (head != null)
        {
            if (head.Data.Key.Equals(key)) return head;

            head = head.Next;
        }

        return null;
    }

    public bool Remove(TKey key)
    {
        var hash = Hash(key);
        var head = _table[hash];
        var prev = head;
        if (head != null)
        {
            if (head.Data.Key.Equals(key))
            {
                _table[hash] = head.Next;
                Size--;
                return true;
            }

            head = head.Next;
            while (head != null)
            {
                if (head.Data.Key.Equals(key))
                {
                    prev.Next = head.Next;
                    Size--;
                    return true;
                }

                prev = head;
                head = head.Next;
            }
        }

        return false;
    }

    public bool Contains(TKey key)
    {
        return GetPair(key) != null;
    }

    public void Clear()
    {
        for (var i = 0; i < _table.Length; i++) _table[i] = null;

        Size = 0;
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public IEnumerable<TKey> Keys()
    {
        return Pairs().Select(pair => pair.Key);
    }

    public IEnumerable<TValue> Values()
    {
        return Pairs().Select(pair => pair.Value);
    }

    public IEnumerable<KvPair<TKey, TValue>> Pairs()
    {
        foreach (var node in _table)
        {
            var head = node;
            while (head != null)
            {
                yield return head.Data;
                head = head.Next;
            }
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerator<KvPair<TKey, TValue>> GetEnumerator()
    {
        return (IEnumerator<KvPair<TKey, TValue>>) Pairs();
    }
}