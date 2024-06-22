using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DStruct.List.Array;

namespace DStruct.Map.Hash;

internal class Shard<TKey, TValue>(ReaderWriterLockSlim rwLock, IMap<TKey, TValue> map)
{
    public ReaderWriterLockSlim Lock { get; set; } = rwLock;
    public IMap<TKey, TValue> Map { get; set; } = map;
}

public class ConcurrentMap<TKey, TValue> : IMap<TKey, TValue>
{
    private readonly ArrayList<Shard<TKey, TValue>> _shards = new();
    private int _size;

    public ConcurrentMap(int shardCount, Func<IMap<TKey, TValue>> func)
    {
        for (var i = 0; i < shardCount; i++)
        {
            var mapShard = new Shard<TKey, TValue>(new ReaderWriterLockSlim(), func());
            _shards.PushBack(mapShard);
        }
    }
    
    public ConcurrentMap(int predictedCount, int shardCount)
    {
        for (var i = 0; i < shardCount; i++)
        {
            var mapShard = new Shard<TKey, TValue>(
                new ReaderWriterLockSlim(), new FlatHashMap<TKey, TValue>(predictedCount));
            _shards.PushBack(mapShard);
        }
    }

    public ConcurrentMap() : this(15, 8)
    {
    }

    public int Size => Thread.VolatileRead(ref _size);

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
            shard.Lock.EnterWriteLock();
            shard.Map.Put(key, val);
        }
        finally
        {
            shard.Lock.ExitWriteLock();
            Interlocked.Increment(ref _size);
        }
    }

    public TValue Get(TKey key)
    {
        var h = Hash(key);
        var shard = _shards[h];

        try
        {
            shard.Lock.EnterReadLock();
            return shard.Map.Get(key);
        }
        finally
        {
            shard.Lock.ExitReadLock();
        }
    }

    public bool Remove(TKey key)
    {
        var h = Hash(key);
        var shard = _shards[h];

        try
        {
            shard.Lock.EnterWriteLock();
            return shard.Map.Remove(key);
        }
        finally
        {
            shard.Lock.ExitWriteLock();
            Interlocked.Decrement(ref _size);
        }
    }

    public bool Contains(TKey key)
    {
        var h = Hash(key);
        var shard = _shards[h];

        try
        {
            shard.Lock.EnterReadLock();
            return shard.Map.Contains(key);
        }
        finally
        {
            shard.Lock.ExitReadLock();
        }
    }

    public void Clear()
    {
        foreach (var shard in _shards.Elements())
            try
            {
                shard.Lock.EnterWriteLock();
                shard.Map.Clear();
            }
            finally
            {
                shard.Lock.ExitWriteLock();
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
        foreach (var shard in _shards.Elements())
            try
            {
                shard.Lock.EnterWriteLock();
                foreach (var pair in shard.Map.Pairs()) yield return pair;
            }
            finally
            {
                shard.Lock.ExitWriteLock();
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

    private int Hash(TKey key)
    {
        return Math.Abs(~key.GetHashCode() % _shards.Size);
    }
}