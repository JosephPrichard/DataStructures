using System.Collections.Generic;

namespace DStruct.Tree;

public interface ISearchTree<K, V> : IMap<K, V>
{
    IEnumerable<V> RangeSearch(K lower, K upper);

    V Min();

    V Max();

    int Rank(K key);

    int Number(K key);
}