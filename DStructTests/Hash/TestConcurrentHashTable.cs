using System.Threading;
using DStruct.Hash;
using DStruct.List;
using NUnit.Framework;

namespace DStructTests.Hash;

public class TestConcurrentHashTable
{
    [Test]
    public void Should_Put_While_Get()
    {
        var hashTable = new ConcurrentHashTable<int, int>(1000, 8);

        const int threadCount = 8;
        const int elemCount = 1000;

        var threads = new ArrayList<Thread>();
        for (var i = 0; i < threadCount; i++)
        {
            var val = i;
            threads.PushBack(new Thread(() =>
            {
                for (var j = 0; j < elemCount; j++)
                {
                    hashTable.Put(j + 10_000 * val, val);
                }
            }));
        }
        
        for (var i = 0; i < threadCount; i++)
        {
            var val = i;
            threads.PushBack(new Thread(() =>
            {
                for (var j = 0; j < elemCount; j++)
                {
                    _ = hashTable.Get(j + 10_000 * val);
                }
            }));
        }

        foreach (var thread in threads.GetEnumerable())
        {
            thread.Start();
        }
        foreach (var thread in threads.GetEnumerable())
        {
            thread.Join();
        }

        for (var i = 0; i < threadCount; i++)
        {
            for (var j = 0; j < elemCount; j++)
            {
                var val = hashTable.Get(j + 10_000 * i);
                Assert.That(val, Is.EqualTo(i));
            }
        }
        
        Assert.That(hashTable.Size, Is.EqualTo(8000));
    }
    
    [Test]
    public void Should_Put_While_Traverse()
    {
        var hashTable = new ConcurrentHashTable<int, int>(1000, 8);
        
        const int threadCount = 8;
        const int elemCount = 1000;

        var threads = new ArrayList<Thread>();
        for (var i = 0; i < threadCount; i++)
        {
            var val = i;
            threads.PushBack(new Thread(() =>
            {
                for (var j = 0; j < elemCount; j++)
                {
                    hashTable.Put(j + 10_000 * val, val);
                }
            }));
        }
        
        for (var i = 0; i < threadCount; i++)
        {
            threads.PushBack(new Thread(() =>
            {
                foreach (var _ in hashTable.Pairs())
                {
                }
            }));
        }

        foreach (var thread in threads.GetEnumerable())
        {
            thread.Start();
        }
        foreach (var thread in threads.GetEnumerable())
        {
            thread.Join();
        }

        for (var i = 0; i < threadCount; i++)
        {
            for (var j = 0; j < elemCount; j++)
            {
                var val = hashTable.Get(j + 10_000 * i);
                Assert.That(val, Is.EqualTo(i));
            }
        }
        
        Assert.That(hashTable.Size, Is.EqualTo(8000));
    }
}