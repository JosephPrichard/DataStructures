using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DStruct.List;

namespace DStruct.Hash;

public class ConcurrentHashTable<TKey, TValue> : IMap<TKey, TValue>
{
    private readonly ArrayList<HtShard<TKey, TValue>> _shards = new();
    
    public ConcurrentHashTable(int predictedCount, int shardCount)
    {
        for (var i = 0; i < shardCount; i++)
        {
            _shards.PushBack(new HtShard<TKey, TValue>(
                new ReaderWriterLock(), new HashTable<TKey, TValue>(predictedCount)));
        }
    }
    
    public ConcurrentHashTable() : this(17, 8)
    {
    }

    private int _size;
    
    public int Size => _size;

    public TValue this[TKey key]
    {
        get => Get(key);
        set => Put(key, value);
    }

    public void Put(TKey key, TValue val)
    {
        var h = Hash(key);
        var shard = _shards[h];

        try
        {
            shard.Lock.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            shard.HashTable.Put(key, val);
        }
        finally
        {
            shard.Lock.ReleaseWriterLock();
            Interlocked.Increment(ref _size);
        }
    }

    public TValue Get(TKey key)
    {
        var h = Hash(key);
        var shard = _shards[h];

        try
        {
            shard.Lock.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            return shard.HashTable.Get(key);
        }
        finally
        {
            shard.Lock.ReleaseReaderLock();
        }
    }

    public bool Remove(TKey key)
    {
        var h = Hash(key);
        var shard = _shards[h];

        try
        {
            shard.Lock.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            return shard.HashTable.Remove(key);
        }
        finally
        {
            shard.Lock.ReleaseWriterLock();
            Interlocked.Decrement(ref _size);
        }
    }

    public bool Contains(TKey key)
    {
        var h = Hash(key);
        var shard = _shards[h];

        try
        {
            shard.Lock.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            return shard.HashTable.Contains(key);
        }
        finally
        { 
            shard.Lock.ReleaseReaderLock();
        }
    }

    public void Clear()
    {
        foreach (var shard in _shards.GetEnumerable())
        {
            try
            {
                shard.Lock.AcquireWriterLock(Timeout.InfiniteTimeSpan);
                shard.HashTable.Clear();
            }
            finally
            {
                shard.Lock.ReleaseWriterLock();
            }
        }
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
        foreach (var shard in _shards.GetEnumerable())
        {
            try
            {
                shard.Lock.AcquireReaderLock(Timeout.InfiniteTimeSpan);
                foreach (var pair in shard.HashTable.Pairs())
                {
                    yield return pair;
                }
            }
            finally
            {
                shard.Lock.ReleaseReaderLock();
            }
        }
    }

    private int Hash(TKey key)
    {
        return _shards.Size - 1 - Math.Abs(key.GetHashCode() % _shards.Size);
    }
}