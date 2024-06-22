using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DStruct.Map.Hash;

internal enum Condition
{
    Empty, Full, Tombstone
}

internal struct Bucket<TKey, TValue>(TKey key, TValue value)
{
    public KvPair<TKey, TValue> Data { get; set; } = new(key, value);
    public Condition Cond { get; set; } = Condition.Empty;
}

public class FlatHashMap<TKey, TValue>(int predictedCount) : IMap<TKey, TValue>
{
    private Bucket<TKey, TValue>[] _table = new Bucket<TKey, TValue>[predictedCount * 2 + 1];
    
    public FlatHashMap() : this(15)
    {
    }
    
    public int Size { get; set; }

    public TValue this[TKey key]
    {
        get => Get(key);
        set => Put(key, value);
    }

    public void Put(TKey key, TValue val)
    {
        Insert(key, val);
        Size++;
        
        if (Size >= 5 * _table.Length / 10)
        {
            Rehash();
        }
    }

    private void Insert( TKey key, TValue val)
    {
        var index = Hash(key);
        
        while (true)
        {
            if (_table[index].Cond == Condition.Empty || 
                _table[index].Cond == Condition.Full && _table[index].Data.Key.Equals(key))
            {
                break;
            }
            
            index++;
            if (index >= _table.Length)
            {
                index = 0;
            }
        }

        _table[index].Cond = Condition.Full;
        _table[index].Data = new KvPair<TKey, TValue>(key, val);
    }
    
    private void Rehash()
    {
        var prevTable = _table;
        _table = new Bucket<TKey, TValue>[prevTable.Length * 2 + 1];

        foreach (var bucket in prevTable)
        {
            if (bucket.Cond == Condition.Full)
            {
                Insert(bucket.Data.Key, bucket.Data.Value);
            }
        }
    }

    private int Hash(TKey key)
    {
        return Math.Abs(key.GetHashCode() % _table.Length);
    }

    public TValue Get(TKey key)
    {
        var index = Hash(key);
        
        while (true)
        {
            if (_table[index].Cond == Condition.Empty)
            {
                return default;
            }
            if (_table[index].Cond == Condition.Full && _table[index].Data.Key.Equals(key))
            {
                return _table[index].Data.Value;
            }
            
            index++;
            if (index >= _table.Length)
            {
                index = 0;
            }
        }
    }

    public bool Remove(TKey key)
    {
        var index = Hash(key);
        
        while (true)
        {
            if (_table[index].Cond == Condition.Empty)
            {
                return false;
            }
            if (_table[index].Cond == Condition.Full && _table[index].Data.Key.Equals(key))
            {
                _table[index].Data = default;
                _table[index].Cond = Condition.Tombstone;
                Size--;
                return true;
            }
            
            index++;
            if (index >= _table.Length)
            {
                index = 0;
            }
        }
    }

    public bool Contains(TKey key)
    {
        var index = Hash(key);
        
        while (true)
        {
            if (_table[index].Cond == Condition.Empty)
            {
                return false;
            }
            if (_table[index].Cond == Condition.Full && _table[index].Data.Key.Equals(key))
            {
                return true;
            }
            
            index++;
            if (index >= _table.Length)
            {
                index = 0;
            }
        }
    }

    public void Clear()
    {
        for (var i = 0; i < _table.Length; i++)
        {
            _table[i].Data = default;
            _table[i].Cond = Condition.Empty;
        }

        Size = 0;
    }

    public bool IsEmpty()
    {
        return Size <= 0;
    }

    public IEnumerable<TKey> Keys()
    {
        return Pairs().Select(data => data.Key);
    }

    public IEnumerable<TValue> Values()
    {
        return Pairs().Select(data => data.Value);
    }

    public IEnumerable<KvPair<TKey, TValue>> Pairs()
    {
        return _table.Where(bucket => bucket.Cond == Condition.Full).Select(bucket => bucket.Data);
    }
    
    public IEnumerator<KvPair<TKey, TValue>> GetEnumerator()
    {
        return (IEnumerator<KvPair<TKey, TValue>>) Pairs();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}