using System;
using System.Collections.Generic;
using System.Linq;
using DStruct.List;

namespace DStruct.Hash;

public class HashTable<TKey, TValue>(int predictedCount) : IMap<TKey, TValue>
{
    private readonly Node<KvPair<TKey, TValue>>[] _table = new Node<KvPair<TKey, TValue>>[predictedCount * 2];

    public HashTable() : this(17)
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
        var h = Hash(key);
        var head = _table[h];
        var pair = new KvPair<TKey, TValue>(key, val);
        if (head == null)
        {
            _table[h] = new Node<KvPair<TKey, TValue>>(pair);
        }
        else
        {
            var prev = head;
            while (head != null)
            {
                if (head.Value.Key.Equals(key))
                {
                    head.Value = pair;
                }

                prev = head;
                head = head.Next;
            }

            prev.Next = new Node<KvPair<TKey, TValue>>(pair);
        }

        Size++;
    }

    public TValue Get(TKey key)
    {
        var pair = GetPair(key);
        return pair is { HasValue: true } ? pair.Value : default;
    }

    public bool Remove(TKey key)
    {
        var h = Hash(key);
        var head = _table[h];
        var prev = head;
        if (head != null)
        {
            if (head.Value.Key.Equals(key))
            {
                _table[h] = head.Next;
                Size--;
                return true;
            }

            head = head.Next;
            while (head != null)
            {
                if (head.Value.Key.Equals(key))
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
        var pair = GetPair(key);
        return pair is { HasValue: true };
    }

    public void Clear()
    {
        for (var i = 0; i < _table.Length; i++)
        {
            _table[i] = null;
        }

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
        foreach (var n in _table)
        {
            var head = n;
            while (head != null)
            {
                yield return head.Value;
                head = head.Next;
            }
        }
    }

    private KvPair<TKey, TValue> GetPair(TKey key)
    {
        var head = _table[Hash(key)];
        while (head != null)
        {
            if (head.Value.Key.Equals(key))
            {
                return head.Value;
            }

            head = head.Next;
        }

        return null;
    }

    private int Hash(TKey key)
    {
        return Math.Abs(key.GetHashCode() % _table.Length);
    }
}