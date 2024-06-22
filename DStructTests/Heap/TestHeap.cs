using System;
using System.Collections;
using System.Collections.Generic;
using DStruct.Heap;
using NUnit.Framework;

namespace DStructTests.Heap;

[TestFixture]
public class TestHeap
{
    [Test]
    public void Should_Push_Then_Pop()
    {
        var heap = new Heap<int>(PriorityType.Min);
        heap.Push(22);
        heap.Push(3);
        heap.Push(62);
        heap.Push(-701);
        heap.Push(5);
        heap.Push(-21);

        var expected = new[] { -701, -21, 3, 5, 22, 62 };
        foreach (var exp in expected)
        {
            Assert.That(exp, Is.EqualTo(heap.Pop()));
        }
    }

    [Test]
    public void Should_Push_InOrder()
    {
        var random = new Random();

        var heap = new Heap<int>(PriorityType.Min);

        var inputs = new List<int>();
        for (var i = 0; i < 100; i++)
        {
            var e = random.Next(-1000, 1000);
            inputs.Add(e);
        }

        foreach (var e in inputs)
        {
            heap.Push(e);
        }

        var results = new ArrayList();
        while (!heap.IsEmpty())
        {
            results.Add(heap.Pop());
        }

        Assert.That(results, Is.Ordered);
    }

    [Test]
    public void Should_PopPush_Then_Peek()
    {
        var random = new Random();

        var heap = new Heap<int>(PriorityType.Min);
        heap.Push(1);

        var prev = 1;
        for (var i = 0; i < 100; i++)
        {
            var e = random.Next(-1000, 1000);
            var r1 = heap.PopPush(e);
            var r2 = heap.Peek();

            Assert.That(r1, Is.EqualTo(prev));
            Assert.That(r2, Is.EqualTo(e));

            prev = e;
        }
    }
}