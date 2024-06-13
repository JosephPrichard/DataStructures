using DStruct.List;
using NUnit.Framework;

namespace DStructTests.List;

public class TestSorter
{
    [Test]
    public void Should_QuickSort_ArrayList()
    {
        var list = new ArrayList<int>(new[] { 2, 4, 5, 4, 11, 6, 4, 51, 5, -4, 1, 21, 2 });
        new Sorter<int>(SortType.Asc).QuickSort(list);

        var expected = new[] { -4, 1, 2, 2, 4, 4, 4, 5, 5, 6, 11, 21, 51 };
        Assert.That(list.GetEnumerable(), Is.EqualTo(expected));
    }

    [Test]
    public void Should_MergeSort_Array()
    {
        var arr = new[] { 2, 4, 5, 4, 11, 6, 4, 51, 5, -4, 1, 21, 2 };
        new Sorter<int>(SortType.Asc).MergeSort(arr);

        var expected = new[] { -4, 1, 2, 2, 4, 4, 4, 5, 5, 6, 11, 21, 51 };
        Assert.That(arr, Is.EqualTo(expected));
    }

    [Test]
    public void Should_MergeSort_LinkedList()
    {
        var list = new LinkedList<int>();
        list.PushFront(2);
        list.PushFront(4);
        list.PushFront(5);
        list.PushFront(4);
        list.PushFront(11);
        list.PushFront(6);
        list.PushFront(4);
        list.PushFront(51);
        list.PushFront(5);
        list.PushFront(-4);
        list.PushFront(1);
        list.PushFront(21);
        list.PushFront(2);

        new Sorter<int>(SortType.Asc).MergeSort(list);

        var expected = new[] { -4, 1, 2, 2, 4, 4, 4, 5, 5, 6, 11, 21, 51 };
        Assert.That(list.GetEnumerable(), Is.EqualTo(expected));
    }
}