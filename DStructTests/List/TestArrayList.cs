using DStruct.List;
using NUnit.Framework;

namespace DStructTests.List;

public class TestArrayList
{
    [Test]
    public void Should_Push()
    {
        var list = new ArrayList<int>(5);
        list.PushFront(1);
        list.PushBack(2);
        list.PushFront(5);
        list.PushFront(6);
        list.PushBack(21);
        list.PushFront(7);
        list.PushBack(11);
        list.PushBack(6);

        var expected = new[] { 7, 6, 5, 1, 2, 21, 11, 6 };
        Assert.That(list.GetEnumerable(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_Insert()
    {
        var list = new ArrayList<int>(new[] { 1, 2, 3, 4, 5, 6 });
        list.Insert(2, 10);
        list.Insert(4, 11);

        var expected = new[] { 1, 2, 10, 3, 11, 4, 5, 6 };
        Assert.That(list.GetEnumerable(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_Remove()
    {
        var list = new ArrayList<int>(new[] { 1, 2, 3, 4, 5, 6 });

        list.Remove(3);
        list.Remove(1);
        list.Remove(0);

        var expected = new[] { 3, 5, 6 };
        Assert.That(list.GetEnumerable(), Is.EqualTo(expected));

        list.Remove(2);

        expected = new[] { 3, 5 };
        Assert.That(list.GetEnumerable(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_AddAll()
    {
        var list = new ArrayList<int>(new[] { 1, 2, 3 });

        // 4, 5, 6
        var list1 = new ArrayList<int>();
        list1.PushFront(5);
        list1.PushBack(6);
        list1.PushFront(4);

        list1.AddAll(list1);
        list.AddAll(list1);

        var expected = new[] { 1, 2, 3, 4, 5, 6, 4, 5, 6 };
        Assert.That(list.GetEnumerable(), Is.EqualTo(expected));
    }
}